using System;
using System.Configuration;

namespace Desktop.Lib
{
    public class CommonKey
    {
        /// <summary>
        /// Địa chỉ Website
        /// </summary>
        public static string WebUrl
        {
            get
            {
                string url = "http://khanhjm.com/";
                try
                {
                    var appSetting = ConfigurationManager.AppSettings["WebUrl"];
                    if (appSetting != null)
                        if (!string.IsNullOrEmpty(appSetting))
                            url = appSetting;
                }
                catch (Exception)
                {
                    url = "http://khanhjm.com/";
                }
                return url;

            }
        }
    }
}
