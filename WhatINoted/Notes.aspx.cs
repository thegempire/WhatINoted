using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

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
        }
    }
}