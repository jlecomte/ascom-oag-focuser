namespace Focuser_App
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.lblCurPos = new System.Windows.Forms.Label();
            this.lblTgtPos = new System.Windows.Forms.Label();
            this.txtBoxTgtPos = new System.Windows.Forms.TextBox();
            this.btnMove = new System.Windows.Forms.Button();
            this.lblIsMoving = new System.Windows.Forms.Label();
            this.btnMoveLeftHigh = new System.Windows.Forms.Button();
            this.btnMoveLeftLow = new System.Windows.Forms.Button();
            this.btnMoveRightLow = new System.Windows.Forms.Button();
            this.btnMoveRightHigh = new System.Windows.Forms.Button();
            this.btnSetZeroPos = new System.Windows.Forms.Button();
            this.btnSettings = new System.Windows.Forms.Button();
            this.picIsMoving = new System.Windows.Forms.PictureBox();
            this.btnConnect = new System.Windows.Forms.Button();
            this.lblCurPosVal = new System.Windows.Forms.Label();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.backlashCompLabel = new System.Windows.Forms.Label();
            this.backlashCompTextBox = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.picIsMoving)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // lblCurPos
            // 
            this.lblCurPos.AutoSize = true;
            this.lblCurPos.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCurPos.Location = new System.Drawing.Point(36, 192);
            this.lblCurPos.Name = "lblCurPos";
            this.lblCurPos.Size = new System.Drawing.Size(113, 17);
            this.lblCurPos.TabIndex = 1;
            this.lblCurPos.Text = "Current Position:";
            // 
            // lblTgtPos
            // 
            this.lblTgtPos.AutoSize = true;
            this.lblTgtPos.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTgtPos.Location = new System.Drawing.Point(36, 238);
            this.lblTgtPos.Name = "lblTgtPos";
            this.lblTgtPos.Size = new System.Drawing.Size(108, 17);
            this.lblTgtPos.TabIndex = 4;
            this.lblTgtPos.Text = "Target Position:";
            // 
            // txtBoxTgtPos
            // 
            this.txtBoxTgtPos.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBoxTgtPos.Location = new System.Drawing.Point(155, 235);
            this.txtBoxTgtPos.Name = "txtBoxTgtPos";
            this.txtBoxTgtPos.Size = new System.Drawing.Size(62, 23);
            this.txtBoxTgtPos.TabIndex = 5;
            this.txtBoxTgtPos.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtBoxTgtPos.Validating += new System.ComponentModel.CancelEventHandler(this.txtBoxTgtPos_Validating);
            // 
            // btnMove
            // 
            this.btnMove.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMove.Location = new System.Drawing.Point(223, 233);
            this.btnMove.Name = "btnMove";
            this.btnMove.Size = new System.Drawing.Size(56, 26);
            this.btnMove.TabIndex = 6;
            this.btnMove.Text = "Move";
            this.btnMove.UseVisualStyleBackColor = true;
            this.btnMove.Click += new System.EventHandler(this.btnMove_Click);
            // 
            // lblIsMoving
            // 
            this.lblIsMoving.AutoSize = true;
            this.lblIsMoving.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIsMoving.Location = new System.Drawing.Point(201, 293);
            this.lblIsMoving.Name = "lblIsMoving";
            this.lblIsMoving.Size = new System.Drawing.Size(71, 17);
            this.lblIsMoving.TabIndex = 7;
            this.lblIsMoving.Text = "Is Moving:";
            // 
            // btnMoveLeftHigh
            // 
            this.btnMoveLeftHigh.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMoveLeftHigh.Location = new System.Drawing.Point(29, 288);
            this.btnMoveLeftHigh.Name = "btnMoveLeftHigh";
            this.btnMoveLeftHigh.Size = new System.Drawing.Size(39, 26);
            this.btnMoveLeftHigh.TabIndex = 9;
            this.btnMoveLeftHigh.Text = "<<";
            this.btnMoveLeftHigh.UseVisualStyleBackColor = true;
            this.btnMoveLeftHigh.Click += new System.EventHandler(this.btnMoveLeftHigh_Click);
            // 
            // btnMoveLeftLow
            // 
            this.btnMoveLeftLow.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMoveLeftLow.Location = new System.Drawing.Point(74, 288);
            this.btnMoveLeftLow.Name = "btnMoveLeftLow";
            this.btnMoveLeftLow.Size = new System.Drawing.Size(29, 26);
            this.btnMoveLeftLow.TabIndex = 10;
            this.btnMoveLeftLow.Text = "<";
            this.btnMoveLeftLow.UseVisualStyleBackColor = true;
            this.btnMoveLeftLow.Click += new System.EventHandler(this.btnMoveLeftLow_Click);
            // 
            // btnMoveRightLow
            // 
            this.btnMoveRightLow.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMoveRightLow.Location = new System.Drawing.Point(109, 288);
            this.btnMoveRightLow.Name = "btnMoveRightLow";
            this.btnMoveRightLow.Size = new System.Drawing.Size(29, 26);
            this.btnMoveRightLow.TabIndex = 11;
            this.btnMoveRightLow.Text = ">";
            this.btnMoveRightLow.UseVisualStyleBackColor = true;
            this.btnMoveRightLow.Click += new System.EventHandler(this.btnMoveRightLow_Click);
            // 
            // btnMoveRightHigh
            // 
            this.btnMoveRightHigh.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMoveRightHigh.Location = new System.Drawing.Point(144, 288);
            this.btnMoveRightHigh.Name = "btnMoveRightHigh";
            this.btnMoveRightHigh.Size = new System.Drawing.Size(37, 26);
            this.btnMoveRightHigh.TabIndex = 12;
            this.btnMoveRightHigh.Text = ">>";
            this.btnMoveRightHigh.UseVisualStyleBackColor = true;
            this.btnMoveRightHigh.Click += new System.EventHandler(this.btnMoveRightHigh_Click);
            // 
            // btnSetZeroPos
            // 
            this.btnSetZeroPos.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSetZeroPos.Location = new System.Drawing.Point(57, 352);
            this.btnSetZeroPos.Name = "btnSetZeroPos";
            this.btnSetZeroPos.Size = new System.Drawing.Size(283, 31);
            this.btnSetZeroPos.TabIndex = 13;
            this.btnSetZeroPos.Text = "Set Zero position!";
            this.btnSetZeroPos.UseVisualStyleBackColor = true;
            this.btnSetZeroPos.Click += new System.EventHandler(this.btnSetZeroPos_Click);
            // 
            // btnSettings
            // 
            this.btnSettings.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSettings.Image = global::Focuser_App.Properties.Resources.gears;
            this.btnSettings.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnSettings.Location = new System.Drawing.Point(57, 18);
            this.btnSettings.Name = "btnSettings";
            this.btnSettings.Padding = new System.Windows.Forms.Padding(10);
            this.btnSettings.Size = new System.Drawing.Size(126, 88);
            this.btnSettings.TabIndex = 14;
            this.btnSettings.Text = "Settings";
            this.btnSettings.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnSettings.UseVisualStyleBackColor = true;
            this.btnSettings.Click += new System.EventHandler(this.btnSettings_Click);
            // 
            // picIsMoving
            // 
            this.picIsMoving.BackColor = System.Drawing.Color.Transparent;
            this.picIsMoving.Image = global::Focuser_App.Properties.Resources.no;
            this.picIsMoving.Location = new System.Drawing.Point(278, 286);
            this.picIsMoving.Name = "picIsMoving";
            this.picIsMoving.Size = new System.Drawing.Size(32, 32);
            this.picIsMoving.TabIndex = 8;
            this.picIsMoving.TabStop = false;
            // 
            // btnConnect
            // 
            this.btnConnect.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConnect.Image = global::Focuser_App.Properties.Resources.power_on;
            this.btnConnect.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnConnect.Location = new System.Drawing.Point(214, 18);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Padding = new System.Windows.Forms.Padding(10);
            this.btnConnect.Size = new System.Drawing.Size(126, 88);
            this.btnConnect.TabIndex = 15;
            this.btnConnect.Text = "Connect";
            this.btnConnect.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // lblCurPosVal
            // 
            this.lblCurPosVal.AutoSize = true;
            this.lblCurPosVal.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCurPosVal.Location = new System.Drawing.Point(152, 192);
            this.lblCurPosVal.Name = "lblCurPosVal";
            this.lblCurPosVal.Size = new System.Drawing.Size(31, 17);
            this.lblCurPosVal.TabIndex = 16;
            this.lblCurPosVal.Text = "N/A";
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // backlashCompLabel
            // 
            this.backlashCompLabel.AutoSize = true;
            this.backlashCompLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.backlashCompLabel.Location = new System.Drawing.Point(36, 145);
            this.backlashCompLabel.Name = "backlashCompLabel";
            this.backlashCompLabel.Size = new System.Drawing.Size(266, 17);
            this.backlashCompLabel.TabIndex = 17;
            this.backlashCompLabel.Text = "Backlash compensation overshoot steps:";
            // 
            // backlashCompTextBox
            // 
            this.backlashCompTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.backlashCompTextBox.Location = new System.Drawing.Point(311, 142);
            this.backlashCompTextBox.Name = "backlashCompTextBox";
            this.backlashCompTextBox.Size = new System.Drawing.Size(62, 23);
            this.backlashCompTextBox.TabIndex = 18;
            this.backlashCompTextBox.Text = "0";
            this.backlashCompTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.backlashCompTextBox.Validating += new System.ComponentModel.CancelEventHandler(this.backlashCompTextBox_Validating);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(395, 401);
            this.Controls.Add(this.backlashCompTextBox);
            this.Controls.Add(this.backlashCompLabel);
            this.Controls.Add(this.lblCurPosVal);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.btnSettings);
            this.Controls.Add(this.btnSetZeroPos);
            this.Controls.Add(this.btnMoveRightHigh);
            this.Controls.Add(this.btnMoveRightLow);
            this.Controls.Add(this.btnMoveLeftLow);
            this.Controls.Add(this.btnMoveLeftHigh);
            this.Controls.Add(this.picIsMoving);
            this.Controls.Add(this.lblIsMoving);
            this.Controls.Add(this.btnMove);
            this.Controls.Add(this.txtBoxTgtPos);
            this.Controls.Add(this.lblTgtPos);
            this.Controls.Add(this.lblCurPos);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "DarkSkyGeek’s OAG Focuser";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.picIsMoving)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lblCurPos;
        private System.Windows.Forms.Label lblTgtPos;
        private System.Windows.Forms.TextBox txtBoxTgtPos;
        private System.Windows.Forms.Button btnMove;
        private System.Windows.Forms.Label lblIsMoving;
        private System.Windows.Forms.PictureBox picIsMoving;
        private System.Windows.Forms.Button btnMoveLeftHigh;
        private System.Windows.Forms.Button btnMoveLeftLow;
        private System.Windows.Forms.Button btnMoveRightLow;
        private System.Windows.Forms.Button btnMoveRightHigh;
        private System.Windows.Forms.Button btnSetZeroPos;
        private System.Windows.Forms.Button btnSettings;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Label lblCurPosVal;
        private System.Windows.Forms.ErrorProvider errorProvider;
        private System.Windows.Forms.Label backlashCompLabel;
        private System.Windows.Forms.TextBox backlashCompTextBox;
    }
}

