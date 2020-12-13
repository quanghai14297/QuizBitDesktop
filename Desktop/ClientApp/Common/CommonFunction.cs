using Desktop.Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ClientApp
{
    public static class CommonFunction
    {
        /// <summary>
        /// Ping đến host
        /// <para>True - Có kết nối || False - Không kết nối được/para>
        /// </summary>
        /// <param name="host"></param>
        /// <returns></returns>
        private static bool IsConnectedToInternet(string host)
        {
            Ping p = new Ping();
            try
            {
                PingReply pr = p.Send(host.Replace("http", "").Replace("/", "").Replace(":", ""), 3000);
                if (pr.Status == IPStatus.Success)
                {
                    return true;
                }
            }
            catch (Exception)
            {
            }
            return false;
        }

        /// <summary>
        /// Hàm kiểm tra mạng
        /// <para>True - Có kết nối || False - Không kết nối được</para>
        /// </summary>
        /// <returns></returns>
        public static bool CheckNetWork()
        {
            //if (IsConnectedToInternet(CommonKey.WebUrl)) return true;
            //MessageBoxCommon.ShowMessageError("Không thể kết nối đến máy chủ");
            //return false;
            return true;
        }

        /// <summary>
        /// Lấy ra mô tả của các Enum
        /// </summary>
        /// <param name="en"></param>
        /// <returns></returns>
        public static string GetDescription(Enum en)
        {
            Type type = en.GetType();
            MemberInfo[] memInfo = type.GetMember(en.ToString());
            if (memInfo != null && memInfo.Length > 0)
            {
                object[] attrs = memInfo[0].GetCustomAttributes(typeof(Description),
                                                                false);
                if (attrs != null && attrs.Length > 0)
                    return ((Description)attrs[0]).Text;
            }
            return en.ToString();
        }

        /// <summary>
        /// Danh sách chuỗi Remove dấu tiếng việt
        /// </summary>
        private static readonly string[] VietnameseSigns = new string[]
        {
            "aAeEoOuUiIdDyY",
            "áàạảãâấầậẩẫăắằặẳẵ",
            "ÁÀẠẢÃÂẤẦẬẨẪĂẮẰẶẲẴ",
            "éèẹẻẽêếềệểễ",
            "ÉÈẸẺẼÊẾỀỆỂỄ",
            "óòọỏõôốồộổỗơớờợởỡ",
            "ÓÒỌỎÕÔỐỒỘỔỖƠỚỜỢỞỠ",
            "úùụủũưứừựửữ",
            "ÚÙỤỦŨƯỨỪỰỬỮ",
            "íìịỉĩ",
            "ÍÌỊỈĨ",
            "đ",
            "Đ",
            "ýỳỵỷỹ",
            "ÝỲỴỶỸ"
        };

        /// <summary>
        /// Hàm chuyển chuỗi Tiếng Việt sang Tiếng Việt không dấu
        /// </summary>
        /// <param name="str">chuỗi cần bỏ dấu</param>
        /// <returns>chuỗi không dấu</returns>
        public static string RemoveSignVietnameseString(string str)
        {
            //Tiến hành thay thế , lọc bỏ dấu cho chuỗi
            for (int i = 1; i < VietnameseSigns.Length; i++)
            {
                for (int j = 0; j < VietnameseSigns[i].Length; j++)
                    str = str.Replace(VietnameseSigns[i][j], VietnameseSigns[0][i - 1]);
            }
            return str;
        }

        /// <summary>
        /// Chuyển thành tiền tệ VNĐ kiểu chữ
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public static string ConvertToCurrency(object t)
        {
            return string.Format(System.Globalization.CultureInfo.GetCultureInfo("vi-VN"), "{0:c0}", t);
        }

    }
}
