using Microsoft.Data.SqlClient;
using sWMS.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace sWMS.DAO
{
    public static class Procedures
    {
        static public void CreateNewWarehouse(string WhCode, string WhName, string WhCountry, string WhCity, string WhStreet, string WhPostal)
        {
            using (SqlConnection con = new SqlConnection(DataAccess.Builder.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sWMS.CreateNewWarehouse", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Wh_Code", WhCode);
                    cmd.Parameters.AddWithValue("@Wh_Name", WhName);
                    cmd.Parameters.AddWithValue("@Wh_Country", WhCountry);
                    cmd.Parameters.AddWithValue("@Wh_City", WhCity);
                    cmd.Parameters.AddWithValue("@Wh_Street", WhStreet);
                    cmd.Parameters.AddWithValue("@Wh_Postal", WhPostal);
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
        static public DataTable GetWarehouses()
        {
            using (SqlConnection con = new SqlConnection(DataAccess.Builder.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sWMS.GetWarehouses", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    DataTable warehouses = new DataTable();
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        adapter.Fill(warehouses);                    
                    return warehouses;
                }
            }
        }
    }
}
