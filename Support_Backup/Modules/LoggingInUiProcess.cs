using PivdcNavisworksSupportModel;
using PivdcSupportUi;
using System;
using System.Reflection;
using System.Windows;
using System.Windows.Input;

namespace PivdcNavisworksSupportModule
{
    /// <summary>
    /// Login window event operation.
    /// </summary>
    public class LoggingInUiProcess
    {
        /// <summary>
        /// The central database connection string.
        /// </summary>
        private string CentralDbConnectionString { get; set; }

        /// <summary>
        /// The local database connection string.
        /// </summary>
        private string LocalDbConnectionString { get; set; }

        /// <summary>
        /// The location of the local database in the currently owrking system.
        /// </summary>
        private string LocalDbLocation { get; set; }

        /// <summary>
        /// The domain name of the current loggedin user.
        /// </summary>
        private string DomainName { get; set; }

        /// <summary>
        /// The user interface window object to access the controls of the user interface.
        /// </summary>
        private LoginWindow LoginWindowObj { get; set; }

        /// <summary>
        /// Creating new instance of the login window user interface operation by taking some of the parameter values.
        /// </summary>
        /// <param name="loginWindow">The login window object.</param>
        /// <param name="centralLoginConnectionString">The central connection detail.</param>
        /// <param name="localDBLocation">The local databse location for the user information.</param>
        /// <param name="localDBConnectionString">The connection details of the local databse.</param>
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

        /// <summary>
        /// The loading event operations for the user interface.
        /// </summary>
        /// <param name="sender">The root object instance of the currenly active or running control.</param>
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

        /// <summary>
        /// Key down event operation for the password box.
        /// </summary>
        /// <param name="sender">The root object instance of the currenly active or running control.</param>
        /// <param name="e">The routed event data for the control.</param>
        public void PasswordTextBoxKeyPressEvent(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key != Key.Enter)
                {
                    LoginWindowObj.txtbShowPaasword.Text = LoginWindowObj.txtPassword.Password;
                }
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

        /// <summary>
        /// The password box text change event operation to update the show password control.
        /// </summary>
        /// <param name="sender">The root object instance of the currenly active or running control.</param>
        /// <param name="e">The routed event data for the control.</param>
        public void PasswordBoxPasswordChanged(object sender, RoutedEventArgs e)
        {
            try
            {
                LoginWindowObj.txtbShowPaasword.Text = string.Format("{0}{1}", LoginWindowObj.txtPassword.Password, string.Empty);
            }
            catch (Exception ex)
            {
                SupportDatas.ToolIdPerDatabase = Convert.ToInt32(SupportDatas.LoginToolId);
                ex.ErrorLogEntry(MethodBase.GetCurrentMethod().Name);
            }
        }

        /// <summary>
        /// The logging in operation.
        /// </summary>
        /// <returns>Void because this represnts the awaitable operation.</returns>
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
                        }
                        else
                        {
                            new Exception("Username or password is not correct").ErrorLogEntry(MethodBase.GetCurrentMethod().Name);
                            MessageBox.Show("Username or password is not correct.", "Unable to Login");
                            return;
                        }
                        SupportDatas.CurrentLoginInformation = loginInformation;
                    }
                    else
                    {
                        new Exception("Username or password is not correct").ErrorLogEntry(MethodBase.GetCurrentMethod().Name);
                        MessageBox.Show("Username or password is not correct.", "Unable to Login");
                        return;
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
                    else
                    {
                        new Exception("Username or password is not correct").ErrorLogEntry(MethodBase.GetCurrentMethod().Name);
                        MessageBox.Show("Username or password is not correct.", "Unable to Login");
                        return;
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