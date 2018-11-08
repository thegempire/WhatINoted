using System.IO;
using WhatINoted.ConnectionManagers;

namespace WhatINoted.Tests.GoogleFirestoreConnectionManagerTests
{
    public class GFCM_DeleteNoteTest : GFCM_Test
    {
        public override bool Run(StreamWriter sw)
        {
            bool passed = true;
            passed = DeleteNoteValidRequest(sw) && passed;
            passed = DeleteNoteNoteDoesNotExist(sw) && passed;
            passed = DeleteNoteNoteIdIsNull(sw) && passed;
            passed = DeleteNoteNoteIdIsEmpty(sw) && passed;
            return passed;
        }

        private bool DeleteNoteValidRequest(StreamWriter sw)
        {
            try
            {
                GoogleFirestoreConnectionManager.HandleLogin(userID1, displayName1, email1);
                Models.Notebook createdNotebook = GoogleFirestoreConnectionManager.CreateNotebook(userID1, notebook1.Title, notebook1.Author, notebook1.Isbn, notebook1.Publisher, notebook1.PublishDate, notebook1.CoverURL);
                Models.Note createdNote = GoogleFirestoreConnectionManager.CreateNote(userID1, createdNotebook.ID, text1);
                if (!GoogleFirestoreConnectionManager.DeleteNote(createdNote.ID))
                {
                    sw.WriteLine("FAILED: DeleteNote(string noteID): Normal test case 1.");
                    GoogleFirestoreConnectionManager.DeleteUser(userID1);
                    return false;
                }
                else
                {
                    try
                    {
                        GoogleFirestoreConnectionManager.GetNote(createdNote.ID);
                    }
                    catch (NotFoundException)
                    {
                        GoogleFirestoreConnectionManager.DeleteUser(userID1);
                        return true;
                    }
                    GoogleFirestoreConnectionManager.DeleteUser(userID1);
                    return false;
                }
            }
            catch
            {
                sw.WriteLine("FAILED: DeleteNote(string noteID): Normal test case 1 - unexpected exception.");
                return false;
            }
        }

        private bool DeleteNoteNoteDoesNotExist(StreamWriter sw)
        {
            try
            {
                if (!GoogleFirestoreConnectionManager.DeleteNote(noteID1 + "NOTEXIST"))
                {
                    sw.WriteLine("FAILED: DeleteNote(string noteID): Note does not exist test case.");
                    return false;
                }
            }
            catch
            {
                sw.WriteLine("FAILED: DeleteNote(string noteID): Note does not exist test case - unexpected exception.");
                return false;
            }
            return true;
        }

        private bool DeleteNoteNoteIdIsNull(StreamWriter sw)
        {
            try
            {
                GoogleFirestoreConnectionManager.DeleteNote(null);
                sw.WriteLine("FAILED: DeleteNote(string noteID): noteID is null test case.");
                return false;
            }
            catch { return true; }
        }

        private bool DeleteNoteNoteIdIsEmpty(StreamWriter sw)
        {
            try
            {
                GoogleFirestoreConnectionManager.DeleteNote("");
                sw.WriteLine("FAILED: DeleteNote(string noteID): noteID is empty test case.");
                return false;
            }
            catch { return true; }
        }
    }
}
