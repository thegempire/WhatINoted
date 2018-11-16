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
        private const string LIMITED_ACCESS_API_KEY = "AIzaSyB2C6rSX-3xrnUUiJVlcrkaoobzM4VoCzo";

        private const string UNLIMITED_ACCESS_API_KEY = "AIzaSyBBYx2A-6F1IMdWhFBEudrPZjPiWJU-Y60";

        protected void Page_Load(object sender, EventArgs e)
        {
            AuthKey.Value = (DebugMode.DEBUG) ? UNLIMITED_ACCESS_API_KEY : LIMITED_ACCESS_API_KEY;
        }
    }
}