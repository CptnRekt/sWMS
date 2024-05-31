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
    /// Interaction logic for WarehousesList.xaml
    /// </summary>
    public partial class WarehousesList : Window
    {
        private ObservableCollection<Warehouse> warehouses = new ObservableCollection<Warehouse>();
        public WarehousesList()
        {
            WarehousesDataGrid.DataContext = warehouses;
            InitializeComponent();
        }

        private void addWarehouseButton_Click(object sender, RoutedEventArgs e)
        {
            WarehouseDetails warehouseDetails = new WarehouseDetails();
            warehouseDetails.ShowDialog();
        }

        private void removeWarehouse_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void openDocumentsList_Click(object sender, RoutedEventArgs e)
        {
            AllDocumentsList allDocumentsList = new AllDocumentsList();
            allDocumentsList.ShowDialog();
        }

        private void findWarehouseButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void editWarehouse_Click(object sender, RoutedEventArgs e)
        {
            WarehouseDetails warehouseDetails = new WarehouseDetails();
            warehouseDetails.ShowDialog();
        }
    }
}
