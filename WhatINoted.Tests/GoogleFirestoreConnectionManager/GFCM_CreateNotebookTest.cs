using System;
using System.Collections.Generic;
using System.IO;

namespace WhatINoted.Tests.GoogleFirestoreConnectionManager
{
    public class GFCM_CreateNotebookTest : GFCM_Test
    {
        public override bool Run(StreamWriter sw)
        {
            bool passed = true;
            passed = NormalCreateNotebookByIsbnRequest(sw) && passed;
            passed = NormalCreateNotebookByDetailsRequest(sw) && passed;
            passed = CreateNotebookByIsbnUserDoesNotExist(sw) && passed;
            passed = CreateNotebookByDetailsUserDoesNotExist(sw) && passed;
            passed = CreateNotebookByIsbnUserIdIsNull(sw) && passed;
            passed = CreateNotebookByDetailsUserIdIsNull(sw) && passed;
            passed = CreateNotebookByIsbnUserIdIsEmpty(sw) && passed;
            passed = CreateNotebookByDetailsUserIdIsEmpty(sw) && passed;
            passed = CreateNotebookByIsbnIsbnIsNull(sw) && passed;
            passed = CreateNotebookByIsbnIsbnIsEmpty(sw) && passed;
            passed = CreateNotebookByIsbnIsbnHasAlphabeticCharacters(sw) && passed;
            passed = CreateNotebookByIsbnIsbnIsInvalid(sw) && passed;
            passed = CreateNotebookByDetailsTitleIsNull(sw) && passed;
            passed = CreateNotebookByDetailsTitleIsEmpty(sw) && passed;
            passed = CreateNotebookByDetailsAuthorIsNull(sw) && passed;
            passed = CreateNotebookByDetailsAuthorIsEmpty(sw) && passed;
            passed = CreateNotebookByDetailsPublisherIsNull(sw) && passed;
            passed = CreateNotebookByDetailsPublisherIsEmpty(sw) && passed;
            passed = CreateNotebookByDetailsPublishDateIsNull(sw) && passed;
            return passed;
        }

        private bool NormalCreateNotebookByIsbnRequest(StreamWriter sw) {
            try
            {
                Models.NotebookModel temp = GoogleFirestoreConnectionManager.CreateNotebook(userID1, isbn1);
                if (!temp.Equals(notebook1))
                {
                    sw.WriteLine("FAILED: CreateNotebook(string userID, string isbn): Normal Create Notebook by ISBN request.");
                    if (temp != null) {
                        GoogleFirestoreConnectionManager.DeleteNotebook(temp.Id);
                    }
                    return false;
                }
                else
                {
                    notebookID1 = temp.Id; //stored for deleting later (auto-generated)
                    GoogleFirestoreConnectionManager.DeleteNotebook(notebookID1);
                }
            }
            catch
            {
                sw.WriteLine("FAILED: CreateNotebook(string userID, string isbn): Normal Create Notebook by ISBN request - unexpected exception.");
                return false;
            }
            return true;
        }

        private bool NormalCreateNotebookByDetailsRequest(StreamWriter sw) {
            try
            {
                Models.NotebookModel temp = GoogleFirestoreConnectionManager.CreateNotebook(userID1, notebook1.Title, notebook1.Author, notebook1.Publisher, notebook1.PublicationDate);
                if (!temp.Equals(notebook2))
                {
                    sw.WriteLine("FAILED: CreateNotebook(string userID, string title, string author, string publisher, DateTime publishDate): Normal Create Notebook by Book Details request.");
                    if (temp != null) {
                        GoogleFirestoreConnectionManager.DeleteNotebook(temp.Id);
                    }
                    return false;
                }
                else
                {
                    notebookID2 = temp.Id;
                    GoogleFirestoreConnectionManager.DeleteNotebook(temp.Id);
                }
            }
            catch
            {
                sw.WriteLine("FAILED: CreateNotebook(string userID, string title, string author, string publisher, DateTime publishDate): Normal Create Notebook by Book Details request - unexpected exception.");
                return false;
            }
        }

        private bool CreateNotebookByIsbnUserDoesNotExist(StreamWriter sw) {
            //CreateNotebook User does not exist - ISBN
            try
            {
                GoogleFirestoreConnectionManager.CreateNotebook(userID1 + "NOTEXIST", isbn1);
                sw.WriteLine("FAILED: CreateNotebook(string userID, string isbn): CreateNotebook by ISBN User does not exist test case.");
                return false;
            }
            catch { return true; }
        }

        private bool CreateNotebookByDetailsUserDoesNotExist(StreamWriter sw) {
            try
            {
                GoogleFirestoreConnectionManager.CreateNotebook(userID1 + "NOTEXIST", notebook1.Title, notebook1.Author, notebook1.Publisher, notebook1.PublicationDate);
                sw.WriteLine("FAILED: CreateNotebook(string userID, string isbn): CreateNotebook by Book Details User does not exist test case.");
                return false;
            }
            catch { return true; }
        }

        private bool CreateNotebookByIsbnUserIdIsNull(StreamWriter sw) {
            try
            {
                GoogleFirestoreConnectionManager.CreateNotebook(null, isbn1);
                sw.WriteLine("FAILED: CreateNotebook(string userID, string isbn): CreateNotebook by ISBN userID is null test case.");
                return false;
            }
            catch { return true; }
        }

        private bool CreateNotebookByDetailsUserIdIsNull(StreamWriter sw) {
            try
            {
                GoogleFirestoreConnectionManager.CreateNotebook(null, notebook1.Title, notebook1.Author, notebook1.Publisher, notebook1.PublicationDate);
                sw.WriteLine("FAILED: CreateNotebook(string userID, string title, string author, string publisher, string publishDate): CreateNotebook by Book Details userID is null test case.");
                return false;
            }
            catch { return true; }
        }

        private bool CreateNotebookByIsbnUserIdIsEmpty(StreamWriter sw) {
            try
            {
                GoogleFirestoreConnectionManager.CreateNotebook("", isbn1);
                sw.WriteLine("FAILED: CreateNotebook(string userID, string isbn): CreateNotebook by ISBN userID is empty test case.");
                return false;
            }
            catch { return true; }
        }

        private bool CreateNotebookByDetailsUserIdIsEmpty(StreamWriter sw) {
            try
            {
                GoogleFirestoreConnectionManager.CreateNotebook("", notebook1.Title, notebook1.Author, notebook1.Publisher, notebook1.PublicationDate);
                sw.WriteLine("FAILED: CreateNotebook(string userID, string title, string author, string publisher, string publishDate): CreateNotebook by Book Details userID is empty test case.");
                return false;
            }
            catch { return true; }
        }

        private bool CreateNotebookByIsbnIsbnIsNull(StreamWriter sw) {
            try
            {
                GoogleFirestoreConnectionManager.CreateNotebook(userID1, null);
                sw.WriteLine("FAILED: CreateNotebook(string userID, string isbn): CreateNotebook by ISBN isbn is null test case.");
                return false;
            }
            catch { return true; }
        }

        private bool CreateNotebookByIsbnIsbnIsEmpty(StreamWriter sw) {
            try
            {
                GoogleFirestoreConnectionManager.CreateNotebook(userID1, "");
                sw.WriteLine("FAILED: CreateNotebook(string userID, string isbn): CreateNotebook by ISBN isbn is empty test case.");
                return false;
            }
            catch { return true; }
        }

        private bool CreateNotebookByIsbnIsbnHasAlphabeticCharacters(StreamWriter sw) {
            try
            {
                GoogleFirestoreConnectionManager.CreateNotebook(userID1, "01234Invalid5");
                sw.WriteLine("FAILED: CreateNotebook(string userID, string isbn): CreateNotebook by ISBN isbn is invalid test case 1.");
                return false;
            }
            catch { return true; }
        }

        private bool CreateNotebookByIsbnIsbnIsInvalid(StreamWriter sw) {
            try
            {
                GoogleFirestoreConnectionManager.CreateNotebook(userID1, "012345");
                sw.WriteLine("FAILED: CreateNotebook(string userID, string isbn): CreateNotebook by ISBN isbn is invalid test case 2.");
                return false;
            }
            catch { return true; }
        }

        private bool CreateNotebookByDetailsTitleIsNull(StreamWriter sw) {
            try
            {
                GoogleFirestoreConnectionManager.CreateNotebook(userID1, null, notebook1.Author, notebook1.Publisher, notebook1.PublicationDate);
                sw.WriteLine("FAILED: CreateNotebook(string userID, string title, string author, string publisher, string publishDate): CreateNotebook by Book Details title is null test case.");
                return false;
            }
            catch { return true; }
        }

        private bool CreateNotebookByDetailsTitleIsEmpty(StreamWriter sw) {
            try
            {
                GoogleFirestoreConnectionManager.CreateNotebook(userID1, "", notebook1.Author, notebook1.Publisher, notebook1.PublicationDate);
                sw.WriteLine("FAILED: CreateNotebook(string userID, string title, string author, string publisher, string publishDate): CreateNotebook by Book Details title is empty test case.");
                return false;
            }
            catch { return true; }
        }

        private bool CreateNotebookByDetailsAuthorIsNull(StreamWriter sw) {
            try
            {
                Models.NotebookModel temp = GoogleFirestoreConnectionManager.CreateNotebook(userID1, notebook1.Title, null, notebook1.Publisher, notebook1.PublicationDate);
                if (!temp.Equals(notebook3))
                {
                    sw.WriteLine("FAILED: CreateNotebook(string userID, string title, string author, string publisher, string publishDate): CreateNotebook by Book Details author is null test case.");
                    if (temp != null) {
                        GoogleFirestoreConnectionManager.DeleteNotebook(temp.Id);
                    }
                    return false;
                }
                else
                {
                    notebookID3 = temp.Id;
                    GoogleFirestoreConnectionManager.DeleteNotebook(notebookID3);
                }
            }
            catch
            {
                sw.WriteLine("FAILED: CreateNotebook(string userID, string title, string author, string publisher, string publishDate): CreateNotebook by Book Details author is null test case.");
                return false;
            }
            return true;
        }

        private bool CreateNotebookByDetailsAuthorIsEmpty(StreamWriter sw) {
            try
            {
                Models.NotebookModel temp = GoogleFirestoreConnectionManager.CreateNotebook(userID1, notebook1.Title, "", notebook1.Publisher, notebook1.PublicationDate);
                if (!temp.Equals(notebook3))
                {
                    sw.WriteLine("FAILED: CreateNotebook(string userID, string title, string author, string publisher, string publishDate): CreateNotebook by Book Details author is empty test case.");
                    if (temp != null)
                    {
                        GoogleFirestoreConnectionManager.DeleteNotebook(temp.Id);
                    }
                    return false;
                }
                else
                {
                    notebookID4 = temp.Id;
                    GoogleFirestoreConnectionManager.DeleteNotebook(notebookID4);
                }
            }
            catch
            {
                sw.WriteLine("FAILED: CreateNotebook(string userID, string title, string author, string publisher, string publishDate): CreateNotebook by Book Details author is empty test case - unexpected exception.");
                return false;
            }
            return true;
        }

        private bool CreateNotebookByDetailsPublisherIsNull(StreamWriter sw) {
            try
            {
                Models.NotebookModel temp = GoogleFirestoreConnectionManager.CreateNotebook(userID1, notebook1.Title, notebook1.Author, null, notebook1.PublicationDate);
                if (!temp.Equals(notebook5))
                {
                    sw.WriteLine("FAILED: CreateNotebook(string userID, string title, string author, string publisher, string publishDate): CreateNotebook by Book Details publisher is null test case.");
                    if (temp != null)
                    {
                        GoogleFirestoreConnectionManager.DeleteNotebook(temp.Id);
                    }
                    return false;
                }
                else
                {
                    notebookID5 = temp.Id;
                    GoogleFirestoreConnectionManager.DeleteNotebook(notebookID5);
                }
            }
            catch
            {
                sw.WriteLine("FAILED: CreateNotebook(string userID, string title, string author, string publisher, string publishDate): CreateNotebook by Book Details publisher is null test case - unexpected exception.");
                return false;
            }
            return true;
        }

        private bool CreateNotebookByDetailsPublisherIsEmpty(StreamWriter sw) {
            try
            {
                Models.NotebookModel temp = GoogleFirestoreConnectionManager.CreateNotebook(userID1, notebook1.Title, notebook1.Author, "", notebook1.PublicationDate);
                if (!temp.Equals(notebook5))
                {
                    sw.WriteLine("FAILED: CreateNotebook(string userID, string title, string author, string publisher, string publishDate): CreateNotebook by Book Details publisher is empty test case.");
                    if (temp != null)
                    {
                        GoogleFirestoreConnectionManager.DeleteNotebook(temp.Id);
                    }
                    return false;
                }
                else
                {
                    notebookID6 = temp.Id;
                    GoogleFirestoreConnectionManager.DeleteNotebook(notebookID6);
                }
            }
            catch
            {
                sw.WriteLine("FAILED: CreateNotebook(string userID, string title, string author, string publisher, string publishDate): CreateNotebook by Book Details publisher is empty test case - unexpected exception.");
                return false;
            }
            return true;
        }

        private bool CreateNotebookByDetailsPublishDateIsNull(StreamWriter sw) {
            try
            {
                Models.NotebookModel temp = GoogleFirestoreConnectionManager.CreateNotebook(userID1, notebook1.Title, notebook1.Author, notebook1.Publisher, default(DateTime));
                if (!temp.Equals(notebook7))
                {
                    sw.WriteLine("FAILED: CreateNotebook(string userID, string title, string author, string publisher, string publishDate): CreateNotebook by Book Details publish date is null test case.");
                    if (temp != null)
                    {
                        GoogleFirestoreConnectionManager.DeleteNotebook(temp.Id);
                    }
                    return false;
                }
                else
                {
                    notebookID7 = temp.Id;
                    GoogleFirestoreConnectionManager.DeleteNotebook(notebookID7);
                }
            }
            catch
            {
                sw.WriteLine("FAILED: CreateNotebook(string userID, string title, string author, string publisher, string publishDate): CreateNotebook by Book Details publish date is null test case - unexpected exception.");
                return false;
            }
            return true;
        }
    }
}
