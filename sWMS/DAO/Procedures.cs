using Microsoft.Data.SqlClient;
using sWMS.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

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
    }
}
