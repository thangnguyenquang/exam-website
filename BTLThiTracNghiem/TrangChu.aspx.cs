using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BTLThiTracNghiem
{
    public partial class TrangChu : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            string path = "Exams.xml";

            List<Exam> listExams = new List<Exam>();

            if (File.Exists(Server.MapPath(path)))
            {
                // Đọc file đề thi
                System.Xml.Serialization.XmlSerializer reader = new System.Xml.Serialization.XmlSerializer(typeof(List<Exam>));
                StreamReader file = new StreamReader(Server.MapPath(path));

                listExams = (List<Exam>)reader.Deserialize(file);
                listExams = listExams.OrderBy(Exam => Exam.Id).ToList();
                file.Close();
            }

            string html = "";

            foreach(Exam exam in listExams)
            {
                html += "<div class='box'>";
                html += "<div class='title'>";
                html += "<a href = DeThi.aspx?id=" + exam.Id + ">";
                html += "<h3>" + exam.TieuDe + "</h3>";
                html += "</a>";
                html += "<br>";
                html += "<p>" + exam.NgayTao + "</p>";
                html += "</div>";
                html += "<div class='details'>";
                html += "<ul>";
                html += "<li>Số lượng câu hỏi : <span>" + exam.SoLuongCauHoi + "</span></li>";
                html += "<li>Thời gian làm bài : <span>" + exam.ThoiGianLamBai + " phút</span></li>";
                html += "<li>Loại bài thi : <span>" + exam.LoaiBaiThi + "</span></li>";
                html += "</ul>";
                html += "</div>";
                html += "<form method='post'>";
                html += "<button class='detail-btn' id='btnChiTietDeThi' name='btnChiTietDeThi' value='" + exam.Id + "'>Chi tiết<i class='fa-solid fa-angles-right'></i></button>";
                html += "</form>";
                html += "</div>";
            }

            listExamHomePage.InnerHtml = html;

            foreach(Exam exam in listExams)
            {
                if (Request.Form["btnChiTietDeThi"] == Convert.ToString(exam.Id))
                {
                    Response.Redirect("DeThi.aspx?id=" + exam.Id);
                }
            }

        }
    }
}