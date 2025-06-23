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
    
    public partial class ExpenseDetails : System.Web.UI.Page
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
                GetExpenseDetails();
            }
        }

        private void GetExpenseDetails()
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

    }
}