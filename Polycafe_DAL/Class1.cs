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
    public class NhanVienDAL
    {
        private string connectionString;

        public NhanVienDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public bool CheckCredentials(string email, string password)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT COUNT(*) FROM NhanVien WHERE Email = @Email AND MatKhau = @Password";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Email", email);
                        command.Parameters.AddWithValue("@Password", password);
                        int count = (int)command.ExecuteScalar();
                        return count > 0;
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Lỗi kết nối hoặc truy vấn database: " + ex.Message);
                }
            }
        }

        public bool CheckEmailExists(string email)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT COUNT(*) FROM NhanVien WHERE Email = @Email";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Email", email);
                        int count = (int)command.ExecuteScalar();
                        return count > 0;
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Lỗi kết nối hoặc truy vấn database: " + ex.Message);
                }
            }
        }

        public bool UpdatePassword(string email, string newPassword)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "UPDATE NhanVien SET MatKhau = @NewPassword WHERE Email = @Email";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Email", email);
                        command.Parameters.AddWithValue("@NewPassword", newPassword);
                        int rowsAffected = command.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Lỗi kết nối hoặc truy vấn database: " + ex.Message);
                }
            }
        }
    }
    public class qlLSP_DAL
    {

        private string connString = "Data Source=SD20302\\ADMINCUTE;Initial Catalog=PolyCafe;Integrated Security=True;";

        public DataTable GetAll()
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                try
                {
                    string query = "SELECT MaLoai, TenLoai, GhiChu FROM LoaiSanPham"; // Chọn rõ ràng các cột
                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    return dt;
                }
                catch (SqlException)
                {
                    return null;
                }
                catch (Exception)
                {

                    return null; // Trả về null khi có lỗi
                }
            }
        }

        public bool Check(string maloai)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT COUNT(*) FROM LoaiSanPham WHERE MaLoai = @MaLoai";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@MaLoai", maloai);
                    return (int)cmd.ExecuteScalar() > 0;
                }
                catch (SqlException ex)
                {
                    System.Diagnostics.Debug.WriteLine($"SQL Error in Check: {ex.Message}");
                    return false;
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"General Error in Check: {ex.Message}");
                    return false;
                }
            }
        }

        public int Add(qlLSP lsp)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                try
                {
                    conn.Open();
                    string query = "INSERT INTO LoaiSanPham (MaLoai, TenLoai, GhiChu) VALUES (@MaLoai, @TenLoai, @GhiChu)";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@MaLoai", lsp.MaLoai);
                    cmd.Parameters.AddWithValue("@TenLoai", lsp.TenLoai);
                    cmd.Parameters.AddWithValue("@GhiChu", lsp.GhiChu);
                    return cmd.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    System.Diagnostics.Debug.WriteLine($"SQL Error in Add: {ex.Message}");
                    return -1; // Trả về -1 để báo hiệu lỗi
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"General Error in Add: {ex.Message}");
                    return -1;
                }
            }
        }

        public int Update(qlLSP lsp)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                try
                {
                    conn.Open();
                    string query = "UPDATE LoaiSanPham SET TenLoai = @TenLoai, GhiChu = @GhiChu WHERE MaLoai = @MaLoai";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@MaLoai", lsp.MaLoai);
                    cmd.Parameters.AddWithValue("@TenLoai", lsp.TenLoai);
                    cmd.Parameters.AddWithValue("@GhiChu", lsp.GhiChu);
                    return cmd.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    System.Diagnostics.Debug.WriteLine($"SQL Error in Update: {ex.Message}");
                    return -1;
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"General Error in Update: {ex.Message}");
                    return -1;
                }
            }
        }

        public int Delete(qlLSP lsp)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                try
                {
                    conn.Open();
                    string query = "DELETE FROM LoaiSanPham WHERE MaLoai = @MaLoai";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@MaLoai", lsp.MaLoai);
                    return cmd.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    System.Diagnostics.Debug.WriteLine($"SQL Error in Delete: {ex.Message}");
                    if (ex.Number == 547) // Foreign Key Violation error number
                    {
                    }
                    else
                    {
                    }
                    return -1;
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"General Error in Delete: {ex.Message}");
                    return -1;
                }
            }
        }

        public List<string> LoadMaLoai()
        {
            List<string> maloai = new List<string>();
            using (SqlConnection conn = new SqlConnection(connString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT DISTINCT MaLoai FROM LoaiSanPham";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        maloai.Add(reader["MaLoai"].ToString());
                    }
                    reader.Close(); // Đóng reader
                    return maloai;
                }
                catch (SqlException ex)
                {
                    System.Diagnostics.Debug.WriteLine($"SQL Error in LoadMaLoai: {ex.Message}");
                    return null; // Trả về null khi có lỗi
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"General Error in LoadMaLoai: {ex.Message}");
                    return null;
                }
            }
        }
    }

}
