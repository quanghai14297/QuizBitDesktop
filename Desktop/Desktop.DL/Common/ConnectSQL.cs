using System.Configuration;
using System.Data.SqlClient;
using QuizBit.Contract;

namespace Desktop.DL
{
    /// <summary>
    /// Lớp kết nối với CSDL
    /// </summary>
    public static class ConnectSQL
    {
        #region Declaration

        /// <summary>
        /// SQLConnection kết nối CSDL
        /// </summary>
        /// <remarks></remarks>
        /// Created by khanhjm - 29/08/2019
        private static SqlConnection _sqlConnection = null;

        #endregion

        #region Property

        #endregion

        #region Function

        /// <summary>
        /// Lấy ConnectionString trong Web.config
        /// </summary>
        /// <returns></returns>
        public static string GetConnectionString()
        {
            string result = ConfigurationManager.ConnectionStrings[CommonKey.Connect_SQL_Cloud].ConnectionString;
#if DEBUG

#endif
            return result;
        }

        /// <summary>
        /// Kết nối với SQL
        /// </summary>
        /// <returns></returns>
        public static SqlConnection GetConnection()
        {
            if (_sqlConnection == null)
            {
                _sqlConnection = new SqlConnection(GetConnectionString());
            }
            return _sqlConnection;
        }

        /// <summary>
        /// Đóng kết nối với SQL
        /// </summary>
        public static void CloseConnection()
        {
            if (_sqlConnection == null)
            {
                _sqlConnection.Close();
            }
        }

        #endregion
    }
}