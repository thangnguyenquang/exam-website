﻿<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/layout.Master" CodeBehind="TrangChu.aspx.cs" Inherits="BTLThiTracNghiem.TrangChu" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="image-section">
        <img src="online exam.jpg" alt="">
    </div>
    <div class="title-head-exam">
        <h3>đề thi trắc nghiệm</h3>
    </div>
    <!-- Phần Đề thi trắc nghiêm -->
    <div class="exams" id="exams">

        <div class="box-container" id="listExamHomePage" name="listExamHomePage" runat="server">

        </div>

    </div>
</asp:Content>
