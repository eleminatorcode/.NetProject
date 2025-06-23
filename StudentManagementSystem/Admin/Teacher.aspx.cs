using MySql.Data.MySqlClient;
using StudentManagementSystem.Models;
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
    public partial class Teacher : System.Web.UI.Page
    {
        Commonfnx fn = new Commonfnx();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["admin"] == null)
            {
                Response.Redirect("../Login.aspx");
            }
            if (!IsPostBack) {
                GetTeacher();
            }
        }

        private void GetTeacher()
        {
            DataTable dt = fn.Fetch("Select  ROW_NUMBER() OVER (ORDER BY TEACHERId) AS `Sr.No`,teacherId,Name,Dob,gender,Mobile,Email,Address,Password from Teacher");
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                string name = txtName.Text.Trim();
                string dob = txtDOB.Text.Trim();
                string gender = ddlGender.SelectedValue;
                string mobile = txtMobile.Text.Trim();
                string email = txtEmail.Text.Trim();
                string address = txtAddress.Text.Trim();
                string password= txtPassword.Text.Trim();
                if (ddlGender.SelectedValue != "0")
                {
                    string emil = txtEmail.Text.Trim();
                    DataTable dt = fn.Fetch("Select * from Teacher where Email = '"+email+"'");
                    if (dt.Rows.Count == 0){
                        string query = "Insert into teacher (Name,DOB,Gender,Mobile,Email,Address,Password) values (@Name,@Dob,@Gender,@Mobile,@Email,@Address,@Password)";
                        fn.Query(query,new MySqlParameter("@Name",name),new MySqlParameter("@Dob", dob),new MySqlParameter("@Gender",gender), 
                            new MySqlParameter("@Mobile",mobile), new MySqlParameter("@Email",emil),new MySqlParameter("@Address",address),new MySqlParameter("@Password",password));
                        lblmsg.Text = "Inserted Successfully!";
                        lblmsg.CssClass = "alert alert-success";
                        ddlGender.SelectedIndex = 0;
                        txtName.Text = string.Empty;
                        txtDOB.Text = string.Empty;
                        txtEmail.Text = string.Empty;
                        txtMobile.Text = string.Empty;
                        txtAddress.Text = string.Empty;
                        txtPassword.Text = string.Empty;
                        GetTeacher();

                    }
                    else 
                    {
                        lblmsg.Text = "Entered email already exists for <b>'" + email + "'</b> already exists!";
                        lblmsg.CssClass = "alert alert-danger";
                    }
                }
                else
                {
                    lblmsg.Text = "Gender is required";
                    lblmsg.CssClass = "alert alert-danger";
                }
            }
            catch (Exception ex) {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }

        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            GetTeacher();
        }

        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {

            GridView1.EditIndex = -1;
            GetTeacher();
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                int teacherId = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Values[0]);

                fn.Query("Delete from Teacher where TeacherId = ?",
                    new MySqlParameter("", teacherId));
                lblmsg.Text = "Teacher Deleted Successfully!";
                lblmsg.CssClass = "alert alert-success";
                GridView1.EditIndex = -1;
                GetTeacher();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex;
            GetTeacher();
        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                GridViewRow row = GridView1.Rows[e.RowIndex];
                int teacherId = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Values[0]);
                string name = (row.FindControl("TextBoxName") as TextBox).Text;
                string mobile = (row.FindControl("TextBoxMobile") as TextBox).Text;
                string password = (row.FindControl("TextBoxPassword") as TextBox).Text;
                string address = (row.FindControl("TextBoxAddress") as TextBox).Text;

                fn.Query("Update Teacher set Name = @Name,Mobile=@Mobile,Password=@Password,Address=@Address Where TeacherID = @TeacherID",
                    new MySqlParameter("@Name", name),
                     new MySqlParameter("@Mobile", mobile),
                    new MySqlParameter("@Password", password),
                    new MySqlParameter("@Address",address),
                    new MySqlParameter("@TeacherID",teacherId)
                   );

                lblmsg.Text = "Teacher Updated Successfully!";
                lblmsg.CssClass = "alert alert-success";

                GridView1.EditIndex = -1;
                GetTeacher();
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
    }
}