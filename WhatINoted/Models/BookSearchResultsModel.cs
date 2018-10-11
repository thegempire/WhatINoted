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
        /// Author of the book.
        /// </summary>
        public readonly String Author;

        /// <summary>
        /// ISBN of the book.
        /// </summary>
        public readonly String ISBN;

        /// <summary>
        /// Construct a BookSearchResultsModel with the given characteristics.
        /// </summary>
        /// <param name="Title">title of the book</param>
        /// <param name="Author">author of the book</param>
        /// <param name="ISBN">isbn of the book</param>
        public BookSearchResultsModel(String Title, String Author, String ISBN)
        {
            this.Title = Title;
            this.Author = Author;
            this.ISBN = ISBN;
        }
    }
}