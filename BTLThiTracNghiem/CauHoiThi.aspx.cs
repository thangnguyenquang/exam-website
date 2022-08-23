using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BTLThiTracNghiem
{
    public partial class CauHoiThi : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if ((string)Session["email"] == "" && (bool)Session["login"] == false)
            {
                Response.Redirect("TrangChu.aspx");
            }

            Session["DangNhapLamBaiThi"] = false;

            if (!IsPostBack)
            {
                Session["thoiGianBatDau"] = DateTime.Now;
            }

            //Dếm ngược thời gian làm bài
            if (!IsPostBack)
            {
                Session["timer"] = DateTime.Now.AddMinutes(Convert.ToDouble(Session["thoiGianLamBai"].ToString()));
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

            string htmlMiniBox = "";

            int stt = 1;

            foreach (Question question in listQuestionExam)
            {
                html += "<div class='box' id='box" + stt + "'>";
                html += "<div class='question' >";
                html += "<p><span>" + stt + "</span>" + question.CauHoi + "</p>";
                if (question.Url_img != "")
                {
                    html += " <img src = '" + question.Url_img + "' alt=''>";
                }
                html += "</div>";
                html += "<div class='answers'>";
                html += "<input type='hidden' id='idQuestion" + stt + "' name ='idQuestion" + stt + "' value='" + question.Id + "'>";
                html += "<div>";
                html += "<input type ='radio' name='question" + stt + "' value='A' id='answeraQuestion" + stt + "' runat='server'>";
                html += "<label for='answeraQuestion" + stt + "'><b>A.</b></label>";
                html += "<label class='answer-content'>" + question.DapAn_A + "</label>";
                html += "</div>";
                html += "<div>";
                html += "<input type = 'radio' name='question" + stt + "' value='B' id='answerbQuestion" + stt + "' runat='server'>";
                html += "<label for='answerbQuestion" + stt + "'><b>B.</b></label>";
                html += "<label class='answer-content'>" + question.DapAn_B + "</label>";
                html += "</div>";
                html += "<div>";
                html += "<input type = 'radio' name='question" + stt + "' value='C' id='answercQuestion" + stt + "' runat='server'>";
                html += "<label for='answercQuestion" + stt + "'><b>C.</b></label>";
                html += "<label class='answer-content'>" + question.DapAn_C + "</label>";
                html += "</div>";
                html += "<div>";
                html += "<input type = 'radio' name='question" + stt + "' value='D' id='answerdQuestion" + stt + "' runat='server'>";
                html += "<label for='answerdQuestion" + stt + "'><b>D.</b></label>";
                html += "<label class='answer-content'>" + question.DapAn_D + "</label>";
                html += "</div>";
                html += "</div>";
                html += "</div>";

                htmlMiniBox += "<div class='mini-box' id='mini-box-question" + stt + "'>";
                htmlMiniBox += "<p>" + stt + "</p>";
                htmlMiniBox += "</div>";

                stt++;
            }


            listQuestionExamHtml.InnerHtml = html;

            listMiniBoxQuestions.InnerHtml = htmlMiniBox;


            string pathUsers = "Users.xml";

            List<User> listUsers = new List<User>();

            if (File.Exists(Server.MapPath(pathUsers)))
            {
                // Đọc file
                System.Xml.Serialization.XmlSerializer reader = new System.Xml.Serialization.XmlSerializer(typeof(List<User>));
                StreamReader file = new StreamReader(Server.MapPath(pathUsers));

                listUsers = (List<User>)reader.Deserialize(file);
                listUsers = listUsers.OrderBy(User => User.Id).ToList();
                file.Close();
            }

            string hoTenNguoiLamBai = "";
            foreach (User user in listUsers)
            {
                if (user.Id == Convert.ToInt32(Session["id"].ToString()))
                {
                    hoTenNguoiLamBai = user.HoTen;
                    break;
                }
            }

            //Phần cột bên phải

            string htmlBoxExam = "";
            htmlBoxExam += "<p class='name-user' id='name-user'>" + hoTenNguoiLamBai + "</p>";

            headerBoxExam.InnerHtml = htmlBoxExam;


            if (Request.Form["btnNopBai"] == "Nộp bài")
            {
                Session["diemSo"] = tinhDiem();
                Session["idDeThi"] = idDeThi;

                Session["thoiGianKetThuc"] = DateTime.Now;

                TimeSpan KhoangThoiGian = Convert.ToDateTime(Session["thoiGianKetThuc"].ToString()).Subtract(Convert.ToDateTime(Session["thoiGianBatDau"].ToString()));

                string thoiGianLamBai = KhoangThoiGian.Hours + ":" + KhoangThoiGian.Minutes + ":" + KhoangThoiGian.Seconds;

                Session["thoiGianLamBai"] = thoiGianLamBai;

                Response.Redirect("KetQuaThi.aspx");
            }


        }

        private double tinhDiem()
        {
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

            //Lấy mã đề thi từ url
            int idDeThi = Convert.ToInt32(Request.QueryString["id"].ToString());

            int stt = 1;

            double diemSoMoiCau = 10 / Convert.ToDouble(Session["soLuongCauHoi"].ToString());

            double tongDiem = 0;

            int soCauHoanThanh = 0;

            int soDapAnDung = 0;

            //Lấy những câu hỏi có cùng một mã đề thi
            foreach (Question question in listQuestions)
            {
                if (Request.Form["idQuestion" + stt + ""] == Convert.ToString(question.Id) && question.Id_DeThi == idDeThi)
                {
                    if(Request.Form["question" + stt + ""] == "A" || Request.Form["question" + stt + ""] == "B" || Request.Form["question" + stt + ""] == "C" || Request.Form["question" + stt + ""] == "D")
                    {
                        soCauHoanThanh++;
                    }
                    if (Request.Form["question" + stt + ""] == Convert.ToString(question.DapAn))
                    {
                        tongDiem += diemSoMoiCau;
                        soDapAnDung++;
                    }
                    stt++;
                }
            }

            Session["soCauHoanThanh"] = soCauHoanThanh;

            Session["soDapAnDung"] = soDapAnDung;

            return tongDiem;
        }

        //Đếm ngược thời gian làm bài
        protected void timer_tick(object sender, EventArgs e)
        {
            if (DateTime.Compare(DateTime.Now, DateTime.Parse(Session["timer"].ToString())) < 0)
            {
                countdown.Text = ((Int32)DateTime.Parse(Session["timer"].ToString()).Subtract(DateTime.Now).TotalHours).ToString() +
                    ":" + ((Int32)DateTime.Parse(Session["timer"].ToString()).Subtract(DateTime.Now).TotalMinutes).ToString() +
                    ":" + (((Int32)DateTime.Parse(Session["timer"].ToString()).Subtract(DateTime.Now).TotalSeconds) % 60).ToString();
            }
            else
            {
                //Lấy mã đề thi từ url
                int idDeThi = Convert.ToInt32(Request.QueryString["id"].ToString());

                Session["diemSo"] = tinhDiem();
                Session["idDeThi"] = idDeThi;

                Session["thoiGianKetThuc"] = DateTime.Now;

                TimeSpan KhoangThoiGian = Convert.ToDateTime(Session["thoiGianKetThuc"].ToString()).Subtract(Convert.ToDateTime(Session["thoiGianBatDau"].ToString()));

                string thoiGianLamBai = KhoangThoiGian.Hours + ":" + KhoangThoiGian.Minutes + ":" + KhoangThoiGian.Seconds;

                Session["thoiGianLamBai"] = thoiGianLamBai;

                Response.Redirect("KetQuaThi.aspx");
            }

        }

    }
}