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
        /// The Note's body text.
        /// </summary>
        public readonly string Text;

        /// <summary>
        /// The Notebook under which this Note is filed.
        /// </summary>
        public Notebook ParentNotebook { get; private set; }

        /// <summary>
        /// The time that this Note was last modified.
        /// </summary>
        public DateTime Modified { get; private set; }

        /// <summary>
        /// The time that this Note was created.
        /// </summary>
        public DateTime Created { get; private set; }

        /// <summary>
        /// Constructs a Note from the provided information.
        /// </summary>
        /// <param name="noteID">note id</param>
        /// <param name="text">note text</param>
        /// <param name="parentNotebook">the notebook holding this note</param>
        /// <param name="modified">when this note was last modified</param>
        /// <param name="created">when this note was created</param>
        public Note(string noteID = "", string text = "", Notebook parentNotebook = null, DateTime modified = default(DateTime), DateTime created = default(DateTime))
        {
            ID = noteID;
            Text = text;
            ParentNotebook = parentNotebook;
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
            Text = jsonNote.Text;
            ParentNotebook = new Notebook(jsonNote.NotebookID);
            Created = jsonNote.Created;
            Modified = jsonNote.Modified;
        }

        /// <summary>
        /// Checks for equality between the calling Note and the passed object.
        /// </summary>
        /// <param name="other">other object.</param>
        /// <returns>true if the calling and passed objects are equal ignoring parent notebook</returns>
        public override bool Equals(object other)
        {
            var model = other as Note;
            return model != null
                && (ID == null || model.ID == null || ID == model.ID)
                && Text == model.Text;
        }

        /// <summary>
        /// Calculates hash code for the object.
        /// </summary>
        /// <returns>the calculated hash code value</returns>
        public override int GetHashCode()
        {
            var hashCode = 1394997702;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(ID);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Text);
            return hashCode;
        }

        /// <summary>
        /// Overloads the == operator.
        /// </summary>
        /// <param name="first">the first note to compare</param>
        /// <param name="second">the second note to compare</param>
        /// <returns>true if the notes are equal</returns>
        public static bool operator ==(Note first, Note second)
        {
            return first.Equals(second);
        }

        /// <summary>
        /// Overloads the != operator.
        /// </summary>
        /// <param name="first">the first note to compare</param>
        /// <param name="second">the second note to compare</param>
        /// <returns>true if the notes are not equal</returns>
        public static bool operator !=(Note first, Note second)
        {
            return !first.Equals(second);
        }
    }
}