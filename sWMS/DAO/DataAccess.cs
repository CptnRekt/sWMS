using Microsoft.Data.SqlClient;
using Microsoft.Win32.SafeHandles;
using sWMS.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using static sWMS.Models.App;
using static System.Net.Mime.MediaTypeNames;

namespace sWMS.DAO
{
    public static class DataAccess
    {
        public static SqlConnectionStringBuilder Builder { get; set; }
        public static void InitializeConnection(string dataSource, string login, string password, string initialCatalog)
        {
            Builder = new SqlConnectionStringBuilder();
            Builder.DataSource = dataSource;
            Builder.UserID = login;
            Builder.Password = password;
            Builder.InitialCatalog = initialCatalog;
        }
        public static DataTable CallStoredProcedure(string storedProcedureName, List<DBParameter> sqlParameters = null, bool getData = true)
        {
            using (SqlConnection con = new SqlConnection(DataAccess.Builder.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(storedProcedureName, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    if (sqlParameters != null)
                        foreach (DBParameter param in sqlParameters)
                            cmd.Parameters.AddWithValue(param.Name, param.Value);
                    con.Open();
                    DataTable dt = new DataTable();
                    if (getData)
                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                            adapter.Fill(dt);
                    else
                        cmd.ExecuteNonQuery();
                    return dt;
                }
            }
        }
    }
}
