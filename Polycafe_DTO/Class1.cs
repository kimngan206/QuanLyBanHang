using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polycafe_DTO
{
    public class LoginDTO
    {
        public string Email { get; set; }
        public string MatKhau { get; set; }
        public string VaiTro { get; set; }

    }
    public class ChangePasswordDTO
    {
        public string Email { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
    }
    public class qlLSP
    {
        public string MaLoai { get; set; }
        public string TenLoai { get; set; }
        public string GhiChu { get; set; }
    }
    

    public class ThongKeNhanVien_DTO
    {
        public string MaNV { get; set; }
        public string TenNV { get; set; }
        public decimal TongTien { get; set; }
        public int SoLuongPhieu { get; set; }
        public DateTime NgayLapPhieu { get; set; }
        public string TrangThai { get; set; }
    }

    public class NhanVien_DTO
    {
        public string MaNV { get; set; }
        public string TenNV { get; set; }
    }
    public class SanPhamDTO
    {
        public string MaSanPham { get; set; }
        public string TenSanPham { get; set; }
        public string MaLoai { get; set; }
        public decimal DonGia { get; set; }
        public string TrangThai { get; set; }
        public string HinhAnh { get; set; }
    }
}
