using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Script.Services;
using System.Web.Services;
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

        protected void Page_Load(object sender, EventArgs e)
        {
            notebookID = Request.QueryString["notebookID"];

            if (notebookID == null || notebookID == "")
            {
                Response.Redirect("./Notebooks.aspx");
            }

            NotebookTitle.InnerHtml = GoogleFirestoreConnectionManager.GetNotebook(notebookID).Title;
        }

        /// <summary>
        /// Edits the note corresponding to the edit button that was clicked.
        /// </summary>
        [WebMethod, ScriptMethod]
        public void EditNote(object sender, EventArgs e)
        {
            Response.Redirect("NoteEditor.aspx?noteID=" + NoteID.Value, true);
        }

        /// <summary>
        /// Deletes the note corresponding to the delete button that was clicked.
        /// </summary>
        [WebMethod, ScriptMethod]
        public void DeleteNote(object sender, EventArgs e)
        {
            // TODO - Some sort of message displayed to the user that allows them to confirm deletion

            // This implementation is correct. It is commented until the above TODO is complete.
            //string noteID = NoteID.Value;
            //if (noteID != null && noteID != "")
            //{
            //    GoogleFirestoreConnectionManager.DeleteNote(noteID);
            //}
        }

        [WebMethod, ScriptMethod]
        public void AddNote(object sender, EventArgs e)
        {
            Response.Redirect("NoteEditor.aspx?notebookID=" + notebookID, true);
        }

        [WebMethod, ScriptMethod]
        public void UpdateNotes(object sender, EventArgs e)
        {
            GenerateNoteRows(GoogleFirestoreConnectionManager.GetNotebookNotes(notebookID));
        }

        protected void GenerateNoteRows(List<Note> notes)
        {
            foreach (Note note in notes)
            {
                TableRow noteRow = new TableRow();
                TableCell textCell = new TableCell();
                string display = note.Text.Replace("\n", "<br/>");
                textCell.Text = display;
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