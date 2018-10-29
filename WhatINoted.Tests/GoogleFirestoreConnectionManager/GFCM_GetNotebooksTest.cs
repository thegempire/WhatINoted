using System;
using System.Collections.Generic;
using System.IO;
using WhatINoted.ConnectionManagers;

namespace WhatINoted.Tests.GoogleFirestoreConnectionManagerTests
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
                GoogleFirestoreConnectionManager.DeleteUser(userID1);
                List<Models.Notebook> compNotebooks = new List<Models.Notebook>
                {
                    notebook1,
                    notebook2,
                    notebook3,
                    notebook4,
                    notebook5
                };
                GoogleFirestoreConnectionManager.HandleLogin(userID1, displayName1, email1);
                GoogleFirestoreConnectionManager.CreateNotebook(userID1, title1, author1, isbn1, publisher1, publishDate1, coverURL1);
                GoogleFirestoreConnectionManager.CreateNotebook(userID1, title2, author2, isbn2, publisher2, publishDate2, coverURL2);
                GoogleFirestoreConnectionManager.CreateNotebook(userID1, title3, author3, isbn3, publisher3, publishDate3, coverURL3);
                GoogleFirestoreConnectionManager.CreateNotebook(userID1, title4, author4, isbn4, publisher4, publishDate4, coverURL4);
                GoogleFirestoreConnectionManager.CreateNotebook(userID1, title5, author5, isbn5, publisher5, publishDate5, coverURL5);
                List<Models.Notebook> tempNotebooks = GoogleFirestoreConnectionManager.GetNotebooks(userID1);
                if (tempNotebooks.Count != compNotebooks.Count)
                {
                    sw.WriteLine("FAILED: GetNotebooks(string userID): Normal test case, count mismatch.");
                    GoogleFirestoreConnectionManager.DeleteUser(userID1);
                    return false;
                }
                else
                {
                    foreach (Models.Notebook n in tempNotebooks)
                    {
                        foreach (Models.Notebook t in compNotebooks)
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
                List<Models.Notebook> tempNotebooks = GoogleFirestoreConnectionManager.GetNotebooks(userID1);
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
                List<Models.Notebook> tempNotebooks = GoogleFirestoreConnectionManager.GetNotebooks(userID1 + "NOTEXIST");
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
