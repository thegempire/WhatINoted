using System;
using System.IO;
using WhatINoted.Models;

namespace WhatINoted.Tests.GoogleFirestoreConnectionManagerTests
{
    public class GFCM_GetNoteTest : GFCM_Test
    {
        public override bool Run(StreamWriter sw)
        {
            bool passed = true;
            passed = GetNoteValidRequest(sw) && passed;
            passed = GetNoteNoteDoesNotExist(sw) && passed;
            passed = GetNoteNoteIdIsNull(sw) && passed;
            passed = GetNoteNoteIdIsEmpty(sw) && passed;
            return passed;
        }

        private bool GetNoteValidRequest(StreamWriter sw)
        {
            try
            {
                GoogleFirestoreConnectionManager.HandleLogin(userID1, displayName1, email1);
                Models.Notebook createdNotebook = GoogleFirestoreConnectionManager.CreateNotebook(userID1, notebook1.Isbn);
                Note createdNote = GoogleFirestoreConnectionManager.CreateNote(userID1, createdNotebook.ID, note1.Text);
                Note expectedNote = new Note(createdNote.ID, userID1, createdNotebook.ID, note1.Text, DateTime.Now, DateTime.Now);
                if (!GoogleFirestoreConnectionManager.GetNote(createdNote.ID).Equals(expectedNote))
                {
                    sw.WriteLine("FAILED: GetNote(string noteID): Normal test case.");
                    GoogleFirestoreConnectionManager.DeleteUser(userID1);
                    return false;
                }
                GoogleFirestoreConnectionManager.DeleteUser(userID1);
            }
            catch
            {
                sw.WriteLine("FAILED: GetNote(string noteID): Normal test case - unexpected exception.");
                return false;
            }
            return true;
        }

        private bool GetNoteNoteDoesNotExist(StreamWriter sw)
        {
            try
            {
                GoogleFirestoreConnectionManager.GetNote(noteID1 + "NOTEXIST");
                return false;
            }
            catch (NotFoundException)
            {
                return true;
            }
            catch
            {
                sw.WriteLine("FAILED: GetNote(string noteID): Note does not exist test case - unexpected exception.");
                return false;
            }
        }

        private bool GetNoteNoteIdIsNull(StreamWriter sw)
        {
            try
            {
                GoogleFirestoreConnectionManager.GetNote(null);
                sw.WriteLine("FAILED: GetNote(string noteID): noteID is null test case.");
                return false;
            }
            catch { return true; }
        }

        private bool GetNoteNoteIdIsEmpty(StreamWriter sw)
        {
            try
            {
                GoogleFirestoreConnectionManager.GetNote("");
                sw.WriteLine("FAILED: GetNote(string noteID): noteID is empty test case.");
                return false;
            }
            catch { return true; }
        }
    }
}
