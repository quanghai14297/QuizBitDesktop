using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientApp
{
    public static class CommonKeyApp
    {
        /// <summary>
        /// Địa chỉ Website
        /// </summary>
        public static string WebUrl
        {
            get
            {
                string url = "https://khanhjm.com/";
                try
                {
                    var appSetting = ConfigurationManager.AppSettings["WebUrl"];
                    if (appSetting != null)
                        if (string.IsNullOrEmpty(appSetting))
                            url = appSetting;
                }
                catch (Exception)
                {
                    url = "https://khanhjm.com/";
                }
                return url;
            }
        }
    }
}
