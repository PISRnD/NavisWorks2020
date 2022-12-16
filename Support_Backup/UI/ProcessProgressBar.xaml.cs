using PivdcNavisworksSupportModel;
using PivdcNavisworksSupportModule;
using System;
using System.Runtime.InteropServices;
using System.Windows;

namespace PiNavisworks.PiNavisworksSupport
{
    /// <summary>
    /// Interaction logic for ProcessProgressBar.xaml
    /// </summary>
    public partial class ProcessProgressBar : Window, IDisposable
    {
        public static double maxValue { get; set; }
        public static double minValue { get; set; }
        public static double currentValue { get; set; }
        public static Action actionRefresh { get; set; }
        public static bool CanContinue { get; set; }

        private const int GWL_STYLE = -16;
        private const int WS_SYSMENU = 0x80000;
        [DllImport("user32.dll", SetLastError = true)]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);
        [DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        public ProcessProgressBar()
        {
            InitializeComponent();
            actionRefresh = RefreshData;
            CanContinue = true;
        }

        public void Dispose()
        {
            //future code
        }

        public void RefreshData()
        {
            processProgressBar.Maximum = maxValue;
            processProgressBar.Minimum = minValue;
            processProgressBar.Value = currentValue;
            txtProgressdata.Text = string.Format("Processing {0}/{1}", currentValue, maxValue);
        }

        public void RefreshData(double maxValueTemp, double minValueTemp, double currentValueTemp)
        {
            maxValue = maxValueTemp;
            minValue = minValueTemp;
            currentValue = currentValueTemp;
        }

        private void btnCancelProcess_Click(object sender, RoutedEventArgs e)
        {
            CanContinue = false;
        }

        private void ProcessProgressBar_Loaded(object sender, RoutedEventArgs e)
        {
            var hwnd = new System.Windows.Interop.WindowInteropHelper(this).Handle;
            SetWindowLong(hwnd, GWL_STYLE, GetWindowLong(hwnd, GWL_STYLE) & ~WS_SYSMENU);
            Icon = sender.UserInterfaceIcon();
            Title = string.Format("Process Progress | {0}", SupportDatas.UserInterfaeTitle);
        }
    }
}