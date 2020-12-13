using Desktop.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop.BL
{
    public class BLSendKitchen : BLBase
    {
        public BLSendKitchen() : base()
        {
            oDL = new DL.DLSendKitchen();
        }

        /// <summary>
        /// Khởi tạo dữ liệu
        /// </summary>
        /// <param name="table"></param>
        public DataRow InitNewRow(DictionaryDataSet.SendKitchenDataTable table, Guid orderDetailID)
        {
            DictionaryDataSet.SendKitchenRow drNewRow = table.NewSendKitchenRow();
            drNewRow.SendKitchenID = Guid.NewGuid();
            //drNewRow.KitchenID = "";
            drNewRow.DataContent = "";
            drNewRow.SenderID = Session.UserLogin.EmployeeID;
            drNewRow.SendDate = DateTime.Now;
            drNewRow.Description = "";
            drNewRow.InventoryItemID = orderDetailID;
            drNewRow.SendKitchenType = 0;
            table.Rows.InsertAt(drNewRow, 0);
            return drNewRow;
        }
    }
}
