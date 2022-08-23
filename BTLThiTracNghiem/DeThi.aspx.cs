using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BTLThiTracNghiem
{
    public partial class DeThi : System.Web.UI.Page
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

            //Lấy mã đề thi từ url
            int idDeThi = Int32.Parse(Request.QueryString["id"].ToString());

            string html = "";

            string htmlBtnDeThi = "";

            int soLuongCauHoi = 0;
            float thoiGianLamBai = 0;
            foreach (Exam exam in listExams)
            {
                if (exam.Id == idDeThi)
                {
                    html += "<h2>" + exam.TieuDe + "</h2>";
                    html += "<p class='border-bottom'>Ngày tạo: " + exam.NgayTao + "</p>";

                    htmlBtnDeThi += "<button id='btnBatDauLamBai' name='btnBatDauLamBai' value='" + exam.Id + "'><i class='fa-solid fa-pen-to-square'></i>Bắt đầu làm bài</button>";
                    htmlBtnDeThi += "<button id='btnQuayLai' name='btnQuayLai' value='Quay lại'><i class='fa-solid fa-arrow-rotate-left'></i>Quay lại</button>";

                    soLuongCauHoi = exam.SoLuongCauHoi;

                    thoiGianLamBai = exam.ThoiGianLamBai;

                    Session["tieuDe"] = exam.TieuDe;
                    Session["maCaThi"] = exam.MaCaThi;
                    break;
                }
            }

            title.InnerHtml = html;

            btnDeThi.InnerHtml = htmlBtnDeThi;

            string htmlExamCode = "";

            if (Session["maCaThi"].ToString() != "")
            {
                htmlExamCode += "<input type='text' name='examCode' class='box' placeholder='Nhập mã ca thi' runat='server' required>";

                examCode.InnerHtml = htmlExamCode;
            }

            if (Request.Form["btnBatDauLamBai"] == Convert.ToString(idDeThi))
            {
                if (Session["maCaThi"].ToString() != "")
                {
                    if (Request.Form["examCode"] == Convert.ToString(Session["maCaThi"]))
                    {
                        Session["soLuongCauHoi"] = soLuongCauHoi;
                        Session["thoiGianLamBai"] = thoiGianLamBai;

                        if ((bool)Session["login"] == true && (string)Session["email"] != "")
                        {
                            Response.Redirect("CauHoiThi.aspx?id=" + idDeThi);
                        }
                        else
                        {
                            Session["DangNhapLamBaiThi"] = true;
                            Session["idDeThi"] = idDeThi;
                            Response.Redirect("DangNhap.aspx");
                        }
                    }
                    else
                    {
                        Response.Write("<script>alert('Mã ca thi không chính xác!');</script>");
                    }
                }
                else
                {
                    Session["soLuongCauHoi"] = soLuongCauHoi;
                    Session["thoiGianLamBai"] = thoiGianLamBai;

                    if ((bool)Session["login"] == true && (string)Session["email"] != "")
                    {
                        Response.Redirect("CauHoiThi.aspx?id=" + idDeThi);
                    }
                    else
                    {
                        Session["DangNhapLamBaiThi"] = true;
                        Session["idDeThi"] = idDeThi;
                        Response.Redirect("DangNhap.aspx");
                    }
                }

            }

            if (Request.Form["btnQuayLai"] == "Quay lại")
            {
                Response.Redirect("TrangChu.aspx");
            }
        }
    }
}