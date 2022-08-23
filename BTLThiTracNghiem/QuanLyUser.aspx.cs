using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BTLThiTracNghiem
{
    public partial class QuanLyUser : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if ((string)Session["email"] != "admin@gmail.com" && (bool)Session["login"] == false)
            {
                Response.Redirect("TrangChu.aspx");
            }

            string path = "Users.xml";

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

            string html = "";
            html += "<table>";
            html += "<tr>";
            html += "<th>STT</th>";
            html += "<th>Email</th>";
            html += "<th>Họ Tên</th>";
            html += "<th>Kết Quả Thi</th>";
            html += "</tr>";

            int stt = 1;

            foreach (User user in listUsers)
            {
                html += "<tr>";
                html += "<td>" + stt + "</td>";
                html += "<td>" + user.Email + "</td>";
                html += "<td>" + user.HoTen + "</td>";
                html += "<form method='post'>";
                html += "<td><button id='btnKetQuaThiUser' class='KetQuaThiUser' name='btnKetQuaThiUser' value='" + user.Id + "'>Xem</button></td>";
                html += "</form>";
                html += "</tr>";

                stt++;
            }

            html += "</table>";
            tableUsers.InnerHtml = html;

            foreach (User user in listUsers)
            {
                if (Request.Form["btnKetQuaThiUser"] == Convert.ToString(user.Id))
                {
                    Session["adminXemKetQuaThiUser"] = true;
                    Response.Redirect("QuanLyKetQuaThiUser.aspx?id=" + user.Id);
                }
            }


        }
    }
}