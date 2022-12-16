using System;
using System.IO;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;

namespace PivdcNavisworksSupportModule
{
    public static class OnlineLicenseCheck
    {
        public static bool IsOlineValidLicense(string path)
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
                        macAddresses += RemoveSpecialCharacters(RemoveSpace(nic.GetPhysicalAddress().ToString()));
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
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
            string newString = "";  // MUST set the Regex result to a variable for it to take effect
            newString = Regex.Replace(text, @"\s+", ""); //Replaces all(+) space characters (\s) with empty("")
            return newString;
        }

        /// <summary>
        /// Finds the MAC address of the NIC with maximum speed.
        /// </summary>
        /// <returns>The MAC address.</returns>
        public static string GetMacAddress()
        {
            const int MIN_MAC_ADDR_LENGTH = 12;
            string macAddress = string.Empty;
            long maxSpeed = -1;
            foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
            {
                string tempMac = nic.GetPhysicalAddress().ToString();
                if ((nic.Speed > maxSpeed) && !string.IsNullOrEmpty(tempMac) && (tempMac.Length >= MIN_MAC_ADDR_LENGTH))
                {
                    maxSpeed = nic.Speed;
                    macAddress = tempMac;
                }
            }
            return macAddress;
        }

        /// <summary>
        /// Searches for the local IP Address.
        /// </summary>
        /// <returns>The local IP address if catches any, empty string otherwise.</returns>
        /// <exception cref="Exception"></exception>
        public static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("No network adapters with an IPv4 address in the system!");
        }
    }
}