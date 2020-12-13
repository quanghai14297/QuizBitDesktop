namespace ClientApp.UI.Dictionary
{
    partial class FrmAreaDetail
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
            Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
            this.txtNumberOfTable = new Infragistics.Win.UltraWinMaskedEdit.UltraMaskedEdit();
            this.ultraLabel5 = new Infragistics.Win.Misc.UltraLabel();
            this.txtAreaName = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
            this.lblNameTitle = new Infragistics.Win.Misc.UltraLabel();
            this.txtDescription = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
            this.lblDescriptionTitle = new Infragistics.Win.Misc.UltraLabel();
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
            ((System.ComponentModel.ISupportInitialize)(this.txtAreaName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDescription)).BeginInit();
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
            this.pnlContent.Size = new System.Drawing.Size(464, 205);
            // 
            // pnlButton
            // 
            this.pnlButton.Location = new System.Drawing.Point(0, 172);
            this.pnlButton.Size = new System.Drawing.Size(464, 33);
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
            // 
            // tabInfo
            // 
            this.tabInfo.Size = new System.Drawing.Size(447, 155);
            this.tabInfo.TabPageMargins.ForceSerialization = true;
            // 
            // tabGeneralInfo
            // 
            this.tabGeneralInfo.Controls.Add(this.txtNumberOfTable);
            this.tabGeneralInfo.Controls.Add(this.ultraLabel5);
            this.tabGeneralInfo.Controls.Add(this.txtAreaName);
            this.tabGeneralInfo.Controls.Add(this.lblNameTitle);
            this.tabGeneralInfo.Controls.Add(this.txtDescription);
            this.tabGeneralInfo.Controls.Add(this.lblDescriptionTitle);
            this.tabGeneralInfo.Size = new System.Drawing.Size(445, 134);
            // 
            // ClientArea_Fill_Panel
            // 
            this.ClientArea_Fill_Panel.Size = new System.Drawing.Size(464, 205);
            // 
            // txtNumberOfTable
            // 
            appearance1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(162)))), ((int)(((byte)(206)))));
            appearance1.BorderColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(162)))), ((int)(((byte)(206)))));
            appearance1.TextHAlignAsString = "Right";
            this.txtNumberOfTable.Appearance = appearance1;
            this.txtNumberOfTable.AutoSize = false;
            this.txtNumberOfTable.EditAs = Infragistics.Win.UltraWinMaskedEdit.EditAsType.UseSpecifiedMask;
            this.txtNumberOfTable.InputMask = "{LOC}nnn";
            this.txtNumberOfTable.Location = new System.Drawing.Point(88, 32);
            this.txtNumberOfTable.MaxValue = 999;
            this.txtNumberOfTable.MinValue = 0;
            this.txtNumberOfTable.Name = "txtNumberOfTable";
            this.txtNumberOfTable.NonAutoSizeHeight = 20;
            this.txtNumberOfTable.Size = new System.Drawing.Size(350, 20);
            this.txtNumberOfTable.SpinButtonDisplayStyle = Infragistics.Win.SpinButtonDisplayStyle.OnRight;
            this.txtNumberOfTable.TabIndex = 20;
            this.txtNumberOfTable.Tag = "Số lượng bàn";
            this.txtNumberOfTable.Text = ".., ";
            this.txtNumberOfTable.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
            this.txtNumberOfTable.UseOsThemes = Infragistics.Win.DefaultableBoolean.False;
            // 
            // ultraLabel5
            // 
            appearance2.ImageVAlign = Infragistics.Win.VAlign.Middle;
            appearance2.TextVAlignAsString = "Middle";
            this.ultraLabel5.Appearance = appearance2;
            this.ultraLabel5.Location = new System.Drawing.Point(8, 32);
            this.ultraLabel5.Name = "ultraLabel5";
            this.ultraLabel5.Size = new System.Drawing.Size(80, 23);
            this.ultraLabel5.TabIndex = 19;
            this.ultraLabel5.Text = "&Số lượng bàn";
            // 
            // txtAreaName
            // 
            appearance3.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(162)))), ((int)(((byte)(206)))));
            appearance3.BorderColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(162)))), ((int)(((byte)(206)))));
            this.txtAreaName.Appearance = appearance3;
            this.txtAreaName.AutoSize = false;
            this.txtAreaName.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            this.txtAreaName.Location = new System.Drawing.Point(88, 8);
            this.txtAreaName.MaxLength = 255;
            this.txtAreaName.Name = "txtAreaName";
            this.txtAreaName.Size = new System.Drawing.Size(350, 21);
            this.txtAreaName.TabIndex = 18;
            this.txtAreaName.Tag = "Tên khu vực";
            this.txtAreaName.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
            this.txtAreaName.UseOsThemes = Infragistics.Win.DefaultableBoolean.False;
            this.txtAreaName.Validating += new System.ComponentModel.CancelEventHandler(this.Control_Validating);
            // 
            // lblNameTitle
            // 
            appearance4.ImageVAlign = Infragistics.Win.VAlign.Middle;
            appearance4.TextVAlignAsString = "Middle";
            this.lblNameTitle.Appearance = appearance4;
            this.lblNameTitle.Location = new System.Drawing.Point(8, 8);
            this.lblNameTitle.Name = "lblNameTitle";
            this.lblNameTitle.Size = new System.Drawing.Size(80, 23);
            this.lblNameTitle.TabIndex = 17;
            this.lblNameTitle.Text = "Tê&n (*)";
            // 
            // txtDescription
            // 
            this.txtDescription.AlwaysInEditMode = true;
            appearance5.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(162)))), ((int)(((byte)(206)))));
            this.txtDescription.Appearance = appearance5;
            this.txtDescription.AutoSize = false;
            this.txtDescription.Location = new System.Drawing.Point(88, 56);
            this.txtDescription.Margin = new System.Windows.Forms.Padding(5);
            this.txtDescription.MaxLength = 255;
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Scrollbars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtDescription.Size = new System.Drawing.Size(350, 70);
            this.txtDescription.TabIndex = 22;
            this.txtDescription.Tag = "Mô tả";
            this.txtDescription.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
            this.txtDescription.UseOsThemes = Infragistics.Win.DefaultableBoolean.False;
            // 
            // lblDescriptionTitle
            // 
            appearance6.TextVAlignAsString = "Middle";
            this.lblDescriptionTitle.Appearance = appearance6;
            this.lblDescriptionTitle.Location = new System.Drawing.Point(8, 56);
            this.lblDescriptionTitle.Name = "lblDescriptionTitle";
            this.lblDescriptionTitle.Size = new System.Drawing.Size(80, 23);
            this.lblDescriptionTitle.TabIndex = 21;
            this.lblDescriptionTitle.Text = "Mô &tả";
            // 
            // FrmAreaDetail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(466, 237);
            this.Name = "FrmAreaDetail";
            this.Tag = "Khu vực";
            this.Text = "FrmAreaDetail";
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
            ((System.ComponentModel.ISupportInitialize)(this.txtAreaName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDescription)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Infragistics.Win.UltraWinMaskedEdit.UltraMaskedEdit txtNumberOfTable;
        internal Infragistics.Win.Misc.UltraLabel ultraLabel5;
        internal Infragistics.Win.UltraWinEditors.UltraTextEditor txtAreaName;
        internal Infragistics.Win.Misc.UltraLabel lblNameTitle;
        internal Infragistics.Win.UltraWinEditors.UltraTextEditor txtDescription;
        internal Infragistics.Win.Misc.UltraLabel lblDescriptionTitle;
    }
}