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
        }

        private void FocuserSetupDialogForm_Load(object sender, EventArgs e)
        {
            chkAutoDetect.Checked = Focuser.autoDetectComPort;

            comboBoxComPort.Enabled = !chkAutoDetect.Checked;

            // Set the list of COM ports to those that are currently available
            comboBoxComPort.Items.Clear();
            // Use System.IO because it's static
            comboBoxComPort.Items.AddRange(System.IO.Ports.SerialPort.GetPortNames());
            // Select the current port if possible
            if (Focuser.comPortOverride != null && comboBoxComPort.Items.Contains(Focuser.comPortOverride))
            {
                comboBoxComPort.SelectedItem = Focuser.comPortOverride;
            }

            chkTrace.Checked = tl.Enabled;

            maxPositionTextBox.Text = Focuser.maxPosition.ToString();
            chkReverseRotation.Checked = Focuser.reverseRotation;
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
            Focuser.maxPosition = Convert.ToInt32(maxPositionTextBox.Text);
            Focuser.reverseRotation = chkReverseRotation.Checked;
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void chkAutoDetect_CheckedChanged(object sender, EventArgs e)
        {
            comboBoxComPort.Enabled = !((CheckBox)sender).Checked;
        }

        private void maxPositionTextBox_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                int value = Convert.ToInt32(maxPositionTextBox.Text);
                if (value <= 0)
                {
                    throw new FormatException("Maximum position must be a strictly positive integer");
                }
                errorProvider.SetError(maxPositionTextBox, string.Empty);
            }
            catch (Exception)
            {
                e.Cancel = true;
                maxPositionTextBox.Select(0, maxPositionTextBox.Text.Length);
                errorProvider.SetError(maxPositionTextBox, "Must be a strictly positive integer");
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