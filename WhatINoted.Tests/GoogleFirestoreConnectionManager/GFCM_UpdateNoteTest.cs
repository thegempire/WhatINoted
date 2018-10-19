using System;
using System.IO;

namespace WhatINoted.Tests.GoogleFirestoreConnectionManager
{
    public class GFCM_UpdateNoteTest : GFCM_Test
    {
        public override bool Run(StreamWriter sw)
        {
            bool passed = true;
            passed = UpdateNoteValidRequest(sw) && passed;
            passed = UpdateNoteNoteDoesNotExist(sw) && passed;
            passed = UpdateNoteNoteIdIsNull(sw) && passed;
            passed = UpdateNoteNoteIdIsEmpty(sw) && passed;
            passed = UpdateNoteNoteTextIsNull(sw) && passed;
            passed = UpdateNoteNoteTextIsEmpty(sw) && passed;
            return passed;
        }

        private bool UpdateNoteValidRequest(StreamWriter sw) {
            try
            {
                if (GoogleFirestoreConnectionManager.UpdateNote(noteID1, noteTextUpdated) == null)
                {
                    sw.WriteLine("FAILED: UpdateNote(string noteID, string noteText): Normal test case.");
                    return false;
                }
                else if (GoogleFirestoreConnectionManager.GetNote(noteID1).Text != noteTextUpdated)
                {
                    sw.WriteLine("FAILED: UpdateNote(string noteID, string noteText): Normal test case, note not updated.");
                    return false;

                }
            }
            catch
            {
                sw.WriteLine("FAILED: UpdateNote(string noteID, string noteText): Normal test case - unexpected exception.");
                return false;
            }
            return true;
        }

        private bool UpdateNoteNoteDoesNotExist(StreamWriter sw) {
            try
            {
                GoogleFirestoreConnectionManager.UpdateNote(noteID1 + "NOTEXIST", noteTextUpdated);
                sw.WriteLine("FAILED: UpdateNote(string noteID, string noteText): User does not exist.");
                return false;
            }
            catch { return true; }
        }

        private bool UpdateNoteNoteIdIsNull(StreamWriter sw) {
            try
            {
                GoogleFirestoreConnectionManager.UpdateNote(null, noteTextUpdated);
                sw.WriteLine("FAILED: UpdateNote(string noteID, string noteText): noteID is null test case.");
                return false;
            }
            catch { return true; }
        }

        private bool UpdateNoteNoteIdIsEmpty(StreamWriter sw) {
            try
            {
                GoogleFirestoreConnectionManager.UpdateNote("", noteTextUpdated);
                sw.WriteLine("FAILED: UpdateNote(string noteID, string noteText): noteID is empty test case.");
                return false;
            }
            catch { return true; }
        }

        private bool UpdateNoteNoteTextIsNull(StreamWriter sw) {
            try
            {
                if (GoogleFirestoreConnectionManager.UpdateNote(noteID1, null) != null)
                {
                    sw.WriteLine("FAILED: UpdateNote(string noteID, string noteText): noteText is null.");
                    return false;
                }
                else if (GoogleFirestoreConnectionManager.GetNote(noteID1).Text != "")
                {
                    sw.WriteLine("FAILED: UpdateNote(string noteID, string noteText): noteText is null, note not updated.");
                    return false;
                }
            }
            catch
            {
                sw.WriteLine("FAILED: UpdateNote(string noteID, string noteText): noteText is null - unexpected exception.");
                return false;
            }
            return true;
        }

        private bool UpdateNoteNoteTextIsEmpty(StreamWriter sw) {
            try
            {
                if (GoogleFirestoreConnectionManager.UpdateNote(noteID1, "") != null)
                {
                    sw.WriteLine("FAILED: UpdateNote(string noteID, string noteText): noteText is empty.");
                    return false;
                }
                else if (GoogleFirestoreConnectionManager.GetNote(noteID1).Text != "")
                {
                    sw.WriteLine("FAILED: UpdateNote(string noteID, string noteText): noteText is empty, note not updated.");
                    return false;
                }
            }
            catch
            {
                sw.WriteLine("FAILED: UpdateNote(string noteID, string noteText): noteText is empty - unexpected exception.");
                return false;
            }
            return true;
        }
    }
}
