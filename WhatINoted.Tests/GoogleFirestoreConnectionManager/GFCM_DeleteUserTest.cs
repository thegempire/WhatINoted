﻿using System;
using System.Collections.Generic;
using System.IO;
using WhatINoted.Models;

namespace WhatINoted.Tests.GoogleFirestoreConnectionManager
{
	public class GFCM_DeleteUserTest : GFCM_Test
    {
        public override bool Run(StreamWriter sw)
        {
            bool passed = true;
            passed = DeleteUserValidRequest(sw) && passed;
            passed = DeleteUserWithNotebooks(sw) && passed;
            passed = DeleteUserUserDoesNotExist(sw) && passed;
            passed = DeleteUserUserIdIsNull(sw) && passed;
            passed = DeleteUserUserIdIsEmpty(sw) && passed;
            return passed;
        }

        private bool DeleteUserValidRequest(StreamWriter sw) {
            try
            {
                if (!GoogleFirestoreConnectionManager.DeleteUser(userID1))
                {
                    sw.WriteLine("FAILED: DeleteUser(string userID): Normal test case.");
                    return false;
                }
                else
                {
                    try
                    {
                        GoogleFirestoreConnectionManager.GetDisplayName(userID1);
                        sw.WriteLine("FAILED: DeleteUser(string userID): Normal test case - User still exists.");
                        return false;
                    }
                    catch { return true; }
                }
            }
            catch
            {
                sw.WriteLine("FAILED: DeleteUser(string userID): Normal test case - unexpected exception.");
                return false;
            }
        }

        private bool DeleteUserWithNotebooks(StreamWriter sw) {
            try
            {
                notebookID1 = GoogleFirestoreConnectionManager.CreateNotebook(userID2, isbn1).Id;
                GoogleFirestoreConnectionManager.DeleteUser(userID2);
                List<NotebookModel> temp = GoogleFirestoreConnectionManager.GetNotebooks(userID2);
                if (temp == null || temp.Count != 0)
                {
                    sw.WriteLine("FAILED: DeleteUser(string userID): delete user with Notebooks.");
                    return false;
                }
            }
            catch
            {
                sw.WriteLine("FAILED: DeleteUser(string userID): delete user with Notebooks - unexpected exception.");
                return false;
            }
            return true;
        }

        private bool DeleteUserUserDoesNotExist(StreamWriter sw) {
            try
            {
                if (!GoogleFirestoreConnectionManager.DeleteUser(userID1 + "NOTEXIST"))
                {
                    sw.WriteLine("FAILED: DeleteUser(string userID): User does not exist test case.");
                    return false;
                }
            }
            catch
            {
                sw.WriteLine("FAILED: DeleteUser(string userID): User does not exist test case - unexpected exception.");
                return false;
            }
            return true;
        }

        private bool DeleteUserUserIdIsNull(StreamWriter sw) {
            try
            {
                GoogleFirestoreConnectionManager.DeleteUser(null);
                sw.WriteLine("FAILED: DeleteUser(string userID): userID is null test case.");
                return false;
            }
            catch { return true; }
        }

        private bool DeleteUserUserIdIsEmpty(StreamWriter sw) {
            try
            {
                GoogleFirestoreConnectionManager.DeleteNotebook("");
                sw.WriteLine("FAILED: DeleteUser(string userID): userID is empty test case.");
                return false;
            }
            catch { return true; }
        }
    }
}