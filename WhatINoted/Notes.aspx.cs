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
    public partial class NotesView : View
    {
        private string notebookID;

        private List<Models.Note> Notes;

        protected void Page_Load(object sender, EventArgs e)
        {
            notebookID = Request.QueryString["notebookID"];
            title.InnerText = GoogleFirestoreConnectionManager.GetNotebook(notebookID).Title;
        }

        /// <summary>
        /// Edits the note at the specified index.
        /// </summary>
        /// <param name="index">Index of the note to edit.</param>
        [WebMethod, ScriptMethod]
        public void EditNote(object sender, EventArgs e)
        {
            Response.Redirect("EditNote.aspx?noteID=" + ActiveNote.Value, true);
        }

        /// <summary>
        /// Deletes the note at the specified index.
        /// </summary>
        /// <param name="index">Index of the note to delete.</param>
        [WebMethod, ScriptMethod]
        public void DeleteNote(object sender, EventArgs e)
        {
            //GoogleFirestoreConnectionManager.DeleteNote();
        }

        [WebMethod, ScriptMethod]
        public void AddNote(object sender, EventArgs e)
        {
            Response.Redirect("AddNote.aspx?notebookID=" + notebookID, true);
        }

        [WebMethod, ScriptMethod]
        public void UpdateNotes(object sender, EventArgs e)
        {
            Notes = GoogleFirestoreConnectionManager.GetNotebookNotes(notebookID);
            GenerateNoteRows();
        }

        protected void GenerateNoteRows()
        {
            foreach (Note note in Notes)
            {
                TableRow noteRow = new TableRow();
                TableCell textCell = new TableCell();
                textCell.Text = note.Text;
                TableCell editCell = new TableCell();
                editCell.HorizontalAlign = HorizontalAlign.Right;
                Button editButton = new Button();
                editButton.Text = "Edit";
                editButton.OnClientClick = "EditNote_Click(\"" + note.ID + "\")";
                editCell.Controls.Add(editButton);
                noteRow.Controls.Add(textCell);
                noteRow.Controls.Add(editCell);
                NotesTable.Controls.Add(noteRow);
            }
        }
    }
}