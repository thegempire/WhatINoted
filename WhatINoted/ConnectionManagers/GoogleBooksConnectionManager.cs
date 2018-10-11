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

        // TODO return BookSearchResults
        // TODO parameter: IsbnModel
        public static void SearchByISBN()
        {

        }

        // TODO return BookSearchResults
        public static void SearchByDetails(String title, String author)
        {

        }
    }
}