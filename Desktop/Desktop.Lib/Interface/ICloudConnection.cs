using QuizBit.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop.Lib
{
    public interface ICloudConnection
    {
        /// <summary>
        /// Hàm thực hiện đăng nhập
        /// </summary>
        /// <returns>Token</returns>
        string Login(string username, string password);

        /// <summary>
        /// Hàm thực hiện đăng nhập
        /// </summary>
        /// <returns>UserLogin</returns>
        UserLogin GetUserLogin(string username, string password);
    }
}
