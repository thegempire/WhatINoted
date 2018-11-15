using System;
using System.IO;
using WhatINoted.ConnectionManagers;

namespace WhatINoted.Tests.GoogleFirestoreConnectionManagerTests
{
    public class GFCM_HandleLoginTest : GFCM_Test
    {
        public override bool Run(StreamWriter sw)
        {
            bool passed = true;
            passed = NormalAccountCreation(sw) && passed;
            passed = NormalAccountLogin(sw) && passed;
            passed = NullUidThrowsException(sw) && passed;
            passed = EmptyUidThrowsException(sw) && passed;
            passed = NullDisplayNameThrowsException(sw) && passed;
            passed = EmptyDisplayNameThrowsException(sw) && passed;
            return passed;
        }

        private bool NormalAccountCreation(StreamWriter sw)
        {
            bool passed = true;
            Models.User expected = new Models.User(userID1, displayName1, email1, DateTime.Now, DateTime.Now);
            Models.User result = null;
            GoogleFirestoreConnectionManager.DeleteUser(userID1);

            try
            {
                result = GoogleFirestoreConnectionManager.HandleLogin(userID1, displayName1, email1);
            }
            catch {
                sw.WriteLine("FAILED: Normal Account Creation; unexpected exception thrown.");
                return false;
            }
            if (result == null)
            {
                sw.WriteLine("FAILED: Normal Account Creation; could not create user.");
                return false;
            }
            if (expected.DisplayName != result.DisplayName || expected.ID != result.ID)
            {
                sw.WriteLine("FAILED: Normal Account Creation; user not created as expected.");
                passed = false;
            }
            GoogleFirestoreConnectionManager.DeleteUser(userID1);
            return passed;
        }

        private bool NormalAccountLogin(StreamWriter sw)
        {
            bool passed = true;
            Models.User expected = new Models.User(userID1, displayName1, email1, DateTime.Now, DateTime.Now);
            try
            {
                if (GoogleFirestoreConnectionManager.HandleLogin(userID1, displayName1, email1) == null)
                {
                    sw.WriteLine("FAILED: Normal Account Login; could not create user.");
                    return false;
                }
            }
            catch {
                sw.WriteLine("FAILED: Normal Account Login; unexpected exception thrown on creation.");
                return false;
            }
            Models.User result = null;
            try
            {
                result = GoogleFirestoreConnectionManager.HandleLogin(userID1, displayName1, email1);
            }
            catch {
                sw.WriteLine("FAILED: Normal Account Login; unexpected exception thrown on login.");
                passed = false;
            }
            if (passed && result == null)
            {
                sw.WriteLine("FAILED: Normal Account Login; could not log in.");
                passed = false;
            }
            else if (passed && (expected.DisplayName != result.DisplayName || expected.ID != result.ID))
            {
                sw.WriteLine("FAILED: Normal Account Login; user not returned as expected.");
                passed = false;
            }
            GoogleFirestoreConnectionManager.DeleteUser(userID1);
            return passed;
        }

        private bool NullUidThrowsException(StreamWriter sw)
        {
            try
            {
                GoogleFirestoreConnectionManager.HandleLogin(null, displayName1, email1);
                sw.WriteLine("FAILED: Null Uid Throws Exception; exception not thrown.");
                return false;
            }
            catch { return true; }
        }

        private bool EmptyUidThrowsException(StreamWriter sw)
        {
            try
            {
                GoogleFirestoreConnectionManager.HandleLogin("", displayName1, email1);
                sw.WriteLine("FAILED: Empty Uid Throws Exception; exception not thrown.");
                return false;
            }
            catch { return true; }
        }

        private bool NullDisplayNameThrowsException(StreamWriter sw)
        {
            try
            {
                GoogleFirestoreConnectionManager.HandleLogin(userID1, null, email1);
                sw.WriteLine("FAILED: Null Display Name Throws Exception; exception not thrown.");
                return false;
            }
            catch { return true; }
        }

        private bool EmptyDisplayNameThrowsException(StreamWriter sw)
        {
            try
            {
                GoogleFirestoreConnectionManager.HandleLogin(userID1, "", email1);
                sw.WriteLine("FAILED: Empty Display Name Throws Exception; exception not thrown.");
                return false;
            }
            catch { return true; }
        }
    }
}
