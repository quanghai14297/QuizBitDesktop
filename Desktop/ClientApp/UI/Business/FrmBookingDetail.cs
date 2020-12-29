using ClientApp.UI.Dictionary;
using Desktop.BL;
using Desktop.Entity;
using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;
using QuizBit.Entity;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace ClientApp.UI.Business
{
    public partial class FrmBookingDetail : BaseForm.FrmBaseDetail
    {

        private Guid _bookingID;

        /// <summary>
        /// ID đặt chỗ
        /// </summary>
        public Guid BookingID
        {
            get
            {
                if (_bookingID == Guid.Empty)
                {
                    if (BsDetail != null && BsDetail.Current != null)
                        _bookingID = ((Desktop.Entity.DictionaryDataSet.BookingRow)((System.Data.DataRowView)BsDetail.Current).Row).BookingID;
                }
                return _bookingID;
            }
            set
            {
                _bookingID = value;
                if (BsDetail != null)
                {
                    var current = BsDetail.Find(ColumnName.BookingID, _bookingID);
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

        /// <summary>
        /// Trạng thái đặt bàn
        /// </summary>
        public EnumBookingStatus BookingStatus { get; set; } = EnumBookingStatus.Booked;

        public FrmBookingDetail()
        {
            InitializeComponent();
        }

        private BLBooking _oBL;

        public BLBooking objBLDetail
        {
            get
            {
                if (_oBL == null) _oBL = new BLBooking();
                return _oBL;
            }
            set { _oBL = value; }
        }

        protected override void BindingData()
        {
            base.BindingData();
            txtBookingNo.DataBindings.Add(new Binding("Text", BsDetail, DsDictionary.Booking.BookingNoColumn.ColumnName, true));
            dteBookingDate.DataBindings.Add(new Binding("Value", BsDetail, DsDictionary.Booking.BookingDateColumn.ColumnName, true));

            cboTableMapping.DataBindings.Add(new Binding("Value", BsDetail, DsDictionary.Booking.TableIDColumn.ColumnName, true));
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
            //cboArea.DataBindings.Add(new Binding("Value", bsTableMapping, dsBusiness.AreaTableMapping.AreaIDColumn.ColumnName, true));

            cboCustomerID.DataBindings.Add(new Binding("Value", BsDetail, DsDictionary.Booking.CustomerIDColumn.ColumnName, true));

            //lblCustomerName.DataBindings.Add(new Binding("Text", bsCustomer, DsDictionary.Customer.CustomerNameColumn.ColumnName, true));
            //lblCustomerMobile.DataBindings.Add(new Binding("Text", bsCustomer, DsDictionary.Customer.MobileColumn.ColumnName, true));
            //lblCustomerAddress.DataBindings.Add(new Binding("Text", bsCustomer, DsDictionary.Customer.AddressColumn.ColumnName, true));

            txtNumberOfPeople.DataBindings.Add(new Binding("Text", BsDetail, DsDictionary.Booking.NumberOfPeopleColumn.ColumnName, true));
            dteFromTime.DataBindings.Add(new Binding("Value", BsDetail, DsDictionary.Booking.FromTimeColumn.ColumnName, true));
            txtDescription.DataBindings.Add(new Binding("Text", BsDetail, DsDictionary.Booking.RequestKitchenColumn.ColumnName, true));

        }

        /// <summary>
        /// Load lại danh sách bàn theo Khu vực
        /// </summary>
        private void LoadTableMapping()
        {
            if (cboArea.Value == null) return;
            Guid areaID;
            if (!Guid.TryParse(cboArea.Value.ToString(), out areaID)) return;
            var tableMapping = new BLArea().GetTableMappingByAreaID(areaID, dteFromTime.DateTime);
            if (tableMapping == null || tableMapping.Rows.Count == 0)
            {
                MessageBoxCommon.ShowExclamation("Khu vực <" + ((Desktop.Entity.DictionaryDataSet.AreaRow)((System.Data.DataRowView)bsArea.Current).Row).AreaName + "> ngày <" + dteFromTime.DateTime.ToString("dd-MM-yyyy") + "> không có bàn trống nên không thể thay đổi khu vực");
                return;
            }
            else
            {
                dsBusiness.AreaTableMapping.Clear();
                dsBusiness.AreaTableMapping.Merge(tableMapping);
                bsTableMapping.DataSource = dsBusiness;
                bsTableMapping.Filter = "TableStatus = 0 OR (TableStatus = 2 AND TableID = " + "'" + TableID.ToString() + "' AND BookingID = '" + BookingID.ToString() + "')";
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
            ShareDictionary.LoadArea(true);
            dsDictionary.Area.Clear();
            dsDictionary.Area.Merge(ShareDictionary.DsDictionary.Area);
            bsArea.DataSource = DsDictionary;
            bsTableMapping.DataSource = dsBusiness;
        }


        protected override void ChangeFormByActionMode()
        {
            base.ChangeFormByActionMode();
            if (FormActionMode == ActionMode.AddNew)
                cboCustomerID.Enabled = true;
            else
                cboCustomerID.Enabled = false;
        }

        protected override bool ValidateForm()
        {
            if (!base.ValidateForm()) return false;
            if (string.IsNullOrEmpty(txtBookingNo.Text.Trim()))
            {
                MessageBoxCommon.ShowExclamation(string.Format(Properties.Resources.Message_Validate_Control, txtBookingNo.Tag));
                txtBookingNo.Focus();
                return false;
            }
            if (dteBookingDate.Value == null)
            {
                MessageBoxCommon.ShowExclamation(string.Format(Properties.Resources.Message_Validate_Control, dteBookingDate.Tag));
                dteBookingDate.Focus();
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
            if (dteBookingDate.Value == null)
            {
                MessageBoxCommon.ShowExclamation(string.Format(Properties.Resources.Message_Validate_Control, dteBookingDate.Tag));
                dteBookingDate.Focus();
                return false;
            }
            if (dteFromTime.Value == null)
            {
                MessageBoxCommon.ShowExclamation(string.Format(Properties.Resources.Message_Validate_Control, dteFromTime.Tag));
                dteFromTime.Focus();
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
            objBLDetail.InitNewRow(DsDictionary.Booking);
            if (FormActionMode == ActionMode.AddNew)
                dteFromTime.Value = Session.Today;
            BsDetail.MoveFirst();
            txtBookingNo.Focus();
        }

        protected override int SaveData()
        {
            int result = 1;
            BsDetail.EndEdit();
            //var tableChanged = dsDictionary.Booking.GetChanges();
            //if (tableChanged == null)
            //{
            //    return (int)EnumResultInsertUpdate.Success;
            //}
            //else
            //{
            //    if (tableChanged.Rows.Count == 0)
            //        return (int)EnumResultInsertUpdate.Success;
            //}
            //DictionaryDataSet.BookingRow drObjectChange = (DictionaryDataSet.BookingRow)tableChanged.Rows[0];
            DictionaryDataSet.BookingDataTable table = DsDictionary.Booking;
            DictionaryDataSet.BookingRow drObjectChange = table.NewBookingRow();
            drObjectChange.BookingID = Guid.NewGuid();
            drObjectChange.BookingDate = DateTime.Now;
            drObjectChange.BookingStatus = 0;
            drObjectChange.FromTime = DateTime.Now;
            drObjectChange.NumberOfPeople = 1;
            drObjectChange.RequestMeal = "";
            drObjectChange.EmployeeID = Session.UserLogin.EmployeeID;
            drObjectChange.CreatedDate = DateTime.Now;
            drObjectChange.CreatedBy = Session.UserLogin.UserName;
            drObjectChange.ModifiedDate = DateTime.Now;
            drObjectChange.ModifiedBy = Session.UserLogin.UserName;
            drObjectChange.BookingNo = txtBookingNo.Text;
            drObjectChange.BookingDate = DateTime.Parse(dteBookingDate.Text);
            drObjectChange.TableID = (Guid)cboTableMapping.Value;
            drObjectChange.NumberOfPeople = Int32.Parse(txtNumberOfPeople.Text);
            drObjectChange.CustomerID = Guid.Parse(cboCustomerID.Value.ToString());
            drObjectChange.BookingDate = DateTime.Now;
            drObjectChange.CancelDescription = txtDescription.Text;
            if (drObjectChange != null)
            {
                drObjectChange.ModifiedDate = DateTime.Now;
                drObjectChange.ModifiedBy = Session.UserLogin.UserName;
                result = objBLDetail.InsertUpdateObject(QuizBit.Contract.CommonFunction.GetItem<Booking>(drObjectChange));
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
                        using (FrmOrderDetail frm = new FrmOrderDetail())
                        {
                            frm.FormActionMode = ActionMode.AddNew;
                            frm.DsDictionary = new DictionaryDataSet();
                            frm.BsDetail.DataSource = frm.DsDictionary;
                            frm.TableID = TableID;
                            frm.BookingID = BookingID;
                            if (frm.ShowDialog() == DialogResult.OK || frm.IsSendKitchen)
                            {
                                BookingStatus = EnumBookingStatus.Receiver;
                                DialogResult = DialogResult.OK;
                            }
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
                            BookingStatus = EnumBookingStatus.Cancel;
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

        private void FrmBookingDetail_Shown(object sender, EventArgs e)
        {
            try
            {
                if (FormActionMode == ActionMode.AddNew)
                {
                    btnCancelBooking.Visible = false;
                    btnReceiveTable.Visible = false;
                }
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
            if (objBLDetail.CheckCodeIsExists(BookingID, txtBookingNo.Text.Trim()))
            {
                MessageBoxCommon.ShowMessageError("Mã đặt bàn <" + txtBookingNo.Text.Trim() + "> đã tồn tại");
                txtBookingNo.Focus();
                return false;
            }
            return base.CheckCodeIsExists();
        }
    }
}
