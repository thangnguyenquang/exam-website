using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BTLThiTracNghiem
{
    public partial class DangKy1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string path = "Users.xml";

            if (Request.Form["btnDangKy"] == "Đăng ký")
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


                User newUser = new User();
                newUser.Id = listUsers.Count;
                newUser.Email = Request.Form["txtEmail"];
                newUser.HoTen = Request.Form["txtHoTen"];
                newUser.MatKhau = Request.Form["txtMatKhau"];

                string matKhau2 = Request.Form["txtMatKhau2"];

                bool trung = false; //Kiểm tra email đang ký đã tồn tại hay chưa
                foreach (User user in listUsers)
                {
                    if (user.Email.Equals(newUser.Email))
                    {
                        trung = true;
                        break;
                    }
                }

                if (trung == false)
                {
                    foreach (User user in listUsers)
                    {
                        if (user.Id == newUser.Id)
                        {
                            newUser.Id++;
                        }
                    }
                    listUsers.Add(newUser);
                    //Ghi file
                    System.Xml.Serialization.XmlSerializer writer = new System.Xml.Serialization.XmlSerializer(typeof(List<User>));

                    System.IO.FileStream _file = System.IO.File.Create(Server.MapPath(path));

                    writer.Serialize(_file, listUsers);
                    _file.Close();

                    // Tạo session
                    Session["login"] = true;
                    Session["id"] = newUser.Id;
                    Session["email"] = newUser.Email;
                    Session["hoTen"] = newUser.HoTen;
                    Session["matKhau"] = newUser.MatKhau;

                    if ((bool)Session["DangNhapLamBaiThi"] == true && (bool)Session["login"] == true && (string)Session["email"] != "")
                    {
                        Response.Redirect("CauHoiThi.aspx?id=" + Session["idDeThi"]);
                    }

                    if ((bool)Session["login"] == true && (string)Session["email"] != "")
                    {
                        Response.Redirect("TrangChu.aspx");
                    }
                    Session["DangNhapLamBaiThi"] = false;
                }
                else
                {
                    Response.Write("<script>alert('Tài khoản đã tồn tại!');</script>");
                }
            }
        }
    }
}