using QuizBit.Contract;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop.DL
{
    public class DLTableMapping : DLBase
    {
        /// <summary>
        /// Lấy ra danh sách bàn theo Khu vực
        /// </summary>
        /// <typeparam name="T">Kiểu đối tượng</typeparam>
        /// <param name="objectID">ID của đối tượng</param>
        /// <returns></returns>
        public DataTable GetTableMappingByAreaID(Guid objectID, DateTime today)
        {
            var table = new DataTable();
            using (var sqlAdapter = new SqlDataAdapter())
            {
                using (var sqlCommand = CreateSqlCommand("Proc_GetTableMappingByAreaID"))
                {
                    sqlCommand.Parameters.AddWithValue("AreaID", objectID);
                    sqlCommand.Parameters.AddWithValue("Today", today);
                    sqlAdapter.SelectCommand = sqlCommand;
                    sqlAdapter.Fill(table);
                    sqlCommand.Connection.Close();
                }
                return table;
            }
        }

        /// <summary>
        /// Lấy ra danh sách bàn theo Khu vực của ID bàn truyền vào
        /// </summary>
        /// <typeparam name="T">Kiểu đối tượng</typeparam>
        /// <param name="objectID">ID của đối tượng</param>
        /// <returns></returns>
        public DataTable GetTableMappingByTableID(Guid objectID, DateTime today)
        {
            var table = new DataTable();
            using (var sqlAdapter = new SqlDataAdapter())
            {
                using (var sqlCommand = CreateSqlCommand("Proc_GetTableMappingByTableID"))
                {
                    sqlCommand.Parameters.AddWithValue("TableID", objectID);
                    sqlCommand.Parameters.AddWithValue("Today", today);
                    sqlAdapter.SelectCommand = sqlCommand;
                    sqlAdapter.Fill(table);
                    sqlCommand.Connection.Close();
                }
                return table;
            }
        }

        /// <summary>
        /// Lấy ra chi tiết bàn
        /// </summary>
        /// <typeparam name="T">Kiểu đối tượng</typeparam>
        /// <param name="objectID">ID của đối tượng</param>
        /// <returns></returns>
        public DataTable GetTableMappingDetailByTableID(Guid objectID, DateTime today)
        {
            var table = new DataTable();
            using (var sqlAdapter = new SqlDataAdapter())
            {
                using (var sqlCommand = CreateSqlCommand("Proc_GetTableMappingDetailByTableID"))
                {
                    sqlCommand.Parameters.AddWithValue("TableID", objectID);
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
