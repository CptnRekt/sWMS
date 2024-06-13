using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using sWMS.DAO;
using sWMS.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Channels;
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
using System.Xml.Linq;
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
        List<DataRow> selectedRows = new List<DataRow>();

        public MainPanel()
        {
            DataAccess.InitializeConnection("(LocalDB)\\MSSQLLocalDB", "sa", "Rambo846303", "sWMS");
            LoadData();
            documents.TableName = "documents";
            warehouses.TableName = "warehouses";
            contractors.TableName = "contractors";
            articles.TableName = "articles";
            units.TableName = "units";
            attrClasses.TableName = "attrClasses";
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

        private void LoadData()
        {
            documents = DataAccess.CallStoredProcedure("sWMS.GetDocuments");
            warehouses = DataAccess.CallStoredProcedure("sWMS.GetWarehouses");
            contractors = DataAccess.CallStoredProcedure("sWMS.GetContractors");
            articles = DataAccess.CallStoredProcedure("sWMS.GetArticles");
            units = DataAccess.CallStoredProcedure("sWMS.GetUnits");
            attrClasses = DataAccess.CallStoredProcedure("sWMS.GetAttrClasses");
            config = DataAccess.CallStoredProcedure("sWMS.GetConfig");
        }

        private void removeSelectedRows_Click(object sender, RoutedEventArgs e)
        {
            foreach (DataRow dataRow in selectedRows)
            {
                DataTable dataTable = dataRow.Table;
                int id = Convert.ToInt32(dataRow.ItemArray[0]);
                List<DBParameter> dbParameters = new List<DBParameter>();
                try
                {
                    dataTable.Rows.Remove(dataRow);
                    if (id != null)
                        switch (dataTable.TableName)
                        {
                            case "documents":
                                dbParameters.Add(new DBParameter("Doc_ObjectId", id));
                                DataAccess.CallStoredProcedure("sWMS.RemoveDocument", dbParameters, false);
                                break;
                            case "warehouses":
                                dbParameters.Add(new DBParameter("Wh_Id", id));
                                DataAccess.CallStoredProcedure("sWMS.RemoveWarehouse", dbParameters, false);
                                break;
                            case "contractors":
                                dbParameters.Add(new DBParameter("Con_Id", id));
                                DataAccess.CallStoredProcedure("sWMS.RemoveContractor", dbParameters, false);
                                break;
                            case "articles":
                                dbParameters.Add(new DBParameter("Art_Id", id));
                                DataAccess.CallStoredProcedure("sWMS.RemoveArticle", dbParameters, false);
                                break;
                            case "units":
                                dbParameters.Add(new DBParameter("Unit_Id", id));
                                DataAccess.CallStoredProcedure("sWMS.RemoveUnit", dbParameters, false);
                                break;
                            case "attrClasses":
                                dbParameters.Add(new DBParameter("AtC_Id", id));
                                DataAccess.CallStoredProcedure("sWMS.RemoveAttrClass", dbParameters, false);
                                break;
                        }
                    dataTable.AcceptChanges();
                }
                catch (Exception ex) 
                {
                    dataTable.RejectChanges();
                    MessageBox.Show(ex.Message);
                }
            }
            selectedRows.Clear();
        }

        private void getChanges(DataTable dataTable)
        {
            DataTable changes = dataTable.GetChanges();
            if (changes != null && changes.Rows.Count > 0) 
            {
                try
                {
                    DataColumnCollection columns = changes.Columns;
                    foreach (DataRow dataRow in changes.Rows)
                    {
                        List<DBParameter> dbParameters = new List<DBParameter>();
                        for (int columnIndex = 0; columnIndex < columns.Count; columnIndex++)
                        {
                            object cellValue = dataRow[columnIndex];
                            DataColumn column = columns[columnIndex];
                            dbParameters.Add(new DBParameter(column.ColumnName, cellValue));
                        }
                        dbParameters = skipParameters(dbParameters);
                        saveToDB(dataTable, dbParameters);
                    }
                }
                catch (Exception ex)
                {
                    dataTable.RejectChanges();
                    MessageBox.Show(ex.Message);
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
            skipParameter(dbParameters, "Doc_NumberString");
            return dbParameters;
        }

        private void saveToDB(DataTable dataTable, List<DBParameter> dbParameters) 
        {
            string? id = dbParameters[0].Value.ToString();
            if (String.IsNullOrEmpty(id))
            {
                dbParameters.RemoveAt(0);
                switch (dataTable.TableName)
                {
                    case "documents":
                        dataTable = DataAccess.CallStoredProcedure("sWMS.AddDocument", dbParameters);
                        break;
                    case "warehouses":
                        dataTable = (DataAccess.CallStoredProcedure("sWMS.AddWarehouse", dbParameters));
                        break;
                    case "contractors":
                        dataTable = DataAccess.CallStoredProcedure("sWMS.AddContractor", dbParameters);
                        break;
                    case "articles":
                        dataTable = DataAccess.CallStoredProcedure("sWMS.AddArticle", dbParameters);
                        break;
                    case "units":
                        dataTable = DataAccess.CallStoredProcedure("sWMS.AddUnit", dbParameters);
                        break;
                    case "attrClasses":
                        dataTable = DataAccess.CallStoredProcedure("sWMS.AddAttrClass", dbParameters);
                        break;
                }
            }
            else
            {
                switch (dataTable.TableName)
                {
                    case "documents":
                        dataTable = DataAccess.CallStoredProcedure("sWMS.EditDocument", dbParameters);
                        break;
                    case "warehouses":
                        dataTable = DataAccess.CallStoredProcedure("sWMS.EditWarehouse", dbParameters);
                        break;
                    case "contractors":
                        dataTable = DataAccess.CallStoredProcedure("sWMS.EditContractor", dbParameters);
                        break;
                    case "articles":
                        dataTable = DataAccess.CallStoredProcedure("sWMS.EditArticle", dbParameters);
                        break;
                    case "units":
                        dataTable = DataAccess.CallStoredProcedure("sWMS.EditUnit", dbParameters);
                        break;
                    case "attrClasses":
                        dataTable = DataAccess.CallStoredProcedure("sWMS.EditAttrClass", dbParameters);
                        break;
                    case "Config":
                        dataTable = DataAccess.CallStoredProcedure("sWMS.EditConfig", dbParameters);
                        break;
                }
            }
            dataTable.AcceptChanges();
        }

        private void saveChanges_Click(object sender, RoutedEventArgs e)
        {
            foreach (DataTable dataTable in dataTables)
                getChanges(dataTable);
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
                selectedRows.Add(dataRow);
            else
                selectedRows.Remove(dataRow);
        }

        private void findStuffFromTextBox(object sender, RoutedEventArgs e)
        {
            TabItem tab = (TabItem)tabs.SelectedItem;
            string? tabHeader = tab.Header.ToString();
            TextBox textBox = (TextBox)sender;
            switch (tabHeader)
            {
                case "Przesunięcia magazynowe":
                    searchDataTable(DocumentsDataGrid, documents, textBox.Text);
                    break;
                case "Magazyny":
                    searchDataTable(WarehousesDataGrid, warehouses, textBox.Text);
                    break;
                case "Kontrahenci":
                    searchDataTable(ContractorsDataGrid, contractors, textBox.Text);
                    break;
                case "Kartoteki towarowe":
                    searchDataTable(ArticlesDataGrid, articles, textBox.Text);
                    break;
                case "Szablony jednostek":
                    searchDataTable(UnitsDataGrid, units, textBox.Text);
                    break;
                case "Szablony atrybutów":
                    searchDataTable(AttrClassesDataGrid, attrClasses, textBox.Text);
                    break;
                case "Ustawienia programu":
                    searchDataTable(ConfigDataGrid, config, textBox.Text);
                    break;
            }
        }

        private void searchDataTable(DataGrid dataGrid, DataTable dataTable, string text)
        {
            //DataTable dataTableNew = dataTable.Clone();
            //dataTableNew.Clear();
            //foreach (DataRow dataRow in dataTable.Rows)
            //{
            //    for (int columnIndex = 0; columnIndex < dataTable.Columns.Count; columnIndex++)
            //        if (dataRow[columnIndex].ToString().Contains(text))
            //            dataTableNew.ImportRow(dataRow);
            //}
            //if (dataTableNew.Rows.Count > 0)
            //    dataGrid.ItemsSource = dataTableNew.DefaultView;
        }
    }
}
