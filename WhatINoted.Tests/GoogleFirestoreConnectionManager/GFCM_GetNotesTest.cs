﻿using System;
using System.Collections.Generic;
using System.IO;

namespace WhatINoted.Tests.GoogleFirestoreConnectionManager
{
    public class GFCM_GetNotesTest : GFCM_Test
    {
        public override bool Run(StreamWriter sw)
        {
            bool passed = true;
            passed = GetNotesValidRequest(sw) && passed;
            passed = GetNotesNotebookHasNoNotes(sw) && passed;
            passed = GetNotesNotebookDoesNotExist(sw) && passed;
            passed = GetNotesNotebookIdIsNull(sw) && passed;
            passed = GetNotesNotebookIsEmpty(sw) && passed;
            return passed;
        }

        private bool GetNotesValidRequest(StreamWriter sw) {
            try
            {
                List<Models.NoteModel> compNotes = new List<Models.NoteModel>();
                compNotes.Add(note1);
                compNotes.Add(note2);
                compNotes.Add(note2);
                List<Models.NoteModel> tempNotes = GoogleFirestoreConnectionManager.GetNotebookNotes(notebookID1);
                if (tempNotes.Count != compNotes.Count)
                {
                    sw.WriteLine("FAILED: GetNotes(string notebookID): Normal test case, count mismatch.");
                    return false;
                }
                else
                {
                    foreach (Models.NoteModel n in tempNotes)
                    {
                        foreach (Models.NoteModel t in compNotes)
                        {
                            if (t.Equals(n))
                            {
                                compNotes.Remove(t);
                                break;
                            }
                        }
                    }

                    if (compNotes.Count > 0)
                    {
                        sw.WriteLine("FAILED: GetNotes(string notebookID): Normal test case, Notes not the same.");
                        return false;
                    }
                }
            }
            catch
            {
                sw.WriteLine("FAILED: GetNotes(string notebookID): Normal test case - unexpected exception.");
                return false;
            }
            return true;
        }

        private bool GetNotesNotebookHasNoNotes(StreamWriter sw) {
            try
            {
                List<Models.NoteModel> tempNotes = GoogleFirestoreConnectionManager.GetNotebookNotes(notebookID2);
                if (tempNotes == null || tempNotes.Count != 0)
                {
                    sw.WriteLine("FAILED: GetNotes(string notebookID): Notebook has no notes test case.");
                    return false;
                }
            }
            catch
            {
                sw.WriteLine("FAILED: GetNotes(string notebookID): Notebook has no notes test case - unexpected exception.");
                return false;
            }
            return true;
        }

        private bool GetNotesNotebookDoesNotExist(StreamWriter sw) {
            try
            {
                List<Models.NoteModel> tempNotes = GoogleFirestoreConnectionManager.GetNotebookNotes(notebookID1 + "NOTEXIST");
                if (tempNotes == null || tempNotes.Count != 0)
                {
                    sw.WriteLine("FAILED: GetNotes(string notebookID): Notebook has no notes test case.");
                    return false;
                }
            }
            catch
            {
                sw.WriteLine("FAILED: GetNotes(string notebookID): Notebook has no notes test case - unexpected exception.");
                return false;
            }
            return true;
        }

        private bool GetNotesNotebookIdIsNull(StreamWriter sw) {
            try
            {
                GoogleFirestoreConnectionManager.GetNotebookNotes(null);
                sw.WriteLine("FAILED: GetNotes(string notebookID): Notebook is null test case.");
                return false;
            }
            catch { return true; }
        }

        private bool GetNotesNotebookIsEmpty(StreamWriter sw) {
            try
            {
                GoogleFirestoreConnectionManager.GetNotebookNotes("");
                sw.WriteLine("FAILED: GetNotes(string notebookID): Notebook is empty test case.");
                return false;
            }
            catch { return true; }
        }
    }
}