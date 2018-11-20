using System;
using System.Collections.Generic;
using System.IO;
using WhatINoted.Models;

namespace WhatINoted.Tests.GoogleFirestoreConnectionManagerTests
{
    public abstract class GFCM_Test : Test
    {
        protected static readonly string userID1 = "test_userID1";
        protected static readonly string userID2 = "test_userID2";
        protected static readonly string displayName1 = "test_displayName1";
        protected static readonly string displayName2 = "test_displayName2";
        protected static readonly string email1 = "test_email1";
        protected static readonly string email2 = "test_email2";

        protected static string notebookID1 = "test_notebookID1";
        protected static string notebookID2;
        protected static string notebookID3;
        protected static string notebookID4;
        protected static string notebookID5;
        protected static string notebookID6;
        protected static string notebookID7;
        protected static readonly string title1 = "test_title1";
        protected static readonly string title2 = "test_title2";
        protected static readonly string title3 = "test_title3";
        protected static readonly string title4 = "test_title4";
        protected static readonly string title5 = "test_title5";
        protected static readonly string author1 = "test_author1";
        protected static readonly string author2 = "test_author2";
        protected static readonly string author3 = "test_author3";
        protected static readonly string author4 = "test_author4";
        protected static readonly string author5 = "test_author5";
        protected static readonly string isbn1 = "test_isbn1";
        protected static readonly string isbn2 = "test_isbn2";
        protected static readonly string isbn3 = "test_isbn3";
        protected static readonly string isbn4 = "test_isbn4";
        protected static readonly string isbn5 = "test_isbn5";
        protected static readonly string publisher1 = "test_publisher1";
        protected static readonly string publisher2 = "test_publisher2";
        protected static readonly string publisher3 = "test_publisher3";
        protected static readonly string publisher4 = "test_publisher4";
        protected static readonly string publisher5 = "test_publisher5";
        protected static readonly string publishDate1 = "2005-11-15";
        protected static readonly string publishDate2 = "2005-11-16";
        protected static readonly string publishDate3 = "2005-11-17";
        protected static readonly string publishDate4 = "2005-11-18";
        protected static readonly string publishDate5 = "2005-11-19";
        protected static readonly string coverURL1 = "test_coverURL1";
        protected static readonly string coverURL2 = "test_coverURL2";
        protected static readonly string coverURL3 = "test_coverURL3";
        protected static readonly string coverURL4 = "test_coverURL4";
        protected static readonly string coverURL5 = "test_coverURL5";

        protected static string noteID1;
        protected static string noteID2;
        protected static string noteID3;
        protected static readonly string text1 = "test_text1 Create text.";
        protected static readonly string text2 = "test_text2 Update text.";
        protected static readonly string text3 = "test_text3 More text.";

        protected static User user1 = new User(userID1, displayName1, email1, DateTime.Now, DateTime.Now);
        protected static User user2 = new User(userID2, displayName2, email2, DateTime.Now, DateTime.Now);

        protected static Notebook notebook1 = new Notebook("", title1, author1, isbn1, publisher1, publishDate1, coverURL1, userID1, DateTime.Now, DateTime.Now);
        protected static Notebook notebook2 = new Notebook("", title2, author2, isbn2, publisher2, publishDate2, coverURL2, userID1, DateTime.Now, DateTime.Now);
        protected static Notebook notebook3 = new Notebook("", title3, author3, isbn3, publisher3, publishDate3, coverURL3, userID1, DateTime.Now, DateTime.Now);
        protected static Notebook notebook4 = new Notebook("", title4, author4, isbn4, publisher4, publishDate4, coverURL4, userID1, DateTime.Now, DateTime.Now);
        protected static Notebook notebook5 = new Notebook("", title5, author5, isbn5, publisher5, publishDate5, coverURL5, userID1, DateTime.Now, DateTime.Now);

        protected static Note note1 = new Note("", userID1, notebookID1, text1, DateTime.Now, DateTime.Now);
        protected static Note note2 = new Note("", userID1, notebookID1, text2, DateTime.Now, DateTime.Now);
        protected static Note note3 = new Note("", userID1, notebookID1, text3, DateTime.Now, DateTime.Now);

        public abstract bool Run(StreamWriter sw);

        public static List<Test> GetTests()
        {
            List<Test> tests = new List<Test>
            {
                new GFCM_HandleLoginTest(),
                new GFCM_CreateNotebookTest(),
                new GFCM_CreateNoteTest(),
                new GFCM_DeleteUserTest(),
                new GFCM_DeleteNotebookTest(),
                new GFCM_DeleteNoteTest(),
                new GFCM_GetNotebooksTest(),
                new GFCM_GetNotebookNotesTest(),
                new GFCM_GetUserNotesTest(),
                new GFCM_GetNoteTest(),
                new GFCM_UpdateNoteTest(),
                new GFCM_GenerateFieldsJsonTest()
            };
            return tests;
        }
    }
}
