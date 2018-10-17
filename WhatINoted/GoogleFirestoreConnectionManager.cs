using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using WhatINoted.Models;

namespace WhatINoted
{
    public class GoogleFirestoreConnectionManager
    {
        private static readonly string DatabaseBaseAddress = "https://firestore.googleapis.com/v1beta1/projects/whatinoted-12345/databases/(default)/documents/";

        private static WebClient _webClient;

        private static WebClient WebClient
        {
            get
            {
                if (_webClient == null)
                {
                    _webClient = new WebClient();
                    _webClient.BaseAddress = DatabaseBaseAddress;
                }
                return _webClient;
            }
        }

        public static UserModel GetUser(string userID)
        {
            string jsonString = WebClient.DownloadString("users/" + userID);
            JsonUser jsonUser = JsonConvert.DeserializeObject<JsonUser>(jsonString);
            return new UserModel("name", "uid");
        }

        /// <summary>
        /// Returns the display name for the given user.
        /// </summary>
        /// <param name="userID">the ID of the user</param>
        /// <returns>the display name of the user if found; otherwise an empty string</returns>
        public static string GetDisplayName(string userID)
        {
            string jsonString = WebClient.DownloadString("users/" + userID);
            JsonUser jsonUser = JsonConvert.DeserializeObject<JsonUser>(jsonString);
            return jsonUser.DisplayName;
        }

        /// <summary>
        /// Returns the notebooks for the given user.
        /// </summary>
        /// <param name="userID">the ID of the user</param>
        /// <returns>a collection of notebooks for the user if found; otherwise an empty list</returns>
        public static List<NotebookModel> GetNotebooks(string userID)
        {
            string jsonResult = RunQuery("notebooks", "userID", userID);
            JsonNotebookWrapper[] notebookWrappers = JsonConvert.DeserializeObject<JsonNotebookWrapper[]>(jsonResult);

            List<NotebookModel> notebooks = new List<NotebookModel>();
            foreach (JsonNotebookWrapper wrapper in notebookWrappers)
            {
                notebooks.Add(new NotebookModel(wrapper.document));
            }
            return notebooks;
        }

        /// <summary>
        /// Returns the notes for the given user.
        /// </summary>
        /// <param name="userID">the ID of the user</param>
        /// <returns>a collection of notes for the user if found; otherwise an empty list</returns>
        public static List<NoteModel> GetUserNotes(string userID)
        {
            string jsonResult = RunQuery("notes", "userID", userID);
            JsonNoteWrapper[] noteWrappers = JsonConvert.DeserializeObject<JsonNoteWrapper[]>(jsonResult);

            List<NoteModel> notes = new List<NoteModel>();
            foreach (JsonNoteWrapper wrapper in noteWrappers)
            {
                notes.Add(new NoteModel(wrapper.document));
            }
            return notes;
        }

        /// <summary>
        /// Returns the notes for the given notebook.
        /// </summary>
        /// <param name="notebookID">the ID of the notebook</param>
        /// <returns>a collecton of notes for the notebook if found; otherwise an empty list</returns>
        public static List<NoteModel> GetNotebookNotes(string notebookID)
        {
            string jsonResult = RunQuery("notes", "notebookID", notebookID);
            JsonNoteWrapper[] noteWrappers = JsonConvert.DeserializeObject<JsonNoteWrapper[]>(jsonResult);

            List<NoteModel> notes = new List<NoteModel>();
            foreach (JsonNoteWrapper wrapper in noteWrappers)
            {
                notes.Add(new NoteModel(wrapper.document));
            }
            return notes;
        }

        /// <summary>
        /// Returns the note with the given ID.
        /// </summary>
        /// <param name="noteID">the ID of the note</param>
        /// <returns>the note if found; otherwise null</returns>
        public static NoteModel GetNote(string noteID)
        {
            string jsonString = WebClient.DownloadString("notes/" + noteID);
            JsonNote jsonNote = JsonConvert.DeserializeObject<JsonNote>(jsonString);
            return new NoteModel(jsonNote);
        }

        /// <summary>
        /// Creates a user.
        /// 
        /// If the user already exists, nothing will be done.
        /// </summary>
        /// <param name="userID">the ID of the user</param>
        /// <param name="displayName">the display name for the user</param>
        /// <param name="email">the email for the user</param>
        /// <returns>true if the user was created or if it already exists; false otherwise</returns>
        public static UserModel CreateUser(string userID, string displayName, string email)
        {
            string path = "users?documentId=" + userID;
            string json = GenerateCreateUserJson(userID, displayName, email);
            try
            {
                string result = WebClient.UploadString(path, json);
            }
            catch (WebException e)
            {
                switch (e.HResult)
                {
                    // Conflict - User already exists
                    case -2146233079:
                        return null;
                    // Unknown exception
                    default:
                        System.Diagnostics.Debug.WriteLine(e);
                        throw e;
                }
            }
            JsonUser jsonUser = JsonConvert.DeserializeObject<JsonUser>(json);
            return new UserModel(jsonUser);
        }

        public static NotebookModel CreateNotebook(string userID, string title, string author, string publisher, DateTime publishDate)
        {
            string path = "notebooks";
            string json = GenerateCreateNotebookJson(userID, title, author, publisher, publishDate);
            string result = WebClient.UploadString(path, json);

            JsonNotebook jsonNotebook = JsonConvert.DeserializeObject<JsonNotebook>(result);
            return new NotebookModel(jsonNotebook);
        }

        public static NotebookModel CreateNotebook(string isbn)
        {
            // TODO - Request book information from ISBN API

            string userID = "";
            string title = "";
            string author = "";
            string publisher = "";
            DateTime publishDate = DateTime.Now;

            string path = "notebooks";
            string json = GenerateCreateNotebookJson(userID, title, author, publisher, publishDate);
            string result = WebClient.UploadString(path, json);

            JsonNotebook jsonNotebook = JsonConvert.DeserializeObject<JsonNotebook>(result);
            return new NotebookModel(jsonNotebook);
        }

        public static NoteModel CreateNote(string userID, string notebookID, string noteText)
        {
            string path = "notes";
            string json = GenerateCreateNoteJson(userID, notebookID, noteText);
            string result = WebClient.UploadString(path, json);

            JsonNote jsonNote = JsonConvert.DeserializeObject<JsonNote>(result);
            return new NoteModel(jsonNote);
        }

        public static NoteModel UpdateNote(string noteID, string noteText)
        {
            string path = "notes/" + noteID;
            string json = GenerateUpdateNoteJson(noteID, noteText);
            try
            {
                string result = WebClient.UploadString(path, json);
            }
            catch (WebException e)
            {
                switch (e.HResult)
                {
                    // Conflict - User already exists
                    case -2146233079:
                        return null;
                    // Unknown exception
                    default:
                        System.Diagnostics.Debug.WriteLine(e);
                        throw e;
                }
            }

            JsonNote jsonNote = JsonConvert.DeserializeObject<JsonNote>(json);

            return new NoteModel(jsonNote);
        }

        public static bool DeleteNote(string noteID)
        {
            string path = "notes/" + noteID;// + "?currentDocument.exists=true";
            string result = WebClient.UploadString(path, "DELETE", "");
            return true;
        }

        private static string RunQuery(string collectionID, string fieldName, string fieldValue)
        {
            string valueType = "stringValue";
            string structuredQuery = "{\"structuredQuery\":{\"where\":{\"fieldFilter\":{\"field\":{\"fieldPath\":\""
                + fieldName + "\"},\"op\":\"EQUAL\",\"value\":{\""
                + valueType + "\":\""
                + fieldValue + "\"}}},\"from\":[{\"collectionId\":\""
                + collectionID + "\"}]}}";
            string path = ":runQuery";
            return WebClient.UploadString(path, structuredQuery);
        }

        private static string GenerateCreateUserJson(string userID, string displayName, string email)
        {
            string createTimeUTC = DateTime.UtcNow.ToString("o");

            List<Tuple<string, string, string>> fieldConfigList = new List<Tuple<string, string, string>>();
            fieldConfigList.Add(new Tuple<string, string, string>("displayName", "stringValue", displayName));
            fieldConfigList.Add(new Tuple<string, string, string>("email", "stringValue", email));
            fieldConfigList.Add(new Tuple<string, string, string>("created", "timestampValue", createTimeUTC));
            fieldConfigList.Add(new Tuple<string, string, string>("modified", "timestampValue", createTimeUTC));

            return GenerateFieldsJson(fieldConfigList);
        }

        private static string GenerateCreateNotebookJson(string userID, string title, string author, string publisher, DateTime publishDate)
        {
            string publishDateUTC = publishDate.ToUniversalTime().ToString("o");
            string createTimeUTC = DateTime.UtcNow.ToString("o");

            List<Tuple<string, string, string>> fieldConfigList = new List<Tuple<string, string, string>>();
            fieldConfigList.Add(new Tuple<string, string, string>("userID", "stringValue", userID));
            fieldConfigList.Add(new Tuple<string, string, string>("title", "stringValue", title));
            fieldConfigList.Add(new Tuple<string, string, string>("author", "stringValue", author));
            fieldConfigList.Add(new Tuple<string, string, string>("publisher", "stringValue", publisher));
            fieldConfigList.Add(new Tuple<string, string, string>("publishDate", "stringValue", publishDateUTC));
            fieldConfigList.Add(new Tuple<string, string, string>("created", "timestampValue", createTimeUTC));
            fieldConfigList.Add(new Tuple<string, string, string>("modified", "timestampValue", createTimeUTC));

            return GenerateFieldsJson(fieldConfigList);
        }

        private static string GenerateCreateNoteJson(string userID, string notebookID, string noteText)
        {
            string createTimeUTC = DateTime.UtcNow.ToString("o");

            List<Tuple<string, string, string>> fieldConfigList = new List<Tuple<string, string, string>>();
            fieldConfigList.Add(new Tuple<string, string, string>("userID", "stringValue", userID));
            fieldConfigList.Add(new Tuple<string, string, string>("notebookID", "stringValue", notebookID));
            fieldConfigList.Add(new Tuple<string, string, string>("noteText", "stringValue", noteText));
            fieldConfigList.Add(new Tuple<string, string, string>("created", "timestampValue", createTimeUTC));
            fieldConfigList.Add(new Tuple<string, string, string>("modified", "timestampValue", createTimeUTC));

            return GenerateFieldsJson(fieldConfigList);
        }

        private static string GenerateUpdateNoteJson(string userID, string noteText)
        {
            string updateTimeUTC = DateTime.UtcNow.ToString("o");

            List<Tuple<string, string, string>> fieldConfigList = new List<Tuple<string, string, string>>();
            fieldConfigList.Add(new Tuple<string, string, string>("userID", "stringValue", userID));
            fieldConfigList.Add(new Tuple<string, string, string>("noteText", "stringValue", noteText));
            fieldConfigList.Add(new Tuple<string, string, string>("modified", "timestampValue", updateTimeUTC));

            return GenerateFieldsJson(fieldConfigList);
        }

        public static void TestGenerateFieldsJson()
        {
            List<Tuple<string, string, string>> fieldConfigList = new List<Tuple<string, string, string>>();
            string noConfigs = GenerateFieldsJson(fieldConfigList);
            string noConfigsExpected = "{\"fields\":{}}";

            fieldConfigList.Add(new Tuple<string, string, string>("fieldName1", "fieldType1", "fieldValue1"));
            string oneConfig = GenerateFieldsJson(fieldConfigList);
            string oneConfigExpected = "{\"fields\":{\"fieldName1\":{\"fieldType1\":\"fieldValue1\"}}}";

            fieldConfigList.Add(new Tuple<string, string, string>("fieldName2", "fieldType2", "fieldValue2"));
            string multipleConfigs = GenerateFieldsJson(fieldConfigList);
            string multipleConfigsExpected = "{\"fields\":{\"fieldName1\":{\"fieldType1\":\"fieldValue1\"},\"fieldName2\":{\"fieldType2\":\"fieldValue2\"}}}";

            // Test fieldConfigList not null
            // Test fieldConfig values not null
            // Test fieldConfig.Item2 value in ValueType enumeration

            Console.WriteLine();
        }

        private static string GenerateFieldsJson(List<Tuple<string, string, string>> fieldConfigList)
        {
            StringBuilder json = new StringBuilder();
            json.Append("{\"fields\":{");
            int extraCommaIndex = json.Length;
            if (fieldConfigList.Count > 0)
            {
                foreach (Tuple<string, string, string> fieldConfig in fieldConfigList)
                {
                    json.Append(",\"");
                    json.Append(fieldConfig.Item1);
                    json.Append("\":{\"");
                    json.Append(fieldConfig.Item2);
                    json.Append("\":\"");
                    json.Append(fieldConfig.Item3);
                    json.Append("\"}");
                }
                json.Remove(extraCommaIndex, 1);
            }
            json.Append("}}");
            return json.ToString();
        }
    }
}