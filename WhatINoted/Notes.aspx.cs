using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using WhatINoted.ConnectionManagers;
using WhatINoted.Models;

namespace WhatINoted
{
    /// <summary>
    /// A view from which the user can view, edit and delete their notes.
    /// </summary>
    public partial class NotesView : AddNoteView
    {
        private List<Models.Note> Notes;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Edits the note at the specified index.
        /// </summary>
        /// <param name="index">Index of the note to edit.</param>
        private void EditNote(int index)
        {

        }

        /// <summary>
        /// Deletes the note at the specified index.
        /// </summary>
        /// <param name="index">Index of the note to delete.</param>
        private void DeleteNote(int index)
        {

        }

        [WebMethod, ScriptMethod]
        public void UpdateNotes(object sender, EventArgs e)
        {
            /*string userID = HandleLoginUserID.Value;

            Notebooks = GoogleFirestoreConnectionManager.GetNotebooks(userID);

            List<HtmlGenericControl> notebookDivs = GenerateNotebookDivs();
            foreach (HtmlGenericControl notebookDiv in notebookDivs)
            {
                MainNotebooks.Controls.Add(notebookDiv);
            }*/
            string notebookID = Request.QueryString["notebookID"];

            Notes = GoogleFirestoreConnectionManager.GetNotebookNotes(notebookID);
            GenerateNoteRows();
        }

        protected void GenerateNoteRows()
        {
            foreach (Note note in Notes)
            {
                /*HtmlGenericControl notebookDiv = new HtmlGenericControl("div");
                notebookDiv.Attributes["class"] = "mainNotebooksDiv notebookColor";
                notebookDiv.Attributes["onclick"] = "click_openNotebook(\"" + notebook.ID + "\")";

                HtmlGenericControl titleDiv = new HtmlGenericControl("div");
                titleDiv.Attributes["class"] = "mainNotebookInnerDiv mainNotebookTitleDiv";
                titleDiv.InnerHtml = notebook.Title;
                notebookDiv.Controls.Add(titleDiv);

                HtmlGenericControl imageDiv = new HtmlGenericControl("div");
                imageDiv.Attributes["class"] = "mainNotebookInnerDiv mainNotebookImageDiv";
                HtmlGenericControl image = new HtmlGenericControl("img");
                image.Attributes["src"] = notebook.CoverURL;
                image.Attributes["alt"] = notebook.Title + " Cover Art";
                image.Attributes["class"] = "mainNotebookImage";
                imageDiv.Controls.Add(image);
                notebookDiv.Controls.Add(imageDiv);

                HtmlGenericControl numNotesDiv = new HtmlGenericControl("div");
                numNotesDiv.Attributes["class"] = "mainNotebookInnerDiv mainNotebookNumNotesDiv";
                numNotesDiv.InnerHtml = GoogleFirestoreConnectionManager.GetNotebookNotes(notebook.ID).Count.ToString();
                notebookDiv.Controls.Add(numNotesDiv);
                notebookDivs.Add(notebookDiv);*/
                TableRow noteRow = new TableRow();
                TableCell textCell = new TableCell();
                textCell.Text = note.Text;
                TableCell editCell = new TableCell();
                editCell.HorizontalAlign = HorizontalAlign.Right;
                Button editButton = new Button();
                editButton.Text = "Edit";
                editCell.Controls.Add(editButton);
                noteRow.Controls.Add(textCell);
                noteRow.Controls.Add(editCell);
                NotesTable.Controls.Add(noteRow);
            }
        }
    }
}