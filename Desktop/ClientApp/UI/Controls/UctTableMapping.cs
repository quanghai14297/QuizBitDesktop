using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClientApp.UI.Business;
using Desktop.Entity;
using Desktop.BL;

namespace ClientApp.UI.Controls
{
    public partial class UctTableMapping : UserControl
    {

        #region Declaration

        const string btnAddOrder = "btnAddOrder";
        const string btnAddBooking = "btnAddBooking";
        const string btnReceiveTable = "btnReceiveTable";
        const string btnPay = "btnPay";

        #endregion

        #region Property

        public Guid TableID;

        private int _numberOfTable = 0;

        /// <summary>
        /// Số thứ tự bàn
        /// </summary>
        public int NumberOfTable
        {
            get { return _numberOfTable; }
            set
            {
                _numberOfTable = value;
                lblNumberOfTable.Text = _numberOfTable.ToString();
            }
        }

        private EnumTableStatus _tableStatus = EnumTableStatus.Empty;

        /// <summary>
        /// Trạng thái của bàn
        /// </summary>
        public EnumTableStatus TableStatus
        {
            get { return _tableStatus; }
            set
            {
                _tableStatus = value;
                switch (_tableStatus)
                {
                    case EnumTableStatus.Empty:
                        pnlBackground.Appearance.ImageBackground = Properties.Resources.table_empty;
                        tbrManager.Tools[btnAddBooking].SharedProps.Visible = true;
                        tbrManager.Tools[btnAddOrder].SharedProps.Visible = true;
                        tbrManager.Tools[btnReceiveTable].SharedProps.Visible = false;
                        tbrManager.Tools[btnPay].SharedProps.Visible = false;
                        break;
                    case EnumTableStatus.Using:
                        pnlBackground.Appearance.ImageBackground = Properties.Resources.table_using;
                        tbrManager.Tools[btnAddBooking].SharedProps.Visible = false;
                        tbrManager.Tools[btnAddOrder].SharedProps.Visible = false;
                        tbrManager.Tools[btnReceiveTable].SharedProps.Visible = false;
                        tbrManager.Tools[btnPay].SharedProps.Visible = true;
                        break;
                    case EnumTableStatus.Booking:
                        pnlBackground.Appearance.ImageBackground = Properties.Resources.table_booking;
                        tbrManager.Tools[btnAddBooking].SharedProps.Visible = false;
                        tbrManager.Tools[btnAddOrder].SharedProps.Visible = false;
                        tbrManager.Tools[btnReceiveTable].SharedProps.Visible = true;
                        tbrManager.Tools[btnPay].SharedProps.Visible = false;
                        break;
                    default:
                        pnlBackground.Appearance.ImageBackground = Properties.Resources.table_empty;
                        tbrManager.Tools[btnAddBooking].SharedProps.Visible = true;
                        tbrManager.Tools[btnAddOrder].SharedProps.Visible = true;
                        tbrManager.Tools[btnReceiveTable].SharedProps.Visible = false;
                        tbrManager.Tools[btnPay].SharedProps.Visible = false;
                        break;
                }
            }
        }

        private DateTime _today;

        /// <summary>
        /// Thời gian để load dữ liệu
        /// </summary>
        public DateTime Today
        {
            get { return _today; }
            set { _today = value; }
        }


        private Guid _bookingID = Guid.Empty;

        /// <summary>
        /// ID đặt chỗ
        /// </summary>
        public Guid BookingID
        {
            get { return _bookingID; }
            set { _bookingID = value; }
        }

        private DateTime _fromTime;

        /// <summary>
        /// Thời gian dùng bữa khi đặt chỗ
        /// </summary>
        public DateTime FromTime
        {
            get { return _fromTime; }
            set { _fromTime = value; }
        }

        private Guid _orderID = Guid.Empty;

        /// <summary>
        /// ID Order
        /// </summary>
        public Guid OrderID
        {
            get { return _orderID; }
            set { _orderID = value; }
        }

        private DateTime _orderDate;

        /// <summary>
        /// Thời gian bắt đầu Order
        /// </summary>
        public DateTime OrderDate
        {
            get { return _orderDate; }
            set { _orderDate = value; }
        }

        #endregion

        public UctTableMapping()
        {
            InitializeComponent();
            //timerRefreshData.Enabled = true;
        }

        public UctTableMapping(Guid tableID, int numberOfTable = 0, EnumTableStatus tableStatus = EnumTableStatus.Empty)
        {
            InitializeComponent();
            TableID = tableID;
            NumberOfTable = numberOfTable;
            TableStatus = tableStatus;
            //timerRefreshData.Enabled = true;
        }

        public UctTableMapping(Guid tableID, int numberOfTable = 0, EnumTableStatus tableStatus = EnumTableStatus.Empty, Guid bookingID = default(Guid), DateTime fromTime = default(DateTime), Guid orderID = default(Guid), DateTime orderDate = default(DateTime))
        {
            InitializeComponent();
            TableID = tableID;
            NumberOfTable = numberOfTable;
            TableStatus = tableStatus;
            BookingID = bookingID;
            FromTime = fromTime;
            OrderID = orderID;
            OrderDate = orderDate;
            //timerRefreshData.Enabled = true;
        }

        private void lblNumberOfTable_Click(object sender, EventArgs e)
        {
            this.OnClick(e);
        }

        private void lblNumberOfTable_DoubleClick(object sender, EventArgs e)
        {
            this.OnDoubleClick(e);
        }

        private void tbrManager_ToolClick(object sender, Infragistics.Win.UltraWinToolbars.ToolClickEventArgs e)
        {
            try
            {
                LoadData();
                ToolbarClick(e.Tool.Key);
                OnLoadAreaStatus(e);
            }
            catch (Exception ex)
            {
                MessageBoxCommon.ShowException(ex);
            }
        }

        /// <summary>
        /// Sự kiện click vào Menu
        /// </summary>
        /// <param name="key"></param>
        private void ToolbarClick(string key)
        {
            switch (key)
            {
                case btnAddOrder:
                    // Thêm Order
                    AddOrder();
                    break;
                case btnAddBooking:
                    // Thêm đặt bàn
                    AddBooking();
                    break;
                case btnReceiveTable:
                    ReceiveTable();
                    break;
                case btnPay:
                    PaymentOrder();
                    break;
            }
            LoadData();
        }

        private void PaymentOrder()
        {
            using (var frm = new FrmInvoice())
            {
                frm.SAInvoiceID = OrderID;
                if (frm.ShowDialog() == DialogResult.OK)
                {
                  // frm.SAInvoiceRow =;
                }
            }
        }

        /// <summary>
        /// Nhận bàn
        /// </summary>
        private void ReceiveTable()
        {
            AddOrder();
        }

        /// <summary>
        /// Thêm đặt bàn
        /// </summary>
        private void AddBooking()
        {
            if (TableStatus != EnumTableStatus.Empty)
            {
                MessageBoxCommon.ShowExclamation("Bàn đã sử dụng không thể thêm Đặt bàn");
                return;
            }
            using (FrmBookingDetail frm = new FrmBookingDetail())
            {
                frm.FormActionMode = ActionMode.AddNew;
                frm.DsDictionary = new DictionaryDataSet();
                frm.BsDetail.DataSource = frm.DsDictionary;
                frm.TableID = TableID;
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    //if (frm.BookingStatus == EnumBookingStatus.Receiver || frm.BookingStatus == EnumBookingStatus.Cancel)
                    //{
                    //    Empty();
                    //}
                    //else if (frm.BookingStatus == EnumBookingStatus.Booked)
                    //{
                    //    TableStatus = EnumTableStatus.Booking;
                    //    if (frm.DsDictionary.Booking.Rows.Count > 0)
                    //    {
                    //        var row = (Desktop.Entity.DictionaryDataSet.BookingRow)frm.DsDictionary.Booking.Rows[0];
                    //        BookingID = row.BookingID;
                    //        FromTime = row.FromTime;
                    //    }
                    //}
                }
            }
        }

        /// <summary>
        /// Thêm Order
        /// </summary>
        private void AddOrder()
        {
            if (TableStatus == EnumTableStatus.Using)
            {
                MessageBoxCommon.ShowExclamation("Bàn đã sử dụng không thể thêm Order");
                return;
            }
            using (FrmOrderDetail frm = new FrmOrderDetail())
            {
                if (TableStatus == EnumTableStatus.Booking)
                    frm.BookingID = BookingID;
                frm.FormActionMode = ActionMode.AddNew;
                frm.DsDictionary = new DictionaryDataSet();
                frm.BsDetail.DataSource = frm.DsDictionary;
                frm.TableID = TableID;
                
                if (frm.ShowDialog() == DialogResult.OK || frm.IsSendKitchen)
                {
                    //if (TableStatus == EnumTableStatus.Booking)
                    //{
                    //    new Desktop.BL.BLBooking().ChangeBookingStatus(BookingID, (int)EnumBookingStatus.Receiver);
                    //}
                    //if (frm.OrderStatus == EnumOrderStatus.Cancel || frm.OrderStatus == EnumOrderStatus.Done)
                    //{
                    //    Empty();
                    //}
                    //else if (frm.OrderStatus == EnumOrderStatus.Ordering || frm.OrderStatus == EnumOrderStatus.WaitPay)
                    //{
                    //    TableStatus = EnumTableStatus.Using;
                    //    if (frm.DsDictionary.Order.Rows.Count > 0)
                    //    {
                    //        var row = (Desktop.Entity.DictionaryDataSet.OrderRow)frm.DsDictionary.Order.Rows[0];
                    //        OrderID = row.OrderID;
                    //        OrderDate = row.OrderDate;
                    //    }
                    //}
                }
            }
        }

        /// <summary>
        /// Gán rỗng cho control
        /// </summary>
        public void Empty()
        {
            TableStatus = EnumTableStatus.Empty;
            BookingID = Guid.Empty;
            FromTime = DateTime.MinValue;
            OrderID = Guid.Empty;
            OrderDate = DateTime.MinValue;
        }

        /// <summary>
        /// Load lại thông tin khu vực
        /// </summary>
        public event EventHandler LoadAreaStatus;

        /// <summary>
        /// Tính lại tổng tiền
        /// </summary>
        protected virtual void OnLoadAreaStatus(EventArgs e)
        {
            if (LoadAreaStatus != null)
            {
                LoadAreaStatus(this, e);
            }
        }

        #region Load ngầm

        private BusinessDataSet loadOnDemand = new BusinessDataSet();

        /// <summary>
        /// Sự kiện Click thì tạm dừng Background Worker
        /// </summary>
        /// <param name="e"></param>
        protected override void OnClick(EventArgs e)
        {
            LoadData();
            base.OnClick(e);
            LoadData();
        }

        /// <summary>
        /// Load lại dữ liệu tránh dữ liệu bị thay đổi
        /// </summary>
        public void LoadData()
        {
            var table = new Desktop.BL.BLArea().GetTableMappingDetailByTableID(TableID, Today);
            if (table != null)
            {
                if (table.Rows.Count > 0)
                {
                    Empty();
                    TableStatus = (EnumTableStatus)table.Rows[0][ColumnName.TableStatus];
                    if (TableStatus == EnumTableStatus.Booking)
                    {
                        BookingID = Guid.Parse(table.Rows[0][ColumnName.BookingID].ToString());
                        FromTime = DateTime.Parse(table.Rows[0][ColumnName.FromTime].ToString());
                        OrderID = Guid.Empty;
                        OrderDate = DateTime.MinValue;
                    }
                    if (TableStatus == EnumTableStatus.Using)
                    {
                        OrderID = Guid.Parse(table.Rows[0][ColumnName.OrderID].ToString());
                        OrderDate = DateTime.Parse(table.Rows[0][ColumnName.OrderDate].ToString());
                        BookingID = Guid.Empty;
                        FromTime = DateTime.MinValue;
                    }
                }
            }
        }

        private void bgwRefreshData_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                MessageBoxCommon.ShowException(ex);
            }
        }

        private void bgwRefreshData_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
            }
            catch (Exception ex)
            {
                MessageBoxCommon.ShowException(ex);
            }
        }

        private void timerRefreshData_Tick(object sender, EventArgs e)
        {
            try
            {
                if (!bgwRefreshData.IsBusy)
                    bgwRefreshData.RunWorkerAsync();
            }
            catch (Exception ex)
            {
                MessageBoxCommon.ShowException(ex);
            }
        }

        #endregion
    }
}
