/*
 * FilterWheelSetupDialogForm.designer.cs
 * Copyright (C) 2022 - Present, Julien Lecomte - All Rights Reserved
 * Licensed under the MIT License. See the accompanying LICENSE file for terms.
 */

namespace ASCOM.DarkSkyGeek
{
    partial class FilterWheelSetupDialogForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FilterWheelSetupDialogForm));
            this.cmdOK = new System.Windows.Forms.Button();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.descriptionLabel = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.filterWheelSelectorLabel = new System.Windows.Forms.Label();
            this.filterWheelSelectorComboBox = new System.Windows.Forms.ComboBox();
            this.filterOffsetsWarning = new System.Windows.Forms.Label();
            this.filtersDataGridView = new System.Windows.Forms.DataGridView();
            this.DSGLogo = new System.Windows.Forms.PictureBox();
            this.chkTrace = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.focuserSelectorComboBox = new System.Windows.Forms.ComboBox();
            this.focuserSelectorLabel = new System.Windows.Forms.Label();
            this.stepRatioTextBox = new System.Windows.Forms.TextBox();
            this.backlashCompTextBox = new System.Windows.Forms.TextBox();
            this.stepRatioLabel = new System.Windows.Forms.Label();
            this.backlashCompLabel = new System.Windows.Forms.Label();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.equipmentProfilesLabel = new System.Windows.Forms.Label();
            this.profileChooser = new System.Windows.Forms.ComboBox();
            this.manageProfilesButton = new System.Windows.Forms.Button();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.newProfileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.renameProfileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteProfileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FilterPositionColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FilterNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FilterOffsetColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.filtersDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DSGLogo)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.contextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmdOK
            // 
            this.cmdOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.cmdOK.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdOK.Image = global::ASCOM.DarkSkyGeek.Properties.Resources.icon_ok_24;
            this.cmdOK.Location = new System.Drawing.Point(422, 673);
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
            this.cmdCancel.Location = new System.Drawing.Point(504, 673);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(74, 37);
            this.cmdCancel.TabIndex = 1;
            this.cmdCancel.UseVisualStyleBackColor = true;
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // descriptionLabel
            // 
            this.descriptionLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.descriptionLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.descriptionLabel.Location = new System.Drawing.Point(107, 12);
            this.descriptionLabel.Name = "descriptionLabel";
            this.descriptionLabel.Size = new System.Drawing.Size(471, 88);
            this.descriptionLabel.TabIndex = 11;
            this.descriptionLabel.Text = resources.GetString("descriptionLabel.Text");
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.filterWheelSelectorLabel);
            this.groupBox1.Controls.Add(this.filterWheelSelectorComboBox);
            this.groupBox1.Controls.Add(this.filterOffsetsWarning);
            this.groupBox1.Controls.Add(this.filtersDataGridView);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(13, 181);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(565, 291);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Filter Wheel / Autofocus Filter Settings";
            // 
            // filterWheelSelectorLabel
            // 
            this.filterWheelSelectorLabel.AutoSize = true;
            this.filterWheelSelectorLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.filterWheelSelectorLabel.Location = new System.Drawing.Point(6, 32);
            this.filterWheelSelectorLabel.Name = "filterWheelSelectorLabel";
            this.filterWheelSelectorLabel.Size = new System.Drawing.Size(120, 13);
            this.filterWheelSelectorLabel.TabIndex = 15;
            this.filterWheelSelectorLabel.Text = "Real filter wheel device:";
            // 
            // filterWheelSelectorComboBox
            // 
            this.filterWheelSelectorComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.filterWheelSelectorComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.filterWheelSelectorComboBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.filterWheelSelectorComboBox.FormattingEnabled = true;
            this.filterWheelSelectorComboBox.Location = new System.Drawing.Point(132, 29);
            this.filterWheelSelectorComboBox.Name = "filterWheelSelectorComboBox";
            this.filterWheelSelectorComboBox.Size = new System.Drawing.Size(427, 21);
            this.filterWheelSelectorComboBox.TabIndex = 14;
            this.filterWheelSelectorComboBox.SelectedIndexChanged += new System.EventHandler(this.filterWheelSelectorComboBox_SelectedIndexChanged);
            // 
            // filterOffsetsWarning
            // 
            this.filterOffsetsWarning.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.filterOffsetsWarning.AutoSize = true;
            this.filterOffsetsWarning.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.filterOffsetsWarning.Location = new System.Drawing.Point(6, 63);
            this.filterOffsetsWarning.Name = "filterOffsetsWarning";
            this.filterOffsetsWarning.Size = new System.Drawing.Size(517, 13);
            this.filterOffsetsWarning.TabIndex = 13;
            this.filterOffsetsWarning.Text = "Make sure that the following filters and offsets are in sync with the values conf" +
    "igured in the client application!";
            // 
            // filtersDataGridView
            // 
            this.filtersDataGridView.AllowUserToAddRows = false;
            this.filtersDataGridView.AllowUserToDeleteRows = false;
            this.filtersDataGridView.AllowUserToResizeColumns = false;
            this.filtersDataGridView.AllowUserToResizeRows = false;
            this.filtersDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.filtersDataGridView.BackgroundColor = System.Drawing.SystemColors.Control;
            this.filtersDataGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.filtersDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.filtersDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.FilterPositionColumn,
            this.FilterNameColumn,
            this.FilterOffsetColumn});
            this.filtersDataGridView.Location = new System.Drawing.Point(6, 88);
            this.filtersDataGridView.Name = "filtersDataGridView";
            this.filtersDataGridView.RowHeadersVisible = false;
            this.filtersDataGridView.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.filtersDataGridView.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.filtersDataGridView.Size = new System.Drawing.Size(553, 197);
            this.filtersDataGridView.TabIndex = 0;
            this.filtersDataGridView.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.filtersDataGridView_CellValidating);
            this.filtersDataGridView.Validating += new System.ComponentModel.CancelEventHandler(this.filtersDataGridView_Validating);
            this.filtersDataGridView.Validated += new System.EventHandler(this.filtersDataGridView_Validated);
            // 
            // DSGLogo
            // 
            this.DSGLogo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.DSGLogo.Image = global::ASCOM.DarkSkyGeek.Properties.Resources.darkskygeek;
            this.DSGLogo.Location = new System.Drawing.Point(13, 12);
            this.DSGLogo.Name = "DSGLogo";
            this.DSGLogo.Size = new System.Drawing.Size(88, 88);
            this.DSGLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.DSGLogo.TabIndex = 12;
            this.DSGLogo.TabStop = false;
            this.DSGLogo.Click += new System.EventHandler(this.BrowseToHomepage);
            this.DSGLogo.DoubleClick += new System.EventHandler(this.BrowseToHomepage);
            // 
            // chkTrace
            // 
            this.chkTrace.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkTrace.AutoSize = true;
            this.chkTrace.Location = new System.Drawing.Point(13, 689);
            this.chkTrace.Name = "chkTrace";
            this.chkTrace.Size = new System.Drawing.Size(69, 17);
            this.chkTrace.TabIndex = 13;
            this.chkTrace.Text = "Trace on";
            this.chkTrace.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.focuserSelectorComboBox);
            this.groupBox2.Controls.Add(this.focuserSelectorLabel);
            this.groupBox2.Controls.Add(this.stepRatioTextBox);
            this.groupBox2.Controls.Add(this.backlashCompTextBox);
            this.groupBox2.Controls.Add(this.stepRatioLabel);
            this.groupBox2.Controls.Add(this.backlashCompLabel);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(13, 503);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(565, 137);
            this.groupBox2.TabIndex = 14;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "OAG Focuser settings";
            // 
            // focuserSelectorComboBox
            // 
            this.focuserSelectorComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.focuserSelectorComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.focuserSelectorComboBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.focuserSelectorComboBox.FormattingEnabled = true;
            this.focuserSelectorComboBox.Location = new System.Drawing.Point(118, 38);
            this.focuserSelectorComboBox.Name = "focuserSelectorComboBox";
            this.focuserSelectorComboBox.Size = new System.Drawing.Size(441, 21);
            this.focuserSelectorComboBox.TabIndex = 24;
            this.focuserSelectorComboBox.SelectedIndexChanged += new System.EventHandler(this.focuserSelectorComboBox_SelectedIndexChanged);
            // 
            // focuserSelectorLabel
            // 
            this.focuserSelectorLabel.AutoSize = true;
            this.focuserSelectorLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.focuserSelectorLabel.Location = new System.Drawing.Point(6, 41);
            this.focuserSelectorLabel.Name = "focuserSelectorLabel";
            this.focuserSelectorLabel.Size = new System.Drawing.Size(106, 13);
            this.focuserSelectorLabel.TabIndex = 23;
            this.focuserSelectorLabel.Text = "OAG focuser device:";
            // 
            // stepRatioTextBox
            // 
            this.stepRatioTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.stepRatioTextBox.Location = new System.Drawing.Point(213, 103);
            this.stepRatioTextBox.Name = "stepRatioTextBox";
            this.stepRatioTextBox.Size = new System.Drawing.Size(56, 20);
            this.stepRatioTextBox.TabIndex = 22;
            this.stepRatioTextBox.Text = "1.0";
            this.stepRatioTextBox.Validating += new System.ComponentModel.CancelEventHandler(this.stepRatioTextBox_Validating);
            this.stepRatioTextBox.Validated += new System.EventHandler(this.stepRatioTextBox_Validated);
            // 
            // backlashCompTextBox
            // 
            this.backlashCompTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.backlashCompTextBox.Location = new System.Drawing.Point(213, 71);
            this.backlashCompTextBox.Name = "backlashCompTextBox";
            this.backlashCompTextBox.Size = new System.Drawing.Size(56, 20);
            this.backlashCompTextBox.TabIndex = 21;
            this.backlashCompTextBox.Text = "0";
            this.backlashCompTextBox.Validating += new System.ComponentModel.CancelEventHandler(this.backlashCompTextBox_Validating);
            this.backlashCompTextBox.Validated += new System.EventHandler(this.backlashCompTextBox_Validated);
            // 
            // stepRatioLabel
            // 
            this.stepRatioLabel.AutoSize = true;
            this.stepRatioLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.stepRatioLabel.Location = new System.Drawing.Point(6, 106);
            this.stepRatioLabel.Name = "stepRatioLabel";
            this.stepRatioLabel.Size = new System.Drawing.Size(196, 13);
            this.stepRatioLabel.TabIndex = 20;
            this.stepRatioLabel.Text = "Telescope and OAG focusers step ratio:";
            // 
            // backlashCompLabel
            // 
            this.backlashCompLabel.AutoSize = true;
            this.backlashCompLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.backlashCompLabel.Location = new System.Drawing.Point(6, 74);
            this.backlashCompLabel.Name = "backlashCompLabel";
            this.backlashCompLabel.Size = new System.Drawing.Size(201, 13);
            this.backlashCompLabel.TabIndex = 18;
            this.backlashCompLabel.Text = "Backlash compensation overshoot steps:";
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // equipmentProfilesLabel
            // 
            this.equipmentProfilesLabel.AutoSize = true;
            this.equipmentProfilesLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.equipmentProfilesLabel.Location = new System.Drawing.Point(10, 131);
            this.equipmentProfilesLabel.Name = "equipmentProfilesLabel";
            this.equipmentProfilesLabel.Size = new System.Drawing.Size(116, 13);
            this.equipmentProfilesLabel.TabIndex = 15;
            this.equipmentProfilesLabel.Text = "Equipment Profiles:";
            // 
            // profileChooser
            // 
            this.profileChooser.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.profileChooser.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.profileChooser.FormattingEnabled = true;
            this.profileChooser.Location = new System.Drawing.Point(132, 128);
            this.profileChooser.Name = "profileChooser";
            this.profileChooser.Size = new System.Drawing.Size(279, 21);
            this.profileChooser.TabIndex = 16;
            this.profileChooser.SelectedIndexChanged += new System.EventHandler(this.profileChooser_SelectedIndexChanged);
            // 
            // manageProfilesButton
            // 
            this.manageProfilesButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.manageProfilesButton.Location = new System.Drawing.Point(429, 125);
            this.manageProfilesButton.Name = "manageProfilesButton";
            this.manageProfilesButton.Size = new System.Drawing.Size(149, 28);
            this.manageProfilesButton.TabIndex = 17;
            this.manageProfilesButton.Text = "Manage Profiles  ▼";
            this.manageProfilesButton.UseVisualStyleBackColor = true;
            this.manageProfilesButton.Click += new System.EventHandler(this.manageProfilesButton_Click);
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newProfileMenuItem,
            this.renameProfileMenuItem,
            this.deleteProfileMenuItem});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(202, 70);
            // 
            // newProfileMenuItem
            // 
            this.newProfileMenuItem.Name = "newProfileMenuItem";
            this.newProfileMenuItem.Size = new System.Drawing.Size(201, 22);
            this.newProfileMenuItem.Text = "New Profile";
            this.newProfileMenuItem.Click += new System.EventHandler(this.newProfileMenuItem_Click);
            // 
            // renameProfileMenuItem
            // 
            this.renameProfileMenuItem.Name = "renameProfileMenuItem";
            this.renameProfileMenuItem.Size = new System.Drawing.Size(201, 22);
            this.renameProfileMenuItem.Text = "Rename Selected Profile";
            this.renameProfileMenuItem.Click += new System.EventHandler(this.renameProfileMenuItem_Click);
            // 
            // deleteProfileMenuItem
            // 
            this.deleteProfileMenuItem.Name = "deleteProfileMenuItem";
            this.deleteProfileMenuItem.Size = new System.Drawing.Size(201, 22);
            this.deleteProfileMenuItem.Text = "Delete Selected Profile";
            this.deleteProfileMenuItem.Click += new System.EventHandler(this.deleteProfileMenuItem_Click);
            // 
            // FilterPositionColumn
            // 
            this.FilterPositionColumn.FillWeight = 30F;
            this.FilterPositionColumn.HeaderText = "Filter Position";
            this.FilterPositionColumn.MinimumWidth = 120;
            this.FilterPositionColumn.Name = "FilterPositionColumn";
            this.FilterPositionColumn.ReadOnly = true;
            this.FilterPositionColumn.Width = 120;
            // 
            // FilterNameColumn
            // 
            this.FilterNameColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.FilterNameColumn.FillWeight = 40F;
            this.FilterNameColumn.HeaderText = "Filter Name";
            this.FilterNameColumn.MinimumWidth = 250;
            this.FilterNameColumn.Name = "FilterNameColumn";
            this.FilterNameColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // FilterOffsetColumn
            // 
            this.FilterOffsetColumn.FillWeight = 30F;
            this.FilterOffsetColumn.HeaderText = "Filter Offset";
            this.FilterOffsetColumn.MinimumWidth = 120;
            this.FilterOffsetColumn.Name = "FilterOffsetColumn";
            this.FilterOffsetColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.FilterOffsetColumn.Width = 120;
            // 
            // FilterWheelSetupDialogForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(590, 718);
            this.Controls.Add(this.manageProfilesButton);
            this.Controls.Add(this.profileChooser);
            this.Controls.Add(this.equipmentProfilesLabel);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.chkTrace);
            this.Controls.Add(this.DSGLogo);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.descriptionLabel);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.cmdOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FilterWheelSetupDialogForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DarkSkyGeek’s Filter Wheel Proxy For OAG Focuser";
            this.Load += new System.EventHandler(this.FilterWheelSetupDialogForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.filtersDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DSGLogo)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.contextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cmdOK;
        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.Label descriptionLabel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.PictureBox DSGLogo;
        private System.Windows.Forms.DataGridView filtersDataGridView;
        private System.Windows.Forms.CheckBox chkTrace;
        private System.Windows.Forms.Label filterOffsetsWarning;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label backlashCompLabel;
        private System.Windows.Forms.Label stepRatioLabel;
        private System.Windows.Forms.TextBox backlashCompTextBox;
        private System.Windows.Forms.TextBox stepRatioTextBox;
        private System.Windows.Forms.ErrorProvider errorProvider;
        private System.Windows.Forms.Label filterWheelSelectorLabel;
        private System.Windows.Forms.ComboBox filterWheelSelectorComboBox;
        private System.Windows.Forms.ComboBox focuserSelectorComboBox;
        private System.Windows.Forms.Label focuserSelectorLabel;
        private System.Windows.Forms.Label equipmentProfilesLabel;
        private System.Windows.Forms.Button manageProfilesButton;
        private System.Windows.Forms.ComboBox profileChooser;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem newProfileMenuItem;
        private System.Windows.Forms.ToolStripMenuItem renameProfileMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteProfileMenuItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn FilterPositionColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn FilterNameColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn FilterOffsetColumn;
    }
}