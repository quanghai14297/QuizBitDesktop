namespace ClientApp.UI.Dictionary
{
    partial class FrmArea
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
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand1 = new Infragistics.Win.UltraWinGrid.UltraGridBand("Area", -1);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn1 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("AreaID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn2 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("AreaName");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn3 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("BranchID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn5 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Description");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn9 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("NumberOfTable");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn10 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Background");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn12 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Inactive");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn13 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("CreatedDate");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn14 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("CreatedBy");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn15 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ModifiedDate");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn16 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ModifiedBy");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn17 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("OldIDs");
            Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance7 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance8 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance9 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance10 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance11 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance12 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinScrollBar.ScrollBarLook scrollBarLook1 = new Infragistics.Win.UltraWinScrollBar.ScrollBarLook();
            Infragistics.Win.Appearance appearance13 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance14 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinListView.UltraListViewSubItemColumn ultraListViewSubItemColumn1 = new Infragistics.Win.UltraWinListView.UltraListViewSubItemColumn("SubItemColumn 0");
            this.lvTest = new Infragistics.Win.UltraWinListView.UltraListView();
            ((System.ComponentModel.ISupportInitialize)(this.bsList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbrFunction)).BeginInit();
            this.frmBaseList_Fill_Panel.ClientArea.SuspendLayout();
            this.frmBaseList_Fill_Panel.SuspendLayout();
            this.pnlDetail.ClientArea.SuspendLayout();
            this.pnlDetail.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tabInfo)).BeginInit();
            this.tabInfo.SuspendLayout();
            this.UltraTabPageControl1.SuspendLayout();
            this.pnlTitle.ClientArea.SuspendLayout();
            this.pnlTitle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsDictionary)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lvTest)).BeginInit();
            this.SuspendLayout();
            // 
            // bsList
            // 
            this.bsList.DataMember = "Area";
            this.bsList.DataSource = this.dsDictionary;
            // 
            // tbrFunction
            // 
            this.tbrFunction.MenuSettings.ForceSerialization = true;
            this.tbrFunction.ToolbarSettings.ForceSerialization = true;
            // 
            // frmBaseList_Fill_Panel
            // 
            this.frmBaseList_Fill_Panel.Location = new System.Drawing.Point(0, 25);
            this.frmBaseList_Fill_Panel.Size = new System.Drawing.Size(800, 425);
            // 
            // splContent
            // 
            this.splContent.Location = new System.Drawing.Point(0, 115);
            // 
            // pnlDetail
            // 
            this.pnlDetail.Location = new System.Drawing.Point(0, 125);
            this.pnlDetail.Size = new System.Drawing.Size(800, 300);
            // 
            // tabInfo
            // 
            this.tabInfo.Size = new System.Drawing.Size(800, 300);
            this.tabInfo.TabPageMargins.ForceSerialization = true;
            // 
            // UltraTabPageControl1
            // 
            this.UltraTabPageControl1.Controls.Add(this.lvTest);
            this.UltraTabPageControl1.Size = new System.Drawing.Size(798, 279);
            // 
            // pnlTitle
            // 
            // 
            // pnlSearch
            // 
            // 
            // lblTitle
            // 
            this.lblTitle.Text = "Khu vực";
            // 
            // grdList
            // 
            this.grdList.DataSource = this.bsList;
            appearance1.BackColor = System.Drawing.SystemColors.Window;
            appearance1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(162)))), ((int)(((byte)(206)))));
            appearance1.TextVAlignAsString = "Middle";
            this.grdList.DisplayLayout.Appearance = appearance1;
            this.grdList.DisplayLayout.AutoFitStyle = Infragistics.Win.UltraWinGrid.AutoFitStyle.ExtendLastColumn;
            ultraGridColumn1.Header.VisiblePosition = 0;
            ultraGridColumn1.Hidden = true;
            ultraGridColumn2.Header.Caption = "Tên khu vực";
            ultraGridColumn2.Header.VisiblePosition = 1;
            ultraGridColumn2.Width = 243;
            ultraGridColumn3.Header.VisiblePosition = 2;
            ultraGridColumn3.Hidden = true;
            ultraGridColumn5.Header.Caption = "Mô tả";
            ultraGridColumn5.Header.VisiblePosition = 4;
            ultraGridColumn5.Width = 332;
            ultraGridColumn9.Header.Caption = "Số lượng bàn";
            ultraGridColumn9.Header.VisiblePosition = 3;
            ultraGridColumn9.Width = 125;
            ultraGridColumn10.Header.VisiblePosition = 5;
            ultraGridColumn10.Hidden = true;
            ultraGridColumn12.Header.Caption = "Ngừng theo dõi";
            ultraGridColumn12.Header.VisiblePosition = 6;
            ultraGridColumn12.Hidden = true;
            ultraGridColumn13.Header.VisiblePosition = 8;
            ultraGridColumn13.Hidden = true;
            ultraGridColumn14.Header.VisiblePosition = 9;
            ultraGridColumn14.Hidden = true;
            ultraGridColumn15.Header.VisiblePosition = 10;
            ultraGridColumn15.Hidden = true;
            ultraGridColumn16.Header.VisiblePosition = 11;
            ultraGridColumn16.Hidden = true;
            ultraGridColumn17.Header.VisiblePosition = 7;
            ultraGridColumn17.Hidden = true;
            ultraGridBand1.Columns.AddRange(new object[] {
            ultraGridColumn1,
            ultraGridColumn2,
            ultraGridColumn3,
            ultraGridColumn5,
            ultraGridColumn9,
            ultraGridColumn10,
            ultraGridColumn12,
            ultraGridColumn13,
            ultraGridColumn14,
            ultraGridColumn15,
            ultraGridColumn16,
            ultraGridColumn17});
            this.grdList.DisplayLayout.BandsSerializer.Add(ultraGridBand1);
            this.grdList.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            this.grdList.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            this.grdList.DisplayLayout.DefaultSelectedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(244)))), ((int)(((byte)(202)))));
            this.grdList.DisplayLayout.DefaultSelectedForeColor = System.Drawing.Color.Black;
            this.grdList.DisplayLayout.EmptyRowSettings.ShowEmptyRows = true;
            this.grdList.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            this.grdList.DisplayLayout.MaxColScrollRegions = 1;
            this.grdList.DisplayLayout.MaxRowScrollRegions = 1;
            this.grdList.DisplayLayout.NewBandLoadStyle = Infragistics.Win.UltraWinGrid.NewBandLoadStyle.Hide;
            this.grdList.DisplayLayout.NewColumnLoadStyle = Infragistics.Win.UltraWinGrid.NewColumnLoadStyle.Hide;
            appearance2.BackColor = System.Drawing.SystemColors.Window;
            appearance2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.grdList.DisplayLayout.Override.ActiveCellAppearance = appearance2;
            appearance3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(244)))), ((int)(((byte)(202)))));
            appearance3.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(244)))), ((int)(((byte)(202)))));
            appearance3.BorderColor = System.Drawing.Color.Black;
            appearance3.ForeColor = System.Drawing.Color.Black;
            this.grdList.DisplayLayout.Override.ActiveRowAppearance = appearance3;
            this.grdList.DisplayLayout.Override.AllowRowLayoutCellSizing = Infragistics.Win.UltraWinGrid.RowLayoutSizing.Horizontal;
            this.grdList.DisplayLayout.Override.AllowRowLayoutLabelSizing = Infragistics.Win.UltraWinGrid.RowLayoutSizing.Horizontal;
            this.grdList.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
            this.grdList.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
            appearance4.BackColor = System.Drawing.SystemColors.Window;
            this.grdList.DisplayLayout.Override.CardAreaAppearance = appearance4;
            appearance5.BorderColor = System.Drawing.Color.Silver;
            appearance5.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
            appearance5.TextVAlignAsString = "Middle";
            this.grdList.DisplayLayout.Override.CellAppearance = appearance5;
            this.grdList.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect;
            this.grdList.DisplayLayout.Override.CellPadding = 0;
            appearance6.TextVAlignAsString = "Middle";
            this.grdList.DisplayLayout.Override.FilterCellAppearance = appearance6;
            this.grdList.DisplayLayout.Override.FilterOperatorDefaultValue = Infragistics.Win.UltraWinGrid.FilterOperatorDefaultValue.Contains;
            appearance7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(225)))));
            appearance7.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(225)))));
            this.grdList.DisplayLayout.Override.FilterRowAppearance = appearance7;
            this.grdList.DisplayLayout.Override.FilterUIType = Infragistics.Win.UltraWinGrid.FilterUIType.FilterRow;
            appearance8.BackColor = System.Drawing.SystemColors.Control;
            appearance8.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance8.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
            appearance8.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance8.BorderColor = System.Drawing.SystemColors.Window;
            this.grdList.DisplayLayout.Override.GroupByRowAppearance = appearance8;
            appearance9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(199)))), ((int)(((byte)(231)))));
            appearance9.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(199)))), ((int)(((byte)(231)))));
            appearance9.ForeColor = System.Drawing.Color.Black;
            appearance9.TextHAlignAsString = "Center";
            this.grdList.DisplayLayout.Override.HeaderAppearance = appearance9;
            this.grdList.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            this.grdList.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
            appearance10.BackColor = System.Drawing.SystemColors.Window;
            appearance10.BorderColor = System.Drawing.Color.Silver;
            this.grdList.DisplayLayout.Override.RowAppearance = appearance10;
            this.grdList.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            appearance11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(244)))), ((int)(((byte)(202)))));
            appearance11.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(244)))), ((int)(((byte)(202)))));
            this.grdList.DisplayLayout.Override.SelectedRowAppearance = appearance11;
            this.grdList.DisplayLayout.Override.SummaryDisplayArea = Infragistics.Win.UltraWinGrid.SummaryDisplayAreas.BottomFixed;
            this.grdList.DisplayLayout.Override.SummaryFooterCaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            appearance12.BackColor = System.Drawing.SystemColors.ControlLight;
            this.grdList.DisplayLayout.Override.TemplateAddRowAppearance = appearance12;
            appearance13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(199)))), ((int)(((byte)(231)))));
            appearance13.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(199)))), ((int)(((byte)(231)))));
            scrollBarLook1.Appearance = appearance13;
            appearance14.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(230)))), ((int)(((byte)(159)))));
            appearance14.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(140)))), ((int)(((byte)(60)))));
            scrollBarLook1.TrackAppearance = appearance14;
            this.grdList.DisplayLayout.ScrollBarLook = scrollBarLook1;
            this.grdList.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.grdList.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.grdList.DisplayLayout.ShowDeleteRowsPrompt = false;
            this.grdList.DisplayLayout.UseFixedHeaders = true;
            this.grdList.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand;
            this.grdList.Size = new System.Drawing.Size(800, 88);
            // 
            // lvTest
            // 
            this.lvTest.BorderStyle = Infragistics.Win.UIElementBorderStyle.None;
            this.lvTest.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvTest.ItemSettings.DefaultImage = global::ClientApp.Properties.Resources.FreeTableS6;
            this.lvTest.ItemSettings.SelectionType = Infragistics.Win.UltraWinListView.SelectionType.None;
            this.lvTest.Location = new System.Drawing.Point(0, 0);
            this.lvTest.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.lvTest.Name = "lvTest";
            this.lvTest.ScrollBarLook.ViewStyle = Infragistics.Win.UltraWinScrollBar.ScrollBarViewStyle.Office2013;
            this.lvTest.Size = new System.Drawing.Size(798, 279);
            ultraListViewSubItemColumn1.Key = "SubItemColumn 0";
            this.lvTest.SubItemColumns.AddRange(new Infragistics.Win.UltraWinListView.UltraListViewSubItemColumn[] {
            ultraListViewSubItemColumn1});
            this.lvTest.TabIndex = 7;
            this.lvTest.TabStop = false;
            this.lvTest.Text = "Danh sách bàn";
            this.lvTest.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
            this.lvTest.UseOsThemes = Infragistics.Win.DefaultableBoolean.False;
            this.lvTest.View = Infragistics.Win.UltraWinListView.UltraListViewStyle.Tiles;
            this.lvTest.ViewSettingsTiles.ImageSize = new System.Drawing.Size(40, 40);
            this.lvTest.ViewSettingsTiles.ItemSize = new System.Drawing.Size(150, 50);
            this.lvTest.ViewSettingsTiles.Spacing = new System.Drawing.Size(10, 8);
            this.lvTest.ViewSettingsTiles.SubItemsVisibleByDefault = true;
            // 
            // FrmArea
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Name = "FrmArea";
            this.Text = "FrmArea";
            ((System.ComponentModel.ISupportInitialize)(this.bsList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbrFunction)).EndInit();
            this.frmBaseList_Fill_Panel.ClientArea.ResumeLayout(false);
            this.frmBaseList_Fill_Panel.ResumeLayout(false);
            this.pnlDetail.ClientArea.ResumeLayout(false);
            this.pnlDetail.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tabInfo)).EndInit();
            this.tabInfo.ResumeLayout(false);
            this.UltraTabPageControl1.ResumeLayout(false);
            this.pnlTitle.ClientArea.ResumeLayout(false);
            this.pnlTitle.ResumeLayout(false);

            ((System.ComponentModel.ISupportInitialize)(this.grdList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsDictionary)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lvTest)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Infragistics.Win.UltraWinListView.UltraListView lvTest;
    }
}