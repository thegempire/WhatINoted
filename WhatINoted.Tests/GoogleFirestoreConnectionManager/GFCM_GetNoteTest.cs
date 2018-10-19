using System;
using System.IO;

namespace WhatINoted.Tests.GoogleFirestoreConnectionManager
{
    public class GFCM_GetNoteTest : GFCM_Test
    {
        public override bool Run(StreamWriter sw)
        {
            bool passed = true;
            passed = GetNoteValidRequest(sw) && passed;
            passed = GetNoteNoteDoesNotExist(sw) && passed;
            passed = GetNoteNoteIdIsNull(sw) && passed;
            passed = GetNoteNoteIdIsEmpty(sw) && passed;
            return passed;
        }

        private bool GetNoteValidRequest(StreamWriter sw)
        {
            try
            {
                if (!GoogleFirestoreConnectionManager.GetNote(noteID1).Equals(note1))
                {
                    sw.WriteLine("FAILED: GetNote(string noteID): Normal test case.");
                    return false;
                }
            }
            catch
            {
                sw.WriteLine("FAILED: GetNote(string noteID): Normal test case - unexpected exception.");
                return false;
            }
            return true;
        }

        private bool GetNoteNoteDoesNotExist(StreamWriter sw) {
            try
            {
                if (GoogleFirestoreConnectionManager.GetNote(noteID1 + "NOTEXIST") != null)
                {
                    sw.WriteLine("FAILED: GetNote(string noteID): Note does not exist test case.");
                    return false;
                }
            }
            catch
            {
                sw.WriteLine("FAILED: GetNote(string noteID): Note does not exist test case - unexpected exception.");
                return false;
            }
            return true;
        }

        private bool GetNoteNoteIdIsNull(StreamWriter sw) {
            try
            {
                GoogleFirestoreConnectionManager.GetNote(null);
                sw.WriteLine("FAILED: GetNote(string noteID): noteID is null test case.");
                return false;
            }
            catch { return true; }
        }

        private bool GetNoteNoteIdIsEmpty(StreamWriter sw) {
            try
            {
                GoogleFirestoreConnectionManager.GetNote("");
                sw.WriteLine("FAILED: GetNote(string noteID): noteID is empty test case.");
                return false;
            }
            catch { return true; }
        }
    }
}
