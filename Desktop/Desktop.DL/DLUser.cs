using Desktop.Lib;
using QuizBit.Contract;
using QuizBit.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop.DL
{
    public class DLUser : DLBase
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

        public DLUser(string token)
        {
            TableName = "User";
            ObjectIDParam = "UserID";
            HeaderParameter = new Dictionary<string, string>();
            Token = token;
            InitStored();
        }
        public DataTable Get()
        {
            ServiceResult<List<User>> list = CloudServiceFactory<List<User>>.ExecuteFunction("User/getlist", null, HeaderParameter, "GET");
            if (list.Success && list.Data != null)
                return CommonFunction.ConvertToDataTable(list.Data);
            else return null;
        }
    }
}
