<%@ Page Title="" Language="C#" MasterPageFile="~/Teacher/TeacherMst.Master" AutoEventWireup="true" CodeBehind="TMarksDetails.aspx.cs" Inherits="StudentManagementSystem.Teacher.TMarksDetails" %>
<%@ Register Src="~/MarksDetailUserControl.ascx" TagPrefix="uc" TagName="MarkDetail" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   
    <uc:MarkDetail runat="server" ID="TMarkDetails"/>
</asp:Content>
