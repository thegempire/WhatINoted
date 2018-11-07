using System.IO;
using WhatINoted.ConnectionManagers;
using WhatINoted.Models;

namespace WhatINoted.Tests.GoogleFirestoreConnectionManagerTests
{
    public class GFCM_UpdateNoteTest : GFCM_Test
    {
        public override bool Run(StreamWriter sw)
        {
            bool passed = true;
            passed = UpdateNoteValidRequest(sw) && passed;
            passed = UpdateNoteNoteDoesNotExist(sw) && passed;
            passed = UpdateNoteNoteIdIsNull(sw) && passed;
            passed = UpdateNoteNoteIdIsEmpty(sw) && passed;
            passed = UpdateNoteNoteTextIsNull(sw) && passed;
            passed = UpdateNoteNoteTextIsEmpty(sw) && passed;
            return passed;
        }

        private bool UpdateNoteValidRequest(StreamWriter sw)
        {
            try
            {
                GoogleFirestoreConnectionManager.HandleLogin(userID1, displayName1, email1);
                Models.Notebook createdNotebook = GoogleFirestoreConnectionManager.CreateNotebook(userID1, notebook1.Title, notebook1.Author, notebook1.Isbn, notebook1.Publisher, notebook1.PublishDate, notebook1.CoverURL);
                Note createdNote = GoogleFirestoreConnectionManager.CreateNote(userID1, createdNotebook.ID, note1.Text);
                Note updatedNote = GoogleFirestoreConnectionManager.UpdateNote(createdNote.ID, text2);
                Note retrievedUpdatedNote = GoogleFirestoreConnectionManager.GetNote(updatedNote.ID);
                if (retrievedUpdatedNote.Text != text2)
                {
                    sw.WriteLine("FAILED: UpdateNote(string noteID, string noteText): Normal test case, note not updated.");
                    GoogleFirestoreConnectionManager.DeleteUser(userID1);
                    return false;

                }
                GoogleFirestoreConnectionManager.DeleteUser(userID1);
            }
            catch
            {
                sw.WriteLine("FAILED: UpdateNote(string noteID, string noteText): Normal test case - unexpected exception.");
                return false;
            }
            return true;
        }

        private bool UpdateNoteNoteDoesNotExist(StreamWriter sw)
        {
            try
            {
                GoogleFirestoreConnectionManager.UpdateNote(noteID1 + "NOTEXIST", text2);
                sw.WriteLine("FAILED: UpdateNote(string noteID, string noteText): User does not exist.");
                return false;
            }
            catch { return true; }
        }

        private bool UpdateNoteNoteIdIsNull(StreamWriter sw)
        {
            try
            {
                GoogleFirestoreConnectionManager.UpdateNote(null, text2);
                sw.WriteLine("FAILED: UpdateNote(string noteID, string noteText): noteID is null test case.");
                return false;
            }
            catch { return true; }
        }

        private bool UpdateNoteNoteIdIsEmpty(StreamWriter sw)
        {
            try
            {
                GoogleFirestoreConnectionManager.UpdateNote("", text2);
                sw.WriteLine("FAILED: UpdateNote(string noteID, string noteText): noteID is empty test case.");
                return false;
            }
            catch { return true; }
        }

        private bool UpdateNoteNoteTextIsNull(StreamWriter sw)
        {
            try
            {
                GoogleFirestoreConnectionManager.UpdateNote(noteID1, null);
                sw.WriteLine("FAILED: UpdateNote(string noteID, string noteText): noteText is null.");
                return false;
            }
            catch { return true; }
        }

        private bool UpdateNoteNoteTextIsEmpty(StreamWriter sw)
        {
            try
            {
                GoogleFirestoreConnectionManager.UpdateNote(noteID1, "");
                sw.WriteLine("FAILED: UpdateNote(string noteID, string noteText): noteText is empty.");
                return false;
            }
            catch { return true; }
        }
    }
}
