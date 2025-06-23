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
    public partial class ClassFees : System.Web.UI.Page
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
                GetFees();
            }
        }

        private void GetClass()
        {
            DataTable dt = fn.Fetch("select * from Classes");
            ddlClass.DataSource = dt;
            ddlClass.DataTextField="ClassName";
            ddlClass.DataValueField="ClassId";
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
                string feeamount = txtFeeAmount.Text.Trim();

                DataTable dt = fn.Fetch("SELECT * FROM fees WHERE ClassId ='"+ddlClass.SelectedItem.Value+"'");
            
                        if (dt.Rows.Count == 0)
                        {
                            // Insert the new class
                            string query = "INSERT INTO fees (ClassId,FeesAmount) VALUES (@ClassId,@FeeAmount)";
                    fn.Query(query, new MySqlParameter("@ClassId", classId), new MySqlParameter("@FeeAmount", feeamount));

                            lblmsg.Text = "Inserted Successfully!";
                            lblmsg.CssClass = "alert alert-success";
                            ddlClass.SelectedIndex = 0;
                            txtFeeAmount.Text = string.Empty;
                            GetFees(); // Refresh grid
                        }
                        else
                        {
                            lblmsg.Text = "Entered Fees already exists for <b>'"+classVal+"'</b>!";
                            lblmsg.CssClass = "alert alert-danger";
                        }
                    }
                
                catch (Exception ex)
                {
                    Response.Write("<script>alert('" + ex.Message + "');</script>");
                }
            }
            



        private void GetFees()
        {
            DataTable dt = fn.Fetch("SELECT ROW_NUMBER() OVER (ORDER BY c.ClassId) AS `Sr.No`, f.FeesId ,f.ClassId,c.ClassName,f.FeesAmount FROM fees f INNER JOIN Classes c ON c.ClassId = f.ClassId;");
            GridView1.DataSource = dt;  
            GridView1.DataBind();   
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
         GridView1.PageIndex = e.NewPageIndex;
            GetFees();
        }

        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
          GridView1.EditIndex = -1;
            GetFees();
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try {
                int feesId = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Values[0]);

                fn.Query("Delete from fees where FeesId = ?",
                    new MySqlParameter("",feesId));
                lblmsg.Text = "Fees Deleted Successfully!";
                lblmsg.CssClass = "alert alert-success";
                GridView1.EditIndex = -1;
                GetFees();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
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
                int FeesId = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value);
                string FeesAmount = (row.FindControl("TextBoxAmount") as TextBox).Text;

                fn.Query("Update fees set FeesAmount = @FeesAmount Where FeesId = @FeesId",
                    new MySqlParameter("@FeesAmount", FeesAmount),
                    new MySqlParameter("@FeesId", FeesId));

                lblmsg.Text = "Fees Updated Successfully!";
                lblmsg.CssClass = "alert alert-success";

                GridView1.EditIndex = -1;
                GetFees(); // Refresh the GridView
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