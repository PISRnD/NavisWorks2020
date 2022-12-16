using Autodesk.Navisworks.Api.Plugins;
using PivdcNavisworksSupportModule;
//using PiNavisworks.PiNavisworksSupport;
using System;
using System.IO;
using System.Management;
using System.Net.NetworkInformation;
using System.Security.Cryptography;
using System.Text;
using Utility.Model;

namespace CreateRevitSchedule
{
    [PluginAttribute("CreateRevitSchedule", "PIS_CreateRevitSchedule_2", DisplayName = "Create Revit Schedule", ToolTip = "Create schedules in excel just like revit")]
    [AddInPluginAttribute(AddInLocation.AddIn, CallCanExecute = CallCanExecute.Always)]
    public class CreateScheduleQuickClass : AddInPlugin
    {
       public override int Execute(params string[] parameters)
        {
            if (ElementDetails.Document.IsValidTool("CreateRevitSchedule",true))
            {
                start();
            }
          
            return 0;
        }
        public void getAppinfo(string path, out string LicKey, out string AddinId, out string baseUrl, out string eulaLink)
        {
            LicKey = ""; AddinId = ""; baseUrl = ""; eulaLink = "";
            try
            {
                string decryptedText = Decrypt(File.ReadAllText(path), false);
                var ss = decryptedText.Split('\n');
                LicKey = ss[0];
                AddinId = ss[1];
                baseUrl = ss[2];
                eulaLink = ss[3];
            }
            catch (Exception ex)
            {
                string str = ex.ToString();
            }
        }
        public string Decrypt(string cipherString, bool useHashing)
        {
            byte[] keyArray;
            //get the byte code of the string

            byte[] toEncryptArray = Convert.FromBase64String(cipherString);

            //System.Configuration.AppSettingsReader settingsReader =  new AppSettingsReader();
            //Get your key from config file to open the lock!
            string key = "GoodMorningggggg";//16 digit key necessary (string)settingsReader.GetValue("SecurityKey", typeof(String));

            if (useHashing)
            {
                //if hashing was used get the hash code with regards to your key
                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                //release any resource held by the MD5CryptoServiceProvider

                hashmd5.Clear();
            }
            else
            {
                //if hashing was not implemented get the byte code of the key
                keyArray = UTF8Encoding.UTF8.GetBytes(key);
            }

            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            //set the secret key for the tripleDES algorithm
            tdes.Key = keyArray;
            //mode of operation. there are other 4 modes. 
            //We choose ECB(Electronic code Book)

            tdes.Mode = CipherMode.ECB;
            //padding mode(if any extra byte added)
            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = tdes.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(
                                 toEncryptArray, 0, toEncryptArray.Length);
            //Release resources held by TripleDes Encryptor                
            tdes.Clear();
            //return the Clear decrypted TEXT
            return UTF8Encoding.UTF8.GetString(resultArray);
        }
        public void start()
        {
            UI_CreateSchedule createRevitScheduleUI = new UI_CreateSchedule();
            //IWin32Window win32WindowRevit = new JtWindowHandle(Autodesk.Windows.ComponentManager.ApplicationWindow);
            //System.Windows.Interop.WindowInteropHelper windowInteropHelper = new System.Windows.Interop.WindowInteropHelper(createRevitScheduleUI);
            //windowInteropHelper.Owner = win32WindowRevit.Handle;
            //ElementHost.EnableModelessKeyboardInterop(createRevitScheduleUI);
            createRevitScheduleUI.Show();
          //  UI_CreateSchedule obj = new UI_CreateSchedule();
           // obj.Show();
        }
        private string getMacId()
        {
            string macAddresses = "";
            try
            {
                foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
                {
                    if (nic.OperationalStatus == OperationalStatus.Up)
                    {
                        macAddresses += nic.GetPhysicalAddress().ToString();
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                string str = ex.ToString();
            }
            return macAddresses;
        }
        private string getProcessorId()
        {
            string ProcessorId = "";
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("select * from Win32_Processor");
            foreach (ManagementObject queryObj in searcher.Get())
            {
                try
                {
                    ProcessorId = queryObj["ProcessorId"].ToString();
                    break;
                }
                catch (Exception ex)
                {
                    string str = ex.ToString();
                    ProcessorId = System.DateTime.Now.Ticks.ToString();

                }
            }
            return ProcessorId;
        }
        private string getMotherboardId()
        {
            string motherboardId = "";
            ManagementObjectSearcher searcher1 = new ManagementObjectSearcher("select * from Win32_BaseBoard");
            foreach (ManagementObject queryObj in searcher1.Get())
            {
                try
                {
                    motherboardId = queryObj["SerialNumber"].ToString();
                    break;
                }
                catch (Exception ex)
                {
                    string str = ex.ToString();
                    motherboardId = System.DateTime.Now.Ticks.ToString();
                }
            }
            return motherboardId;
        }
        private string getHDDId()
        {
            string hddID = "";
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("select * from Win32_DiskDrive");
            foreach (ManagementObject queryObj in searcher.Get())
            {
                try
                {
                    hddID = queryObj.GetPropertyValue("SerialNumber").ToString();
                    break;
                }
                catch (Exception ex)
                {
                    string str = ex.ToString();
                    hddID = System.DateTime.Now.Ticks.ToString();
                }
            }
            return hddID;
        }
    }
}
