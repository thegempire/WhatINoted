using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WhatINoted.Models
{
    /// <summary>
    /// Stores information respresenting the results of a book search.
    /// </summary>
    public class BookSearchResultsModel
    {
        /// <summary>
        /// Title of the book.
        /// </summary>
        public readonly String Title;

        /// <summary>
        /// Authors of the book.
        /// </summary>
        public readonly String Authors;

        /// <summary>
        /// Publisher of the book.
        /// </summary>
        public readonly String Publisher;

        /// <summary>
        /// ISBN of the book.
        /// </summary>
        public readonly IsbnModel ISBN;

        /// <summary>
        /// Publish Date of the book.
        /// </summary>
        public readonly DateTime PublishDate;

        /// <summary>
        /// Cover URL of the book.
        /// </summary>
        public readonly String CoverURL;

        /// <summary>
        /// Construct a BookSearchResultsModel with the given characteristics.
        /// </summary>
        /// <param name="Title">title of the book</param>
        /// <param name="Authors">author of the book</param>
        /// <param name="Publisher">publisher of the book</param>
        /// <param name="ISBN">isbn of the book</param>
        /// <param name="PublishDate">publish date of the book</param>
        /// <param name="CoverURL">cover url of the book</param>
        public BookSearchResultsModel(String Title, String Authors, String Publisher, IsbnModel ISBN, DateTime PublishDate, String CoverURL)
        {
            this.Title = Title;
            this.Authors = Authors;
            this.Publisher = Publisher;
            this.ISBN = ISBN;
            this.PublishDate = PublishDate;
            this.CoverURL = CoverURL;
        }
    }
}