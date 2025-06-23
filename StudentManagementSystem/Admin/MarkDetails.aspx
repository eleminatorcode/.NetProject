<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="MarkDetails.aspx.cs" Inherits="StudentManagementSystem.Admin.MarkDetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
      <div>
      <div class="container p-md-4 p-sm-4">
          <div>
              <asp:Label ID="lblmsg" runat="server" Text="Label"></asp:Label>
          </div>
          <h3 class="text-center">Marks Details</h3>

          <div class="row mb-3 mr-lg-5 ml-lg-5 mt-md-5 ">
              <div class=" col-md-3">
                  <div>
                  <label for="ddlClass">Class</label></div>
                  <asp:DropDownList ID="ddlClass" runat="server" CssClass="form-control"></asp:DropDownList>
                  <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                      ErrorMessage="Class is required" ControlToValidate="ddlClass" Display="Dynamic" 
                      ForeColor="Red" SetFocusOnError="True" InitialValue="Select Class"></asp:RequiredFieldValidator>
              </div>
              <div class="col-md-6">
                  <label for="txtRoll">Student Roll Number</label>
                  <asp:TextBox ID="txtRoll" runat="server" CssClass="form-control" placeholder="Enter Roll No"  required></asp:TextBox>
              </div>
          </div>
          <div class="row mb-3 mr-lg-2 ml-lg-2 mt-md-3">
              <div class="col-md-1 col-md-offset-10 mb-3">
                  <asp:Button ID="btnAdd" runat="server" CssClass="btn btn-primary btn-block" BackColor="#5558c" Text="Get Marks"  Width="223px" OnClick="btnAdd_Click"/>
              </div>
          </div>
          <div class="row mb-3 mr-lg-5 ml-lg-5 mt-md-5">
              <div class="col-md-6">
                  <asp:GridView ID="GridView1" runat="server" CssClass="table table-hover table-bordered"  EmptyDataText="No record to display!" 
                      AutoGenerateColumns="False" OnPageIndexChanging="GridView1_PageIndexChanging"
                       AllowPaging ="True" PageSize="8">
                      <Columns>
                          <asp:BoundField DataField="Sr.No" HeaderText="Sr.No" ReadOnly="True">
                          <FooterStyle HorizontalAlign="Center" />
                          <ItemStyle HorizontalAlign="Center" />
                          </asp:BoundField>
                          <asp:BoundField DataField="ExamId" HeaderText="ExamId" ReadOnly="True">
                              <FooterStyle HorizontalAlign="Center" />
                              <ItemStyle HorizontalAlign="Center" />
                          </asp:BoundField>
                          <asp:BoundField DataField="ClassName" HeaderText="Class" ReadOnly="True">
                          <HeaderStyle HorizontalAlign="Center" />
                          <ItemStyle HorizontalAlign="Center" />
                          </asp:BoundField>
                          <asp:BoundField DataField="SubjectName" HeaderText="Subject" ReadOnly="True">
                              <HeaderStyle HorizontalAlign="Center" />
                              <ItemStyle HorizontalAlign="Center" />
                          </asp:BoundField>
                          <asp:BoundField DataField="RollNo" HeaderText="Roll Number" ReadOnly="True">
                              <HeaderStyle HorizontalAlign="Center" />
                              <ItemStyle HorizontalAlign="Center" />
                          </asp:BoundField>
                          <asp:BoundField DataField="TotalMarks" HeaderText="Total Marks" ReadOnly="True">
                              <HeaderStyle HorizontalAlign="Center" />
                              <ItemStyle HorizontalAlign="Center" />
                          </asp:BoundField>
                          <asp:BoundField DataField="OutOfMarks" HeaderText="Out Of Marks" ReadOnly="True">
                              <HeaderStyle HorizontalAlign="Center" />
                              <ItemStyle HorizontalAlign="Center" />
                          </asp:BoundField>
                        
                      </Columns>
                      <HeaderStyle BackColor="Red" ForeColor="White" />
                  </asp:GridView>

              </div>
          </div>
      </div>
  </div>
</asp:Content>
