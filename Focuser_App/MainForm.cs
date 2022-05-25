using ASCOM.DriverAccess;

using System;
using System.Threading;
using System.Windows.Forms;

namespace Focuser_App
{
    public partial class MainForm : Form
    {
        internal static string DRIVER_ID = "ASCOM.DarkSkyGeek.Focuser";
        internal static int HIGH_JUMP = 300;
        internal static int LOW_JUMP = 100;

        private Focuser device = null;
        private Thread pollDeviceStatusThread = null;

        public MainForm()
        {
            InitializeComponent();
            toggleDeviceControls();
            pollDeviceStatusThread = new Thread(pollDeviceStatus);
            pollDeviceStatusThread.Start();
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

        private void toggleDeviceControls()
        {
            bool connected = device != null && device.Connected;
            btnSettings.Enabled = !connected;
            backlashCompTextBox.Enabled = !connected;
            txtBoxTgtPos.Enabled = connected;
            btnMove.Enabled = connected;
            btnMoveLeftHigh.Enabled = connected;
            btnMoveLeftLow.Enabled = connected;
            btnMoveRightLow.Enabled = connected;
            btnMoveRightHigh.Enabled = connected;
            btnSetZeroPos.Enabled = connected;
        }

        private void pollDeviceStatus()
        {
            while (true)
            {
                // Polling the device every 100 msec when connected is more frequently
                // than I would want a real application to poll the driver, but this
                // gives almost instantaneous feedback on what's happening, which is great!
                Thread.Sleep(100);

                lock (this)
                {
                    if (device != null && device.Connected)
                    {
                        try
                        {
                            Invoke(new Action(() =>
                            {
                                this.lblCurPosVal.Text = device.Position.ToString();
                                this.picIsMoving.Image = device.IsMoving ? Properties.Resources.yes : Properties.Resources.no;
                            }));
                        }
                        catch (Exception)
                        {
                            Invoke(new Action(() =>
                            {
                                this.lblCurPosVal.Text = "N/A";
                                this.picIsMoving.Image = Properties.Resources.no;
                            }));
                        }
                    }
                    else
                    {
                        Invoke(new Action(() =>
                        {
                            this.lblCurPosVal.Text = "N/A";
                            this.picIsMoving.Image = Properties.Resources.no;
                        }));
                    }
                }
            }
        }

        private void move(int targetPosition)
        {
            if (device != null && device.Connected)
            {
                int delta = targetPosition - device.Position;
                if (delta > 0)
                {
                    int backlashCompSteps = Convert.ToInt32(backlashCompTextBox.Text);

                    // If we're moving OUT, we overshoot to deal with backlash...
                    device.Move(device.Position + backlashCompSteps + delta);

                    // Wait for the focuser to reach the desired position...
                    while (device.IsMoving)
                    {
                        Thread.Sleep(100);
                    }

                    // Once the focuser has stopped moving, we tell it to move to its final position...
                    device.Move(device.Position - backlashCompSteps);
                }
                else
                {
                    device.Move(targetPosition);
                }
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            pollDeviceStatusThread.Abort();
            if (device != null)
            {
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
                toggleDeviceControls();
            }
            else
            {
                btnConnect.Enabled = false;

                instantiateDevice();
                if (device != null)
                {
                    // This can take a while. It can also throw...
                    try
                    {
                        device.Connected = true;
                        btnConnect.Text = "Disconnect";
                        btnConnect.Image = Properties.Resources.power_off;
                        toggleDeviceControls();
                    }
                    catch (Exception)
                    {
                        MessageBox.Show(this, "An error occurred while connecting to the focuser.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                btnConnect.Enabled = true;
            }
        }

        private void btnMove_Click(object sender, EventArgs e)
        {
            int tgtPos = Convert.ToInt32(txtBoxTgtPos.Text);
            move(tgtPos);
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

        private void txtBoxTgtPos_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                Convert.ToInt32(txtBoxTgtPos.Text);
                errorProvider.SetError(txtBoxTgtPos, String.Empty);
            }
            catch (Exception)
            {
                e.Cancel = true;
                txtBoxTgtPos.Select(0, txtBoxTgtPos.Text.Length);
                errorProvider.SetError(txtBoxTgtPos, "Must be an integer (positive or negative)");
            }
        }
    }
}
