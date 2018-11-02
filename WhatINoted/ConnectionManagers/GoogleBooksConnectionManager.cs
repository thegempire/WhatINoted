using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WhatINoted.Models;
using System.Net;

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

        public static IList<BookSearchResultsModel> SearchVolumes(String title, String author, String publisher, IsbnModel isbn)
        {
            String jsonString;

            using (WebClient wc = new WebClient())
            {
                // Get and parse JSON
                jsonString = wc.DownloadString(BuildRequestURI(title, author, publisher, isbn.Number));
            }

            return SearchResultsFromJSON(jsonString);
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

        private static IList<BookSearchResultsModel> SearchResultsFromJSON(String jsonString)
        {
            JObject resultObject = JObject.Parse(jsonString);

            // Parse JSON into item list
            IList<JToken> resultTokens = resultObject["items"].Children().ToList();

            IList<BookSearchResultsModel> results = new List<BookSearchResultsModel>();
            foreach (JToken result in resultTokens)
            {
                BookSearchResultsModel deserializedResult = result.ToObject<BookSearchResultsModel>();
                results.Add(deserializedResult);
            }

            return results;
        }
    }
}