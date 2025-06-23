<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="EmpAttendanceDetails.aspx.cs" Inherits="StudentManagementSystem.Admin.EmpAttendanceDetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
    <div class="container p-md-4 p-sm-4">
        <div>
            <asp:Label ID="lblmsg" runat="server" Text="Label"></asp:Label>
        </div>
        <h3 class="text-center">Teacher Attendance Details</h3>

        <div class="row mb-3 mr-lg-5 ml-lg-5 mt-md-5 ">
            <div class=" col-md-3">
                <label for="ddlTeacher">Teacher</label>
                <asp:DropDownList ID="ddlTeacher" runat="server" CssClass="form-control"></asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
                    ErrorMessage="Teacher is required" ControlToValidate="ddlTeacher" Display="Dynamic"
                    ForeColor="Red" SetFocusOnError="True" InitialValue="Select Teacher">
                </asp:RequiredFieldValidator>
            </div>
            <div class="col-md-6">
                <label for="txtMonth">Month</label>
                <asp:TextBox ID="txtMonth" runat="server" CssClass="form-control" TextMode="Month" required></asp:TextBox>
                
            </div>
        </div>
        
    </div>

    <div class="row mb-3 mr-lg-5 ml-lg-3 mt-md-3">
        <div class="col-md-1 col-md-offset-1 ">
            <asp:Button ID="btnCheckAttendance" runat="server" CssClass="btn btn-primary btn-block" BackColor="#5558c" Text="Check Attendance" Width="223px" onClick="btnCheckAttendance_Click"/>
        </div>
    </div>
    <div class="row mb-3 mr-lg-5 ml-lg-5 mt-md-5">
        <div class="col-md-8">
            <asp:GridView ID="GridView1" runat="server" CssClass="table table-hover table-bordered"  EmptyDataText="No record to display!"
     AutoGenerateColumns="False">
     <Columns>
         <asp:BoundField DataField="Sr.No" HeaderText="Sr.No" >                     
             <HeaderStyle BackColor="#5C5EB9" />
             <ItemStyle HorizontalAlign="Center" />
         </asp:BoundField>
         <asp:BoundField DataField="name" HeaderText="Name">   
             <HeaderStyle BackColor="#5C5EB9" />
             <itemstyle horizontalalign="Center" />
         </asp:BoundField>
        <%-- %> <asp:BoundField DataField="status" HeaderText="Status">                       
             <HeaderStyle BackColor="#5C5EB9" />
             <itemstyle horizontalalign="Center" />
         </asp:BoundField>--%>
         <asp:TemplateField HeaderText="Status">
             <ItemTemplate>
                 <asp:label runat="server" ID="label1" Text='<%# Convert.ToBoolean(Eval("Status")) ? "Present" : "Absent" %>'></asp:label>
             </ItemTemplate>
              <HeaderStyle BackColor="#5C5EB9" />
         </asp:TemplateField>
         <asp:BoundField DataField="date" HeaderText="Date" DataFormatString="{0:dd MMMM yyyy}">                      
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
