using Desktop.Entity;
using Desktop.Lib;
using QuizBit.Contract;
using QuizBit.Entity;
using System;
using System.Collections.Generic;
using System.Data;

namespace Desktop.DL
{
    public class DLArea
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

        public DLArea(string token)
        {
            HeaderParameter = new Dictionary<string, string>();
            Token = token;
        }

        public DataTable Get()
        {
            ServiceResult<List<Area>> list = CloudServiceFactory<List<Area>>.ExecuteFunction("Area/getlist", null, HeaderParameter, "GET");
            if (list.Success && list.Data != null)
                return CommonFunction.ConvertToDataTable(list.Data);
            else return null;
        }

        public int InsertUpdate(DictionaryDataSet.AreaRow drObjectChange, Guid oldID)
        {
            ServiceResult result = CloudServiceFactory.ExecuteFunction("Area/InsertUpdate", new InsertUpdateParameter<Area>(oldID, CommonFunction.GetItem<Area>(drObjectChange)), HeaderParameter);
            if (result.Success == false)
            {
                if (result.ErrorCode == ErrorCode.DuplicateCode)
                    return 2;
                return 0;
            }
            return 1;
        }

        public bool CheckCodeExists(DictionaryDataSet.AreaRow drObjectChange)
        {
            ServiceResult result = CloudServiceFactory.ExecuteFunction("Area/CheckCodeExists", CommonFunction.GetItem<Area>(drObjectChange), HeaderParameter);
            return result.Success;
        }

        public int Delete(Guid id)
        {
            ServiceResult result = CloudServiceFactory.ExecuteFunction("Area/Delete", id.ToString(), HeaderParameter);
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
            ServiceResult result = CloudServiceFactory.ExecuteFunction("Area/checkbeforedelete", id.ToString(), HeaderParameter);
            return result.Success;
        }
    }
}
