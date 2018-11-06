using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web.Services;
using System.Web.Script.Services;
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
            List<BookSearchResultsModel> searchResults = searchKey == "ISBN" ?
                GoogleBooksConnectionManager.SearchVolumes("", "", "", IsbnEntry.Text) :
                GoogleBooksConnectionManager.SearchVolumes(TitleEntry.Text, AuthorEntry.Text, PublisherEntry.Text, null);

            //
            // Convert the search results to HTML table rows
            //
            List<TableRow> resultRows = new List<TableRow>();
            foreach(BookSearchResultsModel volume in searchResults)
            {
                TableRow volumeRow = new TableRow();
                volumeRow.CssClass = "search_result";

                TableCell titleCell = new TableCell();
                HtmlGenericControl innerDiv = new HtmlGenericControl("div");
                innerDiv.InnerHtml = volume.Title;
                titleCell.Controls.Add(innerDiv);
                volumeRow.Controls.Add(titleCell);

                TableCell authorCell = new TableCell();
                innerDiv = new HtmlGenericControl("div");
                innerDiv.InnerHtml = volume.Authors;
                authorCell.Controls.Add(innerDiv);
                volumeRow.Controls.Add(authorCell);

                TableCell publisherCell = new TableCell();
                innerDiv = new HtmlGenericControl("div");
                innerDiv.InnerHtml = volume.Publisher;
                publisherCell.Controls.Add(innerDiv);
                volumeRow.Controls.Add(publisherCell);

                TableCell pubDateCell = new TableCell();
                innerDiv = new HtmlGenericControl("div");
                innerDiv.InnerHtml = volume.PublishDate;
                pubDateCell.Controls.Add(innerDiv);
                volumeRow.Controls.Add(pubDateCell);

                TableCell isbnCell = new TableCell();
                innerDiv = new HtmlGenericControl("div");
                innerDiv.InnerHtml = volume.ISBN;
                isbnCell.Controls.Add(innerDiv);
                volumeRow.Controls.Add(isbnCell);

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

        protected override void GenerateText() {

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