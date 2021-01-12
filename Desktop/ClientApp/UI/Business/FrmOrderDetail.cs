using ClientApp.UI.Controls;
using ClientApp.UI.Dictionary;
using Desktop.BL;
using Desktop.Entity;
using Infragistics.Win.UltraWinGrid;
using QuizBit.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClientApp.UI.Business
{
    public partial class FrmOrderDetail : BaseForm.FrmBaseDetail
    {

        #region Declaration
        private const string key_tabGeneralInfo = "tabGeneralInfo";
        private const string key_tabInventoryItem = "tabInventoryItem";
        #endregion

        public FrmOrderDetail()
        {
            InitializeComponent();
        }

        private Guid _orderID;

        /// <summary>
        /// ID Order
        /// </summary>
        public Guid OrderID
        {
            get
            {
                if (_orderID == Guid.Empty)
                {
                    if (BsDetail != null && BsDetail.Current != null)
                        _orderID = ((Desktop.Entity.DictionaryDataSet.OrderRow)((System.Data.DataRowView)BsDetail.Current).Row).OrderID;
                }
                return _orderID;
            }
            set
            {
                _orderID = value;
                if (BsDetail != null)
                {
                    var current = BsDetail.Find(ColumnName.OrderID, _orderID);
                    BsDetail.Position = current;
                }
            }
        }

        private Guid guid;

        /// <summary>
        /// ID bàn chọn lúc xem đặt bàn
        /// </summary>
        public Guid TableID
        {
            get { return guid; }
            set { guid = value; }
        }

        private Guid _bookingID;

        /// <summary>
        /// ID Đặt bàn
        /// </summary>
        public Guid BookingID
        {
            get { return _bookingID; }
            set { _bookingID = value; }
        }

        private decimal _totalAmount = 0;

        /// <summary>
        /// Tổng tiền
        /// </summary>
        public decimal TotalAmount
        {
            get { return _totalAmount; }
            set
            {
                _totalAmount = value;
                lblTotalAmount.Text = CommonFunction.ConvertToCurrency(_totalAmount);
            }
        }

        private EnumOrderStatus _orderStatus = EnumOrderStatus.Ordering;

        /// <summary>
        /// Trạng thái Order
        /// </summary>
        public EnumOrderStatus OrderStatus
        {
            get { return _orderStatus; }
            set
            {
                _orderStatus = value;
                if (_orderStatus == EnumOrderStatus.Cancel)
                {
                    btnCancel.Visible = false;
                    txtCancelReason.Visible = true;
                    lblCancelReason.Visible = true;
                }
            }
        }

        private bool _isSendKitchen = false;

        /// <summary>
        /// Order này đã có món nào gửi bếp chưa
        /// <para>true- có || false- không</para>
        /// </summary>
        public bool IsSendKitchen
        {
            get { return _isSendKitchen; }
            set
            {
                _isSendKitchen = value;
                btnCancelOrder.Visible = !_isSendKitchen && FormActionMode != ActionMode.AddNew;
                btnPay.Visible = _isSendKitchen && FormActionMode != ActionMode.AddNew;
            }
        }

        private BLOrder _oBL;

        public BLOrder objBLDetail
        {
            get
            {
                if (_oBL == null) _oBL = new BLOrder();
                return _oBL;
            }
            set { _oBL = value; }
        }

        protected override void BindingData()
        {
            base.BindingData();
            txtOrderNo.DataBindings.Add(new Binding("Text", BsDetail, DsDictionary.Order.OrderNoColumn.ColumnName, true));

            cboTableMapping.DataBindings.Add(new Binding("Value", BsDetail, DsDictionary.Order.TableIDColumn.ColumnName, true));
            cboTableMapping.Value = TableID;
            if (cboTableMapping.Value != null)
            {
                dsBusiness.AreaTableMapping.Clear();
                dsBusiness.AreaTableMapping.Merge(new BLArea().GetTableMappingByTableID(Guid.Parse(cboTableMapping.Value.ToString()), DateTime.Now));
                bsTableMapping.DataSource = dsBusiness;
            }
            else
            {

            }

            cboArea.Value = ((Desktop.Entity.BusinessDataSet.AreaTableMappingRow)((System.Data.DataRowView)bsTableMapping.Current).Row).AreaID;
            cboCustomerID.DataBindings.Add(new Binding("Value", BsDetail, DsDictionary.Order.CustomerIDColumn.ColumnName, true));

            txtNumberOfPeople.DataBindings.Add(new Binding("Text", BsDetail, DsDictionary.Order.NumberOfPeopleColumn.ColumnName, true));
            dteOrderDate.DataBindings.Add(new Binding("Value", BsDetail, DsDictionary.Order.OrderDateColumn.ColumnName, true));
            txtCancelReason.DataBindings.Add(new Binding("Text", BsDetail, DsDictionary.Order.CancelReasonColumn.ColumnName, true));
        }

        /// <summary>
        /// Load lại danh sách bàn theo Khu vực
        /// </summary>
        private void LoadTableMapping()
        {
            if (cboArea.Value == null) return;
            Guid areaID;
            if (!Guid.TryParse(cboArea.Value.ToString(), out areaID)) return;
            var tableMapping = new BLArea().GetTableMappingByAreaID(areaID, dteOrderDate.DateTime);
            if (tableMapping == null || tableMapping.Rows.Count == 0)
            {
                MessageBoxCommon.ShowExclamation("Khu vực <" + ((Desktop.Entity.DictionaryDataSet.AreaRow)((System.Data.DataRowView)bsArea.Current).Row).AreaName + "> ngày <" + dteOrderDate.DateTime.ToString("dd-MM-yyyy") + "> không có bàn trống nên không thể thay đổi khu vực");
                return;
            }
            else
            {
                dsBusiness.AreaTableMapping.Clear();
                dsBusiness.AreaTableMapping.Merge(tableMapping);
                bsTableMapping.DataSource = dsBusiness;
                if (FormActionMode == ActionMode.AddNew && BookingID != Guid.Empty)
                    bsTableMapping.Filter = "TableStatus = 0 OR TableID = " + "'" + TableID.ToString() + "'";
                else
                    bsTableMapping.Filter = "TableStatus = 0 OR (TableStatus = 1 AND TableID = " + "'" + TableID.ToString() + "' AND OrderID = '" + OrderID.ToString() + "')";
                if (bsTableMapping.Find(ColumnName.TableID, TableID) == -1)
                    cboTableMapping.Value = ((Desktop.Entity.BusinessDataSet.AreaTableMappingRow)dsBusiness.AreaTableMapping.Rows[0]).TableID;
                else
                    cboTableMapping.Value = TableID;
            }
        }

        protected override void LoadDataForeignKey()
        {
            base.LoadDataForeignKey();
            ShareDictionary.LoadCustomer(true);
            dsDictionary.Customer.Clear();
            dsDictionary.Customer.Merge(ShareDictionary.DsDictionary.Customer);
            bsCustomer.DataSource = DsDictionary;
            ShareDictionary.LoadArea();
            dsDictionary.Area.Clear();
            dsDictionary.Area.Merge(ShareDictionary.DsDictionary.Area);
            bsArea.DataSource = DsDictionary;
            bsTableMapping.DataSource = dsBusiness;
            ShareDictionary.LoadInventoryItem();
            dsDictionary.InventoryItem.Clear();
            dsDictionary.InventoryItem.Merge(ShareDictionary.DsDictionary.InventoryItem);
        }

        protected override void LoadDataForm()
        {
            base.LoadDataForm();
            LoadInventoryItem();
            LoadDetail();
        }

        /// <summary>
        /// Load chi tiết Order
        /// </summary>
        private void LoadDetail()
        {
            DsDictionary.OrderDetail.Clear();
            DsDictionary.OrderDetail.Merge(objBLDetail.GetDetailByID(OrderID));
            fpnlChooseFood.Controls.Clear();
            foreach (var row in dsDictionary.OrderDetail.Rows)
            {
                var uct = CreateUctChooseFoodByOrderDetail((DictionaryDataSet.OrderDetailRow)row);
                fpnlChooseFood.Controls.Add(uct);
                TotalAmount += uct.UnitPrice * uct.Quantity;
            }
        }

        protected override void ChangeFormByActionMode()
        {
            base.ChangeFormByActionMode();
            if (FormActionMode == ActionMode.AddNew)
            {
                cboCustomerID.Enabled = true;
                dteOrderDate.Enabled = true;
            }
            else
            {
                cboCustomerID.Enabled = false;
                dteOrderDate.Enabled = false;
            }
        }

        protected override bool ValidateForm()
        {
            if (!base.ValidateForm()) return false;
            if (string.IsNullOrEmpty(txtOrderNo.Text.Trim()))
            {
                MessageBoxCommon.ShowExclamation(string.Format(Properties.Resources.Message_Validate_Control, txtOrderNo.Tag));
                txtOrderNo.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtOrderNo.Text.Trim()))
            {
                MessageBoxCommon.ShowExclamation(string.Format(Properties.Resources.Message_Validate_Control, txtOrderNo.Tag));
                txtOrderNo.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(cboArea.Text.Trim()))
            {
                MessageBoxCommon.ShowExclamation(string.Format(Properties.Resources.Message_Validate_Control, cboArea.Tag));
                cboArea.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(cboTableMapping.Text.Trim()))
            {
                MessageBoxCommon.ShowExclamation(string.Format(Properties.Resources.Message_Validate_Control, cboTableMapping.Tag));
                cboTableMapping.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(cboCustomerID.Text.Trim()))
            {
                MessageBoxCommon.ShowExclamation(string.Format(Properties.Resources.Message_Validate_Control, cboCustomerID.Tag));
                cboCustomerID.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtNumberOfPeople.Text.Trim()))
            {
                MessageBoxCommon.ShowExclamation(string.Format(Properties.Resources.Message_Validate_Control, txtNumberOfPeople.Tag));
                txtNumberOfPeople.Focus();
                return false;
            }
            else
            {
                int table = 0;
                if (int.TryParse(txtNumberOfPeople.Value.ToString(), out table))
                {
                    if (table <= 0)
                    {
                        MessageBoxCommon.ShowExclamation(string.Format(Properties.Resources.Message_Validate_Control_More, txtNumberOfPeople.Tag, "0"));
                        txtNumberOfPeople.Focus();
                        return false;
                    }
                }
            }
            if (dteOrderDate.Value == null)
            {
                MessageBoxCommon.ShowExclamation(string.Format(Properties.Resources.Message_Validate_Control, dteOrderDate.Tag));
                dteOrderDate.Focus();
                return false;
            }
            return true;
        }

        private void cboInventoryItemCategory_EditorButtonClick(object sender, Infragistics.Win.UltraWinEditors.EditorButtonEventArgs e)
        {
            dsDictionary.Customer.AcceptChanges();
            using (var fDetail = new FrmCustomerDetail())
            {
                fDetail.FormActionMode = ActionMode.AddNew;
                fDetail.DsDictionary = dsDictionary;
                fDetail.BsDetail = bsCustomer;
                fDetail.objBLDetail = new BLCustomer();
                if (fDetail.ShowDialog() != DialogResult.OK) dsDictionary.Customer.RejectChanges();
                else
                {
                    dsDictionary.Customer.AcceptChanges();
                    cboCustomerID.Value = ((Desktop.Entity.DictionaryDataSet.CustomerRow)((System.Data.DataRowView)bsCustomer.Current).Row).CustomerID;
                }
            }
        }

        private void cboArea_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                LoadTableMapping();
            }
            catch (Exception ex)
            {
                MessageBoxCommon.ShowException(ex);
            }
        }

        private void dteFromTime_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                LoadTableMapping();
            }
            catch (Exception ex)
            {
                MessageBoxCommon.ShowException(ex);
            }
        }

        private void cboCustomerID_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                Guid id = Guid.Empty;
                if (cboCustomerID.Value == null)
                {
                    lblCustomerName.Text = "";
                    lblCustomerMobile.Text = "";
                    lblCustomerAddress.Text = "";
                    return;
                }
                if (Guid.TryParse(cboCustomerID.Value.ToString(), out id))
                {
                    var row = dsDictionary.Customer.FindByCustomerID(Guid.Parse(cboCustomerID.Value.ToString()));
                    //Desktop.Entity.DictionaryDataSet.CustomerRow cRow = lstRow
                    lblCustomerName.Text = row.IsCustomerNameNull() ? "" : row.CustomerName;
                    lblCustomerMobile.Text = row.IsMobileNull() ? "" : row.Mobile;
                    lblCustomerAddress.Text = row.IsAddressNull() ? "" : row.Address;
                }
                else
                {
                    lblCustomerName.Text = "";
                    lblCustomerMobile.Text = "";
                    lblCustomerAddress.Text = "";
                }
            }
            catch (Exception ex)
            {
                MessageBoxCommon.ShowException(ex);
            }
        }

        protected override void InitNewRow()
        {
            base.InitNewRow();
            DictionaryDataSet.OrderRow row = (DictionaryDataSet.OrderRow)objBLDetail.InitNewRow(DsDictionary.Order);
            if (BookingID != Guid.Empty)
            {
                row.BookingID = BookingID;
                var bookingRow = ShareDictionary.DsDictionary.Booking.FindByBookingID(BookingID);
                if (bookingRow != null)
                {
                    row.CustomerID = ShareDictionary.DsDictionary.Booking.FindByBookingID(BookingID).CustomerID;
                    row.NumberOfPeople = bookingRow.NumberOfPeople;
                    row.OrderDate = bookingRow.FromTime;
                }
                else row.OrderDate = Session.Today;
            }
            BsDetail.MoveFirst();
            txtOrderNo.Focus();
        }

        protected override int SaveData()
        {
            int result = 1;
            BsDetail.EndEdit();

            //var tableChanged = dsDictionary.Order.GetChanges();
            //var tableDetailChanged = dsDictionary.OrderDetail.GetChanges();
            //if (tableChanged == null && tableDetailChanged == null)
            //{
            //    return (int)EnumResultInsertUpdate.Success;
            //}
            //else
            //{
            //    //if (tableChanged.Rows.Count == 0 && tableDetailChanged.Rows.Count == 0)
            //    //    return (int)EnumResultInsertUpdate.Success;
            //}
            //DictionaryDataSet.OrderRow drObjectChange = (DictionaryDataSet.OrderRow)CurrentRow;
            DictionaryDataSet.OrderDataTable table = DsDictionary.Order;
            DictionaryDataSet.OrderRow drObjectChange = table.NewOrderRow();
           
                drObjectChange.OrderID = Guid.NewGuid();
           
            drObjectChange.OrderNo = txtOrderNo.Text;
            drObjectChange.OrderDate = DateTime.Now;
            drObjectChange.OrderStatus = 0;
            drObjectChange.NumberOfPeople = 1;
            drObjectChange.EmployeeID = Session.UserLogin.EmployeeID;
            drObjectChange.CreatedDate = DateTime.Now;
            drObjectChange.CreatedBy = Session.UserLogin.UserName;
            drObjectChange.ModifiedDate = DateTime.Now;
            drObjectChange.ModifiedBy = Session.UserLogin.UserName;
            drObjectChange.TableID = Guid.Parse(cboTableMapping.Value.ToString());
            drObjectChange.CustomerID = Guid.Parse(cboCustomerID.Value.ToString());
            drObjectChange.NumberOfPeople = Int32.Parse(txtNumberOfPeople.Text);
            drObjectChange.OrderDate = DateTime.Now;
            drObjectChange.CancelReason = txtCancelReason.Text;
            drObjectChange.BookingID = BookingID;
            //////Chỗ này nhớ lấy booking id
            if (drObjectChange != null)
            {
                drObjectChange.ModifiedDate = DateTime.Now;
                drObjectChange.ModifiedBy = Session.UserLogin.UserName;
                objBLDetail.InsertUpdateOrder(drObjectChange,DsDictionary);
            }
            return result;
        }

        /// <summary>
        /// Nhận bàn
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReceiveTable_Click(object sender, EventArgs e)
        {
            int result = 1;
            try
            {
                DictionaryDataSet.BookingRow drObjectChange = (DictionaryDataSet.BookingRow)CurrentRow;
                if (drObjectChange != null)
                {
                    drObjectChange.BookingStatus = (int)EnumBookingStatus.Receiver;
                    result = objBLDetail.InsertUpdateObject(QuizBit.Contract.CommonFunction.GetItem<Booking>(drObjectChange));
                    if (result > 0)
                    {
                        OrderStatus = EnumOrderStatus.WaitPay;
                        DialogResult = DialogResult.OK;
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBoxCommon.ShowException(ex);
            }
        }

        /// <summary>
        /// Hủy đặt bàn
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancelBooking_Click(object sender, EventArgs e)
        {
            try
            {
                int result = 1;
                string cancelDescription = "";
                if (MessageBoxCommon.ShowYesNoQuestion_Cancel(EnumCancelAction.Booking, out cancelDescription) == DialogResult.Yes)
                {
                    DictionaryDataSet.BookingRow drObjectChange = (DictionaryDataSet.BookingRow)CurrentRow;
                    if (drObjectChange != null)
                    {
                        drObjectChange.CancelDescription = cancelDescription;
                        drObjectChange.BookingStatus = (int)EnumBookingStatus.Cancel;
                        result = objBLDetail.InsertUpdateObject(QuizBit.Contract.CommonFunction.GetItem<Booking>(drObjectChange));
                        if (result > 0)
                        {
                            OrderStatus = EnumOrderStatus.Cancel;
                            DialogResult = DialogResult.OK;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBoxCommon.ShowException(ex);
            }
        }

        private void cboTableMapping_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (cboTableMapping.Value != null)
                    Guid.TryParse(cboTableMapping.Value.ToString(), out guid);
            }
            catch (Exception ex)
            {
                MessageBoxCommon.ShowException(ex);
            }
        }

        private void FrmOrderDetail_Shown(object sender, EventArgs e)
        {
            try
            {
                if (FormActionMode == ActionMode.AddNew)
                {
                    //btnPay.Visible = false;
                    //btnReceiveTable.Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBoxCommon.ShowException(ex);
            }
        }

        /// <summary>
        /// Load thông tin mặt hàng
        /// </summary>
        private void LoadInventoryItem()
        {
            fpnlInventoryItem.Controls.Clear();
            foreach (DictionaryDataSet.InventoryItemRow item in ShareDictionary.DsDictionary.InventoryItem.Rows)
            {
                var control = CreateUctFood(item);
                control.Click += Food_Click;
                fpnlInventoryItem.Controls.Add(control);
            }
        }

        /// <summary>
        /// Khởi tạo UserControl TableMapping
        /// </summary>
        /// <param name="iAreaTable"></param>
        /// <returns></returns>
        private UctFood CreateUctFood(DictionaryDataSet.InventoryItemRow iInventoryItem)
        {
            return new UctFood(iInventoryItem);
        }

        /// <summary>
        /// Thêm món ăn vào Order
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Food_Click(object sender, EventArgs e)
        {
            try
            {
                if (OrderStatus == EnumOrderStatus.Cancel)
                {
                    MessageBoxCommon.ShowExclamation("Order đã hủy không thể thêm món ăn");
                    return;
                }
                UctFood control = (UctFood)sender;
                if (control != null)
                {
                    var uct = CreateUctChooseFood(control.DataInventoryItemRow);
                    if (uct != null)
                    {
                        fpnlChooseFood.Controls.Add(uct);
                        TotalAmount += uct.UnitPrice * uct.Quantity;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBoxCommon.ShowException(ex);
            }
        }

        private void ChooseFood_Click(object sender, EventArgs e)
        {
            try
            {
                UctChooseFood control = (UctChooseFood)sender;
                if (control != null)
                {
                    if (control.IsRemove && !control.IsSendKitchen)
                    {
                        fpnlChooseFood.Controls.Remove(control);
                        control.OrderDetailRow.Delete();
                        //DsDictionary.OrderDetail.RemoveOrderDetailRow(control.OrderDetailRow);
                        TotalAmount -= control.UnitPrice * control.Quantity;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBoxCommon.ShowException(ex);
            }
        }

        /// <summary>
        /// Thêm mới món ăn
        /// </summary>
        /// <param name="inventoryItem"></param>
        /// <returns></returns>
        private UctChooseFood CreateUctChooseFood(DictionaryDataSet.InventoryItemRow inventoryItem)
        {
            DictionaryDataSet.OrderRow master = (DictionaryDataSet.OrderRow)dsDictionary.Order.FindByOrderID(OrderID);
            var row = dsDictionary.OrderDetail.FirstOrDefault((t) => t.RowState != DataRowState.Deleted && t.InventoryItemID == inventoryItem.InventoryItemID && t.OrderDetailStatus == (int)EnumOrderDetailStatus.Ordering);
            if (row != null)
            {
                row.Quantity += 1;
                return null;
            }
            else
            {
                DictionaryDataSet.OrderDetailRow orderDetail = (DictionaryDataSet.OrderDetailRow)_oBL.InitNewRowDetail(dsDictionary.OrderDetail, master, inventoryItem.InventoryItemID);
                var uct = new UctChooseFood(orderDetail);
                uct.Click += ChooseFood_Click;
                uct.CalculateAmount += new EventHandler(CalculateAmount);
                return uct;
            }
        }

        /// <summary>
        /// Load món ăn
        /// </summary>
        /// <param name="inventoryItem"></param>
        /// <returns></returns>
        private UctChooseFood CreateUctChooseFoodByOrderDetail(DictionaryDataSet.OrderDetailRow orderDetail)
        {
            var uct = new UctChooseFood(orderDetail);
            uct.Click += ChooseFood_Click;
            uct.CalculateAmount += new EventHandler(CalculateAmount);
            if (!IsSendKitchen && uct.IsSendKitchen) IsSendKitchen = true;
            return uct;
        }

        private void tabInfo_SelectedTabChanged(object sender, Infragistics.Win.UltraWinTabControl.SelectedTabChangedEventArgs e)
        {

        }

        private void FrmOrderDetail_Load(object sender, EventArgs e)
        {
            try
            {
                btnSendKitchen.Visible = FormActionMode != ActionMode.AddNew;
                btnCancelOrder.Visible = !_isSendKitchen && FormActionMode != ActionMode.AddNew;
                btnPay.Visible = _isSendKitchen && FormActionMode != ActionMode.AddNew;
                if (FormActionMode != ActionMode.AddNew)
                {
                    tabInfo.SelectedTab = tabInfo.Tabs[key_tabInventoryItem];
                }
            }
            catch (Exception ex)
            {
                MessageBoxCommon.ShowException(ex);
            }
        }

        private void btnLoadInventory_Click(object sender, EventArgs e)
        {
            try
            {
                LoadInventoryItem();
                LoadDetail();
            }
            catch (Exception ex)
            {
                MessageBoxCommon.ShowException(ex);
            }
        }

        /// <summary>
        /// Tính lại tổng tiền
        /// </summary>
        public void CalculateAmount(object sender, EventArgs e)
        {
            decimal amount = 0;
            foreach (var item in fpnlChooseFood.Controls)
            {
                if (item.GetType() == typeof(UctChooseFood))
                {
                    UctChooseFood uct = (UctChooseFood)item;
                    amount += uct.UnitPrice * uct.Quantity;
                }
            }
            TotalAmount = amount;
        }

        private void btnSendKitchen_Click(object sender, EventArgs e)
        {
            try
            {
                int countSendKitchen = 0;
                var oBLSendKitchen = new BLSendKitchen();
                foreach (var item in fpnlChooseFood.Controls)
                {
                    if (item.GetType() == typeof(UctChooseFood))
                    {
                        UctChooseFood uct = (UctChooseFood)item;
                        if (!uct.IsSendKitchen)
                        {
                            countSendKitchen++;
                            // Insert To SendKitchen
                            var sendKitchenRow = oBLSendKitchen.InitNewRow(dsDictionary.SendKitchen, uct.OrderDetailRow.OrderDetailID);
                            oBLSendKitchen.InsertUpdate(sendKitchenRow);
                            // Update detail
                            uct.OrderDetailRow.OrderDetailStatus = (int)EnumOrderDetailStatus.Served;
                            uct.IsSendKitchen = true;
                        }
                    }
                }
                if (SaveData() > 0)
                {
                    FormActionMode = ActionMode.Edit;
                    DsDictionary.AcceptChanges();
                }
                if (countSendKitchen == 0)
                {
                    MessageBoxCommon.ShowExclamation("Không có món nào mới để gửi bếp.");
                }
                else IsSendKitchen = true;
            }
            catch (Exception ex)
            {
                MessageBoxCommon.ShowException(ex);
            }
        }

        /// <summary>
        /// Thanh toán thì tạo Hóa đơn
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPay_Click(object sender, EventArgs e)
        {
            try
            {
                using (var frm = new FrmInvoice())
                {
                    frm.FormActionMode = ActionMode.AddNew;
                    frm.OrderID = OrderID;
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        OrderStatus = EnumOrderStatus.Done;
                        DialogResult = DialogResult.OK;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBoxCommon.ShowException(ex);
            }
        }

        private void rdoAll_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                int condition = 0;
                if (rdoAll.Checked) condition = 0;
                else if (rdoFood.Checked) condition = 1;
                else if (rdoDrink.Checked) condition = 2;
                FilterInventory(condition);
            }
            catch (Exception ex)
            {
                MessageBoxCommon.ShowException(ex);
            }
        }

        /// <summary>
        /// Lọc mặt hàng theo điều kiện
        /// 0 - ALL, 1- Đồ ăn, 2- Đồ uống
        /// </summary>
        /// <param name="condition"></param>
        private void FilterInventory(int condition)
        {
            if (condition == 0)
            {
                foreach (var item in fpnlInventoryItem.Controls)
                {
                    if (item.GetType() == typeof(UctFood))
                    {
                        UctFood control = (UctFood)item;
                        control.Visible = true;
                    }
                }
            }
            else if (condition == 1)
            {
                foreach (var item in fpnlInventoryItem.Controls)
                {
                    if (item.GetType() == typeof(UctFood))
                    {
                        UctFood control = (UctFood)item;
                        if (control.InventoryItemType == EnumInventoryItemType.Other || control.InventoryItemType == EnumInventoryItemType.Food)
                            control.Visible = true;
                        else control.Visible = false;
                    }
                }
            }
            else if (condition == 2)
            {
                foreach (var item in fpnlInventoryItem.Controls)
                {
                    if (item.GetType() == typeof(UctFood))
                    {
                        UctFood control = (UctFood)item;
                        if (control.InventoryItemType == EnumInventoryItemType.Other || control.InventoryItemType == EnumInventoryItemType.Drink)
                            control.Visible = true;
                        else control.Visible = false;
                    }
                }
            }
        }

        /// <summary>
        /// Hủy Order
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancelOrder_Click(object sender, EventArgs e)
        {
            try
            {
                if (IsSendKitchen)
                {
                    MessageBoxCommon.ShowExclamation("Order đã có món gửi bếp nên không thể hủy Order.");
                    return;
                }
                int result = 1;
                string cancelDescription = "";
                if (MessageBoxCommon.ShowYesNoQuestion_Cancel(EnumCancelAction.Booking, out cancelDescription) == DialogResult.Yes)
                {
                    DictionaryDataSet.OrderRow drObjectChange = (DictionaryDataSet.OrderRow)CurrentRow;
                    if (drObjectChange != null)
                    {
                        drObjectChange.CancelReason = cancelDescription;
                        drObjectChange.OrderStatus = (int)EnumOrderStatus.Cancel;
                        drObjectChange.CancelEmployeeID = Session.UserLogin.EmployeeID;
                        result = objBLDetail.InsertUpdateObject(QuizBit.Contract.CommonFunction.GetItem<Order>(drObjectChange));
                        if (result > 0)
                        {
                            OrderStatus = EnumOrderStatus.Cancel;
                            DialogResult = DialogResult.OK;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBoxCommon.ShowException(ex);
            }
        }

        /// <summary>
        /// Thêm món vào panel chọn món
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpnlChooseFood_ControlAdded(object sender, ControlEventArgs e)
        {
            try
            {
                if (!btnSendKitchen.Visible) btnSendKitchen.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBoxCommon.ShowException(ex);
            }
        }
        private void cboArea_ItemNotInList(object sender, Infragistics.Win.UltraWinEditors.ValidationErrorEventArgs e)
        {
            try
            {
                e.RetainFocus = true;
                if (sender.GetType() == typeof(Infragistics.Win.UltraWinGrid.UltraCombo))
                {
                    UltraCombo control = (UltraCombo)sender;
                    MessageBoxCommon.ShowMessageError(string.Format("Dữ liệu <{0}> không có trong danh mục", control.Tag));
                    control.SelectAll();
                    if (!control.IsDroppedDown)
                        control.ToggleDropdown();
                }
            }
            catch (Exception ex)
            {
                MessageBoxCommon.ShowException(ex);
            }
        }

        protected override bool CheckCodeIsExists()
        {
            if (objBLDetail.CheckCodeIsExists(OrderID, txtOrderNo.Text.Trim()))
            {
                MessageBoxCommon.ShowMessageError("Mã Order <" + txtOrderNo.Text.Trim() + "> đã tồn tại");
                txtOrderNo.Focus();
                return false;
            }
            return base.CheckCodeIsExists();
        }
    }
}
