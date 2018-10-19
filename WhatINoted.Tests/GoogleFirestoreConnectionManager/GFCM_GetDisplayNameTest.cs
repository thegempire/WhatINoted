using System;
using System.IO;

namespace WhatINoted.Tests.GoogleFirestoreConnectionManager
{
    public class GFCM_GetDisplayNameTest : GFCM_Test
    {
        public override bool Run(StreamWriter sw)
        {
            bool passed = true;
            passed = NormalDisplayNameRequest(sw) && passed;
            passed = GetDisplayNameUserDoesNotExist(sw) && passed;
            passed = GetDisplayNameNullUserId(sw) && passed;
            passed = GetDisplayNameEmptyUserId(sw) && passed;
            return passed;
        }

        private bool NormalDisplayNameRequest(StreamWriter sw) {
            try
            {
                GoogleFirestoreConnectionManager.HandleLogin(userID1, displayName1, email1);
                if (GoogleFirestoreConnectionManager.GetDisplayName(userID1) != displayName1)
                {
                    sw.WriteLine("FAILED: GetDisplayName(string userID): Normal DisplayName request.");
                    GoogleFirestoreConnectionManager.DeleteUser(userID1);
                    return false;
                }
                GoogleFirestoreConnectionManager.DeleteUser(userID1);
            }
            catch
            {
                sw.WriteLine("FAILED: GetDisplayName(string userID): Normal DisplayName request - unexpected exception.");
                return false;
            }
            return true;
        }

        private bool GetDisplayNameUserDoesNotExist(StreamWriter sw) {
            try
            {
                GoogleFirestoreConnectionManager.GetDisplayName(userID1 + "NOTEXIST");
                sw.WriteLine("FAILED: GetDisplayName(string userID): DisplayName request for non-existing user.");
                return false;
            }
            catch { return true; }
        }

        private bool GetDisplayNameNullUserId(StreamWriter sw) {
            try
            {
                GoogleFirestoreConnectionManager.GetDisplayName(null);
                sw.WriteLine("FAILED: GetDisplayName(string userID): userID is null test case.");
                return false;
            }
            catch { return true; }
        }

        private bool GetDisplayNameEmptyUserId(StreamWriter sw) {
            try
            {
                GoogleFirestoreConnectionManager.GetDisplayName("");
                sw.WriteLine("FAILED: GetDisplayName(string userID): userID is empty test case.");
                return false;
            }
            catch { return true; }
        }
    }
}
