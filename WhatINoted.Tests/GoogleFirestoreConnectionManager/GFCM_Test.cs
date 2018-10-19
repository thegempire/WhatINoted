using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using WhatINoted.Tests;

namespace WhatINoted.Tests.GoogleFirestoreConnectionManager
{
    public abstract class GFCM_Test : Test
    {
        protected const string userID1 = "GoogleFirestoreConnectionManagerTests_UserID1";
        protected const string displayName1 = "GoogleFirestoreConnectionManagerTests_DisplayName1";
        protected const string email1 = "GoogleFirestoreConnectionManagerTests_Email1";
        public const string userID2 = "GoogleFirestoreConnectionManagerTests_UserID2";
        protected const string isbn1 = "9780553804577";
        protected const string notebook1Title = "The Google Story";
        protected const string notebook1Author = "David A. Vise; Mark Malseed";
        protected const string notebook1Publisher = "Delacorte Press";
        protected static DateTime notebook1PublishDate { get; } = new DateTime(2005, 11, 15);
        protected static Models.NotebookModel notebook1 = new Models.NotebookModel(notebook1Title, notebook1Author, new Models.IsbnModel(isbn1), notebook1Publisher, notebook1PublishDate, null, null);
        protected string notebookID1;
        protected string notebookID2;
        protected Models.NotebookModel notebook2 = new Models.NotebookModel(notebook1Title, notebook1Author, null, notebook1Publisher, notebook1PublishDate, null, null);
        protected string notebookID3;
        protected Models.NotebookModel notebook3;
        protected string notebookID4;
        protected string notebookID5;
        protected Models.NotebookModel notebook5 = new Models.NotebookModel(notebook1Title, notebook1Author, null, "", notebook1PublishDate, null, null);
        protected string notebookID6;
        protected string notebookID7;
        protected Models.NotebookModel notebook7 = new Models.NotebookModel(notebook1Title, notebook1Author, null, notebook1Publisher, DateTime.MinValue, null, null);
        protected const string noteText = "Test_CreateNote note text.";
        protected string noteID1;
        protected Models.NoteModel note1 = new Models.NoteModel(noteText, notebook1, DateTime.Now, DateTime.Now, null);
        protected string noteID2;
        protected Models.NoteModel note2 = new Models.NoteModel("", notebook1, DateTime.Now, DateTime.Now, null);
        protected string noteID3;
        protected const string noteTextUpdated = "Test_UpdateNote note text.";

        public abstract bool Run(StreamWriter sw);
    }
}
