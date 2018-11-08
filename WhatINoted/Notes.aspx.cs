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
            string notebookID = Request.QueryString["notebookID"];

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
                editCell.Controls.Add(editButton);
                noteRow.Controls.Add(textCell);
                noteRow.Controls.Add(editCell);
                NotesTable.Controls.Add(noteRow);
            }
        }
    }
}