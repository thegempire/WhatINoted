using System;
using System.Collections.Generic;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI.WebControls;
using WhatINoted.ConnectionManagers;
using WhatINoted.Models;

namespace WhatINoted
{
    public partial class CreateEditNoteView : TextGenerationView
    {
        private static string notebookID;
        private static string noteID;

        private Models.Note Note;

        private List<Models.Notebook> Notebooks;

        /// <summary>
        /// Set to true if the Note is being updated.
        /// </summary>
        private static bool UpdateNote;

        /// <summary>
        /// Set to true if the notebook should be updated.
        /// </summary>
        private bool UpdateNotebook;

        protected void Page_Load(object sender, EventArgs e)
        {
            notebookID = Request.QueryString["notebookID"];
            noteID = Request.QueryString["noteID"];

            if (notebookID != null && notebookID != "")
            {
                // create note
                UpdateNote = false;
            }
            else if (noteID != null && noteID != "")
            {
                // update note
                UpdateNote = true;

                PageTitle.InnerText = "Edit Note";
                ByImage1.Visible = false;
                ByImage2.Visible = false;
                HandleNoteButton.InnerText = "Edit Note";
            }

            UpdateNotebook = false;
        }

        [WebMethod, ScriptMethod]
        public static bool CreateNote(string userID, string notebookID, string noteText)
        {
            GoogleFirestoreConnectionManager.CreateNote(userID, notebookID, noteText);
            return true;
        }

        protected override void GenerateText(object o, EventArgs e)
        {

        }

        /// <summary>
        /// Edits the note, including the text and the notebook.
        /// </summary>
        [WebMethod, ScriptMethod]
        protected static void EditNote(string notebookID, string noteText)
        {
            GoogleFirestoreConnectionManager.UpdateNote(noteID, notebookID, noteText);
        }

        /// <summary>
        /// Handles the note, either creating or updating depending on context.
        /// </summary>
        [WebMethod, ScriptMethod]
        public static void HandleNote(string userID, string noteText)
        {
            if (UpdateNote)
            {
                GoogleFirestoreConnectionManager.UpdateNote(noteID, notebookID, noteText);
            }
            else
            {
                GoogleFirestoreConnectionManager.CreateNote(userID, notebookID, noteText);
            }
        }

        /// <summary>
        /// Sets the text of the note.
        /// </summary>
        /// <param name="text">Text.</param>
        private void SetText(string text)
        {

        }

        /// <summary>
        /// Sets the notebook to which the note belongs.
        /// </summary>
        /// <param name="index">Index of the notebook.</param>
        private void SetNotebook(int index)
        {

        }

        public void UpdatePage(object sender, EventArgs e)
        {
            string userID = HandleLoginUserID.Value;
            Note note;

            if (UpdateNote)
            {
                note = GoogleFirestoreConnectionManager.GetNote(noteID);
                NoteText.InnerText = note.Text;
                notebookID = note.NotebookID;
            }

            Notebooks = GoogleFirestoreConnectionManager.GetNotebooks(userID);
            foreach (Notebook notebook in Notebooks)
            {
                NotebookList.Items.Add(new ListItem(notebook.Title, notebook.ID));
            }

            if (notebookID != null)
            {
                NotebookList.SelectedValue = notebookID;
            }
        }

        //public void UpdateEditPage(object sender, EventArgs e)
        //{
        //    string userID = HandleLoginUserID.Value;
        //    Note note = GoogleFirestoreConnectionManager.GetNote(noteID);

        //    NoteText.InnerText = note.Text;

        //    Notebooks = GoogleFirestoreConnectionManager.GetNotebooks(userID);
        //    foreach (Notebook notebook in Notebooks)
        //    {
        //        NotebookList.Items.Add(new ListItem(notebook.Title, notebook.ID));
        //    }

        //    notebookID = note.NotebookID;
        //    if (notebookID != null)
        //    {
        //        NotebookList.SelectedValue = notebookID;
        //    }
        //}
    }
}