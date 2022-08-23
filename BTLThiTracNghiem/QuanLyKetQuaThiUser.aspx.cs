using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BTLThiTracNghiem
{
    public partial class QuanLyBaiThiUser : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string path = "Results.xml";

            List<Result> listResults = new List<Result>();

            if (File.Exists(Server.MapPath(path)))
            {
                // Đọc file
                System.Xml.Serialization.XmlSerializer reader = new System.Xml.Serialization.XmlSerializer(typeof(List<Result>));
                StreamReader file = new StreamReader(Server.MapPath(path));

                listResults = (List<Result>)reader.Deserialize(file);
                listResults = listResults.OrderBy(Result => Result.Id).ToList();
                file.Close();
            }

            string pathUser = "Users.xml";

            List<User> listUsers = new List<User>();

            if (File.Exists(Server.MapPath(pathUser)))
            {
                // Đọc file
                System.Xml.Serialization.XmlSerializer reader = new System.Xml.Serialization.XmlSerializer(typeof(List<User>));
                StreamReader file = new StreamReader(Server.MapPath(pathUser));

                listUsers = (List<User>)reader.Deserialize(file);
                listUsers = listUsers.OrderBy(User => User.Id).ToList();
                file.Close();
            }

            //id user khi xem kết quả thi của user 
            //id đề khi khi xem những người đã làm một bài thi
            
            int id = Convert.ToInt32(Request.QueryString["id"].ToString());

            string html = "";
            html += "<div class='name-user'>";
            foreach(User user in listUsers)
            {
                if ((bool)Session["adminXemKetQuaThiUser"] == true && user.Id == id)
                {
                    html += "<h3>Kết quả thi của " + user.HoTen + "</h3>";
                    break;
                }
                if((bool)Session["adminXemNhungNguoiDaLamBaiThi"] == true)
                {
                    html += "";
                    break;
                }
                if((bool)Session["adminXemKetQuaThiUser"] == false && (bool)Session["adminXemNhungNguoiDaLamBaiThi"] == false)
                {
                    html += "<h3>Kết quả thi của " + Session["hoTen"] + "</h3>";
                    break;
                }
            }
            html += "</div>";
            html += "<div class='table-result-user'>";
            html += "<table>";
            html += "<tr>";
            if((bool)Session["adminXemNhungNguoiDaLamBaiThi"] == true)
            {
                html += "<th>Họ Tên</th>";
            }
            else
            {
                html += "<th>Tiêu Đề Bài Thi</th>";
            }
            html += "<th>Thời Gian Bắt Đầu</th>";
            html += "<th>Thời Gian Kết Thúc</th>";
            html += "<th>Thời Gian Làm Bài</th>";
            html += "<th>Điểm Số</th>";
            html += "<th>Chi Tiết</th>";
            html += "</tr>";

            foreach(Result result in listResults)
            {
                if (result.IdUser == id && (bool)Session["adminXemKetQuaThiUser"] == true)
                {
                    html += "<tr>";
                    html += "<td>" + result.TieuDeDeThi + "</td>";
                    html += "<td>" + result.ThoiGianBatDau + "</td>";
                    html += "<td>" + result.ThoiGianKetThuc + "</td>";
                    html += "<td>" + result.ThoiGianLamBai + "</td>";
                    html += "<td>" + result.DiemSo + "</td>";
                    html += "<td><a href = '' >Xem </a></td>";
                    html += "</tr>";
                }
                else if (result.IdDeThi == id && (bool)Session["adminXemNhungNguoiDaLamBaiThi"] == true)
                {
                    html += "<tr>";
                    html += "<td>" + result.HoTen + "</td>";
                    html += "<td>" + result.ThoiGianBatDau + "</td>";
                    html += "<td>" + result.ThoiGianKetThuc + "</td>";
                    html += "<td>" + result.ThoiGianLamBai + "</td>";
                    html += "<td>" + result.DiemSo + "</td>";
                    html += "<td><a href = '' >Xem </a></td>";
                    html += "</tr>";
                }
                else if (result.IdUser == Convert.ToInt32(Session["id"].ToString()) && (bool)Session["adminXemNhungNguoiDaLamBaiThi"] == false)
                {
                    html += "<tr>";
                    html += "<td>" + result.TieuDeDeThi + "</td>";
                    html += "<td>" + result.ThoiGianBatDau + "</td>";
                    html += "<td>" + result.ThoiGianKetThuc + "</td>";
                    html += "<td>" + result.ThoiGianLamBai + "</td>";
                    html += "<td>" + result.DiemSo + "</td>";
                    html += "<td><a href = '' >Xem </a></td>";
                    html += "</tr>";
                }
                   
            }

            html += "</table>";
            html += "</div>";

            resultUser.InnerHtml = html;

            Session["adminXemNhungNguoiDaLamBaiThi"] = false;
            Session["adminXemKetQuaThiUser"] = false;
        }
    }
}