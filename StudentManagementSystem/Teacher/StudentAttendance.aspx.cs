using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static StudentManagementSystem.Models.Commonfn;

namespace StudentManagementSystem.Teacher
{
    public partial class StudentAttendance : System.Web.UI.Page
    {
        Commonfnx fn = new Commonfnx();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["staff"] == null)
            {
                Response.Redirect("../Login.aspx");
            }
            if (!IsPostBack) {
                GetClass();
                btnMarkAttendance.Visible = false;
            }
        }
        protected void Timer1_Tick(object sender, EventArgs e)
        {
            lblTime.Text = DateTime.Now.ToString();
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
        
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string query = @"
        SELECT 
            Name,
            RollNo,
            StudentId,
            Mobile
        FROM 
            Student 
        where ClassId='"+ddlClass.SelectedValue+"' ";

            DataTable dt = fn.Fetch(query);
            GridView1.DataSource = dt;
            GridView1.DataBind();
            if (dt.Rows.Count >0)
            {
                btnMarkAttendance.Visible = true;
            }
            else
            {
                btnMarkAttendance.Visible = false;
            }
        }

        protected void btnMarkAttendance_Click(object sender, EventArgs e)
        {
            bool isTrue = false;
            foreach (GridViewRow row in GridView1.Rows)
            {
                string rollNo = row.Cells[2].Text.Trim();
                string subject = ddlSubject.SelectedValue;
                string classId = ddlClass.SelectedValue;
                RadioButton rb1 = (row.Cells[0].FindControl("RadioButton1") as RadioButton);
                RadioButton rb2 = (row.Cells[0].FindControl("RadioButton1") as RadioButton);
                int status = 0;
                if (rb1.Checked)
                {
                    status = 1;
                }
                else if (rb2.Checked)
                {
                    status = 0;
                }
                fn.Query(
                  "INSERT INTO studentattendance (ClassID,SubjectId, Status,Date,RollNo) VALUES (@SubjectId, @ClassId,@stat, @dt,@RollNo)",
                  new MySqlParameter("@RollNo", rollNo),
                  new MySqlParameter("@stat", status),    // integer 0 or 1
                  new MySqlParameter("@dt", DateTime.Now.ToString("yyyy-MM-dd")),
                  new MySqlParameter("@SubjectId", subject),
                  new MySqlParameter("@ClassId", classId)
                ); 
                isTrue = true;
               


            }
            if (isTrue)
            {
                lblmsg.Text = "Inserted Successfully!";
                lblmsg.CssClass = "alert alert-success";
            }
            else
            {
                lblmsg.Text = "Something went wrong";
                    lblmsg.CssClass = "alert alert-warn";
            }
        }
    }
}