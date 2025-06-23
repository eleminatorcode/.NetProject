<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="TeacherSubject.aspx.cs" Inherits="StudentManagementSystem.Admin.TeacherSubject" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <div class="container p-md-4 p-sm-4">
            <div>
                <asp:Label ID="lblmsg" runat="server" Text="Label"></asp:Label>
            </div>
            <h3 class="text-center">Add Teacher Subject</h3>

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
                    <asp:DropDownList ID="ddlSubject" runat="server" CssClass="form-control" ></asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                        ErrorMessage="Subject is required" ControlToValidate="ddlSubject" Display="Dynamic"
                        ForeColor="Red" SetFocusOnError="True" InitialValue="">
                    </asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="col-md-6">
                <label for="ddlTeacher">Teacher</label>
                <asp:DropDownList ID="ddlTeacher" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlClass_SelectedIndexChanged"></asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
                    ErrorMessage="Teacher is required" ControlToValidate="ddlTeacher" Display="Dynamic"
                    ForeColor="Red" SetFocusOnError="True" InitialValue="Select Teacher">
                </asp:RequiredFieldValidator>
            </div>
        </div>

        <div class="row mb-3 mr-lg-2 ml-lg-2 mt-md-3">
            <div class="col-md-1 col-md-offset-10 mb-3">
                <asp:Button ID="btnAdd" runat="server" CssClass="btn btn-primary btn-block" BackColor="#5558c" Text="Assign Subject" Width="223px" OnClick="btnAdd_Click" />
            </div>
        </div>
        <div class="row mb-3 mr-lg-5 ml-lg-5 mt-md-5">
            <div class="col-md-6">
                <asp:GridView ID="GridView1" runat="server" CssClass="table table-hover table-bordered" DataKeyNames="Id" EmptyDataText="No record to display!"
                    AutoGenerateColumns="False" OnPageIndexChanging="GridView1_PageIndexChanging" OnRowCancelingEdit="GridView1_RowCancelingEdit"
                    OnRowEditing="GridView1_RowEditing" OnRowUpdating="GridView1_RowUpdating" AllowPaging="True" PageSize="4" OnRowDeleting="GridView1_RowDeleting" OnRowDataBound="GridView1_RowDataBound">
                    <columns>
                        <asp:BoundField DataField="Sr.No" HeaderText="Sr.No" ReadOnly="True">
                            <footerstyle horizontalalign="Center" />
                             <headerstyle horizontalalign="Center" BackColor="#5C5EB9" forecolor="white"/>
                            <itemstyle horizontalalign="Center" />
                        </asp:BoundField>

                        <asp:TemplateField HeaderText="Class">
                            <edititemtemplate>
                                <asp:DropDownList ID="ddlClassGv" runat="server" DataSourceID="SqlDataSource1"
                                    DataTextField="ClassName" DataValueField="ClassId" SelectedValue='<%# Eval("ClassId") %>'
                                    AutoPostBack="true" OnSelectedIndexChanged="ddlClassGv_SelectedIndexChanged">
                                    <asp:ListItem>Select Class</asp:ListItem>
                                </asp:DropDownList>
                                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:schoolsysdbConnectionString %>" ProviderName="<%$ ConnectionStrings:schoolsysdbConnectionString.ProviderName %>" SelectCommand="select * from Classes"></asp:SqlDataSource>
                            </edititemtemplate>
                            <itemtemplate>
                                <asp:Label ID="Label2" runat="server" Text='<%# Eval("ClassName") %>'></asp:Label>
                            </itemtemplate>
                             <headerstyle horizontalalign="Center" BackColor="#5C5EB9" forecolor="white" />
                            <itemstyle horizontalalign="Center" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Subject">
                            <Edititemtemplate>
                                <asp:DropDownList ID="ddlSubjectGv" runat="server"  CssClass="form-control" > </asp:DropDownList>
                                 <headerstyle horizontalalign="Center" BackColor="#5C5EB9" forecolor="white"//>
                            </Edititemtemplate>
                            <Itemtemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# Eval("SubjectName") %>'></asp:Label>
                            </Itemtemplate>

                               <headerstyle horizontalalign="Center" BackColor="#5C5EB9" forecolor="white"/>
                            <itemstyle horizontalalign="Center" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Teacher">
                            <edititemtemplate>
                                <asp:DropDownList ID="ddlTeacherGv" runat="server" DataSourceID="SqlDataSource2"
                                    DataTextField="Name" DataValueField="TeacherID" SelectedValue='<%# Eval("TeacherId") %>'>
                                </asp:DropDownList>
                                <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:schoolsysdbConnectionString %>"
                                    ProviderName="<%$ ConnectionStrings:schoolsysdbConnectionString.ProviderName %>
                                    " SelectCommand="select teacherid,name from teacher"></asp:SqlDataSource>
                                 <headerstyle horizontalalign="Center" BackColor="#5C5EB9"forecolor="white" />
                            </edititemtemplate>
                            <itemtemplate>
                                <asp:Label ID="Label3" runat="server" Text='<%# Eval("TeacherName") %>'></asp:Label>
                            </itemtemplate>
                             <headerstyle horizontalalign="Center" BackColor="#5C5EB9" forecolor="white"/>
                            <itemstyle horizontalalign="Center" />
                        </asp:TemplateField>

                        <asp:CommandField CausesValidation="False" HeaderText="Operation" ShowEditButton="True" ShowDeleteButton="True">
                            <headerstyle horizontalalign="Center" BackColor="#5C5EB9" forecolor="white" />
                            <itemstyle horizontalalign="Center" />
                        </asp:CommandField>
                    </columns>
                </asp:GridView>

            </div>
        </div>
    </div>
    
</asp:Content>
