using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WhatINoted
{
    public partial class AddNote : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ByImageGroupContainer.Visible = false;
        }

        protected void ByImage_Click(object sender, EventArgs e)
        {
            ByImageGroupContainer.Visible = !ByImageGroupContainer.Visible;
        }
    }
}