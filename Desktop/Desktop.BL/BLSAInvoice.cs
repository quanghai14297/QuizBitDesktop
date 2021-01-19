using Desktop.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop.BL
{
    public class BLSAInvoice : BLBase
    {
        public BLSAInvoice() : base()
        {
            TableMasterName = "SAInvoice";
            TableDetailName = "SAInvoiceDetail";
            oDL = new DL.DLSAInvoice();
        }

        public DataRow InitNewRow(DictionaryDataSet.SAInvoiceDataTable table, DictionaryDataSet.OrderRow orderRow)
        {
            DictionaryDataSet.SAInvoiceRow drNewRow = table.NewSAInvoiceRow();
            drNewRow.OrderID = Guid.NewGuid();
            drNewRow.EmployeeID = Session.UserLogin.EmployeeID;
            drNewRow.CreatedDate = DateTime.Now;
            drNewRow.CreatedBy = Session.UserLogin.UserName;
            drNewRow.ModifiedDate = DateTime.Now;
            drNewRow.ModifiedBy = Session.UserLogin.UserName;
            table.Rows.InsertAt(drNewRow, 0);
            return drNewRow;
        }

        public DataRow InitNewRowDetail(DictionaryDataSet.OrderDetailDataTable table, DictionaryDataSet.OrderRow master, Guid inventoryItemID)
        {
            DictionaryDataSet.OrderDetailRow drNewRow = table.NewOrderDetailRow();
            drNewRow.OrderDetailID = Guid.NewGuid();
            drNewRow.OrderID = master.OrderID;
            drNewRow.InventoryItemID = inventoryItemID;
            drNewRow.Quantity = 1;
            drNewRow.SortOrder = table.Rows.Count + 1;
            drNewRow.CookedQuantity = 0;
            drNewRow.ServedQuantity = 0;
            drNewRow.ServedQuantity = 0;
            drNewRow.CookingQuantity = 0;
            drNewRow.OrderDetailStatus = 0;
            table.Rows.InsertAt(drNewRow, 0);
            return drNewRow;
        }

        /// <summary>
        /// Thêm mới hóa đơn
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        public DictionaryDataSet InitSAInvoice(DictionaryDataSet ds)
        {
            var orderRow = ds.Order.FirstOrDefault();
            DictionaryDataSet.SAInvoiceRow masterRow = ds.SAInvoice.NewSAInvoiceRow();
            if (orderRow == null)
            {
                var saRow = ds.SAInvoice.FirstOrDefault();
                masterRow.RefID = Guid.NewGuid();
                masterRow.RefNo = saRow.RefNo;
                masterRow.RefDate = DateTime.Now;
                masterRow.OrderID = saRow.OrderID;
                masterRow.CustomerID = saRow.CustomerID;
                masterRow.EmployeeID = Session.UserLogin.EmployeeID;
                masterRow.CreatedDate = DateTime.Now;
                masterRow.CreatedBy = Session.UserLogin.UserName;
                masterRow.ModifiedDate = DateTime.Now;
                masterRow.ModifiedBy = Session.UserLogin.UserName;
                //var table = new DL.DLTableMapping().GetTableMappingDetailByTableID(saRow., saRow.RefDate);
                //if (table != null)
                //    if (table.Rows.Count > 0)
                //    {
                //        var row = table.Rows[0];
                //        masterRow.JournalMemo = row["TableName"] + " - " + row["AreaName"];
                //    }
                // Gán tổng tiền mua trước
                masterRow.TotalSaleAmount = 0;
                masterRow.TotalDiscountAmount = 0;

                BLInventoryItem oBLInventoryItem = new BLInventoryItem();
                // Duyệt chi tiết
                foreach (DictionaryDataSet.OrderDetailRow orderDetailRow in ds.OrderDetail)
                {
                    // Tìm xem có dòng nào trùng Mặt hàng thì tăng số lượng thôi
                    DictionaryDataSet.SAInvoiceDetailRow findRow = ds.SAInvoiceDetail.FirstOrDefault((t) => t.ItemID == orderDetailRow.InventoryItemID);
                    if (findRow != null)
                    {
                        findRow.Quantity += orderDetailRow.Quantity;
                        findRow.SaleAmount += orderDetailRow.Quantity * findRow.UnitPrice;
                        masterRow.TotalSaleAmount += orderDetailRow.Quantity * findRow.UnitPrice;
                    }
                    // Chưa có thì thêm mới vào hóa đơn
                    else
                    {
                        DictionaryDataSet.SAInvoiceDetailRow detailRow = ds.SAInvoiceDetail.NewSAInvoiceDetailRow();
                        detailRow.RefDetailID = Guid.NewGuid();
                        detailRow.RefID = masterRow.RefID;
                        detailRow.ItemID = orderDetailRow.InventoryItemID;
                        detailRow.Quantity = orderDetailRow.Quantity;
                        var inventory = oBLInventoryItem.GetByID(orderDetailRow.InventoryItemID);
                        if (ds.InventoryItem.FindByInventoryItemID(inventory.InventoryItemID) == null)
                            ds.InventoryItem.Merge(oBLInventoryItem.GetByID_SAInvoice(orderDetailRow.InventoryItemID));
                        detailRow.UnitPrice = inventory != null ? inventory.UnitPrice : 0;
                        detailRow.SaleAmount = detailRow.Quantity * detailRow.UnitPrice;
                        masterRow.TotalSaleAmount += detailRow.SaleAmount;
                        detailRow.DiscountRate = 0;
                        detailRow.DiscountAmount = 0;
                        detailRow.OrderDetailID = orderDetailRow.OrderDetailID;
                        detailRow.SortOrder = ds.SAInvoiceDetail.Count + 1;
                        ds.SAInvoiceDetail.Rows.InsertAt(detailRow, ds.SAInvoiceDetail.Rows.Count);
                    }
                }

                masterRow.TotalDiscountAmount = 0;
                masterRow.ServiceRate = 0;
                masterRow.ServiceAmount = masterRow.TotalSaleAmount * masterRow.ServiceRate;
                masterRow.TotalAmount = masterRow.TotalSaleAmount - masterRow.TotalDiscountAmount + masterRow.ServiceAmount;
                masterRow.VATRate = 0;
                masterRow.VATAmount = masterRow.TotalAmount * masterRow.VATRate;
                masterRow.PaymentAmount = masterRow.TotalAmount + masterRow.VATAmount;
                masterRow.ReceiveAmount = 0;
                masterRow.ReturnAmount = 0;
                masterRow.PaymentStatus = 0;
                masterRow.IsApplyTaxWhenRequire = false;

                ds.SAInvoice.Rows.InsertAt(masterRow, 0);

                return ds;
            }
            else
            {
                masterRow.RefID = Guid.NewGuid();
                masterRow.RefNo = orderRow.OrderNo;
                masterRow.RefDate = DateTime.Now;
                masterRow.OrderID = orderRow.OrderID;
                masterRow.CustomerID = orderRow.CustomerID;
                masterRow.EmployeeID = Session.UserLogin.EmployeeID;
                masterRow.CreatedDate = DateTime.Now;
                masterRow.CreatedBy = Session.UserLogin.UserName;
                masterRow.ModifiedDate = DateTime.Now;
                masterRow.ModifiedBy = Session.UserLogin.UserName;
                var table = new DL.DLTableMapping().GetTableMappingDetailByTableID(orderRow.TableID, orderRow.OrderDate);
                if (table != null)
                    if (table.Rows.Count > 0)
                    {
                        var row = table.Rows[0];
                        masterRow.JournalMemo = row["TableName"] + " - " + row["AreaName"];
                    }
                // Gán tổng tiền mua trước
                masterRow.TotalSaleAmount = 0;
                masterRow.TotalDiscountAmount = 0;

                BLInventoryItem oBLInventoryItem = new BLInventoryItem();
                // Duyệt chi tiết
                foreach (DictionaryDataSet.OrderDetailRow orderDetailRow in ds.OrderDetail)
                {
                    // Tìm xem có dòng nào trùng Mặt hàng thì tăng số lượng thôi
                    DictionaryDataSet.SAInvoiceDetailRow findRow = ds.SAInvoiceDetail.FirstOrDefault((t) => t.ItemID == orderDetailRow.InventoryItemID);
                    if (findRow != null)
                    {
                        findRow.Quantity += orderDetailRow.Quantity;
                        findRow.SaleAmount += orderDetailRow.Quantity * findRow.UnitPrice;
                        masterRow.TotalSaleAmount += orderDetailRow.Quantity * findRow.UnitPrice;
                    }
                    // Chưa có thì thêm mới vào hóa đơn
                    else
                    {
                        DictionaryDataSet.SAInvoiceDetailRow detailRow = ds.SAInvoiceDetail.NewSAInvoiceDetailRow();
                        detailRow.RefDetailID = Guid.NewGuid();
                        detailRow.RefID = masterRow.RefID;
                        detailRow.ItemID = orderDetailRow.InventoryItemID;
                        detailRow.Quantity = orderDetailRow.Quantity;
                        var inventory = oBLInventoryItem.GetByID(orderDetailRow.InventoryItemID);
                        if (ds.InventoryItem.FindByInventoryItemID(inventory.InventoryItemID) == null)
                            ds.InventoryItem.Merge(oBLInventoryItem.GetByID_SAInvoice(orderDetailRow.InventoryItemID));
                        detailRow.UnitPrice = inventory != null ? inventory.UnitPrice : 0;
                        detailRow.SaleAmount = detailRow.Quantity * detailRow.UnitPrice;
                        masterRow.TotalSaleAmount += detailRow.SaleAmount;
                        detailRow.DiscountRate = 0;
                        detailRow.DiscountAmount = 0;
                        detailRow.OrderDetailID = orderDetailRow.OrderDetailID;
                        detailRow.SortOrder = ds.SAInvoiceDetail.Count + 1;
                        ds.SAInvoiceDetail.Rows.InsertAt(detailRow, ds.SAInvoiceDetail.Rows.Count);
                    }
                }

                masterRow.TotalDiscountAmount = 0;
                masterRow.ServiceRate = 0;
                masterRow.ServiceAmount = masterRow.TotalSaleAmount * masterRow.ServiceRate;
                masterRow.TotalAmount = masterRow.TotalSaleAmount - masterRow.TotalDiscountAmount + masterRow.ServiceAmount;
                masterRow.VATRate = 0;
                masterRow.VATAmount = masterRow.TotalAmount * masterRow.VATRate;
                masterRow.PaymentAmount = masterRow.TotalAmount + masterRow.VATAmount;
                masterRow.ReceiveAmount = 0;
                masterRow.ReturnAmount = 0;
                masterRow.PaymentStatus = 0;
                masterRow.IsApplyTaxWhenRequire = false;

                ds.SAInvoice.Rows.InsertAt(masterRow, 0);

                return ds;
            }
            
           
        }

        /// <summary>
        /// Lấy ra danh sách Hóa đơn theo thời gian
        /// </summary>
        /// <typeparam name="T">Kiểu đối tượng</typeparam>
        /// <returns></returns>
        public DataTable GetSAInvoice_Today(DateTime today)
        {
            return new DL.DLSAInvoice().GetSAInvoice_Today(today == default(DateTime) ? DateTime.Now : today);
        }

        /// <summary>
        /// Lấy ra Hóa đơn theo ID
        /// </summary>
        /// <typeparam name="T">Kiểu đối tượng</typeparam>
        /// <returns></returns>
        public DataTable GetSAInvoice_Today_ByID(Guid id)
        {
            return new DL.DLSAInvoice().GetSAInvoice_Today_ByID(id);
        }
        /// <summary>
        /// Lấy ra Hóa đơn theo ID
        /// </summary>
        /// <typeparam name="T">Kiểu đối tượng</typeparam>
        /// <returns></returns>
        public DataTable UpdateInvoiceNumber(Guid id,string invNo, string TransactionID)
        {
            return new DL.DLSAInvoice().UpdateInvoiceNumber(id,invNo, TransactionID);
        }
        
        /// <summary>
        /// Lấy ra Hóa đơn theo ID
        /// </summary>
        /// <typeparam name="T">Kiểu đối tượng</typeparam>
        /// <returns></returns>
        public DataTable GetSAInvoiceDetailAll(Guid id)
        {
            return new DL.DLSAInvoice().GetSAInvoiceDetailAll(id);
        }
        /// <summary>
        /// Lấy ra Hóa đơn theo ID
        /// </summary>
        /// <typeparam name="T">Kiểu đối tượng</typeparam>
        /// <returns></returns>
        public string GetSAInvoiceTransactionID(Guid id)
        {
            return new DL.DLSAInvoice().GetSAInvoiceTransactionID(id);
        }
    }
}
