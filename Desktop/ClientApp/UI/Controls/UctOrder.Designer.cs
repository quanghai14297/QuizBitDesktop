namespace ClientApp.UI.Controls
{
    partial class UctOrder
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
            Infragistics.Win.Appearance appearance7 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinToolTip.UltraToolTipInfo ultraToolTipInfo2 = new Infragistics.Win.UltraWinToolTip.UltraToolTipInfo("Thanh toán", Infragistics.Win.ToolTipImage.Default, null, Infragistics.Win.DefaultableBoolean.Default);
            Infragistics.Win.Appearance appearance9 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinToolTip.UltraToolTipInfo ultraToolTipInfo3 = new Infragistics.Win.UltraWinToolTip.UltraToolTipInfo("Trả món", Infragistics.Win.ToolTipImage.Default, null, Infragistics.Win.DefaultableBoolean.Default);
            Infragistics.Win.Appearance appearance10 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinToolTip.UltraToolTipInfo ultraToolTipInfo4 = new Infragistics.Win.UltraWinToolTip.UltraToolTipInfo("Hủy Order", Infragistics.Win.ToolTipImage.Default, null, Infragistics.Win.DefaultableBoolean.Default);
            Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinToolTip.UltraToolTipInfo ultraToolTipInfo1 = new Infragistics.Win.UltraWinToolTip.UltraToolTipInfo("Thành tiền", Infragistics.Win.ToolTipImage.Default, null, Infragistics.Win.DefaultableBoolean.Default);
            Infragistics.Win.Appearance appearance12 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinToolTip.UltraToolTipInfo ultraToolTipInfo5 = new Infragistics.Win.UltraWinToolTip.UltraToolTipInfo("Mã Order", Infragistics.Win.ToolTipImage.Default, null, Infragistics.Win.DefaultableBoolean.Default);
            Infragistics.Win.Appearance appearance13 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinToolTip.UltraToolTipInfo ultraToolTipInfo6 = new Infragistics.Win.UltraWinToolTip.UltraToolTipInfo("Số người tham dự", Infragistics.Win.ToolTipImage.Default, null, Infragistics.Win.DefaultableBoolean.Default);
            Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance8 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance11 = new Infragistics.Win.Appearance();
            this.ttmManager_UserControl = new Infragistics.Win.UltraWinToolTip.UltraToolTipManager(this.components);
            this.btnPayment = new Infragistics.Win.Misc.UltraButton();
            this.btnReturnFood = new Infragistics.Win.Misc.UltraButton();
            this.btnCancel = new Infragistics.Win.Misc.UltraButton();
            this.lblTotalAmount = new Infragistics.Win.Misc.UltraLabel();
            this.lblOrderNo = new Infragistics.Win.Misc.UltraLabel();
            this.lblNumberOfPeople = new Infragistics.Win.Misc.UltraLabel();
            this.pnlBody = new Infragistics.Win.Misc.UltraPanel();
            this.pnlContent = new Infragistics.Win.Misc.UltraPanel();
            this.pnlDetail = new Infragistics.Win.Misc.UltraPanel();
            this.pnlSpace = new Infragistics.Win.Misc.UltraPanel();
            this.pnlArea = new Infragistics.Win.Misc.UltraPanel();
            this.lblTable = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
            this.lblArea = new Infragistics.Win.Misc.UltraLabel();
            this.pnlBottom = new Infragistics.Win.Misc.UltraPanel();
            this.ultraPanel1 = new Infragistics.Win.Misc.UltraPanel();
            this.pnlTop = new Infragistics.Win.Misc.UltraPanel();
            this.pnlBody.ClientArea.SuspendLayout();
            this.pnlBody.SuspendLayout();
            this.pnlContent.ClientArea.SuspendLayout();
            this.pnlContent.SuspendLayout();
            this.pnlDetail.ClientArea.SuspendLayout();
            this.pnlDetail.SuspendLayout();
            this.pnlSpace.SuspendLayout();
            this.pnlArea.ClientArea.SuspendLayout();
            this.pnlArea.SuspendLayout();
            this.pnlBottom.ClientArea.SuspendLayout();
            this.pnlBottom.SuspendLayout();
            this.ultraPanel1.SuspendLayout();
            this.pnlTop.ClientArea.SuspendLayout();
            this.pnlTop.SuspendLayout();
            this.SuspendLayout();
            // 
            // ttmManager_UserControl
            // 
            this.ttmManager_UserControl.ContainingControl = this;
            // 
            // btnPayment
            // 
            appearance7.BackColor = System.Drawing.SystemColors.ControlLight;
            appearance7.Image = global::ClientApp.Properties.Resources.btnPay;
            this.btnPayment.Appearance = appearance7;
            this.btnPayment.AutoSize = true;
            this.btnPayment.ButtonStyle = Infragistics.Win.UIElementButtonStyle.FlatBorderless;
            this.btnPayment.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnPayment.Location = new System.Drawing.Point(40, 0);
            this.btnPayment.Name = "btnPayment";
            this.btnPayment.Padding = new System.Drawing.Size(7, 0);
            this.btnPayment.Size = new System.Drawing.Size(38, 27);
            this.btnPayment.TabIndex = 1;
            this.btnPayment.TabStop = false;
            ultraToolTipInfo2.ToolTipText = "Thanh toán";
            this.ttmManager_UserControl.SetUltraToolTip(this.btnPayment, ultraToolTipInfo2);
            this.btnPayment.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
            this.btnPayment.UseOsThemes = Infragistics.Win.DefaultableBoolean.False;
            this.btnPayment.Click += new System.EventHandler(this.btnPayment_Click);
            // 
            // btnReturnFood
            // 
            appearance9.BackColor = System.Drawing.SystemColors.ControlLight;
            appearance9.Image = global::ClientApp.Properties.Resources.btnReturnFood;
            this.btnReturnFood.Appearance = appearance9;
            this.btnReturnFood.AutoSize = true;
            this.btnReturnFood.ButtonStyle = Infragistics.Win.UIElementButtonStyle.FlatBorderless;
            this.btnReturnFood.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnReturnFood.Location = new System.Drawing.Point(0, 0);
            this.btnReturnFood.Name = "btnReturnFood";
            this.btnReturnFood.Padding = new System.Drawing.Size(7, 0);
            this.btnReturnFood.Size = new System.Drawing.Size(38, 27);
            this.btnReturnFood.TabIndex = 0;
            this.btnReturnFood.TabStop = false;
            ultraToolTipInfo3.ToolTipText = "Trả món";
            this.ttmManager_UserControl.SetUltraToolTip(this.btnReturnFood, ultraToolTipInfo3);
            this.btnReturnFood.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
            this.btnReturnFood.UseOsThemes = Infragistics.Win.DefaultableBoolean.False;
            this.btnReturnFood.Click += new System.EventHandler(this.btnReturnFood_Click);
            // 
            // btnCancel
            // 
            appearance10.BackColor = System.Drawing.SystemColors.ControlLight;
            appearance10.Image = global::ClientApp.Properties.Resources.btnCancel;
            this.btnCancel.Appearance = appearance10;
            this.btnCancel.AutoSize = true;
            this.btnCancel.ButtonStyle = Infragistics.Win.UIElementButtonStyle.FlatBorderless;
            this.btnCancel.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnCancel.Location = new System.Drawing.Point(166, 0);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Padding = new System.Drawing.Size(7, 0);
            this.btnCancel.Size = new System.Drawing.Size(38, 27);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.TabStop = false;
            ultraToolTipInfo4.ToolTipText = "Hủy Order";
            this.ttmManager_UserControl.SetUltraToolTip(this.btnCancel, ultraToolTipInfo4);
            this.btnCancel.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
            this.btnCancel.UseOsThemes = Infragistics.Win.DefaultableBoolean.False;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lblTotalAmount
            // 
            appearance2.TextHAlignAsString = "Center";
            appearance2.TextVAlignAsString = "Middle";
            this.lblTotalAmount.Appearance = appearance2;
            this.lblTotalAmount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTotalAmount.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalAmount.Location = new System.Drawing.Point(0, 0);
            this.lblTotalAmount.Name = "lblTotalAmount";
            this.lblTotalAmount.Size = new System.Drawing.Size(126, 54);
            this.lblTotalAmount.TabIndex = 0;
            this.lblTotalAmount.Text = "100.000.000";
            ultraToolTipInfo1.ToolTipText = "Thành tiền";
            this.ttmManager_UserControl.SetUltraToolTip(this.lblTotalAmount, ultraToolTipInfo1);
            this.lblTotalAmount.Click += new System.EventHandler(this.Label_Click);
            this.lblTotalAmount.DoubleClick += new System.EventHandler(this.Label_DoubleClick);
            // 
            // lblOrderNo
            // 
            appearance12.ForeColor = System.Drawing.Color.White;
            appearance12.TextHAlignAsString = "Left";
            appearance12.TextVAlignAsString = "Middle";
            this.lblOrderNo.Appearance = appearance12;
            this.lblOrderNo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblOrderNo.Location = new System.Drawing.Point(0, 0);
            this.lblOrderNo.Name = "lblOrderNo";
            this.lblOrderNo.Padding = new System.Drawing.Size(5, 0);
            this.lblOrderNo.Size = new System.Drawing.Size(155, 27);
            this.lblOrderNo.TabIndex = 0;
            this.lblOrderNo.Text = "1.120";
            ultraToolTipInfo5.ToolTipText = "Mã Order";
            this.ttmManager_UserControl.SetUltraToolTip(this.lblOrderNo, ultraToolTipInfo5);
            this.lblOrderNo.Click += new System.EventHandler(this.Label_Click);
            this.lblOrderNo.DoubleClick += new System.EventHandler(this.Label_DoubleClick);
            // 
            // lblNumberOfPeople
            // 
            appearance13.ForeColor = System.Drawing.Color.White;
            appearance13.Image = global::ClientApp.Properties.Resources.Customers___White___36x36;
            appearance13.TextHAlignAsString = "Right";
            appearance13.TextVAlignAsString = "Middle";
            this.lblNumberOfPeople.Appearance = appearance13;
            this.lblNumberOfPeople.AutoSize = true;
            this.lblNumberOfPeople.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblNumberOfPeople.Location = new System.Drawing.Point(155, 0);
            this.lblNumberOfPeople.Name = "lblNumberOfPeople";
            this.lblNumberOfPeople.Padding = new System.Drawing.Size(5, 0);
            this.lblNumberOfPeople.Size = new System.Drawing.Size(49, 27);
            this.lblNumberOfPeople.TabIndex = 1;
            this.lblNumberOfPeople.Text = "999";
            ultraToolTipInfo6.ToolTipText = "Số người tham dự";
            this.ttmManager_UserControl.SetUltraToolTip(this.lblNumberOfPeople, ultraToolTipInfo6);
            this.lblNumberOfPeople.Click += new System.EventHandler(this.Label_Click);
            this.lblNumberOfPeople.DoubleClick += new System.EventHandler(this.Label_DoubleClick);
            // 
            // pnlBody
            // 
            appearance1.BorderColor = System.Drawing.Color.Black;
            this.pnlBody.Appearance = appearance1;
            this.pnlBody.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            // 
            // pnlBody.ClientArea
            // 
            this.pnlBody.ClientArea.Controls.Add(this.pnlContent);
            this.pnlBody.ClientArea.Controls.Add(this.pnlBottom);
            this.pnlBody.ClientArea.Controls.Add(this.pnlTop);
            this.pnlBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBody.Location = new System.Drawing.Point(7, 5);
            this.pnlBody.Name = "pnlBody";
            this.pnlBody.Size = new System.Drawing.Size(206, 110);
            this.pnlBody.TabIndex = 3;
            // 
            // pnlContent
            // 
            // 
            // pnlContent.ClientArea
            // 
            this.pnlContent.ClientArea.Controls.Add(this.pnlDetail);
            this.pnlContent.ClientArea.Controls.Add(this.pnlSpace);
            this.pnlContent.ClientArea.Controls.Add(this.pnlArea);
            this.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContent.Location = new System.Drawing.Point(0, 27);
            this.pnlContent.Name = "pnlContent";
            this.pnlContent.Size = new System.Drawing.Size(204, 54);
            this.pnlContent.TabIndex = 3;
            // 
            // pnlDetail
            // 
            // 
            // pnlDetail.ClientArea
            // 
            this.pnlDetail.ClientArea.Controls.Add(this.lblTotalAmount);
            this.pnlDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlDetail.Location = new System.Drawing.Point(78, 0);
            this.pnlDetail.Name = "pnlDetail";
            this.pnlDetail.Size = new System.Drawing.Size(126, 54);
            this.pnlDetail.TabIndex = 2;
            // 
            // pnlSpace
            // 
            appearance3.BackColor = System.Drawing.Color.DarkGray;
            this.pnlSpace.Appearance = appearance3;
            this.pnlSpace.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlSpace.Location = new System.Drawing.Point(77, 0);
            this.pnlSpace.Name = "pnlSpace";
            this.pnlSpace.Size = new System.Drawing.Size(1, 54);
            this.pnlSpace.TabIndex = 1;
            // 
            // pnlArea
            // 
            // 
            // pnlArea.ClientArea
            // 
            this.pnlArea.ClientArea.Controls.Add(this.lblTable);
            this.pnlArea.ClientArea.Controls.Add(this.lblArea);
            this.pnlArea.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlArea.Location = new System.Drawing.Point(0, 0);
            this.pnlArea.Name = "pnlArea";
            this.pnlArea.Size = new System.Drawing.Size(77, 54);
            this.pnlArea.TabIndex = 0;
            // 
            // lblTable
            // 
            appearance4.TextHAlignAsString = "Center";
            appearance4.TextVAlignAsString = "Top";
            this.lblTable.Appearance = appearance4;
            this.lblTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTable.Location = new System.Drawing.Point(0, 28);
            this.lblTable.Name = "lblTable";
            this.lblTable.Size = new System.Drawing.Size(77, 26);
            this.lblTable.TabIndex = 1;
            this.lblTable.TabStop = true;
            this.lblTable.Value = "Bàn 15";
            this.lblTable.Click += new System.EventHandler(this.Label_Click);
            this.lblTable.DoubleClick += new System.EventHandler(this.Label_DoubleClick);
            // 
            // lblArea
            // 
            appearance5.TextHAlignAsString = "Center";
            appearance5.TextVAlignAsString = "Bottom";
            this.lblArea.Appearance = appearance5;
            this.lblArea.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblArea.Location = new System.Drawing.Point(0, 0);
            this.lblArea.Name = "lblArea";
            this.lblArea.Size = new System.Drawing.Size(77, 28);
            this.lblArea.TabIndex = 5;
            this.lblArea.Text = "Tầng 1";
            this.lblArea.Click += new System.EventHandler(this.Label_Click);
            this.lblArea.DoubleClick += new System.EventHandler(this.Label_DoubleClick);
            // 
            // pnlBottom
            // 
            appearance6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.pnlBottom.Appearance = appearance6;
            // 
            // pnlBottom.ClientArea
            // 
            this.pnlBottom.ClientArea.Controls.Add(this.btnPayment);
            this.pnlBottom.ClientArea.Controls.Add(this.ultraPanel1);
            this.pnlBottom.ClientArea.Controls.Add(this.btnReturnFood);
            this.pnlBottom.ClientArea.Controls.Add(this.btnCancel);
            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottom.Location = new System.Drawing.Point(0, 81);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Size = new System.Drawing.Size(204, 27);
            this.pnlBottom.TabIndex = 2;
            this.pnlBottom.Click += new System.EventHandler(this.Label_Click);
            this.pnlBottom.DoubleClick += new System.EventHandler(this.Label_DoubleClick);
            // 
            // ultraPanel1
            // 
            appearance8.BackColor = System.Drawing.Color.DarkGray;
            this.ultraPanel1.Appearance = appearance8;
            this.ultraPanel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.ultraPanel1.Location = new System.Drawing.Point(38, 0);
            this.ultraPanel1.Name = "ultraPanel1";
            this.ultraPanel1.Size = new System.Drawing.Size(2, 27);
            this.ultraPanel1.TabIndex = 3;
            // 
            // pnlTop
            // 
            appearance11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.pnlTop.Appearance = appearance11;
            // 
            // pnlTop.ClientArea
            // 
            this.pnlTop.ClientArea.Controls.Add(this.lblOrderNo);
            this.pnlTop.ClientArea.Controls.Add(this.lblNumberOfPeople);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(204, 27);
            this.pnlTop.TabIndex = 1;
            // 
            // UctOrder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlBody);
            this.Name = "UctOrder";
            this.Padding = new System.Windows.Forms.Padding(7, 5, 7, 5);
            this.Size = new System.Drawing.Size(220, 120);
            this.Load += new System.EventHandler(this.UctOrder_Load);
            this.pnlBody.ClientArea.ResumeLayout(false);
            this.pnlBody.ResumeLayout(false);
            this.pnlContent.ClientArea.ResumeLayout(false);
            this.pnlContent.ResumeLayout(false);
            this.pnlDetail.ClientArea.ResumeLayout(false);
            this.pnlDetail.ResumeLayout(false);
            this.pnlSpace.ResumeLayout(false);
            this.pnlArea.ClientArea.ResumeLayout(false);
            this.pnlArea.ResumeLayout(false);
            this.pnlBottom.ClientArea.ResumeLayout(false);
            this.pnlBottom.ClientArea.PerformLayout();
            this.pnlBottom.ResumeLayout(false);
            this.ultraPanel1.ResumeLayout(false);
            this.pnlTop.ClientArea.ResumeLayout(false);
            this.pnlTop.ClientArea.PerformLayout();
            this.pnlTop.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private Infragistics.Win.UltraWinToolTip.UltraToolTipManager ttmManager_UserControl;
        private Infragistics.Win.Misc.UltraPanel pnlBody;
        private Infragistics.Win.Misc.UltraPanel pnlContent;
        private Infragistics.Win.Misc.UltraPanel pnlDetail;
        private Infragistics.Win.Misc.UltraLabel lblTotalAmount;
        private Infragistics.Win.Misc.UltraPanel pnlSpace;
        private Infragistics.Win.Misc.UltraPanel pnlArea;
        private Infragistics.Win.Misc.UltraPanel pnlBottom;
        private Infragistics.Win.Misc.UltraButton btnPayment;
        private Infragistics.Win.Misc.UltraButton btnReturnFood;
        private Infragistics.Win.Misc.UltraButton btnCancel;
        private Infragistics.Win.Misc.UltraPanel pnlTop;
        private Infragistics.Win.Misc.UltraLabel lblOrderNo;
        private Infragistics.Win.Misc.UltraLabel lblNumberOfPeople;
        private Infragistics.Win.Misc.UltraPanel ultraPanel1;
        private Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel lblTable;
        private Infragistics.Win.Misc.UltraLabel lblArea;
    }
}
