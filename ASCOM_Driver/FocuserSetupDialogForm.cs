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
            chkTrace.Checked = tl.Enabled;
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
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void chkAutoDetect_CheckedChanged(object sender, EventArgs e)
        {
            comboBoxComPort.Enabled = !((CheckBox)sender).Checked;
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