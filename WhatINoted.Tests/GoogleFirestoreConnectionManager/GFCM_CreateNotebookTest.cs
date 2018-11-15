using System;
using System.IO;
using WhatINoted.ConnectionManagers;
using WhatINoted.Models;
using static WhatINoted.ConnectionManagers.GoogleFirestoreConnectionManager;

namespace WhatINoted.Tests.GoogleFirestoreConnectionManagerTests
{
    public class GFCM_CreateNotebookTest : GFCM_Test
    {
        public override bool Run(StreamWriter sw)
        {
            bool passed = true;
            passed = CreateNotebookByDetails(sw) && passed;
            passed = CreateNotebookByDetailsUserDoesNotExist(sw) && passed;
            passed = CreateNotebookByDetailsUserIdIsNull(sw) && passed;
            passed = CreateNotebookByDetailsUserIdIsEmpty(sw) && passed;
            passed = CreateNotebookByDetailsTitleIsNull(sw) && passed;
            passed = CreateNotebookByDetailsTitleIsEmpty(sw) && passed;
            passed = CreateNotebookByDetailsAuthorIsNull(sw) && passed;
            passed = CreateNotebookByDetailsPublisherIsNull(sw) && passed;
            return passed;
        }

        private void Setup()
        {
            notebook1 = new Notebook(userID1, title1, author1, isbn1, publisher1, publishDate1, coverURL1, DateTime.Now, DateTime.Now);
        }

        private bool CreateNotebookByDetails(StreamWriter sw)
        {
            Setup();
            try
            {
                GoogleFirestoreConnectionManager.HandleLogin(userID1, displayName1, email1);
                Notebook createdNotebook = CreateNotebook(userID1, notebook1.Title, notebook1.Author, notebook1.Isbn, notebook1.Publisher, notebook1.PublishDate, notebook1.CoverURL);
                if (!notebook1.Equals(createdNotebook))
                {
                    sw.WriteLine("FAILED: CreateNotebookByDetails");
                    if (createdNotebook != null)
                    {
                        DeleteNotebook(createdNotebook.ID);
                    }
                    return false;
                }
                else
                {
                    DeleteNotebook(createdNotebook.ID);
                }
            }
            catch
            {
                sw.WriteLine("FAILED: CreateNotebookByDetails: Unexpected exception");
                return false;
            }
            return true;
        }

        private bool CreateNotebookByDetailsUserDoesNotExist(StreamWriter sw)
        {
            try
            {
                Models.Notebook temp = CreateNotebook(userID1 + "NOTEXIST", notebook1.Title, notebook1.Author, notebook1.Isbn, notebook1.Publisher, notebook1.PublishDate, notebook1.CoverURL);
                sw.WriteLine("FAILED: CreateNotebook(string userID, string isbn): CreateNotebook by Book Details User does not exist test case.");
                DeleteNotebook(temp.ID);
                return false;
            }
            catch { return true; }
        }

        private bool CreateNotebookByDetailsUserIdIsNull(StreamWriter sw)
        {
            try
            {
                Models.Notebook temp = CreateNotebook(null, notebook1.Title, notebook1.Author, notebook1.Isbn, notebook1.Publisher, notebook1.PublishDate, notebook1.CoverURL);
                sw.WriteLine("FAILED: CreateNotebook(string userID, string title, string author, string publisher, string publishDate): CreateNotebook by Book Details userID is null test case.");
                DeleteNotebook(temp.ID);
                return false;
            }
            catch { return true; }
        }

        private bool CreateNotebookByDetailsUserIdIsEmpty(StreamWriter sw)
        {
            try
            {
                Models.Notebook temp = CreateNotebook("", notebook1.Title, notebook1.Author, notebook1.Isbn, notebook1.Publisher, notebook1.PublishDate, notebook1.CoverURL);
                sw.WriteLine("FAILED: CreateNotebook(string userID, string title, string author, string publisher, string publishDate): CreateNotebook by Book Details userID is empty test case.");
                DeleteNotebook(temp.ID);
                return false;
            }
            catch { return true; }
        }

        private bool CreateNotebookByDetailsTitleIsNull(StreamWriter sw)
        {
            try
            {
                Models.Notebook temp = CreateNotebook(userID1, null, notebook1.Author, notebook1.Isbn, notebook1.Publisher, notebook1.PublishDate, notebook1.CoverURL);
                sw.WriteLine("FAILED: CreateNotebook(string userID, string title, string author, string publisher, string publishDate): CreateNotebook by Book Details title is null test case.");
                DeleteNotebook(temp.ID);
                return false;
            }
            catch { return true; }
        }

        private bool CreateNotebookByDetailsTitleIsEmpty(StreamWriter sw)
        {
            try
            {
                Models.Notebook temp = CreateNotebook(userID1, "", notebook1.Author, notebook1.Isbn, notebook1.Publisher, notebook1.PublishDate, notebook1.CoverURL);
                sw.WriteLine("FAILED: CreateNotebook(string userID, string title, string author, string publisher, string publishDate): CreateNotebook by Book Details title is empty test case.");
                DeleteNotebook(temp.ID);
                return false;
            }
            catch { return true; }
        }

        private bool CreateNotebookByDetailsAuthorIsNull(StreamWriter sw)
        {
            try
            {
                Models.Notebook temp = CreateNotebook(userID1, notebook1.Title, null, notebook1.Isbn, notebook1.Publisher, notebook1.PublishDate, notebook1.CoverURL);
                DeleteNotebook(temp.ID);
                return true;
            }
            catch
            {
                sw.WriteLine("FAILED: CreateNotebook(string userID, string title, string author, string publisher, string publishDate): CreateNotebook by Book Details author is null test case.");
                return false;
            }
        }

        private bool CreateNotebookByDetailsPublisherIsNull(StreamWriter sw)
        {
            try
            {
                Models.Notebook temp = CreateNotebook(userID1, notebook1.Title, notebook1.Author, notebook1.Isbn, null, notebook1.PublishDate, notebook1.CoverURL);
                DeleteNotebook(temp.ID);
                return true;
            }
            catch
            {
                sw.WriteLine("FAILED: CreateNotebook(string userID, string title, string author, string publisher, string publishDate): CreateNotebook by Book Details publisher is null test case.");
                return false;
            }
        }
    }
}