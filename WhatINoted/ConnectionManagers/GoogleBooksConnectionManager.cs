using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing;
using Newtonsoft.Json;

namespace WhatINoted.ConnectionManagers
{
    /// <summary>
    /// Connection Manager to handle interactions with the Google Books API.
    /// Specifically this class is used to search for book details in Google's
    /// database of titles.
    /// </summary>
    public class GoogleBooksConnectionManager
    {
        private static readonly String REQUEST_BASE = "https://www.googleapis.com/books/v1/volumes?q=";

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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="title">title to search for</param>
        /// <param name="author">author to search for</param>
        /// <param name="publisher">publisher to search for</param>
        /// <param name="isbn">isbn to search for</param>
        /// <returns></returns>
        private static String BuildRequestURI(String title, String author, String publisher, String isbn)
        {
            bool append = false;
            String requestUri = REQUEST_BASE;

            // Append title if not empty
            if (title != "")
            {
                requestUri += "intitle:" + title;
                append = true;
            }

            // Append author if not empty
            if (author != "")
            {
                if (append)
                    requestUri += "+";

                requestUri += "inauthor:" + author;
                append = true;
            }

            // Append publisher if not empty
            if (publisher != "")
            {
                if (append)
                    requestUri += "+";

                requestUri += "inpublisher:" + publisher;
                append = true;
            }

            // Append ISBN if not empty
            if (isbn != "")
            {
                if (append)
                    requestUri += "+";

                requestUri += "isbn:" + isbn;
                append = true;
            }

            return requestUri;
        }

        private static List<Models.BookSearchResultsModel> SearchResultsFromJSON(String jsonString)
        {
            var jsonObj = JsonConvert.DeserializeObject(jsonString);


        }
    }
}