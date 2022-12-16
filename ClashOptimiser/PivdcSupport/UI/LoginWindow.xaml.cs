using PivdcSupportModule;
using System;
using System.Windows;
using System.Windows.Input;

namespace PivdcSupportUi
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private LoggingInUiProcess LoggingInProcessObj { get; set; }

        /// <summary>
        /// The login window
        /// </summary>
        /// <param name="centralLoginConnectionString">The central login connection string</param>
        /// <param name="localDBLocation">The local db location</param>
        /// <param name="localDBConnectionString">The local db connection string</param>
        public LoginWindow(string centralLoginConnectionString, string localDBLocation, string localDBConnectionString)
        {
            LoggingInProcessObj = new LoggingInUiProcess(this, centralLoginConnectionString, localDBLocation, localDBConnectionString);
            InitializeComponent();
        }

        /// <summary>
        /// Run time key word pressing checking if the user press the correct key for logged in
        /// </summary>
        /// <param name="sender">The action with object information</param>
        /// <param name="e">The key event arguments</param>
        private void txtPasswordKeyDown(object sender, KeyEventArgs e)
        {
            LoggingInProcessObj.PasswordTextBoxKeyPressEvent(sender, e);
        }

        /// <summary>
        /// The login button clicked event to check the logged in information with central database
        /// </summary>
        /// <param name="sender">The action with object information</param>
        /// <param name="e">The routed event arguments</param>
        private void btnloginClicked(object sender, RoutedEventArgs e)
        {
            System.Threading.Tasks.Task task = LoggingInProcessObj.LoggedInAsync();
        }

        /// <summary>
        /// display the login window for user input
        /// </summary>
        /// <param name="sender">The action with object information</param>
        /// <param name="e">The routed event arguments</param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoggingInProcessObj.LoadLoginWindow(sender);
        }

        /// <summary>
        /// closing function of the loaded window
        /// </summary>
        /// <param name="sender">The action with object information</param>
        /// <param name="e">The event arguments</param>
        private void Window_Closed(object sender, EventArgs e)
        {
            Close();
        }
    }
}