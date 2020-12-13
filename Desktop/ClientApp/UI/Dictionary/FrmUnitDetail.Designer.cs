namespace ClientApp.UI.Dictionary
{
    partial class FrmUnitDetail
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
            this.txtDescription = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
            this.txtUnitName = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
            this.lblDescriptionTitle = new Infragistics.Win.Misc.UltraLabel();
            this.lblUnitNameTitle = new Infragistics.Win.Misc.UltraLabel();
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
            ((System.ComponentModel.ISupportInitialize)(this.txtUnitName)).BeginInit();
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
            this.pnlContent.Size = new System.Drawing.Size(463, 181);
            // 
            // pnlButton
            // 
            this.pnlButton.Location = new System.Drawing.Point(0, 148);
            this.pnlButton.Size = new System.Drawing.Size(463, 33);
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
            this.tabInfo.Size = new System.Drawing.Size(447, 131);
            this.tabInfo.TabIndex = 0;
            this.tabInfo.TabPageMargins.ForceSerialization = true;
            // 
            // tabGeneralInfo
            // 
            this.tabGeneralInfo.Controls.Add(this.txtDescription);
            this.tabGeneralInfo.Controls.Add(this.txtUnitName);
            this.tabGeneralInfo.Controls.Add(this.lblDescriptionTitle);
            this.tabGeneralInfo.Controls.Add(this.lblUnitNameTitle);
            this.tabGeneralInfo.Size = new System.Drawing.Size(445, 110);
            // 
            // bsDetail
            // 
            this.bsDetail.DataMember = "Unit";
            this.bsDetail.DataSource = this.dsDictionary;
            // 
            // ClientArea_Fill_Panel
            // 
            this.ClientArea_Fill_Panel.Size = new System.Drawing.Size(463, 181);
            // 
            // txtDescription
            // 
            this.txtDescription.AlwaysInEditMode = true;
            appearance1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(162)))), ((int)(((byte)(206)))));
            this.txtDescription.Appearance = appearance1;
            this.txtDescription.AutoSize = false;
            this.txtDescription.Location = new System.Drawing.Point(88, 32);
            this.txtDescription.Margin = new System.Windows.Forms.Padding(5);
            this.txtDescription.MaxLength = 255;
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Scrollbars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtDescription.Size = new System.Drawing.Size(350, 70);
            this.txtDescription.TabIndex = 3;
            this.txtDescription.Tag = "Mô tả";
            this.txtDescription.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
            this.txtDescription.UseOsThemes = Infragistics.Win.DefaultableBoolean.False;
            // 
            // txtUnitName
            // 
            appearance2.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(162)))), ((int)(((byte)(206)))));
            appearance2.BorderColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(162)))), ((int)(((byte)(206)))));
            this.txtUnitName.Appearance = appearance2;
            this.txtUnitName.AutoSize = false;
            this.txtUnitName.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            this.txtUnitName.Location = new System.Drawing.Point(88, 8);
            this.txtUnitName.MaxLength = 255;
            this.txtUnitName.Name = "txtUnitName";
            this.txtUnitName.Size = new System.Drawing.Size(350, 21);
            this.txtUnitName.TabIndex = 1;
            this.txtUnitName.Tag = "Tên đơn vị tính";
            this.txtUnitName.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
            this.txtUnitName.UseOsThemes = Infragistics.Win.DefaultableBoolean.False;
            this.txtUnitName.Validating += new System.ComponentModel.CancelEventHandler(this.Control_Validating);
            // 
            // lblDescriptionTitle
            // 
            appearance3.TextVAlignAsString = "Middle";
            this.lblDescriptionTitle.Appearance = appearance3;
            this.lblDescriptionTitle.Location = new System.Drawing.Point(8, 32);
            this.lblDescriptionTitle.Name = "lblDescriptionTitle";
            this.lblDescriptionTitle.Size = new System.Drawing.Size(80, 23);
            this.lblDescriptionTitle.TabIndex = 2;
            this.lblDescriptionTitle.Text = "Mô &tả";
            // 
            // lblUnitNameTitle
            // 
            appearance4.ImageVAlign = Infragistics.Win.VAlign.Middle;
            appearance4.TextVAlignAsString = "Middle";
            this.lblUnitNameTitle.Appearance = appearance4;
            this.lblUnitNameTitle.Location = new System.Drawing.Point(8, 8);
            this.lblUnitNameTitle.Name = "lblUnitNameTitle";
            this.lblUnitNameTitle.Size = new System.Drawing.Size(80, 23);
            this.lblUnitNameTitle.TabIndex = 0;
            this.lblUnitNameTitle.Text = "Tê&n (*)";
            // 
            // FrmUnitDetail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(465, 213);
            this.Name = "FrmUnitDetail";
            this.Tag = "Đơn vị tính";
            this.Text = "FrmUnitDetail";
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
            ((System.ComponentModel.ISupportInitialize)(this.txtUnitName)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        internal Infragistics.Win.UltraWinEditors.UltraTextEditor txtDescription;
        internal Infragistics.Win.UltraWinEditors.UltraTextEditor txtUnitName;
        internal Infragistics.Win.Misc.UltraLabel lblDescriptionTitle;
        internal Infragistics.Win.Misc.UltraLabel lblUnitNameTitle;
    }
}