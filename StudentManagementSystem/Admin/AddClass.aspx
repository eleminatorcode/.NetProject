<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="AddClass.aspx.cs" Inherits="StudentManagementSystem.Admin.AddClass" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <div class="container p-md-4 p-sm-4">
            <div>
                <asp:Label ID="lblmsg" runat="server" Text="Label"></asp:Label>
            </div>
            <h3 class="text-center">New Class</h3>

            <div class="row mb-3 mr-lg-2 ml-lg-5 mt-md-5">
                <div class="col-md-6">
                    <label for="txtclass">Class Name</label>
                    <asp:TextBox ID="txtclass" runat="server" CssClass="form-control" placeholder="Enter Class Name" required></asp:TextBox>
                </div>
             </div>
                <div class="row mb-3 mr-lg-2 ml-lg-2 mt-md-3">
                    <div class="col-md-1 col-md-offset-10 mb-3">
                    <asp:Button ID="btnAdd" runat="server" CssClass="btn btn-primary btn-block" BackColor="#5558c" Text ="Add Class" OnClick="btnAdd_Click" Width="223px" />
                </div>
            </div>
                <div class="row mb-3 mr-lg-5 ml-lg-5 mt-md-5">
                    <div class="col-md-6">
                        <asp:GridView ID="GridView1" runat="server" CssClass="table table-hover table-bordered" DataKeyNames="ClassID" AutoGenerateColumns="False" 
                            EmptyDataText="No Record To Display" OnPageIndexChanging="GridView1_PageIndexChanging" 
                            OnRowCancelingEdit="GridView1_RowCancelingEdit" OnRowEditing="GridView1_RowEditing" OnRowUpdating="GridView1_RowUpdating" AllowPaging="true" PageSize="4">
                            <Columns>
                                <asp:BoundField DataField="Sr.No" HeaderText="Sr.No" ReadOnly="True">
                                <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="Class">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtClassEdit" runat="server" Text='<%# Eval("ClassName") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lableClassName" runat="server" Text='<%# Eval("ClassName") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                
                                <asp:CommandField CausesValidation="False" HeaderText="Operation" ShowEditButton="True" />
                                
                            </Columns>
                            <HeaderStyle BackColor="#5558C" ForeColor="White"/>
                        </asp:GridView>

                    </div>
                </div>
            </div>
        </div>
</asp:Content>
