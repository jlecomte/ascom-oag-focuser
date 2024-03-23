using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace ASCOM.DarkSkyGeek
{
    // Form not registered for COM!
    [ComVisible(false)]

    public partial class ProfileNameEditor : Form
    {
        public string ReturnValue = null;
        private readonly FilterWheelProxy driver;

        public ProfileNameEditor(string ProfileName, FilterWheelProxy driver)
        {
            InitializeComponent();
            this.driver = driver;
            if (ProfileName != null)
            {
                profileNameTextBox.Text = ProfileName.Trim();
            }
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void cmdOK_Click(object sender, EventArgs e)
        {
            var profileName = profileNameTextBox.Text.Trim();
            if (profileName == "")
            {
                MessageBox.Show("Profile name cannot be empty!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                FilterWheelProxyProfile profile = driver.GetProfile(profileName);
                if (profile != null)
                {
                    MessageBox.Show("A profile with this name already exists!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    ReturnValue = profileName;
                    DialogResult = DialogResult.OK;
                    Close();
                }
            }
        }
    }
}
