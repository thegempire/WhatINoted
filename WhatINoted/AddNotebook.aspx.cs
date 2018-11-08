using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using WhatINoted.ConnectionManagers;

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
        private void CreateNotebook(int searchResultIndex)
        {

        }

        /// <summary>
        /// Gets the base64 encoded image from the Hidden Field ImageInBase64 and sets the IsbnBox value to an ISBN that exists in the image.
        /// </summary>
        /// <param name="o"></param>
        /// <param name="e"></param>
        [WebMethod, ScriptMethod]
        protected override void GenerateText(object o, EventArgs e)
        {
            string image64 = ImageInBase64.Value.Split(',')[1];
            byte[] byteBuffer = Convert.FromBase64String(image64);
            System.Drawing.Image image;
            using (MemoryStream mStream = new MemoryStream(byteBuffer))
            {
                image = System.Drawing.Image.FromStream(mStream);
            }
            string text = GoogleVisionConnectionManager.ExtractText(image);
            text = ParseIsbn(text);
            IsbnBox.Value = text;
        }

        /// <summary>
        /// Attemps to return a possible ISBN from text.
        /// </summary>
        /// <param name="text">The text that may contain an ISBN.</param>
        /// <returns>A potential ISBN (10 or 13 characters, only digits and X/x), or text if none found.</returns>
        private string ParseIsbn(string text)
        {
            string[] words = text.Split(' ');
            foreach (string s in words)
            {
                string word = s.Replace("-", "");
                bool valid = true;

                if (word.Length != 10 && word.Length != 13)
                    continue;

                foreach (char c in word)
                {
                    if (!char.IsDigit(c) && c != 'X' && c != 'x')
                    {
                        valid = false;
                        break;
                    }
                }

                if (valid)
                    return word;
            }
            return text;
        }

        /// <summary>
        /// Searches for a book with the specified ISBN.
        /// </summary>
        /// <returns>The book with the specified ISBN.</returns>
        private Models.BookSearchResultsModel SearchByIsbn(Models.IsbnModel isbn)
        {
            return new Models.BookSearchResultsModel("", "", "", "", "", "");
        }

        /// <summary>
        /// Searches for a book with the specified details.
        /// </summary>
        /// <returns>The books with the specified details.</returns>
        /// <param name="title">Title.</param>
        /// <param name="author">Author.</param>
        private List<Models.BookSearchResultsModel> SearchByDetails(string title, string author)
        {
            return new List<Models.BookSearchResultsModel>();
        }
    }
}