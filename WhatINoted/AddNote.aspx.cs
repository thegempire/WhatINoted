using System;
using System.Collections.Generic;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using WhatINoted.ConnectionManagers;
using WhatINoted.Models;

namespace WhatINoted
{
    public partial class CreateEditNoteView : TextGenerationView
    {
        private Models.Note Note;

        private List<Models.Notebook> Notebooks;

        /// <summary>
        /// Set to true if the text should be updated.
        /// </summary>
        private bool UpdateText;

        /// <summary>
        /// Set to true if the notebook should be updated.
        /// </summary>
        private bool UpdateNotebook;

        protected void Page_Load(object sender, EventArgs e)
        {
            UpdateText = false;
            UpdateNotebook = false;
        }

        [WebMethod, ScriptMethod]
        public static bool CreateNote(string userID, string notebookID, string noteText)
        {
            return GoogleFirestoreConnectionManager.CreateNote(userID, notebookID, noteText) != null;
        }

        protected override void GenerateText()
        {

        }

        /// <summary>
        /// Updates the note, including the text and the notebook.
        /// </summary>
        protected void UpdateNote()
        {
            if (UpdateText)
            {
                SetText("");
            }
            if (UpdateNotebook)
            {
                SetNotebook(0);
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

        public void UpdateNotes(object sender, EventArgs e)
        {
            string userID = HandleLoginUserID.Value;

            Notebooks = GoogleFirestoreConnectionManager.GetNotebooks(userID);
            List<HtmlGenericControl> notebookOptions = GenerateNotebookOptions();
            foreach (HtmlGenericControl notebookOption in notebookOptions)
            {
                Control list = FindControl("NotebookList");
                list.Controls.Add(notebookOption);
            }
        }

        private List<HtmlGenericControl> GenerateNotebookOptions()
        {
            List<HtmlGenericControl> notebookDivs = new List<HtmlGenericControl>();
            foreach (Notebook notebook in Notebooks)
            {
                HtmlGenericControl notebookDiv = new HtmlGenericControl("option");
                notebookDiv.Attributes["value"] = notebook.ID;
                notebookDiv.InnerText = notebook.Title;
            }
            return notebookDivs;
        }
    }
}