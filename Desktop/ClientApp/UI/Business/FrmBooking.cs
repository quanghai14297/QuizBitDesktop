using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClientApp.UI.Controls;
using Desktop.BL;
using Desktop.Entity;
using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;
using Infragistics.Win.UltraWinTabControl;

namespace ClientApp.UI.Business
{
    public partial class FrmBooking : Form
    {

        private const string Key_All = "All";

        private Dictionary<string, int> Area_All_Status = new Dictionary<string, int>();
        private Dictionary<string, int> Area_All_Status_Empty = new Dictionary<string, int>();

        BLArea _oblArea = new BLArea();
        BLOrder _oblOrder = new BLOrder();

        private DateTime _today;

        /// <summary>
        /// Ngày đang lọc
        /// </summary>
        public DateTime Today
        {
            get { return _today; }
            set
            {
                _today = value;
                Session.Today = _today;
                LoadArea();
                LoadOrder(_today);
                LoadBooking(_today);
                LoadOrderPanel();
            }
        }

        public FrmBooking()
        {
            InitializeComponent();
        }

        private void FrmBooking_Load(object sender, EventArgs e)
        {
            try
            {
                dteDateAreaTableMapping.DateTime = Session.Today;
                dteInvoice.DateTime = Session.Today;
                LoadArea();
                LoadOrder();
                LoadBooking();
                LoadSAInvoice();
                EventHandlerCommon.LoadArea += LoadArea;
            }
            catch (Exception ex)
            {
                MessageBoxCommon.ShowException(ex);
            }
        }

        private void LoadBooking(DateTime today = default(DateTime))
        {
            ShareDictionary.LoadBooking(true, today);
            dsDictionary.Booking.Clear();
            dsDictionary.Booking.Merge(ShareDictionary.DsDictionary.Booking);
        }

        /// <summary>
        /// Load danh sách Order trong hôm nay
        /// </summary>
        private void LoadOrder(DateTime today = default(DateTime))
        {
            ShareDictionary.LoadOrder(true, today);
            dsDictionary.Order.Clear();
            dsDictionary.Order.Merge(ShareDictionary.DsDictionary.Order);
        }

        /// <summary>
        /// Load danh sách hóa đơn trong hôm nay
        /// </summary>
        private void LoadSAInvoice(DateTime today = default(DateTime))
        {
            dsBusiness.SAInvoice.Clear();
            dsBusiness.SAInvoice.Merge(new BLSAInvoice().GetSAInvoice_Today(today));
        }

        /// <summary>
        /// Load danh sách khu vực
        /// </summary>
        private void LoadArea()
        {
            Area_All_Status.Clear();
            Area_All_Status_Empty.Clear();
            Area_All_Status.Add(Key_All, 0);
            Area_All_Status_Empty.Add(Key_All, 0);

            ShareDictionary.LoadArea(true);
            dsDictionary.Area.Clear();
            dsDictionary.Area.Merge(ShareDictionary.DsDictionary.Area);
            tabAreaTableMapping.Tabs.Clear();

            // Bắt đầu sửa giao diện
            tabAreaTableMapping.BeginUpdate();
            tabAreaTableMapping.UseMnemonics = DefaultableBoolean.True;
            // Danh sách Tab
            UltraTabsCollection tabs = tabAreaTableMapping.Tabs;
            if(dsDictionary.Area.Rows.Count == 0)
            {
                lblArea_Detail_Title.Text = String.Empty;
                    lblArea_Detail.Text = String.Empty;
            }
            foreach (DictionaryDataSet.AreaRow iArea in dsDictionary.Area.Rows)
            {
                // Tăng tổng toàn cửa hàng
                Area_All_Status[Key_All] += iArea.NumberOfTable;
                // Tổng số bàn của khu vực
                Area_All_Status.Add(iArea.AreaID.ToString(), iArea.NumberOfTable);
                Area_All_Status_Empty.Add(iArea.AreaID.ToString(), 0);

                // Khởi tạo Tab
                UltraTab ultraTab;
                ultraTab = tabs.Add(iArea.AreaID.ToString(), iArea.AreaName + " (" + iArea.NumberOfTable.ToString() + ")");

                BusinessDataSet dsTable = new BusinessDataSet();
                dsTable.AreaTableMapping.Merge(_oblArea.GetTableMappingByAreaID(iArea.AreaID, dteDateAreaTableMapping.DateTime));
                FlowLayoutPanel flowLayout = new FlowLayoutPanel();
                flowLayout.Padding = new Padding(10);
                flowLayout.Dock = DockStyle.Fill;
                flowLayout.AutoScroll = true;
                if (dsTable.AreaTableMapping.Rows.Count > 0)
                {
                    foreach (BusinessDataSet.AreaTableMappingRow iAreaTable in dsTable.AreaTableMapping.Rows)
                    {
                        EnumTableStatus tableStatus = (EnumTableStatus)iAreaTable.TableStatus;
                        if (tableStatus == EnumTableStatus.Empty)
                        {
                            Area_All_Status_Empty[iArea.AreaID.ToString()] += 1;
                            Area_All_Status_Empty[Key_All] += 1;
                        }
                        UctTableMapping tableMapping = CreateUctTableMapping(iAreaTable);
                        tableMapping.Click += Table_Click;
                        tableMapping.DoubleClick += Table_DoubleClick;
                        tableMapping.LoadAreaStatus += ChangeAreaStatus;
                        flowLayout.Controls.Add(tableMapping);
                    }
                }
                ultraTab.TabPage.Controls.Add(flowLayout);
                ChangeAreaStatus();
            }
            tabAreaTableMapping.EndUpdate();
        }

        private void Table_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                HandlerTableMapping(sender);
            }
            catch (Exception ex)
            {
                MessageBoxCommon.ShowException(ex);
            }
        }

        private void Table_Click(object sender, EventArgs e)
        {
            try
            {
                HandlerTableMapping(sender);
                ChangeAreaStatus(sender, e);
            }
            catch (Exception ex)
            {
                MessageBoxCommon.ShowException(ex);
            }
        }

        /// <summary>
        /// Xử lý sự kiện nhấn vào bàn
        /// </summary>
        /// <param name="sender"></param>
        private void HandlerTableMapping(object sender)
        {
            UctTableMapping uctTableMapping = (UctTableMapping)sender;
            if (uctTableMapping.TableStatus == EnumTableStatus.Booking)
            {
                ShowBooking(uctTableMapping);
            }
            else if (uctTableMapping.TableStatus == EnumTableStatus.Using)
            {
                ShowOrder(uctTableMapping);
            }
            else
            {
                MessageBoxCommon.ShowMessage("Bàn chưa được sử dụng");
            }
        }

        /// <summary>
        /// Chuyển khu vực
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tabAreaTableMapping_SelectedTabChanged(object sender, SelectedTabChangedEventArgs e)
        {
            try
            {
                //LoadTableByAreaID();
                ChangeAreaStatus();
            }
            catch (Exception ex)
            {
                MessageBoxCommon.ShowException(ex);
            }
        }

        /// <summary>
        /// Tính lại tổng tiền
        /// </summary>
        public void ChangeAreaStatus(object sender, EventArgs e)
        {
            int emptyAll = Area_All_Status_Empty.ContainsKey(Key_All) ? Area_All_Status_Empty[Key_All] : 0;
            int All = Area_All_Status.ContainsKey(Key_All) ? Area_All_Status[Key_All] : 0;

            int empty = 0;
            foreach (var item in tabAreaTableMapping.ActiveTab.TabPage.Controls[0].Controls)
            {
                if (item.GetType() == typeof(UctTableMapping))
                {
                    var uct = (UctTableMapping)item;
                    if (uct.TableStatus == EnumTableStatus.Empty)
                        empty++;
                }
            }
            Area_All_Status_Empty[tabAreaTableMapping.ActiveTab.Key] = empty;

            //int emptyDetail = Area_All_Status_Empty.ContainsKey(tabAreaTableMapping.ActiveTab.Key) ? Area_All_Status_Empty[tabAreaTableMapping.ActiveTab.Key] : 0;
            int Detail = Area_All_Status.ContainsKey(tabAreaTableMapping.ActiveTab.Key) ? Area_All_Status[tabAreaTableMapping.ActiveTab.Key] : 0;

            lblArea_All.Text = string.Format(Properties.Resources.CountTableEmpty, emptyAll, All);
            lblArea_Detail.Text = string.Format(Properties.Resources.CountTableEmpty, empty, Detail);
            lblArea_Detail_Title.Text = tabAreaTableMapping.ActiveTab.Text.Replace(" (" + Detail.ToString() + ")", "");
        }

        private void ChangeAreaStatus()
        {
            int emptyAll = Area_All_Status_Empty.ContainsKey(Key_All) ? Area_All_Status_Empty[Key_All] : 0;
            int All = Area_All_Status.ContainsKey(Key_All) ? Area_All_Status[Key_All] : 0;

            int emptyDetail = Area_All_Status_Empty.ContainsKey(tabAreaTableMapping.ActiveTab.Key) ? Area_All_Status_Empty[tabAreaTableMapping.ActiveTab.Key] : 0;
            int Detail = Area_All_Status.ContainsKey(tabAreaTableMapping.ActiveTab.Key) ? Area_All_Status[tabAreaTableMapping.ActiveTab.Key] : 0;

            lblArea_All.Text = string.Format(Properties.Resources.CountTableEmpty, emptyAll, All);
            lblArea_Detail.Text = string.Format(Properties.Resources.CountTableEmpty, emptyDetail, Detail);
            lblArea_Detail_Title.Text = tabAreaTableMapping.ActiveTab.Text.Replace(" (" + Detail.ToString() + ")", "");
        }

        private void btnLoadArea_Click(object sender, EventArgs e)
        {
            try
            {
                LoadTableByAreaID();
                ChangeAreaStatus(sender, e);
            }
            catch (Exception ex)
            {
                MessageBoxCommon.ShowException(ex);
            }
        }

        private void btnLoadOrder_Click(object sender, EventArgs e)
        {
            try
            {
                LoadOrderPanel();
            }
            catch (Exception ex)
            {
                MessageBoxCommon.ShowException(ex);
            }
        }

        private void btnLoadInvoice_Click(object sender, EventArgs e)
        {
            try
            {
                LoadSAInvoice(dteInvoice.DateTime);
            }
            catch (Exception ex)
            {
                MessageBoxCommon.ShowException(ex);
            }
        }

        /// <summary>
        /// Load lại bảng theo khu vực
        /// </summary>
        private void LoadTableByAreaID()
        {
            UltraTab activeTab = tabAreaTableMapping.ActiveTab;
            if (activeTab == null || string.IsNullOrEmpty(activeTab.Key)) return;

            Guid areaID = Guid.Parse(activeTab.Key);

            BusinessDataSet dsTable = new BusinessDataSet();
            dsTable.AreaTableMapping.Merge(_oblArea.GetTableMappingByAreaID(areaID, dteDateAreaTableMapping.DateTime));
            FlowLayoutPanel flowLayout = new FlowLayoutPanel();
            flowLayout.Padding = new Padding(10);
            flowLayout.Dock = DockStyle.Fill;
            flowLayout.AutoScroll = true;
            if (dsTable.AreaTableMapping.Rows.Count > 0)
            {
                // Bỏ số lượng của Khu vực đi để cộng lại
                Area_All_Status_Empty[Key_All] -= Area_All_Status_Empty[areaID.ToString()];
                Area_All_Status_Empty[areaID.ToString()] = 0;

                foreach (BusinessDataSet.AreaTableMappingRow iAreaTable in dsTable.AreaTableMapping.Rows)
                {
                    EnumTableStatus tableStatus = (EnumTableStatus)iAreaTable.TableStatus;
                    if (tableStatus == EnumTableStatus.Empty)
                    {
                        Area_All_Status_Empty[areaID.ToString()] += 1;
                        Area_All_Status_Empty[Key_All] += 1;
                    }
                    else if (tableStatus == EnumTableStatus.Booking)
                    {

                        TimeSpan timeSpan;
                        timeSpan = iAreaTable.BookingDate - DateTime.Now;
                        if (timeSpan.Minutes >= 15)
                        {
                            BLBooking bLBooking = new BLBooking();
                            DictionaryDataSet.BookingRow row=dsDictionary.Booking.FindByBookingID(iAreaTable.BookingID);
                            row.BookingStatus = 2;
                            bLBooking.InsertUpdate(row);
                        }
                        
                    }
                    UctTableMapping tableMapping = CreateUctTableMapping(iAreaTable);
                    tableMapping.Click += Table_Click;
                    tableMapping.DoubleClick += Table_DoubleClick;
                    tableMapping.LoadAreaStatus += ChangeAreaStatus;
                    flowLayout.Controls.Add(tableMapping);
                }
            }
            activeTab.TabPage.Controls.Clear();
            activeTab.TabPage.Controls.Add(flowLayout);
        }

        /// <summary>
        /// Khởi tạo UserControl TableMapping
        /// </summary>
        /// <param name="iAreaTable"></param>
        /// <returns></returns>
        private UctTableMapping CreateUctTableMapping(BusinessDataSet.AreaTableMappingRow iAreaTable)
        {
            EnumTableStatus tableStatus = (EnumTableStatus)iAreaTable.TableStatus;

            UctTableMapping tableMapping = new UctTableMapping(iAreaTable.TableID, iAreaTable.SortOrder, tableStatus);
            tableMapping.Today = Today;
            if (tableStatus == EnumTableStatus.Booking)
            {
                tableMapping.BookingID = iAreaTable.BookingID;
                tableMapping.FromTime = iAreaTable.FromTime;
            }
            else if (tableStatus == EnumTableStatus.Using)
            {
                tableMapping.OrderID = iAreaTable.OrderID;
                tableMapping.OrderDate = iAreaTable.OrderDate;
            }
            return tableMapping;
        }

        /// <summary>
        /// Hiện Order
        /// </summary>
        private void ShowOrder(UctTableMapping control)
        {
            LoadOrder(_today);
            using (FrmOrderDetail frm = new FrmOrderDetail())
            {
                frm.FormActionMode = ActionMode.Edit;
                frm.DsDictionary = dsDictionary;
                frm.BsDetail.DataSource = frm.DsDictionary;
                frm.OrderID = control.OrderID;
                frm.TableID = control.TableID;
                frm.BookingID = control.BookingID;
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    dsDictionary.AcceptChanges();
                    if (frm.OrderStatus == EnumOrderStatus.Cancel || frm.OrderStatus == EnumOrderStatus.Done)
                    {
                        control.Empty();
                    }
                    else if (control.TableID != frm.TableID)
                    {
                        var tabControl = control.Parent.Parent;
                        foreach (var fLayout in tabControl.Controls)
                        {
                            if (fLayout.GetType() == typeof(FlowLayoutPanel))
                            {
                                foreach (var uctControl in ((FlowLayoutPanel)fLayout).Controls)
                                {
                                    if (uctControl.GetType() == typeof(UctTableMapping))
                                    {
                                        var uct = (UctTableMapping)uctControl;
                                        if (uct.TableID == frm.TableID)
                                        {
                                            uct.OrderID = frm.OrderID;
                                            uct.TableStatus = EnumTableStatus.Using;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    LoadOrderPanel();
                }
            }
        }

        /// <summary>
        /// Hiện Order
        /// </summary>
        private void ShowOrder(UctOrder control)
        {
            LoadOrder(_today);
            using (FrmOrderDetail frm = new FrmOrderDetail())
            {
                frm.FormActionMode = ActionMode.Edit;
                frm.DsDictionary = dsDictionary;
                frm.BsDetail.DataSource = frm.DsDictionary;
                frm.OrderID = control.OrderID;
                frm.TableID = control.OrderRow.TableID;
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    dsDictionary.AcceptChanges();
                    if (frm.OrderStatus == EnumOrderStatus.Cancel || frm.OrderStatus == EnumOrderStatus.Done)
                    {
                        fpnlOrdering.Controls.Remove(control);
                        //control.Empty();
                    }
                    else if (control.OrderRow.TableID != frm.TableID)
                    {
                        var tabControl = control.Parent.Parent;
                        foreach (var fLayout in tabControl.Controls)
                        {
                            if (fLayout.GetType() == typeof(FlowLayoutPanel))
                            {
                                foreach (var uctControl in ((FlowLayoutPanel)fLayout).Controls)
                                {
                                    if (uctControl.GetType() == typeof(UctTableMapping))
                                    {
                                        var uct = (UctTableMapping)uctControl;
                                        if (uct.TableID == frm.TableID)
                                        {
                                            uct.OrderID = frm.OrderID;
                                            uct.TableStatus = EnumTableStatus.Using;
                                        }
                                    }
                                }
                            }
                        }
                        ShareDictionary.LoadOrderView(true);
                        control.OrderRow = ShareDictionary.DsBusiness.OrderView.FindByOrderID(control.OrderID);
                    }
                    //LoadOrderPanel();
                }
            }
        }

        /// <summary>
        /// Hiện Đặt bàn
        /// </summary>
        private void ShowBooking(UctTableMapping control)
        {
            LoadBooking(_today);
            using (FrmBookingDetail frm = new FrmBookingDetail())
            {
                frm.FormActionMode = ActionMode.Edit;
                frm.DsDictionary = dsDictionary;
                frm.BsDetail.DataSource = frm.DsDictionary;
                frm.BookingID = control.BookingID;
                frm.TableID = control.TableID;
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    dsDictionary.AcceptChanges();
                    if (frm.BookingStatus == EnumBookingStatus.Receiver || frm.BookingStatus == EnumBookingStatus.Cancel)
                    {
                        control.Empty();
                    }
                    else if (control.TableID != frm.TableID)
                    {
                        var tabControl = control.Parent.Parent;
                        foreach (var fLayout in tabControl.Controls)
                        {
                            if (fLayout.GetType() == typeof(FlowLayoutPanel))
                            {
                                foreach (var uctControl in ((FlowLayoutPanel)fLayout).Controls)
                                {
                                    if (uctControl.GetType() == typeof(UctTableMapping))
                                    {
                                        var uct = (UctTableMapping)uctControl;
                                        if (uct.TableID == frm.TableID)
                                        {
                                            uct.BookingID = frm.BookingID;
                                            uct.TableStatus = EnumTableStatus.Booking;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private bool IsChangeDateAreaTableMapping = false;

        private bool IsChangeDateOrder = false;

        private bool IsChangeDateInvoice = false;

        private void dteDateAreaTableMapping_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                IsChangeDateAreaTableMapping = true;
                if (!IsChangeDateOrder)
                {
                    Today = dteDateAreaTableMapping.DateTime;
                    dteDateOrder.Value = Today;
                    dteInvoice.Value = Today;
                }
                IsChangeDateAreaTableMapping = false;
            }
            catch (Exception ex)
            {
                MessageBoxCommon.ShowException(ex);
            }
        }

        private void dteDateOrder_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                IsChangeDateOrder = true;
                if (!IsChangeDateAreaTableMapping)
                {
                    Today = dteDateAreaTableMapping.DateTime;
                    dteDateAreaTableMapping.Value = Today;
                    dteInvoice.Value = Today;
                }
                IsChangeDateOrder = false;
            }
            catch (Exception ex)
            {
                MessageBoxCommon.ShowException(ex);
            }
        }

        /// <summary>
        /// Load dữ liệu Tab Order
        /// </summary>
        private void LoadOrderPanel()
        {
            ShareDictionary.LoadOrderView(true, dteDateOrder.DateTime);
            fpnlOrdering.Controls.Clear();
            fpnlWaitPay.Controls.Clear();
            fpnlCancel.Controls.Clear();
            if (ShareDictionary.DsBusiness.OrderView.Count > 0)
            {
                foreach (var item in ShareDictionary.DsBusiness.OrderView)
                {
                    var control = new UctOrder();
                    control.OrderRow = item;
                    control.Click += OrderView_Click;
                    if (item.OrderStatus == (int)EnumOrderStatus.Ordering)
                    {
                        fpnlOrdering.Controls.Add(control);
                    }
                    else if (item.OrderStatus == (int)EnumOrderStatus.WaitPay)
                    {
                        fpnlWaitPay.Controls.Add(control);
                    }
                    else
                    {
                        fpnlCancel.Controls.Add(control);
                    }
                }
            }
        }

        /// <summary>
        /// Nhấn vào Order
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OrderView_Click(object sender, EventArgs e)
        {
            try
            {
                UctOrder control = (UctOrder)sender;
                if (control.OrderStatus == EnumOrderStatus.Done || control.OrderStatus == EnumOrderStatus.Cancel)
                {
                    control.Visible = false;
                    control.Parent.Controls.Remove(control);
                }
                else
                {
                    ShowOrder((UctOrder)sender);
                }
            }
            catch (Exception ex)
            {
                MessageBoxCommon.ShowException(ex);
            }
        }

        private void grdList_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                var mousePoint = new Point(e.X, e.Y);
                var element = grdList.DisplayLayout.UIElement.ElementFromPoint(mousePoint);
                UltraGridRow row = null;
                row = (UltraGridRow)element.GetContext(typeof(UltraGridRow));
                if (row != null)
                    if (row.Index >= 0)
                    {
                        using (var frm = new FrmInvoice())
                        {
                            frm.SAInvoiceID = Guid.Parse(row.Cells[ColumnName.RefID].Text);
                            frm.ShowDialog();
                        }
                    }
            }
            catch (Exception ex)
            {
                MessageBoxCommon.ShowException(ex);
            }
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            try
            {
                LoadTableByAreaID();
                ChangeAreaStatus(sender, e);
                LoadOrderPanel();
                LoadSAInvoice(dteInvoice.DateTime);
            }
            catch (Exception ex)
            {
                MessageBoxCommon.ShowException(ex);
            }
        }
    }
}
