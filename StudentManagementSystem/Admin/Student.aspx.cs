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
    public partial class Student : System.Web.UI.Page
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
                GetStudent();
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

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                string name = txtName.Text.Trim();
                string dob = txtDOB.Text.Trim();
                string gender = ddlGender.SelectedValue;
                string mobile = txtMobile.Text.Trim();
                string rollNo = txtRoll.Text.Trim();
                string address = txtAddress.Text.Trim();
                string classn = ddlClass.SelectedValue;
                if (ddlGender.SelectedValue != "0")
                {
                    string roll = txtRoll.Text.Trim();
                    DataTable dt = fn.Fetch("Select * from Student where ClassID='"+classn+"' and RollNo = '" + rollNo + "'");
                    if (dt.Rows.Count == 0)
                    {
                        string query = "Insert into Student (Name,DOB,Gender,Mobile,RollNo,Address,ClassId) values (@Name,@Dob,@Gender,@Mobile,@RollNo,@Address,@Class)";
                        fn.Query(query, new MySqlParameter("@Name", name), new MySqlParameter("@Dob", dob), new MySqlParameter("@Gender", gender),
                            new MySqlParameter("@Mobile", mobile), new MySqlParameter("@RollNo", roll), new MySqlParameter("@Address", address), new MySqlParameter("@Class", classn));
                        lblmsg.Text = "Inserted Successfully!";
                        lblmsg.CssClass = "alert alert-success";
                        ddlGender.SelectedIndex = 0;
                        txtName.Text = string.Empty;
                        txtDOB.Text = string.Empty;
                        txtRoll.Text = string.Empty;
                        txtMobile.Text = string.Empty;
                        ddlClass.SelectedIndex = 0;
                        
                        GetStudent();

                    }
                    else
                    {
                        lblmsg.Text = "Entered Student  already exists for <b>'" + rollNo + "'</b> already exists!";
                        lblmsg.CssClass = "alert alert-danger";
                    }
                }
                else
                {
                    lblmsg.Text = "Gender is required";
                    lblmsg.CssClass = "alert alert-danger";
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }

        private void GetStudent()
        {
            DataTable dt = fn.Fetch("Select  ROW_NUMBER() OVER (ORDER BY StudentId) AS `Sr.No`,s.StudentId,s.Name,s.Dob,s.gender,s.Mobile,s.RollNo,s.Address,c.ClassId,c.ClassName from Student s inner join Classes c on c.classId = s.ClassId");
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            GetStudent();
        }

        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex = -1;
            GetStudent();
        }

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex;
            GetStudent();
        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                GridViewRow row = GridView1.Rows[e.RowIndex];
                int StudentId = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Values[0]);
                string name = (row.FindControl("TextBoxName") as TextBox).Text;
                string mobile = (row.FindControl("TextBoxMobile") as TextBox).Text;
                string rollNo = (row.FindControl("txtRoll") as TextBox).Text;
                string address = (row.FindControl("TextBoxAddress") as TextBox).Text;
                string classId = ((DropDownList)GridView1.Rows[e.RowIndex].Cells[4].FindControl("ddlClass")).SelectedValue;

                fn.Query("Update Student set Name = @Name,Mobile=@Mobile,rollNo=@RollNo,Address=@Address, ClassId=@ClassId Where StudentID = @StudentId",
                    new MySqlParameter("@Name", name),
                     new MySqlParameter("@Mobile", mobile),
                    new MySqlParameter("@RollNo", rollNo),
                    new MySqlParameter("@Address", address),
                    new MySqlParameter("@StudentId", StudentId),
                    new MySqlParameter("@ClassId",classId)
                   );

                lblmsg.Text = "Student Updated Successfully!";
                lblmsg.CssClass = "alert alert-success";

                GridView1.EditIndex = -1;
                GetStudent();
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
            if (e.Row.RowType == DataControlRowType.DataRow && GridView1.EditIndex== e.Row.RowIndex)
            {
               
                 
                    DropDownList ddlClass = (DropDownList)e.Row.FindControl("ddlClass");


                    // Fetch subjects based on selected class
                    string classId = ddlClass.SelectedValue;
                    DataTable dtSubjects = fn.Fetch("SELECT * FROM classes");
                ddlClass.DataSource = dtSubjects;
                ddlClass.DataTextField = "ClassName";
                ddlClass.DataValueField = "ClassId";
                ddlClass.DataBind();

                ddlClass.Items.Insert(0, new ListItem("Select Class", ""));

                    // Get existing subject id to pre-select
                string selectedClass = DataBinder.Eval(e.Row.DataItem, "ClassName").ToString();
                ddlClass.Items.FindByText(selectedClass).Selected=true;
                  
                    
                
            }
        }
    }
}