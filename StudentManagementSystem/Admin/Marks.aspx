<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="Marks.aspx.cs" Inherits="StudentManagementSystem.Admin.Marks" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
      <div>
      <div class="container p-md-4 p-sm-4">
          <div>
              <asp:Label ID="lblmsg" runat="server" Text="Label"></asp:Label>
          </div>
          <h3 class="text-center">Add Marks</h3>

          <div class="row mb-3 mr-lg-2 ml-lg-5 mt-md-5">
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
          <div class="col-md-6">
              <label for="txtRoll">Student Roll number</label>
              <asp:TextBox ID="txtRoll" runat="server" Placeholder="Enter Student Roll No"  CssClass="form-control" required></asp:TextBox>
            
          </div>
          <div class="col-md-6">
              <label for="txtStuMark"> Marks Obtained (Student Marks)</label>
              <asp:TextBox ID="txtStuMark" runat="server" Placeholder="Enter Marks Obtained" TextMode="Number" CssClass="form-control" required></asp:TextBox>
             
          </div>
          <div class="col-md-6">
    <label for="txtOutOfMark">Out Of Marks</label>
    <asp:TextBox ID="txtOutOfMark" runat="server" Placeholder="Enter Out Of Marks" TextMode="Number" CssClass="form-control" required></asp:TextBox>
    
</div>
      </div>

          <div class="row mb-3 mr-lg-1 ml-lg-5 mt-md-1">
              <div class="col-md-1 col-md-offset-10 mb-3">
              <asp:Button ID="btnAdd" runat="server" CssClass="btn btn-primary btn-block" BackColor="#5558c" Text="Add Marks" Width="215px" OnClick="btnAdd_Click" />
          </div>
      </div>
      <div class="row mb-3 mr-lg-5 ml-lg-5 mt-md-5">
          <div class="col-md-11">
              <asp:GridView ID="GridView1" runat="server" EnableTheming="true" CssClass="table table-hover table-bordered" DataKeyNames="ExamId" EmptyDataText="No record to display!"
                  AutoGenerateColumns="False" OnPageIndexChanging="GridView1_PageIndexChanging" OnRowCancelingEdit="GridView1_RowCancelingEdit"
                  OnRowEditing="GridView1_RowEditing" OnRowUpdating="GridView1_RowUpdating" AllowPaging="True" PageSize="4" OnRowDataBound="GridView1_RowDataBound">
                  <Columns>
                      <asp:BoundField DataField="Sr.No" HeaderText="Sr.No" ReadOnly="True">
                          <FooterStyle HorizontalAlign="Center" />
                          <ItemStyle HorizontalAlign="Center" />
                      </asp:BoundField>

                      <asp:TemplateField HeaderText="Class">
                          <EditItemTemplate>
                              <asp:DropDownList ID="ddlClassGv" runat="server" DataSourceID="SqlDataSource1"
                                  DataTextField="ClassName" DataValueField="ClassId" SelectedValue='<%# Eval("ClassId") %>'
                                  AutoPostBack="true" OnSelectedIndexChanged="ddlClassGv_SelectedIndexChanged">
                                  <asp:ListItem>Select Class</asp:ListItem>
                              </asp:DropDownList>
                              <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:schoolsysdbConnectionString %>" ProviderName="<%$ ConnectionStrings:schoolsysdbConnectionString.ProviderName %>" SelectCommand="select * from Classes"></asp:SqlDataSource>
                          </EditItemTemplate>
                          <ItemTemplate>
                              <asp:Label ID="Label2" runat="server" Text='<%# Eval("ClassName") %>'></asp:Label>
                          </ItemTemplate>
                          <ItemStyle HorizontalAlign="Center" />
                      </asp:TemplateField>

                      <asp:TemplateField HeaderText="Subject">
                          <EditItemTemplate>
                              <asp:DropDownList ID="ddlSubjectGv" runat="server" CssClass="form-control"></asp:DropDownList>
                          </EditItemTemplate>
                          <ItemTemplate>
                              <asp:Label ID="Label1" runat="server" Text='<%# Eval("SubjectName") %>'></asp:Label>
                          </ItemTemplate>

                          <HeaderStyle HorizontalAlign="Center" />
                          <ItemStyle HorizontalAlign="Center" />
                      </asp:TemplateField>

                      <asp:TemplateField HeaderText="Roll No">
                          <EditItemTemplate>
                              <asp:TextBox ID="txtRoll" runat="server"  CssClass="form-control" Text='<%# Eval("RollNo") %>'></asp:TextBox>
                          </EditItemTemplate>
                          <ItemTemplate>
                              <asp:Label ID="Label3" runat="server" Text='<%# Eval("RollNo") %>'></asp:Label>
                          </ItemTemplate>
                          <ItemStyle HorizontalAlign="Center" />
                      </asp:TemplateField>

                      <asp:TemplateField HeaderText="Marks Obtained">
                          <EditItemTemplate>
                              <asp:TextBox ID="txtStuObtianedGv" runat="server" TextMode="Number" CssClass="form-control" Text='<%# Eval("TotalMarks") %>'></asp:TextBox>
                          </EditItemTemplate>
                          <ItemTemplate>
                              <asp:Label ID="Label3" runat="server" Text='<%# Eval("TotalMarks") %>'></asp:Label>
                          </ItemTemplate>
                          <ItemStyle HorizontalAlign="Center" />
                      </asp:TemplateField>

                      <asp:TemplateField HeaderText="Out Of Marks">
                          <EditItemTemplate>
                              <asp:TextBox ID="txtOutcOfMarksGv" runat="server" TextMode="Number" CssClass="form-control" Text='<%# Eval("OutOfMarks") %>'></asp:TextBox>
                          </EditItemTemplate>
                          <ItemTemplate>
                              <asp:Label ID="Label3" runat="server" Text='<%# Eval("OutOfMarks") %>'></asp:Label>
                          </ItemTemplate>
                          <ItemStyle HorizontalAlign="Center" />
                      </asp:TemplateField>

                      <asp:CommandField CausesValidation="False" HeaderText="Operation" ShowEditButton="True" >
                          <HeaderStyle HorizontalAlign="Center" />
                          <ItemStyle HorizontalAlign="Center" />
                      </asp:CommandField>
                  </Columns>
                  <HeaderStyle BackColor="#5C5EB9" ForeColor="White" />
              </asp:GridView>

          </div>
      </div>
  </div>
</asp:Content>
