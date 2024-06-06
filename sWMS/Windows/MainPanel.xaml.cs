using Microsoft.EntityFrameworkCore.Migrations.Operations;
using sWMS.DAO;
using sWMS.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static sWMS.Models.Enums;

namespace sWMS.Windows
{
    /// <summary>
    /// Interaction logic for Attributes.xaml
    /// </summary>
    public partial class MainPanel : Window
    {
        DataTable documents;
        DataTable warehouses;
        DataTable contractors;
        DataTable articles;
        DataTable units;
        DataTable attrClasses;
        DataTable config;
        List<UnsavedChange> unsavedChanges = new List<UnsavedChange>();

        public MainPanel()
        {
            DataAccess.InitializeConnection("(LocalDB)\\MSSQLLocalDB", "sa", "Rambo846303", "sWMS");
            documents = DataAccess.CallStoredProcedure("sWMS.GetDocuments");
            warehouses = DataAccess.CallStoredProcedure("sWMS.GetWarehouses");
            contractors = DataAccess.CallStoredProcedure("sWMS.GetContractors");
            articles = DataAccess.CallStoredProcedure("sWMS.GetArticles");
            units = DataAccess.CallStoredProcedure("sWMS.GetUnits");
            attrClasses = DataAccess.CallStoredProcedure("sWMS.GetAttrClasses");
            config = DataAccess.CallStoredProcedure("sWMS.GetConfig");
            InitializeComponent();
            DocumentsDataGrid.DataContext = documents;
            WarehousesDataGrid.DataContext = warehouses;
            ContractorsDataGrid.DataContext = contractors; 
            ArticlesDataGrid.DataContext = articles; 
            UnitsDataGrid.DataContext = units;
            AttrClassesDataGrid.DataContext = attrClasses;
            ConfigDataGrid.DataContext = config;
        }

        private void addWarehouseButton_Click(object sender, RoutedEventArgs e)
        {
            // Get the element that handled the event.
            //FrameworkElement fe = (FrameworkElement)sender;
            //Console.WriteLine(fe.Name);
            int Index = warehouses.Rows.Count;
            addNewChange(warehouses, Index, WMSObjectTypesEnum.Warehouse);
        }

        private void addNewChange(DataTable dataTable, int _Index, WMSObjectTypesEnum _Type)
        {
            DataGridRow row = new DataGridRow();
            row.Background = Brushes.LightGreen;
            dataTable.Rows.Add(row);
            UnsavedChange change = new UnsavedChange()
            {
                Index = _Index,
                SQL_Type = _Type,
                DataOperation = DataOperationsEnum.Add
            };
            unsavedChanges.Add(change);
        }

        private DataRow searchDataTable(DataTable dataTable, int _Id, WMSObjectTypesEnum _Type)
        {
            foreach (DataRow dataRow in dataTable.Rows)
            {
                if (dataRow[0] == _Id.ToString() && dataRow[1] == _Type.ToString())
                    return dataRow;
            }
            return null;
        }

        private void onChecked(DataTable dataTable, int _Index, int _Id, WMSObjectTypesEnum _Type)
        {
            UnsavedChange change = new UnsavedChange()
            {
                Index = _Index,
                SQL_Id = _Id,
                SQL_Type = _Type,
                DataOperation = DataOperationsEnum.Delete
            };
            unsavedChanges.Add(change);
        }

        private void removeSelected_Click(object sender, RoutedEventArgs e)
        {
            foreach (UnsavedChange change in unsavedChanges)
            {
                if (change.DataOperation == DataOperationsEnum.Delete)
                {
                    List<SQLParameter> sqlParameters = new List<SQLParameter>();
                    switch (change.SQL_Type)
                    {
                        case WMSObjectTypesEnum.Document:
                            documents.Rows.RemoveAt(change.Index);
                            sqlParameters.Add(new SQLParameter("Doc_ObjectId", change.SQL_Id));
                            DataAccess.CallStoredProcedure("sWMS.RemoveDocument", sqlParameters);
                            break;
                        case WMSObjectTypesEnum.Warehouse:
                            warehouses.Rows.RemoveAt(change.Index);
                            sqlParameters.Add(new SQLParameter("Wh_Id", change.SQL_Id));
                            DataAccess.CallStoredProcedure("sWMS.RemoveWarehouse", sqlParameters);
                            break;
                        case WMSObjectTypesEnum.Contractor:
                            contractors.Rows.RemoveAt(change.Index);
                            sqlParameters.Add(new SQLParameter("Con_Id", change.SQL_Id));
                            DataAccess.CallStoredProcedure("sWMS.RemoveContractor", sqlParameters);
                            break;
                        case WMSObjectTypesEnum.Article:
                            articles.Rows.RemoveAt(change.Index);
                            sqlParameters.Add(new SQLParameter("Art_Id", change.SQL_Id));
                            DataAccess.CallStoredProcedure("sWMS.RemoveArticle", sqlParameters);
                            break;
                        case WMSObjectTypesEnum.Unit:
                            units.Rows.RemoveAt(change.Index);
                            sqlParameters.Add(new SQLParameter("Unit_Id", change.SQL_Id));
                            DataAccess.CallStoredProcedure("sWMS.RemoveUnit", sqlParameters);
                            break;
                        case WMSObjectTypesEnum.AttrClass:
                            attrClasses.Rows.RemoveAt(change.Index);
                            sqlParameters.Add(new SQLParameter("AtC_Id", change.SQL_Id));
                            DataAccess.CallStoredProcedure("sWMS.RemoveAttrClass", sqlParameters);
                            break;
                    }
                }
            }
        }

        private void saveChanges_Click(object sender, RoutedEventArgs e)
        {

            //IEnumerable<DataGridRow> rowsCollection = mainGrid.Children.OfType<DataGridRow>();
            //foreach (DataGridRow row in rowsCollection)
            //{
            //    if (row.Background = Brushes.LightGreen)
            //    {
            //        switch (row.Parent.GetValue)
            //        {
            //            case "WarehouseDataGridRow":
            //                Console.WriteLine("WarehouseDtg");
            //            break;
            //        }
            //    }
            //}
        }

        private void findAttributeButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DocumentsDataGrid_AddingNewItem(object sender, AddingNewItemEventArgs e)
        {

        }

        private void DocumentsDataGrid_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {

        }

        private void DocumentsDataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {

        }

        private void showDestination_Click(object sender, RoutedEventArgs e)
        {

        }

        private void WarehousesDataGrid_AutoGeneratedColumns(object sender, EventArgs e)
        {

        }

        private void WarehousesDataGrid_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {

        }

        private void WarehousesDataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {

        }

        private void ContractorsDataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {

        }

        private void ContractorsDataGrid_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {

        }

        private void ArticlesDataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {

        }

        private void ArticlesDataGrid_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {

        }

        private void UnitsDataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {

        }

        private void addDocumentButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void removeDocument_Click(object sender, RoutedEventArgs e)
        {

        }

        private void showSender_Click(object sender, RoutedEventArgs e)
        {

        }

        private void removeWarehouse_Click(object sender, RoutedEventArgs e)
        {

        }

        private void editWarehouse_Click(object sender, RoutedEventArgs e)
        {

        }

        private void addContractorButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void removeContractor_Click(object sender, RoutedEventArgs e)
        {

        }

        private void addArticleButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void removeArticle_Click(object sender, RoutedEventArgs e)
        {

        }

        private void addUnitButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void removeUnit_Click(object sender, RoutedEventArgs e)
        {

        }

        private void addAttrClassButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void removeAttrClass_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
