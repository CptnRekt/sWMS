using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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
using System.Windows.Controls.Primitives;
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
        List<SelectedRow> selectedRows = new List<SelectedRow>();

        public MainPanel()
        {
            DataAccess.InitializeConnection("(LocalDB)\\MSSQLLocalDB", "sa", "Rambo846303", "sWMS");
            documents = DataAccess.CallStoredProcedure("sWMS.GetDocuments");
            documents.TableName = "documents";
            warehouses = DataAccess.CallStoredProcedure("sWMS.GetWarehouses");
            warehouses.TableName = "warehouses";
            contractors = DataAccess.CallStoredProcedure("sWMS.GetContractors");
            contractors.TableName = "contractors";
            articles = DataAccess.CallStoredProcedure("sWMS.GetArticles");
            articles.TableName = "articles";
            units = DataAccess.CallStoredProcedure("sWMS.GetUnits");
            units.TableName = "units";
            attrClasses = DataAccess.CallStoredProcedure("sWMS.GetAttrClasses");
            attrClasses.TableName = "attrClasses";
            config = DataAccess.CallStoredProcedure("sWMS.GetConfig");
            config.TableName = "config";
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
            DocumentsDataGrid.ItemsSource = documents.DefaultView;
            WarehousesDataGrid.ItemsSource = warehouses.DefaultView;
            ContractorsDataGrid.ItemsSource = contractors.DefaultView; 
            ArticlesDataGrid.ItemsSource = articles.DefaultView; 
            UnitsDataGrid.ItemsSource = units.DefaultView;
            AttrClassesDataGrid.ItemsSource = attrClasses.DefaultView;
            ConfigDataGrid.ItemsSource = config.DefaultView;
        }

        //private DataRow searchDataTable(DataTable dataTable, int _Id, WMSObjectTypes _Type)
        //{
        //    foreach (DataRow dataRow in dataTable.Rows)
        //    {
        //        if (dataRow[0] == _Id.ToString() && dataRow[1] == _Type.ToString())
        //            return dataRow;
        //    }
        //    return null;
        //}

        private void saveSelectedRows(WMSObjectTypes type, DataRow dataRow)
        {
            SelectedRow selectedRow = new SelectedRow()
            {
                Type = type,
                AffectedRow = dataRow
            };
            selectedRows.Add(selectedRow);
        }

        private void unsaveSelectedRows(DataRow dataRow)
        {
            foreach(SelectedRow selectedRow in selectedRows)
            {
                if (selectedRow.AffectedRow == dataRow)
                {
                    selectedRows.Remove(selectedRow);
                    break;
                }
            }
        }

        private void removeSelectedRows_Click(object sender, RoutedEventArgs e)
        {
            foreach (SelectedRow selectedRow in selectedRows)
            {
                DataRow dataRow = selectedRow.AffectedRow;
                int id = Convert.ToInt32(dataRow.ItemArray[0]);
                List<DBParameter> dbParameters = new List<DBParameter>();
                switch (selectedRow.Type)
                {
                    case WMSObjectTypes.Document:
                        documents.Rows.Remove(dataRow);
                        dbParameters.Add(new DBParameter("Doc_ObjectId", id));
                        if (id != null)
                            DataAccess.CallStoredProcedure("sWMS.RemoveDocument", dbParameters, false);
                        break;
                    case WMSObjectTypes.Warehouse:
                        warehouses.Rows.Remove(dataRow);
                        dbParameters.Add(new DBParameter("Wh_Id", id));
                        if (id != null)
                            DataAccess.CallStoredProcedure("sWMS.RemoveWarehouse", dbParameters, false);
                        break;
                    case WMSObjectTypes.Contractor:
                        contractors.Rows.Remove(dataRow);
                        dbParameters.Add(new DBParameter("Con_Id", id));
                        if (id != null)
                            DataAccess.CallStoredProcedure("sWMS.RemoveContractor", dbParameters, false);
                        break;
                    case WMSObjectTypes.Article:
                        articles.Rows.Remove(dataRow);
                        dbParameters.Add(new DBParameter("Art_Id", id));
                        if (id != null)
                            DataAccess.CallStoredProcedure("sWMS.RemoveArticle", dbParameters, false);
                        break;
                    case WMSObjectTypes.Unit:
                        units.Rows.Remove(dataRow);
                        dbParameters.Add(new DBParameter("Unit_Id", id));
                        if (id != null)
                            DataAccess.CallStoredProcedure("sWMS.RemoveUnit", dbParameters, false);
                        break;
                    case WMSObjectTypes.AttrClass:
                        attrClasses.Rows.Remove(dataRow);
                        dbParameters.Add(new DBParameter("AtC_Id", id));
                        if (id != null)
                            DataAccess.CallStoredProcedure("sWMS.RemoveAttrClass", dbParameters, false);
                        break;
                }
            }
            selectedRows.Clear();
        }

        private void getChanges(DataTable dt)
        {
            DataTable changes = dt.GetChanges();
            if (changes != null && changes.Rows.Count > 0) 
            {
                DataColumnCollection columns = changes.Columns;
                foreach (DataRow row in changes.Rows)
                {
                    List<DBParameter> dbParameters = new List<DBParameter>();
                    for (int columnIndex = 0; columnIndex < columns.Count; columnIndex++)
                    {
                        object cellValue = row[columnIndex];
                        DataColumn column = columns[columnIndex];
                        //ehh, wiedzialem, ze to nie moze byc takie proste
                        //if (!column.ReadOnly)
                        dbParameters.Add(new DBParameter(column.ColumnName, cellValue));
                    }
                    dbParameters = skipParameters(dbParameters);
                    saveToDB(dt.TableName, dbParameters);
                }
            }
        }

        List<DBParameter> skipParameter(List<DBParameter> dbParameters, string name)
        {
            DBParameter? dbParameter = dbParameters.Find(a => { return a.Name == name; });
            if (dbParameter != null)
                dbParameters.Remove(dbParameter);
            return dbParameters;
        }

        private List<DBParameter> skipParameters(List<DBParameter> dbParameters)
        {
            skipParameter(dbParameters, "Wh_AcceptancesNumber");
            skipParameter(dbParameters, "Wh_IssuesNumber");
            return dbParameters;
        }

        private void saveToDB(string tableName, List<DBParameter> dbParameters) 
        {
            string? id = dbParameters[0].Value.ToString();
            if (String.IsNullOrEmpty(id))
            {
                dbParameters.RemoveAt(0);
                switch (tableName)
                {
                    case "documents":
                        DataAccess.CallStoredProcedure("sWMS.AddDocument", dbParameters, false);
                        break;
                    case "warehouses":
                        DataAccess.CallStoredProcedure("sWMS.AddWarehouse", dbParameters, false);
                        break;
                    case "contractors":
                        DataAccess.CallStoredProcedure("sWMS.AddContractor", dbParameters, false);
                        break;
                    case "articles":
                        DataAccess.CallStoredProcedure("sWMS.AddArticle", dbParameters, false);
                        break;
                    case "units":
                        DataAccess.CallStoredProcedure("sWMS.AddUnit", dbParameters, false);
                        break;
                    case "attrClasses":
                        DataAccess.CallStoredProcedure("sWMS.AddAttrClass", dbParameters, false);
                        break;
                }
            }
            else
            {
                switch (tableName)
                {
                    case "documents":
                        DataAccess.CallStoredProcedure("sWMS.EditDocument", dbParameters, false);
                        break;
                    case "warehouses":
                        DataAccess.CallStoredProcedure("sWMS.EditWarehouse", dbParameters, false);
                        break;
                    case "contractors":
                        DataAccess.CallStoredProcedure("sWMS.EditContractor", dbParameters, false);
                        break;
                    case "articles":
                        DataAccess.CallStoredProcedure("sWMS.EditArticle", dbParameters, false);
                        break;
                    case "units":
                        DataAccess.CallStoredProcedure("sWMS.EditUnit", dbParameters, false);
                        break;
                    case "attrClasses":
                        DataAccess.CallStoredProcedure("sWMS.EditAttrClass", dbParameters, false);
                        break;
                    case "Config":
                        DataAccess.CallStoredProcedure("sWMS.EditConfig", dbParameters, false);
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
            DocumentsDataGrid.CanUserAddRows = true;
            
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

        private void removeContractor_Click(object sender, RoutedEventArgs e)
        {

        }

        private void removeArticle_Click(object sender, RoutedEventArgs e)
        {

        }

        private void removeUnit_Click(object sender, RoutedEventArgs e)
        {

        }

        private void removeAttrClass_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Checkbox_Click(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            DataRowView dataRowView = (DataRowView)checkBox.DataContext;
            DataRow dataRow = dataRowView.Row;
            int id = Convert.ToInt32(dataRow.ItemArray[0]);
            if (checkBox.IsChecked.Value == true)
            {
                switch (checkBox.Name)
                {
                    case "DocumentsCheckbox":
                        saveSelectedRows(WMSObjectTypes.Document, dataRow);
                        break;
                    case "WarehousesCheckbox":
                        saveSelectedRows(WMSObjectTypes.Warehouse, dataRow);
                        break;
                    case "ContractorsCheckbox":
                        saveSelectedRows(WMSObjectTypes.Contractor, dataRow);
                        break;
                    case "ArticlesCheckbox":
                        saveSelectedRows(WMSObjectTypes.Article, dataRow);
                        break;
                    case "UnitsCheckbox":
                        saveSelectedRows(WMSObjectTypes.Document, dataRow);
                        break;
                    case "AttrClasses":
                        saveSelectedRows(WMSObjectTypes.Document, dataRow);
                        break;
                }

            }
            else
                unsaveSelectedRows(dataRow);
        }

    }
}
