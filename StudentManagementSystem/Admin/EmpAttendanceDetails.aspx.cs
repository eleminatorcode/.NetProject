using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static StudentManagementSystem.Models.Commonfn;

namespace StudentManagementSystem.Admin
{
    public partial class EmpAttendanceDetails : System.Web.UI.Page
    {
        Commonfnx fn = new Commonfnx();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["admin"] == null)
            {
                Response.Redirect("../Login.aspx");
            }
            if (!IsPostBack)
            {
                GetTeacher();
            }
        }
        private void GetTeacher()
        {
            DataTable dt = fn.Fetch("select * from Teacher");
            ddlTeacher.DataSource = dt;
            ddlTeacher.DataTextField = "Name";
            ddlTeacher.DataValueField = "TeacherId";
            ddlTeacher.DataBind();
        }


        protected void btnCheckAttendance_Click(object sender, EventArgs e)
        {

            DateTime date = Convert.ToDateTime(txtMonth.Text);
            

            string query = @"
        SELECT 
    ROW_NUMBER() OVER (ORDER BY ta.teacherId) AS `Sr.No`,
    t.name,
    ta.status,ta.date from teacherAttendance ta

    INNER JOIN Teacher t ON t.teacherId = ta.TeacherId
     where EXTRACT(YEAR FROM ta.date) = '"+date.Year+"' AND EXTRACT(MONTH FROM ta.date) ='"+date.Month+"' and ta.status =1 and ta.teacherid= '"+ddlTeacher.SelectedValue+"';";

            DataTable dt = fn.Fetch(query);
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }
        
    }
}