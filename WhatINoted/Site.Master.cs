using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WhatINoted
{
    public partial class SiteMaster : MasterPage
    {
        public static readonly bool DEBUG = false; // MUST BE FALSE WHEN YOU DEPLOY

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod, ScriptMethod]
        public void UpdateAuthKey(object sender, EventArgs e)
        {
            AuthKey.Value = (DEBUG) ? "AIzaSyBBYx2A-6F1IMdWhFBEudrPZjPiWJU-Y60" : "AIzaSyB2C6rSX-3xrnUUiJVlcrkaoobzM4VoCzo";
        }
    }
}