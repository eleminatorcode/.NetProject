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
    public partial class Expense : System.Web.UI.Page
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
                GetExpense();
               
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
        private void GetExpense()
        {
            string query = @"
        SELECT 
            ROW_NUMBER() OVER (ORDER BY e.expensesId) AS `Sr.No`,
            e.expensesId,
            c.ClassName,
            e.ClassId,
            s.SubjectName,
            e.SubjectId,
            e.ChargeAmount
        FROM 
            Expenses e
        INNER JOIN Classes c ON e.ClassId = c.ClassId
        INNER JOIN Subject s ON e.SubjectId = s.SubjectId;
    ";

            DataTable dt = fn.Fetch(query);
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {


                // Check if class already exists
                string classId = ddlClass.SelectedValue;
                string subjectId = ddlSubject.SelectedValue;
                string chargeAmount = txtAmount.Text.Trim();
                

                DataTable dt = fn.Fetch("SELECT * FROM expenses WHERE ClassId ='" + classId + "' and SubjectId='" + subjectId + "' and ChargeAmount = '" + chargeAmount + "'");

                if (dt.Rows.Count == 0)
                {
                    // Insert the new class
                    string query = "INSERT INTO Expenses (ClassId,SubjectId,ChargeAmount) VALUES (@ClassId,@SubjectId,@ChargeAmount)";
                    fn.Query(query, new MySqlParameter("@ClassId", classId), new MySqlParameter("@SubjectId", subjectId),
                        new MySqlParameter("@ChargeAmount", chargeAmount));

                    lblmsg.Text = "Inserted Successfully!";
                    lblmsg.CssClass = "alert alert-success";
                    ddlClass.SelectedIndex = 0;
                    ddlSubject.SelectedIndex = 0;
                    
                    GetExpense(); // Refresh grid
                }
                else
                {
                    lblmsg.Text = "Entered <b>Expenses Alerady</b> Exist!";
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
            GetExpense();
        }

        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex = -1;
            GetExpense();
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                int ExpensesId = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Values[0]);

                fn.Query("Delete from expenses where ExpensesId = ?",
                    new MySqlParameter("", ExpensesId));
                lblmsg.Text = "Expense Deleted Successfully!";
                lblmsg.CssClass = "alert alert-success";
                GridView1.EditIndex = -1;
                GetExpense();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex;
            GetExpense();
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
        

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                GridViewRow row = GridView1.Rows[e.RowIndex];
                string classId = ((DropDownList)row.FindControl("ddlClassGv")).SelectedValue;
                string subId = ((DropDownList)row.FindControl("ddlSubjectGv")).SelectedValue;
                string chargeAmt = (row.FindControl("TextBox1") as TextBox).Text.Trim();
                int expensesId = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Values[0]);


                fn.Query("Update expenses set ClassId=@ClassId,SubjectId=@SubId,ChargeAmount=@ChargeAmt Where expensesId = @ExpensesId",
                     new MySqlParameter("@ClassId", classId),
                     new MySqlParameter("@SubID", subId),
                     new MySqlParameter("@ChargeAmt",chargeAmt),
                    new MySqlParameter("@ExpensesId", expensesId)
                   );

                lblmsg.Text = "Fees Updated Successfully!";
                lblmsg.CssClass = "alert alert-success";

                GridView1.EditIndex = -1;
                GetExpense();
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