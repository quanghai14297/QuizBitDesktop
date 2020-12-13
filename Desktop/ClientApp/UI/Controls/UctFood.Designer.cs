namespace ClientApp.UI.Controls
{
    partial class UctFood
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinToolTip.UltraToolTipInfo ultraToolTipInfo1 = new Infragistics.Win.UltraWinToolTip.UltraToolTipInfo("Cua gạch nướng than xào mật ong chấm nước mắm", Infragistics.Win.ToolTipImage.Default, null, Infragistics.Win.DefaultableBoolean.Default);
            this.pnlBackground = new Infragistics.Win.Misc.UltraPanel();
            this.lblUnitPrice = new Infragistics.Win.Misc.UltraLabel();
            this.pnlBottom = new Infragistics.Win.Misc.UltraPanel();
            this.lblInventoryName = new Infragistics.Win.Misc.UltraLabel();
            this.ttpManager = new Infragistics.Win.UltraWinToolTip.UltraToolTipManager(this.components);
            this.lblFillBackground = new Infragistics.Win.Misc.UltraLabel();
            this.pnlBackground.ClientArea.SuspendLayout();
            this.pnlBackground.SuspendLayout();
            this.pnlBottom.ClientArea.SuspendLayout();
            this.pnlBottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlBackground
            // 
            appearance1.BackColor = System.Drawing.Color.Transparent;
            appearance1.BackColor2 = System.Drawing.Color.Transparent;
            appearance1.ImageBackground = global::ClientApp.Properties.Resources._00a59e71_1b27_4bd4_a838_0a375ccd1fdb;
            this.pnlBackground.Appearance = appearance1;
            this.pnlBackground.BorderStyle = Infragistics.Win.UIElementBorderStyle.None;
            // 
            // pnlBackground.ClientArea
            // 
            this.pnlBackground.ClientArea.Controls.Add(this.lblUnitPrice);
            this.pnlBackground.ClientArea.Controls.Add(this.lblFillBackground);
            this.pnlBackground.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBackground.Location = new System.Drawing.Point(7, 5);
            this.pnlBackground.Name = "pnlBackground";
            this.pnlBackground.Size = new System.Drawing.Size(116, 89);
            this.pnlBackground.TabIndex = 1;
            // 
            // lblUnitPrice
            // 
            appearance2.BackColor = System.Drawing.SystemColors.ActiveCaption;
            appearance2.BackColor2 = System.Drawing.SystemColors.ActiveCaption;
            appearance2.ForeColor = System.Drawing.Color.White;
            appearance2.TextHAlignAsString = "Center";
            appearance2.TextVAlignAsString = "Middle";
            this.lblUnitPrice.Appearance = appearance2;
            this.lblUnitPrice.AutoSize = true;
            this.lblUnitPrice.Location = new System.Drawing.Point(0, 0);
            this.lblUnitPrice.Name = "lblUnitPrice";
            this.lblUnitPrice.Padding = new System.Drawing.Size(3, 3);
            this.lblUnitPrice.Size = new System.Drawing.Size(60, 20);
            this.lblUnitPrice.TabIndex = 0;
            this.lblUnitPrice.Text = "600.000 đ";
            this.lblUnitPrice.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
            this.lblUnitPrice.UseOsThemes = Infragistics.Win.DefaultableBoolean.False;
            this.lblUnitPrice.Click += new System.EventHandler(this.lblUnitPrice_Click);
            // 
            // pnlBottom
            // 
            appearance4.BackColor = System.Drawing.Color.Transparent;
            appearance4.BackColor2 = System.Drawing.Color.Transparent;
            this.pnlBottom.Appearance = appearance4;
            this.pnlBottom.BorderStyle = Infragistics.Win.UIElementBorderStyle.None;
            // 
            // pnlBottom.ClientArea
            // 
            this.pnlBottom.ClientArea.Controls.Add(this.lblInventoryName);
            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottom.Location = new System.Drawing.Point(7, 94);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Size = new System.Drawing.Size(116, 31);
            this.pnlBottom.TabIndex = 2;
            // 
            // lblInventoryName
            // 
            appearance5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            appearance5.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            appearance5.TextHAlignAsString = "Center";
            appearance5.TextVAlignAsString = "Middle";
            this.lblInventoryName.Appearance = appearance5;
            this.lblInventoryName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblInventoryName.Location = new System.Drawing.Point(0, 0);
            this.lblInventoryName.Name = "lblInventoryName";
            this.lblInventoryName.Size = new System.Drawing.Size(116, 31);
            this.lblInventoryName.TabIndex = 0;
            this.lblInventoryName.Text = "Cua gạch nướng than xào mật ong chấm nước mắm";
            this.lblInventoryName.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
            this.lblInventoryName.UseOsThemes = Infragistics.Win.DefaultableBoolean.False;
            this.lblInventoryName.Click += new System.EventHandler(this.lblInventoryName_Click);
            // 
            // ttpManager
            // 
            this.ttpManager.ContainingControl = this;
            // 
            // lblFillBackground
            // 
            appearance3.BackColor = System.Drawing.Color.Transparent;
            appearance3.BackColor2 = System.Drawing.Color.Transparent;
            appearance3.BackColorAlpha = Infragistics.Win.Alpha.Transparent;
            appearance3.ImageAlpha = Infragistics.Win.Alpha.Transparent;
            appearance3.ImageBackgroundAlpha = Infragistics.Win.Alpha.Transparent;
            this.lblFillBackground.Appearance = appearance3;
            this.lblFillBackground.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblFillBackground.Location = new System.Drawing.Point(0, 0);
            this.lblFillBackground.Name = "lblFillBackground";
            this.lblFillBackground.Size = new System.Drawing.Size(116, 89);
            this.lblFillBackground.TabIndex = 1;
            this.lblFillBackground.Click += new System.EventHandler(this.lblFillBackground_Click);
            // 
            // UctFood
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.pnlBackground);
            this.Controls.Add(this.pnlBottom);
            this.Name = "UctFood";
            this.Padding = new System.Windows.Forms.Padding(7, 5, 7, 5);
            this.Size = new System.Drawing.Size(130, 130);
            ultraToolTipInfo1.ToolTipText = "Cua gạch nướng than xào mật ong chấm nước mắm";
            this.ttpManager.SetUltraToolTip(this, ultraToolTipInfo1);
            this.Load += new System.EventHandler(this.UctFood_Load);
            this.pnlBackground.ClientArea.ResumeLayout(false);
            this.pnlBackground.ClientArea.PerformLayout();
            this.pnlBackground.ResumeLayout(false);
            this.pnlBottom.ClientArea.ResumeLayout(false);
            this.pnlBottom.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Infragistics.Win.Misc.UltraPanel pnlBackground;
        private Infragistics.Win.Misc.UltraLabel lblUnitPrice;
        private Infragistics.Win.Misc.UltraPanel pnlBottom;
        private Infragistics.Win.Misc.UltraLabel lblInventoryName;
        private Infragistics.Win.UltraWinToolTip.UltraToolTipManager ttpManager;
        private Infragistics.Win.Misc.UltraLabel lblFillBackground;
    }
}
