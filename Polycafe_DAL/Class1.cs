using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using Polycafe_DTO;

namespace Polycafe_DAL
{
    public class DBUtil
    {
        private static string connectionString = "Data Source=SD20302\\ADMINCUTE;Initial Catalog=PolyCafe;Integrated Security=True;";

        public static SqlCommand GetCommand(string sql, List<object> args, CommandType cmdType)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(sql, conn)
            {
                CommandType = cmdType
            };
            for (int i = 0; i < args.Count; i++)
            {
                cmd.Parameters.AddWithValue($"@param{i}", args[i]);
            }
            return cmd;
        }

        public static void Update(string sql, List<object> args, CommandType cmdType)
        {
            using (SqlCommand cmd = GetCommand(sql, args, cmdType))
            {
                cmd.Connection.Open();
                using (var transaction = cmd.Connection.BeginTransaction())
                {
                    cmd.Transaction = transaction;
                    try
                    {
                        cmd.ExecuteNonQuery();
                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        public static SqlDataReader Query(string sql, List<object> args, CommandType cmdType = CommandType.Text)
        {
            try {
                SqlCommand cmd = GetCommand(sql, args, cmdType);
                cmd.Connection.Open();
                return cmd.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (Exception)
            {
                throw;
            }
            
        }

        public static Object Value(string sql, List<object> args, CommandType cmdType = CommandType.Text)
        {
            using (SqlCommand cmd = GetCommand(sql, args, cmdType))
            {
                cmd.Connection.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        var result = new object();
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            string columnName = reader.GetName(i);
                            PropertyInfo propertyInfo = result.GetType().GetProperty(columnName);
                            if (propertyInfo != null)
                            {
                                var value = reader.IsDBNull(i) ? null : reader[columnName];
                                propertyInfo.SetValue(result, value);
                            }
                        }
                        return result;
                    }
                }
            }
            return null;
        }
    }

    public class LoginDAL
    {
        public string GetMatKhau(string username)
        {
            string query = "SELECT MatKhau FROM NhanVien WHERE Email = @username";
            using (SqlCommand cmd = DBUtil.GetCommand(query, new List<object>(), CommandType.Text))
            {
                cmd.Parameters.AddWithValue("@username", username); // Ensure parameter is added
                cmd.Connection.Open();
                object result = cmd.ExecuteScalar();
                return result?.ToString().Trim();
            }
        }

        public string GetVaiTro(string username)
        {
            string query = "SELECT VaiTro FROM NhanVien WHERE Email = @username";
            using (SqlCommand cmd = DBUtil.GetCommand(query, new List<object>(), CommandType.Text))
            {
                cmd.Parameters.AddWithValue("@username", username); // Ensure parameter is added
                cmd.Connection.Open();
                object result = cmd.ExecuteScalar();
                return result?.ToString().Trim();
            }
        }
    }

}
