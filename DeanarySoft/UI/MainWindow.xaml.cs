using DeanarySoft.DataLayer;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using DeanarySoft.DataLayer;
using DeanarySoft.BuisnessLayer;

namespace DeanarySoft {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
            DataContext = new MainViewModel();
        }

        private void StaffList_Click(object sender, RoutedEventArgs e) {
            Sourse.HaveListObjectsFromDataBase(1);
        }
        private void EquipmentList_Click(object sender, RoutedEventArgs e) {
            Sourse.HaveListObjectsFromDataBase(2);
        }

        public void FillList(List<IToStringValue> list) {
            ListView.Items.Clear();
            foreach (IToStringValue value in list) {
                ListView.Items.Add(value.ToString());
            }
        }

    }
}