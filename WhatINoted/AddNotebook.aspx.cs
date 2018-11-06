using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web.Services;
using System.Web.Script.Services;
using WhatINoted.ConnectionManagers;
using System.IO;

namespace WhatINoted
{
    /// <summary>
    /// Allows the user to create a Notebook.
    /// </summary>
    public partial class NotebookCreationView : TextGenerationView
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
            Response.Redirect("Notes.aspx", true);
        }

        /// <summary>
        /// Creates the notebook based on the search result with a particular index..
        /// </summary>
        /// <param name="searchResultIndex">Search result index.</param>
        private void CreateNotebook(int searchResultIndex) {

        }

        [WebMethod, ScriptMethod]
        protected override void GenerateText(object o, EventArgs e) {
            string image64 = ImageInBase64.Value;
            byte[] byteBuffer = Convert.FromBase64String(image64);
            System.Drawing.Image image;
            using (MemoryStream mStream = new MemoryStream(byteBuffer))
            {
                image = System.Drawing.Image.FromStream(mStream);
            }
            string text = GoogleVisionConnectionManager.ExtractText(image);
            IsbnBox.Value = text;
        }

        /// <summary>
        /// Searches for a book with the specified ISBN.
        /// </summary>
        /// <returns>The book with the specified ISBN.</returns>
        private Models.BookSearchResultsModel SearchByIsbn(Models.IsbnModel isbn) {
            return new Models.BookSearchResultsModel("", "", "", "", "", "");
        }

        /// <summary>
        /// Searches for a book with the specified details.
        /// </summary>
        /// <returns>The books with the specified details.</returns>
        /// <param name="title">Title.</param>
        /// <param name="author">Author.</param>
        private List<Models.BookSearchResultsModel> SearchByDetails(string title, string author) {
            return new List<Models.BookSearchResultsModel>();
        }
    }
}