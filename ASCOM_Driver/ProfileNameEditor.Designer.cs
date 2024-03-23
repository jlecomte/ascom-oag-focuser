namespace ASCOM.DarkSkyGeek
{
    partial class ProfileNameEditor
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.profileNameLabel = new System.Windows.Forms.Label();
            this.profileNameTextBox = new System.Windows.Forms.TextBox();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.cmdOK = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // profileNameLabel
            // 
            this.profileNameLabel.AutoSize = true;
            this.profileNameLabel.Location = new System.Drawing.Point(13, 13);
            this.profileNameLabel.Name = "profileNameLabel";
            this.profileNameLabel.Size = new System.Drawing.Size(70, 13);
            this.profileNameLabel.TabIndex = 0;
            this.profileNameLabel.Text = "Profile Name:";
            // 
            // profileNameTextBox
            // 
            this.profileNameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.profileNameTextBox.Location = new System.Drawing.Point(89, 10);
            this.profileNameTextBox.Name = "profileNameTextBox";
            this.profileNameTextBox.Size = new System.Drawing.Size(281, 20);
            this.profileNameTextBox.TabIndex = 1;
            // 
            // cmdCancel
            // 
            this.cmdCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdCancel.CausesValidation = false;
            this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdCancel.Image = global::ASCOM.DarkSkyGeek.Properties.Resources.icon_cancel_24;
            this.cmdCancel.Location = new System.Drawing.Point(296, 48);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(74, 37);
            this.cmdCancel.TabIndex = 3;
            this.cmdCancel.UseVisualStyleBackColor = true;
            this.cmdCancel.Click += new System.EventHandler(this.CmdCancel_Click);
            // 
            // cmdOK
            // 
            this.cmdOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdOK.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdOK.Image = global::ASCOM.DarkSkyGeek.Properties.Resources.icon_ok_24;
            this.cmdOK.Location = new System.Drawing.Point(214, 49);
            this.cmdOK.Name = "cmdOK";
            this.cmdOK.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.cmdOK.Size = new System.Drawing.Size(76, 35);
            this.cmdOK.TabIndex = 2;
            this.cmdOK.UseVisualStyleBackColor = true;
            this.cmdOK.Click += new System.EventHandler(this.CmdOK_Click);
            // 
            // ProfileNameEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(382, 97);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.cmdOK);
            this.Controls.Add(this.profileNameTextBox);
            this.Controls.Add(this.profileNameLabel);
            this.Name = "ProfileNameEditor";
            this.Text = "Set Profile Name";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label profileNameLabel;
        private System.Windows.Forms.TextBox profileNameTextBox;
        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.Button cmdOK;
    }
}