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
}
