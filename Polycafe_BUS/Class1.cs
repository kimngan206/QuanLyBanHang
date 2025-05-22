using Polycafe_DAL;
using Polycafe_DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polycafe_BUS
{
    public class LoginBUS
    {
        private LoginDAL dal = new LoginDAL();

        public bool KiemTraDangNhap(string username, string password)
        {
            string storedPassword = dal.GetMatKhau(username);
            return storedPassword != null && storedPassword == password.Trim();
        }

        public string LayVaiTro(string username)
        {
            return dal.GetVaiTro(username);
        }
    }
    public class NhanVienBLL
    {
        private NhanVienDAL nhanVienDAL;

        public NhanVienBLL(string connectionString)
        {
            nhanVienDAL = new NhanVienDAL(connectionString);
        }

        public bool CheckAccount(string email, string password)
        {
            try
            {
                return nhanVienDAL.CheckCredentials(email, password);
            }
            catch (Exception ex)
            {
                // Ghi log lỗi nếu cần
                throw new Exception("Lỗi khi kiểm tra đăng nhập: " + ex.Message);
            }
        }

        public bool ChangePassword(ChangePasswordDTO changePasswordDTO)
        {
            return nhanVienDAL.UpdatePassword(changePasswordDTO.Email, changePasswordDTO.NewPassword);
        }
    }
    public class qlLSP_BUS
    {
        private qlLSP_DAL dal = new qlLSP_DAL();

        public DataTable get()
        {
            DataTable dt = dal.GetAll();
            if (dt == null)
            {
                // Nếu DAL trả về null, trả về một DataTable trống để tránh lỗi NullReferenceException
                return new DataTable();
            }
            return dt;
        }

        public bool check(string maloai)
        {
            return dal.Check(maloai);
        }

        public bool add(qlLSP lsp)
        {
            return dal.Add(lsp) > 0;
        }

        public bool update(qlLSP lsp)
        {
            return dal.Update(lsp) > 0;
        }

        public bool delete(qlLSP lsp)
        {
            return dal.Delete(lsp) > 0;
        }

        public List<string> Loadmaloai()
        {
            return dal.LoadMaLoai();
        }
    }
    public class ThongkeNVBUS
    {
        private ThongkeNVDAL dal = new ThongkeNVDAL();
        public DataTable getEmp()
        {
            return dal.GetNV();
        }
        public DataTable getTK(string MaNV, DateTime startDate, DateTime endDate)
        {
            return dal.GetTK(MaNV, startDate, endDate);
        }
        public DataTable LoadmaNV()
        {
            return dal.LoadMaNV();
        }

    }
    public class SanPhamBUS
    {
        private SanPhamDAL dal = new SanPhamDAL();
        public DataTable GetAllSanPham()
        {
            return dal.GetAllSanPham();
        }
        public bool AddSanPham(SanPhamDTO sanPham)
        {
            return dal.AddSanPham(sanPham);
        }
        public bool UpdateSanPham(SanPhamDTO sanPham)
        {
            return dal.UpdateSanPham(sanPham);
        }
        public bool DeleteSanPham(string maSanPham)
        {
            return dal.DeleteSanPham(maSanPham);
        }
        public DataTable SearchSanPham(string searchTerm)
        {
            return dal.SearchSanPham(searchTerm);
        }
        public DataTable GetLoaiSanPham()
        {
            return dal.GetLoaiSanPham();
        }
    }
}
