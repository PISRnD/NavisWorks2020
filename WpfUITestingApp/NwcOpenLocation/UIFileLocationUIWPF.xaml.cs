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

namespace NwcOpenLocation.UI
{
    /// <summary>
    /// Interaction logic for UIFileLocationUIWPF.xaml
    /// </summary>
    public partial class UIFileLocationUIWPF : DevExpress.Xpf.Core.ThemedWindow
    {
        ObservableCollection<ClassDataTable> classDataTables = new ObservableCollection<ClassDataTable>();
        public UIFileLocationUIWPF()
        {
            InitializeComponent();
            dataGridViewFileInfo.ItemsSource = classDataTables;
        }   
    }
    public class ClassDataTable
    {
        public string FileName { get; set; }
        public Url FileLocation { get; set; }
    }
}
