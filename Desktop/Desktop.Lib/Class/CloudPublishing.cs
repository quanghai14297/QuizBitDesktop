using QuizBit.Contract;
using QuizBit.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop.Lib
{
    class CloudPublishing : ICloudPublishing
    {
        private string token;

        public string Token
        {
            get { return token; }
            set { token = value; HeaderParameter.Add("Authorization", Token); }
        }

        private Dictionary<string, string> headerParameter;

        public Dictionary<string, string> HeaderParameter
        {
            get { return headerParameter; }
            set { headerParameter = value; }
        }



        public CloudPublishing(string token)
        {
            HeaderParameter = new Dictionary<string, string>();
            Token = token;
        }

        /// <summary>
        /// Lấy ra thông tin đơn vị tính
        /// </summary>
        /// <returns></returns>
        public List<Unit> GetUnitList()
        {
            ServiceResult<List<Unit>> result = CloudServiceFactory<List<Unit>>.ExecuteFunction("unit/getlist", null, HeaderParameter, "GET");
            if (result.Success && result.Data != null)
                return result.Data;
            else return null;
        }
    }
}
