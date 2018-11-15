using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using WhatINoted.Models;

namespace WhatINoted.ConnectionManagers
{
    /// <summary>
    /// Defines a collection of static methods for accessing the Google Firestore database.
    /// </summary>
    public class GoogleFirestoreConnectionManager
    {
        // The base address to access the database
        private static readonly string DatabaseBaseAddress = "https://firestore.googleapis.com/v1beta1/projects/whatinoted-12345/databases/(default)/documents/";

        // The hidden web client for accessing the database
        private static WebClient _webClient;

        // Provides access to the web client for accessing the database
        private static WebClient WebClient
        {
            get
            {
                if (_webClient == null)
                {
                    _webClient = new WebClient();
                    _webClient.Encoding = Encoding.UTF8;
                    _webClient.BaseAddress = DatabaseBaseAddress;
                }
                return _webClient;
            }
        }

        /// <summary>
        /// Returns the user with the given ID.
        /// </summary>
        /// <param name="userID">the ID of the user</param>
        /// <returns>the user if found; otherwise a NotFoundExeption is thrown</returns>
        public static User GetUser(string userID)
        {
            if (userID == null || userID == "")
            {
                throw new ArgumentNullException();
            }

            try
            {
                string jsonString = WebClient.DownloadString("users/" + userID);
                JsonUser jsonUser = JsonConvert.DeserializeObject<JsonUser>(jsonString);
                return new User(jsonUser);
            }
            catch (WebException e)
            {
                switch (e.HResult)
                {
                    // User not found
                    case -2146233079:
                        throw new NotFoundException("User");
                    // Unknown exception
                    default:
                        throw;
                }
            }
        }

        /// <summary>
        /// Returns the notebook with the given ID.
        /// </summary>
        /// <param name="notebookID">the ID of the notebook</param>
        /// <returns>the notebook if found; otherwise a NotFoundExeption is thrown</returns>
        public static Notebook GetNotebook(string notebookID)
        {
            if (notebookID == null || notebookID == "")
            {
                throw new ArgumentNullException();
            }

            try
            {
                string jsonString = WebClient.DownloadString("notebooks/" + notebookID);
                JsonNotebook jsonNotebook = JsonConvert.DeserializeObject<JsonNotebook>(jsonString);
                return new Notebook(jsonNotebook);
            }
            catch (WebException e)
            {
                switch (e.HResult)
                {
                    // Notebook not found
                    case -2146233079:
                        throw new NotFoundException("Notebook");
                    // Unknown exception
                    default:
                        throw;
                }
            }
        }

        /// <summary>
        /// Returns the note with the given ID.
        /// </summary>
        /// <param name="noteID">the ID of the note</param>
        /// <returns>the note if found; otherwise a NotFoundExeption is thrown</returns>
        public static Note GetNote(string noteID)
        {
            if (noteID == null || noteID == "")
            {
                throw new ArgumentNullException();
            }

            try
            {
                string jsonString = WebClient.DownloadString("notes/" + noteID);
                JsonNote jsonNote = JsonConvert.DeserializeObject<JsonNote>(jsonString);
                return new Note(jsonNote);
            }
            catch (WebException e)
            {
                switch (e.HResult)
                {
                    // Note not found
                    case -2146233079:
                        throw new NotFoundException("Note");
                    // Unknown exception
                    default:
                        throw;
                }
            }
        }

        /// <summary>
        /// Returns the notebooks for the given user.
        /// </summary>
        /// <param name="userID">the ID of the user</param>
        /// <returns>a collection of notebooks for the user if found; otherwise an empty list</returns>
        public static List<Notebook> GetNotebooks(string userID)
        {
            if (userID == null || userID == "")
            {
                throw new ArgumentNullException();
            }

            string jsonResult = RunQuery("notebooks", "userID", userID);
            JsonNotebookWrapper[] notebookWrappers = JsonConvert.DeserializeObject<JsonNotebookWrapper[]>(jsonResult);

            List<Notebook> notebooks = new List<Notebook>();
            if (notebookWrappers.Length >= 1 && notebookWrappers[0].document != null)
            {
                foreach (JsonNotebookWrapper wrapper in notebookWrappers)
                {
                    notebooks.Add(new Notebook(wrapper.document));
                }
            }
            return notebooks;
        }

        /// <summary>
        /// Returns the notes for the given user.
        /// </summary>
        /// <param name="userID">the ID of the user</param>
        /// <returns>a collection of notes for the user if found; otherwise an empty list</returns>
        public static List<Note> GetUserNotes(string userID)
        {
            if (userID == null || userID == "")
            {
                throw new ArgumentNullException();
            }

            string jsonResult = RunQuery("notes", "userID", userID);
            JsonNoteWrapper[] noteWrappers = JsonConvert.DeserializeObject<JsonNoteWrapper[]>(jsonResult);

            List<Note> notes = new List<Note>();
            if (noteWrappers.Length >= 1 && noteWrappers[0].document != null)
            {
                foreach (JsonNoteWrapper wrapper in noteWrappers)
                {
                    notes.Add(new Note(wrapper.document));
                }
            }
            return notes;
        }

        /// <summary>
        /// Returns the notes for the given notebook.
        /// </summary>
        /// <param name="notebookID">the ID of the notebook</param>
        /// <returns>a collecton of notes for the notebook if found; otherwise an empty list</returns>
        public static List<Note> GetNotebookNotes(string notebookID)
        {
            if (notebookID == null || notebookID == "")
            {
                throw new ArgumentNullException();
            }

            string jsonResult = RunQuery("notes", "notebookID", notebookID);
            JsonNoteWrapper[] noteWrappers = JsonConvert.DeserializeObject<JsonNoteWrapper[]>(jsonResult);

            List<Note> notes = new List<Note>();
            if (noteWrappers.Length >= 1 && noteWrappers[0].document != null)
            {
                foreach (JsonNoteWrapper wrapper in noteWrappers)
                {
                    notes.Add(new Note(wrapper.document));
                }
            }
            return notes;
        }
        
        /// <summary>
        /// Creates a user if it does not exist. If it does, the existing user is updated with the given information.
        /// </summary>
        /// <param name="userID">the ID of the user</param>
        /// <param name="displayName">the display name for the user</param>
        /// <param name="email">the email for the user</param>
        /// <returns>true if the user was created or if it already exists; false otherwise</returns>
        public static User HandleLogin(string userID, string displayName, string email)
        {
            if (userID == null || displayName == null || email == null || userID == "" || displayName == "" || email == "")
            {
                throw new ArgumentNullException();
            }

            string path = "users?documentId=" + userID;
            string createJson = GenerateCreateUserJson(userID, displayName, email);
            string jsonResult = "";
            try
            {
                jsonResult = WebClient.UploadString(path, createJson);

                // If the user already exists, then an exception will be thrown and this line will not be executed
                // Therefore, the "Unfiled Notes" notebook will only be created if this is a new user
                CreateNotebook(userID, "Unfiled Notes", coverURL: "./Notebook.png");
            }
            catch (WebException e)
            {
                switch (e.HResult)
                {
                    // Conflict - User already exists
                    case -2146233079:
                        return GetUser(userID);
                    // Unknown exception
                    default:
                        System.Diagnostics.Debug.WriteLine(e);
                        throw;
                }
            }
            JsonUser jsonUser = JsonConvert.DeserializeObject<JsonUser>(jsonResult);
            return new User(jsonUser);
        }

        /// <summary>
        /// Creates a notebook using the given information.
        /// </summary>
        /// <param name="userID">the ID of the user that the notebook belongs to</param>
        /// <param name="title">the title of the notebook</param>
        /// <param name="author">the author of the notebook</param>
        /// <param name="isbn">the isbn of the notebook</param>
        /// <param name="publisher">the publisher of the notebook</param>
        /// <param name="publishDate">the date that the notebook was published</param>
        /// <param name="coverURL">a URL pointing to an image of the cover of the notebook</param>
        /// <returns>the created notebook</returns>
        public static Notebook CreateNotebook(string userID, string title, string author = "", string isbn = "", string publisher = "", string publishDate = "", string coverURL = "")
        {
            if (userID == null || title == null //|| author == null || isbn == null || publisher == null || coverURL == null
                || userID == "" || title == "" )//|| author == "" || isbn == "" || publisher == "" || coverURL == "")
            {
                throw new ArgumentNullException();
            }

            User user = GetUser(userID);
            if (user == null)
            {
                throw new NotFoundException("User");
            }

            string path = "notebooks";
            string json = GenerateCreateNotebookJson(userID, title, author, isbn, publisher, publishDate, coverURL);
            string result = WebClient.UploadString(path, json);

            JsonNotebook jsonNotebook = JsonConvert.DeserializeObject<JsonNotebook>(result);
            return new Notebook(jsonNotebook);
        }

        /// <summary>
        /// Creates a note.
        /// </summary>
        /// <param name="userID">the ID of the user that the note belongs to</param>
        /// <param name="notebookID">the ID of the notebook that the note belongs to</param>
        /// <param name="noteText">the text of the note</param>
        /// <returns>the created note</returns>
        public static Note CreateNote(string userID, string notebookID, string noteText)
        {
            if (userID == null || notebookID == null || noteText == null || userID == "" || notebookID == "" || noteText == "")
            {
                throw new ArgumentNullException();
            }

            User user = GetUser(userID);
            Notebook notebook = GetNotebook(notebookID);

            string path = "notes";
            string json = GenerateCreateNoteJson(userID, notebookID, noteText);
            string result = WebClient.UploadString(path, json);

            JsonNote jsonNote = JsonConvert.DeserializeObject<JsonNote>(result);
            return new Note(jsonNote);
        }

        /// <summary>
        /// Updates the text of the note with the given ID.
        /// </summary>
        /// <param name="noteID">the ID of the note to update</param>
        /// <param name="notebookID">the ID of the notebook that this note belongs to</param>
        /// <param name="noteText">the updated text</param>
        /// <returns>the updated note</returns>
        public static Note UpdateNote(string noteID, string notebookID, string noteText)
        {
            if (noteID == null || notebookID == null || noteText == "" || noteID == "" || notebookID == "" || noteText == "")
            {
                throw new ArgumentNullException();
            }

            Note oldNote = GetNote(noteID);

            string path = "notes/" + noteID;
            string json = GenerateUpdateNoteJson(oldNote.UserID, notebookID, noteText, oldNote.Created);
            try
            {
                WebClient.UploadString(path, "PATCH", json);
            }
            catch (WebException e)
            {
                switch (e.HResult)
                {
                    // Note not found
                    case -2146233079:
                        throw new NotFoundException("Note");
                    // Unknown exception
                    default:
                        System.Diagnostics.Debug.WriteLine(e);
                        throw;
                }
            }

            return GetNote(noteID);
        }

        /// <summary>
        /// Deletes the user with the given ID.
        /// 
        /// All notebooks for this user are also deleted. 
        /// </summary>
        /// <param name="userID">the ID of the user to delete</param>
        /// <returns>true if the user is deleted or if it does not exist</returns>
        public static bool DeleteUser(string userID)
        {
            if (userID == null || userID == "")
            {
                throw new ArgumentNullException();
            }

            try
            {
                GetUser(userID);
            }
            catch (NotFoundException)
            {
                return true;
            }

            try
            {
                foreach (Notebook notebook in GetNotebooks(userID))
                {
                    DeleteNotebook(notebook.ID);
                }
            }
            catch (NotFoundException)
            {
                // Notebook or Note not found - Unexpected!
                throw;
            }

            string path = "users/" + userID;
            string result = WebClient.UploadString(path, "DELETE", "");
            return true;
        }

        /// <summary>
        /// Deletes the notebook with the given ID.
        /// 
        /// All notes for this notebook are also deleted.
        /// </summary>
        /// <param name="notebookID">the ID of the notebook to delete</param>
        /// <returns>true if the notebook was deleted or if it does not exist</returns>
        public static bool DeleteNotebook(string notebookID)
        {
            if (notebookID == null || notebookID == "")
            {
                throw new ArgumentNullException();
            }

            try
            {
                foreach (Note note in GetNotebookNotes(notebookID))
                {
                    DeleteNote(note.ID);
                }
            }
            catch (NotFoundException)
            {
                // Note not found - Unexpected!
                throw;
            }
            string path = "notebooks/" + notebookID;
            string result = WebClient.UploadString(path, "DELETE", "");
            return true;
        }

        /// <summary>
        /// Deletes the note with the given ID.
        /// </summary>
        /// <param name="noteID">the ID of the note to delete</param>
        /// <returns>true if the note was deleted or if it does not exist</returns>
        public static bool DeleteNote(string noteID)
        {
            if (noteID == null || noteID == "")
            {
                throw new ArgumentNullException();
            }

            string path = "notes/" + noteID;
            string result = WebClient.UploadString(path, "DELETE", "");
            return true;
        }

        /// <summary>
        /// Runs a query against the database.
        /// 
        /// This is used to perform a basic filter before returning results from the Database.
        /// 
        /// This can be expanded similar to the method using GenerateFieldJson to allow for more than one filtering condition.
        /// </summary>
        /// <param name="collectionID">the ID of the collection to query</param>
        /// <param name="fieldName">the field to filter by</param>
        /// <param name="fieldValue">the required value of the field</param>
        /// <returns>the Json string response from the Database</returns>
        private static string RunQuery(string collectionID, string fieldName, string fieldValue)
        {
            string structuredQuery = "{\"structuredQuery\":{\"where\":{\"fieldFilter\":{\"field\":{\"fieldPath\":\""
                + fieldName + "\"},\"op\":\"EQUAL\",\"value\":{\""
                + FieldTypesClass.GetString(FieldTypes.StringValue) + "\":\""
                + fieldValue + "\"}}},\"from\":[{\"collectionId\":\""
                + collectionID + "\"}]}}";
            string path = ":runQuery";
            return WebClient.UploadString(path, structuredQuery);
        }

        /// <summary>
        /// Generates the Json string required for a create user request.
        /// </summary>
        private static string GenerateCreateUserJson(string userID, string displayName, string email)
        {
            string createTimeUTC = DateTime.UtcNow.ToString("o");

            List<Tuple<string, FieldTypes, string>> fieldConfigList = new List<Tuple<string, FieldTypes, string>>
            {
                new Tuple<string, FieldTypes, string>("displayName", FieldTypes.StringValue, displayName),
                new Tuple<string, FieldTypes, string>("email", FieldTypes.StringValue, email),
                new Tuple<string, FieldTypes, string>("created", FieldTypes.TimestampValue, createTimeUTC),
                new Tuple<string, FieldTypes, string>("modified", FieldTypes.TimestampValue, createTimeUTC)
            };

            return GenerateFieldsJson(fieldConfigList);
        }

        /// <summary>
        /// Generates the Json string required for a create notebook request.
        /// </summary>
        private static string GenerateCreateNotebookJson(string userID, string title, string author, string isbn, string publisher, string publishDate, string coverURL)
        {
            string createTimeUTC = DateTime.UtcNow.ToString("o");
            title = HttpUtility.JavaScriptStringEncode(title);
            author = HttpUtility.JavaScriptStringEncode(author);
            isbn = HttpUtility.JavaScriptStringEncode(isbn);
            publisher = HttpUtility.JavaScriptStringEncode(publisher);
            publishDate = HttpUtility.JavaScriptStringEncode(publishDate);

            List<Tuple<string, FieldTypes, string>> fieldConfigList = new List<Tuple<string, FieldTypes, string>>
            {
                new Tuple<string, FieldTypes, string>("userID", FieldTypes.StringValue, userID),
                new Tuple<string, FieldTypes, string>("title", FieldTypes.StringValue, title),
                new Tuple<string, FieldTypes, string>("author", FieldTypes.StringValue, author),
                new Tuple<string, FieldTypes, string>("isbn", FieldTypes.StringValue, isbn),
                new Tuple<string, FieldTypes, string>("publisher", FieldTypes.StringValue, publisher),
                new Tuple<string, FieldTypes, string>("publishDate", FieldTypes.StringValue, publishDate),
                new Tuple<string, FieldTypes, string>("coverURL", FieldTypes.StringValue, coverURL),
                new Tuple<string, FieldTypes, string>("created", FieldTypes.TimestampValue, createTimeUTC),
                new Tuple<string, FieldTypes, string>("modified", FieldTypes.TimestampValue, createTimeUTC)
            };

            return GenerateFieldsJson(fieldConfigList);
        }

        /// <summary>
        /// Generates the Json string required for a create note request.
        /// </summary>
        private static string GenerateCreateNoteJson(string userID, string notebookID, string noteText)
        {
            string createTimeUTC = DateTime.UtcNow.ToString("o");
            noteText = HttpUtility.JavaScriptStringEncode(noteText);
            List<Tuple<string, FieldTypes, string>> fieldConfigList = new List<Tuple<string, FieldTypes, string>>
            {
                new Tuple<string, FieldTypes, string>("userID", FieldTypes.StringValue, userID),
                new Tuple<string, FieldTypes, string>("notebookID", FieldTypes.StringValue, notebookID),
                new Tuple<string, FieldTypes, string>("noteText", FieldTypes.StringValue, noteText),
                new Tuple<string, FieldTypes, string>("created", FieldTypes.TimestampValue, createTimeUTC),
                new Tuple<string, FieldTypes, string>("modified", FieldTypes.TimestampValue, createTimeUTC)
            };

            return GenerateFieldsJson(fieldConfigList);
        }

        /// <summary>
        /// Generates the Json string required for an update note request.
        /// </summary>
        private static string GenerateUpdateNoteJson(string userID, string notebookID, string noteText, DateTime created)
        {
            string updateTimeUTC = DateTime.UtcNow.ToString("o");
            noteText = HttpUtility.JavaScriptStringEncode(noteText);
            List<Tuple<string, FieldTypes, string>> fieldConfigList = new List<Tuple<string, FieldTypes, string>>
            {
                new Tuple<string, FieldTypes, string>("userID", FieldTypes.StringValue, userID),
                new Tuple<string, FieldTypes, string>("notebookID", FieldTypes.StringValue, notebookID),
                new Tuple<string, FieldTypes, string>("noteText", FieldTypes.StringValue, noteText),
                new Tuple<string, FieldTypes, string>("created", FieldTypes.TimestampValue, created.ToString("o")),
                new Tuple<string, FieldTypes, string>("modified", FieldTypes.TimestampValue, updateTimeUTC)
            };

            return GenerateFieldsJson(fieldConfigList);
        }

        /// <summary>
        /// Generates the Json string required for certain API calls.
        /// </summary>
        /// <param name="fieldConfigList">a list of configurations</param>
        /// <returns></returns>
        public static string GenerateFieldsJson(List<Tuple<string, FieldTypes, string>> fieldConfigList)
        {
            if (fieldConfigList == null)
            {
                throw new ArgumentNullException("Field Config List cannot be null");
            }

            StringBuilder json = new StringBuilder();
            json.Append("{\"fields\":{");
            int extraCommaIndex = json.Length;
            if (fieldConfigList.Count > 0)
            {
                foreach (Tuple<string, FieldTypes, string> fieldConfig in fieldConfigList)
                {
                    if (fieldConfig.Item1 == null || fieldConfig.Item3 == null)
                    {
                        throw new ArgumentException("Field Config Values cannot be null");
                    }
                    json.Append(",\"");
                    json.Append(fieldConfig.Item1);
                    json.Append("\":{\"");
                    json.Append(FieldTypesClass.GetString(fieldConfig.Item2));
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

    public enum FieldTypes
    {
        StringValue = 0,
        IntegerValue = 1,
        DoubleValue = 2,
        TimestampValue = 3
    }

    public static class FieldTypesClass
    {
        public static string GetString(FieldTypes type)
        {
            switch (type)
            {
                case FieldTypes.StringValue:
                    return "stringValue";
                case FieldTypes.IntegerValue:
                    return "integerValue";
                case FieldTypes.DoubleValue:
                    return "doubleValue";
                case FieldTypes.TimestampValue:
                    return "timestampValue";
                default:
                    return "";
            }
        }
    }

    /// <summary>
    /// Base class for all Database-specific exceptions.
    /// </summary>
    public class DBException : Exception
    {
        public DBException() { }
        public DBException(string msg) : base("GFCM Exception: " + msg) { }
    }

    /// <summary>
    /// Thrown when an operation fails due to the requested entity not being found.
    /// 
    /// This is typically used in Get operations.
    /// 
    /// Eg. GetNote throws this exception if there is no Note found in the database with the given ID.
    /// </summary>
    public class NotFoundException : DBException
    {
        public NotFoundException() { }
        public NotFoundException(string msg) : base("Not Found: " + msg) { }
    }

    /// <summary>
    /// Thrown when an operation fails due to an existing entity.
    /// 
    /// Eg. A user exists with a certain ID, then a request is made to create a user with that same ID.
    /// </summary>
    public class AlreadyExistsException : DBException
    {
        public AlreadyExistsException() { }
        public AlreadyExistsException(string msg) : base("Already exists: " + msg) { }
    }
}