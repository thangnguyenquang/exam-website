using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BTLThiTracNghiem
{
    public partial class QuanLyDeThi : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if ((string)Session["email"] != "admin@gmail.com" && (bool)Session["login"] == false)
            {
                Response.Redirect("TrangChu.aspx");
            }

            string path = "Exams.xml";

            string pathQuestions = "Questions.xml";

            List<Exam> listExams = new List<Exam>();

            List<Question> listQuestions = new List<Question>();


            if (File.Exists(Server.MapPath(path)))
            {
                // Đọc file đề thi
                System.Xml.Serialization.XmlSerializer reader = new System.Xml.Serialization.XmlSerializer(typeof(List<Exam>));
                StreamReader file = new StreamReader(Server.MapPath(path));

                listExams = (List<Exam>)reader.Deserialize(file);
                listExams = listExams.OrderBy(Exam => Exam.Id).ToList();
                file.Close();
            }

            if (File.Exists(Server.MapPath(pathQuestions)))
            {
                // Đọc file câu hỏi
                System.Xml.Serialization.XmlSerializer reader = new System.Xml.Serialization.XmlSerializer(typeof(List<Question>));
                StreamReader file = new StreamReader(Server.MapPath(pathQuestions));

                listQuestions = (List<Question>)reader.Deserialize(file);
                listQuestions = listQuestions.OrderBy(Question => Question.Id).ToList();
                file.Close();
            }

            string html = "";
            html += "<table>";
            html += "<tr>";
            html += "<th>STT</th>";
            html += "<th>Tiêu Đề</th>";
            html += "<th>Ngày Tạo</th>";
            html += "<th>Số Lượng Câu Hỏi</th>";
            html += "<th>Thời Gian Làm Bài (Phút)</th>";
            html += "<th>Loại Bài Thi</th>";
            html += "<th>Mã Ca Thi</th>";
            html += "<th></th>";
            html += "<th></th>";
            html += "<th></th>";
            html += "</tr>";

            int stt = 1;
            int soLuongBoDeThi = 0;

            foreach (Exam exam in listExams)
            {

                html += "<tr>";
                html += "<td> " + stt + " </td>";
                html += "<td> " + exam.TieuDe + " </td>";
                html += "<td> " + exam.NgayTao + " </td>";
                html += "<td> " + exam.SoLuongCauHoi + " </td>";
                html += "<td> " + exam.ThoiGianLamBai + " </td>";
                html += "<td> " + exam.LoaiBaiThi + " </td>";
                html += "<td> " + exam.MaCaThi + " </td>";
                html += "<form method='post'>";
                html += "<td>";
                html += "<button id='btnDaLamBai' name='btnDaLamBai' value='" + exam.Id + "'>Những người đã làm</button></td>";
                html += "<td>";
                html += "<button id='btnChiTiet' name='btnChiTiet' value='" + exam.Id + "'>Chi tiết</button></td>";
                html += "<td>";
                html += "<button id='btnXoaDeThi' type='submit' name='btnXoaDeThi' value='" + exam.Id + "'>Xóa</button></td>";
                html += "<form>";
                html += "</tr>";

                stt++;

                soLuongBoDeThi++;

            }
            html += "</table>";

            listExam.InnerHtml = html;

            if (Request.Form["btnTaoDeThi"] == "Tạo đề thi")
            {
                DateTime now = DateTime.Now;

                Exam newExam = new Exam();
                newExam.Id = listExams.Count;
                newExam.TieuDe = Request.Form["txtTieuDe"];
                newExam.NgayTao = now.ToString();
                newExam.SoLuongCauHoi = Int32.Parse(Request.Form["txtSoLuongCauHoi"]);
                newExam.ThoiGianLamBai = float.Parse(Request.Form["txtThoiGianLamBai"]);
                newExam.LoaiBaiThi = Request.Form["txtLoaiBaiThi"];
                newExam.MaCaThi = Request.Form["txtMaCaThi"];

                bool trung = false; //Kiểm tra đề thi đã tồn tại hay chưa
                foreach (Exam exam in listExams)
                {
                    if (exam.TieuDe == newExam.TieuDe)
                    {
                        trung = true;
                        break;
                    }
                }

                if (trung == false)
                {
                    foreach (Exam exam in listExams)
                    {
                        if (exam.Id == newExam.Id)
                        {
                            newExam.Id++;
                        }
                    }

                    listExams.Add(newExam);
                    //Ghi file
                    System.Xml.Serialization.XmlSerializer writer = new System.Xml.Serialization.XmlSerializer(typeof(List<Exam>));

                    System.IO.FileStream _file = System.IO.File.Create(Server.MapPath(path));

                    writer.Serialize(_file, listExams);
                    _file.Close();

                    // Tạo session
                    Session["idDeThi"] = newExam.Id;
                    Session["tieuDe"] = newExam.TieuDe;
                    Session["soLuongCauHoi"] = newExam.SoLuongCauHoi;
                    Session["thoiGianLamBai"] = newExam.ThoiGianLamBai;
                    Session["loaiBaiThi"] = newExam.LoaiBaiThi;

                    Response.Redirect("QuanLyCauHoiDeThi.aspx?id=" + newExam.Id);

                }
                else
                {
                    Response.Write("<script>alert('Đề thi đã tồn tại!');</script>");
                }
            }

            foreach (Exam exam in listExams)
            {
                if (Request.Form["btnXoaDeThi"] == Convert.ToString(exam.Id))
                {
                    listExams.Remove(exam);
                    //Ghi file
                    System.Xml.Serialization.XmlSerializer writer = new System.Xml.Serialization.XmlSerializer(typeof(List<Exam>));

                    System.IO.FileStream _file = System.IO.File.Create(Server.MapPath(path));

                    writer.Serialize(_file, listExams);
                    _file.Close();

                    //Xóa câu hỏi có cùng mã đề thi xóa
                    foreach (Question question in listQuestions.ToList())
                    {
                        if(question.Id_DeThi == exam.Id)
                        {
                            listQuestions.Remove(question);
                        }
                    }
                    //Ghi file
                    System.Xml.Serialization.XmlSerializer writer2 = new System.Xml.Serialization.XmlSerializer(typeof(List<Question>));

                    System.IO.FileStream _file2 = System.IO.File.Create(Server.MapPath(pathQuestions));

                    writer2.Serialize(_file2, listQuestions);
                    _file2.Close();

                    Page.Response.Redirect(Page.Request.Url.ToString(), true);
                    break;
                }
            }

            foreach (Exam exam in listExams)
            {
                if (Request.Form["btnChiTiet"] == Convert.ToString(exam.Id))
                {
                    Response.Redirect("QuanLyCauHoiDeThi.aspx?id=" + exam.Id );
                }
            }

            foreach (Exam exam in listExams)
            {
                if (Request.Form["btnDaLamBai"] == Convert.ToString(exam.Id))
                {
                    Session["adminXemNhungNguoiDaLamBaiThi"] = true;
                    Response.Redirect("QuanLyKetQuaThiUser.aspx?id=" + exam.Id);
                }
            }

            string htmlSoLuongBoDeThi = "";
            htmlSoLuongBoDeThi += "<h3>Có tổng cộng "+ soLuongBoDeThi + " bộ đề thi </3>";

            soLuongBoDeThihtml.InnerHtml = htmlSoLuongBoDeThi;

        }
    }
}