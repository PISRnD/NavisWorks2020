using PivdcSupportModel;
using RestSharp;
using System;
using System.IO;

namespace PivdcSupportModule
{
    /// <summary>
    /// Offline/ Online Error logging 
    /// </summary>
    public static class ErrorLog
    {
        public static void ErrorLogEntry(this Exception exception, string errorPage)
        {
            if (SupportDatas.ToolLicenseAccessType is "000int")
            {
                if (string.IsNullOrEmpty(SupportDatas.CurrentLoginInformation.EmployeeId) || string.IsNullOrWhiteSpace(SupportDatas.ToolNameInDatabase)
                    || string.IsNullOrWhiteSpace(SupportDatas.CurrentLoginInformation.EmployeeId) || string.IsNullOrEmpty(SupportDatas.ToolNameInDatabase))
                {
                    TextFileLog(exception.ToString());
                }
                else if (!LogError(SupportDatas.CurrentLoginInformation.EmployeeId, SupportDatas.ToolNameInDatabase, exception.ToString(),
                    exception.GetType().ToString(), SupportDatas.SystemHostName, errorPage))
                {
                    TextFileLog(exception.ToString());
                }
            }
            else if ((SupportDatas.ToolLicenseAccessType is "111ext") || (SupportDatas.ToolLicenseAccessType is "111extOffline"))
            {
                TextFileLog(exception.ToString());
            }
            else
            {
                //Future code
            }
            exception.JournalLog(errorPage);
        }

        /// <summary>
        /// Log to local system
        /// </summary>
        /// <param name="errorMessage"> error message.</param>
        private static void TextFileLog(string errorMessage)
        {
            // This should give you something like C:\Users\Public\Documents
            string errorLogDirectory = string.Format("{0}\\PinnacleErrorLog", Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments));
            if (!Directory.Exists(errorLogDirectory))
            {
                Directory.CreateDirectory(errorLogDirectory);
            }
            string errorLogFileFullPath = string.Format("{0}\\ErrorLog.txt", errorLogDirectory);
            File.AppendAllText(errorLogFileFullPath, string.Format("error={0} at {1}\n", errorMessage, DateTime.Now.ToLongDateString()));
        }

        /// <summary>
        /// Log error in web with userid, appliction ID & error details
        /// </summary>
        /// <param name="uid">User Id from Login details</param>
        /// <param name="appID">Application ID from config file</param>
        /// <param name="error">Exception from Catch block or manual entry</param>
        /// <param name="errorType">Type of error</param>
        /// <param name="errorLocation">For Windows/APP/Plugin: System/IP | For Web: Server/Project- Optional </param>
        /// <param name="errorPage">App Class/Controller- method.. </param>
        /// <returns>return true if the error was enter into the error log file</returns>
        private static bool LogError(string uid, string appID, string error, string errorType, string errorLocation, string errorPage)
        {
            string urlString = string.Format("http://10.1.2.47:2029/api/error?uid={0}&appid={1}&errmsg={2}&errtype={3}&errloc={4}&errpage={5}",
                uid, appID, error, errorType, errorLocation, errorPage);
            var client = new RestClient(urlString);
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);
            Console.WriteLine(response.Content);
            if (response.Content.Contains("true"))
            {
                return true;
            }
            else
            {
                TextFileLog(error);
                return false;
            }
        }
    }
}