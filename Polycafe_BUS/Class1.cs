using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Polycafe_DTO;
using Polycafe_DAL;

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
}
