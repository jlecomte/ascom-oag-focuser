/*
 * MainForm.cs
 * Copyright (C) 2022 - Present, Julien Lecomte - All Rights Reserved
 * Licensed under the MIT License. See the accompanying LICENSE file for terms.
 */

using ASCOM.DriverAccess;

using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ASCOM.DarkSkyGeek.FocuserApp
{
    public partial class MainForm : Form
    {
        internal static string DRIVER_ID = "ASCOM.DarkSkyGeek.Focuser";
        internal static int HIGH_JUMP = 300;
        internal static int LOW_JUMP = 100;

        private Focuser device = null;

        public MainForm()
        {
            InitializeComponent();
            updateUI();
        }

        private void instantiateDevice()
        {
            if (device == null)
            {
                try
                {
                    device = new Focuser(DRIVER_ID);
                }
                catch (Exception)
                {
                    MessageBox.Show(this, "An error occurred while loading the focuser driver.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void updateUI()
        {
            bool connected = device != null && device.Connected;
            bool moving = connected && device.IsMoving;

            btnSettings.Enabled = !connected;
            backlashCompTextBox.Enabled = !connected;
            txtBoxTgtPos.Enabled = connected;
            btnMove.Enabled = connected && !moving;
            btnHalt.Enabled = connected && moving;
            btnMoveLeftHigh.Enabled = connected && !moving;
            btnMoveLeftLow.Enabled = connected && !moving;
            btnMoveRightLow.Enabled = connected && !moving;
            btnMoveRightHigh.Enabled = connected && !moving;
            btnSetZeroPos.Enabled = connected && !moving;

            if (connected)
            {
                this.lblCurPosVal.Text = device.Position.ToString();
            }
            else
            {
                this.lblCurPosVal.Text = "N/A";
                this.picIsMoving.Image = Properties.Resources.no;
            }
        }

        private async void move(int targetPosition)
        {
            if (device != null && device.Connected)
            {
                int delta = targetPosition - device.Position;
                if (delta > 0)
                {
                    int backlashCompSteps = Convert.ToInt32(backlashCompTextBox.Text);

                    // If we're moving OUT, we overshoot to deal with backlash...
                    device.Move(device.Position + backlashCompSteps + delta);

                    updateUI();

                    await waitForDeviceToStopMoving();

                    // Once the focuser has stopped moving, we tell it to move to
                    // its final position, thereby clearing the mechanical backlash.
                    device.Move(device.Position - backlashCompSteps);

                    await waitForDeviceToStopMoving();

                    updateUI();
                }
                else
                {
                    device.Move(targetPosition);

                    await waitForDeviceToStopMoving();

                    updateUI();
                }
            }
        }

        private async Task waitForDeviceToStopMoving()
        {
            await Task.Run(() =>
            {
                // Wait for the focuser to reach the desired position...
                while (device.IsMoving)
                {
                    Thread.Sleep(100);
                    Invoke(new Action(() =>
                    {
                        try
                        {
                            this.lblCurPosVal.Text = device.Position.ToString();
                            this.picIsMoving.Image = Properties.Resources.yes;
                        }
                        catch (Exception) {; }
                    }));
                }

                this.picIsMoving.Image = Properties.Resources.no;
            });
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (device != null)
            {
                if (device.Connected)
                {
                    device.Connected = false;
                }
                device.Dispose();
                device = null;
            }
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            instantiateDevice();
            if (device != null)
            {
                device.SetupDialog();
            }
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (device != null && device.Connected)
            {
                device.Connected = false;
                btnConnect.Text = "Connect";
                btnConnect.Image = Properties.Resources.power_on;
                updateUI();
            }
            else
            {
                btnSettings.Enabled = false;
                btnConnect.Enabled = false;
                backlashCompTextBox.Enabled = false;

                // Hack to avoid having to use a thread/background worker.
                // This allows the previous lines to be immediately reflected in the UI.
                Application.DoEvents();

                instantiateDevice();
                if (device != null)
                {
                    try
                    {
                        // This can take a while. It can also throw...
                        device.Connected = true;
                        btnConnect.Text = "Disconnect";
                        btnConnect.Image = Properties.Resources.power_off;
                        updateUI();
                    }
                    catch (Exception)
                    {
                        MessageBox.Show(this, "An error occurred while connecting to the focuser.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        updateUI();
                    }
                }

                btnConnect.Enabled = true;
            }
        }

        private void btnMove_Click(object sender, EventArgs e)
        {
            int tgtPos;

            try
            {
                tgtPos = Convert.ToInt32(txtBoxTgtPos.Text);
                if (tgtPos < 0)
                {
                    throw new FormatException("The target position cannot be a negative number");
                }
                errorProvider.SetError(txtBoxTgtPos, string.Empty);
            }
            catch (Exception)
            {
                txtBoxTgtPos.Select(0, txtBoxTgtPos.Text.Length);
                errorProvider.SetError(txtBoxTgtPos, "Must be an integer (positive or negative)");
                return;
            }

            move(tgtPos);
        }

        private void btnHalt_Click(object sender, EventArgs e)
        {
            if (device != null && device.Connected)
            {
                device.Halt();
            }
        }

        private void btnMoveLeftHigh_Click(object sender, EventArgs e)
        {
            int tgtPos = Math.Max(device.Position - HIGH_JUMP, 0);
            move(tgtPos);
        }

        private void btnMoveLeftLow_Click(object sender, EventArgs e)
        {
            int tgtPos = Math.Max(device.Position - LOW_JUMP, 0);
            move(tgtPos);
        }

        private void btnMoveRightLow_Click(object sender, EventArgs e)
        {
            int tgtPos = Math.Min(device.Position + LOW_JUMP, device.MaxStep);
            move(tgtPos);
        }

        private void btnMoveRightHigh_Click(object sender, EventArgs e)
        {
            int tgtPos = Math.Min(device.Position + HIGH_JUMP, device.MaxStep);
            move(tgtPos);
        }

        private void btnSetZeroPos_Click(object sender, EventArgs e)
        {
            device.Action("SetZeroPosition", "");
            updateUI();
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
                errorProvider.SetError(backlashCompTextBox, string.Empty);
            }
            catch (Exception)
            {
                e.Cancel = true;
                backlashCompTextBox.Select(0, backlashCompTextBox.Text.Length);
                errorProvider.SetError(backlashCompTextBox, "Must be an integer (positive or negative)");
            }
        }
    }
}
