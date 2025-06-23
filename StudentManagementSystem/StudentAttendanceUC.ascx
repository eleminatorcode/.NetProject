<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="StudentAttendanceUC.ascx.cs" Inherits="StudentManagementSystem.StudentAttendanceUC" %>
     <div>
 <div class="container p-md-4 p-sm-4">
     <div>
         i<asp:Label ID="lblmsg" runat="server" Text="Label"></asp:Label>
     </div>
    
     <h3 class="text-center">Student Attendance Details</h3>
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
       
    </div>
</div>

           <div class="row mb-3 mr-lg-5 ml-lg-5 mt-md-5 ">
    <div class=" col-md-3">
        <div>
            <label for="txtRollNo">Roll Number</label>
        </div>
        <asp:TextBox ID="txtRollNo" runat="server" placeholder="Enter Student Roll No"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
            ErrorMessage="Roll Number is required" ControlToValidate="txtRollNo" Display="Dynamic"
            ForeColor="Red" SetFocusOnError="True" >
        </asp:RequiredFieldValidator>
    </div>
    <div class="col-md-6">
        <label for="txtMonth">Month</label>
        <asp:TextBox ID="txtMonth" runat="server" TextMode="Month"></asp:TextBox>
       <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
    ErrorMessage="Month is required" ControlToValidate="txtMonth" Display="Dynamic"
    ForeColor="Red" SetFocusOnError="True" >
</asp:RequiredFieldValidator>
    </div>
</div>
      <div class="row mb-3 mr-lg-5 ml-lg-3 mt-md-3">
    <div class="col-md-1 col-md-offset-1 ">
        <asp:Button ID="btnCheckAttendance" runat="server" CssClass="btn btn-primary btn-block" BackColor="#5558c" Text="Check Attendance" Width="223px" onclick="btnCheckAttendance_Click"  />
    </div>
</div>

   
         
     </div>
         <div class="row mb-3 mr-lg-5 ml-lg-5 mt-md-5" >
             <div class="col-md-11 ">
                 <asp:GridView ID="GridView1" runat="server" CssClass="table table-hover table-bordered"  EmptyDataText="No record to display!"
     AutoGenerateColumns="False">
     <Columns>
         <asp:BoundField DataField="Sr.No" HeaderText="Sr.No" >                     
             <HeaderStyle BackColor="#5C5EB9" />
             <ItemStyle HorizontalAlign="Center" />
         </asp:BoundField>
         <asp:BoundField DataField="Name" HeaderText="Name">   
             <HeaderStyle BackColor="#5C5EB9" />
             <itemstyle horizontalalign="Center" />
         </asp:BoundField>
         <asp:BoundField DataField="Status" HeaderText="Status">                       
             <HeaderStyle BackColor="#5C5EB9" />
             <itemstyle horizontalalign="Center" />
         </asp:BoundField>
         <asp:BoundField DataField="Date" HeaderText="Date" DataFormatString="{0:dd MMMM yyyy}">                      
             <HeaderStyle BackColor="#5C5EB9" />
             <itemstyle horizontalalign="Center" />
         </asp:BoundField>
     </Columns>
     <HeaderStyle BackColor="#5C5EB9" ForeColor="White" />
 </asp:GridView>

             </div>
         </div>
    
     </div>
  
