using QuizBit.Contract;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace Desktop.Lib
{
    public static class CloudServiceFactory
    {

        /// <summary>
        /// Thực thi gọi hàm lên API
        /// </summary>
        /// <param name="funtionName"></param>
        /// <param name="parameter"></param>
        /// <param name="headers"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        public static ServiceResult ExecuteFunction(string funtionName, object parameter, Dictionary<string, string> headers = null, string method = "POST")
        {
            var result = new ServiceResult();
            try
            {
                var apiUrl = CommonKey.WebUrl;
                if (!apiUrl.EndsWith("/"))
                {
                    apiUrl += "/";
                }
                apiUrl += "api/" + funtionName;
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(apiUrl);
                request.Method = method;

                // Tạo đối tượng param để send
                var sParam = string.Empty;
                if (parameter != null)
                {
                    sParam = SerializeUtil.Serialize(parameter);
                }
                var encode = new System.Text.UTF8Encoding();
                var byteArray = encode.GetBytes(sParam);

                request.ContentLength = byteArray.Length;
                request.ContentType = "application/json; charset=utf-8";

                if (headers != null && headers.Count > 0)
                    foreach (KeyValuePair<string, string> header in headers)
                        request.Headers.Add(header.Key, header.Value);

                // Truyền param vào request
                var dataStream = request.GetRequestStream();
                dataStream.Write(byteArray, 0, byteArray.Length);

                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        var sr = new StreamReader(response.GetResponseStream());
                        var resultString = sr.ReadToEnd();
                        if (!string.IsNullOrWhiteSpace(resultString))
                            result = SerializeUtil.Deserialize<ServiceResult>(resultString);
                    }
                    else
                    {
                        result.Success = false;
                        result.ErrorCode = response.StatusDescription;
                        result.Errors.Add(response.StatusDescription);
                    }
                }
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorCode = ErrorCode.Exception;
                // Nếu không truy cập được vào Cloud
                if (ex.Message.Contains("(401) Unauthorized"))
                    result.ErrorCode = ErrorCode.InvalidPassword;
                result.Errors.Add(ex.Message);
            }
            return result;
        }
    }

    public static class CloudServiceFactory<T>
    {

        /// <summary>
        /// Thực thi gọi hàm lên API
        /// </summary>
        /// <param name="funtionName"></param>
        /// <param name="parameter"></param>
        /// <param name="headers"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        public static ServiceResult<T> ExecuteFunction(string funtionName, object parameter, Dictionary<string, string> headers = null, string method = "POST")
        {
            var result = new ServiceResult<T>();
            try
            {
                var apiUrl = CommonKey.WebUrl;
                if (!apiUrl.EndsWith("/"))
                {
                    apiUrl += "/";
                }
                apiUrl += "api/" + funtionName;
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(apiUrl);
                request.Method = method;

                // Tạo đối tượng param để send
                var sParam = string.Empty;
                if (parameter != null)
                {
                    sParam = SerializeUtil.Serialize(parameter);
                }
                var encode = new System.Text.UTF8Encoding();
                var byteArray = encode.GetBytes(sParam);

                request.ContentLength = byteArray.Length;
                request.ContentType = "application/json; charset=utf-8";

                if (headers != null && headers.Count > 0)
                    foreach (KeyValuePair<string, string> header in headers)
                        request.Headers.Add(header.Key, header.Value);


                if (method == "POST")
                {
                    // Truyền param vào request
                    var dataStream = request.GetRequestStream();
                    dataStream.Write(byteArray, 0, byteArray.Length);
                }

                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        var sr = new StreamReader(response.GetResponseStream());
                        var resultString = sr.ReadToEnd();
                        if (!string.IsNullOrWhiteSpace(resultString))
                            result = SerializeUtil.Deserialize<ServiceResult<T>>(resultString);
                    }
                    else
                    {
                        result.Success = false;
                        result.ErrorCode = response.StatusDescription;
                        result.Errors.Add(response.StatusDescription);
                    }
                }
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorCode = ErrorCode.Exception;
                // Nếu không truy cập được vào Cloud
                if (ex.Message.Contains("(401) Unauthorized"))
                    result.ErrorCode = ErrorCode.InvalidPassword;
                result.Errors.Add(ex.Message);
            }
            return result;
        }
    }
}
