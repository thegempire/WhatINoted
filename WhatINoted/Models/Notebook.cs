using System;
using System.Collections.Generic;

namespace WhatINoted.Models
{
    /// <summary>
    /// A Notebook that acts as a holder for a collection of Notes.
    /// </summary>
    public class Notebook
    {
        /// <summary>
        /// Notebook's database ID.
        /// </summary>
        public readonly string ID;

        /// <summary>
        /// Title of the Notebook.
        /// </summary>
        public readonly string Title;

        /// <summary>
        /// Author of the Notebook.
        /// </summary>
        public readonly string Author;

        /// <summary>
        /// ISBN of the Notebook.
        /// </summary>
        public readonly IsbnModel Isbn;

        /// <summary>
        /// Publisher of the Notebook.
        /// </summary>
        public readonly string Publisher;

        /// <summary>
        /// Publish Date of the Notebook.
        /// </summary>
        public readonly DateTime PublishDate;

        /// <summary>
        /// List of Notes filed under the Notebook.
        /// </summary>
        public List<Note> Notes;

        /// <summary>
        /// Constructs a Notebook from the provided information.
        /// </summary>
        /// <param name="notebookID">notebook id</param>
        /// <param name="title">notebook title</param>
        /// <param name="author">notebook author</param>
        /// <param name="isbn">notebook isbn</param>
        /// <param name="publisher">notebook publisher</param>
        /// <param name="publishDate">notebook publishing date</param>
        /// <param name="notes">list of notebook's notes</param>
        public Notebook(string notebookID = "", string title = "", string author = "", string isbn = "", string publisher = "", DateTime publishDate = default(DateTime), List<Note> notes = null)
        {
            ID = notebookID;
            Title = title;
            Author = author;
            Isbn = new IsbnModel(isbn);
            Publisher = publisher;
            PublishDate = publishDate;
            Notes = notes ?? new List<Note>();
        }

        /// <summary>
        /// Constructs a Notebook from the provided Json object.
        /// </summary>
        /// <param name="jsonNotebook">Json object containing information related to a Notebook</param>
        public Notebook(JsonNotebook jsonNotebook)
        {
            ID = jsonNotebook.ID;
            Title = jsonNotebook.Title;
            Author = jsonNotebook.Author;
            Isbn = new IsbnModel(jsonNotebook.ISBN);
            Publisher = jsonNotebook.Publisher;
            PublishDate = jsonNotebook.PublishDate;
            Notes = new List<Note>();
        }

        /// <summary>
        /// Checks for equality between the calling Notebook and the passed object.
        /// </summary>
        /// <param name="other">other object</param>
        /// <returns>true if the calling and passed objects are equal ignoring note list</returns>
        public override bool Equals(object other)
        {
            var model = other as Notebook;
            return model != null
                && (ID == null || model.ID == null || ID == model.ID)
                && Title == model.Title
                && Author == model.Author
                && Isbn == model.Isbn
                && Publisher == model.Publisher
                && PublishDate == model.PublishDate;
        }

        /// <summary>
        /// Calculates hash code for the object.
        /// </summary>
        /// <returns>the calculated hash code value</returns>
        public override int GetHashCode()
        {
            var hashCode = -907078312;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(ID);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Title);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Author);
            hashCode = hashCode * -1521134295 + EqualityComparer<IsbnModel>.Default.GetHashCode(Isbn);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Publisher);
            hashCode = hashCode * -1521134295 + PublishDate.GetHashCode();
            return hashCode;
        }

        /// <summary>
        /// Overloads the == operator.
        /// </summary>
        /// <param name="first">the first notebook to compare</param>
        /// <param name="second">the second notebook to compare</param>
        /// <returns>true if the notebooks are equal</returns>
        public static bool operator ==(Notebook first, Notebook second)
        {
            return first.Equals(second);
        }

        /// <summary>
        /// Overloads the != operator.
        /// </summary>
        /// <param name="first">the first notebook to compare</param>
        /// <param name="second">the second notebook to compare</param>
        /// <returns>true if the notebooks are not equal</returns>
        public static bool operator !=(Notebook first, Notebook second)
        {
            return !first.Equals(second);
        }
    }
}