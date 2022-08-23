<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/layout.Master" CodeBehind="QuanLyCauHoiDeThi.aspx.cs" Inherits="BTLThiTracNghiem.QuanLyCauHoiDeThi" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
        /* Phần css quản lý câu hỏi của đề thi admin */
        .add-questions-exam-container {
            display: grid;
            grid-template-columns: 30% 70%;
            gap: 1rem;
            padding: 8rem 10%;
        }

            .add-questions-exam-container .title-exam {
                position: relative;
                background-color: #f2f2f2;
                padding: 2rem 2rem;
                font-size: 1.5rem;
            }

                .add-questions-exam-container .title-exam h3 {
                    padding: 1rem 0;
                }

                .add-questions-exam-container .title-exam p {
                    padding: .5rem 0;
                }

            .add-questions-exam-container .add-questions {
                position: relative;
                align-items: center;
                justify-content: center;
                background-color: #f2f2f2;
            }

                .add-questions-exam-container .add-questions form {
                    margin: 1rem;
                    padding: 1rem 1rem;
                    background-color: #f2f2f2;
                    border-radius: .5rem;
                }

                    .add-questions-exam-container .add-questions form .box {
                        width: 100%;
                        padding: 1rem;
                        font-size: 1.5rem;
                        color: #333;
                        margin: 1rem 0;
                        border: .1rem solid rgba(0, 0, 0, .3);
                        text-transform: none;
                    }

                    .add-questions-exam-container .add-questions form h3 {
                        font-size: 3rem;
                        color: #444;
                        text-transform: uppercase;
                        text-align: center;
                        padding: 1rem 0;
                    }

                    .add-questions-exam-container .add-questions form .box:focus {
                        border-color: var(--orange);
                    }

                    .add-questions-exam-container .add-questions form .btnAddQuestion {
                        display: inline-block;
                        background: #ffc04d;
                        padding: 1rem 2rem;
                        text-align: center;
                        text-decoration: none;
                        border: none;
                        cursor: pointer;
                        font-size: 1.5rem;
                    }

        .table-questions {
            padding: 0 10%;
            margin-bottom: 4rem;
        }

            .table-questions table {
                border-collapse: collapse;
                width: 100%;
                font-size: 1.5rem;
            }

                .table-questions table td,
                th {
                    border: .1rem solid #f2f2f2;
                    text-align: left;
                    padding: 1.5rem;
                }

                .table-questions table tr:nth-child(odd) {
                    background-color: #dddddd;
                }

                .table-questions table td img {
                    width: 100%;
                    max-width: 10rem;
                }

                .table-questions table td #btnXoaCauHoi {
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

        @media(max-width: 768px) {
            html {
                font-size: 46%
            }

             .add-questions-exam-container {
            padding: 8rem 2%;
        }

               .table-questions {
            padding: 0 1%;
        }

               .table-questions table {
                width: 100%;
            }

        }
    </style>
    <div class="add-questions-exam-container">
        <div class="title-exam" id="titleExam" name="titleExam" runat="server">
        </div>
        <div class="add-questions">
            <form action="" method="post" enctype="multipart/form-data">
                <h3>Câu Hỏi</h3>
                <input type="text" class="box" name="txtCauHoi" id="txtCauHoi" class="box" placeholder="Câu hỏi" required>
                <input type="file" class="box" name="imgCauHoi" id="imgCauHoi" class="box" accept="image/*">
                <input type="text" class="box" name="txtDapAnA" id="txtDapAnA" class="box" placeholder="Đáp án A" required>
                <input type="text" class="box" name="txtDapAnB" id="txtDapAnB" class="box" placeholder="Đáp án B" required>
                <input type="text" class="box" name="txtDapAnC" id="txtDapAnC" class="box" placeholder="Đáp án C" required>
                <input type="text" class="box" name="txtDapAnD" id="txtDapAnD" class="box" placeholder="Đáp án D" required>
                <input type="text" class="box" name="txtDapAnDung" id="txtDapAnDung" class="box"
                    placeholder="Đáp án đúng (VD:A)" required>
                <input type="submit" class="btnAddQuestion" name="btnThemCauHoi" id="btnThemCauHoi"
                    value="Thêm câu hỏi">
            </form>
        </div>
    </div>
    <div class="table-questions" id="listQuestion" name="listQuestion" runat="server">
    </div>

</asp:Content>
