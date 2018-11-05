using System;
using System.Collections.Generic;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using WhatINoted.ConnectionManagers;
using WhatINoted.Models;

namespace WhatINoted
{
    /// <summary>
    /// Notebooks view. From here, the user can see all their notebooks and add another.
    /// </summary>
    public partial class NotebooksView : AddNoteView
    {
        private List<Models.Notebook> Notebooks;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod, ScriptMethod]
        public void UpdateNotebooks(object sender, EventArgs e)
        {
            string userID = HandleLoginUserID.Value;
            Notebooks = GoogleFirestoreConnectionManager.GetNotebooks(userID);

            List<HtmlGenericControl> notebookDivs = GenerateNotebookDivs();
            foreach (HtmlGenericControl notebookDiv in notebookDivs)
            {
                MainNotebooks.Controls.Add(notebookDiv);
            }
        }

        protected List<HtmlGenericControl> GenerateNotebookDivs()
        {
            List<HtmlGenericControl> notebookDivs = new List<HtmlGenericControl>();
            foreach (Notebook notebook in Notebooks)
            {
                HtmlGenericControl notebookDiv = new HtmlGenericControl("div");
                notebookDiv.Attributes["class"] = "mainNotebooksDiv notebookColor";
                notebookDiv.Attributes["onclick"] = "click_openNotebook()";

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
                notebookDivs.Add(notebookDiv);
            }
            return notebookDivs;
        }

        protected void CreateNotebook(object sender, EventArgs e)
        {
            Response.Redirect("AddNotebook.aspx", true);
        }

        protected void CreateNote(object sender, EventArgs e)
        {
            Response.Redirect("AddNote.aspx", true);
        }

        protected void OpenNotebook(object sender, EventArgs e)
        {
            Response.Redirect("Notes.aspx", true);
        }
    }
}