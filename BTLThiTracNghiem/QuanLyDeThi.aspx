<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/layout.Master" CodeBehind="QuanLyDeThi.aspx.cs" Inherits="BTLThiTracNghiem.QuanLyDeThi" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <button class="btnThemMoiDeThi" id="btnThemDeThi" name="btnThemDeThi" value="">Thêm đề thi mới</button>
    <div class="add-exam-box">
        <form method="post">
            <h3>Đề Thi</h3>
            <input type="text" name="txtTieuDe" id="txtTieuDe" class="box" placeholder="Tiêu đề đề thi" required>
            <input type="number" name="txtSoLuongCauHoi" id="txtSoLuongCauHoi" class="box"
                placeholder="Số lượng câu hỏi" required>
            <input type="number" name="txtThoiGianLamBai" id="txtThoiGianLamBai" class="box"
                placeholder="Thời gian làm bài (phút)" required>
            <input type="text" name="txtLoaiBaiThi" id="txtLoaiBaiThi" class="box" placeholder="Loại bài thi" required>
            <input type="text" name="txtMaCaThi" id="txtMaCaThi" class="box" placeholder="Mã ca thi (nếu có)">
            <input type="submit" class="btn" name="btnTaoDeThi" id="btnTaoDeThi" value="Tạo đề thi">
        </form>
    </div>
    <div class="table-exams" id="listExam" name="listExam" runat="server">
       
    </div>

    <div class="soLuongBoDeThi" id="soLuongBoDeThihtml" runat="server">

    </div>
    <style>
        /* Phần css của tạo đề thi admin */
        .soLuongBoDeThi{
            font-size:1.5rem;
            margin-bottom:3rem;
            text-align:center;
        }
        .add-exam-box {
            position: relative;
            padding: 6rem 30%;
        }

        .add-exam-box {
            position: relative;
            align-items: center;
            justify-content: center;
        }

            .add-exam-box form {
                margin: 1rem;
                padding: 1rem 1rem;
                border-radius: .5rem;
                background-color: #f2f2f2;
            }

                .add-exam-box form h3 {
                    font-size: 3rem;
                    color: #444;
                    text-transform: uppercase;
                    text-align: center;
                    padding: 1rem 0;
                }

                .add-exam-box form .box {
                    width: 100%;
                    padding: 1rem;
                    font-size: 1.5rem;
                    color: #333;
                    margin: 1rem 0;
                    border: .1rem solid rgba(0, 0, 0, .3);
                    text-transform: none;
                }

                .add-exam-box form .btn {
                    display: inline-block;
                    width: 100%;
                }

                .add-exam-box form .box:focus {
                    border-color: var(--orange);
                }

        .add-exam-box {
            display: none;
        }

        .btnThemMoiDeThi {
            display: inline-block;
            margin: 0 10%;
            margin-top: 7rem;
            margin-bottom: 3rem;
            background: #ffc04d;
            padding: 1rem 2rem;
            text-align: center;
            text-decoration: none;
            border: none;
            cursor: pointer;
            font-size: 1.5rem;
        }

        .table-exams {
            padding: 0 10%;
            margin-bottom: 3rem;
        }

            .table-exams table {
                font-family: arial, sans-serif;
                border-collapse: collapse;
                width: 100%;
                font-size: 1.5rem;
            }

                .table-exams table td,
                th {
                    border: .1rem solid #f2f2f2;
                    text-align: left;
                    padding: 1rem;
                }

                .table-exams table tr:nth-child(odd) {
                    background-color: #dddddd;
                }

                .table-exams table td #btnDaLamBai {
                    background-color: #ffc04d;
                    padding: .4rem 1rem;
                    color: #000;
                    text-align: center;
                    text-decoration: none;
                    border: none;
                    cursor: pointer;
                    font-size: 1.5rem;
                    border-radius: .5rem;
                }

                .table-exams table td #btnChiTiet {
                    background: #3366ff;
                    padding: .2rem 2rem;
                    color: #fff;
                    text-align: center;
                    text-decoration: none;
                    border: none;
                    cursor: pointer;
                    font-size: 1.5rem;
                    border-radius: .5rem;
                }

                .table-exams table td #btnXoaDeThi {
                    background: #ff1a1a;
                    padding: 1rem 2rem;
                    color: #fff;
                    text-align: center;
                    text-decoration: none;
                    border: none;
                    cursor: pointer;
                    font-size: 1.5rem;
                    border-radius: .5rem;
                }

@media(max-width: 768px){
    html{font-size:46%}
      .add-exam-box {
            padding: 6rem 2%;
        }

       .table-exams {
            padding: 0 1%;
        }
}
    </style>
</asp:Content>
