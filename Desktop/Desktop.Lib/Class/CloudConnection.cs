using QuizBit.Contract;
using QuizBit.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop.Lib
{
    class CloudConnection : ICloudConnection
    {
        private string token;

        public string Token
        {
            get { return token; }
            set { token = value; }
        }


        public CloudConnection(string token)
        {
            Token = token;
        }

        /// <summary>
        /// Hàm thực hiện đăng nhập
        /// </summary>
        /// <param name="username">Tên đăng nhập</param>
        /// <param name="password">Mật khẩu</param>
        /// <returns>Token</returns>
        public string Login(string username, string password)
        {
            ServiceResult result = CloudServiceFactory.ExecuteFunction("login", new UserLogin(username, password));
            if (result.Success && result.Data != null)
                return result.Data.ToString();
            else return string.Empty;
        }

        /// <summary>
        /// Lấy ra thông tin tài khoản
        /// </summary>
        /// <param name="username">Tên đăng nhập</param>
        /// <param name="password">Mật khẩu</param>
        /// <returns></returns>
        public UserLogin GetUserLogin(string username, string password)
        {
            ServiceResult<UserLogin> result = CloudServiceFactory<UserLogin>.ExecuteFunction("login/getuser", new UserLogin(username, password));
            if (result.Success && result.Data != null)
                return result.Data;
            else return null;
        }
    }
}
