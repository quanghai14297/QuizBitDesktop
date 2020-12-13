namespace ClientApp.UI.Controls
{
    partial class UctTableMapping
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
            Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool1 = new Infragistics.Win.UltraWinToolbars.ButtonTool("btnAddOrder");
            Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool2 = new Infragistics.Win.UltraWinToolbars.ButtonTool("btnAddBooking");
            Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool1 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("popMenu");
            Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool3 = new Infragistics.Win.UltraWinToolbars.ButtonTool("btnAddBooking");
            Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool4 = new Infragistics.Win.UltraWinToolbars.ButtonTool("btnAddOrder");
            Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool7 = new Infragistics.Win.UltraWinToolbars.ButtonTool("btnReceiveTable");
            Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool9 = new Infragistics.Win.UltraWinToolbars.ButtonTool("btnPay");
            Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool5 = new Infragistics.Win.UltraWinToolbars.ButtonTool("btnReceiveTable");
            Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool6 = new Infragistics.Win.UltraWinToolbars.ButtonTool("btnPay");
            this.pnlBackground = new Infragistics.Win.Misc.UltraPanel();
            this.lblNumberOfTable = new Infragistics.Win.Misc.UltraLabel();
            this._UctTableMapping_Toolbars_Dock_Area_Left = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
            this.tbrManager = new Infragistics.Win.UltraWinToolbars.UltraToolbarsManager(this.components);
            this._UctTableMapping_Toolbars_Dock_Area_Right = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
            this._UctTableMapping_Toolbars_Dock_Area_Top = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
            this._UctTableMapping_Toolbars_Dock_Area_Bottom = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
            this.bgwRefreshData = new System.ComponentModel.BackgroundWorker();
            this.timerRefreshData = new System.Windows.Forms.Timer(this.components);
            this.pnlBackground.ClientArea.SuspendLayout();
            this.pnlBackground.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbrManager)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlBackground
            // 
            appearance1.BackColor = System.Drawing.Color.Transparent;
            appearance1.BackColor2 = System.Drawing.Color.Transparent;
            appearance1.ImageBackground = global::ClientApp.Properties.Resources.table_empty;
            this.pnlBackground.Appearance = appearance1;
            this.pnlBackground.BorderStyle = Infragistics.Win.UIElementBorderStyle.None;
            // 
            // pnlBackground.ClientArea
            // 
            this.pnlBackground.ClientArea.Controls.Add(this.lblNumberOfTable);
            this.tbrManager.SetContextMenuUltra(this.pnlBackground, "popMenu");
            this.pnlBackground.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBackground.Location = new System.Drawing.Point(7, 5);
            this.pnlBackground.Name = "pnlBackground";
            this.pnlBackground.Size = new System.Drawing.Size(76, 60);
            this.pnlBackground.TabIndex = 0;
            // 
            // lblNumberOfTable
            // 
            appearance2.BackColor = System.Drawing.Color.Transparent;
            appearance2.BackColor2 = System.Drawing.Color.Transparent;
            appearance2.ForeColor = System.Drawing.Color.White;
            appearance2.TextHAlignAsString = "Center";
            appearance2.TextVAlignAsString = "Middle";
            this.lblNumberOfTable.Appearance = appearance2;
            this.tbrManager.SetContextMenuUltra(this.lblNumberOfTable, "popMenu");
            this.lblNumberOfTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblNumberOfTable.Location = new System.Drawing.Point(0, 0);
            this.lblNumberOfTable.Name = "lblNumberOfTable";
            this.lblNumberOfTable.Size = new System.Drawing.Size(76, 60);
            this.lblNumberOfTable.TabIndex = 0;
            this.lblNumberOfTable.Text = "1";
            this.lblNumberOfTable.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
            this.lblNumberOfTable.UseOsThemes = Infragistics.Win.DefaultableBoolean.False;
            this.lblNumberOfTable.Click += new System.EventHandler(this.lblNumberOfTable_Click);
            this.lblNumberOfTable.DoubleClick += new System.EventHandler(this.lblNumberOfTable_DoubleClick);
            // 
            // _UctTableMapping_Toolbars_Dock_Area_Left
            // 
            this._UctTableMapping_Toolbars_Dock_Area_Left.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
            this._UctTableMapping_Toolbars_Dock_Area_Left.BackColor = System.Drawing.Color.Transparent;
            this._UctTableMapping_Toolbars_Dock_Area_Left.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Left;
            this._UctTableMapping_Toolbars_Dock_Area_Left.ForeColor = System.Drawing.SystemColors.ControlText;
            this._UctTableMapping_Toolbars_Dock_Area_Left.Location = new System.Drawing.Point(7, 5);
            this._UctTableMapping_Toolbars_Dock_Area_Left.Name = "_UctTableMapping_Toolbars_Dock_Area_Left";
            this._UctTableMapping_Toolbars_Dock_Area_Left.Size = new System.Drawing.Size(0, 60);
            this._UctTableMapping_Toolbars_Dock_Area_Left.ToolbarsManager = this.tbrManager;
            // 
            // tbrManager
            // 
            this.tbrManager.DesignerFlags = 1;
            this.tbrManager.DockWithinContainer = this;
            this.tbrManager.ShowFullMenusDelay = 500;
            buttonTool1.SharedPropsInternal.Caption = "Thêm Order";
            buttonTool2.SharedPropsInternal.Caption = "Thêm đặt bàn";
            popupMenuTool1.SharedPropsInternal.Caption = "Chức năng";
            popupMenuTool1.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[] {
            buttonTool3,
            buttonTool4,
            buttonTool7,
            buttonTool9});
            buttonTool5.SharedPropsInternal.Caption = "Nhận bàn";
            buttonTool6.SharedPropsInternal.Caption = "Thanh toán";
            this.tbrManager.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[] {
            buttonTool1,
            buttonTool2,
            popupMenuTool1,
            buttonTool5,
            buttonTool6});
            this.tbrManager.ToolClick += new Infragistics.Win.UltraWinToolbars.ToolClickEventHandler(this.tbrManager_ToolClick);
            // 
            // _UctTableMapping_Toolbars_Dock_Area_Right
            // 
            this._UctTableMapping_Toolbars_Dock_Area_Right.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
            this._UctTableMapping_Toolbars_Dock_Area_Right.BackColor = System.Drawing.Color.Transparent;
            this._UctTableMapping_Toolbars_Dock_Area_Right.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Right;
            this._UctTableMapping_Toolbars_Dock_Area_Right.ForeColor = System.Drawing.SystemColors.ControlText;
            this._UctTableMapping_Toolbars_Dock_Area_Right.Location = new System.Drawing.Point(83, 5);
            this._UctTableMapping_Toolbars_Dock_Area_Right.Name = "_UctTableMapping_Toolbars_Dock_Area_Right";
            this._UctTableMapping_Toolbars_Dock_Area_Right.Size = new System.Drawing.Size(0, 60);
            this._UctTableMapping_Toolbars_Dock_Area_Right.ToolbarsManager = this.tbrManager;
            // 
            // _UctTableMapping_Toolbars_Dock_Area_Top
            // 
            this._UctTableMapping_Toolbars_Dock_Area_Top.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
            this._UctTableMapping_Toolbars_Dock_Area_Top.BackColor = System.Drawing.Color.Transparent;
            this._UctTableMapping_Toolbars_Dock_Area_Top.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Top;
            this._UctTableMapping_Toolbars_Dock_Area_Top.ForeColor = System.Drawing.SystemColors.ControlText;
            this._UctTableMapping_Toolbars_Dock_Area_Top.Location = new System.Drawing.Point(7, 5);
            this._UctTableMapping_Toolbars_Dock_Area_Top.Name = "_UctTableMapping_Toolbars_Dock_Area_Top";
            this._UctTableMapping_Toolbars_Dock_Area_Top.Size = new System.Drawing.Size(76, 0);
            this._UctTableMapping_Toolbars_Dock_Area_Top.ToolbarsManager = this.tbrManager;
            // 
            // _UctTableMapping_Toolbars_Dock_Area_Bottom
            // 
            this._UctTableMapping_Toolbars_Dock_Area_Bottom.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
            this._UctTableMapping_Toolbars_Dock_Area_Bottom.BackColor = System.Drawing.Color.Transparent;
            this._UctTableMapping_Toolbars_Dock_Area_Bottom.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Bottom;
            this._UctTableMapping_Toolbars_Dock_Area_Bottom.ForeColor = System.Drawing.SystemColors.ControlText;
            this._UctTableMapping_Toolbars_Dock_Area_Bottom.Location = new System.Drawing.Point(7, 65);
            this._UctTableMapping_Toolbars_Dock_Area_Bottom.Name = "_UctTableMapping_Toolbars_Dock_Area_Bottom";
            this._UctTableMapping_Toolbars_Dock_Area_Bottom.Size = new System.Drawing.Size(76, 0);
            this._UctTableMapping_Toolbars_Dock_Area_Bottom.ToolbarsManager = this.tbrManager;
            // 
            // bgwRefreshData
            // 
            this.bgwRefreshData.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwRefreshData_DoWork);
            this.bgwRefreshData.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgwRefreshData_RunWorkerCompleted);
            // 
            // timerRefreshData
            // 
            this.timerRefreshData.Interval = 1000;
            this.timerRefreshData.Tick += new System.EventHandler(this.timerRefreshData_Tick);
            // 
            // UctTableMapping
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.tbrManager.SetContextMenuUltra(this, "popMenu");
            this.Controls.Add(this.pnlBackground);
            this.Controls.Add(this._UctTableMapping_Toolbars_Dock_Area_Left);
            this.Controls.Add(this._UctTableMapping_Toolbars_Dock_Area_Right);
            this.Controls.Add(this._UctTableMapping_Toolbars_Dock_Area_Bottom);
            this.Controls.Add(this._UctTableMapping_Toolbars_Dock_Area_Top);
            this.Name = "UctTableMapping";
            this.Padding = new System.Windows.Forms.Padding(7, 5, 7, 5);
            this.Size = new System.Drawing.Size(90, 70);
            this.pnlBackground.ClientArea.ResumeLayout(false);
            this.pnlBackground.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tbrManager)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Infragistics.Win.Misc.UltraPanel pnlBackground;
        private Infragistics.Win.Misc.UltraLabel lblNumberOfTable;
        private Infragistics.Win.UltraWinToolbars.UltraToolbarsManager tbrManager;
        private Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea _UctTableMapping_Toolbars_Dock_Area_Left;
        private Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea _UctTableMapping_Toolbars_Dock_Area_Right;
        private Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea _UctTableMapping_Toolbars_Dock_Area_Bottom;
        private Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea _UctTableMapping_Toolbars_Dock_Area_Top;
        private System.ComponentModel.BackgroundWorker bgwRefreshData;
        private System.Windows.Forms.Timer timerRefreshData;
    }
}
