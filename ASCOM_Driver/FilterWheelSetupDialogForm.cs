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
            // Create initial set of rows in the DataGridView...
            for (int i = 0; i < FilterWheelProxy.MAX_FILTER_COUNT; i++)
            {
                filtersDataGridView.Rows.Add(i + 1, string.Empty, string.Empty);
            }

            // Initialize current values of user settings from the ASCOM Profile
            InitUI();
        }

        private void cmdOK_Click(object sender, EventArgs e)
        {
            if (Validate())
            {
                if (devicesComboBox.SelectedItem != null)
                {
                    FilterWheelProxy.filterWheelId = (devicesComboBox.SelectedItem as ComboboxItem).Value;
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

        private void InitUI()
        {
            chkTrace.Checked = tl.Enabled;

            // Populate device list...
            Profile profile = new Profile();
            ArrayList filterWheelDevices = profile.RegisteredDevices("FilterWheel");
            foreach (KeyValuePair kv in filterWheelDevices)
            {
                // Don't include the filter wheel proxy in the list, for obvious reasons...
                if (kv.Key != FilterWheelProxy.driverID)
                {
                    ComboboxItem item = new ComboboxItem();
                    item.Text = kv.Value;
                    item.Value = kv.Key;
                    int index = devicesComboBox.Items.Add(item);
                    // Select newly added item if it matches the value stored in the profile.
                    if (kv.Key == FilterWheelProxy.filterWheelId)
                    {
                        devicesComboBox.SelectedIndex = index;
                    }
                }
            }

            // Populate autofocus filter settings...
            for (int i = 0; i < FilterWheelProxy.MAX_FILTER_COUNT; i++)
            {
                string name = FilterWheelProxy.filterNames[i];
                int offset = FilterWheelProxy.filterOffsets[i];
                if (!string.IsNullOrEmpty(name))
                {
                    filtersDataGridView.Rows[i].Cells[1].Value = name;
                    filtersDataGridView.Rows[i].Cells[2].Value = offset;
                }
            }
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
                    filtersDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].ErrorText =
                        (!string.IsNullOrEmpty(e.FormattedValue.ToString()) && !int.TryParse(e.FormattedValue.ToString(), out _))
                            ? "Filter offset must be an integer"
                            : "";
                    break;
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