using System;
using Desktop.Entity;
using System.Data;

namespace Desktop.BL
{
    public class BLArea
    {
        public BLArea()
        {
        }

        /// <summary>
        /// Khởi tạo dữ liệu
        /// </summary>
        /// <param name="table"></param>
        public DataRow InitNewRow(DictionaryDataSet.AreaDataTable table)
        {
            DictionaryDataSet.AreaRow drNewRow = table.NewAreaRow();
            drNewRow.AreaID = Guid.NewGuid();
            drNewRow.AreaName = "";
            drNewRow.NumberOfTable = 1;
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

        public DataRow InitCopyRow(DictionaryDataSet.AreaDataTable table, DictionaryDataSet.AreaRow row)
        {
            DictionaryDataSet.AreaRow drNewRow = table.NewAreaRow();
            drNewRow.AreaID = Guid.NewGuid();
            drNewRow.AreaName = row.AreaName;
            drNewRow.NumberOfTable = row.NumberOfTable;
            drNewRow.Description = row.Description;
            drNewRow.Inactive = row.Inactive;
            drNewRow.CreatedDate = DateTime.Now;
            drNewRow.ModifiedDate = DateTime.Now;
            drNewRow.CreatedBy = Session.UserLogin.DisplayName;
            drNewRow.ModifiedBy = Session.UserLogin.DisplayName;
            drNewRow.OldIDs = String.Empty;
            table.Rows.InsertAt(drNewRow, 0);
            return drNewRow;
        }

        public DataTable Get()
        {
            return new DL.DLArea(Session.Token).Get();
        }

        public int InsertUpdate(DictionaryDataSet.AreaRow drObjectChange, Guid oldID)
        {
            return new DL.DLArea(Session.Token).InsertUpdate(drObjectChange, oldID);
        }

        public bool CheckCodeExists(DictionaryDataSet.AreaRow drObjectChange)
        {
            drObjectChange.ModifiedDate = DateTime.Now;
            drObjectChange.ModifiedBy = Session.UserLogin.DisplayName;
            return new DL.DLArea(Session.Token).CheckCodeExists(drObjectChange);
        }

        public int Delete(Guid id)
        {
            return new DL.DLArea(Session.Token).Delete(id);
        }

        public bool CheckBeforeDelete(Guid id)
        {
            return new DL.DLArea(Session.Token).CheckBeforeDelete(id);
        }

        /// <summary>
        /// Lấy ra danh sách bàn theo Khu vực
        /// </summary>
        /// <typeparam name="T">Kiểu đối tượng</typeparam>
        /// <param name="objectID">ID của đối tượng</param>
        /// <returns></returns>
        public DataTable GetTableMappingByAreaID(Guid id, DateTime today)
        {
            return new DL.DLTableMapping().GetTableMappingByAreaID(id, today);
        }

        /// <summary>
        /// Lấy ra danh sách bàn theo Khu vực
        /// </summary>
        /// <typeparam name="T">Kiểu đối tượng</typeparam>
        /// <param name="objectID">ID của đối tượng</param>
        /// <returns></returns>
        public DataTable GetTableMappingByTableID(Guid id, DateTime today)
        {
            return new DL.DLTableMapping().GetTableMappingByTableID(id, today);
        }

        /// <summary>
        /// Lấy ra chi tiết bàn
        /// </summary>
        /// <typeparam name="T">Kiểu đối tượng</typeparam>
        /// <param name="objectID">ID của đối tượng</param>
        /// <returns></returns>
        public DataTable GetTableMappingDetailByTableID(Guid id, DateTime today)
        {
            return new DL.DLTableMapping().GetTableMappingDetailByTableID(id, today);
        }
    }
}
