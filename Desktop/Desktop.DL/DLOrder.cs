using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop.DL
{
    public class DLOrder : DLBase
    {
        public DLOrder()
        {
            TableName = "Order";
            ObjectIDParam = "OrderID";
            ObjectIDParamDetail = "OrderDetailID";
            InitStored();
        }

        /// <summary>
        /// Lấy ra danh sách Order theo thời gian
        /// </summary>
        /// <typeparam name="T">Kiểu đối tượng</typeparam>
        /// <param name="objectID">ID của đối tượng</param>
        /// <returns></returns>
        public DataTable GetOrderByTime(DateTime today)
        {
            var table = new DataTable();
            using (var sqlAdapter = new SqlDataAdapter())
            {
                using (var sqlCommand = CreateSqlCommand("Proc_GetOrderByTime"))
                {
                    sqlCommand.Parameters.AddWithValue("Today", today);
                    sqlAdapter.SelectCommand = sqlCommand;
                    sqlAdapter.Fill(table);
                    sqlCommand.Connection.Close();
                }
                return table;
            }
        }

        /// <summary>
        /// Lấy ra số Order tiếp theo trong năm
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        public string GetNumberOrder(int year)
        {
            using (var sqlAdapter = new SqlDataAdapter())
            {
                using (var sqlCommand = CreateSqlCommand("Proc_GetNumberOrder"))
                {
                    sqlCommand.Parameters.AddWithValue("Year", year);
                    sqlAdapter.SelectCommand = sqlCommand;
                    string result = sqlCommand.ExecuteScalar().ToString();
                    sqlCommand.Connection.Close();
                    return result;
                }
            }
        }

        /// <summary>
        /// Lấy ra bảng chi tiết theo OrderID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DataTable GetDetailByID(Guid id)
        {
            var table = new DataTable();
            using (var sqlAdapter = new SqlDataAdapter())
            {
                using (var sqlCommand = CreateSqlCommand("Proc_GetOrderDetailByOrderIDAll"))
                {
                    sqlCommand.Parameters.AddWithValue("OrderID", id);
                    sqlAdapter.SelectCommand = sqlCommand;
                    sqlAdapter.Fill(table);
                    sqlCommand.Connection.Close();
                }
                return table;
            }
        }

        /// <summary>
        /// Kiểm tra xem đã gửi bếp chưa
        /// <para>true - đã gửi || false - chưa gửi</para>
        /// </summary>
        /// <param name="orderDetailID"></param>
        /// <returns></returns>
        public bool CheckSendKitchenOrderDetail(Guid orderDetailID)
        {
            using (var sqlAdapter = new SqlDataAdapter())
            {
                using (var sqlCommand = CreateSqlCommand("Proc_CheckSendKitchen"))
                {
                    sqlCommand.Parameters.AddWithValue("OrderDetailID", orderDetailID);
                    sqlAdapter.SelectCommand = sqlCommand;
                    string result = sqlCommand.ExecuteScalar().ToString();
                    sqlCommand.Connection.Close();
                    return result == "1";
                }
            }
        }

        /// <summary>
        /// Kiểm tra xem đã gửi bếp chưa
        /// <para>true - đã gửi || false - chưa gửi</para>
        /// </summary>
        /// <param name="orderID"></param>
        /// <returns></returns>
        public bool CheckSendKitchenOrder(Guid orderID)
        {
            using (var sqlAdapter = new SqlDataAdapter())
            {
                using (var sqlCommand = CreateSqlCommand("Proc_CheckOrderAlreadySendKitchen"))
                {
                    sqlCommand.Parameters.AddWithValue("OrderID", orderID);
                    sqlAdapter.SelectCommand = sqlCommand;
                    string result = sqlCommand.ExecuteScalar().ToString();
                    sqlCommand.Connection.Close();
                    return result == "1";
                }
            }
        }

        /// <summary>
        /// Thay đổi trạng thái Order
        /// </summary>
        /// <param name="id"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public bool ChangeOrderStatus(Guid id, int status)
        {
            using (var sqlAdapter = new SqlDataAdapter())
            {
                using (var sqlCommand = CreateSqlCommand("Proc_ChangeOrderStatus"))
                {
                    sqlCommand.Parameters.AddWithValue("OrderID", id);
                    sqlCommand.Parameters.AddWithValue("OrderStatus", status);
                    sqlAdapter.SelectCommand = sqlCommand;
                    var result = sqlCommand.ExecuteNonQuery();
                    sqlCommand.Connection.Close();
                    return result > 0;
                }
            }
        }

        /// <summary>
        /// Kiểm tra OrderNo có hợp lệ không
        /// <para>true - đã tồn tại || false - chưa tồn tại</para>
        /// </summary>
        /// <returns></returns>
        public bool CheckCodeIsExists(Guid id, string orderNo)
        {
            using (var sqlAdapter = new SqlDataAdapter())
            {
                using (var sqlCommand = CreateSqlCommand("Proc_CheckExistsOrderNo"))
                {
                    sqlCommand.Parameters.AddWithValue("OrderID", id);
                    sqlCommand.Parameters.AddWithValue("OrderNo", orderNo);
                    sqlAdapter.SelectCommand = sqlCommand;
                    string result = sqlCommand.ExecuteScalar().ToString();
                    sqlCommand.Connection.Close();
                    return result == "1";
                }
            }
        }

        /// <summary>
        /// Lấy ra danh sách Order để View
        /// </summary>
        /// <param name="today"></param>
        /// <returns></returns>
        public DataTable GetOrderByOrderDate(DateTime today)
        {
            var table = new DataTable();
            using (var sqlAdapter = new SqlDataAdapter())
            {
                using (var sqlCommand = CreateSqlCommand("Proc_GetListOrderByOrderDate"))
                {
                    sqlCommand.Parameters.AddWithValue("Today", today);
                    sqlAdapter.SelectCommand = sqlCommand;
                    sqlAdapter.Fill(table);
                    sqlCommand.Connection.Close();
                }
                return table;
            }
        }
    }
}
