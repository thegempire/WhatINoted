using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web.Services;
using System.Web.Script.Services;

namespace WhatINoted
{
    public partial class AddNotebook : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod, ScriptMethod]
        protected void SearchForBook(object sender, EventArgs e)
        {
            string searchKey;
            if (((WebControl)sender).ID == "btnISBNPostback")
                searchKey = "ISBN";
            else
                searchKey = "details";
            //search for books

            //convert to elements
            TableRow result = new TableRow();
            result.CssClass = "search_result";

            TableCell resultTitle = new TableCell();
            HtmlGenericControl innerDiv = new HtmlGenericControl("div");
            innerDiv.InnerHtml = "Book Title, 9th Edition";
            resultTitle.Controls.Add(innerDiv);
            result.Controls.Add(resultTitle);

            TableCell resultAuthor = new TableCell();
            innerDiv = new HtmlGenericControl("div");
            innerDiv.InnerHtml = "Dr. Book Author";
            resultAuthor.Controls.Add(innerDiv);
            result.Controls.Add(resultAuthor);

            TableCell resultISBN = new TableCell();
            innerDiv = new HtmlGenericControl("div");
            innerDiv.InnerHtml = "1-2-34567-8-9";
            resultISBN.Controls.Add(innerDiv);
            result.Controls.Add(resultISBN);

            //insert into element
            WebControl key;
            if (searchKey == "details")
            {
                key = SearchGridDetails;
            }
            else
            {
                key = SearchGridISBN;
            }

            key.Controls.Add(result);
        }

        protected void CreateNotebook(object sender, EventArgs e)
        {
            //Validation

            //redirect
            Server.Transfer("Notes.aspx", true);
        }
    }
}