using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using QuizBit.Contract;
using System.Data.Common;

namespace Desktop.DL
{
    public class DLBase
    {
        #region Declaration
        public static string IS_EXISTS = "1";

        /// <summary>
        /// Tên bảng
        /// </summary>
        protected string TableName = "";

        /// <summary>
        /// ID name của đối tượng
        /// </summary>
        protected string ObjectIDParam = "";

        /// <summary>
        /// ID name của đối tượng
        /// </summary>
        protected string ObjectIDParamDetail = "";

        /// <summary>
        /// Tên thủ tục Lấy dữ liệu
        /// </summary>
        protected string Stored_Get = "Proc_Get";

        /// <summary>
        /// Tên thủ tục Lấy dữ liệu by ID
        /// </summary>
        protected string Stored_Get_By_ID = "";

        /// <summary>
        /// Tên thủ tục Lấy dữ liệu by ID
        /// </summary>
        protected string Stored_GetDetail_By_ID = "";

        /// <summary>
        /// Tên thủ tục Lấy toàn bộ dữ liệu
        /// </summary>
        protected string Stored_GetAll = "Proc_Get";

        /// <summary>
        /// Tên thủ tục Lấy dữ liệu theo Code
        /// </summary>
        protected string Stored_GetByCode = "Proc_GetByCode";

        /// <summary>
        /// Tên thủ tục Thêm-Cập nhật dữ liệu master
        /// </summary>
        protected string Stored_InsertUpdate = "Proc_InsertUpdate";

        /// <summary>
        /// Tên thủ tục Xóa dữ liệu master
        /// </summary>
        protected string Stored_Delete = "Proc_Delete";

        /// <summary>
        /// Thủ tục kiểm tra dữ liệu đã là khóa ngoại không
        /// </summary>
        protected string Stored_CheckBeforeDelete = "Proc_CheckBeforeDelete";

        /// <summary>
        /// Tên thủ tục lấy ra dữ liệu FK
        /// </summary>
        protected string Stored_Get_FK = "Proc_Get_FK_";

        /// <summary>
        /// Tên thủ tục lấy ra dữ liệu kiểm tra trùng
        /// </summary>
        protected string Stored_Get_Unique = "Proc_GetUnique_";

        /// <summary>
        /// Tên ID - Khóa chính
        /// </summary>
        protected string Param_ObjectID = "";

        /// <summary>
        /// Bắt đầu của Param Procedure
        /// </summary>
        protected string ParamToken = "@";

        #endregion

        #region Property

        private Database _db;

        /// <summary>
        /// Đối tượng thao tác với Database
        /// </summary>
        public Database DB
        {
            get
            {
                if (_db == null)
                {
                    _db = CreateDatabaseObject();
                }
                return _db;
            }
            set
            {
                _db = value;
            }
        }

        protected DbConnection Connection
        {
            get
            {
                return DB.CreateConnection();
            }
        }


        #endregion

        #region Function

        /// <summary>
        /// Khởi tạo tên thủ tục trong Database theo Tên bảng
        /// </summary>
        protected void InitStored()
        {
            Stored_Get += TableName;
            Stored_Get_By_ID += Stored_Get + "ByID";
            Stored_GetDetail_By_ID += Stored_Get_By_ID + "_Detail";
            Stored_GetAll += TableName;
            Stored_GetByCode += TableName;
            Stored_Delete += TableName;
            Stored_CheckBeforeDelete += TableName;
            Stored_Get_FK += TableName;
            Stored_Get_Unique += TableName;
            Stored_InsertUpdate += TableName;
        }

        /// <summary>
        /// Khởi tạo SqlCommand
        /// </summary>
        /// <param name="procdureName">Tên Procedure cần dùng</param>
        /// <returns>SqlCommand</returns>
        /// <remarks></remarks>
        protected SqlCommand CreateSqlCommand(string procdureName = "", string commandText = "")
        {
            var sqlCommand = new SqlCommand();
            sqlCommand.Connection = ConnectSQL.GetConnection();
            if (string.IsNullOrEmpty(procdureName))
            {
                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.CommandText = commandText;
            }
            else
            {
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.CommandText = procdureName;
            }
            if (sqlCommand.Connection.State == ConnectionState.Closed) sqlCommand.Connection.Open();
            return sqlCommand;
        }

        /// <summary>
        /// Phương thức lấy danh sách đối tượng
        /// </summary>
        /// <typeparam name="T">Kiểu đối tượng</typeparam>
        /// <returns>danh sách đối tượng</returns>
        public List<T> GetListObject<T>()
        {
            var table = new DataTable();
            using (var sqlAdapter = new SqlDataAdapter())
            {
                using (var sqlCommand = CreateSqlCommand(Stored_Get))
                {
                    sqlAdapter.SelectCommand = sqlCommand;
                    sqlAdapter.Fill(table);
                    sqlCommand.Connection.Close();
                }
                return CommonFunction.ConvertDataTable<T>(table);
            }
        }

        /// <summary>
        /// Phương thức lấy 1 đối tượng dựa vào ID truyền vào
        /// </summary>
        /// <typeparam name="T">Kiểu đối tượng</typeparam>
        /// <param name="objectID">ID của đối tượng</param>
        /// <returns></returns>
        public List<T> GetObjectByID<T, G>(G objectID)
        {
            var table = new DataTable();
            using (var sqlAdapter = new SqlDataAdapter())
            {
                using (var sqlCommand = CreateSqlCommand(Stored_Get_By_ID))
                {
                    sqlCommand.Parameters.AddWithValue(ObjectIDParam, objectID);
                    sqlAdapter.SelectCommand = sqlCommand;
                    sqlAdapter.Fill(table);
                    sqlCommand.Connection.Close();
                }
                return CommonFunction.ConvertDataTable<T>(table);
            }
        }

        /// <summary>
        /// Phương thức lấy 1 đối tượng/1 danh sách đối tượng theo 1 tiêu chí truyền vào
        /// </summary>
        /// <typeparam name="T">Kiểu đối tượng</typeparam>
        /// <typeparam name="T">Kiểu đối tượng điều kiện</typeparam>
        /// <typeparam name="storedName">tên procedure</typeparam>
        /// <param name="condition">tiêu chí </param>
        /// <param name="paramName">tên param trong sql</param>
        /// <returns>danh sách đối tượng </returns>
        public List<T> GetObjectByCondition<T, G>(string storedName, string paramName, G condition)
        {
            var table = new DataTable();
            using (var sqlAdapter = new SqlDataAdapter())
            {
                using (var sqlCommand = CreateSqlCommand(storedName))
                {
                    sqlCommand.Parameters.AddWithValue(paramName, condition);
                    sqlAdapter.SelectCommand = sqlCommand;
                    sqlAdapter.Fill(table);
                    sqlCommand.Connection.Close();
                }
                return CommonFunction.ConvertDataTable<T>(table);
            }
        }

        /// <summary>
        /// Phương thức thêm/cập nhật mới đối tượng
        /// </summary>
        /// <typeparam name="T">loại đối tượng</typeparam>
        /// <param name="obj">đối tượng cần thêm</param>
        /// <returns>thêm/cập nhật đối tượng thành công/thất bại</returns>
        public int InsertUpdateObject<T>(T obj)
        {
            object[] param = CommonFunction.ConvertToParamArray(obj);
            var result = DB.ExecuteNonQuery(Stored_InsertUpdate, param);
            return result;
        }

        /// <summary>
        /// Phương thức xóa 1 đối tượng
        /// </summary>
        /// <param name="objectID"> xóa đối tượng có ID = objectID</param>
        /// <returns>xóa thành công/thất bại</returns>
        public bool DeleteObject<T>(T objectID)
        {
            using (var sqlCommand = CreateSqlCommand(Stored_Delete))
            {
                sqlCommand.Parameters.AddWithValue(ObjectIDParam, objectID);
                int i = sqlCommand.ExecuteNonQuery();
                sqlCommand.Connection.Close();
                return i > 0;
            }
        }

        /// <summary>
        /// Kiểm tra đối tượng đã tồn tại khóa ngoại chưa
        /// </summary>
        /// <param name="objectID"> xóa đối tượng có ID = objectID</param>
        /// <returns>đối tượng đã tồn tại khóa ngoại chưa</returns>
        public bool CheckBeforeDeleteObject<T>(T objectID)
        {
            using (var sqlCommand = CreateSqlCommand(Stored_CheckBeforeDelete))
            {
                sqlCommand.Parameters.AddWithValue(ObjectIDParam, objectID);
                string result = sqlCommand.ExecuteScalar().ToString();
                sqlCommand.Connection.Close();
                return result == IS_EXISTS ? true : false;
            }
        }


        #region Method - Enterprise Library

        /// <summary>
        /// Khởi tạo đối tượng Database để thao tác với dữ liệu
        /// </summary>
        /// <returns></returns>
        protected Database CreateDatabaseObject()
        {
            return new SqlDatabase(ConnectSQL.GetConnectionString());
        }

        /// <summary>
        /// Lấy toàn bộ dữ liệu
        /// </summary>
        /// <param name="storeName">Tên Store</param>
        /// <param name="ds">DataSet kết quả</param>
        /// <param name="tableName">Tên bảng chứa dữ liệu</param>
        protected void GetData(string storeName, DataSet ds, string tableName)
        {
            try
            {
                ds.Tables[tableName].Clear();
                DB.LoadDataSet(Stored_GetAll, ds, new string[] { tableName });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Cập nhật MasterDetail
        /// </summary>
        /// <param name="masterData"></param>
        /// <param name="detailData"></param>
        /// <returns></returns>
        public bool InsertUpdateDataMasterDetail(DataRow masterData, DataTable detailData)
        {
            DbConnection con = Connection;
            DbTransaction ts = null;
            try
            {
                con.Open();
                ts = con.BeginTransaction();
                int iMaster = ExecuteNoneQueryTypedParam(Stored_InsertUpdate, masterData, ts);
                if (iMaster > 0)
                {
                    // Update các dòng Detail
                    UpdateDataTable(detailData.TableName, detailData, ts);
                }
                ts.Commit();
            }
            catch (Exception ex)
            {
                ts.Rollback();
                throw ex;
            }
            finally
            {
                if (Connection != null && con.State != ConnectionState.Closed)
                {
                    con.Close();
                    con.Dispose();
                }
            }
            return true;
        }

        /// <summary>
        /// Cập nhật dữ liệu chính
        /// </summary>
        /// <param name="masterData"></param>
        /// <param name="detailData"></param>
        /// <returns></returns>
        public int InsertUpdate(DataRow masterData)
        {
            DbConnection con = Connection;
            DbTransaction ts = null;
            try
            {
                con.Open();
                ts = con.BeginTransaction();
                int iMaster = ExecuteNoneQueryTypedParam(Stored_InsertUpdate, masterData, ts);
                ts.Commit();
            }
            catch (Exception ex)
            {
                ts.Rollback();
                throw ex;
            }
            finally
            {
                if (Connection != null && con.State != ConnectionState.Closed)
                {
                    con.Close();
                    con.Dispose();
                }
            }
            return 1;
        }

        /// <summary>
        /// Cập nhật dữ liệu DataTable
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="dt"></param>
        /// <param name="ts"></param>
        protected void UpdateDataTable(string tableName, DataTable dt, DbTransaction ts)
        {
            foreach (DataRow oDetail in dt.Rows)
            {
                if (oDetail.RowState == DataRowState.Deleted)
                {
                    ExecuteNonQuery(ts, string.Format("dbo.Proc_Delete{0}", tableName), oDetail[0, DataRowVersion.Original]);
                }
                else
                {
                    ExecuteNoneQueryTypedParam(string.Format("dbo.Proc_InsertUpdate{0}", tableName), oDetail, ts);
                }
            }
        }

        /// <summary>
        /// Thực thi ExecuteNonQuery
        /// </summary>
        /// <param name="ts"></param>
        /// <param name="storedProcedure"></param>
        /// <param name="parameterValues"></param>
        protected int ExecuteNonQuery(DbTransaction ts, string storedProcedure, params object[] parameterValues)
        {
            if (ts != null)
            {
                return DB.ExecuteNonQuery(ts, storedProcedure, parameterValues);
            }
            else
            {
                return DB.ExecuteNonQuery(storedProcedure, parameterValues);
            }
        }

        /// <summary>
        /// ExecuteNoneQueryTypedParam
        /// </summary>
        /// <param name="stored_InsertUpdate"></param>
        /// <param name="data"></param>
        /// <param name="ts"></param>
        /// <returns></returns>
        protected int ExecuteNoneQueryTypedParam(string stored_InsertUpdate, DataRow data, DbTransaction ts)
        {
            DbCommand cmd = DB.GetStoredProcCommand(stored_InsertUpdate);
            DB.DiscoverParameters(cmd);
            foreach (DbParameter pr in cmd.Parameters)
            {
                if (pr.Direction != ParameterDirection.ReturnValue)
                {
                    if (data[pr.ParameterName.Replace(ParamToken, "")] == null)
                    {
                        pr.Value = DBNull.Value;
                    }
                    else
                    {
                        pr.Value = data[pr.ParameterName.Replace(ParamToken, "")];
                    }
                }
            }
            if (ts != null)
            {
                return DB.ExecuteNonQuery(cmd, ts);
            }
            else
            {
                return DB.ExecuteNonQuery(cmd);
            }
        }

        #endregion

        #endregion

        #region Event

        /// <summary>
        /// Lấy ra toàn bộ Master
        /// </summary>
        /// <returns></returns>
        public DataTable GetAll()
        {
            var table = new DataTable();
            using (var sqlAdapter = new SqlDataAdapter())
            {
                using (var sqlCommand = CreateSqlCommand(Stored_GetAll))
                {
                    sqlAdapter.SelectCommand = sqlCommand;
                    sqlAdapter.Fill(table);
                    sqlCommand.Connection.Close();
                }
                return table;
            }
        }

        /// <summary>
        /// Lấy ra Master
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DataTable GetDataByID(Guid id)
        {
            var table = new DataTable();
            using (var sqlAdapter = new SqlDataAdapter())
            {
                using (var sqlCommand = CreateSqlCommand(Stored_Get_By_ID))
                {
                    sqlCommand.Parameters.AddWithValue(ObjectIDParam, id);
                    sqlAdapter.SelectCommand = sqlCommand;
                    sqlAdapter.Fill(table);
                    sqlCommand.Connection.Close();
                }
                return table;
            }
        }

        /// <summary>
        /// Lấy ra Detail
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DataTable GetDataDetailByID(Guid id)
        {
            var table = new DataTable();
            using (var sqlAdapter = new SqlDataAdapter())
            {
                using (var sqlCommand = CreateSqlCommand(Stored_GetDetail_By_ID))
                {
                    sqlCommand.Parameters.AddWithValue(ObjectIDParam, id);
                    sqlAdapter.SelectCommand = sqlCommand;
                    sqlAdapter.Fill(table);
                    sqlCommand.Connection.Close();
                }
                return table;
            }
        }

        #endregion
    }
}
