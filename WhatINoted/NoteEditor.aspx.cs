using System;
using System.Collections.Generic;
using System.IO;
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
                GoogleFirestoreConnectionManager.UpdateNote(NoteID, NotebookID, NoteText.Text);
            }
            else
            {
                GoogleFirestoreConnectionManager.CreateNote(HandleLoginUserID.Value, NotebookID, NoteText.Text);
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
                if (HandleLoginUserID.Value != note.UserID)
                {
                    Response.Redirect("./Notebooks.aspx");
                }
                NoteText.Text = note.Text;
                NotebookID = note.NotebookID;
                ExtractTextUpdatePanel.Update();
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
            DropdownUpdatePanel.Update();
        }

        /// <summary>
        /// Gets the base64 encoded image from the Hidden Field ImageInBase64 and sets the NoteText value to the text that exists in the image.
        /// </summary>
        /// <param name="o"></param>
        /// <param name="e"></param>
        [WebMethod, ScriptMethod]
        protected override void GenerateText(object o, EventArgs e)
        {
            string image64 = ImageInBase64.Value.Split(',')[1];
            byte[] byteBuffer = Convert.FromBase64String(image64);
            System.Drawing.Image image;
            using (MemoryStream mStream = new MemoryStream(byteBuffer))
            {
                image = System.Drawing.Image.FromStream(mStream);
            }
            string text = GoogleVisionConnectionManager.ExtractText(image);

            if (text.Length > 0)
                NoteText.Text = text;
        }
    }
}