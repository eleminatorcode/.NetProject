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
    public partial class AdminHome : System.Web.UI.Page
    {
        Commonfnx fn = new Commonfnx();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["admin"] == null)
            {
                Response.Redirect("../Login.aspx");
            }
            else{
                studentCount();
                teacherCount();
                subjectCount();
                classCount();
            }

        }

        void studentCount()
        {
            DataTable dt = fn.Fetch("Select count(*) from student");
            Session["student"] = dt.Rows[0][0];
        }
        void classCount()
        {
            DataTable dt = fn.Fetch("Select count(*) from classes");
            Session["class"] = dt.Rows[0][0];
        }
        void teacherCount()
        {
            DataTable dt = fn.Fetch("Select count(*) from teacher");
            Session["teacher"] = dt.Rows[0][0];
        }
        void subjectCount()
        {
            DataTable dt = fn.Fetch("Select count(*) from subject");
            Session["subject"] = dt.Rows[0][0];
        }
    }
}