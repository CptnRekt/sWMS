using Microsoft.Data.SqlClient;
using Microsoft.Win32.SafeHandles;
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

        //public static int ExecuteNonQuery(string objectName, object[] parameters = null)
        //{
        //    try
        //    {
        //        using (SqlConnection con = new SqlConnection(builder.ConnectionString))
        //        {
        //            using (SqlCommand cmd = new SqlCommand(objectName, con))
        //            {
        //                cmd.CommandType = CommandType.StoredProcedure;
        //                foreach (string parameter in parameters)
        //                    cmd.Parameters.Add(new SqlParameter($"@{parameter}", parameter));
        //                con.Open();
        //                return cmd.ExecuteNonQuery();
        //            }
        //        }
        //    }
        //    catch (Exception ex) 
        //    {
        //        throw ex;
        //    }
        //}

    }
}
