using Desktop.Entity;
using QuizBit.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop.BL
{
    /// <summary>
    /// Session theo mỗi phiên làm việc
    /// </summary>
    public static class Session
    {
        /// <summary>
        /// Đường dẫn thư mục
        /// </summary>
        public static string PathBin => AppDomain.CurrentDomain.BaseDirectory;

        /// <summary>
        /// Đường dẫn file LocalSetting
        /// </summary>
        public static string LocalSettingPath => PathBin + "LocalSetting.xml";

        /// <summary>
        /// Token đăng nhập
        /// </summary>
        public static string Token { get; set; }

        /// <summary>
        /// Tài khoản đăng nhập
        /// </summary>
        public static UserLogin UserLogin { get; set; }

        /// <summary>
        /// Kiểm tra đã đăng nhập chưa
        /// <para>True - đã đăng nhập || False - chưa đăng nhập</para>
        /// </summary>
        public static bool IsLogin
        {
            get
            {
                if (string.IsNullOrEmpty(Token) && UserLogin == null) return false;
                return true;
            }
        }

        /// <summary>
        /// Thời gian bắt đầu làm việc
        /// </summary>
        public static DateTime StartDate
        {
            get
            {
                // Lấy thông tin từ Thiết lập
                return new DateTime(2019, 1, 1, 8, 0, 0);
            }
            set
            {
                // Lưu thông tin vào thiết lập
            }
        }

        /// <summary>
        /// Thời gian kết thúc làm việc
        /// </summary>
        public static DateTime EndDate
        {
            get
            {
                // Lấy thông tin từ Thiết lập
                return new DateTime(2019, 1, 1, 17, 0, 0);
            }
            set
            {
                // Lưu thông tin vào thiết lập
            }
        }

        private static DateTime _today = DateTime.Now;

        /// <summary>
        /// Ngày hiện tại trên danh sách
        /// </summary>
        public static DateTime Today
        {
            get { return _today; }
            set { _today = value; }
        }

        private static DictionaryDataSet.SystemInfoDataTable _companyInfo = new DictionaryDataSet.SystemInfoDataTable();

        /// <summary>
        /// Thông tin doanh nghiệp
        /// </summary>
        public static DictionaryDataSet.SystemInfoRow CompanyInfo
        {
            get
            {
                var table = new DL.DLSystemInfo().GetAll();
                if (table != null)
                    if (table.Rows.Count > 0)
                    {
                        _companyInfo.Clear();
                        _companyInfo.Merge(table);
                    }
                return _companyInfo[0];
            }
            set
            {
                if (value != null)
                {
                    _companyInfo[0].Name = value.Name;
                    _companyInfo[0].Email = value.Email;
                    _companyInfo[0].Hotline = value.Hotline;
                    _companyInfo[0].Website = value.Website;
                    _companyInfo[0].Address = value.Address;
                    new DL.DLSystemInfo().InsertUpdate(_companyInfo[0]);
                }
            }
        }
    }
}
