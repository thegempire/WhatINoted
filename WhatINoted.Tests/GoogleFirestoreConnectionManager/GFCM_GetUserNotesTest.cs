using System;
using System.Collections.Generic;
using System.IO;
using WhatINoted.Models;

namespace WhatINoted.Tests.GoogleFirestoreConnectionManager
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
                List<NoteModel> compNotes = new List<NoteModel>();
                compNotes.Add(note1);
                compNotes.Add(note2);
                compNotes.Add(note2);
                GoogleFirestoreConnectionManager.HandleLogin(userID1, displayName1, email1);
                Models.NotebookModel temp = GoogleFirestoreConnectionManager.CreateNotebook(userID1, notebook1.Isbn);
                GoogleFirestoreConnectionManager.CreateNote(userID1, notebookID1, note1.Text);
                GoogleFirestoreConnectionManager.CreateNote(userID1, notebookID1, note2.Text);
                GoogleFirestoreConnectionManager.CreateNote(userID1, notebookID1, note2.Text);
                List<NoteModel> tempNotes = GoogleFirestoreConnectionManager.GetUserNotes(userID1);
                if (tempNotes.Count != compNotes.Count)
                {
                    sw.WriteLine("FAILED: GetUserNotes(string userID): Normal test case, count mismatch.");
                    GoogleFirestoreConnectionManager.DeleteUser(userID1);
                    return false;
                }
                else
                {
                    foreach (NoteModel n in tempNotes)
                    {
                        foreach (NoteModel t in compNotes)
                        {
                            if (t.Equals(n))
                            {
                                compNotes.Remove(t);
                                break;
                            }
                        }
                    }

                    if (compNotes.Count > 0)
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
                List<NoteModel> tempNotes = GoogleFirestoreConnectionManager.GetUserNotes(userID2);
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
                List<NoteModel> tempNotes = GoogleFirestoreConnectionManager.GetUserNotes(userID1 + "NOTEXIST");
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
