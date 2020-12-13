using Desktop.Entity;
using Desktop.Lib;
using QuizBit.Contract;
using QuizBit.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Desktop.DL
{
    public class DLInventoryItem : DLBase
    {
        private string token;

        public string Token
        {
            get { return token; }
            set { token = value; headerParameter.Add(DLKey.Authorization, Token); }
        }

        private Dictionary<string, string> headerParameter;

        public Dictionary<string, string> HeaderParameter
        {
            get { return headerParameter; }
            set { headerParameter = value; }
        }

        public DLInventoryItem(string token)
        {
            HeaderParameter = new Dictionary<string, string>();
            Token = token;
        }

        public DataTable Get()
        {
            ServiceResult<List<InventoryItem>> list = CloudServiceFactory<List<InventoryItem>>.ExecuteFunction("InventoryItem/GetList", null, HeaderParameter, "GET");
            if (list.Success && list.Data != null)
                return CommonFunction.ConvertToDataTable(list.Data);
            else return null;
        }

        public int InsertUpdate(DictionaryDataSet.InventoryItemRow drObjectChange, Guid oldID)
        {
            ServiceResult result = CloudServiceFactory.ExecuteFunction("InventoryItem/InsertUpdate", new InsertUpdateParameter<InventoryItem>(oldID, CommonFunction.GetItem<InventoryItem>(drObjectChange)), HeaderParameter);
            if (result.Success == false)
            {
                if (result.ErrorCode == ErrorCode.DuplicateCode)
                    return 2;
                return 0;
            }
            return 1;
        }

        public bool CheckCodeExists(DictionaryDataSet.InventoryItemRow drObjectChange)
        {
            ServiceResult result = CloudServiceFactory.ExecuteFunction("InventoryItem/CheckCodeExists", CommonFunction.GetItem<InventoryItem>(drObjectChange), HeaderParameter);
            return result.Success;
        }

        public int Delete(Guid id)
        {
            ServiceResult result = CloudServiceFactory.ExecuteFunction("InventoryItem/Delete", id.ToString(), HeaderParameter);
            if (result.Success == false)
            {
                if (result.ErrorCode == ErrorCode.ItemWasUsed)
                    return 2;
                return 0;
            }
            return 1;
        }

        public bool CheckBeforeDelete(Guid id)
        {
            ServiceResult result = CloudServiceFactory.ExecuteFunction("InventoryItem/CheckBeforeDelete", id.ToString(), HeaderParameter);
            return result.Success;
        }

        public DictionaryDataSet.InventoryItemRow GetByID(Guid objectID)
        {
            var table = new DataTable();
            using (var sqlAdapter = new SqlDataAdapter())
            {
                using (var sqlCommand = CreateSqlCommand("Proc_GetInventoryItemByID"))
                {
                    sqlCommand.Parameters.AddWithValue("InventoryItemID", objectID);
                    sqlAdapter.SelectCommand = sqlCommand;
                    sqlAdapter.Fill(table);
                    sqlCommand.Connection.Close();
                }
            }
            if (table != null && table.Rows.Count > 0)
            {
                var ds = new DictionaryDataSet();
                ds.InventoryItem.Merge(table);
                DictionaryDataSet.InventoryItemRow result = (DictionaryDataSet.InventoryItemRow)ds.InventoryItem[0];
                return result;
            }
            return null;
        }

        public DataTable GetByID_SAInvoice(Guid objectID)
        {
            var table = new DataTable();
            using (var sqlAdapter = new SqlDataAdapter())
            {
                using (var sqlCommand = CreateSqlCommand("Proc_GetInventoryItemByID"))
                {
                    sqlCommand.Parameters.AddWithValue("InventoryItemID", objectID);
                    sqlAdapter.SelectCommand = sqlCommand;
                    sqlAdapter.Fill(table);
                    sqlCommand.Connection.Close();
                }
            }
            return table;
        }
    }
}
