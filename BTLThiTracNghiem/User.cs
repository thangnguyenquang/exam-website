using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BTLThiTracNghiem
{
    public class User
    {
        int id;
        string email;
        string hoTen;
        string matKhau;

        public int Id { get => id; set => id = value; }
        public string Email { get => email; set => email = value; }
        public string HoTen { get => hoTen; set => hoTen = value; }
        public string MatKhau { get => matKhau; set => matKhau = value; }
    }
}