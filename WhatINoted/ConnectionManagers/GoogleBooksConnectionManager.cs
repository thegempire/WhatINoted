using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing;

namespace WhatINoted.ConnectionManagers
{
    /// <summary>
    /// Connection Manager to handle interactions with the Google Books API.
    /// Specifically this class is used to search for book details in Google's
    /// database of titles.
    /// </summary>
    public class GoogleBooksConnectionManager
    {
        private static readonly String API_KEY = ""; // TODO Get an API key for Google Books. Maybe read this from a config file? 

        /// <summary>
        /// Redirect URI to which Google Books will send responses.
        /// </summary>
        private static readonly String RedirectURI = ""; // TODO Set up a redirect URI for Google Books. Maybe read this from a config file?

        /// <summary>
        /// Searches for books by ISBN via the Google Books API.
        /// </summary>
        /// <returns>A list of matching books.</returns>
        /// <param name="isbn">ISBN.</param>
        public static List<Models.BookSearchResultsModel> SearchByISBN(Models.IsbnModel isbn)
        {
            return new List<Models.BookSearchResultsModel>();
        }

        /// <summary>
        /// Searches for books by title and author via the Google Books API.
        /// </summary>
        /// <returns>A list of matching books.</returns>
        /// <param name="title">Title.</param>
        /// <param name="author">Author.</param>
        public static List<Models.BookSearchResultsModel> SearchByDetails(String title, String author)
        {
            return new List<Models.BookSearchResultsModel>();
        }
    }
}