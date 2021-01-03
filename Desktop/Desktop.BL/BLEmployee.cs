using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Desktop.Entity;

namespace Desktop.BL
{
    public class BLEmployee : BLBase
    {
        public BLEmployee() : base()
        {
            TableMasterName = "Customer";
            TableDetailName = "";
            oDL = new DL.DLCustomer(Session.Token);
        }

        /// <summary>
        /// Khởi tạo dữ liệu
        /// </summary>
        /// <param name="table"></param>
        public DataRow InitNewRow(DictionaryDataSet.EmployeeInforDataTable table)
        {
            DictionaryDataSet.EmployeeInforRow drNewRow = table.NewEmployeeInforRow();
            drNewRow.EmployeeID = Guid.NewGuid();
            drNewRow.EmployeeCode = "";
            drNewRow.EmployeeName = "";
            drNewRow.Birthday = DateTime.Now.AddYears(-10);
            drNewRow.Email = "";
            drNewRow.Mobile = "";
            drNewRow.Address = "";
            drNewRow.Description = "";
            table.Rows.InsertAt(drNewRow, 0);
            return drNewRow;
        }

        public DataRow InitCopyRow(DictionaryDataSet.EmployeeInforDataTable table, DictionaryDataSet.EmployeeInforRow row)
        {
            DictionaryDataSet.EmployeeInforRow drNewRow = table.NewEmployeeInforRow();
            drNewRow.EmployeeID = Guid.NewGuid();
            drNewRow.EmployeeCode = row.EmployeeCode;
            drNewRow.EmployeeName = row.EmployeeName;
            drNewRow.Email = row.Email;
            drNewRow.Mobile = row.Mobile;
            drNewRow.Address = row.Address;
            drNewRow.Description = row.Description;
            table.Rows.InsertAt(drNewRow, 0);
            return drNewRow;
        }

        public DataTable Get()
        {
            return new DL.DLEmployee(Session.Token).Get();
        }

        public int InsertUpdate(DictionaryDataSet.EmployeeInforRow drObjectChange)
        {
            return new DL.DLEmployee(Session.Token).InsertUpdate(drObjectChange);
        }

        public bool CheckCodeExists(DictionaryDataSet.EmployeeInforRow drObjectChange)
        {
            return new DL.DLEmployee(Session.Token).CheckCodeExists(drObjectChange);
        }

        public int Delete(Guid id)
        {
            return new DL.DLEmployee(Session.Token).Delete(id);
        }

        public bool CheckBeforeDelete(Guid id)
        {
            return new DL.DLEmployee(Session.Token).CheckBeforeDelete(id);
        }
    }
}
