using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WhatINoted
{
    /// <summary>
    /// A view from which the user can log in.
    /// </summary>
    public partial class _Default : View
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

        /// <summary>
        /// Attempts to log the user in; returns true if successful.
        /// </summary>
        /// <returns><c>true</c>, if user logged in, <c>false</c> otherwise.</returns>
        private bool LogIn()
        {
            return false;
        }

        protected void Redirect_Click(object sender, EventArgs e)
        {
            Response.Redirect("Main.aspx", true);
        }
    }
}