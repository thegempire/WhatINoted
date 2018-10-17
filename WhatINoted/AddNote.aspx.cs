using System;
using System.Web.Script.Services;
using System.Web.Services;
using WhatINoted.ConnectionManagers;

namespace WhatINoted
{
    public partial class CreateEditNoteView : TextGenerationView
    {
        private Models.NoteModel Note;

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
            GoogleVisionConnectionManager.ExtractText(null);
        }

        [WebMethod, ScriptMethod]
        public static bool CreateNote()
        {
            return true;
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
    }
}