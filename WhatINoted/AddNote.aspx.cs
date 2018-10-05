using System;
using System.Web.Script.Services;
using System.Web.Services;

namespace WhatINoted
{
    public partial class AddNote : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod, ScriptMethod]
        public static bool CreateNote()
        {
            return true;
        }
    }
}