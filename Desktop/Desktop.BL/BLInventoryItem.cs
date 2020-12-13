using System;
using Desktop.Entity;
using System.Data;

namespace Desktop.BL
{
    public class BLInventoryItem
    {
        public BLInventoryItem()
        {
        }

        /// <summary>
        /// Khởi tạo dữ liệu
        /// </summary>
        /// <param name="table"></param>
        public DataRow InitNewRow(DictionaryDataSet.InventoryItemDataTable table)
        {
            DictionaryDataSet.InventoryItemRow drNewRow = table.NewInventoryItemRow();
            drNewRow.InventoryItemID = Guid.NewGuid();
            drNewRow.InventoryItemCode = "";
            drNewRow.InventoryItemName = "";
            drNewRow.InventoryItemType = 0;
            drNewRow.SetInventoryItemCategoryIDNull();
            drNewRow.SetUnitIDNull();
            drNewRow.Description = "";
            drNewRow.SetFileResourceNull();
            drNewRow.CourseType = 0;
            drNewRow.UnitPrice = 0;
            drNewRow.Inactive = false;
            drNewRow.CreatedDate = DateTime.Now;
            drNewRow.ModifiedDate = DateTime.Now;
            drNewRow.CreatedBy = Session.UserLogin.DisplayName;
            drNewRow.ModifiedBy = Session.UserLogin.DisplayName;
            drNewRow.OldIDs = "";
            table.Rows.InsertAt(drNewRow, 0);
            return drNewRow;
        }

        public DataRow InitCopyRow(DictionaryDataSet.InventoryItemDataTable table, DictionaryDataSet.InventoryItemRow row)
        {
            DictionaryDataSet.InventoryItemRow drNewRow = table.NewInventoryItemRow();
            drNewRow.InventoryItemID = Guid.NewGuid();
            drNewRow.InventoryItemCode = row.InventoryItemCode;
            drNewRow.InventoryItemName = row.InventoryItemName;
            drNewRow.InventoryItemType = row.InventoryItemType;
            if (row.IsInventoryItemCategoryIDNull())
                drNewRow.SetInventoryItemCategoryIDNull();
            else
                drNewRow.InventoryItemCategoryID = row.InventoryItemCategoryID;
            if (row.IsUnitIDNull())
                drNewRow.SetUnitIDNull();
            else
                drNewRow.UnitID = row.UnitID;
            drNewRow.Description = row.Description;
            if (row.IsFileResourceNull())
                drNewRow.SetFileResourceNull();
            else
                drNewRow.FileResource = row.FileResource;
            drNewRow.CourseType = row.CourseType;
            drNewRow.UnitPrice = row.IsUnitPriceNull() ? 0 : row.UnitPrice;
            drNewRow.Inactive = row.Inactive;
            drNewRow.CreatedDate = DateTime.Now;
            drNewRow.ModifiedDate = DateTime.Now;
            drNewRow.CreatedBy = Session.UserLogin.DisplayName;
            drNewRow.ModifiedBy = Session.UserLogin.DisplayName;
            drNewRow.OldIDs = row.OldIDs;
            table.Rows.InsertAt(drNewRow, 0);
            return drNewRow;
        }

        public DataTable Get()
        {
            return new DL.DLInventoryItem(Session.Token).Get();
        }

        public int InsertUpdate(DictionaryDataSet.InventoryItemRow drObjectChange, Guid oldID)
        {
            return new DL.DLInventoryItem(Session.Token).InsertUpdate(drObjectChange, oldID);
        }

        public bool CheckCodeExists(DictionaryDataSet.InventoryItemRow drObjectChange)
        {
            drObjectChange.ModifiedDate = DateTime.Now;
            drObjectChange.ModifiedBy = Session.UserLogin.DisplayName;
            return new DL.DLInventoryItem(Session.Token).CheckCodeExists(drObjectChange);
        }

        public int Delete(Guid id)
        {
            return new DL.DLInventoryItem(Session.Token).Delete(id);
        }

        public bool CheckBeforeDelete(Guid id)
        {
            return new DL.DLInventoryItem(Session.Token).CheckBeforeDelete(id);
        }

        public DictionaryDataSet.InventoryItemRow GetByID(Guid objectID)
        {
            return new DL.DLInventoryItem(Session.Token).GetByID(objectID);
        }

        public DataTable GetByID_SAInvoice(Guid objectID)
        {
            return new DL.DLInventoryItem(Session.Token).GetByID_SAInvoice(objectID);
        }
    }
}
