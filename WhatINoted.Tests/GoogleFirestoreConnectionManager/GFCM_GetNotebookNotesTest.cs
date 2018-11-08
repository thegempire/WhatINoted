using System;
using System.Collections.Generic;
using System.IO;
using WhatINoted.ConnectionManagers;
using WhatINoted.Models;

namespace WhatINoted.Tests.GoogleFirestoreConnectionManagerTests
{
    public class GFCM_GetNotebookNotesTest : GFCM_Test
    {
        public override bool Run(StreamWriter sw)
        {
            bool passed = true;
            passed = GetNotesValidRequest(sw) && passed;
            passed = GetNotesNotebookHasNoNotes(sw) && passed;
            passed = GetNotesNotebookDoesNotExist(sw) && passed;
            passed = GetNotesNotebookIdIsNull(sw) && passed;
            passed = GetNotesNotebookIsEmpty(sw) && passed;
            return passed;
        }

        private bool GetNotesValidRequest(StreamWriter sw)
        {
            try
            {
                GoogleFirestoreConnectionManager.HandleLogin(userID1, displayName1, email1);
                Models.Notebook createdNotebook = GoogleFirestoreConnectionManager.CreateNotebook(userID1, notebook1.Title, notebook1.Author, notebook1.Isbn, notebook1.Publisher, notebook1.PublishDate, notebook1.CoverURL);

                Note created1 = GoogleFirestoreConnectionManager.CreateNote(userID1, createdNotebook.ID, note1.Text);
                Note created2 = GoogleFirestoreConnectionManager.CreateNote(userID1, createdNotebook.ID, note2.Text);
                Note created3 = GoogleFirestoreConnectionManager.CreateNote(userID1, createdNotebook.ID, note3.Text);
                List<Models.Note> createdNotes = GoogleFirestoreConnectionManager.GetNotebookNotes(createdNotebook.ID);

                List<Models.Note> expectedNotes = new List<Models.Note>
                {
                    new Note(created1.ID, userID1, createdNotebook.ID, text1, DateTime.Now, DateTime.Now),
                    new Note(created2.ID, userID1, createdNotebook.ID, text2, DateTime.Now, DateTime.Now),
                    new Note(created3.ID, userID1, createdNotebook.ID, text3, DateTime.Now, DateTime.Now)
                };

                if (createdNotes.Count != expectedNotes.Count)
                {
                    sw.WriteLine("FAILED: GetNotes(string notebookID): Normal test case, count mismatch.");
                    GoogleFirestoreConnectionManager.DeleteUser(userID1);
                    return false;
                }
                else
                {
                    foreach (Models.Note n in createdNotes)
                    {
                        foreach (Models.Note t in expectedNotes)
                        {
                            if (t.Equals(n))
                            {
                                expectedNotes.Remove(t);
                                break;
                            }
                        }
                    }

                    if (expectedNotes.Count > 0)
                    {
                        sw.WriteLine("FAILED: GetNotes(string notebookID): Normal test case, Notes not the same.");
                        GoogleFirestoreConnectionManager.DeleteUser(userID1);
                        return false;
                    }
                }
                GoogleFirestoreConnectionManager.DeleteUser(userID1);
            }
            catch
            {
                sw.WriteLine("FAILED: GetNotes(string notebookID): Normal test case - unexpected exception.");
                return false;
            }
            return true;
        }

        private bool GetNotesNotebookHasNoNotes(StreamWriter sw)
        {
            try
            {
                GoogleFirestoreConnectionManager.HandleLogin(userID1, displayName1, email1);
                Models.Notebook createdNotebook = GoogleFirestoreConnectionManager.CreateNotebook(userID1, notebook1.Title, notebook1.Author, notebook1.Isbn, notebook1.Publisher, notebook1.PublishDate, notebook1.CoverURL);
                List<Models.Note> tempNotes = GoogleFirestoreConnectionManager.GetNotebookNotes(createdNotebook.ID);
                if (tempNotes == null || tempNotes.Count != 0)
                {
                    sw.WriteLine("FAILED: GetNotes(string notebookID): Notebook has no notes test case.");
                    GoogleFirestoreConnectionManager.DeleteUser(userID1);
                    return false;
                }
                GoogleFirestoreConnectionManager.DeleteUser(userID1);
            }
            catch
            {
                sw.WriteLine("FAILED: GetNotes(string notebookID): Notebook has no notes test case - unexpected exception.");
                return false;
            }
            return true;
        }

        private bool GetNotesNotebookDoesNotExist(StreamWriter sw)
        {
            try
            {
                List<Models.Note> tempNotes = GoogleFirestoreConnectionManager.GetNotebookNotes(notebookID1 + "NOTEXIST");
                if (tempNotes == null || tempNotes.Count != 0)
                {
                    sw.WriteLine("FAILED: GetNotes(string notebookID): Notebook has no notes test case.");
                    return false;
                }
            }
            catch
            {
                sw.WriteLine("FAILED: GetNotes(string notebookID): Notebook has no notes test case - unexpected exception.");
                return false;
            }
            return true;
        }

        private bool GetNotesNotebookIdIsNull(StreamWriter sw)
        {
            try
            {
                GoogleFirestoreConnectionManager.GetNotebookNotes(null);
                sw.WriteLine("FAILED: GetNotes(string notebookID): Notebook is null test case.");
                return false;
            }
            catch { return true; }
        }

        private bool GetNotesNotebookIsEmpty(StreamWriter sw)
        {
            try
            {
                GoogleFirestoreConnectionManager.GetNotebookNotes("");
                sw.WriteLine("FAILED: GetNotes(string notebookID): Notebook is empty test case.");
                return false;
            }
            catch { return true; }
        }
    }
}
