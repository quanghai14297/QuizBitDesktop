using Desktop.Entity;
using System;
using System.Data;

namespace Desktop.BL
{
    public class BLUnit
    {
        public BLUnit()
        {
        }

        /// <summary>
        /// Khởi tạo dữ liệu
        /// </summary>
        /// <param name="table"></param>
        public DataRow InitNewRow(DictionaryDataSet.UnitDataTable table)
        {
            DictionaryDataSet.UnitRow drNewRow = table.NewUnitRow();
            drNewRow.UnitID = Guid.NewGuid();
            drNewRow.UnitName = "";
            drNewRow.Description = "";
            drNewRow.Inactive = false;
            drNewRow.CreatedDate = DateTime.Now;
            drNewRow.ModifiedDate = DateTime.Now;
            drNewRow.CreatedBy = Session.UserLogin.DisplayName;
            drNewRow.ModifiedBy = Session.UserLogin.DisplayName;
            drNewRow.OldIDs = String.Empty;
            table.Rows.InsertAt(drNewRow, 0);
            return drNewRow;
        }

        public DataRow InitCopyRow(DictionaryDataSet.UnitDataTable table, DictionaryDataSet.UnitRow row)
        {
            DictionaryDataSet.UnitRow drNewRow = table.NewUnitRow();
            drNewRow.UnitID = Guid.NewGuid();
            drNewRow.UnitName = row.UnitName;
            drNewRow.Description = row.Description;
            drNewRow.Inactive = false;
            drNewRow.CreatedDate = DateTime.Now;
            drNewRow.ModifiedDate = DateTime.Now;
            drNewRow.CreatedBy = Session.UserLogin.DisplayName;
            drNewRow.ModifiedBy = Session.UserLogin.DisplayName;
            table.Rows.InsertAt(drNewRow, 0);
            return drNewRow;
        }

        public DataTable Get()
        {
            return new DL.DLUnit(Session.Token).Get();
        }

        public int InsertUpdate(DictionaryDataSet.UnitRow drObjectChange, Guid oldID)
        {
            return new DL.DLUnit(Session.Token).InsertUpdate(drObjectChange, oldID);
        }

        public bool CheckCodeExists(DictionaryDataSet.UnitRow drObjectChange)
        {
            drObjectChange.ModifiedDate = DateTime.Now;
            drObjectChange.ModifiedBy = Session.UserLogin.DisplayName;
            return new DL.DLUnit(Session.Token).CheckCodeExists(drObjectChange);
        }

        public int Delete(Guid id)
        {
            return new DL.DLUnit(Session.Token).Delete(id);
        }

        public bool CheckBeforeDelete(Guid id)
        {
            return new DL.DLUnit(Session.Token).CheckBeforeDelete(id);
        }
    }
}
