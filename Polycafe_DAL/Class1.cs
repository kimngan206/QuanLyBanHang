using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using Polycafe_DTO;
using System.Linq;
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
   
    public class SanPhamDAL
    {
        private static string connectionString = "Data Source=SD20302\\ADMINCUTE;Initial Catalog=PolyCafe;Integrated Security=True;";
        public DataTable GetAllSanPham()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM SanPham", conn);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                return dt;
            }
        }
        public bool AddSanPham(SanPhamDTO sanPham)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO SanPham (MaSanPham, TenSanPham, MaLoai, DonGia, TrangThai, HinhAnh) VALUES (@MaSanPham, @TenSanPham, @MaLoai, @DonGia, @TrangThai, @HinhAnh)", conn);
                cmd.Parameters.AddWithValue("@MaSanPham", sanPham.MaSanPham);
                cmd.Parameters.AddWithValue("@TenSanPham", sanPham.TenSanPham);
                cmd.Parameters.AddWithValue("@MaLoai", sanPham.MaLoai);
                cmd.Parameters.AddWithValue("@DonGia", sanPham.DonGia);
                cmd.Parameters.AddWithValue("@TrangThai", sanPham.TrangThai);
                cmd.Parameters.AddWithValue("@HinhAnh", sanPham.HinhAnh);
                return cmd.ExecuteNonQuery() > 0;
            }
        }
        public bool UpdateSanPham(SanPhamDTO sanPham)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("UPDATE SanPham SET TenSanPham = @TenSanPham, MaLoai = @MaLoai, DonGia = @DonGia, TrangThai = @TrangThai, HinhAnh = @HinhAnh WHERE MaSanPham = @MaSanPham", conn);
                cmd.Parameters.AddWithValue("@MaSanPham", sanPham.MaSanPham);
                cmd.Parameters.AddWithValue("@TenSanPham", sanPham.TenSanPham);
                cmd.Parameters.AddWithValue("@MaLoai", sanPham.MaLoai);
                cmd.Parameters.AddWithValue("@DonGia", sanPham.DonGia);
                cmd.Parameters.AddWithValue("@TrangThai", sanPham.TrangThai);
                cmd.Parameters.AddWithValue("@HinhAnh", sanPham.HinhAnh);
                return cmd.ExecuteNonQuery() > 0;
            }
        }
        public bool DeleteSanPham(string maSanPham)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("DELETE FROM SanPham WHERE MaSanPham = @MaSanPham", conn);
                cmd.Parameters.AddWithValue("@MaSanPham", maSanPham);
                return cmd.ExecuteNonQuery() > 0;
            }
        }
        public DataTable SearchSanPham(string searchTerm)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                // --- CORRECTED: Select all columns for proper DataGridView display ---
                // Changed from SELECT MaSanPham to SELECT *
                // Changed search column from MaSanPham to TenSanPham (more common for text search)
                SqlCommand cmd = new SqlCommand("SELECT * FROM SanPham WHERE TenSanPham LIKE @SearchTerm OR MaSanPham LIKE @SearchTerm", conn);
                cmd.Parameters.AddWithValue("@SearchTerm", "%" + searchTerm + "%");
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                return dt;
            }
        }
        public DataTable GetLoaiSanPham()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM LoaiSanPham", conn);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                return dt;
            }
        }
    }
    // Lớp tiện ích quản lý chuỗi kết nối
    public static class DBConnection
    {
        public static string ConnectionString { get; private set; }

        static DBConnection()
        {
            ConnectionString = "Data Source=SD20302\\ADMINCUTE;Initial Catalog=PolyCafe;Integrated Security=True;";
        }
    }

    public class SaleInvoiceDAL
    {
        // Lấy tất cả thông tin thẻ lưu động cho ComboBox
        public List<CardDTO> GetAllCards()
        {
            List<CardDTO> cards = new List<CardDTO>();
            string query = "SELECT MaThe FROM TheLuuDong WHERE TrangThai = 1"; // Chỉ lấy thẻ đang hoạt động

            using (SqlConnection connection = new SqlConnection(DBConnection.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            cards.Add(new CardDTO
                            {
                                CardId = reader["MaThe"].ToString(),
                                //OwnerName = reader["ChuSoHuu"].ToString()
                            });
                        }
                    }
                }
            }
            return cards;
        }

        // Lấy tất cả thông tin nhân viên cho ComboBox
        public List<EmployeeDto> GetAllEmployees()
        {
            List<EmployeeDto> employees = new List<EmployeeDto>();
            string query = "SELECT MaNhanVien, HoTen FROM NhanVien WHERE TrangThai = 1"; // Chỉ lấy nhân viên đang hoạt động

            using (SqlConnection connection = new SqlConnection(DBConnection.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            employees.Add(new EmployeeDto
                            {
                                EmployeeId = reader["MaNhanVien"].ToString(),
                                FullName = reader["HoTen"].ToString()
                            });
                        }
                    }
                }
            }
            return employees;
        }

        // Lấy tất cả thông tin sản phẩm cho ComboBox và lấy đơn giá
        public List<ProductDTO> GetAllProducts()
        {
            List<ProductDTO> products = new List<ProductDTO>();
            string query = "SELECT MaSanPham, TenSanPham, DonGia FROM SanPham WHERE TrangThai = 1"; // Chỉ lấy sản phẩm đang hoạt động

            using (SqlConnection connection = new SqlConnection(DBConnection.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            products.Add(new ProductDTO
                            {
                                ProductId = reader["MaSanPham"].ToString(),
                                ProductName = reader["TenSanPham"].ToString(),
                                UnitPrice = Convert.ToDecimal(reader["DonGia"])
                            });
                        }
                    }
                }
            }
            return products;
        }

        // Lấy đơn giá của một sản phẩm theo Mã sản phẩm
        public decimal GetProductUnitPrice(string productId)
        {
            decimal unitPrice = 0;
            string query = "SELECT DonGia FROM SanPham WHERE MaSanPham = @MaSanPham";

            using (SqlConnection connection = new SqlConnection(DBConnection.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@MaSanPham", productId);
                    connection.Open();
                    object result = command.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        unitPrice = Convert.ToDecimal(result);
                    }
                }
            }
            return unitPrice;
        }

        // Lấy tất cả các phiếu bán hàng (có thể join để lấy tên thẻ, tên nhân viên)
        public List<SaleInvoiceDTO> GetAllSaleInvoices()
        {
            List<SaleInvoiceDTO> invoices = new List<SaleInvoiceDTO>();
            string query = @"
            SELECT
                pbh.MaPhieu,
                pbh.MaThe,
                tld.ChuSoHuu,
                pbh.MaNhanVien,
                nv.HoTen,
                pbh.NgayTao,
                pbh.TrangThai
            FROM PhieuBanHang pbh
            LEFT JOIN TheLuuDong tld ON pbh.MaThe = tld.MaThe
            LEFT JOIN NhanVien nv ON pbh.MaNhanVien = nv.MaNhanVien";

            using (SqlConnection connection = new SqlConnection(DBConnection.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            SaleInvoiceDTO invoice = new SaleInvoiceDTO
                            {
                                InvoiceId = reader["MaPhieu"].ToString(),
                                CardId = reader["MaThe"] == DBNull.Value ? null : reader["MaThe"].ToString(),
                                CardOwnerName = reader["ChuSoHuu"] == DBNull.Value ? "N/A" : reader["ChuSoHuu"].ToString(),
                                EmployeeId = reader["MaNhanVien"] == DBNull.Value ? null : reader["MaNhanVien"].ToString(),
                                EmployeeName = reader["HoTen"] == DBNull.Value ? "N/A" : reader["HoTen"].ToString(),
                                CreatedDate = Convert.ToDateTime(reader["NgayTao"]),
                                Status = Convert.ToBoolean(reader["TrangThai"])
                                // TotalQuantity và TotalAmount sẽ được tính ở BLL hoặc khi lấy chi tiết
                            };
                            invoices.Add(invoice);
                        }
                    }
                }
            }
            return invoices;
        }

        // Lấy một phiếu bán hàng theo ID (bao gồm cả chi tiết)
        public SaleInvoiceDTO GetSaleInvoiceById(string invoiceId)
        {
            SaleInvoiceDTO invoice = null;
            string invoiceQuery = @"
            SELECT
                pbh.MaPhieu,
                pbh.MaThe,
                tld.ChuSoHuu,
                pbh.MaNhanVien,
                nv.HoTen,
                pbh.NgayTao,
                pbh.TrangThai
            FROM PhieuBanHang pbh
            LEFT JOIN TheLuuDong tld ON pbh.MaThe = tld.MaThe
            LEFT JOIN NhanVien nv ON pbh.MaNhanVien = nv.MaNhanVien
            WHERE pbh.MaPhieu = @MaPhieu";

            string detailQuery = @"
            SELECT
                ctp.Id,
                ctp.MaPhieu,
                ctp.MaSanPham,
                sp.TenSanPham,
                ctp.SoLuong,
                ctp.DonGia
            FROM ChiTietPhieu ctp
            JOIN SanPham sp ON ctp.MaSanPham = sp.MaSanPham
            WHERE ctp.MaPhieu = @MaPhieu";

            using (SqlConnection connection = new SqlConnection(DBConnection.ConnectionString))
            {
                connection.Open();

                // Lấy thông tin phiếu chính
                using (SqlCommand command = new SqlCommand(invoiceQuery, connection))
                {
                    command.Parameters.AddWithValue("@MaPhieu", invoiceId);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            invoice = new SaleInvoiceDTO
                            {
                                InvoiceId = reader["MaPhieu"].ToString(),
                                CardId = reader["MaThe"] == DBNull.Value ? null : reader["MaThe"].ToString(),
                                CardOwnerName = reader["ChuSoHuu"] == DBNull.Value ? "N/A" : reader["ChuSoHuu"].ToString(),
                                EmployeeId = reader["MaNhanVien"] == DBNull.Value ? null : reader["MaNhanVien"].ToString(),
                                EmployeeName = reader["HoTen"] == DBNull.Value ? "N/A" : reader["HoTen"].ToString(),
                                CreatedDate = Convert.ToDateTime(reader["NgayTao"]),
                                Status = Convert.ToBoolean(reader["TrangThai"])
                            };
                        }
                    }
                }

                // Lấy chi tiết phiếu nếu phiếu chính tồn tại
                if (invoice != null)
                {
                    using (SqlCommand detailCommand = new SqlCommand(detailQuery, connection))
                    {
                        detailCommand.Parameters.AddWithValue("@MaPhieu", invoiceId);
                        using (SqlDataReader detailReader = detailCommand.ExecuteReader())
                        {
                            while (detailReader.Read())
                            {
                                SaleInvoiceDetailDTO detail = new SaleInvoiceDetailDTO
                                {
                                    Id = Convert.ToInt32(detailReader["Id"]),
                                    InvoiceId = detailReader["MaPhieu"].ToString(),
                                    ProductId = detailReader["MaSanPham"].ToString(),
                                    ProductName = detailReader["TenSanPham"].ToString(),
                                    Quantity = Convert.ToInt32(detailReader["SoLuong"]),
                                    UnitPrice = Convert.ToDecimal(detailReader["DonGia"]),
                                    LineAmount = Convert.ToDecimal(detailReader["SoLuong"]) * Convert.ToDecimal(detailReader["DonGia"]) // Tính toán ở đây
                                };
                                invoice.InvoiceDetails.Add(detail);
                            }
                        }
                    }
                    // Tính toán TotalQuantity và TotalAmount sau khi có tất cả chi tiết
                    invoice.TotalQuantity = invoice.InvoiceDetails.Sum(d => d.Quantity);
                    invoice.TotalAmount = invoice.InvoiceDetails.Sum(d => d.LineAmount);
                }
            }
            return invoice;
        }

        // Thêm mới một phiếu bán hàng (có sử dụng Transaction để đảm bảo tính toàn vẹn)
        public bool AddSaleInvoice(SaleInvoiceDTO invoice)
        {
            bool success = false;
            string insertInvoiceQuery = "INSERT INTO PhieuBanHang (MaPhieu, MaThe, MaNhanVien, NgayTao, TrangThai) VALUES (@MaPhieu, @MaThe, @MaNhanVien, @NgayTao, @TrangThai)";
            string insertDetailQuery = "INSERT INTO ChiTietPhieu (MaPhieu, MaSanPham, SoLuong, DonGia) VALUES (@MaPhieu, @MaSanPham, @SoLuong, @DonGia)";

            using (SqlConnection connection = new SqlConnection(DBConnection.ConnectionString))
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction(); // Bắt đầu Transaction

                try
                {
                    // Thêm phiếu bán hàng chính
                    using (SqlCommand command = new SqlCommand(insertInvoiceQuery, connection, transaction))
                    {
                        command.Parameters.AddWithValue("@MaPhieu", invoice.InvoiceId);
                        command.Parameters.AddWithValue("@MaThe", (object)invoice.CardId ?? DBNull.Value); // Xử lý NULL
                        command.Parameters.AddWithValue("@MaNhanVien", (object)invoice.EmployeeId ?? DBNull.Value); // Xử lý NULL
                        command.Parameters.AddWithValue("@NgayTao", invoice.CreatedDate);
                        command.Parameters.AddWithValue("@TrangThai", invoice.Status);
                        command.ExecuteNonQuery();
                    }

                    // Thêm các chi tiết phiếu
                    foreach (var detail in invoice.InvoiceDetails)
                    {
                        using (SqlCommand detailCommand = new SqlCommand(insertDetailQuery, connection, transaction))
                        {
                            detailCommand.Parameters.AddWithValue("@MaPhieu", invoice.InvoiceId);
                            detailCommand.Parameters.AddWithValue("@MaSanPham", detail.ProductId);
                            detailCommand.Parameters.AddWithValue("@SoLuong", detail.Quantity);
                            detailCommand.Parameters.AddWithValue("@DonGia", detail.UnitPrice);
                            detailCommand.ExecuteNonQuery();
                        }
                    }

                    transaction.Commit(); // Commit Transaction nếu tất cả đều thành công
                    success = true;
                }
                catch (Exception ex)
                {
                    transaction.Rollback(); // Rollback Transaction nếu có lỗi
                                            // Ghi log lỗi (quan trọng!)
                    System.Diagnostics.Debug.WriteLine("Lỗi khi thêm phiếu bán hàng: " + ex.Message);
                    success = false;
                }
            }
            return success;
        }

        // Cập nhật một phiếu bán hàng (có sử dụng Transaction)
        public bool UpdateSaleInvoice(SaleInvoiceDTO invoice)
        {
            bool success = false;
            string updateInvoiceQuery = "UPDATE PhieuBanHang SET MaThe = @MaThe, MaNhanVien = @MaNhanVien, NgayTao = @NgayTao, TrangThai = @TrangThai WHERE MaPhieu = @MaPhieu";
            string deleteDetailsQuery = "DELETE FROM ChiTietPhieu WHERE MaPhieu = @MaPhieu";
            string insertDetailQuery = "INSERT INTO ChiTietPhieu (MaPhieu, MaSanPham, SoLuong, DonGia) VALUES (@MaPhieu, @MaSanPham, @SoLuong, @DonGia)";

            using (SqlConnection connection = new SqlConnection(DBConnection.ConnectionString))
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();

                try
                {
                    // Cập nhật phiếu bán hàng chính
                    using (SqlCommand command = new SqlCommand(updateInvoiceQuery, connection, transaction))
                    {
                        command.Parameters.AddWithValue("@MaPhieu", invoice.InvoiceId);
                        command.Parameters.AddWithValue("@MaThe", (object)invoice.CardId ?? DBNull.Value); // Xử lý NULL
                        command.Parameters.AddWithValue("@MaNhanVien", (object)invoice.EmployeeId ?? DBNull.Value); // Xử lý NULL
                        command.Parameters.AddWithValue("@NgayTao", invoice.CreatedDate);
                        command.Parameters.AddWithValue("@TrangThai", invoice.Status);
                        command.ExecuteNonQuery();
                    }

                    // Xóa tất cả chi tiết cũ của phiếu
                    using (SqlCommand deleteDetailCommand = new SqlCommand(deleteDetailsQuery, connection, transaction))
                    {
                        deleteDetailCommand.Parameters.AddWithValue("@MaPhieu", invoice.InvoiceId);
                        deleteDetailCommand.ExecuteNonQuery();
                    }

                    // Thêm lại các chi tiết mới
                    foreach (var detail in invoice.InvoiceDetails)
                    {
                        using (SqlCommand insertDetailCommand = new SqlCommand(insertDetailQuery, connection, transaction))
                        {
                            insertDetailCommand.Parameters.AddWithValue("@MaPhieu", invoice.InvoiceId);
                            insertDetailCommand.Parameters.AddWithValue("@MaSanPham", detail.ProductId);
                            insertDetailCommand.Parameters.AddWithValue("@SoLuong", detail.Quantity);
                            insertDetailCommand.Parameters.AddWithValue("@DonGia", detail.UnitPrice);
                            insertDetailCommand.ExecuteNonQuery();
                        }
                    }

                    transaction.Commit();
                    success = true;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    System.Diagnostics.Debug.WriteLine("Lỗi khi cập nhật phiếu bán hàng: " + ex.Message);
                    success = false;
                }
            }
            return success;
        }

        // Xóa một phiếu bán hàng và tất cả chi tiết liên quan (có sử dụng Transaction)
        public bool DeleteSaleInvoice(string invoiceId)
        {
            bool success = false;
            string deleteDetailsQuery = "DELETE FROM ChiTietPhieu WHERE MaPhieu = @MaPhieu";
            string deleteInvoiceQuery = "DELETE FROM PhieuBanHang WHERE MaPhieu = @MaPhieu";

            using (SqlConnection connection = new SqlConnection(DBConnection.ConnectionString))
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();

                try
                {
                    // Xóa tất cả chi tiết của phiếu trước (do FK ON DELETE CASCADE, dòng này có thể không cần nếu CSDL đã cấu hình đúng, nhưng vẫn an toàn)
                    using (SqlCommand deleteDetailCommand = new SqlCommand(deleteDetailsQuery, connection, transaction))
                    {
                        deleteDetailCommand.Parameters.AddWithValue("@MaPhieu", invoiceId);
                        deleteDetailCommand.ExecuteNonQuery();
                    }

                    // Sau đó xóa phiếu chính
                    using (SqlCommand deleteInvoiceCommand = new SqlCommand(deleteInvoiceQuery, connection, transaction))
                    {
                        deleteInvoiceCommand.Parameters.AddWithValue("@MaPhieu", invoiceId);
                        deleteInvoiceCommand.ExecuteNonQuery();
                    }

                    transaction.Commit();
                    success = true;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    System.Diagnostics.Debug.WriteLine("Lỗi khi xóa phiếu bán hàng: " + ex.Message);
                    success = false;
                }
            }
            return success;
        }

        // Thêm mới một chi tiết phiếu bán hàng riêng lẻ
        // (Thường được gọi từ BLL, sau đó BLL sẽ cập nhật TotalQuantity/TotalAmount của phiếu chính)
        public bool AddSaleInvoiceDetail(SaleInvoiceDetailDTO detail)
        {
            bool success = false;
            string insertDetailQuery = "INSERT INTO ChiTietPhieu (MaPhieu, MaSanPham, SoLuong, DonGia) VALUES (@MaPhieu, @MaSanPham, @SoLuong, @DonGia)";

            using (SqlConnection connection = new SqlConnection(DBConnection.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(insertDetailQuery, connection))
                {
                    command.Parameters.AddWithValue("@MaPhieu", detail.InvoiceId);
                    command.Parameters.AddWithValue("@MaSanPham", detail.ProductId);
                    command.Parameters.AddWithValue("@SoLuong", detail.Quantity);
                    command.Parameters.AddWithValue("@DonGia", detail.UnitPrice);

                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    success = rowsAffected > 0;
                }
            }
            return success;
        }

        // Cập nhật một chi tiết phiếu bán hàng riêng lẻ
        public bool UpdateSaleInvoiceDetail(SaleInvoiceDetailDTO detail)
        {
            bool success = false;
            string updateDetailQuery = "UPDATE ChiTietPhieu SET MaSanPham = @MaSanPham, SoLuong = @SoLuong, DonGia = @DonGia WHERE Id = @Id AND MaPhieu = @MaPhieu";

            using (SqlConnection connection = new SqlConnection(DBConnection.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(updateDetailQuery, connection))
                {
                    command.Parameters.AddWithValue("@Id", detail.Id);
                    command.Parameters.AddWithValue("@MaPhieu", detail.InvoiceId); // Đảm bảo chi tiết thuộc đúng phiếu
                    command.Parameters.AddWithValue("@MaSanPham", detail.ProductId);
                    command.Parameters.AddWithValue("@SoLuong", detail.Quantity);
                    command.Parameters.AddWithValue("@DonGia", detail.UnitPrice);

                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    success = rowsAffected > 0;
                }
            }
            return success;
        }

        // Xóa một chi tiết phiếu bán hàng theo ID của chi tiết
        public bool DeleteSaleInvoiceDetail(int detailId)
        {
            bool success = false;
            string deleteDetailQuery = "DELETE FROM ChiTietPhieu WHERE Id = @Id";

            using (SqlConnection connection = new SqlConnection(DBConnection.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(deleteDetailQuery, connection))
                {
                    command.Parameters.AddWithValue("@Id", detailId);

                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    success = rowsAffected > 0;
                }
            }
            return success;
        }

        // Lấy tất cả chi tiết của một phiếu cụ thể
        public List<SaleInvoiceDetailDTO> GetDetailsByInvoiceId(string invoiceId)
        {
            List<SaleInvoiceDetailDTO> details = new List<SaleInvoiceDetailDTO>();
            string query = @"
            SELECT
                ctp.Id,
                ctp.MaPhieu,
                ctp.MaSanPham,
                sp.TenSanPham,
                ctp.SoLuong,
                ctp.DonGia
            FROM ChiTietPhieu ctp
            JOIN SanPham sp ON ctp.MaSanPham = sp.MaSanPham
            WHERE ctp.MaPhieu = @MaPhieu";

            using (SqlConnection connection = new SqlConnection(DBConnection.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@MaPhieu", invoiceId);
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            details.Add(new SaleInvoiceDetailDTO
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                InvoiceId = reader["MaPhieu"].ToString(),
                                ProductId = reader["MaSanPham"].ToString(),
                                ProductName = reader["TenSanPham"].ToString(),
                                Quantity = Convert.ToInt32(reader["SoLuong"]),
                                UnitPrice = Convert.ToDecimal(reader["DonGia"]),
                                LineAmount = Convert.ToDecimal(reader["SoLuong"]) * Convert.ToDecimal(reader["DonGia"])
                            });
                        }
                    }
                }
            }
            return details;
        }
    }
    public class QLTheLuuDongDAL
    {
        private string connectionString = "Data Source=SD20302\\ADMINCUTE;Initial Catalog=PolyCafe;Integrated Security=True;";

        // Phương thức trợ giúp để tạo ID tuần tự tiếp theo (ví dụ: "THE001", "SP001", "PBH001")
        private string GenerateNextId(string prefix, string tableName, string idColumnName, SqlConnection connection, SqlTransaction transaction)
        {
            string query = $"SELECT MAX({idColumnName}) FROM {tableName} WHERE {idColumnName} LIKE '{prefix}%'";
            using (var cmd = new SqlCommand(query, connection, transaction))
            {
                object result = cmd.ExecuteScalar();
                string maxId = result == DBNull.Value ? null : result.ToString();

                if (string.IsNullOrEmpty(maxId))
                {
                    return $"{prefix}001"; // ID đầu tiên
                }
                else
                {
                    // Trích xuất phần số, tăng lên và định dạng
                    string numericPart = maxId.Substring(prefix.Length);
                    if (int.TryParse(numericPart, out int num))
                    {
                        return $"{prefix}{(num + 1).ToString("D3")}"; // D3 cho 3 chữ số, ví dụ: 001, 002
                    }
                    else
                    {
                        throw new Exception("Lỗi khi tạo mã ID mới: Không thể phân tích phần số.");
                    }
                }
            }
        }

        public void AddTheLuuDong(QLTheLuuDongDTO theLuuDong)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var transaction = connection.BeginTransaction();

                try
                {
                    // 1. Thêm vào bảng TheLuuDong
                    // MaThe được cung cấp từ UI, đảm bảo nó là duy nhất hoặc xử lý trùng lặp
                    // Để đơn giản, giả sử MaThe từ UI là duy nhất. Nếu không, hãy kiểm tra sự tồn tại trước.
                    string insertThe = @"INSERT INTO TheLuuDong (MaThe, ChuSoHuu, TrangThai)
                                         VALUES (@MaThe, @ChuSoHuu, @TrangThai)";
                    using (var cmd = new SqlCommand(insertThe, connection, transaction))
                    {
                        cmd.Parameters.Add("@MaThe", SqlDbType.Char, 6).Value = theLuuDong.MaThe;
                        cmd.Parameters.Add("@ChuSoHuu", SqlDbType.NVarChar, 100).Value = theLuuDong.ChuSoHuu;
                        cmd.Parameters.Add("@TrangThai", SqlDbType.Bit).Value = theLuuDong.TrangThai;
                        cmd.ExecuteNonQuery();
                    }

                    // 2. Lấy hoặc thêm MaSanPham từ TenSanPham
                    string maSanPham;
                    string getSP = "SELECT MaSanPham FROM SanPham WHERE TenSanPham = @Ten";
                    using (var cmd = new SqlCommand(getSP, connection, transaction))
                    {
                        cmd.Parameters.Add("@Ten", SqlDbType.NVarChar, 100).Value = theLuuDong.TenSanPham;
                        var result = cmd.ExecuteScalar();
                        if (result != null)
                        {
                            maSanPham = result.ToString(); // MaSanPham là CHAR(6)
                        }
                        else
                        {
                            // Tạo MaSanPham mới
                            maSanPham = GenerateNextId("SP", "SanPham", "MaSanPham", connection, transaction);
                            string insertSP = "INSERT INTO SanPham (MaSanPham, TenSanPham, DonGia, MaLoai, TrangThai) VALUES (@MaSanPham, @Ten, @DonGia, @MaLoai, @TrangThai)";
                            using (var insertCmd = new SqlCommand(insertSP, connection, transaction))
                            {
                                insertCmd.Parameters.Add("@MaSanPham", SqlDbType.Char, 6).Value = maSanPham;
                                insertCmd.Parameters.Add("@Ten", SqlDbType.NVarChar, 100).Value = theLuuDong.TenSanPham;
                                // Giả sử giá trị mặc định hoặc lấy từ đâu đó cho sản phẩm mới
                                insertCmd.Parameters.Add("@DonGia", SqlDbType.Decimal).Value = 0; // Giá mặc định, điều chỉnh nếu cần
                                insertCmd.Parameters.Add("@MaLoai", SqlDbType.Char, 6).Value = "LSP001"; // Giá trị mặc định, điều chỉnh nếu cần hoặc lấy từ UI
                                insertCmd.Parameters.Add("@TrangThai", SqlDbType.Bit).Value = true; // Mặc định hoạt động
                                insertCmd.ExecuteNonQuery();
                            }
                        }
                    }

                    // 3. Tạo Phiếu Bán Hàng
                    string maPhieu = GenerateNextId("PBH", "PhieuBanHang", "MaPhieu", connection, transaction);
                    string insertPBH = @"INSERT INTO PhieuBanHang (MaPhieu, MaThe, NgayTao, TrangThai)
                                         VALUES (@MaPhieu, @MaThe, GETDATE(), @TrangThai)";
                    using (var cmd = new SqlCommand(insertPBH, connection, transaction))
                    {
                        cmd.Parameters.Add("@MaPhieu", SqlDbType.Char, 6).Value = maPhieu;
                        cmd.Parameters.Add("@MaThe", SqlDbType.Char, 6).Value = theLuuDong.MaThe;
                        cmd.Parameters.Add("@TrangThai", SqlDbType.Bit).Value = true; // Mặc định hoạt động cho phiếu bán hàng mới
                        cmd.ExecuteNonQuery(); // Không cần OUTPUT vì chúng ta đã tạo ID
                    }


                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    throw; // Ném lại ngoại lệ gốc
                }
            }
        }

        public void UpdateTheLuuDong(Polycafe_DTO.QLTheLuuDongDTO theLuuDong)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                const string query = @"
                UPDATE TheLuuDong
                SET ChuSoHuu = @ChuSoHuu, TrangThai = @TrangThai
                WHERE MaThe = @MaThe";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@ChuSoHuu", SqlDbType.NVarChar, 100).Value = theLuuDong.ChuSoHuu ?? (object)DBNull.Value;
                    command.Parameters.Add("@TrangThai", SqlDbType.Bit).Value = theLuuDong.TrangThai; // Truyền trực tiếp boolean
                    command.Parameters.Add("@MaThe", SqlDbType.Char, 6).Value = theLuuDong.MaThe; // MaThe là CHAR(6)

                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected == 0)
                    {
                        throw new Exception($"Không tìm thấy thẻ với Mã thẻ = {theLuuDong.MaThe}");
                    }
                }
            }
        }

        public void RemoveTheLuuDong(string maThe) // Đã thay đổi kiểu tham số thành string
        {
            using (var connection = new SqlConnection(connectionString))
            {
                const string query = "DELETE FROM TheLuuDong WHERE MaThe = @MaThe";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@MaThe", SqlDbType.Char, 6).Value = maThe; // MaThe là CHAR(6)

                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected == 0)
                    {
                        throw new Exception($"Không tìm thấy thẻ lưu động với Mã thẻ = {maThe} để xóa.");
                    }
                }
            }
        }

        public List<Polycafe_DTO.QLTheLuuDongDTO> GetAllTheLuuDong()
        {
            var list = new List<Polycafe_DTO.QLTheLuuDongDTO>();
            using (var connection = new SqlConnection(connectionString))
            {
                // Đã loại bỏ pb.NgayTao khỏi SELECT vì nó không có trong QLTheLuuDongDTO
                string query = @"
                SELECT
                t.MaThe,
                t.ChuSoHuu,
                t.TrangThai,
                sp.TenSanPham,
                ct.SoLuong
                FROM TheLuuDong t
                INNER JOIN PhieuBanHang pb ON t.MaThe = pb.MaThe
                INNER JOIN ChiTietPhieu ct ON pb.MaPhieu = ct.MaPhieu
                INNER JOIN SanPham sp ON ct.MaSanPham = sp.MaSanPham";

                using (var command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var item = new Polycafe_DTO.QLTheLuuDongDTO
                            {
                                MaThe = reader.GetString(reader.GetOrdinal("MaThe")), // Đọc dưới dạng chuỗi
                                ChuSoHuu = reader["ChuSoHuu"].ToString(),
                                TrangThai = reader.GetBoolean(reader.GetOrdinal("TrangThai")), // Đọc dưới dạng boolean
                                TenSanPham = reader["TenSanPham"].ToString(),
                                SoLuong = reader.GetInt32(reader.GetOrdinal("SoLuong")),
                            };
                            list.Add(item);
                        }
                    }
                }
            }
            return list;
        }

        public List<Polycafe_DTO.QLTheLuuDongDTO> SearchTheLuuDongByMaThe(string maThe)
        {
            var list = new List<Polycafe_DTO.QLTheLuuDongDTO>();
            using (var connection = new SqlConnection(connectionString))
            {
                string query = @"
                SELECT
                t.MaThe,
                t.ChuSoHuu,
                t.TrangThai,
                sp.TenSanPham,
                ct.SoLuong
                FROM TheLuuDong t
                INNER JOIN PhieuBanHang pb ON t.MaThe = pb.MaThe
                INNER JOIN ChiTietPhieu ct ON pb.MaPhieu = ct.MaPhieu
                INNER JOIN SanPham sp ON ct.MaSanPham = sp.MaSanPham
                WHERE t.MaThe = @MaThe"; // Thêm điều kiện WHERE để tìm kiếm theo MaThe

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@MaThe", SqlDbType.Char, 6).Value = maThe; // Thêm tham số MaThe

                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var item = new Polycafe_DTO.QLTheLuuDongDTO
                            {
                                MaThe = reader.GetString(reader.GetOrdinal("MaThe")),
                                ChuSoHuu = reader["ChuSoHuu"].ToString(),
                                TrangThai = reader.GetBoolean(reader.GetOrdinal("TrangThai")),
                                TenSanPham = reader["TenSanPham"].ToString(),
                                SoLuong = reader.GetInt32(reader.GetOrdinal("SoLuong")),
                            };
                            list.Add(item);
                        }
                    }
                }
            }
            return list;
        }

        public List<string> GetAllProductNames()
        {
            var productNames = new List<string>();
            using (var connection = new SqlConnection(connectionString))
            {
                string query = "SELECT TenSanPham FROM SanPham ORDER BY TenSanPham";
                using (var command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            productNames.Add(reader["TenSanPham"].ToString());
                        }
                    }
                }
            }
            return productNames;
        }
    }
    public class ThongkeNVDAL
    {
        private static string connectionString = "Data Source=SD20302\\ADMINCUTE;Initial Catalog=PolyCafe;Integrated Security=True;";

        public DataTable GetNV()
        {
            DataTable dt = new DataTable();
            string query = "SELECT MaNhanVien, HoTen FROM NhanVien ";
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        conn.Open();
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(dt);
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Lỗi khi lấy dữ liệu nhân viên từ CSDL." + ex.Message);
            }
            return dt;
        }
        public DataTable GetTK(string MaNV, DateTime startDate, DateTime endDate)
        {
            DataTable dt = new DataTable();
            string query = @"
                SELECT NV.MaNhanVien,
                    NV.HoTen,
                    PB.NgayTao,
                    PB.TrangThai,
                    Count(ChiTietPhieu.SoLuong) as [So Luong Phieu],
                    Sum(ChiTietPhieu.DonGia * ChiTietPhieu.SoLuong) as [Tong Tien]
                FROM NhanVien NV
                Inner join PhieuBanHang PB on PB.MaNhanVien = NV.MaNhanVien
                Inner join ChiTietPhieu on ChiTietPhieu.MaPhieu = PB.MaPhieu
                WHERE (@MaNV IS NULL OR NV.MaNhanVien = @MaNV)
                AND PB.NgayTao BETWEEN @TuNgay AND @DenNgay
                group by NV.MaNhanVien, 
                NV.HoTen,
                PB.TrangThai,
                PB.NgayTao";
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        if (string.IsNullOrEmpty(MaNV))
                        {
                            cmd.Parameters.AddWithValue("@MaNV", DBNull.Value);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@MaNV", MaNV);
                        }
                        cmd.Parameters.AddWithValue("@TuNgay", startDate);
                        cmd.Parameters.AddWithValue("@DenNgay", endDate);

                        conn.Open();
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(dt);
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Lỗi khi lấy dữ liệu thống kê từ CSDL." + ex.Message);
            }
            return dt;

        }


        public List<string> LoadMaNV()
        {
            List<string> maNV = new List<string>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT DISTINCT MaNhanVien FROM NhanVien";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        maNV.Add(reader["MaNhanVien"].ToString());
                    }
                    reader.Close(); // Đóng reader
                    return maNV;
                }
                catch (SqlException ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Lỗi tải Mã nhân viên : {ex.Message}", "Lỗi SQL");
                    return null; // Trả về null khi có lỗi
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Lỗi không xác định khi tải Mã nhân viên: {ex.Message}", "Lỗi");
                    return null;
                }
            }
        }


        public List<SanPham_DTO> LoadMaSP()
        {
            List<SanPham_DTO> list = new List<SanPham_DTO>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT MaSanPham, TenSanPham FROM SanPham";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(new SanPham_DTO
                    {
                        MaSP = reader["MaSanPham"].ToString(),
                        TenSP = reader["TenSanPham"].ToString()
                    });
                }
            }
            return list;
        }


        public DataTable GetTKSP(string MaSP, DateTime startDate, DateTime endDate)
        {
            DataTable dt = new DataTable();
            string query = @"
                SELECT SP.MaSanPham,
                       SP.TenSanPham,
                       PB.NgayTao,
                       SUM(CTP.SoLuong) AS SoLuongBan,
                       SUM(CTP.SoLuong * CTP.DonGia) AS TongTien
                FROM SanPham SP
                INNER JOIN ChiTietPhieu CTP ON SP.MaSanPham = CTP.MaSanPham
                INNER JOIN PhieuBanHang PB ON PB.MaPhieu = CTP.MaPhieu
                WHERE (@MaSP IS NULL OR SP.MaSanPham = @MaSP)
                  AND PB.NgayTao BETWEEN @TuNgay AND @DenNgay
                GROUP BY SP.MaSanPham, SP.TenSanPham, PB.NgayTao";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@MaSP", string.IsNullOrEmpty(MaSP) ? DBNull.Value : (object)MaSP);
                    cmd.Parameters.AddWithValue("@TuNgay", startDate);
                    cmd.Parameters.AddWithValue("@DenNgay", endDate);

                    conn.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }
            }

            return dt;
        }

    }
    public class DBConnections
    {
        // Chuỗi kết nối đến cơ sở dữ liệu PolyCafe
        private static string connectionString = "Data Source=SD20302\\ADMINCUTE;Initial Catalog=PolyCafe;Integrated Security=True;";

        // Phương thức lấy SqlConnection
        public static SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }

        // Phương thức thực thi câu lệnh SQL (INSERT, UPDATE, DELETE)
        public static int ExecuteNonQuery(string query, SqlParameter[] parameters = null)
        {
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters);
                    }
                    return command.ExecuteNonQuery();
                }
            }
        }

        // Phương thức thực thi câu lệnh SQL trả về dữ liệu (SELECT)
        public static DataTable ExecuteQuery(string query, SqlParameter[] parameters = null)
        {
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters);
                    }
                    DataTable data = new DataTable();
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(data);
                    return data;
                }
            }
        }
    }
    public class EmployeeDAL
    {
        // Lấy tất cả nhân viên
        public List<EmployeeDTO> GetAllEmployees()
        {
            List<EmployeeDTO> employees = new List<EmployeeDTO>();
            // Câu lệnh SQL để lấy tất cả nhân viên
            string query = "SELECT MaNhanVien, HoTen, Email, MatKhau, VaiTro, TrangThai FROM NhanVien";
            DataTable data = DBConnections.ExecuteQuery(query);

            foreach (DataRow row in data.Rows)
            {
                EmployeeDTO employee = new EmployeeDTO
                (
                    row["MaNhanVien"].ToString(),
                    row["HoTen"].ToString(),
                    row["Email"].ToString(),
                    row["MatKhau"].ToString(),
                    (bool)row["VaiTro"],
                    (bool)row["TrangThai"]
                );
                employees.Add(employee);
            }
            return employees;
        }

        // Thêm một nhân viên mới
        public bool AddEmployee(EmployeeDTO employee)
        {
            // Câu lệnh SQL để thêm nhân viên
            string query = "INSERT INTO NhanVien (MaNhanVien, HoTen, Email, MatKhau, VaiTro, TrangThai) VALUES (@MaNhanVien, @HoTen, @Email, @MatKhau, @VaiTro, @TrangThai)";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaNhanVien", employee.EmployeeId),
                new SqlParameter("@HoTen", employee.FullName),
                new SqlParameter("@Email", employee.Email),
                new SqlParameter("@MatKhau", employee.Password),
                new SqlParameter("@VaiTro", employee.Role),
                new SqlParameter("@TrangThai", employee.Status)
            };
            int rowsAffected = DBConnections.ExecuteNonQuery(query, parameters);
            return rowsAffected > 0;
        }

        // Cập nhật thông tin nhân viên
        public bool UpdateEmployee(EmployeeDTO employee)
        {
            // Câu lệnh SQL để cập nhật nhân viên
            string query = "UPDATE NhanVien SET HoTen = @HoTen, Email = @Email, MatKhau = @MatKhau, VaiTro = @VaiTro, TrangThai = @TrangThai WHERE MaNhanVien = @MaNhanVien";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@HoTen", employee.FullName),
                new SqlParameter("@Email", employee.Email),
                new SqlParameter("@MatKhau", employee.Password),
                new SqlParameter("@VaiTro", employee.Role),
                new SqlParameter("@TrangThai", employee.Status),
                new SqlParameter("@MaNhanVien", employee.EmployeeId)
            };
            int rowsAffected = DBConnections.ExecuteNonQuery(query, parameters);
            return rowsAffected > 0;
        }

        // Xóa nhân viên theo mã
        public bool DeleteEmployee(string employeeId)
        {
            // Câu lệnh SQL để xóa nhân viên
            string query = "DELETE FROM NhanVien WHERE MaNhanVien = @MaNhanVien";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaNhanVien", employeeId)
            };
            int rowsAffected = DBConnections.ExecuteNonQuery(query, parameters);
            return rowsAffected > 0;
        }

        // Tìm kiếm nhân viên theo Mã NV hoặc Tên NV
        public List<EmployeeDTO> SearchEmployees(string keyword)
        {
            List<EmployeeDTO> employees = new List<EmployeeDTO>();
            // Câu lệnh SQL để tìm kiếm nhân viên
            string query = "SELECT MaNhanVien, HoTen, Email, MatKhau, VaiTro, TrangThai FROM NhanVien WHERE MaNhanVien LIKE @Keyword OR HoTen LIKE @Keyword";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Keyword", "%" + keyword + "%") // Sử dụng LIKE để tìm kiếm gần đúng
            };
            DataTable data = DBConnections.ExecuteQuery(query, parameters);

            foreach (DataRow row in data.Rows)
            {
                EmployeeDTO employee = new EmployeeDTO
                (
                    row["MaNhanVien"].ToString(),
                    row["HoTen"].ToString(),
                    row["Email"].ToString(),
                    row["MatKhau"].ToString(),
                    (bool)row["VaiTro"],
                    (bool)row["TrangThai"]
                );
                employees.Add(employee);
            }
            return employees;
        }

        // Kiểm tra xem Email đã tồn tại chưa
        public bool IsEmailExist(string email, string excludeEmployeeId = null)
        {
            // Câu lệnh SQL để kiểm tra email
            string query = "SELECT COUNT(*) FROM NhanVien WHERE Email = @Email";
            List<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter("@Email", email)
            };

            if (!string.IsNullOrEmpty(excludeEmployeeId))
            {
                query += " AND MaNhanVien <> @ExcludeEmployeeId";
                parameters.Add(new SqlParameter("@ExcludeEmployeeId", excludeEmployeeId));
            }

            using (SqlConnection connection = DBConnections.GetConnection())
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddRange(parameters.ToArray());
                    int count = (int)command.ExecuteScalar();
                    return count > 0;
                }
            }
        }

        // Kiểm tra xem Mã NV đã tồn tại chưa
        public bool IsEmployeeIdExist(string employeeId)
        {
            // Câu lệnh SQL để kiểm tra mã nhân viên
            string query = "SELECT COUNT(*) FROM NhanVien WHERE MaNhanVien = @MaNhanVien";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaNhanVien", employeeId)
            };

            using (SqlConnection connection = DBConnections.GetConnection())
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddRange(parameters);
                    int count = (int)command.ExecuteScalar();
                    return count > 0;
                }
            }
        }
    }
    public class HoSo_DAL
    {
        private string connString = "Data Source=SD20302\\ADMINCUTE;Initial Catalog=PolyCafe;Integrated Security=True;";
        public DataTable GetUser(string email)
        {
            DataTable dt = new DataTable();
            using (SqlConnection connection = new SqlConnection(connString))
            {
                string query = "SELECT HoTen, Email, VaiTro FROM NhanVien ";
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(dt);
            }
            return dt;
        }
    }

}
