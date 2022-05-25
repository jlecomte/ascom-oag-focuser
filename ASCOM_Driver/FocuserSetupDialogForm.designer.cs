/*
 * FocuserSetupDialogForm.designer.cs
 * Copyright (C) 2022 - Present, Julien Lecomte - All Rights Reserved
 * Licensed under the MIT License. See the accompanying LICENSE file for terms.
 */

namespace ASCOM.DarkSkyGeek
{
    partial class FocuserSetupDialogForm
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
            this.components = new System.ComponentModel.Container();
            this.cmdOK = new System.Windows.Forms.Button();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.comPortOverrideLabel = new System.Windows.Forms.Label();
            this.chkTrace = new System.Windows.Forms.CheckBox();
            this.comboBoxComPort = new System.Windows.Forms.ComboBox();
            this.DSGLogo = new System.Windows.Forms.PictureBox();
            this.chkAutoDetect = new System.Windows.Forms.CheckBox();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.DSGLogo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // cmdOK
            // 
            this.cmdOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.cmdOK.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdOK.Image = global::ASCOM.DarkSkyGeek.Properties.Resources.icon_ok_24;
            this.cmdOK.Location = new System.Drawing.Point(137, 106);
            this.cmdOK.Name = "cmdOK";
            this.cmdOK.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.cmdOK.Size = new System.Drawing.Size(76, 35);
            this.cmdOK.TabIndex = 0;
            this.cmdOK.UseVisualStyleBackColor = true;
            this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
            // 
            // cmdCancel
            // 
            this.cmdCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdCancel.CausesValidation = false;
            this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdCancel.Image = global::ASCOM.DarkSkyGeek.Properties.Resources.icon_cancel_24;
            this.cmdCancel.Location = new System.Drawing.Point(219, 106);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(74, 37);
            this.cmdCancel.TabIndex = 1;
            this.cmdCancel.UseVisualStyleBackColor = true;
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // comPortOverrideLabel
            // 
            this.comPortOverrideLabel.AutoSize = true;
            this.comPortOverrideLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comPortOverrideLabel.Location = new System.Drawing.Point(112, 41);
            this.comPortOverrideLabel.Name = "comPortOverrideLabel";
            this.comPortOverrideLabel.Size = new System.Drawing.Size(99, 13);
            this.comPortOverrideLabel.TabIndex = 5;
            this.comPortOverrideLabel.Text = "COM Port Override:";
            // 
            // chkTrace
            // 
            this.chkTrace.AutoSize = true;
            this.chkTrace.Location = new System.Drawing.Point(115, 66);
            this.chkTrace.Name = "chkTrace";
            this.chkTrace.Size = new System.Drawing.Size(69, 17);
            this.chkTrace.TabIndex = 6;
            this.chkTrace.Text = "Trace on";
            this.chkTrace.UseVisualStyleBackColor = true;
            // 
            // comboBoxComPort
            // 
            this.comboBoxComPort.FormattingEnabled = true;
            this.comboBoxComPort.Location = new System.Drawing.Point(217, 38);
            this.comboBoxComPort.Name = "comboBoxComPort";
            this.comboBoxComPort.Size = new System.Drawing.Size(71, 21);
            this.comboBoxComPort.TabIndex = 7;
            // 
            // DSGLogo
            // 
            this.DSGLogo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.DSGLogo.Image = global::ASCOM.DarkSkyGeek.Properties.Resources.darkskygeek;
            this.DSGLogo.Location = new System.Drawing.Point(13, 12);
            this.DSGLogo.Name = "DSGLogo";
            this.DSGLogo.Size = new System.Drawing.Size(88, 88);
            this.DSGLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.DSGLogo.TabIndex = 3;
            this.DSGLogo.TabStop = false;
            this.DSGLogo.Click += new System.EventHandler(this.BrowseToHomepage);
            this.DSGLogo.DoubleClick += new System.EventHandler(this.BrowseToHomepage);
            // 
            // chkAutoDetect
            // 
            this.chkAutoDetect.AutoSize = true;
            this.chkAutoDetect.Checked = true;
            this.chkAutoDetect.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoDetect.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkAutoDetect.Location = new System.Drawing.Point(115, 12);
            this.chkAutoDetect.Name = "chkAutoDetect";
            this.chkAutoDetect.Size = new System.Drawing.Size(131, 17);
            this.chkAutoDetect.TabIndex = 8;
            this.chkAutoDetect.Text = "Auto-Detect COM port";
            this.chkAutoDetect.UseVisualStyleBackColor = true;
            this.chkAutoDetect.CheckedChanged += new System.EventHandler(this.chkAutoDetect_CheckedChanged);
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // FocuserSetupDialogForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(305, 151);
            this.Controls.Add(this.chkAutoDetect);
            this.Controls.Add(this.comboBoxComPort);
            this.Controls.Add(this.chkTrace);
            this.Controls.Add(this.comPortOverrideLabel);
            this.Controls.Add(this.DSGLogo);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.cmdOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FocuserSetupDialogForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DarkSkyGeek’s OAG Focuser";
            this.Load += new System.EventHandler(this.FocuserSetupDialogForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DSGLogo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cmdOK;
        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.Label comPortOverrideLabel;
        private System.Windows.Forms.CheckBox chkTrace;
        private System.Windows.Forms.ComboBox comboBoxComPort;
        private System.Windows.Forms.PictureBox DSGLogo;
        private System.Windows.Forms.CheckBox chkAutoDetect;
        private System.Windows.Forms.ErrorProvider errorProvider;
    }
}