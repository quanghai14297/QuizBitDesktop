using Desktop.BL;
using Desktop.Entity;
using MISA.MeInvoice.DataContract.Entity;
using MISA.MeInvoice.DataContract.SDKResult;
using MISA.MeInvoice.Lib2;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

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
                if (table.Rows.Count <= 0)
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
                try
                {
                    string token=null;
                    IInvoice invoiceObject = null;
                    var authen = MeInvoiceFactory.CreatUserClass();
                    GetTokenOperationResult oResult = authen.GetToken("8C3B3E41F3D34F639CEAF489FD62AD33", "0101243150-999", "demo@gmail.com", "12345678a");
                    if (oResult.Success && !string.IsNullOrWhiteSpace(oResult.Token))
                    {
                        token = oResult.Token;
                    }
                    OriginalInvoiceData invoiceData = BuildOriginalInvoiceData(dsDictionary);
                    List<OriginalInvoiceData> lstData = new List<OriginalInvoiceData>();
                    lstData.Add(invoiceData);
                    //Tạo dữ liệu hóa đơn định dạng XML
                    invoiceObject = MeInvoiceFactory.CreatInvoiceClass(token);
                    CreateInvoiceOperationResult result = invoiceObject.CreateEInvoice(lstData);
                    if (result.Success && result.EInvoiceResultDataList != null && result.EInvoiceResultDataList.Count > 0)
                    {
                        XmlDocument invoiceXML = new XmlDocument();
                        invoiceXML.LoadXml(result.EInvoiceResultDataList[0].InvoiceData);
                        MISA.MeInvoice.DataContract.SDKResult.SignedXmlResult CertFile = SignerUtils.SignByFile(invoiceXML.InnerXml, @"D:\MST0101243150-999-V2.p12", "12345678");
                        List<PublishInvoiceData> signedInvoice = new List<PublishInvoiceData>();
                        PublishInvoiceData pubInvData = new PublishInvoiceData();
                        pubInvData.RefID = invoiceData.RefID;
                        pubInvData.TransactionID = result.EInvoiceResultDataList[0].TransactionID;
                        pubInvData.InvoiceData = CertFile.SignedXmlContent;
                        signedInvoice.Add(pubInvData);
                        PublishInvoiceOperationResult oPubResult = invoiceObject.PublishInvoice(signedInvoice);
                        if (oPubResult.Success)
                        {
                            List<PublishInvoiceResult> pubResult = oPubResult.PublishInvoiceResultList;
                            if (pubResult != null && pubResult.Count > 0)
                            {
                                BLSAInvoice bLSAInvoice = new BLSAInvoice();
                                
                                bLSAInvoice.UpdateInvoiceNumber(Guid.Parse(dsDictionary.SAInvoice.Rows[0]["RefID"].ToString()), pubResult[0].InvoiceNumber, pubInvData.TransactionID);
                                btnPrint.Visible = true;
                                WebClient client = new WebClient();
                                string reply = client.DownloadString("https://meinvoice.vn/tra-cuu/GetRequestTimeEnCode");
                                var link = String.Format("http://meinvoice.vn/tra-cuu/downloadhandler.ashx?type=pdf&code={0}&viewer=1&ext={1}", pubInvData.TransactionID, reply.ToString().Substring(55, 8));
                                Process.Start(link);
                                
                            }
                        }

                    }


                }
                catch (Exception)
                {

                }

            }
            catch (Exception ex)
            {
                MessageBoxCommon.ShowException(ex);
            }
        }
        private OriginalInvoiceData BuildOriginalInvoiceData(DictionaryDataSet ds)
        {
            var row = Session.CompanyInfo;
            OriginalInvoiceData invoiceData = new OriginalInvoiceData();
            invoiceData.RefID = Guid.NewGuid().ToString();
            //0313686411
            invoiceData.SellerTaxCode = "0101243150-999";
            invoiceData.SellerLegalName = row.Name;
            invoiceData.SellerAddressLine = row.Address;
            //
            invoiceData.BuyerLegalName = ds.Customer.Rows[0]["CustomerName"].ToString();
            invoiceData.BuyerAddressLine = ds.Customer.Rows[0]["Address"].ToString();

            invoiceData.TemplateCode = "01GTKT0/157";
            invoiceData.InvoiceType = "01GTKT0";
            invoiceData.InvoiceSeries = "AB/21E";
            invoiceData.PaymentMethodName = "Tiền mặt";
            invoiceData.AdjustmentType = 1;
            invoiceData.IsSendEmail = false;

            invoiceData.ExchangeRate = 1;

            invoiceData.TotalAmountWithoutVAT = Decimal.Parse(ds.SAInvoice.Rows[0]["TotalAmount"].ToString());
            invoiceData.VatPercentage = 0;
            invoiceData.TotalVATAmount = invoiceData.TotalAmountWithoutVAT * invoiceData.VatPercentage / 100;
            invoiceData.TotalAmountWithVAT = invoiceData.TotalAmountWithoutVAT + invoiceData.TotalVATAmount;

            int count = 1;
            List<OriginalInvoiceDetail> detail = new List<OriginalInvoiceDetail>();

            BLSAInvoice bLSAInvoice = new BLSAInvoice();
            DataTable table = bLSAInvoice.GetSAInvoiceDetailAll(Guid.Parse(ds.SAInvoice.Rows[0]["RefID"].ToString()));
            for (int i = 0; i < table.Rows.Count; i++)
            {
                OriginalInvoiceDetail newDetail = new OriginalInvoiceDetail();
                newDetail.LineNumber = count;
                newDetail.ItemCode = table.Rows[0]["InventoryItemCode"].ToString();
                newDetail.ItemName = table.Rows[0]["InventoryItemName"].ToString();
                newDetail.UnitName = table.Rows[0]["UnitName"].ToString();
                newDetail.Quantity = decimal.Parse(table.Rows[0]["UnitPrice"].ToString());
                newDetail.Amount = decimal.Parse(table.Rows[0]["SaleAmount"].ToString()); ;
                newDetail.VatPercentage = 0;
                newDetail.VatAmount = 0;
                newDetail.UnitPrice = decimal.Parse(table.Rows[0]["UnitPrice"].ToString());

                detail.Add(newDetail);
            }

            invoiceData.OriginalInvoiceDetail = detail;

            return invoiceData;
        }
        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                //using (var f = new FrmPrintInvoice())
                //{
                //    f.SAInvoiceID = SAInvoiceRow.RefID;
                //    f.ShowDialog();
                //}
                BLSAInvoice bLSAInvoice = new BLSAInvoice();
                string trans=bLSAInvoice.GetSAInvoiceTransactionID(Guid.Parse(dsDictionary.SAInvoice.Rows[0]["RefID"].ToString()));
                WebClient client = new WebClient();
                string reply = client.DownloadString("https://meinvoice.vn/tra-cuu/GetRequestTimeEnCode");
                var link = String.Format("http://meinvoice.vn/tra-cuu/downloadhandler.ashx?type=pdf&code={0}&viewer=1&ext={1}", trans, reply.ToString().Substring(55, 8));
                Process.Start(link);
            }
            catch (Exception ex)
            {
                MessageBoxCommon.ShowException(ex);
            }
        }
    }
}
