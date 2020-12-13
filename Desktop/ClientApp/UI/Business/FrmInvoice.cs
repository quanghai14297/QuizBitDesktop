using Desktop.BL;
using Desktop.Entity;
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
    public partial class FrmInvoice : Form
    {

        private ActionMode _formActionMode = ActionMode.View;

        public ActionMode FormActionMode
        {
            get { return _formActionMode; }
            set
            {
                _formActionMode = value;
                if (_formActionMode == ActionMode.AddNew)
                {
                    btnSave.Visible = true;
                    btnPrint.Visible = false;
                }
                else
                {
                    btnSave.Visible = false;
                    btnPrint.Visible = true;
                }
            }
        }


        private BLOrder oBLOrder = new BLOrder();

        private BLSAInvoice oBLSAInvoice = new BLSAInvoice();

        private Guid _orderID = Guid.Empty;

        /// <summary>
        /// OrderID - Nếu tạo mới thì chuyển OrderID vào
        /// </summary>
        public Guid OrderID
        {
            get { return _orderID; }
            set { _orderID = value; }
        }

        private Guid _saInvoiceID;

        /// <summary>
        /// SAInvoiceID - Nếu xem hóa đơn đã tạo thì chuyển SAInvoiceID vào
        /// </summary>
        public Guid SAInvoiceID
        {
            get { return _saInvoiceID; }
            set { _saInvoiceID = value; }
        }


        private DictionaryDataSet.OrderRow _orderRow;

        /// <summary>
        /// Dữ liệu Order
        /// </summary>
        public DictionaryDataSet.OrderRow OrderRow
        {
            get { return _orderRow; }
            set { _orderRow = value; }
        }

        private DictionaryDataSet.SAInvoiceRow _saInvoiceRow;

        /// <summary>
        /// Dữ liệu hóa đơn
        /// </summary>
        public DictionaryDataSet.SAInvoiceRow SAInvoiceRow
        {
            get { return _saInvoiceRow; }
            set { _saInvoiceRow = value; }
        }

        private DictionaryDataSet.CustomerRow _customerRow;

        public DictionaryDataSet.CustomerRow CustomerRow
        {
            get { return _customerRow; }
            set { _customerRow = value; }
        }


        public FrmInvoice()
        {
            InitializeComponent();
        }

        private void FrmInvoice_Load(object sender, EventArgs e)
        {
            try
            {
                dsDictionary.EnforceConstraints = false;
                // Nếu tạo mới từ Order
                if (OrderID != Guid.Empty)
                {
                    btnSave.Visible = true;
                    dsDictionary.Clear();
                    dsDictionary.Order.Merge(oBLOrder.GetDataByID(OrderID), false, MissingSchemaAction.Ignore);
                    dsDictionary.OrderDetail.Merge(oBLOrder.GetDataDetailByID(OrderID), false, MissingSchemaAction.Ignore);
                    CreateSAInvoice();
                }
                // Nếu không phải tạo mới thì load dữ liệu
                else
                {
                    btnSave.Visible = false;
                    LoadSAInvoice();
                }
                SAInvoiceRow = dsDictionary.SAInvoice.FirstOrDefault();
                dsDictionary.Customer.Merge(new BLCustomer().GetDataByID(SAInvoiceRow.CustomerID));
                CustomerRow = dsDictionary.Customer.FindByCustomerID(SAInvoiceRow.CustomerID);
                BindingData();

            }
            catch (Exception ex)
            {
                MessageBoxCommon.ShowException(ex);
            }
        }

        /// <summary>
        /// Load thông tin hóa đơn
        /// </summary>
        private void LoadSAInvoice()
        {
            dsDictionary.Clear();
            dsDictionary.SAInvoice.Merge(oBLSAInvoice.GetDataByID(SAInvoiceID), false, MissingSchemaAction.Ignore);
            dsDictionary.SAInvoiceDetail.Merge(oBLSAInvoice.GetDataDetailByID(SAInvoiceID), false, MissingSchemaAction.Ignore);
            BLInventoryItem oBLInventoryItem = new BLInventoryItem();
            foreach (var item in dsDictionary.SAInvoiceDetail)
            {
                if (dsDictionary.InventoryItem.FindByInventoryItemID(item.ItemID) == null)
                    dsDictionary.InventoryItem.Merge(oBLInventoryItem.GetByID_SAInvoice(item.ItemID));
            }
        }

        /// <summary>
        /// Thêm Binding vào control
        /// </summary>
        private void BindingData()
        {
            lblRefNo.Text = SAInvoiceRow.RefNo;
            lblRefDate.Text = SAInvoiceRow.RefDate.ToString("dd-MM-yyyy HH:mm");
            lblJournalMemo.Text = SAInvoiceRow.JournalMemo;
            if (CustomerRow != null)
                lblCustomer.Text = CustomerRow.CustomerName + (string.IsNullOrEmpty(CustomerRow.Mobile) ? "" : " - " + CustomerRow.Mobile);
        }

        /// <summary>
        /// Tạo mới Hóa đơn từ Order
        /// </summary>
        private void CreateSAInvoice()
        {
            try
            {
                oBLSAInvoice.InitSAInvoice(dsDictionary);
                dsDictionary.InventoryItem.AcceptChanges();
            }
            catch (Exception ex)
            {
                MessageBoxCommon.ShowException(ex);
            }
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                Close();
            }
            catch (Exception ex)
            {
                MessageBoxCommon.ShowException(ex);
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (oBLSAInvoice.InsertUpdate(dsDictionary, 0))
                {
                    var updateOrder = oBLOrder.ChangeOrderStatus(SAInvoiceRow.OrderID, (int)EnumOrderStatus.Done);
                    if (updateOrder)
                    {
                        MessageBoxCommon.ShowMessage("Lập hóa đơn thành công");
                        DialogResult = DialogResult.OK;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBoxCommon.ShowException(ex);
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                using (var f = new FrmPrintInvoice())
                {
                    f.SAInvoiceID = SAInvoiceRow.RefID;
                    f.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBoxCommon.ShowException(ex);
            }
        }
    }
}
