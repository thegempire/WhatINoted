﻿using System;
using System.IO;

namespace WhatINoted.Tests.GoogleFirestoreConnectionManager
{
	public class GFCM_DeleteNoteTest : GFCM_Test
    {
        public override bool Run(StreamWriter sw)
        {
            bool passed = true;
            passed = DeleteNoteValidRequest(sw) && passed;
            passed = DeleteNoteNoteDoesNotExist(sw) && passed;
            passed = DeleteNoteNoteIdIsNull(sw) && passed;
            passed = DeleteNoteNoteIdIsEmpty(sw) && passed;
            return passed;
        }

        private bool DeleteNoteValidRequest(StreamWriter sw) {
            try
            {
                if (!GoogleFirestoreConnectionManager.DeleteNote(noteID1))
                {
                    sw.WriteLine("FAILED: DeleteNote(string noteID): Normal test case 1.");
                    return false;
                }
                else if (GoogleFirestoreConnectionManager.GetNote(noteID1) != null)
                {
                    sw.WriteLine("FAILED: DeleteNote(string noteID, string noteText): Normal test case 1, note not deleted.");
                    return false;
                }
            }
            catch
            {
                sw.WriteLine("FAILED: DeleteNote(string noteID): Normal test case 1 - unexpected exception.");
                return false;
            }
            return true;
        }

        private bool DeleteNoteNoteDoesNotExist(StreamWriter sw) {
            try
            {
                if (!GoogleFirestoreConnectionManager.DeleteNote(noteID1 + "NOTEXIST"))
                {
                    sw.WriteLine("FAILED: DeleteNote(string noteID): Note does not exist test case.");
                    return false;
                }
            }
            catch
            {
                sw.WriteLine("FAILED: DeleteNote(string noteID): Note does not exist test case - unexpected exception.");
                return false;
            }
            return true;
        }

        private bool DeleteNoteNoteIdIsNull(StreamWriter sw) {
            try
            {
                GoogleFirestoreConnectionManager.DeleteNote(null);
                sw.WriteLine("FAILED: DeleteNote(string noteID): noteID is null test case.");
                return false;
            }
            catch { return true; }
        }

        private bool DeleteNoteNoteIdIsEmpty(StreamWriter sw) {
            try
            {
                GoogleFirestoreConnectionManager.DeleteNote("");
                sw.WriteLine("FAILED: DeleteNote(string noteID): noteID is empty test case.");
                return false;
            }
            catch { return true; }
        }
    }
}
