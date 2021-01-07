using System;
using System.Linq;
using System.Windows.Forms;
using Desktop.BL;
using Desktop.Entity;

namespace ClientApp.UI.Report
{
    public partial class frmReportSalesArea : Form
    {
        public frmReportSalesArea()
        {
            InitializeComponent();
        }
        private BLReport oBL;
        /// <summary>
        /// Lấy ra ngày đầu tiên trong tháng có chứa 
        /// 1 ngày bất kỳ được truyền vào
        /// </summary>
        /// <param name="dtDate">Ngày nhập vào</param>
        /// <returns>Ngày đầu tiên trong tháng</returns>
        public static DateTime GetFirstDayOfMonth(DateTime dtInput)
        {
            DateTime dtResult = dtInput;
            dtResult = dtResult.AddDays((-dtResult.Day) + 1);
            return dtResult;
        }
        /// <summary>
        /// Lấy ra ngày cuối cùng trong tháng có chứa 
        /// 1 ngày bất kỳ được truyền vào
        /// </summary>
        /// <param name="dtInput">Ngày nhập vào</param>
        /// <returns>Ngày cuối cùng trong tháng</returns>
        public static DateTime GetLastDayOfMonth(DateTime dtInput)
        {
            DateTime dtResult = dtInput;
            dtResult = dtResult.AddMonths(1);
            dtResult = dtResult.AddDays(-(dtResult.Day));
            return dtResult;
        }
        private void frmReportSalesArea_Load(object sender, EventArgs e)
        {
            oBL = new BLReport();
            dtFromDate.Value = GetFirstDayOfMonth(DateTime.Now);
            dtToDate.Value = GetLastDayOfMonth(DateTime.Now);
            var table = oBL.GetReportSalesArea(DateTime.Parse(dtFromDate.Value.ToString()), DateTime.Parse(dtToDate.Value.ToString()));
            if (table != null && table.Rows.Count > 0)
            {
                dsReport.ReportSalesArea.Clear();
                dsReport.Merge(table);
                dsReport.AcceptChanges();
            }
        }

        private void btnGetData_Click(object sender, EventArgs e)
        {
            oBL = new BLReport();
            dtFromDate.Value = GetFirstDayOfMonth(DateTime.Now);
            dtToDate.Value = GetLastDayOfMonth(DateTime.Now);
            var table = oBL.GetReportSalesArea(DateTime.Parse(dtFromDate.Value.ToString()), DateTime.Parse(dtToDate.Value.ToString()));
            if (table != null && table.Rows.Count > 0)
            {
                dsReport.ReportSalesArea.Clear();
                dsReport.Merge(table);
                dsReport.AcceptChanges();
            }
        }
    }
}
