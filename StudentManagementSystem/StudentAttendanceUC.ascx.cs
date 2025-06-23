using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static StudentManagementSystem.Models.Commonfn;

namespace StudentManagementSystem
{
    public partial class StudentAttendanceUC : System.Web.UI.UserControl
    {
        Commonfnx fn = new Commonfnx();

        protected void Page_Load(object sender, EventArgs e)
        {
           // if (Session["admin"] == null)
           // {
          //      Response.Redirect("../Login.aspx");
          //  }

            if (!IsPostBack)
            {
                GetClass();
                
            }
        }

        private void GetClass()
        {
            DataTable dt = fn.Fetch("select * from Classes");
            ddlClass.DataSource = dt;
            ddlClass.DataTextField = "ClassName";
            ddlClass.DataValueField = "ClassId";
            ddlClass.DataBind();
            ddlClass.Items.Insert(0, "Select Class");
        }
        protected void ddlClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            string classId = ddlClass.SelectedValue;

            if (!string.IsNullOrEmpty(classId))
            {
                DataTable dt = fn.Fetch("SELECT * FROM Subject WHERE ClassId = '" + classId + "'");

                ddlSubject.DataSource = dt;
                ddlSubject.DataTextField = "SubjectName";
                ddlSubject.DataValueField = "SubjectId";
                ddlSubject.DataBind();
                ddlSubject.Items.Insert(0, new ListItem("Select Subject", ""));
            }
        }

        protected void btnCheckAttendance_Click(object sender, EventArgs e)
        {
           
            DataTable dt;
           DateTime date = Convert.ToDateTime(txtMonth.Text);
            if (ddlSubject.SelectedValue == "Select Subject")
            {
                dt =fn.Fetch( @"
        SELECT 
    ROW_NUMBER() OVER (ORDER BY sa.classId) AS `Sr.No`,
    s.name,
    sa.status,sa.date from studentAttendance sa

    INNER JOIN student s ON s.RollNo = sa.RollNo
     where sa.Classid = '" + ddlClass.SelectedValue + "' and sa.rollno='"+txtRollNo.Text.Trim()+"' and EXTRACT(YEAR FROM sa.date) = '" + date.Year + "' AND EXTRACT(MONTH FROM sa.date) ='" + date.Month + "' and sa.status =1;"
               );
            }

            else {
                dt=fn.Fetch( @"
         SELECT 
    ROW_NUMBER() OVER (ORDER BY sa.classId) AS `Sr.No`,
    s.name,
    sa.status,sa.date from studentAttendance sa

    INNER JOIN student s ON s.RollNo = sa.RollNo
     where sa.Classid = '" + ddlClass.SelectedValue + "' and sa.rollno='" + txtRollNo.Text.Trim() + "' and sa.subjectid='"+ddlSubject.SelectedValue+"' and EXTRACT(YEAR FROM sa.date) = '" + date.Year + "' AND EXTRACT(MONTH FROM sa.date) ='" + date.Month + "' and sa.status =1;"
                );
            }
                
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }
    }
}