using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Desktop.Entity;

namespace Desktop.BL
{
    public class BLOrder : BLBase
    {
        public BLOrder() : base()
        {
            TableMasterName = "Order";
            TableDetailName = "OrderDetail";
            oDL = new DL.DLOrder();
        }

        public DataTable GetOrderByTime(DateTime today)
        {
            return new DL.DLOrder().GetOrderByTime(today);
        }

        public DataRow InitNewRow(DictionaryDataSet.OrderDataTable table)
        {
            DictionaryDataSet.OrderRow drNewRow = table.NewOrderRow();
            drNewRow.OrderID = Guid.NewGuid();
            drNewRow.OrderNo = DateTime.Now.Year.ToString() + "." + GetNumberOrder(DateTime.Now.Year);
            drNewRow.OrderDate = DateTime.Now;
            drNewRow.OrderStatus = 0;
            drNewRow.NumberOfPeople = 1;
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
        /// Lấy ra số Order tiếp theo trong năm
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        private string GetNumberOrder(int year)
        {
            return new DL.DLOrder().GetNumberOrder(year);
        }

        /// <summary>
        /// Kiểm tra xem chi tiết đã gửi bếp chưa
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        public bool CheckSendKitchenOrderDetail(Guid orderDetailID)
        {
            return new DL.DLOrder().CheckSendKitchenOrderDetail(orderDetailID);
        }

        /// <summary>
        /// Kiểm tra order đã gửi bếp chưa
        /// </summary>
        /// <param name="orderID"></param>
        /// <returns></returns>
        public bool CheckSendKitchenOrder(Guid orderID)
        {
            return new DL.DLOrder().CheckSendKitchenOrder(orderID);
        }

        /// <summary>
        /// Kiểm tra OrderNo có trùng không
        /// <para>true - đã tồn tại || false - chưa tồn tại</para>
        /// </summary>
        /// <param name="id"></param>
        /// <param name="orderNo"></param>
        /// <returns></returns>
        public bool CheckCodeIsExists(Guid id, string orderNo)
        {
            return new DL.DLOrder().CheckCodeIsExists(id, orderNo);
        }

        /// <summary>
        /// Lấy ra bảng chi tiết theo OrderID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DataTable GetDetailByID(Guid id)
        {
            return new DL.DLOrder().GetDetailByID(id);
        }

        /// <summary>
        /// Lấy ra danh sách Order để View
        /// </summary>
        /// <param name="today"></param>
        /// <returns></returns>
        public DataTable GetOrderByOrderDate(DateTime today)
        {
            return new DL.DLOrder().GetOrderByOrderDate(today);
        }

        public bool ChangeOrderStatus(Guid id, int status)
        {
            return new DL.DLOrder().ChangeOrderStatus(id, status);
        }
    }
}
