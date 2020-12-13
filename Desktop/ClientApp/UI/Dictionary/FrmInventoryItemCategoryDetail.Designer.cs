namespace ClientApp.UI.Dictionary
{
    partial class FrmInventoryItemCategoryDetail
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
            Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance8 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance9 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance7 = new Infragistics.Win.Appearance();
            this.txtDescription = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
            this.txtInventoryItemCategoryCode = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
            this.lblDescriptionTitle = new Infragistics.Win.Misc.UltraLabel();
            this.lblInventoryItemCategoryCodeTitle = new Infragistics.Win.Misc.UltraLabel();
            this.txtInventoryItemCategoryName = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
            this.lblInventoryItemCategoryNameTitle = new Infragistics.Win.Misc.UltraLabel();
            ((System.ComponentModel.ISupportInitialize)(this.tbrFunction)).BeginInit();
            this.pnlContent.ClientArea.SuspendLayout();
            this.pnlContent.SuspendLayout();
            this.pnlButton.ClientArea.SuspendLayout();
            this.pnlButton.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tabInfo)).BeginInit();
            this.tabInfo.SuspendLayout();
            this.tabGeneralInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bsDetail)).BeginInit();
            this.ClientArea_Fill_Panel.ClientArea.SuspendLayout();
            this.ClientArea_Fill_Panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dsDictionary)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDescription)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtInventoryItemCategoryCode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtInventoryItemCategoryName)).BeginInit();
            this.SuspendLayout();
            // 
            // tbrFunction
            // 
            this.tbrFunction.MenuSettings.ForceSerialization = true;
            this.tbrFunction.ToolbarSettings.ForceSerialization = true;
            // 
            // pnlContent
            // 
            this.pnlContent.Location = new System.Drawing.Point(1, 31);
            this.pnlContent.Size = new System.Drawing.Size(464, 206);
            // 
            // pnlButton
            // 
            this.pnlButton.Location = new System.Drawing.Point(0, 173);
            this.pnlButton.Size = new System.Drawing.Size(464, 33);
            this.pnlButton.TabIndex = 1;
            // 
            // btnSaveAdd
            // 
            this.btnSaveAdd.Location = new System.Drawing.Point(267, 0);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(184, 0);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(380, 0);
            this.btnCancel.TabIndex = 2;
            // 
            // tabInfo
            // 
            this.tabInfo.Size = new System.Drawing.Size(447, 155);
            this.tabInfo.TabIndex = 0;
            this.tabInfo.TabPageMargins.ForceSerialization = true;
            // 
            // tabGeneralInfo
            // 
            this.tabGeneralInfo.Controls.Add(this.txtInventoryItemCategoryName);
            this.tabGeneralInfo.Controls.Add(this.lblInventoryItemCategoryNameTitle);
            this.tabGeneralInfo.Controls.Add(this.txtDescription);
            this.tabGeneralInfo.Controls.Add(this.txtInventoryItemCategoryCode);
            this.tabGeneralInfo.Controls.Add(this.lblDescriptionTitle);
            this.tabGeneralInfo.Controls.Add(this.lblInventoryItemCategoryCodeTitle);
            this.tabGeneralInfo.Size = new System.Drawing.Size(445, 134);
            // 
            // ClientArea_Fill_Panel
            // 
            this.ClientArea_Fill_Panel.Size = new System.Drawing.Size(464, 206);
            // 
            // txtDescription
            // 
            this.txtDescription.AlwaysInEditMode = true;
            appearance3.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(162)))), ((int)(((byte)(206)))));
            this.txtDescription.Appearance = appearance3;
            this.txtDescription.AutoSize = false;
            this.txtDescription.Location = new System.Drawing.Point(88, 56);
            this.txtDescription.Margin = new System.Windows.Forms.Padding(5);
            this.txtDescription.MaxLength = 255;
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Scrollbars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtDescription.Size = new System.Drawing.Size(350, 70);
            this.txtDescription.TabIndex = 5;
            this.txtDescription.Tag = "Mô tả";
            this.txtDescription.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
            this.txtDescription.UseOsThemes = Infragistics.Win.DefaultableBoolean.False;
            // 
            // txtInventoryItemCategoryCode
            // 
            appearance4.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(162)))), ((int)(((byte)(206)))));
            appearance4.BorderColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(162)))), ((int)(((byte)(206)))));
            this.txtInventoryItemCategoryCode.Appearance = appearance4;
            this.txtInventoryItemCategoryCode.AutoSize = false;
            this.txtInventoryItemCategoryCode.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            this.txtInventoryItemCategoryCode.Location = new System.Drawing.Point(88, 8);
            this.txtInventoryItemCategoryCode.MaxLength = 50;
            this.txtInventoryItemCategoryCode.Name = "txtInventoryItemCategoryCode";
            this.txtInventoryItemCategoryCode.Size = new System.Drawing.Size(350, 21);
            this.txtInventoryItemCategoryCode.TabIndex = 1;
            this.txtInventoryItemCategoryCode.Tag = "Mã Nhóm thực đơn";
            this.txtInventoryItemCategoryCode.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
            this.txtInventoryItemCategoryCode.UseOsThemes = Infragistics.Win.DefaultableBoolean.False;
            this.txtInventoryItemCategoryCode.Validating += new System.ComponentModel.CancelEventHandler(this.Control_Validating);
            // 
            // lblDescriptionTitle
            // 
            appearance8.TextVAlignAsString = "Middle";
            this.lblDescriptionTitle.Appearance = appearance8;
            this.lblDescriptionTitle.Location = new System.Drawing.Point(8, 56);
            this.lblDescriptionTitle.Name = "lblDescriptionTitle";
            this.lblDescriptionTitle.Size = new System.Drawing.Size(80, 23);
            this.lblDescriptionTitle.TabIndex = 4;
            this.lblDescriptionTitle.Text = "Mô &tả";
            // 
            // lblInventoryItemCategoryCodeTitle
            // 
            appearance9.ImageVAlign = Infragistics.Win.VAlign.Middle;
            appearance9.TextVAlignAsString = "Middle";
            this.lblInventoryItemCategoryCodeTitle.Appearance = appearance9;
            this.lblInventoryItemCategoryCodeTitle.Location = new System.Drawing.Point(8, 8);
            this.lblInventoryItemCategoryCodeTitle.Name = "lblInventoryItemCategoryCodeTitle";
            this.lblInventoryItemCategoryCodeTitle.Size = new System.Drawing.Size(80, 23);
            this.lblInventoryItemCategoryCodeTitle.TabIndex = 0;
            this.lblInventoryItemCategoryCodeTitle.Text = "&Mã (*)";
            // 
            // txtInventoryItemCategoryName
            // 
            appearance1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(162)))), ((int)(((byte)(206)))));
            appearance1.BorderColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(162)))), ((int)(((byte)(206)))));
            this.txtInventoryItemCategoryName.Appearance = appearance1;
            this.txtInventoryItemCategoryName.AutoSize = false;
            this.txtInventoryItemCategoryName.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            this.txtInventoryItemCategoryName.Location = new System.Drawing.Point(88, 32);
            this.txtInventoryItemCategoryName.MaxLength = 255;
            this.txtInventoryItemCategoryName.Name = "txtInventoryItemCategoryName";
            this.txtInventoryItemCategoryName.Size = new System.Drawing.Size(350, 21);
            this.txtInventoryItemCategoryName.TabIndex = 3;
            this.txtInventoryItemCategoryName.Tag = "Tên Nhóm thực đơn";
            this.txtInventoryItemCategoryName.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
            this.txtInventoryItemCategoryName.UseOsThemes = Infragistics.Win.DefaultableBoolean.False;
            this.txtInventoryItemCategoryName.Validating += new System.ComponentModel.CancelEventHandler(this.Control_Validating);
            // 
            // lblInventoryItemCategoryNameTitle
            // 
            appearance7.ImageVAlign = Infragistics.Win.VAlign.Middle;
            appearance7.TextVAlignAsString = "Middle";
            this.lblInventoryItemCategoryNameTitle.Appearance = appearance7;
            this.lblInventoryItemCategoryNameTitle.Location = new System.Drawing.Point(8, 32);
            this.lblInventoryItemCategoryNameTitle.Name = "lblInventoryItemCategoryNameTitle";
            this.lblInventoryItemCategoryNameTitle.Size = new System.Drawing.Size(80, 23);
            this.lblInventoryItemCategoryNameTitle.TabIndex = 2;
            this.lblInventoryItemCategoryNameTitle.Text = "Tê&n (*)";
            // 
            // FrmInventoryItemCategoryDetail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(466, 238);
            this.Name = "FrmInventoryItemCategoryDetail";
            this.Tag = "Nhóm thực đơn";
            this.Text = "FrmInventoryItemCategoryDetail";
            ((System.ComponentModel.ISupportInitialize)(this.tbrFunction)).EndInit();
            this.pnlContent.ClientArea.ResumeLayout(false);
            this.pnlContent.ResumeLayout(false);
            this.pnlButton.ClientArea.ResumeLayout(false);
            this.pnlButton.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tabInfo)).EndInit();
            this.tabInfo.ResumeLayout(false);
            this.tabGeneralInfo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.bsDetail)).EndInit();
            this.ClientArea_Fill_Panel.ClientArea.ResumeLayout(false);
            this.ClientArea_Fill_Panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dsDictionary)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDescription)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtInventoryItemCategoryCode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtInventoryItemCategoryName)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        internal Infragistics.Win.UltraWinEditors.UltraTextEditor txtDescription;
        internal Infragistics.Win.UltraWinEditors.UltraTextEditor txtInventoryItemCategoryCode;
        internal Infragistics.Win.Misc.UltraLabel lblDescriptionTitle;
        internal Infragistics.Win.Misc.UltraLabel lblInventoryItemCategoryCodeTitle;
        internal Infragistics.Win.UltraWinEditors.UltraTextEditor txtInventoryItemCategoryName;
        internal Infragistics.Win.Misc.UltraLabel lblInventoryItemCategoryNameTitle;
    }
}