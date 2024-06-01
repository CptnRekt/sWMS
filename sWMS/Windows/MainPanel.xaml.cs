using sWMS.DAO;
using sWMS.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace sWMS.Windows
{
    /// <summary>
    /// Interaction logic for Attributes.xaml
    /// </summary>
    public partial class MainPanel : Window
    {
        ObservableCollection<Warehouse> warehouses;
        public MainPanel()
        {
            DataAccess.InitializeConnection("(LocalDB)\\MSSQLLocalDB", "sa", "Rambo846303", "sWMS");
            warehouses = new ObservableCollection<Warehouse>(DataAccess.ExecuteStoredProcedure("ab"));
            InitializeComponent();
        }

        private void addWarehouseButton_Click(object sender, RoutedEventArgs e)
        {
            WarehousesDataGrid.
        }
    }
}
