using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BTLThiTracNghiem
{
    public partial class DangKy : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if ((bool)Session["DangNhapLamBaiThi"] == true)
            {
                Response.Write("<script>alert('Bạn phải đăng nhập để làm bài thi!');</script>");
            }

            string path = "Users.xml";

            if (Request.Form["btnDangNhap"] == "Đăng nhập")
            {
                List<User> listUsers = new List<User>();

                if (File.Exists(Server.MapPath(path)))
                {
                    // Đọc file
                    System.Xml.Serialization.XmlSerializer reader = new System.Xml.Serialization.XmlSerializer(typeof(List<User>));
                    StreamReader file = new StreamReader(Server.MapPath(path));

                    listUsers = (List<User>)reader.Deserialize(file);
                    listUsers = listUsers.OrderBy(User => User.Id).ToList();
                    file.Close();
                }

                User userDangNhap = new User();
                userDangNhap.Email = Request.Form["txtEmail"];
                userDangNhap.MatKhau = Request.Form["txtMatKhau"];

                bool trung = false;
                foreach (User user in listUsers)
                {
                    if (user.Email.Equals(userDangNhap.Email) && user.MatKhau.Equals(userDangNhap.MatKhau))
                    {
                        trung = true;
                        userDangNhap.Id = user.Id ;
                        Session["hoTen"] = user.HoTen;
                        break;
                    }
                }

                if (trung == false)
                {
                    Response.Write("<script>alert('Tài khoản hoặc mật khẩu không đúng!');</script>");
                }
                else
                {
                    // Tạo session
                    Session["login"] = true;
                    Session["id"] = userDangNhap.Id;
                    Session["email"] = userDangNhap.Email;
                    Session["matKhau"] = userDangNhap.MatKhau;

                    if ((bool)Session["DangNhapLamBaiThi"] == true && (bool)Session["login"] == true && (string)Session["email"] != "")
                    {
                        Response.Redirect("CauHoiThi.aspx?id=" + Session["idDeThi"]);
                    }

                    Session["DangNhapLamBaiThi"] = false;

                    if ((bool)Session["login"] == true && (string)Session["email"] != "")
                    {
                        Response.Redirect("TrangChu.aspx");
                    }

                }
            }
   
        }
    }
}