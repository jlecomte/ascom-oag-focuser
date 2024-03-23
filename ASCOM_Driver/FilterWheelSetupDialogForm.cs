/*
 * FilterWheelSetupDialogForm.cs
 * Copyright (C) 2022 - Present, Julien Lecomte - All Rights Reserved
 * Licensed under the MIT License. See the accompanying LICENSE file for terms.
 */

using ASCOM.DriverAccess;
using ASCOM.Utilities;

using System;
using System.Collections;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace ASCOM.DarkSkyGeek
{
    // Form not registered for COM!
    [ComVisible(false)]

    public partial class FilterWheelSetupDialogForm : Form
    {
        // Holder for a reference to the driver instance
        readonly FilterWheelProxy driver;

        public FilterWheelSetupDialogForm(FilterWheelProxy driver)
        {
            InitializeComponent();

            // Save the provided driver instance for use within the setup dialog
            this.driver = driver;
        }

        private void UpdateFormFields(bool updateProfileChooser = false)
        {
            Profile AscomProfile = new Profile();

            var selectedProfile = driver.GetSelectedProfile();

            filterWheelSelectorComboBox.Items.Clear();
            focuserSelectorComboBox.Items.Clear();

            if (updateProfileChooser)
            {
                profileChooser.Items.Clear();

                // Populate profile chooser list...
                foreach (var profile in driver.profiles.profiles)
                {
                    int index = profileChooser.Items.Add(profile.name);
                    if (profile.name.Trim() == selectedProfile.name.Trim())
                    {
                        profileChooser.SelectedIndex = index;
                    }
                }
            }

            // Populate filter wheel device list...
            ArrayList filterWheelDevices = AscomProfile.RegisteredDevices("FilterWheel");
            foreach (KeyValuePair kv in filterWheelDevices)
            {
                // Don't include the filter wheel proxy in the list, for obvious reasons...
                if (kv.Key != FilterWheelProxy.driverID)
                {
                    ComboboxItem item = new ComboboxItem
                    {
                        Text = kv.Value,
                        Value = kv.Key
                    };

                    int index = filterWheelSelectorComboBox.Items.Add(item);

                    // Select newly added item if it matches the value stored in the profile.
                    if (kv.Key == selectedProfile.filterWheelId)
                    {
                        filterWheelSelectorComboBox.SelectedIndex = index;
                    }
                }
            }

            // Populate focuser device list...
            ArrayList focuserDevices = AscomProfile.RegisteredDevices("Focuser");
            foreach (KeyValuePair kv in focuserDevices)
            {
                ComboboxItem item = new ComboboxItem
                {
                    Text = kv.Value,
                    Value = kv.Key
                };
                int index = focuserSelectorComboBox.Items.Add(item);
                // Select newly added item if it matches the value stored in the profile.
                if (kv.Key == selectedProfile.focuserId)
                {
                    focuserSelectorComboBox.SelectedIndex = index;
                }
            }

            // Populate autofocus filter settings...
            filtersDataGridView.Rows.Clear();
            for (int i = 0; i < FilterWheelProxy.MAX_FILTER_COUNT; i++)
            {
                string name = null;
                int offset = 0;

                if (i < selectedProfile.filterNames.Count)
                {
                    name = selectedProfile.filterNames[i];
                    offset = selectedProfile.filterOffsets[i];
                }

                if (string.IsNullOrEmpty(name))
                {
                    filtersDataGridView.Rows.Add(i + 1, string.Empty, string.Empty);
                }
                else
                {
                    filtersDataGridView.Rows.Add(i + 1, name, offset);
                }
            }

            backlashCompTextBox.Text = selectedProfile.backlashCompSteps.ToString();
            stepRatioTextBox.Text = selectedProfile.stepRatio.ToString();

            chkTrace.Checked = driver.tl.Enabled;
        }

        private void FilterWheelSetupDialogForm_Load(object sender, EventArgs e)
        {
            UpdateFormFields(true);
        }

        private void CmdOK_Click(object sender, EventArgs e)
        {
            foreach (var profile in driver.profiles.profiles)
            {
                if (profile.filterWheelId == null)
                {
                    MessageBox.Show($"You have not chosen a filter wheel in profile \"{profile.name}\"", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (profile.focuserId == null)
                {
                    MessageBox.Show($"You have not chosen a focuser in profile \"{profile.name}\"", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            if (Validate())
            {
                driver.tl.Enabled = chkTrace.Checked;
                // All the profile(s) values will have already been updated by now...
                DialogResult = DialogResult.OK;
            }
        }

        private void CmdCancel_Click(object sender, EventArgs e)
        {
            Close();
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
                {
                    MessageBox.Show(noBrowser.Message);
                }
            }
            catch (Exception other)
            {
                MessageBox.Show(other.Message);
            }
        }

        private void ManageProfilesButton_Click(object sender, EventArgs e)
        {
            Button btnSender = (Button)sender;
            Point ptLowerLeft = new Point(0, btnSender.Height);
            ptLowerLeft = btnSender.PointToScreen(ptLowerLeft);
            contextMenuStrip.Show(ptLowerLeft);
        }

        private void ProfileChooser_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Validate())
            {
                var profile = driver.GetProfile(profileChooser.GetItemText(profileChooser.SelectedItem));
                if (profile != null)
                {
                    driver.profiles.currentlySelectedProfileName = profile.name;
                }
                UpdateFormFields();
            }
            else
            {
                UpdateFormFields(true);
            }
        }

        private void NewProfileMenuItem_Click(object sender, EventArgs e)
        {
            var profileNameEditor = new ProfileNameEditor("<New Profile Name>", driver)
            {
                StartPosition = FormStartPosition.CenterParent
            };
            var result = profileNameEditor.ShowDialog();
            if (result == DialogResult.OK)
            {
                var newProfile = new FilterWheelProxyProfile
                {
                    name = profileNameEditor.ReturnValue
                };
                driver.profiles.profiles.Add(newProfile);
                driver.profiles.currentlySelectedProfileName = newProfile.name;
                UpdateFormFields(true);
            }
        }

        private void RenameProfileMenuItem_Click(object sender, EventArgs e)
        {
            var selectedProfile = driver.GetSelectedProfile();
            var profileNameEditor = new ProfileNameEditor(selectedProfile.name, driver)
            {
                StartPosition = FormStartPosition.CenterParent
            };
            var result = profileNameEditor.ShowDialog();
            if (result == DialogResult.OK)
            {
                selectedProfile.name = profileNameEditor.ReturnValue;
                UpdateFormFields(true);
            }
        }

        private void DeleteProfileMenuItem_Click(object sender, EventArgs e)
        {
            var selectedProfile = driver.GetSelectedProfile();
            driver.profiles.profiles.Remove(selectedProfile);
            
            // Select the first profile...
            if (driver.profiles.profiles.Count > 0)
            {
                selectedProfile = driver.profiles.profiles[0];
                driver.profiles.currentlySelectedProfileName = selectedProfile.name;
            }
            else
            {
                var defaultProfile = driver.CreateDefaultProfile();
                driver.profiles.currentlySelectedProfileName = defaultProfile.name;
                driver.profiles.profiles.Add(defaultProfile);
            }

            UpdateFormFields(true);
        }

        private void FilterWheelSelectorComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedProfile = driver.GetSelectedProfile();
            selectedProfile.filterWheelId = (filterWheelSelectorComboBox.SelectedItem as ComboboxItem).Value;
        }

        private void FiltersDataGridView_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
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

        private void FiltersDataGridView_Validating(object sender, System.ComponentModel.CancelEventArgs e)
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

        private void FiltersDataGridView_Validated(object sender, EventArgs e)
        {
            var selectedProfile = driver.GetSelectedProfile();
            selectedProfile.filterNames.Clear();
            selectedProfile.filterOffsets.Clear();

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
                    selectedProfile.filterNames.Add(name);
                    selectedProfile.filterOffsets.Add(int.Parse(offset));
                }
            }
        }

        private void FocuserSelectorComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedProfile = driver.GetSelectedProfile();
            selectedProfile.focuserId = (focuserSelectorComboBox.SelectedItem as ComboboxItem).Value;
        }

        private void BacklashCompTextBox_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                int value = Convert.ToInt32(backlashCompTextBox.Text);
                if (value < 0)
                {
                    throw new FormatException("Backlash compensation cannot be a negative number");
                }
                errorProvider.SetError(backlashCompTextBox, string.Empty);
            }
            catch (Exception)
            {
                e.Cancel = true;
                backlashCompTextBox.Select(0, backlashCompTextBox.Text.Length);
                errorProvider.SetError(backlashCompTextBox, "Must be an integer (positive or negative)");
            }
        }

        private void BacklashCompTextBox_Validated(object sender, EventArgs e)
        {
            var selectedProfile = driver.GetSelectedProfile();
            selectedProfile.backlashCompSteps = Convert.ToInt32(backlashCompTextBox.Text);
        }

        private void StepRatioTextBox_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                decimal value = Convert.ToDecimal(stepRatioTextBox.Text);
                errorProvider.SetError(stepRatioTextBox, string.Empty);
            }
            catch (Exception)
            {
                e.Cancel = true;
                stepRatioTextBox.Select(0, stepRatioTextBox.Text.Length);
                errorProvider.SetError(stepRatioTextBox, "Must be a decimal value");
            }
        }

        private void StepRatioTextBox_Validated(object sender, EventArgs e)
        {
            var selectedProfile = driver.GetSelectedProfile();
            selectedProfile.stepRatio = Convert.ToDecimal(stepRatioTextBox.Text);
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