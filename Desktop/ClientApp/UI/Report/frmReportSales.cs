using Desktop.BL;
using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Infragistics.Documents.Excel;
using Infragistics.Win.UltraWinGrid;
using System.Globalization;
using  Infragistics.Win.UltraWinGrid;

namespace ClientApp.UI.Report
{
    public partial class frmReportSales : Form
    {
        public frmReportSales()
        {
            InitializeComponent();
        }
        private BLReport oBL;
        string FormatString = "#{0}##0{1}{2}_);[Red](#{0}##0{1}{2})";
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
        private void frmReportSales_Load(object sender, EventArgs e)
        {
            oBL = new BLReport();
            dtFromDate.Value = GetFirstDayOfMonth(DateTime.Now);
            dtToDate.Value = GetLastDayOfMonth(DateTime.Now);
            var table = oBL.GetReportSales(DateTime.Parse(dtFromDate.Value.ToString()), DateTime.Parse(dtToDate.Value.ToString()));
            if (table != null && table.Rows.Count > 0)
            {
                dsReport.SAInvoiceViewer.Clear();
                dsReport.Merge(table);
                dsReport.AcceptChanges();
            }
        }
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void ultraDateTimeEditor1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void ultraDateTimeEditor2_ValueChanged(object sender, EventArgs e)
        {

        }

        private void btnGetData_Click(object sender, EventArgs e)
        {
            oBL = new BLReport();
            dtFromDate.Value = GetFirstDayOfMonth(DateTime.Now);
            dtToDate.Value = GetLastDayOfMonth(DateTime.Now);
            var table = oBL.GetReportSales(DateTime.Parse(dtFromDate.Value.ToString()), DateTime.Parse(dtToDate.Value.ToString()));
            if (table != null && table.Rows.Count > 0)
            {
                dsReport.SAInvoiceViewer.Clear();
                dsReport.Merge(table);
                dsReport.AcceptChanges();
            }
        }

        private void tbrFunction_ToolClick(object sender, Infragistics.Win.UltraWinToolbars.ToolClickEventArgs e)
        {
            try
            {
                ToolbarClick(e.Tool.Key);
            }
            catch (Exception ex)
            {
                MessageBoxCommon.ShowException(ex);
            }
        }
        private void GetData()
        {
            oBL = new BLReport();
            dtFromDate.Value = GetFirstDayOfMonth(DateTime.Now);
            dtToDate.Value = GetLastDayOfMonth(DateTime.Now);
            var table = oBL.GetReportSales(DateTime.Parse(dtFromDate.Value.ToString()), DateTime.Parse(dtToDate.Value.ToString()));
            if (table != null && table.Rows.Count > 0)
            {
                dsReport.SAInvoiceViewer.Clear();
                dsReport.Merge(table);
                dsReport.AcceptChanges();
            }
        }
        /// <summary>
        /// Hàm khi click vào Toolbar
        /// </summary>
        protected virtual void ToolbarClick(string itemKey)
        {
            switch (itemKey)
            {
                case "mnuExport": ExportData(); break;
                case "mnuGet": GetData(); break;
            }
        }
        private void ExportData()
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            DateTime FromDate = DateTime.Parse(dtFromDate.Value.ToString());
            DateTime ToDate = DateTime.Parse(dtToDate.Value.ToString());
            saveFileDialog1.DefaultExt = "txt";
            saveFileDialog1.Filter = "ExcelFile |*.xlsx";
            saveFileDialog1.AddExtension = true;
            saveFileDialog1.RestoreDirectory = true;
            saveFileDialog1.Title = "Bạn cần lưu file ở đâu?";
            saveFileDialog1.InitialDirectory = @"C:/";
            string datetime = string.Format("Tử ngày {0} đến ngày {1}", FromDate.ToString("dd/MM/yyyy"), ToDate.ToString("dd/MM/yyyy"));
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                var row = Session.CompanyInfo;
                string[] lstTitle = { String.Format("{0} - {1}", row.Name, row.Address),"BÁO CÁO TÌNH HÌNH DOANH THU", datetime };
                Infragistics.Documents.Excel.Workbook workbook = new Infragistics.Documents.Excel.Workbook();
                if (Path.GetExtension(saveFileDialog1.FileName) == ".xlsx")
                {
                    workbook.SetCurrentFormat(WorkbookFormat.Excel2007);
                }
                workbook.Culture = CultureInfo.CurrentCulture;
                Infragistics.Documents.Excel.Worksheet SheetExcel  =null;
                string sGroupSeparator = workbook.Culture.NumberFormat.CurrencyGroupSeparator;
                string sDecimalSeparator = workbook.Culture.NumberFormat.CurrencyDecimalSeparator;
                List<UltraGridColumn> lstColumn = new List<UltraGridColumn>();
                UltraGridColumn clColumn = grdList.DisplayLayout.Bands[0].GetFirstVisibleCol(grdList.ActiveColScrollRegion, true);
                while(clColumn != null)
                {
                    lstColumn.Add(clColumn);
                    clColumn = clColumn.GetRelatedVisibleColumn(VisibleRelation.Next);
                }
                List<UltraGridColumn> lstGroup = new List<UltraGridColumn>();
                foreach (UltraGridColumn gridColumnGroup in grdList.DisplayLayout.Bands[0].SortedColumns)
                {
                    if (gridColumnGroup.IsGroupByColumn)
                    {
                        lstGroup.Add(gridColumnGroup);
                    }
                }
                SheetExcel = workbook.Worksheets.Add("Bao_cao_doanh_thu");
                int iHeaderPosition = lstColumn.Count;
                for (int i = 0; i <= lstTitle.Length - 1; i++)
                {
                    string sTitle = lstTitle[i];
                    {

                        SheetExcel.Rows[i].Cells[iHeaderPosition / 2].CellFormat.Alignment = HorizontalCellAlignment.Center;
                        SheetExcel.Rows[i].Cells[iHeaderPosition / 2].CellFormat.Font.Bold = ExcelDefaultableBoolean.True;
                        SheetExcel.Rows[i].Cells[iHeaderPosition / 2].Value = sTitle;
                       
                        SheetExcel.Rows[i].Cells[iHeaderPosition / 2].CellFormat.Font.Name = "";
                        SheetExcel.Rows[i].Cells[iHeaderPosition / 2].CellFormat.BottomBorderStyle = CellBorderLineStyle.None;
                        SheetExcel.Rows[i].Cells[iHeaderPosition / 2].CellFormat.TopBorderStyle = CellBorderLineStyle.None;
                        SheetExcel.Rows[i].Cells[iHeaderPosition / 2].CellFormat.LeftBorderStyle = CellBorderLineStyle.None;
                        SheetExcel.Rows[i].Cells[iHeaderPosition / 2].CellFormat.RightBorderStyle = CellBorderLineStyle.None;
                        if (i == 0)
                        {
                            SheetExcel.Rows[i].Cells[iHeaderPosition / 2].CellFormat.Font.Height = 280;
                            SheetExcel.Rows[i].Cells[iHeaderPosition / 2].CellFormat.Alignment = HorizontalCellAlignment.Left;
                        }
                        else if (i == 1)
                        {
                            SheetExcel.Rows[i].Cells[iHeaderPosition / 2].CellFormat.Font.Height = 380;
                        }

                    }
                    SheetExcel.MergedCellsRegions.Add(i, 0, i, iHeaderPosition - 1);
                }
                Infragistics.Win.UltraWinGrid.ExcelExport.UltraGridExcelExporter ultraGridExcelExporter = new Infragistics.Win.UltraWinGrid.ExcelExport.UltraGridExcelExporter();
                ultraGridExcelExporter.BandSpacing = Infragistics.Win.UltraWinGrid.ExcelExport.BandSpacing.None;
                ultraGridExcelExporter.Export(grdList, SheetExcel, lstTitle.Length, 0);
                for (int i = 0; i < lstGroup.Count - 1; i += 1)
                {
                    SheetExcel.Columns[i].SetWidth(10, WorksheetColumnWidthUnit.Pixel);
                }
                for (int i = lstGroup.Count; i <= lstGroup.Count + lstColumn.Count - 1; i += 1)
                {
                    SheetExcel.Columns[i].SetWidth(lstColumn[i - lstGroup.Count].Width, WorksheetColumnWidthUnit.Pixel);
                    if (lstColumn[i - lstGroup.Count].DataType == typeof(DateTime))
                    {
                        SheetExcel.Columns[i].CellFormat.FormatString = "dd/MM/yyyy";
                    }
                    else if (lstColumn[i - lstGroup.Count].DataType == typeof(double) || lstColumn[i - lstGroup.Count].DataType == typeof(decimal) || lstColumn[i - lstGroup.Count].DataType == typeof(int) || lstColumn[i - lstGroup.Count].DataType == typeof(Int16) || lstColumn[i - lstGroup.Count].DataType == typeof(Int32) || lstColumn[i - lstGroup.Count].DataType == typeof(Int64))
                    {

                        string sFormat = lstColumn[i - lstGroup.Count].Format == null ? "" : lstColumn[i - lstGroup.Count].Format;
                        int iDecimalDigit = 0;
                        if ((sFormat.StartsWith("N") || sFormat.StartsWith("C")) && int.TryParse(sFormat.Substring(1), out iDecimalDigit))
                        {
                            if (iDecimalDigit > 0)
                            {
                                SheetExcel.Columns[i].CellFormat.FormatString = string.Format(FormatString, sGroupSeparator, sDecimalSeparator, new string('0', iDecimalDigit));
                            }
                            else
                            {
                                SheetExcel.Columns[i].CellFormat.FormatString = string.Format(FormatString, sGroupSeparator, string.Empty, string.Empty);
                            }
                        }
                        else
                        {
                            SheetExcel.Columns[i].CellFormat.FormatString = "0";
                        }
                    }
                }
                workbook.Save(saveFileDialog1.FileName);
            }
            else
            {
               
            }
            saveFileDialog1.Dispose();
            saveFileDialog1 = null;
                         
        }
        //Hàm thu hồi bộ nhớ cho COM Excel
        private static void releaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                obj = null;
            }
            finally
            { GC.Collect(); }
        }

    }
}
