using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WhatINoted
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LoginDiv.Visible = false;
            SignUpDiv.Visible = false;
        }

        protected void Login_Click(object sender, EventArgs e)
        {
            LoginSignUp.Visible = false;
            LoginDiv.Visible = true;
        }

        protected void SignUp_Click(object sender, EventArgs e)
        {
            LoginSignUp.Visible = false;
            SignUpDiv.Visible = true;
        }

        protected void Redirect_Click(object sender, EventArgs e)
        {
            Response.Redirect("Main.aspx", true);
        }
    }
}