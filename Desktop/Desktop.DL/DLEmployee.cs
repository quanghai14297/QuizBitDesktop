using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Desktop.Entity;
using Desktop.Lib;
using QuizBit.Contract;
using QuizBit.Entity;

namespace Desktop.DL
{
    public class DLEmployee : DLBase
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

        public DLEmployee(string token)
        {
            TableName = "Employee";
            ObjectIDParam = "EmployeeID";
            HeaderParameter = new Dictionary<string, string>();
            Token = token;
            InitStored();
        }

        public DataTable Get()
        {
            ServiceResult<List<Employee>> list = CloudServiceFactory<List<Employee>>.ExecuteFunction("Employee/getlist", null, HeaderParameter, "GET");
            if (list.Success && list.Data != null)
                return CommonFunction.ConvertToDataTable(list.Data);
            else return null;
        }

        public int InsertUpdate(DictionaryDataSet.EmployeeRow drObjectChange)
        {
            ServiceResult result = CloudServiceFactory.ExecuteFunction("Employee/InsertUpdate", CommonFunction.GetItem<Employee>(drObjectChange), HeaderParameter);
            if (result.Success == false)
            {
                if (result.ErrorCode == ErrorCode.DuplicateCode)
                    return 2;
                return 0;
            }
            return 1;
        }

        public bool CheckCodeExists(DictionaryDataSet.EmployeeInforRow drObjectChange)
        {
            ServiceResult result = CloudServiceFactory.ExecuteFunction("Employee/CheckCodeExists", CommonFunction.GetItem<Employee>(drObjectChange), HeaderParameter);
            return result.Success;
        }

        public int Delete(Guid id)
        {
            ServiceResult result = CloudServiceFactory.ExecuteFunction("Employee/Delete", id.ToString(), HeaderParameter);
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
            ServiceResult result = CloudServiceFactory.ExecuteFunction("Employee/checkbeforedelete", id.ToString(), HeaderParameter);
            return result.Success;
        }
    }
}
