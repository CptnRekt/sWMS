using Microsoft.Data.SqlClient;
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
        List<DataTable> dataTables;
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
            dataTables = new List<DataTable>()
            {
                documents,
                warehouses,
                contractors,
                articles,
                units,
                attrClasses,
                config
            };
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
            addNewChange(WarehousesDataGrid);
        }

        private void addNewChange(DataGrid dataGrid)
        {
            DataGridRow row = new DataGridRow();
            row.Background = Brushes.LightGreen;
            dataGrid.Items.Add(row);
            //UnsavedChange change = new UnsavedChange()
            //{
            //    Index = _Index,
            //    SQL_Type = _Type,
            //    DataOperation = DataOperationsEnum.Edit
            //};
            //unsavedChanges.Add(change);
        }

        //private DataRow searchDataTable(DataTable dataTable, int _Id, WMSObjectTypesEnum _Type)
        //{
        //    foreach (DataRow dataRow in dataTable.Rows)
        //    {
        //        if (dataRow[0] == _Id.ToString() && dataRow[1] == _Type.ToString())
        //            return dataRow;
        //    }
        //    return null;
        //}

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
                            if (change.SQL_Id != null)
                                DataAccess.CallStoredProcedure("sWMS.RemoveDocument", sqlParameters);
                            break;
                        case WMSObjectTypesEnum.Warehouse:
                            warehouses.Rows.RemoveAt(change.Index);
                            sqlParameters.Add(new SQLParameter("Wh_Id", change.SQL_Id));
                            if (change.SQL_Id != null)
                                DataAccess.CallStoredProcedure("sWMS.RemoveWarehouse", sqlParameters);
                            break;
                        case WMSObjectTypesEnum.Contractor:
                            contractors.Rows.RemoveAt(change.Index);
                            sqlParameters.Add(new SQLParameter("Con_Id", change.SQL_Id));
                            if (change.SQL_Id != null)
                                DataAccess.CallStoredProcedure("sWMS.RemoveContractor", sqlParameters);
                            break;
                        case WMSObjectTypesEnum.Article:
                            articles.Rows.RemoveAt(change.Index);
                            sqlParameters.Add(new SQLParameter("Art_Id", change.SQL_Id));
                            if (change.SQL_Id != null)
                                DataAccess.CallStoredProcedure("sWMS.RemoveArticle", sqlParameters);
                            break;
                        case WMSObjectTypesEnum.Unit:
                            units.Rows.RemoveAt(change.Index);
                            sqlParameters.Add(new SQLParameter("Unit_Id", change.SQL_Id));
                            if (change.SQL_Id != null)
                                DataAccess.CallStoredProcedure("sWMS.RemoveUnit", sqlParameters);
                            break;
                        case WMSObjectTypesEnum.AttrClass:
                            attrClasses.Rows.RemoveAt(change.Index);
                            sqlParameters.Add(new SQLParameter("AtC_Id", change.SQL_Id));
                            if (change.SQL_Id != null)
                                DataAccess.CallStoredProcedure("sWMS.RemoveAttrClass", sqlParameters);
                            break;
                    }
                }
            }
        }

        private void getChanges(DataTable dt)
        {
            DataTable changes = dt.GetChanges();
            if (changes != null && changes.Rows.Count > 0) 
            {
                foreach (DataRow row in changes.Rows)
                {
                    int columnIndex = 0;
                    List<SQLParameter> sqlParameters = new List<SQLParameter>();
                    foreach (DataColumn column in changes.Columns)
                    {
                        sqlParameters.Add(new SQLParameter(column.ColumnName, row[columnIndex]));
                        columnIndex++;
                    }
                    saveToDB(dt, row, sqlParameters);
                }
            }
        }

        private void saveToDB(DataTable dt, DataRow row, List<SQLParameter> sqlParameters) 
        {
            if (row[0] == null && row[1] == null)
            {
                switch (dt.TableName)
                {
                    case "documents":
                        DataAccess.CallStoredProcedure("sWMS.AddDocument", sqlParameters);
                        break;
                    case "warehouses":
                        DataAccess.CallStoredProcedure("sWMS.AddWarehouse", sqlParameters);
                        break;
                    case "contractors":
                        DataAccess.CallStoredProcedure("sWMS.AddContractor", sqlParameters);
                        break;
                    case "articles":
                        DataAccess.CallStoredProcedure("sWMS.AddArticle", sqlParameters);
                        break;
                    case "units":
                        DataAccess.CallStoredProcedure("sWMS.AddUnit", sqlParameters);
                        break;
                    case "attrClasses":
                        DataAccess.CallStoredProcedure("sWMS.AddAttrClass", sqlParameters);
                        break;
                }
            }
            else
            {
                switch (dt.TableName)
                {
                    case "documents":
                        DataAccess.CallStoredProcedure("sWMS.EditDocument", sqlParameters);
                        break;
                    case "warehouses":
                        DataAccess.CallStoredProcedure("sWMS.EditWarehouse", sqlParameters);
                        break;
                    case "contractors":
                        DataAccess.CallStoredProcedure("sWMS.EditContractor", sqlParameters);
                        break;
                    case "articles":
                        DataAccess.CallStoredProcedure("sWMS.EditArticle", sqlParameters);
                        break;
                    case "units":
                        DataAccess.CallStoredProcedure("sWMS.EditUnit", sqlParameters);
                        break;
                    case "attrClasses":
                        DataAccess.CallStoredProcedure("sWMS.EditAttrClass", sqlParameters);
                        break;
                    case "Config":
                        DataAccess.CallStoredProcedure("sWMS.EditConfig", sqlParameters);
                        break;
                }
            }
        }

        private void saveChanges_Click(object sender, RoutedEventArgs e)
        {
            foreach (DataTable dt in dataTables)
                getChanges(dt);
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
