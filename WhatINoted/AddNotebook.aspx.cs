using System;
using System.IO;
using System.Collections.Generic;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using WhatINoted.Models;
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
        protected void ValidateIsbnField(object source, ServerValidateEventArgs args)
        {
            try
            {
                new IsbnModel(args.Value);
                args.IsValid = true;
            }
            catch (ArgumentException ex)
            {
                args.IsValid = false;
            }
        }

        [WebMethod, ScriptMethod]
        protected void SearchForBook(object sender, EventArgs e)
        {
            //
            // Determine what parameters we are using to search: ISBN or Title/Author/Publisher
            //
            string searchKey = ((WebControl)sender).ID == "btnISBNPostback" ? "ISBN" : "details";

            //
            // Search Google Books with the provided parameters
            //
            List<BookSearchResultsModel> searchResults;

            try
            {
                searchResults = searchKey == "ISBN" ?
                GoogleBooksConnectionManager.SearchVolumes("", "", "", IsbnEntry.Text) :
                GoogleBooksConnectionManager.SearchVolumes(TitleEntry.Text, AuthorEntry.Text, PublisherEntry.Text, null);
            }
            catch (ArgumentNullException ex)
            {
                return;
            }

            //
            // Convert the search results to HTML table rows
            //
            List<TableRow> resultRows = new List<TableRow>();
            int idnum = 0;
            foreach(BookSearchResultsModel volume in searchResults)
            {
                TableRow volumeRow = new TableRow();
                volumeRow.CssClass = "search_result";
                volumeRow.ID = searchKey == "ISBN" ? "ResultRowISBN" + idnum++ : "ResultRowDetails" + idnum++;

                volumeRow.Attributes["onclick"] = "rowClicked(this)";

                TableCell titleCell = new TableCell();
                HtmlGenericControl innerDiv = new HtmlGenericControl("div");
                innerDiv.ID = volumeRow.ID + "_Title";
                innerDiv.InnerHtml = volume.Title;
                titleCell.Controls.Add(innerDiv);
                volumeRow.Controls.Add(titleCell);

                TableCell authorCell = new TableCell();
                innerDiv = new HtmlGenericControl("div");
                innerDiv.ID = volumeRow.ID + "_Authors";
                innerDiv.InnerHtml = volume.Authors;
                authorCell.Controls.Add(innerDiv);
                volumeRow.Controls.Add(authorCell);

                TableCell publisherCell = new TableCell();
                innerDiv = new HtmlGenericControl("div");
                innerDiv.ID = volumeRow.ID + "_Publisher";
                innerDiv.InnerHtml = volume.Publisher;
                publisherCell.Controls.Add(innerDiv);
                volumeRow.Controls.Add(publisherCell);

                TableCell pubDateCell = new TableCell();
                innerDiv = new HtmlGenericControl("div");
                innerDiv.ID = volumeRow.ID + "_PublishDate";
                innerDiv.InnerHtml = volume.PublishDate;
                pubDateCell.Controls.Add(innerDiv);
                volumeRow.Controls.Add(pubDateCell);

                TableCell isbnCell = new TableCell();
                innerDiv = new HtmlGenericControl("div");
                innerDiv.ID = volumeRow.ID + "_ISBN";
                innerDiv.InnerHtml = volume.ISBN;
                isbnCell.Controls.Add(innerDiv);
                volumeRow.Controls.Add(isbnCell);

                TableCell coverUrlCell = new TableCell();
                innerDiv = new HtmlGenericControl("div");
                innerDiv.ID = volumeRow.ID + "_CoverUrl";
                innerDiv.InnerHtml = volume.CoverURL;
                coverUrlCell.Controls.Add(innerDiv);
                coverUrlCell.Style.Add("display", "none");
                volumeRow.Controls.Add(coverUrlCell);


                resultRows.Add(volumeRow);
            }

            //
            // Insert search results into the relevant table
            //
            WebControl resultsTable = searchKey == "ISBN" ?
                SearchGridISBN : SearchGridDetails;

            foreach (TableRow volume in resultRows)
                resultsTable.Controls.Add(volume);
        }

        [WebMethod, ScriptMethod]
        protected void CreateNotebook(object sender, EventArgs e)
        {
            try
            {
                Notebook notebook = GoogleFirestoreConnectionManager.CreateNotebook(
                    HandleLoginUserID.Value, TitleSelection.Value, AuthorsSelection.Value, IsbnSelection.Value,
                    PublisherSelection.Value, PublishDateSelection.Value, System.Web.HttpUtility.HtmlDecode(CoverUrlSelection.Value));

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
                //redirect
                Response.Redirect("Notes.aspx?notebookID=" + notebook.ID, true);
            }
            catch (Exception ex)
            {
                return;
                // TODO -- actually handle failure, notify user something has gone wrong
            }
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