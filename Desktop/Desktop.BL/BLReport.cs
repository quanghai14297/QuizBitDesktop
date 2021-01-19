using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Desktop.Entity;

namespace Desktop.BL
{
    public class BLReport : BLBase
    {
        public BLReport() : base()
        {
            TableMasterName = "";
            TableDetailName = "";
            oDL = new DL.DLCustomer(Session.Token);
        }
        public DataTable GetReportSalesCustomer(DateTime FromDate, DateTime ToDate)
        {
            return new DL.DLReport(Session.Token).GetReportSalesCustomer(FromDate, ToDate);
        }
        public DataTable GetReportSalesArea(DateTime FromDate, DateTime ToDate)
        {
            return new DL.DLReport(Session.Token).GetReportSalesArea(FromDate, ToDate);
        }

        public DataTable GetReportSales(DateTime FromDate, DateTime ToDate)
        {
            return new DL.DLReport(Session.Token).GetReportSales(FromDate, ToDate);
        }
        public DataTable GetReportSalesEmployee(DateTime FromDate, DateTime ToDate)
        {
            return new DL.DLReport(Session.Token).GetReportSalesEmployee(FromDate, ToDate);
        }

    }
}
