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
        static public void CreateNewWarehouse(Warehouse warehouse)
        {
            using (SqlConnection con = new SqlConnection(DataAccess.Builder.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sWMS.CreateNewWarehouse", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Wh_Code", warehouse.WhCode);
                    cmd.Parameters.AddWithValue("@Wh_Name", warehouse.WhName);
                    cmd.Parameters.AddWithValue("@Wh_Country", warehouse.WhCountry);
                    cmd.Parameters.AddWithValue("@Wh_City", warehouse.WhCity);
                    cmd.Parameters.AddWithValue("@Wh_Street", warehouse.WhStreet);
                    cmd.Parameters.AddWithValue("@Wh_Postal", warehouse.WhPostal);
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
        static public List<Warehouse> GetWarehouses()
        {
            using (SqlConnection con = new SqlConnection(DataAccess.Builder.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sWMS.GetWarehouses", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    List<Warehouse> warehouses = new List<Warehouse>();
                    while (reader.Read())
                    {
                        Warehouse warehouse = new Warehouse
                        {
                            WhCode = reader["Wh_Code"].ToString(),
                            WhName = reader["Wh_Name"].ToString(),
                            WhCountry = reader["Wh_Country"].ToString(),
                            WhCity = reader["Wh_City"].ToString(),
                            WhStreet = reader["Wh_Street"].ToString(),
                            WhPostal = reader["Wh_Postal"].ToString(),
                            WhAcceptancesNumber = Convert.ToInt32(reader["Wh_AcceptancesNumber"]),
                            WhIssuesNumber = Convert.ToInt32(reader["Wh_IssuesNumber"])
                        };
                        warehouses.Add(warehouse);
                    }
                    return warehouses;
                }
            }
        }
    }
}
