using MySql.Data.MySqlClient;
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
    public partial class Marks : System.Web.UI.Page
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
                GetMarks();
            }
        }

        private void GetMarks()
        {
            string query = @"
        SELECT 
            ROW_NUMBER() OVER (ORDER BY e.examId) AS `Sr.No`,
            e.ExamId,
            c.ClassName,
            e.ClassId,
            s.SubjectName,
            e.SubjectId,
            e.RollNo,
            e.TotalMarks,
            e.OutOfMarks
        FROM 
            Exam e
        INNER JOIN Classes c ON e.ClassId = c.ClassId
        INNER JOIN Subject s ON e.SubjectId = s.SubjectId;
    ";

            DataTable dt = fn.Fetch(query);
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
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {


                // Check if class already exists
                string classId = ddlClass.SelectedValue;
                string subjectId = ddlSubject.SelectedValue;
                string studentMarks = txtStuMark.Text.Trim();
                string outOfMarks = txtOutOfMark.Text.Trim();
                string rollNo = txtRoll.Text.Trim();

                DataTable dtbl = fn.Fetch("SELECT studentId FROM student WHERE ClassId ='" + classId + "' and RollNo='" + rollNo + "' ");
                if (dtbl.Rows.Count > 0)
                {
                    DataTable dt = fn.Fetch("SELECT * FROM exam WHERE ClassId ='" + classId + "' and SubjectId='" + subjectId + "' and RollNo = '" + rollNo + "'");

                    if (dt.Rows.Count == 0)
                    {
                        // Insert the new class
                        string query = "INSERT INTO exam (ClassId,SubjectId,RollNo,TotalMarks,OutOfMarks) VALUES (@ClassId,@SubjectId,@RollNo,@TotalMarks,@OutOfMarks)";
                        fn.Query(query, new MySqlParameter("@ClassId", classId), new MySqlParameter("@SubjectId", subjectId),
                            new MySqlParameter("@RollNo", rollNo),
                            new MySqlParameter("@TotalMarks", studentMarks),
                            new MySqlParameter("@OutOfMarks", outOfMarks));

                        lblmsg.Text = "Inserted Successfully!";
                        lblmsg.CssClass = "alert alert-success";
                        ddlClass.SelectedIndex = 0;
                        ddlSubject.SelectedIndex = 0;
                        txtStuMark.Text = string.Empty;
                        txtRoll.Text = string.Empty;
                        txtOutOfMark.Text = string.Empty;

                        GetMarks(); // Refresh grid
                    }
                    else
                    {
                        lblmsg.Text = "Entered <b>Data Alerady</b> Exist!";
                        lblmsg.CssClass = "alert alert-danger";
                    }
                }
                else
                {
                    lblmsg.Text = "Entered <b>Roll Number "+rollNo+"</b>Does Not Exist!";
                    lblmsg.CssClass = "alert alert-danger";
                }
            }

            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            GetMarks();
        }

        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex = -1;
            GetMarks();
        }

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex;
            GetMarks();
        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                GridViewRow row = GridView1.Rows[e.RowIndex];
                string classId = ((DropDownList)row.FindControl("ddlClassGv")).SelectedValue;
                string subId = ((DropDownList)row.FindControl("ddlSubjectGv")).SelectedValue;
                string rollNo = (row.FindControl("txtRoll") as TextBox).Text.Trim();
                int examId = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Values[0]);
                string studentTotalMarks = (row.FindControl("txtStuObtianedGv") as TextBox).Text.Trim();
                string outOfMarks = (row.FindControl("txtOutcOfMarksGv") as TextBox).Text.Trim();


                fn.Query("Update exam set ClassId=@ClassId,SubjectId=@SubId,RollNo=@RollNo,TotalMarks=@StudentTotalMarks,OutOfMarks=@OutOfMarks Where examId = @ExamId",
                     new MySqlParameter("@ClassId", classId),
                     new MySqlParameter("@SubID", subId),
                     new MySqlParameter("@RollNo", rollNo),
                     new MySqlParameter("@ExamId", examId),
                     new MySqlParameter("@StudentTotalMarks", studentTotalMarks),
                     new MySqlParameter("@OutOfMarks", outOfMarks)
                   );

                lblmsg.Text = "Fees Updated Successfully!";
                lblmsg.CssClass = "alert alert-success";

                GridView1.EditIndex = -1;
                GetMarks();
            }
            catch (MySqlException ex)
            {
                lblmsg.Text = "Database error: " + ex.Message;
                lblmsg.CssClass = "alert alert-danger";
            }
            catch (Exception ex)
            {
                lblmsg.Text = "An error occurred: " + ex.Message;
                lblmsg.CssClass = "alert alert-danger";
            }
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if ((e.Row.RowState & DataControlRowState.Edit) > 0)
                {
                    DropDownList ddlSubject = (DropDownList)e.Row.FindControl("ddlSubjectGv");
                    DropDownList ddlClass = (DropDownList)e.Row.FindControl("ddlClassGv");


                    // Fetch subjects based on selected class
                    string classId = ddlClass.SelectedValue;
                    DataTable dtSubjects = fn.Fetch("SELECT * FROM subject WHERE ClassId='" + classId + "'");
                    ddlSubject.DataSource = dtSubjects;
                    ddlSubject.DataTextField = "SubjectName";
                    ddlSubject.DataValueField = "SubjectId";
                    ddlSubject.DataBind();

                    ddlSubject.Items.Insert(0, new ListItem("Select Subject", ""));

                    // Get existing subject id to pre-select
                    string selectedSubject = DataBinder.Eval(e.Row.DataItem, "SubjectName").ToString();
                    ddlSubject.Items.FindByText(selectedSubject).Selected = true;



                }
            }
        }

        protected void ddlClassGv_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddlClassSelected = (DropDownList)sender;
            GridViewRow row = (GridViewRow)ddlClassSelected.NamingContainer;
            if (row != null)
            {
                if ((row.RowState & DataControlRowState.Edit) > 0)
                {
                    DropDownList ddlSubjectGv = (DropDownList)row.FindControl("ddlSubjectGv");
                    DataTable dt = fn.Fetch("Select * from subject where ClassId='" + ddlClassSelected.SelectedValue + "'");
                    ddlSubjectGv.DataSource = dt;
                    ddlSubjectGv.DataTextField = "SubjectName";
                    ddlSubjectGv.DataValueField = "SubjectId";
                    ddlSubjectGv.DataBind();
                }
            }
        }

        
    }
}