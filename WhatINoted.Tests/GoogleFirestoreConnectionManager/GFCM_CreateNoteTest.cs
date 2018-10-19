using System;
using System.IO;

namespace WhatINoted.Tests.GoogleFirestoreConnectionManager
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
                Models.NoteModel temp = GoogleFirestoreConnectionManager.CreateNote(userID1, notebookID1, noteText);
                if (!temp.Equals(note1))
                {
                    sw.WriteLine("FAILED: CreateNote(string userID, string notebookID, string noteText): CreateNote normal test case.");
                    GoogleFirestoreConnectionManager.DeleteNote(temp.Id);
                    return false;
                }
                else
                {
                    noteID1 = temp.Id;
                    GoogleFirestoreConnectionManager.DeleteNote(noteID1);
                }
            }
            catch
            {
                sw.WriteLine("FAILED: CreateNote(string userID, string notebookID, string noteText): CreateNote normal test case - unexpected exception.");
                return false;
            }
            return true;
        }

        private bool CreateNoteUserDoesNotExist(StreamWriter sw) {
            try
            {
                Models.NoteModel temp = GoogleFirestoreConnectionManager.CreateNote(userID1 + "NOTEXIST", notebookID1, noteText);
                sw.WriteLine("FAILED: CreateNote(string userID, string notebookID, string noteText): CreateNote User does not exist test case.");
                GoogleFirestoreConnectionManager.DeleteNote(temp.Id);
                return false;
            }
            catch { return true; }
        }

        private bool CreateNoteUserIdIsNull(StreamWriter sw) {
            try
            {
                Models.NoteModel temp = GoogleFirestoreConnectionManager.CreateNote(null, notebookID1, noteText);
                sw.WriteLine("FAILED: CreateNote(string userID, string notebookID, string noteText): CreateNote userID is null test case.");
                GoogleFirestoreConnectionManager.DeleteNote(temp.Id);
                return false;
            }
            catch { return true; }
        }

        private bool CreateNoteUserIdIsEmpty(StreamWriter sw) {
            try
            {
                Models.NoteModel temp = GoogleFirestoreConnectionManager.CreateNote("", notebookID1, noteText);
                sw.WriteLine("FAILED: CreateNote(string userID, string notebookID, string noteText): CreateNote userID is empty test case.");
                GoogleFirestoreConnectionManager.DeleteNote(temp.Id);
                return false;
            }
            catch { return true; }
        }

        private bool CreateNoteNotebookDoesNotExist(StreamWriter sw) {
            try
            {
                Models.NoteModel temp = GoogleFirestoreConnectionManager.CreateNote(userID1, notebookID1 + "NOTEXIST", noteText);
                sw.WriteLine("FAILED: CreateNote(string userID, string notebookID, string noteText): CreateNote Notebook does not exist test case.");
                GoogleFirestoreConnectionManager.DeleteNote(temp.Id);
                return false;
            }
            catch { return true; }
        }

        private bool CreateNoteNotebookIdIsNull(StreamWriter sw) {
            try
            {
                Models.NoteModel temp = GoogleFirestoreConnectionManager.CreateNote(userID1, null, noteText);
                sw.WriteLine("FAILED: CreateNote(string userID, string notebookID, string noteText): CreateNote notebookID is null test case.");
                GoogleFirestoreConnectionManager.DeleteNote(temp.Id);
                return false;
            }
            catch { return true; }
        }

        private bool CreateNoteNotebookIdIsEmpty(StreamWriter sw) {
            try
            {
                Models.NoteModel temp = GoogleFirestoreConnectionManager.CreateNote(userID1, "", noteText);
                sw.WriteLine("FAILED: CreateNote(string userID, string notebookID, string noteText): CreateNote notebookID is empty test case.");
                GoogleFirestoreConnectionManager.DeleteNote(temp.Id);
                return false;
            }
            catch { return true; }
        }

        private bool CreateNoteNoteTextIsNull(StreamWriter sw) {
            try
            {
                Models.NoteModel temp = GoogleFirestoreConnectionManager.CreateNote(userID1, notebookID1, null);
                if (!temp.Equals(note2))
                {
                    sw.WriteLine("FAILED: CreateNote(string userID, string notebookID, string noteText): CreateNote noteText is null test case.");
                    if (temp != null) {
                        GoogleFirestoreConnectionManager.DeleteNote(temp.Id);
                    }
                    return false;
                }
                else
                {
                    noteID2 = temp.Id;
                    GoogleFirestoreConnectionManager.DeleteNote(noteID2);
                }
            }
            catch
            {
                sw.WriteLine("FAILED: CreateNote(string userID, string notebookID, string noteText): CreateNote noteText is null test case - unexpected exception.");
                return false;
            }
            return true;
        }

        private bool CreateNoteNoteTextIsEmpty(StreamWriter sw) {
            try
            {
                Models.NoteModel temp = GoogleFirestoreConnectionManager.CreateNote(userID1, notebookID1, "");
                if (!temp.Equals(note2))
                {
                    sw.WriteLine("FAILED: CreateNote(string userID, string notebookID, string noteText): CreateNote noteText is empty test case.");
                    if (temp != null) {
                        GoogleFirestoreConnectionManager.DeleteNote(temp.Id);
                    }
                    return false;
                }
                else
                {
                    noteID3 = temp.Id;
                    GoogleFirestoreConnectionManager.DeleteNote(noteID3);
                }
            }
            catch
            {
                sw.WriteLine("FAILED: CreateNote(string userID, string notebookID, string noteText): CreateNote noteText is empty test case- unexpected exception.");
                return false;
            }
            return true;
        }
    }

}
