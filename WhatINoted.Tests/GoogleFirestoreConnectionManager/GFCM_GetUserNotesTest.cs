using System;
using System.Collections.Generic;
using System.IO;
using WhatINoted.ConnectionManagers;
using WhatINoted.Models;

namespace WhatINoted.Tests.GoogleFirestoreConnectionManagerTests
{
    public class GFCM_GetUserNotesTest : GFCM_Test
    {
        public override bool Run(StreamWriter sw)
        {
            bool passed = true;
            passed = GetUserNotesValidRequest(sw) && passed;
            passed = GetUserNotesUserHasNoNotes(sw) && passed;
            passed = GetUserNotesUserDoesNotExist(sw) && passed;
            passed = GetUserNotesUserIdIsNull(sw) && passed;
            passed = GetUserNotesUserIdIsEmpty(sw) && passed;
            return passed;
        }

        private bool GetUserNotesValidRequest(StreamWriter sw) {
            try
            {
                GoogleFirestoreConnectionManager.HandleLogin(userID1, displayName1, email1);
                Models.Notebook createdNotebook = GoogleFirestoreConnectionManager.CreateNotebook(userID1, notebook1.Title, notebook1.Author, notebook1.Isbn, notebook1.Publisher, notebook1.PublishDate, notebook1.CoverURL);

                Note created1 = GoogleFirestoreConnectionManager.CreateNote(userID1, createdNotebook.ID, note1.Text);
                Note created2 = GoogleFirestoreConnectionManager.CreateNote(userID1, createdNotebook.ID, note2.Text);
                Note created3 = GoogleFirestoreConnectionManager.CreateNote(userID1, createdNotebook.ID, note3.Text);
                List<Models.Note> createdNotes = GoogleFirestoreConnectionManager.GetUserNotes(userID1);

                List<Models.Note> expectedNotes = new List<Models.Note>
                {
                    new Note(created1.ID, userID1, createdNotebook.ID, text1, DateTime.Now, DateTime.Now),
                    new Note(created2.ID, userID1, createdNotebook.ID, text2, DateTime.Now, DateTime.Now),
                    new Note(created3.ID, userID1, createdNotebook.ID, text3, DateTime.Now, DateTime.Now)
                };

                if (createdNotes.Count != expectedNotes.Count)
                {
                    sw.WriteLine("FAILED: GetUserNotes(string userID): Normal test case, count mismatch.");
                    GoogleFirestoreConnectionManager.DeleteUser(userID1);
                    return false;
                }
                else
                {
                    foreach (Note n in createdNotes)
                    {
                        foreach (Note t in expectedNotes)
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
                        sw.WriteLine("FAILED: GetUserNotes(string userID): Normal test case, Notes not the same.");
                        GoogleFirestoreConnectionManager.DeleteUser(userID1);
                        return false;
                    }
                }
                GoogleFirestoreConnectionManager.DeleteUser(userID1);
            }
            catch
            {
                sw.WriteLine("FAILED: GetUserNotes(string userID): Normal test case - unexpected exception.");
                return false;
            }
            return true;
        }

        private bool GetUserNotesUserHasNoNotes(StreamWriter sw) {
            try
            {
                List<Note> tempNotes = GoogleFirestoreConnectionManager.GetUserNotes(userID2);
                if (tempNotes == null || tempNotes.Count != 0)
                {
                    sw.WriteLine("FAILED: GetNotes(string userID): User has no notes test case.");
                    return false;
                }
            }
            catch
            {
                sw.WriteLine("FAILED: GetNotes(string userID): User has no notes test case - unexpected exception.");
                return false;
            }
            return true;
        }

        private bool GetUserNotesUserDoesNotExist(StreamWriter sw) {
            try
            {
                List<Note> tempNotes = GoogleFirestoreConnectionManager.GetUserNotes(userID1 + "NOTEXIST");
                if (tempNotes == null || tempNotes.Count != 0)
                {
                    sw.WriteLine("FAILED: GetUserNotes(string userID): User has no notes test case.");
                    return false;
                }
            }
            catch
            {
                sw.WriteLine("FAILED: GetUserNotes(string userID): User has no notes test case - unexpected exception.");
                return false;
            }
            return true;
        }

        private bool GetUserNotesUserIdIsNull(StreamWriter sw) {
            try
            {
                GoogleFirestoreConnectionManager.GetUserNotes(null);
                sw.WriteLine("FAILED: GetUserNotes(string userID): userID is null test case.");
                return false;
            }
            catch { return true; }
        }

        private bool GetUserNotesUserIdIsEmpty(StreamWriter sw) {
            try
            {
                GoogleFirestoreConnectionManager.GetUserNotes("");
                sw.WriteLine("FAILED: GetUserNotes(string userID): userID is empty test case.");
                return false;
            }
            catch { return true;  }
        }
    }
}
