using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BTLThiTracNghiem
{
    public partial class KetQuaThi : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if ((string)Session["email"] == "" && (bool)Session["login"] == false)
            {
                Response.Redirect("TrangChu.aspx");
            }

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

            string html = "";

            html += "<p class='title-result-exam'>" + Session["tieuDe"] + "</p>";
            html += "<div class='result-exam'>";
            html += "<h3>Điểm số bài thi</h3>";
            html += "<h2 class='scores-exam'>" + Session["diemSo"] + "</h2>";
            //html += "<p>Số câu hoàn thành:<span>" + Session["soCauHoanThanh"] + "/" + Session["soLuongCauHoi"] + "</span></p>";
            html += "<p>Số đáp án đúng:<span>" + Session["soDapAnDung"] + "/" + Session["soLuongCauHoi"] +"</span></p>";
            html += "<p>Thời gian làm bài:<span>" + Session["thoiGianLamBai"] + "</span> </p>";
            html += "</div>";

            ketQuaThi.InnerHtml = html;

            if(Request.Form["btnThoatBaiThi"] == "Thoát bài thi")
            {
                Response.Redirect("TrangChu.aspx");
            }

            
            Result newResult = new Result();
            newResult.Id = listResults.Count;
            foreach (Result result in listResults)
            {
                if(result.Id == newResult.Id)
                {
                    newResult.Id++;
                }
            }
            newResult.IdUser = Convert.ToInt32(Session["id"]);
            newResult.IdDeThi = Convert.ToInt32(Session["idDeThi"]);
            newResult.HoTen = Session["hoTen"].ToString();
            newResult.TieuDeDeThi = Session["tieuDe"].ToString();
            newResult.ThoiGianBatDau = Session["thoiGianBatDau"].ToString();
            newResult.ThoiGianKetThuc = Session["thoiGianKetThuc"].ToString();
            newResult.ThoiGianLamBai = Session["thoiGianLamBai"].ToString();
            newResult.DiemSo = Convert.ToDouble(Session["diemSo"]);

            listResults.Add(newResult);
            //Ghi file
            System.Xml.Serialization.XmlSerializer writer = new System.Xml.Serialization.XmlSerializer(typeof(List<Result>));

            System.IO.FileStream _file = System.IO.File.Create(Server.MapPath(path));

            writer.Serialize(_file, listResults);
            _file.Close();

            //Page.Response.Redirect(Page.Request.Url.ToString(), true);
        }
    }
}