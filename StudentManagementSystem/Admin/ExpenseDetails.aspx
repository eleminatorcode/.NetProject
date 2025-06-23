<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="ExpenseDetails.aspx.cs" Inherits="StudentManagementSystem.Admin.ExpenseDetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
      <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript" src="https://cdn.datatables.net/1.10.20/js/jquery.dataTables.min.js"></script>
    <link href="https://cdn.datatables.net/1.10.20/css/jquery.dataTables.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
        $(document).ready(function (){
            $('#<%=GridView1.ClientID%>').prepend($("<thead></thead>").append($(this).find("tr:first"))).DataTable({ "paging": true, "ordering": true, "searching": true });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     
     <div class="container p-md-4 p-sm-4">
         <h3 class="text-center">Expense Details</h3>
     <div class="row mb-3 mr-lg-5 ml-lg-5 mt-md-5">
         <div class="col-md-12">
             <asp:GridView ID="GridView1" runat="server" CssClass="table table-hover table-bordered"  EmptyDataText="No record to display!"
                 AutoGenerateColumns="False">
                 <Columns>
                     <asp:BoundField DataField="Sr.No" HeaderText="Sr.No" >                     
                         <HeaderStyle BackColor="#5C5EB9" />
                         <ItemStyle HorizontalAlign="Center" />
                     </asp:BoundField>
                     <asp:BoundField DataField="ClassName" HeaderText="Class">   
                         <HeaderStyle BackColor="#5C5EB9" />
                         <itemstyle horizontalalign="Center" />
                     </asp:BoundField>
                     <asp:BoundField DataField="SubjectName" HeaderText="Subject">                       
                         <HeaderStyle BackColor="#5C5EB9" />
                         <itemstyle horizontalalign="Center" />
                     </asp:BoundField>
                     <asp:BoundField DataField="ChargeAmount" HeaderText="Charge Amount(Per Lecture)">                      
                         <HeaderStyle BackColor="#5C5EB9" />
                         <itemstyle horizontalalign="Center" />
                     </asp:BoundField>
                 </Columns>
                 <HeaderStyle BackColor="#5C5EB9" ForeColor="White" />
             </asp:GridView>

         </div>
     </div>
 </div>
</asp:Content>
