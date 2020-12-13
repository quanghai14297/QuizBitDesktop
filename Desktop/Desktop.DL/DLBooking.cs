using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop.DL
{
    public class DLBooking : DLBase
    {
        public DLBooking()
        {
            TableName = "Booking";
            ObjectIDParam = "@BookingID";
            InitStored();
        }

        /// <summary>
        /// Lấy ra danh sách đặt bàn theo ngày
        /// </summary>
        /// <param name="fromTime">Thời gian lấy dữ liệu</param>
        /// <returns></returns>
        public DataTable GetBookingByFromTime(DateTime fromTime)
        {
            var table = new DataTable();
            using (var sqlAdapter = new SqlDataAdapter())
            {
                using (var sqlCommand = CreateSqlCommand("Proc_GetBookingByFromTime"))
                {
                    sqlCommand.Parameters.AddWithValue("Today", fromTime);
                    sqlAdapter.SelectCommand = sqlCommand;
                    sqlAdapter.Fill(table);
                    sqlCommand.Connection.Close();
                }
                return table;
            }
        }

        /// <summary>
        /// Lấy ra số đặt bàn tiếp theo trong năm
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        public string GetNumberBooking(int year)
        {
            using (var sqlAdapter = new SqlDataAdapter())
            {
                using (var sqlCommand = CreateSqlCommand("Proc_GetNumberBooking"))
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
        /// Thay đổi trạng thái đặt bàn
        /// </summary>
        /// <param name="bookingID"></param>
        /// <param name="bookingStatus"></param>
        /// <returns></returns>
        public bool ChangeBookingStatus(Guid bookingID, int bookingStatus)
        {
            string query = "UPDATE dbo.Booking SET BookingStatus = " + bookingStatus + " WHERE BookingID = '" + bookingID + "'";
            using (var sqlCommand = CreateSqlCommand())
            {
                sqlCommand.CommandText = query;
                return sqlCommand.ExecuteNonQuery() > 0;
            }
        }
        /// <summary>
        /// Kiểm tra BookingNo có hợp lệ không
        /// <para>true - đã tồn tại || false - chưa tồn tại</para>
        /// </summary>
        /// <returns></returns>
        public bool CheckCodeIsExists(Guid id, string bookingNo)
        {
            using (var sqlAdapter = new SqlDataAdapter())
            {
                using (var sqlCommand = CreateSqlCommand("Proc_CheckExistsBookingNo"))
                {
                    sqlCommand.Parameters.AddWithValue("BookingID", id);
                    sqlCommand.Parameters.AddWithValue("BookingNo", bookingNo);
                    sqlAdapter.SelectCommand = sqlCommand;
                    string result = sqlCommand.ExecuteScalar().ToString();
                    sqlCommand.Connection.Close();
                    return result == "1";
                }
            }
        }
    }
}
