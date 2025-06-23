<%@ Page Title="" Language="C#" MasterPageFile="~/Teacher/TeacherMst.Master" AutoEventWireup="true" CodeBehind="StudentAttendance.aspx.cs" Inherits="StudentManagementSystem.Teacher.StudentAttendance" %>
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
      <h3 class="text-center">Student Attendance</h3>
       <div class="row mb-3 mr-lg-5 ml-lg-5 mt-md-5 ">
     <div class=" col-md-3">
         <div>
             <label for="ddlClass">Class</label>
         </div>
         <asp:DropDownList ID="ddlClass" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlClass_SelectedIndexChanged"></asp:DropDownList>
         <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
             ErrorMessage="Class is required" ControlToValidate="ddlClass" Display="Dynamic"
             ForeColor="Red" SetFocusOnError="True" InitialValue="Select Class">
         </asp:RequiredFieldValidator>
     </div>
     <div class="col-md-6">
         <label for="ddlSubject">Subject</label>
         <asp:DropDownList ID="ddlSubject" runat="server" CssClass="form-control"></asp:DropDownList>
         <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
             ErrorMessage="Subject is required" ControlToValidate="ddlSubject" Display="Dynamic"
             ForeColor="Red" SetFocusOnError="True" InitialValue="">
         </asp:RequiredFieldValidator>
     </div>
 </div>
       <div class="row mb-3 mr-lg-5 ml-lg-3 mt-md-3">
     <div class="col-md-1 col-md-offset-1 ">
         <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-primary btn-block" BackColor="#5558c" Text="Submit" Width="223px" OnClick="btnSubmit_Click"  />
     </div>
 </div>

    
          
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
              <asp:Button ID="btnMarkAttendance" runat="server" CssClass="btn btn-primary btn-block" BackColor="#5558c" Text="Mark Attendance"  Width="223px" OnClick="btnMarkAttendance_Click" />
          </div>
      </div>
      </div>
  
</asp:Content>
