<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="EmployeeAttendance.aspx.cs" Inherits="StudentManagementSystem.Admin.EmployeeAttendance" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div>
     <div class="container p-md-4 p-sm-4">
         <div>
             i<asp:Label ID="lblmsg" runat="server" Text="Label"></asp:Label>
         </div>
         <div class="ml-auto text-right">
             <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
             <asp:UpdatePanel ID="UpdatePanel1" runat="server"> 
                 <ContentTemplate>
                     <asp:Timer ID="Timer1" runat="server" OnTick="Timer1_Tick"></asp:Timer>
                     <asp:Label ID="lblTime" runat="server" Text="Label"></asp:Label>

                 </ContentTemplate>
             </asp:UpdatePanel>
         </div>
         <h3 class="text-center">Teacher Attendance</h3>

       
             
         </div>
             <div class="row mb-3 mr-lg-5 ml-lg-5 mt-md-5" >
                 <div class="col-md-11 ">
                     <asp:GridView ID="GridView1" runat="server" CssClass="table table-hover table-bordered"  
                         EmptyDataText="No Record To Display" CellPadding="2" Height="1px" >
                         <Columns>
                             <asp:TemplateField HeaderText="Class">
                                 <ItemTemplate>
                                     <div class="form-check form-check-inline">
                                         <asp:RadioButton ID="RadioButton1" runat="server" Text="Present" Checked="true" GroupName="attendance" CssClass="form-check-input"/>
                                     </div>
                                     <div class="form-check form-check-inline offset-3" >
                                         <asp:RadioButton ID="RadioButton2" runat="server" Text="Absent"  GroupName="attendance" CssClass="form-check-input" />
                                     </div>
                                 </ItemTemplate>
                                 <ItemStyle HorizontalAlign="Center"  Width="260px" Height="70px"/>
                             </asp:TemplateField>                          
                         </Columns>
                         <HeaderStyle BackColor="#5558C" ForeColor="White"/>
                     </asp:GridView>

                 </div>
             </div>
         <div class="row mb-3 mr-lg-2 ml-lg-2 mt-md-3">
             <div class="col-md-1 col-md-offset-2 mb-3">
                 <asp:Button ID="btnMarkAttendance" runat="server" CssClass="btn btn-primary btn-block" BackColor="#5558c" Text="Mark Attendance" OnClick="btnMarkAttendance_Click" Width="223px" />
             </div>
         </div>
         </div>
     
</asp:Content>
