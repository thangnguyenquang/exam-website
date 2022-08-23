using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BTLThiTracNghiem
{
    public partial class layout : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                string html = "";
                if ((bool)Session["login"] == true && (string)Session["email"] == "admin@gmail.com")
                {
                    html += "<a href = '#' class='drop-btn' onclick='dropContentAccount()'>";
                    html += "<i class='fa fa-user' id='login-btn'></i> " + Session["email"] + " <i class='fa-solid fa-caret-down'></i></a>";
                    html += "<div id='dropDownContentAccount' class='dropdown-content-account'>";
                    html += "<a href ='QuanLyUser.aspx' > Quản lý tài khoản </a>";
                    html += "<a href ='QuanLyDeThi.aspx' > Quản lý đề thi </a >";
                    html += "<a href = 'DangXuat.aspx' id='btnDangXuat'> Thoát tài khoản</a ></div > ";

                    account.InnerHtml = html;
                }
                else if ((bool)Session["login"] == true)
                {
                    html += "<a href = '#' class='drop-btn' onclick='dropContentAccount()'>";
                    html += "<i class='fa fa-user' id='login-btn'></i> " + Session["email"] + " <i class='fa-solid fa-caret-down'></i></a>";
                    html += "<div id='dropDownContentAccount' class='dropdown-content-account'>";
                    html += "<a href = QuanLyKetQuaThiUser.aspx?id=" + Session["id"] + " id='btnKetQuaThi' value='Kết quả thi'>Kết quả thi</a> ";
                    html += "<a href = 'DangXuat.aspx' id='btnDangXuat' value='Đăng xuất'>Thoát tài khoản</a > ";
                    html += "</div>";

                    account.InnerHtml = html;
                }
                else
                {
                    html += "<a href='DangNhap.aspx'><i class='fa fa-user' id='login-btn'></i></a>";

                    account.InnerHtml = html;
                }

            }







            //string ipaddress;
            //ipaddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            //if (ipaddress == "" || ipaddress == null)
            //{
            //    ipaddress = Request.ServerVariables["REMOTE_ADDR"];
            //}
        }
    }
}
