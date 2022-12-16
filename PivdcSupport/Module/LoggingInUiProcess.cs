using PivdcSupportModel;
using PivdcSupportUi;
using System;
using System.Reflection;
using System.Windows;
using System.Windows.Input;

namespace PivdcSupportModule
{
    public class LoggingInUiProcess
    {
        private string CentralDbConnectionString { get; set; }
        private string LocalDbConnectionString { get; set; }
        private string LocalDbLocation { get; set; }
        private string DomainName { get; set; }
        private LoginWindow LoginWindowObj { get; set; }

        public LoggingInUiProcess(LoginWindow loginWindow, string centralLoginConnectionString, string localDBLocation, string localDBConnectionString)
        {
            CentralDbConnectionString = centralLoginConnectionString;
            LocalDbConnectionString = localDBConnectionString;
            LocalDbLocation = localDBLocation;
            DomainName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
            LoginWindowObj = loginWindow;
            double screenWidth = SystemParameters.PrimaryScreenWidth;
            double screenHeight = SystemParameters.PrimaryScreenHeight;
            double windowWidth = LoginWindowObj.Width;
            double windowHeight = LoginWindowObj.Height;
            LoginWindowObj.Left = (screenWidth / 2) - (windowWidth / 2);
            LoginWindowObj.Top = (screenHeight / 2) - (windowHeight / 2);
        }

        public void LoadLoginWindow(object sender)
        {
            LoginWindowObj.Icon = sender.UserInterfaceIcon();
            LoginWindowObj.Title = string.Format("Login Window | {0}", SupportDatas.UserInterfaeTitle);
            LoginWindowObj.txtDomainName.Text = string.Format("Domain Name : {0}", DomainName);
            if (SupportDatas.CanLoginThroughAmazon)
            {
                LoginWindowObj.PISID.Text = "Enter P360 Login Email Id : ";
                LoginWindowObj.Password.Text = "Enter P360 Login Credential : ";
            }
            else
            {
                LoginWindowObj.PISID.Text = "Enter your Emp ID (without PIS) : ";
                LoginWindowObj.Password.Text = "Enter P360 Login Password (Old) : ";
            }
        }

        public void PasswordTextBoxKeyPressEvent(object sender, KeyEventArgs e)
        {
            try
            {
                if ((e.Key == Key.Enter) && LoginWindowObj.Password.IsEnabled)
                {
                    System.Threading.Tasks.Task task = LoggedInAsync();
                }
            }
            catch (Exception ex)
            {
                SupportDatas.ToolIdPerDatabase = Convert.ToInt32(SupportDatas.LoginToolId);
                ex.ErrorLogEntry(MethodBase.GetCurrentMethod().Name);
            }
        }

        public async System.Threading.Tasks.Task LoggedInAsync()
        {
            try
            {
                SupportDatas.CurrentLoginInformation = LocalDatabaseInteraction.HasLoggedInformation(LocalDbLocation, LocalDbConnectionString);
                if (SupportDatas.CurrentLoginInformation.LoginStatus)
                {
                    MessageBox.Show("You are already logged-in.");
                    return;
                }
                if (SupportDatas.CanLoginThroughAmazon)
                {
                    OnlineLoginClass onlineLoginClass = new OnlineLoginClass();
                    //Amazon.Extensions.CognitoAuthentication
                    //    .CognitoUser cognitoUser = await onlineLoginClass.ValidateUser(LoginWindowObj.txtPIS.Text, LoginWindowObj.txtPassword.Password);
                    bool status = await onlineLoginClass.ValidateLogin(LoginWindowObj.txtPIS.Text, LoginWindowObj.txtPassword.Password);
                    /*if (cognitoUser != null)*/
                    if (status)
                    {
                        LoginInformation loginInformation = new LoginInformation
                        {
                            DomainName = DomainName
                        };
                        loginInformation = DatabaseInformation.LoginInformationMatchedWithCentralDatabase(LoginWindowObj.txtPIS.Text, loginInformation,
                            SupportDatas.CentralLoginConnectionString);
                        if (!string.IsNullOrEmpty(loginInformation.EmployeeId))
                        {
                            //insert the verified information into local database
                            if (!LocalDatabaseInteraction.InsertInformationToLocalDatabase(loginInformation, LocalDbLocation, LocalDbConnectionString))
                            {
                                new Exception("Unable to insert the data in local database").ErrorLogEntry(MethodBase.GetCurrentMethod().Name);
                            }
                            else
                            {
                                SupportDatas.CurrentLoginInformation.StatusMessage = "Error in local Db insertion";
                            }
                        }
                        SupportDatas.CurrentLoginInformation = loginInformation;
                    }
                }
                else
                {
                    LoginInformation loginInformation = new LoginInformation
                    {
                        DomainName = DomainName,
                        EmployeeId = LoginWindowObj.txtPIS.Text,
                        Password = LoginWindowObj.txtPassword.Password
                    };
                    //verification with central database with the provided information 
                    loginInformation = DatabaseInformation.LoginInformationMatchedWithCentralDatabase(loginInformation, CentralDbConnectionString);
                    if (!string.IsNullOrEmpty(loginInformation.EmployeeId))
                    {
                        //insert the verified information into local database
                        if (!LocalDatabaseInteraction.InsertInformationToLocalDatabase(loginInformation, LocalDbLocation, LocalDbConnectionString))
                        {
                            new Exception("Unable to insert the data in local database").ErrorLogEntry(MethodBase.GetCurrentMethod().Name);
                        }
                    }
                    SupportDatas.CurrentLoginInformation = loginInformation;
                }
            }
            catch (Exception ex)
            {
                SupportDatas.ToolIdPerDatabase = Convert.ToInt32(SupportDatas.LoginToolId);
                ex.ErrorLogEntry(MethodBase.GetCurrentMethod().Name);
            }
            LoginWindowObj.Close();
        }
    }
}