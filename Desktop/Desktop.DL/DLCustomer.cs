using Desktop.Entity;
using Desktop.Lib;
using QuizBit.Contract;
using QuizBit.Entity;
using System;
using System.Collections.Generic;
using System.Data;

namespace Desktop.DL
{
    public class DLCustomer : DLBase
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

        public DLCustomer(string token)
        {
            TableName = "Customer";
            ObjectIDParam = "CustomerID";
            HeaderParameter = new Dictionary<string, string>();
            Token = token;
            InitStored();
        }

        public DataTable Get()
        {
            ServiceResult<List<Customer>> list = CloudServiceFactory<List<Customer>>.ExecuteFunction("Customer/getlist", null, HeaderParameter, "GET");
            if (list.Success && list.Data != null)
                return CommonFunction.ConvertToDataTable(list.Data);
            else return null;
        }

        public int InsertUpdate(DictionaryDataSet.CustomerRow drObjectChange)
        {
            ServiceResult result = CloudServiceFactory.ExecuteFunction("Customer/InsertUpdate", CommonFunction.GetItem<Customer>(drObjectChange), HeaderParameter);
            if (result.Success == false)
            {
                if (result.ErrorCode == ErrorCode.DuplicateCode)
                    return 2;
                return 0;
            }
            return 1;
        }

        public bool CheckCodeExists(DictionaryDataSet.CustomerRow drObjectChange)
        {
            ServiceResult result = CloudServiceFactory.ExecuteFunction("Customer/CheckCodeExists", CommonFunction.GetItem<Customer>(drObjectChange), HeaderParameter);
            return result.Success;
        }

        public int Delete(Guid id)
        {
            ServiceResult result = CloudServiceFactory.ExecuteFunction("Customer/Delete", id.ToString(), HeaderParameter);
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
            ServiceResult result = CloudServiceFactory.ExecuteFunction("Customer/checkbeforedelete", id.ToString(), HeaderParameter);
            return result.Success;
        }
    }
}
