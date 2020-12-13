using System;
using Desktop.Entity;
using System.Data;

namespace Desktop.BL
{
    public class BLCustomer : BLBase
    {
        public BLCustomer() : base()
        {
            TableMasterName = "Customer";
            TableDetailName = "";
            oDL = new DL.DLCustomer(Session.Token);
        }

        /// <summary>
        /// Khởi tạo dữ liệu
        /// </summary>
        /// <param name="table"></param>
        public DataRow InitNewRow(DictionaryDataSet.CustomerDataTable table)
        {
            DictionaryDataSet.CustomerRow drNewRow = table.NewCustomerRow();
            drNewRow.CustomerID = Guid.NewGuid();
            drNewRow.CustomerCode = "";
            drNewRow.CustomerName = "";
            drNewRow.Birthday = DateTime.Now.AddYears(-10);
            drNewRow.Email = "";
            drNewRow.Mobile = "";
            drNewRow.Address = "";
            drNewRow.Description = "";
            drNewRow.Inactive = false;
            table.Rows.InsertAt(drNewRow, 0);
            return drNewRow;
        }

        public DataRow InitCopyRow(DictionaryDataSet.CustomerDataTable table, DictionaryDataSet.CustomerRow row)
        {
            DictionaryDataSet.CustomerRow drNewRow = table.NewCustomerRow();
            drNewRow.CustomerID = Guid.NewGuid();
            drNewRow.CustomerCode = row.CustomerCode;
            drNewRow.CustomerName = row.CustomerName;
            drNewRow.Email = row.Email;
            drNewRow.Mobile = row.Mobile;
            drNewRow.Address = row.Address;
            drNewRow.Description = row.Description;
            drNewRow.Inactive = row.Inactive;
            table.Rows.InsertAt(drNewRow, 0);
            return drNewRow;
        }

        public DataTable Get()
        {
            return new DL.DLCustomer(Session.Token).Get();
        }

        public int InsertUpdate(DictionaryDataSet.CustomerRow drObjectChange)
        {
            return new DL.DLCustomer(Session.Token).InsertUpdate(drObjectChange);
        }

        public bool CheckCodeExists(DictionaryDataSet.CustomerRow drObjectChange)
        {
            return new DL.DLCustomer(Session.Token).CheckCodeExists(drObjectChange);
        }

        public int Delete(Guid id)
        {
            return new DL.DLCustomer(Session.Token).Delete(id);
        }

        public bool CheckBeforeDelete(Guid id)
        {
            return new DL.DLCustomer(Session.Token).CheckBeforeDelete(id);
        }
    }
}
