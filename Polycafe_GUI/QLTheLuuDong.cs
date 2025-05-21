using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Polycafe_GUI.DTO;
using Polycafe_GUI.BUS;
using Polycafe_GUI.DAL;

namespace Polycafe_GUI
{
    public partial class QLTheLuuDong : UserControl
    {
        public QLTheLuuDong()
        {
            InitializeComponent();
            // Đặt DropDownStyle để ngăn người dùng nhập liệu vào ComboBox
            comboSP.DropDownStyle = ComboBoxStyle.DropDownList; // <--- Dòng đã thêm/chỉnh sửa
            // Tải dữ liệu ban đầu khi control được tạo
            LoadData();
            PopulateProductComboBox(); // Gọi phương thức để điền ComboBox sản phẩm
        }

        // Phương thức để tải dữ liệu vào DataGridView
        private void LoadData()
        {
            try
            {
                var bus = new QLTheLuuDongBUS();
                dataGridView1.DataSource = bus.GetAllTheLuuDong();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Phương thức mới để điền dữ liệu vào comboSP
        private void PopulateProductComboBox()
        {
            try
            {
                var bus = new QLTheLuuDongBUS();
                List<string> productNames = bus.GetAllProductNames();

                // --- Bắt đầu phần gỡ lỗi ---
                if (productNames != null && productNames.Any())
                {
                    // Đã bỏ dòng MessageBox.Show để tránh làm phiền người dùng cuối
                    // MessageBox.Show($"Đã tải {productNames.Count} sản phẩm. Sản phẩm đầu tiên: {productNames.First()}", "Thông báo gỡ lỗi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    comboSP.DataSource = productNames;
                }
                else
                {
                    MessageBox.Show("Không tìm thấy sản phẩm nào để tải vào ComboBox hoặc có lỗi khi tải dữ liệu.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    comboSP.DataSource = null; // Đảm bảo ComboBox trống
                }
                // --- Kết thúc phần gỡ lỗi ---
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách sản phẩm: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UserControl1_Load(object sender, EventArgs e)
        {
            // Sự kiện này kích hoạt khi UserControl được tải vào container của nó
            // LoadData(); // Đã gọi trong hàm tạo, không cần gọi lại ở đây trừ khi có lý do đặc biệt
        }

        private void label7_Click(object sender, EventArgs e)
        {
            // Xử lý sự kiện trống
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            // Xử lý sự kiện trống
        }

        private void ngaytao_ValueChanged(object sender, EventArgs e)
        {
            // Xử lý sự kiện trống
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                // Lấy dữ liệu từ các control trên form
                string maThe = id_card.Text.Trim(); // MaThe là CHAR(6) trong DB, nên nó là một chuỗi
                string chuSoHuu = textBox1.Text.Trim();
                string tenSanPham = comboSP.Text.Trim();
                int soLuong = (int)numericUpDown1.Value;

                // Kiểm tra dữ liệu đầu vào
                if (string.IsNullOrEmpty(maThe) || string.IsNullOrEmpty(chuSoHuu) || string.IsNullOrEmpty(tenSanPham))
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Tạo DTO
                bool trangThai = radioButton1.Checked; // TrangThai là BIT trong DB, nên nó là một boolean
                var theLuuDong = new QLTheLuuDongDTO
                {
                    MaThe = maThe,
                    ChuSoHuu = chuSoHuu,
                    TrangThai = trangThai,
                    TenSanPham = tenSanPham,
                    SoLuong = soLuong
                };

                // Gọi BUS để thêm vào DB
                var bus = new QLTheLuuDongBUS();
                bus.AddTheLuuDong(theLuuDong);

                MessageBox.Show("Thêm thẻ lưu động thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Làm mới form nếu cần
                // ClearForm(); // Bỏ ghi chú và thực hiện nếu cần
                LoadData();  // Tải lại dữ liệu DataGridView
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                // Lấy dữ liệu từ form
                string maThe = id_card.Text.Trim(); // MaThe là CHAR(6) trong DB, nên nó là một chuỗi
                string chuSoHuu = textBox1.Text.Trim();
                bool trangThai = radioButton1.Checked; // TrangThai là BIT trong DB, nên nó là một boolean

                // Xác thực đầu vào
                if (string.IsNullOrEmpty(maThe) || string.IsNullOrEmpty(chuSoHuu))
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin (Mã thẻ, Chủ sở hữu)!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Tạo DTO
                var theLuuDong = new QLTheLuuDongDTO
                {
                    MaThe = maThe,
                    ChuSoHuu = chuSoHuu,
                    TrangThai = trangThai,
                    // TenSanPham và SoLuong không được cập nhật trực tiếp cho bảng TheLuuDong
                    // Nếu cần cập nhật các trường này, phương thức DAL và DTO sẽ cần phản ánh điều đó cho ChiTietPhieu
                };

                // Gọi BUS để cập nhật thẻ
                var bus = new QLTheLuuDongBUS();
                bus.UpdateTheLuuDong(theLuuDong);

                MessageBox.Show("Cập nhật thẻ lưu động thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadData(); // Tải lại dữ liệu DataGridView
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            try
            {
                // Lấy mã thẻ từ control
                string maThe = id_card.Text.Trim(); // MaThe là CHAR(6) trong DB, nên nó là một chuỗi

                if (string.IsNullOrEmpty(maThe))
                {
                    MessageBox.Show("Vui lòng chọn một thẻ để xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Xác nhận xóa
                var result = MessageBox.Show("Bạn có chắc muốn xóa thẻ lưu động này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    // Gọi BUS để xóa
                    var bus = new QLTheLuuDongBUS();
                    bus.RemoveTheLuuDong(maThe);
                    MessageBox.Show("Xóa thẻ lưu động thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData();  // Tải lại dữ liệu DataGridView
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            // Xử lý sự kiện trống
        }

        private void comboSP_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Xử lý sự kiện trống
        }

        private void btnSP_Click(object sender, EventArgs e)
        {
            // Xử lý sự kiện trống
        }

        private void btnXoaSP_Click(object sender, EventArgs e)
        {
            // Xử lý sự kiện trống
        }

        private void btnfound_Click(object sender, EventArgs e)
        {
            try
            {
                string searchTerm = comboBox1.Text.Trim();

                if (string.IsNullOrEmpty(searchTerm))
                {
                    MessageBox.Show("Vui lòng nhập Mã thẻ để tìm kiếm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    LoadData(); // Tải lại tất cả dữ liệu nếu trường tìm kiếm trống
                    return;
                }

                var bus = new QLTheLuuDongBUS();
                List<QLTheLuuDongDTO> searchResults = bus.SearchTheLuuDongByMaThe(searchTerm);

                if (searchResults.Any())
                {
                    dataGridView1.DataSource = searchResults;
                    MessageBox.Show($"Tìm thấy {searchResults.Count} kết quả.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    dataGridView1.DataSource = null; // Xóa DataGridView nếu không tìm thấy kết quả
                    MessageBox.Show("Không tìm thấy thẻ lưu động nào với mã thẻ này.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tìm kiếm: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void textboxTim_TextChanged(object sender, EventArgs e)
        {
            // Xử lý sự kiện trống
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Xử lý sự kiện trống
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Kiểm tra xem có hàng nào được nhấp không và hàng đó không phải là tiêu đề cột
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                // Lấy giá trị MaThe từ ô tương ứng (ví dụ: cột có tên "MaThe")
                // Đảm bảo tên cột chính xác trong DataGridView của bạn
                string maThe = row.Cells["MaThe"].Value.ToString();

                // Điền giá trị vào textbox id_card
                id_card.Text = maThe;

                // Tùy chọn: Điền các trường khác nếu cần cho chỉnh sửa
                textBox1.Text = row.Cells["ChuSoHuu"].Value.ToString();
                bool trangThai = (bool)row.Cells["TrangThai"].Value;
                radioButton1.Checked = trangThai;
                // Giả sử bạn có radioButton2 cho trạng thái "Không hoạt động"
                // if (radioButton2 != null) radioButton2.Checked = !trangThai; 
                comboSP.Text = row.Cells["TenSanPham"].Value.ToString();
                numericUpDown1.Value = Convert.ToInt32(row.Cells["SoLuong"].Value);
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {
            // Xử lý sự kiện trống
        }
    }
}

namespace Polycafe_GUI.DTO
{
    public class QLTheLuuDongDTO
    {
        public string MaThe { get; set; } // Đã thay đổi từ int sang string để phù hợp với CHAR(6) trong DB
        public string ChuSoHuu { get; set; }
        public bool TrangThai { get; set; } // Đã thay đổi từ string sang bool để phù hợp với BIT trong DB
        public string TenSanPham { get; set; }
        public int SoLuong { get; set; }
    }
}

namespace Polycafe_GUI.DAL
{
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

                    // 4. Thêm vào ChiTietPhieu
                    string insertCT = @"INSERT INTO ChiTietPhieu (MaPhieu, MaSanPham, SoLuong, DonGia)
                                         VALUES (@MaPhieu, @MaSanPham, @SoLuong, @DonGia)";
                    using (var cmd = new SqlCommand(insertCT, connection, transaction))
                    {
                        cmd.Parameters.Add("@MaPhieu", SqlDbType.Char, 6).Value = maPhieu;
                        cmd.Parameters.Add("@MaSanPham", SqlDbType.Char, 6).Value = maSanPham;
                        cmd.Parameters.Add("@SoLuong", SqlDbType.Int).Value = theLuuDong.SoLuong;
                        cmd.Parameters.Add("@DonGia", SqlDbType.Decimal).Value = 0; // Giá mặc định, điều chỉnh nếu cần (ví dụ: lấy từ bảng SanPham)
                        cmd.ExecuteNonQuery();
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
       
        public void UpdateTheLuuDong(Polycafe_GUI.DTO.QLTheLuuDongDTO theLuuDong)
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

        public List<Polycafe_GUI.DTO.QLTheLuuDongDTO> GetAllTheLuuDong()
        {
            var list = new List<Polycafe_GUI.DTO.QLTheLuuDongDTO>();
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
                            var item = new Polycafe_GUI.DTO.QLTheLuuDongDTO
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

        public List<Polycafe_GUI.DTO.QLTheLuuDongDTO> SearchTheLuuDongByMaThe(string maThe)
        {
            var list = new List<Polycafe_GUI.DTO.QLTheLuuDongDTO>();
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
                            var item = new Polycafe_GUI.DTO.QLTheLuuDongDTO
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
}

namespace Polycafe_GUI.BUS
{
    public class QLTheLuuDongBUS
    {
        private QLTheLuuDongDAL theLuuDongDAL = new QLTheLuuDongDAL();

        public void AddTheLuuDong(Polycafe_GUI.DTO.QLTheLuuDongDTO theLuuDong)
        {
            theLuuDongDAL.AddTheLuuDong(theLuuDong);
        }

        public void UpdateTheLuuDong(Polycafe_GUI.DTO.QLTheLuuDongDTO theLuuDong)
        {
            theLuuDongDAL.UpdateTheLuuDong(theLuuDong);
        }

        public void RemoveTheLuuDong(string maThe) // Đã thay đổi kiểu tham số thành string
        {
            theLuuDongDAL.RemoveTheLuuDong(maThe);
        }

        public List<Polycafe_GUI.DTO.QLTheLuuDongDTO> GetAllTheLuuDong()
        {
            return theLuuDongDAL.GetAllTheLuuDong();
        }

        public List<Polycafe_GUI.DTO.QLTheLuuDongDTO> SearchTheLuuDongByMaThe(string maThe)
        {
            return theLuuDongDAL.SearchTheLuuDongByMaThe(maThe);
        }

        public List<string> GetAllProductNames()
        {
            return theLuuDongDAL.GetAllProductNames();
        }
    }
}