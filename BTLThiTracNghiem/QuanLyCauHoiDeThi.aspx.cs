using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BTLThiTracNghiem
{
    public partial class QuanLyCauHoiDeThi : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if ((string)Session["email"] != "admin@gmail.com" && (bool)Session["login"] == false)
            {
                Response.Redirect("TrangChu.aspx");
            }

            string path = "Questions.xml";

            List<Question> listQuestions = new List<Question>();

            if (File.Exists(Server.MapPath(path)))
            {
                // Đọc file câu hỏi
                System.Xml.Serialization.XmlSerializer reader = new System.Xml.Serialization.XmlSerializer(typeof(List<Question>));
                StreamReader file = new StreamReader(Server.MapPath(path));

                listQuestions = (List<Question>)reader.Deserialize(file);
                listQuestions = listQuestions.OrderBy(Question => Question.Id).ToList();
                file.Close();
            }

            string pathExams = "Exams.xml";

            List<Exam> listExams = new List<Exam>();

            if (File.Exists(Server.MapPath(pathExams)))
            {
                // Đọc file đề thi
                System.Xml.Serialization.XmlSerializer reader = new System.Xml.Serialization.XmlSerializer(typeof(List<Exam>));
                StreamReader file = new StreamReader(Server.MapPath(pathExams));

                listExams = (List<Exam>)reader.Deserialize(file);
                listExams = listExams.OrderBy(Exam => Exam.Id).ToList();
                file.Close();
            }
            
            //Lấy mã đề thi từ url
            int idDeThi = Convert.ToInt32(Request.QueryString["id"].ToString());
            //Lấy những câu hỏi có cùng một mã đề thi
            List<Question> listQuestionExam = new List<Question>();
            foreach (Question question in listQuestions)
            {
                if (question.Id_DeThi == idDeThi)
                {
                    listQuestionExam.Add(question);
                }
            }

            string html = "";

            //html phần tiêu đề đề thi
            string htmlTitleExam = "";

            foreach(Exam exam in listExams)
            {
                if(exam.Id == idDeThi)
                {
                    htmlTitleExam += "<h3>" + exam.TieuDe + "</h3>";
                    htmlTitleExam += "<p>Số lượng câu hỏi : <span>" + exam.SoLuongCauHoi + "</span></p>";
                    htmlTitleExam += "<p>Thời gian làm bài : <span>" + exam.ThoiGianLamBai + " phút</span></p>";
                    htmlTitleExam += "<p>Loại bài thi : <span>" + exam.LoaiBaiThi + "</span></p>";
                    titleExam.InnerHtml = htmlTitleExam;
                    break;
                }
            }

            //html phần bảng hiển thị các câu hỏi của đề thi
            html += "<table>";
            html += "<tr>";
            html += "<th>STT</th>";
            html += "<th>Câu Hỏi</th>";
            html += "<th>Ảnh</th>";
            html += "<th>Đáp Án A</th>";
            html += "<th>Đáp Án B</th>";
            html += "<th>Đáp Án C</th>";
            html += "<th>Đáp Án D</th>";
            html += "<th>Đáp Án Đúng</th>";
            html += "<th></th>";
            html += "</tr>";

            //Số thứ tự câu hỏi
            int stt = 1;

            Session["soLuongCauHoiDaThem"] = 0;

            foreach (Question question in listQuestionExam)
            {
                html += "<tr>";
                html += "<td> " + stt + " </td>";
                html += "<td> " + question.CauHoi + " </td>";
                html += "<td>";
                if(question.Url_img != "")
                {
                    html += "<a target='_blank' href='" + question.Url_img + "'>";
                    html += "<img src='" + question.Url_img + "'>";
                    html += "</a>";
                }                
                html += "</td>";
                html += "<td> " + question.DapAn_A + " </td>";
                html += "<td> " + question.DapAn_B + " </td>";
                html += "<td> " + question.DapAn_C + " </td>";
                html += "<td> " + question.DapAn_D + " </td>";
                html += "<td> " + question.DapAn + " </td>";
                html += "<form method='post'>";
                html += "<td>";
                html += "<button id='btnXoaCauHoi' name='btnXoaCauHoi' value='" + question.Id + "'>Xóa</button></td>";
                html += "</form>";
                html += "</tr>";

                stt++;

                Session["soLuongCauHoiDaThem"] = stt;

            }


            html += "</table>";

            listQuestion.InnerHtml = html;

            foreach (Exam exam in listExams)
            {
                if (exam.Id == idDeThi)
                {
                    Session["soLuongCauHoi"] = exam.SoLuongCauHoi;
                    break;
                }
            }

            int demSoLuongCauHoiThem = Convert.ToInt32(Session["soLuongCauHoiDaThem"].ToString());

            if (Request.Form["btnThemCauHoi"] == "Thêm câu hỏi")
            {
                if (demSoLuongCauHoiThem <= Convert.ToInt32(Session["soLuongCauHoi"].ToString()))
                {

                    Question newQuestion = new Question();
                    newQuestion.Id = listQuestions.Count;
                    newQuestion.Id_DeThi = idDeThi;
                    newQuestion.CauHoi = Request.Form["txtCauHoi"];

                    //Truy cập tệp bằng tên của tệp HTML INPUT.
                    HttpPostedFile txtFile = Request.Files["imgCauHoi"];
                    string imageUrl = "";
                    //Kiểm tra file có hợp lệ.
                    if (txtFile != null && txtFile.ContentLength > 0)
                    {
                        try
                        {
                            string filename = Path.GetFileName(txtFile.FileName);
                            string filePath = Server.MapPath("~/Images/") + filename;
                            txtFile.SaveAs(filePath);
                            imageUrl = "Images/" + filename;
                        }
                        catch (Exception ex)
                        {
                            Response.Write("<script>alert('Đã xảy ra lỗi không thể tải file lên! " + ex.Message + "');</script>");
                        }
                    }
                    newQuestion.Url_img = imageUrl;

                    newQuestion.DapAn_A = Request.Form["txtDapAnA"];
                    newQuestion.DapAn_B = Request.Form["txtDapAnB"];
                    newQuestion.DapAn_C = Request.Form["txtDapAnC"];
                    newQuestion.DapAn_D = Request.Form["txtDapAnD"];
                    newQuestion.DapAn = Request.Form["txtDapAnDung"];


                    bool trung = false; //Kiểm tra câu hỏi đã tồn tại hay chưa
                    foreach (Question question in listQuestions)
                    {
                        if (question.CauHoi.Equals(newQuestion.CauHoi) && question.Id_DeThi == newQuestion.Id_DeThi)
                        {
                            trung = true;
                            break;
                        }
                    }

                    if (trung == false)
                    {
                        foreach (Question question in listQuestions)
                        {
                            if (question.Id == newQuestion.Id)
                            {
                                newQuestion.Id++;
                            }
                        }

                        listQuestions.Add(newQuestion);
                        //Ghi file
                        System.Xml.Serialization.XmlSerializer writer = new System.Xml.Serialization.XmlSerializer(typeof(List<Question>));

                        System.IO.FileStream _file = System.IO.File.Create(Server.MapPath(path));

                        writer.Serialize(_file, listQuestions);
                        _file.Close();

                        Page.Response.Redirect(Page.Request.Url.ToString(), true);

                    }
                    else
                    {
                        Response.Write("<script>alert('Câu hỏi đã tồn tại!');</script>");
                    }
                }
                else
                {
                    Response.Write("<script>alert('Đã thêm đủ số câu hỏi!');</script>");
                }
            }

            foreach (Question question in listQuestions)
            {
                if (Request.Form["btnXoaCauHoi"] == Convert.ToString(question.Id))
                {
                    listQuestions.Remove(question);
                    //Ghi file
                    System.Xml.Serialization.XmlSerializer writer = new System.Xml.Serialization.XmlSerializer(typeof(List<Question>));

                    System.IO.FileStream _file = System.IO.File.Create(Server.MapPath(path));

                    writer.Serialize(_file, listQuestions);
                    _file.Close();

                    Page.Response.Redirect(Page.Request.Url.ToString(), true);
                    break;
                }
            }


        }
    }
}