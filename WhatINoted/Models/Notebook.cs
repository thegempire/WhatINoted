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
        public readonly string Isbn;

        /// <summary>
        /// Publisher of the Notebook.
        /// </summary>
        public readonly string Publisher;

        /// <summary>
        /// Publish Date of the Notebook.
        /// </summary>
        public readonly DateTime PublishDate;

        /// <summary>
        /// The URL for an image of the Notebook cover.
        /// </summary>
        public readonly string CoverURL;

        /// <summary>
        /// The time that this Notebook was last modified.
        /// </summary>
        public DateTime Modified { get; private set; }

        /// <summary>
        /// The time that this Notebook was created.
        /// </summary>
        public DateTime Created { get; private set; }

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
        /// <param name="coverURL">url for an image of the notebook's cover</param>
        /// <param name="modified">when this notebook was last modified</param>
        /// <param name="created">when this notebook was created</param>
        public Notebook(string notebookID, string title, string author, string isbn, string publisher, DateTime publishDate, string coverURL, DateTime modified, DateTime created)
        {
            ID = notebookID;
            Title = title;
            Author = author;
            Isbn = isbn;
            Publisher = publisher;
            PublishDate = publishDate;
            CoverURL = coverURL;
            Modified = modified;
            Created = created;
            Notes = new List<Note>();
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
            Isbn = jsonNotebook.ISBN;
            Publisher = jsonNotebook.Publisher;
            PublishDate = jsonNotebook.PublishDate;
            CoverURL = jsonNotebook.CoverURL;
            Modified = jsonNotebook.Modified;
            Created = jsonNotebook.Created;
            Notes = new List<Note>();
        }

        /// <summary>
        /// Checks for equality between the calling Notebook and the passed object.
        /// 
        /// Currently compares Title, Author, Isbn, Publisher, PublishDate, CoverURL.
        /// </summary>
        /// <param name="other">other object</param>
        /// <returns>true if the calling and passed objects are equal</returns>
        public override bool Equals(object other)
        {
            var model = other as Notebook;
            return model != null
                && Title == model.Title
                && Author == model.Author
                && Isbn == model.Isbn
                && Publisher == model.Publisher
                // Dates returned from database have rounding errors making comparison with == inconsistent
                // Eg. PublishDate == model.PublishDate does not always return true when it should
                // This should be sufficient
                && PublishDate.Year == model.PublishDate.Year
                && PublishDate.Month == model.PublishDate.Month
                && PublishDate.Day == model.PublishDate.Day
                && PublishDate.Hour == model.PublishDate.Hour
                && PublishDate.Minute == model.PublishDate.Minute
                && PublishDate.Second == model.PublishDate.Second
                && CoverURL == model.CoverURL;
        }

        /// <summary>
        /// Calculates hash code for the object.
        /// </summary>
        /// <returns>the calculated hash code value</returns>
        public override int GetHashCode()
        {
            var hashCode = -907078312;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Title);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Author);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Isbn);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Publisher);
            hashCode = hashCode * -1521134295 + PublishDate.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(CoverURL);
            return hashCode;
        }
    }
}