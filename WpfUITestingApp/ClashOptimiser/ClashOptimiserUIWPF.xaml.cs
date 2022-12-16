using DevExpress.Xpf.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ClashOptimiser.UI
{
    /// <summary>
    /// Interaction logic for ClashOptimiserUIWPF.xaml
    /// </summary>
    public partial class ClashOptimiserUIWPF : DevExpress.Xpf.Core.ThemedWindow
    {
        ObservableCollection<ClassDataTable> classDataTables = new ObservableCollection<ClassDataTable>();

        public ClashOptimiserUIWPF()
        {
            InitializeComponent();
            dataClashGrid.ItemsSource = classDataTables;
        }
        public class ClassDataTable
        {
            public string ClashTest { get; set; }
            public string NewOrActive { get; set; }
            public string Reviewed { get; set; }
            public string Approved { get; set; }
        }

    }
}
