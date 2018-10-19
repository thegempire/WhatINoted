using System;
using System.Collections.Generic;
using System.IO;

namespace WhatINoted.Tests.GoogleFirestoreConnectionManager
{
    public class GFCM_GetNotebooksTest : GFCM_Test
    {
        public override bool Run(StreamWriter sw)
        {
            bool passed = true;
            passed = GetNotebooksValidRequest(sw) && passed;
            passed = GetNotebooksUserHasNoNotebooks(sw) && passed;
            passed = GetNotebooksUserDoesNotExist(sw) && passed;
            passed = GetNotebooksUserIdIsNull(sw) && passed;
            passed = GetNotebooksUserIdIsEmpty(sw) && passed;
            return passed;
        }

        private bool GetNotebooksValidRequest(StreamWriter sw) {
            try
            {
                List<Models.NotebookModel> compNotebooks = new List<Models.NotebookModel>();
                compNotebooks.Add(notebook1);
                compNotebooks.Add(notebook2);
                compNotebooks.Add(notebook3);
                compNotebooks.Add(notebook3);
                compNotebooks.Add(notebook5);
                compNotebooks.Add(notebook5);
                compNotebooks.Add(notebook7);
                GoogleFirestoreConnectionManager.HandleLogin(userID1, displayName1, email1);
                Models.NotebookModel temp = GoogleFirestoreConnectionManager.CreateNotebook(userID1, notebook1.Isbn);
                Models.NotebookModel temp = GoogleFirestoreConnectionManager.CreateNotebook(userID1, notebook2.Isbn);
                Models.NotebookModel temp = GoogleFirestoreConnectionManager.CreateNotebook(userID1, notebook3.Isbn);
                Models.NotebookModel temp = GoogleFirestoreConnectionManager.CreateNotebook(userID1, notebook3.Isbn);
                Models.NotebookModel temp = GoogleFirestoreConnectionManager.CreateNotebook(userID1, notebook5.Isbn);
                Models.NotebookModel temp = GoogleFirestoreConnectionManager.CreateNotebook(userID1, notebook5.Isbn);
                Models.NotebookModel temp = GoogleFirestoreConnectionManager.CreateNotebook(userID1, notebook7.Isbn);
                List<Models.NotebookModel> tempNotebooks = GoogleFirestoreConnectionManager.GetNotebooks(userID1);
                if (tempNotebooks.Count != compNotebooks.Count)
                {
                    sw.WriteLine("FAILED: GetNotebooks(string userID): Normal test case, count mismatch.");
                    GoogleFirestoreConnectionManager.DeleteUser(userID1);
                    return false;
                }
                else
                {
                    foreach (Models.NotebookModel n in tempNotebooks)
                    {
                        foreach (Models.NotebookModel t in compNotebooks)
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
                        GoogleFirestoreConnectionManager.DeleteUser(userID1);
                        return false;
                    }
                }
                GoogleFirestoreConnectionManager.DeleteUser(userID1);
            }
            catch
            {
                sw.WriteLine("FAILED: GetNotebooks(string userID): Normal test case - unexpected exception.");
                return false;
            }
            return true;
        }

        private bool GetNotebooksUserHasNoNotebooks(StreamWriter sw) {
            try
            {
                GoogleFirestoreConnectionManager.HandleLogin(userID1, displayName1, email1);
                List<Models.NotebookModel> tempNotebooks = GoogleFirestoreConnectionManager.GetNotebooks(userID2);
                if (tempNotebooks == null || tempNotebooks.Count != 0)
                {
                    sw.WriteLine("FAILED: GetNotebooks(string userID): User has no notebooks test case.");
                    GoogleFirestoreConnectionManager.DeleteUser(userID1);
                    return false;
                }
                GoogleFirestoreConnectionManager.DeleteUser(userID1);
            }
            catch
            {
                sw.WriteLine("FAILED: GetNotebooks(string userID): User has no notebooks test case - unexpected exception.");
                return false;
            }
            return true;
        }

        private bool GetNotebooksUserDoesNotExist(StreamWriter sw) {
            try
            {
                List<Models.NotebookModel> tempNotebooks = GoogleFirestoreConnectionManager.GetNotebooks(userID1 + "NOTEXIST");
                if (tempNotebooks == null || tempNotebooks.Count != 0)
                {
                    sw.WriteLine("FAILED: GetNotebooks(string userID): User does not exist test case.");
                    return false;
                }
            }
            catch
            {
                sw.WriteLine("FAILED: GetNotebooks(string userID): User does not exist test case - unexpected exception.");
                return false;
            }
            return true;
        }

        private bool GetNotebooksUserIdIsNull(StreamWriter sw) {
            try
            {
                GoogleFirestoreConnectionManager.GetNotebooks(null);
                sw.WriteLine("FAILED: GetNotebooks(string userID): User is null test case.");
                return false;
            }
            catch { return true; }
        }

        private bool GetNotebooksUserIdIsEmpty(StreamWriter sw) {
            try
            {
                GoogleFirestoreConnectionManager.GetNotebooks("");
                sw.WriteLine("FAILED: GetNotebooks(string userID): User is empty test case.");
                return false;
            }
            catch { return true; }
        }
    }
}
