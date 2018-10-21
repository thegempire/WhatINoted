using System;
using System.Collections.Generic;

namespace WhatINoted.Models
{
    /// <summary>
    /// A Note that stores note text.
    /// </summary>
    public class Note
    {
        /// <summary>
        /// The Note's database ID.
        /// </summary>
        public readonly string ID;

        /// <summary>
        /// The ID of the User that the Note belongs to.
        /// </summary>
        public string UserID;

        /// <summary>
        /// The ID of the Notebook that the Note belongs to.
        /// </summary>
        public string NotebookID;

        /// <summary>
        /// The Note's body text.
        /// </summary>
        public readonly string Text;

        /// <summary>
        /// The time that this Note was last modified.
        /// </summary>
        public DateTime Modified { get; private set; }

        /// <summary>
        /// The time that this Note was created.
        /// </summary>
        public DateTime Created { get; private set; }

        /// <summary>
        /// The Notebook under which this Note is filed.
        /// </summary>
        public Notebook ParentNotebook { get; private set; }

        /// <summary>
        /// Constructs a Note from the provided information.
        /// </summary>
        /// <param name="noteID">note id</param>
        /// <param name="userID">the ID of the user holding this note</param>
        /// <param name="notebookID">the ID of the notebook holding this note</param>
        /// <param name="text">note text</param>
        /// <param name="modified">when this note was last modified</param>
        /// <param name="created">when this note was created</param>
        public Note(string noteID, string userID, string notebookID, string text, DateTime modified, DateTime created)
        {
            ID = noteID;
            UserID = userID;
            NotebookID = notebookID;
            Text = text;
            Modified = modified;
            Created = created;
        }

        /// <summary>
        /// Constructs a Note from the provided Json object.
        /// </summary>
        /// <param name="jsonNote">Json object containing information related to a Note</param>
        public Note(JsonNote jsonNote)
        {
            ID = jsonNote.ID;
            UserID = jsonNote.UserID;
            NotebookID = jsonNote.NotebookID;
            Text = jsonNote.Text;
            Created = jsonNote.Created;
            Modified = jsonNote.Modified;
        }

        /// <summary>
        /// Checks for equality between the calling Note and the passed object.
        /// 
        /// Currently compares UserID, NotebookID, Text.
        /// </summary>
        /// <param name="other">other object.</param>
        /// <returns>true if the calling and passed objects are equal</returns>
        public override bool Equals(object other)
        {
            var model = other as Note;
            return model != null
                && UserID == model.UserID
                && NotebookID == model.NotebookID
                && Text == model.Text;
        }

        /// <summary>
        /// Calculates hash code for the object.
        /// </summary>
        /// <returns>the calculated hash code value</returns>
        public override int GetHashCode()
        {
            var hashCode = 1394997702;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(UserID);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(NotebookID);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Text);
            return hashCode;
        }
    }
}