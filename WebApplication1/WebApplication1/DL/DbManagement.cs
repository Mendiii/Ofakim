using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WebApplication1.Models;

namespace WebApplication1.DL
{
    public static class DbManagement
    {
        public static bool InsertNewUser(User user)
        {

            int Result = 0;
            try
            {
                using (SqlConnection mycon = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
                {
                    mycon.Open();
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "sp_add_new_user";
                        cmd.Connection = mycon;
                        cmd.Parameters.Add("@MailAddress", SqlDbType.NVarChar,50).Value = user.MailAddress;
                        cmd.Parameters.Add("@birthDate", SqlDbType.SmallDateTime).Value = user.birthDate;
                        cmd.Parameters.Add("@FullName", SqlDbType.NVarChar, 50).Value = user.FullName;
                        cmd.Parameters.Add("@PhoneNumber", SqlDbType.NVarChar, 50).Value = user.PhoneNumber;
                        cmd.Parameters.Add("@Gender", SqlDbType.NVarChar, 10).Value = user.Gender;
                        Result = int.Parse(cmd.ExecuteScalar().ToString());
                    }
                    mycon.Close();
                }
            }
            catch (Exception)
            {
                return false;
            }
            if (Result == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static string GetAllUsers()
        {
            DataTable dt = new DataTable();
            using (SqlConnection mycon = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandTimeout = 0;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "sp_get_all_users";
                    cmd.Connection = mycon;

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                    System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                    List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
                    Dictionary<string, object> row;
                    foreach (DataRow dr in dt.Rows)
                    {
                        row = new Dictionary<string, object>();
                        foreach (DataColumn col in dt.Columns)
                        {
                            row.Add(col.ColumnName, dr[col]);
                        }
                        rows.Add(row);
                    }
                    return serializer.Serialize(rows);
                }
            }
        }
    }
}
