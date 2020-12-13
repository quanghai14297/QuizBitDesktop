using Desktop.Entity;
using System;
using System.Data;

namespace Desktop.BL
{
    public class BLInventoryItemCategory
    {
        public BLInventoryItemCategory()
        {
        }

        /// <summary>
        /// Khởi tạo dữ liệu
        /// </summary>
        /// <param name="table"></param>
        public DataRow InitNewRow(DictionaryDataSet.InventoryItemCategoryDataTable table)
        {
            DictionaryDataSet.InventoryItemCategoryRow drNewRow = table.NewInventoryItemCategoryRow();
            drNewRow.InventoryItemCategoryID = Guid.NewGuid();
            drNewRow.InventoryItemCategoryCode = "";
            drNewRow.InventoryItemCategoryName = "";
            drNewRow.Description = "";
            drNewRow.Inactive = false;
            drNewRow.CreatedDate = DateTime.Now;
            drNewRow.ModifiedDate = DateTime.Now;
            drNewRow.CreatedBy = Session.UserLogin.DisplayName;
            drNewRow.ModifiedBy = Session.UserLogin.DisplayName;
            table.Rows.InsertAt(drNewRow, 0);
            return drNewRow;
        }

        public DataRow InitCopyRow(DictionaryDataSet.InventoryItemCategoryDataTable table, DictionaryDataSet.InventoryItemCategoryRow row)
        {
            DictionaryDataSet.InventoryItemCategoryRow drNewRow = table.NewInventoryItemCategoryRow();
            drNewRow.InventoryItemCategoryID = Guid.NewGuid();
            drNewRow.InventoryItemCategoryCode = row.InventoryItemCategoryCode;
            drNewRow.InventoryItemCategoryName = row.InventoryItemCategoryName;
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
            return new DL.DLInventoryItemCategory(Session.Token).Get();
        }

        public int InsertUpdate(DictionaryDataSet.InventoryItemCategoryRow drObjectChange, Guid oldID)
        {
            return new DL.DLInventoryItemCategory(Session.Token).InsertUpdate(drObjectChange, oldID);
        }

        public bool CheckCodeExists(DictionaryDataSet.InventoryItemCategoryRow drObjectChange)
        {
            drObjectChange.ModifiedDate = DateTime.Now;
            drObjectChange.ModifiedBy = Session.UserLogin.DisplayName;
            return new DL.DLInventoryItemCategory(Session.Token).CheckCodeExists(drObjectChange);
        }

        public int Delete(Guid id)
        {
            return new DL.DLInventoryItemCategory(Session.Token).Delete(id);
        }

        public bool CheckBeforeDelete(Guid id)
        {
            return new DL.DLInventoryItemCategory(Session.Token).CheckBeforeDelete(id);
        }
    }
}
