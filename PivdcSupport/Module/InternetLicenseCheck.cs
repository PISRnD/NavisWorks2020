using System;
using System.IO;
using System.Net;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace PivdcSupportModule
{
    public static class InternetLicenseCheck
    {
        public static bool IsValidLicense(string path)
        {
            WebClient webclient = new WebClient();
            string macId = getMacId();
            macId = Regex.Replace(macId, "[^A-Za-z0-9]+", "");
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            string[] readLicFile = File.ReadAllLines(path);
            string licCode = readLicFile[0];
            string licRaft = readLicFile[1];
            licCode = licCode.Trim();
            licRaft = licRaft.Trim();
            string endPoint = string.Format("https://license.pivdc.com/verifyLicense?ProductKey={0}&MacID={1}&PluginName={2}", licCode, macId, licRaft);
            string result = webclient.DownloadString(endPoint);
            if (result.Contains("true"))
            {
                webclient.Dispose();
                return true;
            }
            webclient.Dispose();
            return false;
        }

        private static string getMacId()
        {
            string macAddresses = "";
            try
            {
                foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
                {
                    if (nic.OperationalStatus == OperationalStatus.Up)
                    {
                        macAddresses = RemoveSpecialCharacters(RemoveSpace(nic.GetPhysicalAddress().ToString()));
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                ex.ErrorLogEntry(MethodBase.GetCurrentMethod().Name);
            }
            return macAddresses;
        }

        public static string RemoveSpecialCharacters(string text)
        {
            StringBuilder sb = new StringBuilder();
            foreach (char c in text)
            {
                if ((c >= '0' && c <= '9') || (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z') || c == '.' || c == '_')
                {
                    sb.Append(c);
                }
            }
            return sb.ToString();
        }

        public static string RemoveSpace(string text)
        {
            return Regex.Replace(text, @"\s+", ""); //Replaces all(+) space characters (\s) with empty("")
        }
    }
}