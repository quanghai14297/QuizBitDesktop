using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Desktop.Entity;
using Desktop.Lib;
using QuizBit.Contract;
using QuizBit.Entity;


namespace Desktop.DL
{
    public class DLReport : DLBase
    {
        private string token;

        public string Token
        {
            get { return token; }
            set { token = value; headerParameter.Add(DLKey.Authorization, Token); }
        }
        private Dictionary<string, string> headerParameter;

        public Dictionary<string, string> HeaderParameter
        {
            get { return headerParameter; }
            set { headerParameter = value; }
        }
        public DLReport(string token)
        {
            HeaderParameter = new Dictionary<string, string>();
            Token = token;
            
        }
        public DataTable GetReportSalesCustomer(DateTime FromDate, DateTime ToDate)
        {
            var objects= new Dictionary<string, DateTime>();
            objects.Add("FromDate", FromDate);
            objects.Add("ToDate", ToDate);
            ServiceResult<ReportSalesCustomer> list = CloudServiceFactory<ReportSalesCustomer>.ExecuteFunction("Report/getReportSalesCustomer", objects, HeaderParameter, "POST");
            if (list.Success && list.Data != null)
                return CommonFunction.ConvertItemToDataTable(list.Data);
            else return null;
        }
        public DataTable GetReportSalesArea(DateTime FromDate, DateTime ToDate)
        {
            var objects = new Dictionary<string, DateTime>();
            objects.Add("FromDate", FromDate);
            objects.Add("ToDate", ToDate);
            ServiceResult<ReportSalesArea> item = CloudServiceFactory<ReportSalesArea>.ExecuteFunction("Report/getReportSalesArea", objects, HeaderParameter, "POST");
            if (item.Success && item.Data != null)
                return CommonFunction.ConvertItemToDataTable(item.Data);
            else return null;
        }
        public DataTable GetReportSales(DateTime FromDate, DateTime ToDate)
        {
            var objects = new Dictionary<string, DateTime>();
            objects.Add("FromDate", FromDate);
            objects.Add("ToDate", ToDate);
            ServiceResult<SAInvoiceViewer> list = CloudServiceFactory<SAInvoiceViewer>.ExecuteFunction("Report/getReportSales", objects, HeaderParameter, "POST");
            if (list.Success && list.Data != null)
                return CommonFunction.ConvertItemToDataTable(list.Data);
            else return null;
        }
        

    }
}
