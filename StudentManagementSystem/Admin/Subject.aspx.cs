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
    public partial class WebForm1 : System.Web.UI.Page
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
                GetSubject();
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
            string classId = ddlClass.SelectedItem.Value;


            try
            {


                // Check if class already exists
                string classVal = ddlClass.SelectedItem.Text;
                string subject = txtSubject.Text.Trim();

                DataTable dt = fn.Fetch("SELECT * FROM subject WHERE ClassId ='" + ddlClass.SelectedItem.Value + "' and SubjectName='"+txtSubject.Text.Trim()+"'");

                if (dt.Rows.Count == 0)
                {
                    // Insert the new class
                    string query = "INSERT INTO Subject (ClassId,SubjectName) VALUES (@ClassId,@Subject)";
                    fn.Query(query, new MySqlParameter("@ClassId", classId), new MySqlParameter("@Subject", subject));

                    lblmsg.Text = "Inserted Successfully!";
                    lblmsg.CssClass = "alert alert-success";
                    ddlClass.SelectedIndex = 0;
                    txtSubject.Text = string.Empty;
                    GetSubject(); // Refresh grid
                }
                else
                {
                    lblmsg.Text = "Entered Subject already exists for <b>'" + classVal + "'</b>!";
                    lblmsg.CssClass = "alert alert-danger";
                }
            }

            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }




        private void GetSubject()
        {
            DataTable dt = fn.Fetch("SELECT ROW_NUMBER() OVER (ORDER BY c.ClassId) AS `Sr.No`, s.subjectName ,s.ClassId,c.ClassName,s.SubjectId FROM Subject s INNER JOIN Classes c ON c.ClassId = s.ClassId;");
            GridView1.DataSource = dt;

            GridView1.DataBind();
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            GetSubject();

        }

        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex = -1;
            GetSubject();

        }



        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex;
            GetSubject();

        }



        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                GridViewRow row = GridView1.Rows[e.RowIndex];
                string classId = ((DropDownList)row.FindControl("DropDownList1")).SelectedValue;
                int subId = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Values[0]);
                string subjectName = (row.FindControl("TextBoxSub") as TextBox).Text;

                fn.Query("Update Subject set SubjectName = @SubjectName,ClassId=@ClassId Where SubjectId = @SubId",
                    new MySqlParameter("@SubjectName", subjectName),
                     new MySqlParameter("@ClassId", classId),
                    new MySqlParameter("@SubId", subId)
                   );

                lblmsg.Text = "Fees Updated Successfully!";
                lblmsg.CssClass = "alert alert-success";

                GridView1.EditIndex = -1;
                GetSubject();
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
