/*
 * FilterWheelSetupDialogForm.cs
 * Copyright (C) 2022 - Present, Julien Lecomte - All Rights Reserved
 * Licensed under the MIT License. See the accompanying LICENSE file for terms.
 */

using ASCOM.Utilities;

using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace ASCOM.DarkSkyGeek
{
    // Form not registered for COM!
    [ComVisible(false)]

    public partial class FilterWheelSetupDialogForm : Form
    {
        // Holder for a reference to the driver's trace logger
        TraceLogger tl;

        public FilterWheelSetupDialogForm(TraceLogger tlDriver)
        {
            InitializeComponent();

            // Save the provided trace logger for use within the setup dialogue
            tl = tlDriver;
        }

        private void FilterWheelSetupDialogForm_Load(object sender, EventArgs e)
        {
            Profile profile = new Profile();

            // Populate filter wheel device list...
            ArrayList filterWheelDevices = profile.RegisteredDevices("FilterWheel");
            foreach (KeyValuePair kv in filterWheelDevices)
            {
                // Don't include the filter wheel proxy in the list, for obvious reasons...
                if (kv.Key != FilterWheelProxy.driverID)
                {
                    ComboboxItem item = new ComboboxItem();
                    item.Text = kv.Value;
                    item.Value = kv.Key;
                    int index = filterWheelSelectorComboBox.Items.Add(item);
                    // Select newly added item if it matches the value stored in the profile.
                    if (kv.Key == FilterWheelProxy.filterWheelId)
                    {
                        filterWheelSelectorComboBox.SelectedIndex = index;
                    }
                }
            }

            // Populate autofocus filter settings...
            for (int i = 0; i < FilterWheelProxy.MAX_FILTER_COUNT; i++)
            {
                string name = FilterWheelProxy.filterNames[i];
                int offset = FilterWheelProxy.filterOffsets[i];
                if (string.IsNullOrEmpty(name))
                {
                    filtersDataGridView.Rows.Add(i + 1, string.Empty, string.Empty);
                }
                else
                {
                    filtersDataGridView.Rows.Add(i + 1, name, offset);
                }
            }

            // Populate focuser device list...
            ArrayList focuserDevices = profile.RegisteredDevices("Focuser");
            foreach (KeyValuePair kv in focuserDevices)
            {
                ComboboxItem item = new ComboboxItem();
                item.Text = kv.Value;
                item.Value = kv.Key;
                int index = focuserSelectorComboBox.Items.Add(item);
                // Select newly added item if it matches the value stored in the profile.
                if (kv.Key == FilterWheelProxy.focuserId)
                {
                    focuserSelectorComboBox.SelectedIndex = index;
                }
            }

            backlashCompTextBox.Text = FilterWheelProxy.backlashCompSteps.ToString();
            stepRatioTextBox.Text = FilterWheelProxy.stepRatio.ToString();

            chkTrace.Checked = tl.Enabled;
        }

        private void cmdOK_Click(object sender, EventArgs e)
        {
            if (Validate())
            {
                if (filterWheelSelectorComboBox.SelectedItem != null)
                {
                    FilterWheelProxy.filterWheelId = (filterWheelSelectorComboBox.SelectedItem as ComboboxItem).Value;
                }

                for (int i = 0; i < FilterWheelProxy.MAX_FILTER_COUNT; i++)
                {
                    string name = string.Empty;
                    string offset = string.Empty;

                    if (filtersDataGridView.Rows[i].Cells[1].Value != null)
                    {
                        name = filtersDataGridView.Rows[i].Cells[1].Value.ToString();
                    }

                    if (filtersDataGridView.Rows[i].Cells[2].Value != null)
                    {
                        offset = filtersDataGridView.Rows[i].Cells[2].Value.ToString();
                    }

                    if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(offset))
                    {
                        FilterWheelProxy.filterNames[i] = name;
                        FilterWheelProxy.filterOffsets[i] = int.Parse(offset);
                    }
                }

                FilterWheelProxy.backlashCompSteps = Convert.ToInt32(backlashCompTextBox.Text);
                FilterWheelProxy.stepRatio = Convert.ToDecimal(stepRatioTextBox.Text);

                tl.Enabled = chkTrace.Checked;
            }
            else
            {
                DialogResult = DialogResult.None;
            }
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void filtersDataGridView_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            for (int i = 0; i < FilterWheelProxy.MAX_FILTER_COUNT; i++)
            {
                string name = string.Empty;
                string offset = string.Empty;

                if (filtersDataGridView.Rows[i].Cells[1].Value != null)
                {
                    name = filtersDataGridView.Rows[i].Cells[1].Value.ToString();
                }

                if (filtersDataGridView.Rows[i].Cells[2].Value != null)
                {
                    offset = filtersDataGridView.Rows[i].Cells[2].Value.ToString();
                }

                if (string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(offset))
                {
                    filtersDataGridView.Rows[i].Cells[1].ErrorText = "Filter name must be set";
                    e.Cancel = true;
                }
                else if (!string.IsNullOrEmpty(name) && string.IsNullOrEmpty(offset))
                {
                    filtersDataGridView.Rows[i].Cells[2].ErrorText = "Filter offset must be set";
                    e.Cancel = true;
                }
                else if (!string.IsNullOrEmpty(offset) && !int.TryParse(offset, out _))
                {
                    filtersDataGridView.Rows[i].Cells[2].ErrorText = "Filter offset must be an integer";
                    e.Cancel = true;
                }
                else
                {
                    filtersDataGridView.Rows[i].Cells[1].ErrorText = "";
                    filtersDataGridView.Rows[i].Cells[2].ErrorText = "";
                }
            }
        }

        private void filtersDataGridView_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            switch (e.ColumnIndex)
            {
                case 2:
                    // Don't set e.Cancel to true because that would prevent the error icon from showing up...
                    filtersDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].ErrorText =
                        (!string.IsNullOrEmpty(e.FormattedValue.ToString()) && !int.TryParse(e.FormattedValue.ToString(), out _))
                            ? "Filter offset must be an integer"
                            : "";
                    break;
            }
        }

        private void backlashCompTextBox_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                int value = Convert.ToInt32(backlashCompTextBox.Text);
                if (value < 0)
                {
                    throw new FormatException("Backlash compensation cannot be a negative number");
                }
                errorProvider.SetError(backlashCompTextBox, String.Empty);
            }
            catch (Exception)
            {
                e.Cancel = true;
                backlashCompTextBox.Select(0, backlashCompTextBox.Text.Length);
                errorProvider.SetError(backlashCompTextBox, "Must be an integer (positive or negative)");
            }
        }

        private void stepRatioTextBox_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                decimal value = Convert.ToDecimal(stepRatioTextBox.Text);
                if (value <= 0)
                {
                    throw new FormatException("The decimal value must be strictly positive");
                }
                errorProvider.SetError(stepRatioTextBox, String.Empty);
            }
            catch (Exception)
            {
                e.Cancel = true;
                stepRatioTextBox.Select(0, stepRatioTextBox.Text.Length);
                errorProvider.SetError(stepRatioTextBox, "Must be a strictly positive decimal value");
            }
        }

        private void BrowseToHomepage(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("https://github.com/jlecomte/ascom-oag-focuser");
            }
            catch (System.ComponentModel.Win32Exception noBrowser)
            {
                if (noBrowser.ErrorCode == -2147467259)
                    MessageBox.Show(noBrowser.Message);
            }
            catch (System.Exception other)
            {
                MessageBox.Show(other.Message);
            }
        }
    }

    public class ComboboxItem
    {
        public string Text { get; set; }
        public string Value { get; set; }

        public override string ToString()
        {
            return Text;
        }
    }
}