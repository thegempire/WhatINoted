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
        private static string NoteID;
        private static string NotebookID;

        private List<Notebook> Notebooks;

        /// <summary>
        /// Set to true if the Note is being edited.
        /// </summary>
        private static bool IsEdit;

        protected void Page_Load(object sender, EventArgs e)
        {
            NoteID = Request.QueryString["noteID"];

            if (NoteID != null && NoteID != "")
            {
                IsEdit = true;

                PageTitle.InnerText = "Edit Note";
                ByImage1.Visible = false;
                ByImage2.Visible = false;
                HandleNoteButton.InnerText = "Edit Note";
            }
            else
            {
                IsEdit = false;
                NotebookID = Request.QueryString["notebookID"];
            }
        }

        /// <summary>
        /// Handles the note, either creating or updating depending on context.
        /// </summary>
        [WebMethod, ScriptMethod]
        public void HandleNote(object sender, EventArgs e)
        {
            NotebookID = NotebookList.SelectedValue;
            if (IsEdit)
            {
                GoogleFirestoreConnectionManager.UpdateNote(NoteID, NotebookID, NoteText.InnerText);
            }
            else
            {
                GoogleFirestoreConnectionManager.CreateNote(HandleLoginUserID.Value, NotebookID, NoteText.InnerText);
            }
            Response.Redirect("Notebook.aspx?notebookID=" + NotebookID);
        }

        public void UpdatePage(object sender, EventArgs e)
        {
            string userID = HandleLoginUserID.Value;
            Note note;

            if (IsEdit)
            {
                note = GoogleFirestoreConnectionManager.GetNote(NoteID);
                NoteText.InnerText = note.Text;
                NotebookID = note.NotebookID;
            }

            Notebooks = GoogleFirestoreConnectionManager.GetNotebooks(userID);
            foreach (Notebook notebook in Notebooks)
            {
                NotebookList.Items.Add(new ListItem(notebook.Title, notebook.ID));
            }
            NotebookList.DataBind();
            if (NotebookID != null)
            {
                NotebookList.SelectedValue = NotebookID;
            }
            AddNoteUpdatePanel.Update();
        }

        protected override void GenerateText(object o, EventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}