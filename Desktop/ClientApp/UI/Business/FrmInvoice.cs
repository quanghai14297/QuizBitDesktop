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
                BLOrder oBL = new BLOrder();
                var table = oBL.GetDataByID(SAInvoiceID);
                if(table.Rows.Count <= 0 )
                {
                    BLSAInvoice oBLSAInvoice = new BLSAInvoice();
                     table = oBLSAInvoice.GetDataByID(SAInvoiceID);
                    // Nếu tạo mới từ Order
                    if (SAInvoiceID != Guid.Empty)
                    {
                        btnSave.Visible = true;
                        dsDictionary.Clear();
                        dsDictionary.SAInvoice.Merge(oBLSAInvoice.GetDataByID(SAInvoiceID), false, MissingSchemaAction.Ignore);
                        dsDictionary.SAInvoiceDetail.Merge(oBLSAInvoice.GetDataDetailByID(SAInvoiceID), false, MissingSchemaAction.Ignore);
                        CreateSAInvoice();
                    }
                    // Nếu không phải tạo mới thì load dữ liệu
                    else
                    {
                        btnSave.Visible = false;
                        LoadSAInvoice();
                    }
                }
                else
                {
                    // Nếu tạo mới từ Order
                    if (SAInvoiceID != Guid.Empty)
                    {
                        btnSave.Visible = true;
                        dsDictionary.Clear();
                        dsDictionary.Order.Merge(oBLOrder.GetDataByID(SAInvoiceID), false, MissingSchemaAction.Ignore);
                        dsDictionary.OrderDetail.Merge(oBLOrder.GetDataDetailByID(SAInvoiceID), false, MissingSchemaAction.Ignore);
                        CreateSAInvoice();
                    }
                    // Nếu không phải tạo mới thì load dữ liệu
                    else
                    {
                        btnSave.Visible = false;
                        LoadSAInvoice();
                    }
                }
                
                if (table != null && table.Rows.Count > 0)
                {
                    SAInvoiceRow = dsDictionary.SAInvoice.FirstOrDefault();
                    dsDictionary.Customer.Merge(new BLCustomer().GetDataByID(Guid.Parse(table.Rows[0][5].ToString())));
                    CustomerRow = dsDictionary.Customer.FindByCustomerID(Guid.Parse(table.Rows[0][5].ToString()));
                }
               
                BindingData(table);

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
        private void BindingData(DataTable table)
        {
            lblRefNo.Text = table.Rows[0][1].ToString();
            lblRefDate.Text = table.Rows[0][3].ToString();
            BLArea bLArea = new BLArea();
            if (table.Columns.Contains("JournalMemo"))
            {
                lblJournalMemo.Text = table.Rows[0][18].ToString();
            }
            else
            {
                DataTable dt = bLArea.GetTableMappingDetailByTableID(Guid.Parse(table.Rows[0][11].ToString()), DateTime.Now);
                lblJournalMemo.Text = String.Format("{0} -  {1}", dt.Rows[0][1].ToString(), dt.Rows[0][3].ToString());
            }
            
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
