/*
 * FocuserSetupDialogForm.cs
 * Copyright (C) 2022 - Present, Julien Lecomte - All Rights Reserved
 * Licensed under the MIT License. See the accompanying LICENSE file for terms.
 */

using ASCOM.Utilities;

using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace ASCOM.DarkSkyGeek
{
    // Form not registered for COM!
    [ComVisible(false)]

    public partial class FocuserSetupDialogForm : Form
    {
        // Holder for a reference to the driver's trace logger
        TraceLogger tl;

        public FocuserSetupDialogForm(TraceLogger tlDriver)
        {
            InitializeComponent();

            // Save the provided trace logger for use within the setup dialogue
            tl = tlDriver;

            // Initialize current values of user settings from the ASCOM Profile
            InitUI();
        }

        private void cmdOK_Click(object sender, EventArgs e)
        {
            if (!Validate())
            {
                DialogResult = DialogResult.None;
            }
            Focuser.autoDetectComPort = chkAutoDetect.Checked;
            Focuser.comPortOverride = (string)comboBoxComPort.SelectedItem;
            tl.Enabled = chkTrace.Checked;
            Focuser.backlashCompSteps = Convert.ToInt32(backlashCompTextBox.Text);
            Focuser.stepRatio = Convert.ToDecimal(stepRatioTextBox.Text);
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void InitUI()
        {
            chkAutoDetect.Checked = Focuser.autoDetectComPort;
            chkTrace.Checked = tl.Enabled;
            comboBoxComPort.Enabled = !chkAutoDetect.Checked;

            // Set the list of COM ports to those that are currently available
            comboBoxComPort.Items.Clear();
            // Use System.IO because it's static
            comboBoxComPort.Items.AddRange(System.IO.Ports.SerialPort.GetPortNames());
            // Select the current port if possible
            if (comboBoxComPort.Items.Contains(Focuser.comPortOverride))
            {
                comboBoxComPort.SelectedItem = Focuser.comPortOverride;
            }

            backlashCompTextBox.Text = Focuser.backlashCompSteps.ToString();
            stepRatioTextBox.Text = Focuser.stepRatio.ToString();
        }

        private void chkAutoDetect_CheckedChanged(object sender, EventArgs e)
        {
            comboBoxComPort.Enabled = !((CheckBox)sender).Checked;
        }

        private void backlashCompTextBox_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                Convert.ToInt32(backlashCompTextBox.Text);
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
}