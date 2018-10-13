using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using WhatINoted.Models;

namespace WhatINoted.Tests2
{
    public class GoogleFirestoreConnectionManagerTests : Test
    {
        //These variables may need work, comparisons may need work
        public const string userID1 = "GoogleFirestoreConnectionManagerTests_UserID1";
        public const string displayName1 = "GoogleFirestoreConnectionManagerTests_DisplayName1";
        public const string email1 = "GoogleFirestoreConnectionManagerTests_Email1";
        public const string userID2 = "GoogleFirestoreConnectionManagerTests_UserID2";
        public const string displayName2 = "GoogleFirestoreConnectionManagerTests_DisplayName2";
        public const string email2 = "GoogleFirestoreConnectionManagerTests_Email2";
        public const string isbn1 = "9780553804577";
        private string notebookID1;
        private NotebookModel notebook1 = new NotebookModel("The Google Story", "David A. Vise; Mark Malseed", null, null, null); //ISBN Notebook
        private string notebookID2;
        private  NotebookModel notebook2 = new NotebookModel("The Google Story", "David A. Vise; Mark Malseed", null, null, null); //Book details
        private string notebookID3;
        private NotebookModel notebook3 = new NotebookModel("The Google Story", "", null, null, null); // book details (no author)
        private string notebookID4; //same info as 3
        private string notebookID5;
        private NotebookModel notebook5 = new NotebookModel("The Google Story", "David A. Vise; Mark Malseed", null, null, null); // Book details (no publisher)
        private string notebookID6; //same info as 5
        private string notebookID7;
        private NotebookModel notebook7 = new NotebookModel("The Google Story", "David A. Vise; Mark Malseed", null, null, null); //Book details (no publisher location)
        //Create the expected notebook object with below and more (no publish date)
        //public const string notebook1Title = "The Google Story";
        //public const string notebook1Author = "David A. Vise; Mark Malseed";
        //public const string notebook1Publisher = "Delacorte Press";
        //private DateTime notebook1PublishDate { get; } = new DateTime(2005, 11, 15);
        public const string noteText = "Test_CreateNote note text.";
        private string noteID1;
        private NoteModel note1;
        private string noteID2;
        private NoteModel note2;
        private string noteID3; //same info as 2
        public const string noteTextUpdated = "Test_UpdateNote note text.";

        public bool Run(StreamWriter sw)
        {
            SetupTestData(sw);
            bool passed = true;

            if (!TestConnection(sw))
                passed = false;
            else
                sw.WriteLine("PASSED: TestConnection");

            if (!Test_HandleLogin(sw))
                passed = false;
            else
                sw.WriteLine("PASSED: HandleLogin");

            if (!Test_GetDisplayName(sw))
                passed = false;
            else
                sw.WriteLine("PASSED: GetDisplayName");

            if (!Test_CreateNotebook(sw))
                passed = false;
            else
                sw.WriteLine("PASSED: CreateNotebook");

            if (!Test_CreateNote(sw))
                passed = false;
            else
                sw.WriteLine("PASSED: CreateNote");

            if (!Test_GetNotebooks(sw))
                passed = false;
            else
                sw.WriteLine("PASSED: GetNotebooks");

            if (!Test_GetNotes(sw))
                passed = false;
            else
                sw.WriteLine("PASSED: GetNotes");

            if (!Test_GetUserNotes(sw))
                passed = false;
            else
                sw.WriteLine("PASSED: GetUserNotes");

            if (!Test_GetNote(sw))
                passed = false;
            else
                sw.WriteLine("PASSED: GetNote");

            if (!Test_UpdateNote(sw))
                passed = false;
            else
                sw.WriteLine("PASSED: UpdateNote");

            if (!Test_DeleteNote(sw))
                passed = false;
            else
                sw.WriteLine("PASSED: DeleteNote");

            if (!Test_DeleteNotebook(sw))
                passed = false;
            else
                sw.WriteLine("PASSED: DeleteNotebook");

            if (!Test_DeleteUser(sw))
                passed = false;
            else
                sw.WriteLine("PASSED: DeleteUser");

            return passed;
        }

        private void SetupTestData(StreamWriter sw)
        {
            note1 = new NoteModel(noteText, notebook1, DateTime.Now, DateTime.Now, null);
            note2 = new NoteModel("", notebook1, DateTime.Now, DateTime.Now, null);
        }

        private bool TestConnection(StreamWriter sw)
        {
            return true;
        }

        private bool Test_HandleLogin(StreamWriter sw)
        {
            bool passed = true;

            //HandleLogin Normal Account Creation
            try
            {
                if (!GoogleFirestoreConnectionManager.HandleLogin(userID1, displayName1, email1))
                {
                    sw.WriteLine("FAILED: HandleLogin(string userID, string displayName, string email): Normal Account Creation.");
                    passed = false;
                }
            }
            catch
            {
                sw.WriteLine("FAILED: HandleLogin(string userID, string displayName, string email): Normal Account Creation - unexpected exception.");
                passed = false;
            }

            //HandleLogin Normal Account Creation 2
            try
            {
                if (!GoogleFirestoreConnectionManager.HandleLogin(userID2, displayName2, email2))
                {
                    sw.WriteLine("FAILED: HandleLogin(string userID, string displayName, string email): Normal Account Creation 2.");
                    passed = false;
                }
            }
            catch
            {
                sw.WriteLine("FAILED: HandleLogin(string userID, string displayName, string email): Normal Account Creation 2 - unexpected exception.");
                passed = false;
            }

            //HandleLogin Normal Account Login
            try
            {
                if (!GoogleFirestoreConnectionManager.HandleLogin(userID1, displayName1, email1))
                {
                    sw.WriteLine("FAILED: HandleLogin(string userID, string displayName, string email): Normal Account Login.");
                    passed = false;
                }
            }
            catch
            {
                sw.WriteLine("FAILED: HandleLogin(string userID, string displayName, string email): Normal Account Login - unexpected exception.");
                passed = false;
            }

            //HandleLogin userID is null
            try
            {
                GoogleFirestoreConnectionManager.HandleLogin(null, displayName1, email1);
                sw.WriteLine("FAILED: HandleLogin(string userID, string displayName, string email): userID is null test case.");
                passed = false;
            }
            catch { }

            //HandleLogin userID is empty
            try
            {
                GoogleFirestoreConnectionManager.HandleLogin("", displayName1, email1);
                sw.WriteLine("FAILED: HandleLogin(string userID, string displayName, string email): userID is empty string test case.");
                passed = false;
            }
            catch { }

            //HandleLogin displayName is null
            try
            {
                GoogleFirestoreConnectionManager.HandleLogin(userID1, null, email1);
                sw.WriteLine("FAILED: HandleLogin(string userID, string displayName, string email): displayName is null test case.");
                passed = false;
            }
            catch { }

            //HandleLogin displayName is empty
            try
            {
                GoogleFirestoreConnectionManager.HandleLogin(userID1, "", email1);
                sw.WriteLine("FAILED: HandleLogin(string userID, string displayName, string email): displayName is empty string test case.");
                passed = false;
            }
            catch { }

            //HandleLogin email is null
            try
            {
                GoogleFirestoreConnectionManager.HandleLogin(userID1, displayName1, null);
                sw.WriteLine("FAILED: HandleLogin(string userID, string displayName, string email): email is null test case.");
                passed = false;
            }
            catch { }

            //HandleLogin email is empty
            try
            {
                GoogleFirestoreConnectionManager.HandleLogin(userID1, displayName1, "");
                sw.WriteLine("FAILED: HandleLogin(string userID, string displayName, string email): email is empty string test case.");
                passed = false;
            }
            catch { }

            return passed;
        }

        private bool Test_GetDisplayName(StreamWriter sw)
        {
            bool passed = true;

            //GetDisplayName Normal request
            try
            {
                if (GoogleFirestoreConnectionManager.GetDisplayName(userID1) != displayName1)
                {
                    sw.WriteLine("FAILED: GetDisplayName(string userID): Normal DisplayName request.");
                    passed = false;
                }
            }
            catch
            {
                sw.WriteLine("FAILED: GetDisplayName(string userID): Normal DisplayName request - unexpected exception.");
                passed = false;
            }

            //GetDisplayName User does not exist
            try
            {
                GoogleFirestoreConnectionManager.GetDisplayName(userID1 + "NOTEXIST");
                sw.WriteLine("FAILED: GetDisplayName(string userID): DisplayName request for non-existing user.");
                passed = false;
            }
            catch { }

            //GetDisplayName userID is null
            try
            {
                GoogleFirestoreConnectionManager.GetDisplayName(null);
                sw.WriteLine("FAILED: GetDisplayName(string userID): userID is null test case.");
                passed = false;
            }
            catch { }

            //GetDisplayName userID is empty
            try
            {
                GoogleFirestoreConnectionManager.GetDisplayName("");
                sw.WriteLine("FAILED: GetDisplayName(string userID): userID is empty test case.");
                passed = false;
            }
            catch { }

            return passed;
        }

        private bool Test_CreateNotebook(StreamWriter sw)
        {
            bool passed = true;

            //CreateNotebook Normal request - ISBN
            try
            {
                NotebookModel temp = GoogleFirestoreConnectionManager.CreateNotebook(userID1, isbn1);
                if (!temp.Equals(notebook1))
                {
                    sw.WriteLine("FAILED: CreateNotebook(string userID, string isbn): Normal Create Notebook by ISBN request.");
                    passed = false;
                }
                else
                {
                    notebookID1 = temp.Id; //stored for deleting later (auto-generated)
                }
            }
            catch
            {
                sw.WriteLine("FAILED: CreateNotebook(string userID, string isbn): Normal Create Notebook by ISBN request - unexpected exception.");
                passed = false;
            }

            //CreateNotebook Normal request - Book Details
            try
            {
                NotebookModel temp = GoogleFirestoreConnectionManager.CreateNotebook(userID1, notebook1.Title, notebook1.Author, notebook1.Publisher, notebook1.PublishDate);
                if (!temp.Equals(notebook2))
                {
                    sw.WriteLine("FAILED: CreateNotebook(string userID, string title, string author, string publisher, DateTime publishDate): Normal Create Notebook by Book Details request.");
                    passed = false;
                }
                else
                {
                    notebookID2 = temp.Id;
                }
            }
            catch
            {
                sw.WriteLine("FAILED: CreateNotebook(string userID, string title, string author, string publisher, DateTime publishDate): Normal Create Notebook by Book Details request - unexpected exception.");
                passed = false;
            }

            //CreateNotebook User does not exist - ISBN
            try
            {
                GoogleFirestoreConnectionManager.CreateNotebook(userID1 + "NOTEXIST", isbn1);
                sw.WriteLine("FAILED: CreateNotebook(string userID, string isbn): CreateNotebook by ISBN User does not exist test case.");
                passed = false;
            }
            catch { }

            //CreateNotebook User does not exist - Book Details
            try
            {
                GoogleFirestoreConnectionManager.CreateNotebook(userID1 + "NOTEXIST", notebook1.Title, notebook1.Author, notebook1.Publisher, notebook1.PublishDate);
                sw.WriteLine("FAILED: CreateNotebook(string userID, string isbn): CreateNotebook by Book Details User does not exist test case.");
                passed = false;
            }
            catch { }

            //CreateNotebook userID is null - ISBN
            try
            {
                GoogleFirestoreConnectionManager.CreateNotebook(null, isbn1);
                sw.WriteLine("FAILED: CreateNotebook(string userID, string isbn): CreateNotebook by ISBN userID is null test case.");
                passed = false;
            }
            catch { }

            //CreateNotebook userID is null - Book Details
            try
            {
                GoogleFirestoreConnectionManager.CreateNotebook(null, notebook1.Title, notebook1.Author, notebook1.Publisher, notebook1.PublishDate);
                sw.WriteLine("FAILED: CreateNotebook(string userID, string title, string author, string publisher, string publishDate): CreateNotebook by Book Details userID is null test case.");
                passed = false;
            }
            catch { }

            //CreateNotebook userID is empty - ISBN
            try
            {
                GoogleFirestoreConnectionManager.CreateNotebook("", isbn1);
                sw.WriteLine("FAILED: CreateNotebook(string userID, string isbn): CreateNotebook by ISBN userID is empty test case.");
                passed = false;
            }
            catch { }

            //CreateNotebook userID is empty - Book Details
            try
            {
                GoogleFirestoreConnectionManager.CreateNotebook("", notebook1.Title, notebook1.Author, notebook1.Publisher, notebook1.PublishDate);
                sw.WriteLine("FAILED: CreateNotebook(string userID, string title, string author, string publisher, string publishDate): CreateNotebook by Book Details userID is empty test case.");
                passed = false;
            }
            catch { }

            //CreateNotebook isbn is null - ISBN
            try
            {
                GoogleFirestoreConnectionManager.CreateNotebook(userID1, null);
                sw.WriteLine("FAILED: CreateNotebook(string userID, string isbn): CreateNotebook by ISBN isbn is null test case.");
                passed = false;
            }
            catch { }

            //CreateNotebook isbn is empty - ISBN
            try
            {
                GoogleFirestoreConnectionManager.CreateNotebook(userID1, "");
                sw.WriteLine("FAILED: CreateNotebook(string userID, string isbn): CreateNotebook by ISBN isbn is empty test case.");
                passed = false;
            }
            catch { }

            //CreateNotebook isbn is invalid 1 - ISBN
            try
            {
                GoogleFirestoreConnectionManager.CreateNotebook(userID1, "01234Invalid5");
                sw.WriteLine("FAILED: CreateNotebook(string userID, string isbn): CreateNotebook by ISBN isbn is invalid test case 1.");
                passed = false;
            }
            catch { }

            //CreateNotebook isbn is invalid 2 - ISBN
            try
            {
                GoogleFirestoreConnectionManager.CreateNotebook(userID1, "012345");
                sw.WriteLine("FAILED: CreateNotebook(string userID, string isbn): CreateNotebook by ISBN isbn is invalid test case 2.");
                passed = false;
            }
            catch { }

            //CreateNotebook title is null - Book Details
            try
            {
                GoogleFirestoreConnectionManager.CreateNotebook(userID1, null, notebook1.Author, notebook1.Publisher, notebook1.PublishDate);
                sw.WriteLine("FAILED: CreateNotebook(string userID, string title, string author, string publisher, string publishDate): CreateNotebook by Book Details title is null test case.");
                passed = false;
            }
            catch { }

            //CreateNotebook title is empty- Book Details
            try
            {
                GoogleFirestoreConnectionManager.CreateNotebook(userID1, "", notebook1.Author, notebook1.Publisher, notebook1.PublishDate);
                sw.WriteLine("FAILED: CreateNotebook(string userID, string title, string author, string publisher, string publishDate): CreateNotebook by Book Details title is empty test case.");
                passed = false;
            }
            catch { }

            //CreateNotebook author is null - Book Details
            try
            {
                NotebookModel temp = GoogleFirestoreConnectionManager.CreateNotebook(userID1, notebook1.Title, null, notebook1.Publisher, notebook1.PublishDate);
                if (!temp.Equals(notebook3))
                {
                    sw.WriteLine("FAILED: CreateNotebook(string userID, string title, string author, string publisher, string publishDate): CreateNotebook by Book Details author is null test case.");
                    passed = false;
                }
                else
                {
                    notebookID3 = temp.Id;
                }
            }
            catch
            {
                sw.WriteLine("FAILED: CreateNotebook(string userID, string title, string author, string publisher, string publishDate): CreateNotebook by Book Details author is null test case.");
                passed = false;
            }

            //CreateNotebook author is empty - Book Details
            try
            {
                NotebookModel temp = GoogleFirestoreConnectionManager.CreateNotebook(userID1, notebook1.Title, "", notebook1.Publisher, notebook1.PublishDate);
                if (!temp.Equals(notebook3))
                {
                    sw.WriteLine("FAILED: CreateNotebook(string userID, string title, string author, string publisher, string publishDate): CreateNotebook by Book Details author is empty test case.");
                    passed = false;
                }
                else
                {
                    notebookID4 = temp.Id;
                }
            }
            catch
            {
                sw.WriteLine("FAILED: CreateNotebook(string userID, string title, string author, string publisher, string publishDate): CreateNotebook by Book Details author is empty test case - unexpected exception.");
                passed = false;
            }

            //CreateNotebook publisher is null - Book Details
            try
            {
                NotebookModel temp = GoogleFirestoreConnectionManager.CreateNotebook(userID1, notebook1.Title, notebook1.Author, null, notebook1.PublishDate);
                if (!temp.Equals(notebook5))
                {
                    sw.WriteLine("FAILED: CreateNotebook(string userID, string title, string author, string publisher, string publishDate): CreateNotebook by Book Details publisher is null test case.");
                    passed = false;
                }
                else
                {
                    notebookID5 = temp.Id;
                }
            }
            catch
            {
                sw.WriteLine("FAILED: CreateNotebook(string userID, string title, string author, string publisher, string publishDate): CreateNotebook by Book Details publisher is null test case - unexpected exception.");
                passed = false;
            }

            //CreateNotebook publisher is empty - Book Details
            try
            {
                NotebookModel temp = GoogleFirestoreConnectionManager.CreateNotebook(userID1, notebook1.Title, notebook1.Author, "", notebook1.PublishDate);
                if (!temp.Equals(notebook5))
                {
                    sw.WriteLine("FAILED: CreateNotebook(string userID, string title, string author, string publisher, string publishDate): CreateNotebook by Book Details publisher is empty test case.");
                    passed = false;
                }
                else
                {
                    notebookID6 = temp.Id;
                }
            }
            catch
            {
                sw.WriteLine("FAILED: CreateNotebook(string userID, string title, string author, string publisher, string publishDate): CreateNotebook by Book Details publisher is empty test case - unexpected exception.");
                passed = false;
            }

            //CreateNotebook publishDate is null - Book Details
            try
            {
                NotebookModel temp = GoogleFirestoreConnectionManager.CreateNotebook(userID1, notebook1.Title, notebook1.Author, notebook1.Publisher, null);
                if (!temp.Equals(notebook7))
                {
                    sw.WriteLine("FAILED: CreateNotebook(string userID, string title, string author, string publisher, string publishDate): CreateNotebook by Book Details publish date is null test case.");
                    passed = false;
                }
                else
                {
                    notebookID7 = temp.Id;
                }
            }
            catch
            {
                sw.WriteLine("FAILED: CreateNotebook(string userID, string title, string author, string publisher, string publishDate): CreateNotebook by Book Details publish date is null test case - unexpected exception.");
                passed = false;
            }

            return passed;
        }

        private bool Test_CreateNote(StreamWriter sw)
        {
            bool passed = true;

            //CreateNote all parameters normal
            try
            {
                NoteModel temp = GoogleFirestoreConnectionManager.CreateNote(userID1, notebookID1, noteText);
                if (!temp.Equals(note1))
                {
                    sw.WriteLine("FAILED: CreateNote(string userID, string notebookID, string noteText): CreateNote normal test case.");
                    passed = false;
                }
                else
                {
                    noteID1 = temp.Id;
                }
            }
            catch
            {
                sw.WriteLine("FAILED: CreateNote(string userID, string notebookID, string noteText): CreateNote normal test case - unexpected exception.");
                passed = false;
            }

            //CreateNote User does not exist
            try
            {
                GoogleFirestoreConnectionManager.CreateNote(userID1 + "NOTEXIST", notebookID1, noteText);
                sw.WriteLine("FAILED: CreateNote(string userID, string notebookID, string noteText): CreateNote User does not exist test case.");
                passed = false;
            }
            catch { }

            //CreateNote userID is null
            try
            {
                GoogleFirestoreConnectionManager.CreateNote(null, notebookID1, noteText);
                sw.WriteLine("FAILED: CreateNote(string userID, string notebookID, string noteText): CreateNote userID is null test case.");
                passed = false;
            }
            catch { }

            //CreateNote userID is empty
            try
            {
                GoogleFirestoreConnectionManager.CreateNote("", notebookID1, noteText);
                sw.WriteLine("FAILED: CreateNote(string userID, string notebookID, string noteText): CreateNote userID is empty test case.");
                passed = false;
            }
            catch { }

            //CreateNote Notebook does not exist
            try
            {
                GoogleFirestoreConnectionManager.CreateNote(userID1, notebookID1 + "NOTEXIST", noteText);
                sw.WriteLine("FAILED: CreateNote(string userID, string notebookID, string noteText): CreateNote Notebook does not exist test case.");
                passed = false;
            }
            catch { }

            //CreateNote notebookID is null
            try
            {
                GoogleFirestoreConnectionManager.CreateNote(userID1, null, noteText);
                sw.WriteLine("FAILED: CreateNote(string userID, string notebookID, string noteText): CreateNote notebookID is null test case.");
                passed = false;
            }
            catch { }

            //CreateNote notebookID is empty
            try
            {
                GoogleFirestoreConnectionManager.CreateNote(userID1, "", noteText);
                sw.WriteLine("FAILED: CreateNote(string userID, string notebookID, string noteText): CreateNote notebookID is empty test case.");
                passed = false;
            }
            catch { }

            //CreateNote noteText is null
            try
            {
                NoteModel temp = GoogleFirestoreConnectionManager.CreateNote(userID1, notebookID1, null);
                if (!temp.Equals(note2))
                {
                    sw.WriteLine("FAILED: CreateNote(string userID, string notebookID, string noteText): CreateNote noteText is null test case.");
                    passed = false;
                }
                else
                {
                    noteID2 = temp.Id;
                }
            }
            catch
            {
                sw.WriteLine("FAILED: CreateNote(string userID, string notebookID, string noteText): CreateNote noteText is null test case - unexpected exception.");
                passed = false;
            }

            //CreateNote noteText is empty
            try
            {
                NoteModel temp = GoogleFirestoreConnectionManager.CreateNote(userID1, notebookID1, "");
                if (!temp.Equals(note2))
                {
                    sw.WriteLine("FAILED: CreateNote(string userID, string notebookID, string noteText): CreateNote noteText is empty test case.");
                    passed = false;
                }
                else
                {
                    noteID3 = temp.Id;
                }
            }
            catch
            {
                sw.WriteLine("FAILED: CreateNote(string userID, string notebookID, string noteText): CreateNote noteText is empty test case- unexpected exception.");
                passed = false;
            }

            return passed;
        }

        private bool Test_GetNotebooks(StreamWriter sw)
        {
            bool passed = true;

            //GetNotebooks all parameters valid
            try
            {
                List<NotebookModel> compNotebooks = new List<NotebookModel>();
                compNotebooks.Add(notebook1);
                compNotebooks.Add(notebook2);
                compNotebooks.Add(notebook3);
                compNotebooks.Add(notebook3);
                compNotebooks.Add(notebook5);
                compNotebooks.Add(notebook5);
                compNotebooks.Add(notebook7);
                List<NotebookModel> tempNotebooks = GoogleFirestoreConnectionManager.GetNotebooks(userID1);
                if (tempNotebooks.Count != compNotebooks.Count)
                {
                    sw.WriteLine("FAILED: GetNotebooks(string userID): Normal test case, count mismatch.");
                    passed = false;
                }
                else
                {
                    foreach (NotebookModel n in tempNotebooks)
                    {
                        foreach (NotebookModel t in compNotebooks)
                        {
                            if (t.Equals(n))
                            {
                                compNotebooks.Remove(t);
                                break;
                            }
                        }
                    }

                    if (compNotebooks.Count > 0)
                    {
                        sw.WriteLine("FAILED: GetNotebooks(string userID): Normal test case, Notebooks not the same.");
                        passed = false;
                    }
                }
            }
            catch
            {
                sw.WriteLine("FAILED: GetNotebooks(string userID): Normal test case - unexpected exception.");
                passed = false;
            }

            //GetNotebooks User has no notebooks
            try
            {
                List<NotebookModel> tempNotebooks = GoogleFirestoreConnectionManager.GetNotebooks(userID2);
                if (tempNotebooks == null || tempNotebooks.Count != 0)
                {
                    sw.WriteLine("FAILED: GetNotebooks(string userID): User has no notebooks test case.");
                    passed = false;
                }
            }
            catch
            {
                sw.WriteLine("FAILED: GetNotebooks(string userID): User has no notebooks test case - unexpected exception.");
                passed = false;
            }

            //GetNotebooks User does not exist
            try
            {
                List<NotebookModel> tempNotebooks = GoogleFirestoreConnectionManager.GetNotebooks(userID1 + "NOTEXIST");
                if (tempNotebooks == null || tempNotebooks.Count != 0)
                {
                    sw.WriteLine("FAILED: GetNotebooks(string userID): User does not exist test case.");
                    passed = false;
                }
            }
            catch
            {
                sw.WriteLine("FAILED: GetNotebooks(string userID): User does not exist test case - unexpected exception.");
                passed = false;
            }

            //GetNotebooks userID is null
            try
            {
                GoogleFirestoreConnectionManager.GetNotebooks(null);
                sw.WriteLine("FAILED: GetNotebooks(string userID): User is null test case.");
                passed = false;
            }
            catch { }

            //GetNotebooks userID is empty
            try
            {
                GoogleFirestoreConnectionManager.GetNotebooks("");
                sw.WriteLine("FAILED: GetNotebooks(string userID): User is empty test case.");
                passed = false;
            }
            catch { }

            return passed;
        }

        private bool Test_GetNotes(StreamWriter sw)
        {
            bool passed = true;

            //GetNotes all parameters valid
            try
            {
                List<NoteModel> compNotes = new List<NoteModel>();
                compNotes.Add(note1);
                compNotes.Add(note2);
                compNotes.Add(note2);
                List<NoteModel> tempNotes = GoogleFirestoreConnectionManager.GetNotes(notebookID1);
                if (tempNotes.Count != compNotes.Count)
                {
                    sw.WriteLine("FAILED: GetNotes(string notebookID): Normal test case, count mismatch.");
                    passed = false;
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
                        sw.WriteLine("FAILED: GetNotes(string notebookID): Normal test case, Notes not the same.");
                        passed = false;
                    }
                }
            }
            catch
            {
                sw.WriteLine("FAILED: GetNotes(string notebookID): Normal test case - unexpected exception.");
                passed = false;
            }

            //GetNotes Notebook has no notes
            try
            {
                List<NoteModel> tempNotes = GoogleFirestoreConnectionManager.GetNotes(notebookID2);
                if (tempNotes == null || tempNotes.Count != 0)
                {
                    sw.WriteLine("FAILED: GetNotes(string notebookID): Notebook has no notes test case.");
                    passed = false;
                }
            }
            catch
            {
                sw.WriteLine("FAILED: GetNotes(string notebookID): Notebook has no notes test case - unexpected exception.");
                passed = false;
            }

            //GetNotes Notebook does not exist
            try
            {
                List<NoteModel> tempNotes = GoogleFirestoreConnectionManager.GetNotes(notebookID1 + "NOTEXIST");
                if (tempNotes == null || tempNotes.Count != 0)
                {
                    sw.WriteLine("FAILED: GetNotes(string notebookID): Notebook has no notes test case.");
                    passed = false;
                }
            }
            catch
            {
                sw.WriteLine("FAILED: GetNotes(string notebookID): Notebook has no notes test case - unexpected exception.");
                passed = false;
            }

            //GetNotes notebookID is null
            try
            {
                GoogleFirestoreConnectionManager.GetNotes(null);
                sw.WriteLine("FAILED: GetNotes(string notebookID): Notebook is null test case.");
                passed = false;
            }
            catch { }

            //GetNotes notebookID is empty
            try
            {
                GoogleFirestoreConnectionManager.GetNotes("");
                sw.WriteLine("FAILED: GetNotes(string notebookID): Notebook is empty test case.");
                passed = false;
            }
            catch { }

            return passed;
        }

        private bool Test_GetUserNotes(StreamWriter sw)
        {
            bool passed = true;

            //GetUserNotes all parameters valid
            try
            {
                List<NoteModel> compNotes = new List<NoteModel>();
                compNotes.Add(note1);
                compNotes.Add(note2);
                compNotes.Add(note2);
                List<NoteModel> tempNotes = GoogleFirestoreConnectionManager.GetUserNotes(userID1);
                if (tempNotes.Count != compNotes.Count)
                {
                    sw.WriteLine("FAILED: GetUserNotes(string userID): Normal test case, count mismatch.");
                    passed = false;
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
                        passed = false;
                    }
                }
            }
            catch
            {
                sw.WriteLine("FAILED: GetUserNotes(string userID): Normal test case - unexpected exception.");
                passed = false;
            }

            //GetUserNotes User has no notes
            try
            {
                List<NoteModel> tempNotes = GoogleFirestoreConnectionManager.GetUserNotes(userID2);
                if (tempNotes == null || tempNotes.Count != 0)
                {
                    sw.WriteLine("FAILED: GetNotes(string userID): User has no notes test case.");
                    passed = false;
                }
            }
            catch
            {
                sw.WriteLine("FAILED: GetNotes(string userID): User has no notes test case - unexpected exception.");
                passed = false;
            }

            //GetUserNotes User does not exist
            try
            {
                List<NoteModel> tempNotes = GoogleFirestoreConnectionManager.GetUserNotes(userID1 + "NOTEXIST");
                if (tempNotes == null || tempNotes.Count != 0)
                {
                    sw.WriteLine("FAILED: GetUserNotes(string userID): User has no notes test case.");
                    passed = false;
                }
            }
            catch
            {
                sw.WriteLine("FAILED: GetUserNotes(string userID): User has no notes test case - unexpected exception.");
                passed = false;
            }

            //GetUserNotes userID is null
            try
            {
                GoogleFirestoreConnectionManager.GetUserNotes(null);
                sw.WriteLine("FAILED: GetUserNotes(string userID): userID is null test case.");
                passed = false;
            }
            catch { }

            //GetNotes userID is empty
            try
            {
                GoogleFirestoreConnectionManager.GetUserNotes("");
                sw.WriteLine("FAILED: GetUserNotes(string userID): userID is empty test case.");
                passed = false;
            }
            catch { }

            return passed;
        }

        private bool Test_GetNote(StreamWriter sw)
        {
            bool passed = true;

            //GetNote all parameters valid
            try
            {
                if (!GoogleFirestoreConnectionManager.GetNote(noteID1).Equals(note1))
                {
                    sw.WriteLine("FAILED: GetNote(string noteID): Normal test case.");
                    passed = false;
                }
            }
            catch
            {
                sw.WriteLine("FAILED: GetNote(string noteID): Normal test case - unexpected exception.");
                passed = false;
            }

            //GetNote note does not exist
            try
            {
                if (GoogleFirestoreConnectionManager.GetNote(noteID1 + "NOTEXIST") != null)
                {
                    sw.WriteLine("FAILED: GetNote(string noteID): Note does not exist test case.");
                    passed = false;
                }
            }
            catch
            {
                sw.WriteLine("FAILED: GetNote(string noteID): Note does not exist test case - unexpected exception.");
                passed = false;
            }

            //GetNote noteID is null
            try
            {
                GoogleFirestoreConnectionManager.GetNote(null);
                sw.WriteLine("FAILED: GetNote(string noteID): noteID is null test case.");
                passed = false;
            }
            catch { }

            //GetNote noteID is empty
            try
            {
                GoogleFirestoreConnectionManager.GetNote("");
                sw.WriteLine("FAILED: GetNote(string noteID): noteID is empty test case.");
                passed = false;
            }
            catch { }

            return passed;
        }

        private bool Test_UpdateNote(StreamWriter sw)
        {
            bool passed = true;

            //UpdateNote all parameters valid
            try
            {
                if (!GoogleFirestoreConnectionManager.UpdateNote(noteID1, noteTextUpdated))
                {
                    sw.WriteLine("FAILED: UpdateNote(string noteID, string noteText): Normal test case.");
                    passed = false;
                }
                else if (GoogleFirestoreConnectionManager.GetNote(noteID1).Text != noteTextUpdated)
                {
                    sw.WriteLine("FAILED: UpdateNote(string noteID, string noteText): Normal test case, note not updated.");
                    passed = false;

                }
            }
            catch
            {
                sw.WriteLine("FAILED: UpdateNote(string noteID, string noteText): Normal test case - unexpected exception.");
                passed = false;
            }

            //UpdatedNote note does not exist
            try
            {
                GoogleFirestoreConnectionManager.UpdateNote(noteID1 + "NOTEXIST", noteTextUpdated);
                sw.WriteLine("FAILED: UpdateNote(string noteID, string noteText): User does not exist.");
                passed = false;
            }
            catch { }

            //UpdateNote noteID is null
            try
            {
                GoogleFirestoreConnectionManager.UpdateNote(null, noteTextUpdated);
                sw.WriteLine("FAILED: UpdateNote(string noteID, string noteText): noteID is null test case.");
                passed = false;
            }
            catch { }

            //UpdateNote noteID is empty
            try
            {
                GoogleFirestoreConnectionManager.UpdateNote("", noteTextUpdated);
                sw.WriteLine("FAILED: UpdateNote(string noteID, string noteText): noteID is empty test case.");
                passed = false;
            }
            catch { }

            //UpdateNote noteText is null
            try
            {
                if (!GoogleFirestoreConnectionManager.UpdateNote(noteID1, null))
                {
                    sw.WriteLine("FAILED: UpdateNote(string noteID, string noteText): noteText is null.");
                    passed = false;
                }
                else if (GoogleFirestoreConnectionManager.GetNote(noteID1).Text != "")
                {
                    sw.WriteLine("FAILED: UpdateNote(string noteID, string noteText): noteText is null, note not updated.");
                    passed = false;
                }
            }
            catch
            {
                sw.WriteLine("FAILED: UpdateNote(string noteID, string noteText): noteText is null - unexpected exception.");
                passed = false;
            }

            //UpdateNote noteText is empty
            try
            {
                if (!GoogleFirestoreConnectionManager.UpdateNote(noteID1, ""))
                {
                    sw.WriteLine("FAILED: UpdateNote(string noteID, string noteText): noteText is empty.");
                    passed = false;
                }
                else if (GoogleFirestoreConnectionManager.GetNote(noteID1).Text != "")
                {
                    sw.WriteLine("FAILED: UpdateNote(string noteID, string noteText): noteText is empty, note not updated.");
                    passed = false;
                }
            }
            catch
            {
                sw.WriteLine("FAILED: UpdateNote(string noteID, string noteText): noteText is empty - unexpected exception.");
                passed = false;
            }

            return passed;
        }

        private bool Test_DeleteNote(StreamWriter sw)
        {
            bool passed = true;

            //DeleteNote all parameters valid 1
            try
            {
                if (!GoogleFirestoreConnectionManager.DeleteNote(noteID1))
                {
                    sw.WriteLine("FAILED: DeleteNote(string noteID): Normal test case 1.");
                    passed = false;
                }
                else if (GoogleFirestoreConnectionManager.GetNote(noteID1) != null)
                {
                    sw.WriteLine("FAILED: DeleteNote(string noteID, string noteText): Normal test case 1, note not deleted.");
                    passed = false;
                }
            }
            catch
            {
                sw.WriteLine("FAILED: DeleteNote(string noteID): Normal test case 1 - unexpected exception.");
                passed = false;
            }

            //DeleteNote all parameters valid 2
            try
            {
                if (!GoogleFirestoreConnectionManager.DeleteNote(noteID2))
                {
                    sw.WriteLine("FAILED: DeleteNote(string noteID): Normal test case 2.");
                    passed = false;
                }
                else if (GoogleFirestoreConnectionManager.GetNote(noteID2) != null)
                {
                    sw.WriteLine("FAILED: DeleteNote(string noteID, string noteText): Normal test case 2, note not deleted.");
                    passed = false;
                }
            }
            catch
            {
                sw.WriteLine("FAILED: DeleteNote(string noteID, string noteText): Normal test case 2 - unexpected exception.");
                passed = false;
            }

            //DeleteNote all parameters valid 3
            try
            {
                if (!GoogleFirestoreConnectionManager.DeleteNote(noteID3))
                {
                    sw.WriteLine("FAILED: DeleteNote(string noteID): Normal test case 3.");
                    passed = false;
                }
                else if (GoogleFirestoreConnectionManager.GetNote(noteID3) != null)
                {
                    sw.WriteLine("FAILED: DeleteNote(string noteID, string noteText): Normal test case 3, note not deleted.");
                    passed = false;
                }
            }
            catch
            {
                sw.WriteLine("FAILED: DeleteNote(string noteID): Normal test case 3 - unexpected exception.");
                passed = false;
            }

            //DeleteNote validate notebook1 has no notes
            try
            {
                List<NoteModel> temp = GoogleFirestoreConnectionManager.GetNotes(notebookID1);
                if (temp == null || temp.Count != 0)
                {
                    sw.WriteLine("FAILED: DeleteNote(string noteID): Delete 1-3 failed - Notes still exist.");
                    passed = false;
                }
            }
            catch
            {
                sw.WriteLine("FAILED: DeleteNote(string noteID): Delete 1-3 failed - unexpected exception.");
                passed = false;
            }

            //DeleteNote note does not exist
            try
            {
                if (!GoogleFirestoreConnectionManager.DeleteNote(noteID1 + "NOTEXIST"))
                {
                    sw.WriteLine("FAILED: DeleteNote(string noteID): Note does not exist test case.");
                    passed = false;
                }
            }
            catch
            {
                sw.WriteLine("FAILED: DeleteNote(string noteID): Note does not exist test case - unexpected exception.");
                passed = false;
            }

            //DeleteNote noteID is null
            try
            {
                GoogleFirestoreConnectionManager.DeleteNote(null);
                sw.WriteLine("FAILED: DeleteNote(string noteID): noteID is null test case.");
                passed = false;
            }
            catch { }

            //DeleteNote noteID is empty
            try
            {
                GoogleFirestoreConnectionManager.DeleteNote("");
                sw.WriteLine("FAILED: DeleteNote(string noteID): noteID is empty test case.");
                passed = false;
            }
            catch { }

            return passed;
        }

        private bool Test_DeleteNotebook(StreamWriter sw)
        {
            bool passed = true;

            //DeleteNotebook all parameters valid 1
            try
            {
                if (!GoogleFirestoreConnectionManager.DeleteNotebook(notebookID1))
                {
                    sw.WriteLine("FAILED: DeleteNotebook(string notebookID): Normal test case 1.");
                    passed = false;
                }
            }
            catch
            {
                sw.WriteLine("FAILED: DeleteNotebook(string notebookID): Normal test case 1 - unexpected exception.");
                passed = false;
            }

            //DeleteNotebook all parameters valid 2
            try
            {
                if (!GoogleFirestoreConnectionManager.DeleteNotebook(notebookID2))
                {
                    sw.WriteLine("FAILED: DeleteNotebook(string notebookID): Normal test case 2.");
                    passed = false;
                }
            }
            catch
            {
                sw.WriteLine("FAILED: DeleteNotebook(string notebookID): Normal test case 2 - unexpected exception.");
                passed = false;
            }

            //DeleteNotebook all parameters valid 3
            try
            {
                if (!GoogleFirestoreConnectionManager.DeleteNotebook(notebookID3))
                {
                    sw.WriteLine("FAILED: DeleteNotebook(string notebookID): Normal test case 3.");
                    passed = false;
                }
            }
            catch
            {
                sw.WriteLine("FAILED: DeleteNotebook(string notebookID): Normal test case 3 - unexpected exception.");
                passed = false;
            }

            //DeleteNotebook all parameters valid 4
            try
            {
                if (!GoogleFirestoreConnectionManager.DeleteNotebook(notebookID4))
                {
                    sw.WriteLine("FAILED: DeleteNotebook(string notebookID): Normal test case 4.");
                    passed = false;
                }
            }
            catch
            {
                sw.WriteLine("FAILED: DeleteNotebook(string notebookID): Normal test case 4 - unexpected exception.");
                passed = false;
            }

            //DeleteNotebook all parameters valid 5
            try
            {
                if (!GoogleFirestoreConnectionManager.DeleteNotebook(notebookID5))
                {
                    sw.WriteLine("FAILED: DeleteNotebook(string notebookID): Normal test case 5.");
                    passed = false;
                }
            }
            catch
            {
                sw.WriteLine("FAILED: DeleteNotebook(string notebookID): Normal test case 5 - unexpected exception.");
                passed = false;
            }

            //DeleteNotebook all parameters valid 6
            try
            {
                if (!GoogleFirestoreConnectionManager.DeleteNotebook(notebookID6))
                {
                    sw.WriteLine("FAILED: DeleteNotebook(string notebookID): Normal test case 6.");
                    passed = false;
                }
            }
            catch
            {
                sw.WriteLine("FAILED: DeleteNotebook(string notebookID): Normal test case 6 - unexpected exception.");
                passed = false;
            }

            //DeleteNotebook all parameters valid 7
            try
            {
                if (!GoogleFirestoreConnectionManager.DeleteNotebook(notebookID7))
                {
                    sw.WriteLine("FAILED: DeleteNotebook(string notebookID): Normal test case 7.");
                    passed = false;
                }
            }
            catch
            {
                sw.WriteLine("FAILED: DeleteNotebook(string notebookID): Normal test case 7 - unexpected exception.");
                passed = false;
            }

            //DeleteNotebook validate user1 has no notebooks
            try
            {
                List<NotebookModel> temp = GoogleFirestoreConnectionManager.GetNotebooks(userID1);
                if (temp == null || temp.Count != 0)
                {
                    sw.WriteLine("FAILED: DeleteNotebook(string notebookID): Delete 1-7 failed - Notebooks still exist.");
                    passed = false;
                }
            }
            catch
            {
                sw.WriteLine("FAILED: DeleteNotebook(string notebookID): Delete 1-7 failed - Notebooks still exist - unexpected exception.");
                passed = false;
            }

            //DeleteNotebook delete notebook with notes
            try
            {
                notebookID1 = GoogleFirestoreConnectionManager.CreateNotebook(userID1, isbn1).ID;
                noteID1 = GoogleFirestoreConnectionManager.CreateNote(notebookID1, noteText).ID;
                GoogleFirestoreConnectionManager.DeleteNotebook(notebookID1);
                List<NoteModel> tempNotes = GoogleFirestoreConnectionManager.GetNotes(notebookID1);
                if (tempNotes == null || tempNotes.Count != 0)
                {
                    sw.WriteLine("FAILED: DeleteNotebook(string notebookID): Delete notebook with notes");
                    passed = false;
                    GoogleFirestoreConnectionManager.DeleteNote(noteID1);
                }
            }
            catch
            {
                sw.WriteLine("FAILED: DeleteNotebook(string notebookID): Delete notebook with notes - unexpected exception.");
                passed = false;
            }

            //DeleteNotebook notebook does not exist
            try
            {
                if (!GoogleFirestoreConnectionManager.DeleteNotebook(notebookID1 + "NOTEXIST"))
                {
                    sw.WriteLine("FAILED: DeleteNotebook(string notebookID): Notebook does not exist test case.");
                    passed = false;
                }
            }
            catch
            {
                sw.WriteLine("FAILED: DeleteNotebook(string notebookID): Notebook does not exist test case - unexpected exception.");
                passed = false;
            }

            //DeleteNote notebookID is null
            try
            {
                GoogleFirestoreConnectionManager.DeleteNote(null);
                sw.WriteLine("FAILED: DeleteNote(string noteID): noteID is null test case.");
                passed = false;
            }
            catch { }

            //DeleteNote notebookID is empty
            try
            {
                GoogleFirestoreConnectionManager.DeleteNotebook("");
                sw.WriteLine("FAILED: DeleteNotebook(string notebookID): notebookID is empty test case.");
                passed = false;
            }
            catch { }

            return passed;
        }

        private bool Test_DeleteUser(StreamWriter sw)
        {
            bool passed = true;

            //DeleteUser all parameters valid
            try
            {
                if (!GoogleFirestoreConnectionManager.DeleteUser(userID1))
                {
                    sw.WriteLine("FAILED: DeleteUser(string userID): Normal test case.");
                    passed = false;
                }
                else
                {
                    try
                    {
                        GoogleFirestoreConnectionManager.GetDisplayName(userID1);
                        sw.WriteLine("FAILED: DeleteUser(string userID): Normal test case - User still exists.");
                        passed = false;
                    }
                    catch { }
                }
            }
            catch
            {
                sw.WriteLine("FAILED: DeleteUser(string userID): Normal test case - unexpected exception.");
                passed = false;
            }

            //DeleteUser delete user with Notebooks
            try
            {
                notebookID1 = GoogleFirestoreConnectionManager.CreateNotebook(userID2, isbn1);
                GoogleFirestoreConnectionManager.DeleteUser(userID2);
                List<NotebookModel> temp = GoogleFirestoreConnectionManager.GetNotebooks(userID2);
                if (temp == null || temp.Count != 0)
                {
                    sw.WriteLine("FAILED: DeleteUser(string userID): delete user with Notebooks.");
                    passed = false;
                }
            }
            catch
            {
                sw.WriteLine("FAILED: DeleteUser(string userID): delete user with Notebooks - unexpected exception.");
                passed = false;
            }

            //DeleteUser user does not exist
            try
            {
                if (!GoogleFirestoreConnectionManager.DeleteUser(userID1 + "NOTEXIST"))
                {
                    sw.WriteLine("FAILED: DeleteUser(string userID): User does not exist test case.");
                    passed = false;
                }
            }
            catch
            {
                sw.WriteLine("FAILED: DeleteUser(string userID): User does not exist test case - unexpected exception.");
                passed = false;
            }

            //DeleteUser userID is null
            try
            {
                GoogleFirestoreConnectionManager.DeleteUser(null);
                sw.WriteLine("FAILED: DeleteUser(string userID): userID is null test case.");
                passed = false;
            }
            catch { }

            //DeleteUser userID is empty
            try
            {
                GoogleFirestoreConnectionManager.DeleteNotebook("");
                sw.WriteLine("FAILED: DeleteUser(string userID): userID is empty test case.");
                passed = false;
            }
            catch { }

            return passed;
        }
    }
}
