using System;
using System.IO;

namespace WhatINoted.Tests.GoogleFirestoreConnectionManager
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
            passed = NullEmailThrowsException(sw) && passed;
            passed = EmptyEmailThrowsException(sw) && passed;
            return passed;
        }

        private bool NormalAccountCreation(StreamWriter sw)
        {
            bool passed = true;
            Models.UserModel expected = new Models.UserModel(displayName1, userID1);
            Models.UserModel result = null;
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
            if (expected.Name != result.Name || expected.Uid != result.Uid)
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
            Models.UserModel expected = new Models.UserModel(displayName1, userID1);
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
            Models.UserModel result = null;
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
            else if (passed && (expected.Name != result.Name || expected.Uid != result.Uid))
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

        private bool NullEmailThrowsException(StreamWriter sw)
        {
            try
            {
                GoogleFirestoreConnectionManager.HandleLogin(userID1, displayName1, null);
                sw.WriteLine("FAILED: Null Email Throws Exception; exception not thrown.");
                return false;
            }
            catch { return true; }
        }

        private bool EmptyEmailThrowsException(StreamWriter sw)
        {
            try
            {
                GoogleFirestoreConnectionManager.HandleLogin(userID1, displayName1, "");
                sw.WriteLine("FAILED: Empty Email Throws Exception; exception not thrown.");
                return false;
            }
            catch { return true; }
        }
    }
}
