using StudentManagementSystem.Models;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static StudentManagementSystem.Models.Commonfn;

namespace StudentManagementSystem.Admin
{
    public partial class AddClass : System.Web.UI.Page
    {

        Commonfnx fn = new Commonfnx();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["admin"]==null)
            {
                Response.Redirect("../Login.aspx");
            }
            if (!IsPostBack)
            {
                GetClass();
            }
        }

        private void GetClass()
        {
            DataTable dt = fn.Fetch("SELECT ROW_NUMBER() OVER (ORDER BY ClassId) AS `Sr.No`,ClassId, ClassName FROM Classes;");
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            string className = txtclass.Text.Trim();
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["SchoolCS"].ConnectionString;

            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                try
                {
                    con.Open();

                    // Check if class already exists
                    string checkQuery = "SELECT COUNT(*) FROM Classes WHERE ClassName = @ClassName";
                    using (MySqlCommand checkCmd = new MySqlCommand(checkQuery, con))
                    {
                        checkCmd.Parameters.AddWithValue("@ClassName", className);
                        int count = Convert.ToInt32(checkCmd.ExecuteScalar());

                        if (count == 0)
                        {
                            // Insert the new class
                            string insertQuery = "INSERT INTO Classes (ClassName) VALUES (@ClassName)";
                            using (MySqlCommand insertCmd = new MySqlCommand(insertQuery, con))
                            {
                                insertCmd.Parameters.AddWithValue("@ClassName", className);
                                insertCmd.ExecuteNonQuery();
                            }

                            lblmsg.Text = "Inserted Successfully!";
                            lblmsg.CssClass = "alert alert-success";
                            txtclass.Text = string.Empty;
                            GetClass(); // Refresh grid
                        }
                        else
                        {
                            lblmsg.Text = "Entered class already exists!";
                            lblmsg.CssClass = "alert alert-danger";
                        }
                    }
                }
                catch (Exception ex)
                {
                    Response.Write("<script>alert('" + ex.Message + "');</script>");
                }
            }
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            GetClass();

        }

        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex = -1;
            GetClass();
        }

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex;
           
        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                GridViewRow row = GridView1.Rows[e.RowIndex];
                int cId = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Values[0]);
                string ClassName = (row.FindControl("txtClassEdit") as TextBox).Text;
                fn.Query("Update Classes set ClassName = ? where ClassId= ? ",new MySqlParameter("@Classname",ClassName),new MySqlParameter ("@Cid",cId));
                lblmsg.Text = "Updated Successfully!";
                lblmsg.CssClass = "alert alert-success";
                GridView1.EditIndex -= 1;
                GetClass();
            }
            catch (Exception ex) {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }
    }
}