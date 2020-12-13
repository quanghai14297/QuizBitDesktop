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
    public partial class FrmPrintInvoice : Form
    {

        private Guid _saInvoiceID;

        public Guid SAInvoiceID
        {
            get { return _saInvoiceID; }
            set { _saInvoiceID = value; }
        }


        public FrmPrintInvoice()
        {
            InitializeComponent();
        }

        private void FrmPrintInvoice_Load(object sender, EventArgs e)
        {
            DataTable master = new DataTable();
            master = new Desktop.BL.BLSAInvoice().GetDataByID(SAInvoiceID);

            DataTable detail = new DataTable();
            detail = new Desktop.BL.BLSAInvoice().GetSAInvoice_Today_ByID(SAInvoiceID);

            Report.Report_PrintSAInvoice rp = new Report.Report_PrintSAInvoice();
            rp.SetDataSource(detail);
            crViewer.ReportSource = rp;
            //crViewer.PrintReport();
        }
    }
}
