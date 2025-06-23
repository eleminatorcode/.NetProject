using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using static StudentManagementSystem.Models.Commonfn;

namespace StudentManagementSystem.Admin
{
    public partial class TeacherSubject : System.Web.UI.Page
    {
        Commonfnx fn = new Commonfnx();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["admin"] == null)
            {
                Response.Redirect("../Login.aspx");
            }
            if (!IsPostBack) {
                GetClass();
                GetTeacher();
                GetTeacherSubject();
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
        private void GetTeacher()
        {
            DataTable dt = fn.Fetch("select * from Teacher");
            ddlTeacher.DataSource = dt;
            ddlTeacher.DataTextField = "Name";
            ddlTeacher.DataValueField = "TeacherId";
            ddlTeacher.DataBind();
            ddlTeacher.Items.Insert(0, "Select Teacher");
        }
        private void GetTeacherSubject()
        {
            string query = @"
        SELECT 
            ROW_NUMBER() OVER (ORDER BY ts.Id) AS `Sr.No`,
            ts.Id,
            c.ClassName,
            ts.ClassId,
            s.SubjectName,
            ts.SubjectId,
            t.Name AS TeacherName,
            ts.TeacherId
        FROM 
            TeacherSubject ts
        INNER JOIN Classes c ON ts.ClassId = c.ClassId
        INNER JOIN Subject s ON ts.SubjectId = s.SubjectId
        INNER JOIN Teacher t ON ts.TeacherId = t.TeacherId;
    ";

            DataTable dt = fn.Fetch(query);
            GridView1.DataSource = dt;
            GridView1.DataBind();
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
                string teacherId = ddlTeacher.SelectedValue;
                

                DataTable dt = fn.Fetch("SELECT * FROM teachersubject WHERE ClassId ='" + classId + "' and SubjectId='" + subjectId + "' or TeacherId = '"+teacherId+"'");

                if (dt.Rows.Count == 0)
                {
                    // Insert the new class
                    string query = "INSERT INTO teachersubject (ClassId,SubjectId,TeacherId) VALUES (@ClassId,@SubjectId,@TeacherId)";
                    fn.Query(query, new MySqlParameter("@ClassId", classId), new MySqlParameter("@SubjectId", subjectId),
                        new MySqlParameter("@TeacherId",teacherId));

                    lblmsg.Text = "Inserted Successfully!";
                    lblmsg.CssClass = "alert alert-success";
                    ddlClass.SelectedIndex = 0;
                    ddlSubject.SelectedIndex = 0;
                    ddlTeacher.SelectedIndex = 0;
                    GetTeacherSubject(); // Refresh grid
                }
                else
                {
                    lblmsg.Text = "Entered <b>Teacher</b>already Exist!";
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
            GetTeacherSubject();
        }

        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex = -1;
            GetTeacherSubject();
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                int TeacherId = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Values[0]);

                fn.Query("Delete from teachersubject where Id = ?",
                    new MySqlParameter("", TeacherId));
                lblmsg.Text = "Teacher Subject Deleted Successfully!";
                lblmsg.CssClass = "alert alert-success";
                GridView1.EditIndex = -1;
                GetTeacherSubject();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                GridViewRow row = GridView1.Rows[e.RowIndex];
                string classId = ((DropDownList)row.FindControl("ddlClassGv")).SelectedValue;
                string subId = ((DropDownList)row.FindControl("ddlSubjectGv")).SelectedValue;
                string teacherId = ((DropDownList)row.FindControl("ddlTeacherGv")).SelectedValue;
                int teacherSubjectID = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Values[0]);
                

                fn.Query("Update teacherSubject set ClassId=@ClassId,SubjectId=@SubId,TeacherId=@TeacherId Where Id = @TsId",
                     new MySqlParameter("@ClassId", classId),
                     new MySqlParameter("@SubID", subId),
                    new MySqlParameter("@TeacherId", teacherId),
                    new MySqlParameter("@TsId",teacherSubjectID)
                   );

                lblmsg.Text = "Fees Updated Successfully!";
                lblmsg.CssClass = "alert alert-success";

                GridView1.EditIndex = -1;
                GetTeacherSubject();
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

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex;
            GetTeacherSubject();
        }

        protected void ddlClassGv_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddlClassSelected =(DropDownList)sender;
            GridViewRow row = (GridViewRow)ddlClassSelected.NamingContainer;
            if (row != null) { 
            if((row.RowState & DataControlRowState.Edit )>0)
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
    }
}