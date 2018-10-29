using System;
using System.IO;
using WhatINoted.ConnectionManagers;
using WhatINoted.Models;

namespace WhatINoted.Tests.GoogleFirestoreConnectionManagerTests
{
    public class GFCM_CreateNoteTest : GFCM_Test
    {
        public override bool Run(StreamWriter sw)
        {
            bool passed = true;
            passed = NormalCreateNoteRequest(sw) && passed;
            passed = CreateNoteUserDoesNotExist(sw) && passed;
            passed = CreateNoteUserIdIsNull(sw) && passed;
            passed = CreateNoteUserIdIsEmpty(sw) && passed;
            passed = CreateNoteNotebookDoesNotExist(sw) && passed;
            passed = CreateNoteNotebookIdIsNull(sw) && passed;
            passed = CreateNoteNotebookIdIsEmpty(sw) && passed;
            passed = CreateNoteNoteTextIsNull(sw) && passed;
            passed = CreateNoteNoteTextIsEmpty(sw) && passed;
            return passed;
        }

        private bool NormalCreateNoteRequest(StreamWriter sw)
        {
            try
            {
                GoogleFirestoreConnectionManager.HandleLogin(userID1, displayName1, email1);
                Notebook createdNotebook = GoogleFirestoreConnectionManager.CreateNotebook(userID1, title1, author1, isbn1, publisher1, publishDate1, coverURL1);
                Note actual = GoogleFirestoreConnectionManager.CreateNote(userID1, createdNotebook.ID, text1);
                Note expected = new Note(actual.ID, userID1, createdNotebook.ID, text1, DateTime.Now, DateTime.Now);
                if (!expected.Equals(actual))
                {
                    sw.WriteLine("FAILED: CreateNote(string userID, string notebookID, string noteText): CreateNote normal test case.");
                    GoogleFirestoreConnectionManager.DeleteUser(userID1);
                    return false;
                }
                else
                {
                    GoogleFirestoreConnectionManager.DeleteUser(userID1);
                }
            }
            catch
            {
                sw.WriteLine("FAILED: CreateNote(string userID, string notebookID, string noteText): CreateNote normal test case - unexpected exception.");
                return false;
            }
            return true;
        }

        private bool CreateNoteUserDoesNotExist(StreamWriter sw)
        {
            try
            {
                Models.Note temp = GoogleFirestoreConnectionManager.CreateNote(userID1 + "NOTEXIST", notebookID1, text1);
                sw.WriteLine("FAILED: CreateNote(string userID, string notebookID, string noteText): CreateNote User does not exist test case.");
                GoogleFirestoreConnectionManager.DeleteNote(temp.ID);
                return false;
            }
            catch { return true; }
        }

        private bool CreateNoteUserIdIsNull(StreamWriter sw)
        {
            try
            {
                Models.Note temp = GoogleFirestoreConnectionManager.CreateNote(null, notebookID1, text1);
                sw.WriteLine("FAILED: CreateNote(string userID, string notebookID, string noteText): CreateNote userID is null test case.");
                GoogleFirestoreConnectionManager.DeleteNote(temp.ID);
                return false;
            }
            catch { return true; }
        }

        private bool CreateNoteUserIdIsEmpty(StreamWriter sw)
        {
            try
            {
                Models.Note temp = GoogleFirestoreConnectionManager.CreateNote("", notebookID1, text1);
                sw.WriteLine("FAILED: CreateNote(string userID, string notebookID, string noteText): CreateNote userID is empty test case.");
                GoogleFirestoreConnectionManager.DeleteNote(temp.ID);
                return false;
            }
            catch { return true; }
        }

        private bool CreateNoteNotebookDoesNotExist(StreamWriter sw)
        {
            try
            {
                Models.Note temp = GoogleFirestoreConnectionManager.CreateNote(userID1, notebookID1 + "NOTEXIST", text1);
                sw.WriteLine("FAILED: CreateNote(string userID, string notebookID, string noteText): CreateNote Notebook does not exist test case.");
                GoogleFirestoreConnectionManager.DeleteNote(temp.ID);
                return false;
            }
            catch { return true; }
        }

        private bool CreateNoteNotebookIdIsNull(StreamWriter sw)
        {
            try
            {
                Models.Note temp = GoogleFirestoreConnectionManager.CreateNote(userID1, null, text1);
                sw.WriteLine("FAILED: CreateNote(string userID, string notebookID, string noteText): CreateNote notebookID is null test case.");
                GoogleFirestoreConnectionManager.DeleteNote(temp.ID);
                return false;
            }
            catch { return true; }
        }

        private bool CreateNoteNotebookIdIsEmpty(StreamWriter sw)
        {
            try
            {
                Models.Note temp = GoogleFirestoreConnectionManager.CreateNote(userID1, "", text1);
                sw.WriteLine("FAILED: CreateNote(string userID, string notebookID, string noteText): CreateNote notebookID is empty test case.");
                GoogleFirestoreConnectionManager.DeleteNote(temp.ID);
                return false;
            }
            catch { return true; }
        }

        private bool CreateNoteNoteTextIsNull(StreamWriter sw)
        {
            try
            {
                Models.Note temp = GoogleFirestoreConnectionManager.CreateNote(userID1, notebookID1, null);
                sw.WriteLine("FAILED: CreateNote(string userID, string notebookID, string noteText): CreateNote note text is null test case.");
                GoogleFirestoreConnectionManager.DeleteNote(temp.ID);
                return false;
            }
            catch { return true; }
        }

        private bool CreateNoteNoteTextIsEmpty(StreamWriter sw)
        {
            try
            {
                Models.Note temp = GoogleFirestoreConnectionManager.CreateNote(userID1, notebookID1, "");
                sw.WriteLine("FAILED: CreateNote(string userID, string notebookID, string noteText): CreateNote note text is empty test case.");
                GoogleFirestoreConnectionManager.DeleteNote(temp.ID);
                return false;
            }
            catch { return true; }
        }
    }
}
