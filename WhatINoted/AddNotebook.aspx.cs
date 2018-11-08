﻿using System;
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
                Response.Redirect("Notes.aspx?notebookID=" + notebook.ID, true);
            }
            catch (Exception ex)
            {
                return;
                // TODO -- actually handle failure, notify user something has gone wrong
            }
        }

        protected override void GenerateText()
        {

        }
    }
}