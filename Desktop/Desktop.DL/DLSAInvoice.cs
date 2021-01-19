using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop.DL
{
    public class DLSAInvoice : DLBase
    {
        public DLSAInvoice()
        {
            TableName = "SAInvoice";
            ObjectIDParam = "RefID";
            ObjectIDParamDetail = "RefDetailID";
            InitStored();
        }

        /// <summary>
        /// Lấy ra danh sách Hóa đơn theo thời gian
        /// </summary>
        /// <typeparam name="T">Kiểu đối tượng</typeparam>
        /// <returns></returns>
        public DataTable GetSAInvoice_Today(DateTime today)
        {
            var table = new DataTable();
            using (var sqlAdapter = new SqlDataAdapter())
            {
                using (var sqlCommand = CreateSqlCommand("Proc_GetSAInvoice_Today"))
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
        /// Lấy ra danh sách Hóa đơn theo thời gian
        /// </summary>
        /// <typeparam name="T">Kiểu đối tượng</typeparam>
        /// <returns></returns>
        public DataTable UpdateInvoiceNumber(Guid id, string invNo, string TransactionID)
        {
            var table = new DataTable();
            using (var sqlAdapter = new SqlDataAdapter())
            {
                using (var sqlCommand = CreateSqlCommand("Proc_UpdateInvoiceNumber"))
                {
                    sqlCommand.Parameters.AddWithValue("@RefID", id);
                    sqlCommand.Parameters.AddWithValue("@invNo", invNo);
                    sqlCommand.Parameters.AddWithValue("@TransactionID", TransactionID);
                    sqlAdapter.SelectCommand = sqlCommand;
                    sqlAdapter.Fill(table);
                    sqlCommand.Connection.Close();
                }
                return table;
            }
        }
        /// <summary>
        /// Lấy ra danh sách Hóa đơn theo thời gian
        /// </summary>
        /// <typeparam name="T">Kiểu đối tượng</typeparam>
        /// <returns></returns>
        public DataTable GetSAInvoice_Today_ByID(Guid id)
        {
            var table = new DataTable();
            using (var sqlAdapter = new SqlDataAdapter())
            {
                using (var sqlCommand = CreateSqlCommand("Proc_GetSAInvoice_Today_ByID"))
                {
                    sqlCommand.Parameters.AddWithValue("RefID", id);
                    sqlAdapter.SelectCommand = sqlCommand;
                    sqlAdapter.Fill(table);
                    sqlCommand.Connection.Close();
                }
                return table;
            }
        }
        /// <summary>
        /// Lấy ra danh sách Hóa đơn theo thời gian
        /// </summary>
        /// <typeparam name="T">Kiểu đối tượng</typeparam>
        /// <returns></returns>
        public DataTable GetSAInvoiceDetailAll(Guid id)
        {
            var table = new DataTable();
            using (var sqlAdapter = new SqlDataAdapter())
            {
                using (var sqlCommand = CreateSqlCommand("Proc_GetSAInvoiceDetailAll"))
                {
                    sqlCommand.Parameters.AddWithValue("RefID", id);
                    sqlAdapter.SelectCommand = sqlCommand;
                    sqlAdapter.Fill(table);
                    sqlCommand.Connection.Close();
                }
                return table;
            }
        }
        /// <summary>
        /// Lấy ra danh sách Hóa đơn theo thời gian
        /// </summary>
        /// <typeparam name="T">Kiểu đối tượng</typeparam>
        /// <returns></returns>
        public string GetSAInvoiceTransactionID(Guid id)
        {
            var table = new DataTable();
            string result;
            using (var sqlAdapter = new SqlDataAdapter())
            {
                using (var sqlCommand = CreateSqlCommand("Proc_GetSAInvoiceTransactionID"))
                {
                    sqlCommand.Parameters.AddWithValue("RefID", id);
                    result=sqlCommand.ExecuteScalar().ToString();
                    sqlCommand.Connection.Close();
                }
                return result;
            }
        }
        

    }
}
