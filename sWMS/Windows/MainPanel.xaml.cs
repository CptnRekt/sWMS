﻿using Microsoft.Data.SqlClient;
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
            DocumentsDataGrid.ItemsSource = documents.DefaultView;
            WarehousesDataGrid.ItemsSource = warehouses.DefaultView;
            ContractorsDataGrid.ItemsSource = contractors.DefaultView; 
            ArticlesDataGrid.ItemsSource = articles.DefaultView; 
            UnitsDataGrid.ItemsSource = units.DefaultView;
            AttrClassesDataGrid.ItemsSource = attrClasses.DefaultView;
            ConfigDataGrid.ItemsSource = config.DefaultView;
        }

        private void addWarehouseButton_Click(object sender, RoutedEventArgs e)
        {
            addNewChange(warehouses, WarehousesDataGrid);
        }

        private void addNewChange(DataTable dataTable, DataGrid dataGrid)
        {
            DataRow dataRow = dataTable.NewRow();
            DataGridRow dataGridRow = new DataGridRow();
            dataGridRow.Background = Brushes.LightGreen;
            dataGridRow.Item = dataRow;
            dataGrid.Items.Add(dataGridRow);
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

        private void saveSelectedRows(int index, int id, WMSObjectTypesEnum type)
        {
            SelectedRow selectedRow = new SelectedRow()
            {
                Index = index,
                SQL_Id = id,
                SQL_Type = type
            };
            selectedRows.Add(selectedRow);
        }

        private void unsaveSelectedRows(SelectedRow selectedRow)
        {
            selectedRows.Remove(selectedRow);
        }

        private void removeSelectedRows_Click(object sender, RoutedEventArgs e)
        {
            foreach (SelectedRow selectedRow in selectedRows)
            {
                List<SQLParameter> sqlParameters = new List<SQLParameter>();
                switch (selectedRow.SQL_Type)
                {
                    case WMSObjectTypesEnum.Document:
                        documents.Rows.RemoveAt(selectedRow.Index);
                        sqlParameters.Add(new SQLParameter("Doc_ObjectId", selectedRow.SQL_Id));
                        if (selectedRow.SQL_Id != null)
                            DataAccess.CallStoredProcedure("sWMS.RemoveDocument", sqlParameters);
                        break;
                    case WMSObjectTypesEnum.Warehouse:
                        warehouses.Rows.RemoveAt(selectedRow.Index);
                        sqlParameters.Add(new SQLParameter("Wh_Id", selectedRow.SQL_Id));
                        if (selectedRow.SQL_Id != null)
                            DataAccess.CallStoredProcedure("sWMS.RemoveWarehouse", sqlParameters);
                        break;
                    case WMSObjectTypesEnum.Contractor:
                        contractors.Rows.RemoveAt(selectedRow.Index);
                        sqlParameters.Add(new SQLParameter("Con_Id", selectedRow.SQL_Id));
                        if (selectedRow.SQL_Id != null)
                            DataAccess.CallStoredProcedure("sWMS.RemoveContractor", sqlParameters);
                        break;
                    case WMSObjectTypesEnum.Article:
                        articles.Rows.RemoveAt(selectedRow.Index);
                        sqlParameters.Add(new SQLParameter("Art_Id", selectedRow.SQL_Id));
                        if (selectedRow.SQL_Id != null)
                            DataAccess.CallStoredProcedure("sWMS.RemoveArticle", sqlParameters);
                        break;
                    case WMSObjectTypesEnum.Unit:
                        units.Rows.RemoveAt(selectedRow.Index);
                        sqlParameters.Add(new SQLParameter("Unit_Id", selectedRow.SQL_Id));
                        if (selectedRow.SQL_Id != null)
                            DataAccess.CallStoredProcedure("sWMS.RemoveUnit", sqlParameters);
                        break;
                    case WMSObjectTypesEnum.AttrClass:
                        attrClasses.Rows.RemoveAt(selectedRow.Index);
                        sqlParameters.Add(new SQLParameter("AtC_Id", selectedRow.SQL_Id));
                        if (selectedRow.SQL_Id != null)
                            DataAccess.CallStoredProcedure("sWMS.RemoveAttrClass", sqlParameters);
                        break;
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
            addNewChange(documents, DocumentsDataGrid);
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
            addNewChange(contractors, ContractorsDataGrid);
        }

        private void removeContractor_Click(object sender, RoutedEventArgs e)
        {

        }

        private void addArticleButton_Click(object sender, RoutedEventArgs e)
        {
            addNewChange(articles, ArticlesDataGrid);
        }

        private void removeArticle_Click(object sender, RoutedEventArgs e)
        {

        }

        private void addUnitButton_Click(object sender, RoutedEventArgs e)
        {
            addNewChange(units, UnitsDataGrid);
        }

        private void removeUnit_Click(object sender, RoutedEventArgs e)
        {

        }

        private void addAttrClassButton_Click(object sender, RoutedEventArgs e)
        {
            addNewChange(attrClasses, AttrClassesDataGrid);
        }

        private void removeAttrClass_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Checkbox_Click(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            //DataRow dtr = (DataRow)checkBox.DataContext;
            //Console.WriteLine(dtr.Table);
            Console.WriteLine(checkBox.DataContext);
        }
    }
}
