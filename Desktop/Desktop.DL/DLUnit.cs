using Desktop.Entity;
using Desktop.Lib;
using QuizBit.Contract;
using QuizBit.Entity;
using System;
using System.Collections.Generic;
using System.Data;

namespace Desktop.DL
{
    public class DLUnit
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

        public DLUnit(string token)
        {
            HeaderParameter = new Dictionary<string, string>();
            Token = token;
        }

        public DataTable Get()
        {
            ServiceResult<List<Unit>> list = CloudServiceFactory<List<Unit>>.ExecuteFunction("Unit/GetList", null, HeaderParameter, "GET");
            if (list.Success && list.Data != null)
                return CommonFunction.ConvertToDataTable(list.Data);
            else return null;
        }

        public int InsertUpdate(DictionaryDataSet.UnitRow drObjectChange, Guid oldID)
        {
            ServiceResult result = CloudServiceFactory.ExecuteFunction("Unit/InsertUpdate", new InsertUpdateParameter<Unit>(oldID, CommonFunction.GetItem<Unit>(drObjectChange)), HeaderParameter);
            if (result.Success == false)
            {
                if (result.ErrorCode == ErrorCode.DuplicateCode)
                    return 2;
                return 0;
            }
            return 1;
        }

        public bool CheckCodeExists(DictionaryDataSet.UnitRow drObjectChange)
        {
            ServiceResult result = CloudServiceFactory.ExecuteFunction("Unit/CheckCodeExists", CommonFunction.GetItem<Unit>(drObjectChange), HeaderParameter);
            return result.Success;
        }

        public int Delete(Guid id)
        {
            ServiceResult result = CloudServiceFactory.ExecuteFunction("Unit/Delete", id.ToString(), HeaderParameter);
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
            ServiceResult result = CloudServiceFactory.ExecuteFunction("Unit/CheckBeforeDelete", id.ToString(), HeaderParameter);
            return result.Success;
        }
    }
}
