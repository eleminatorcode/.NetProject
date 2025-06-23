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
    public partial class MarkDetails : System.Web.UI.Page
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
                GetClass();
                GetMarksDetail();
            }
        }

        private void GetMarksDetail()
        {
            DataTable dt = fn.Fetch("SELECT ROW_NUMBER() OVER (ORDER BY e.examId) AS `Sr.No`,e.examid,e.subjectid,e.ClassId,c.ClassName,s.SubjectName,e.rollno,e.totalMarks,e.OutOfMarks FROM exam e INNER JOIN Classes c ON c.ClassId = e.ClassId inner join Subject s on s.subjectid=e.subjectid;");
            GridView1.DataSource = dt;
            GridView1.DataBind();
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

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                string classid=ddlClass.SelectedValue;
                string rollNo= txtRoll.Text.Trim();
                DataTable dt = fn.Fetch("SELECT ROW_NUMBER() OVER (ORDER BY e.examId) AS `Sr.No`,e.examid,e.subjectid,e.ClassId,c.ClassName,s.SubjectName,e.rollno,e.totalMarks,e.OutOfMarks FROM exam e INNER JOIN Classes c ON c.ClassId = e.ClassId inner join Subject s on s.subjectid=e.subjectid where e.classid='"+classid+"' and e.rollNo='"+rollNo+"';");
                GridView1.DataSource = dt;
                GridView1.DataBind();
               
                    }
            catch (Exception ex) {
                throw;
            }
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
        }
    }
}