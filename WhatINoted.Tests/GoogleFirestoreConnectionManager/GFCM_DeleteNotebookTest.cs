using System;
using System.Collections.Generic;
using System.IO;
using WhatINoted.ConnectionManagers;
using WhatINoted.Models;

namespace WhatINoted.Tests.GoogleFirestoreConnectionManagerTests
{
    public class GFCM_DeleteNotebookTest : GFCM_Test
    {
        public override bool Run(StreamWriter sw)
        {
            bool passed = true;
            passed = DeleteNotebookValidRequest(sw) && passed;
            passed = DeleteNotebookWithNotes(sw) && passed;
            passed = DeleteNotebookNotebookDoesNotExist(sw) && passed;
            passed = DeleteNotebookNotebookIdIsNull(sw) && passed;
            passed = DeleteNotebookNotebookIdIsEmpty(sw) && passed;
            return passed;
        }

        private bool DeleteNotebookValidRequest(StreamWriter sw) {
            try
            {
                GoogleFirestoreConnectionManager.HandleLogin(userID1, displayName1, email1);
                Models.Notebook temp = GoogleFirestoreConnectionManager.CreateNotebook(userID1, isbn1);
                if (!GoogleFirestoreConnectionManager.DeleteNotebook(temp.ID))
                {
                    sw.WriteLine("FAILED: DeleteNotebook(string notebookID): Normal test case 1.");
                    return false;
                }
            }
            catch
            {
                sw.WriteLine("FAILED: DeleteNotebook(string notebookID): Normal test case 1 - unexpected exception.");
                return false;
            }
            return true;
        }

        private bool DeleteNotebookWithNotes(StreamWriter sw) {
            try
            {
                GoogleFirestoreConnectionManager.HandleLogin(userID1, displayName1, email1);
                notebookID1 = GoogleFirestoreConnectionManager.CreateNotebook(userID1, isbn1).ID;
                noteID1 = GoogleFirestoreConnectionManager.CreateNote(userID1, notebookID1, text1).ID;
                GoogleFirestoreConnectionManager.DeleteNotebook(notebookID1);
                List<Note> tempNotes = GoogleFirestoreConnectionManager.GetNotebookNotes(notebookID1);
                if (tempNotes == null || tempNotes.Count != 0)
                {
                    sw.WriteLine("FAILED: DeleteNotebook(string notebookID): Delete notebook with notes");
                    GoogleFirestoreConnectionManager.DeleteNote(noteID1);
                    return false;
                }
            }
            catch
            {
                sw.WriteLine("FAILED: DeleteNotebook(string notebookID): Delete notebook with notes - unexpected exception.");
                return false;
            }
            return true;
        }

        private bool DeleteNotebookNotebookDoesNotExist(StreamWriter sw) {
            try
            {
                if (!GoogleFirestoreConnectionManager.DeleteNotebook(notebookID1 + "NOTEXIST"))
                {
                    sw.WriteLine("FAILED: DeleteNotebook(string notebookID): Notebook does not exist test case.");
                    return false;
                }
            }
            catch
            {
                sw.WriteLine("FAILED: DeleteNotebook(string notebookID): Notebook does not exist test case - unexpected exception.");
                return false;
            }
            return true;
        }

        private bool DeleteNotebookNotebookIdIsNull(StreamWriter sw)
        {
            try
            {
                GoogleFirestoreConnectionManager.DeleteNotebook(null);
                sw.WriteLine("FAILED: DeleteNotebook(string notebookID): notebookID is null test case.");
                return false;
            }
            catch { return true; }
        }

        private bool DeleteNotebookNotebookIdIsEmpty(StreamWriter sw) {
            try
            {
                GoogleFirestoreConnectionManager.DeleteNotebook("");
                sw.WriteLine("FAILED: DeleteNotebook(string notebookID): notebookID is empty test case.");
                return false;
            }
            catch { return true; }
        }
    }
}
