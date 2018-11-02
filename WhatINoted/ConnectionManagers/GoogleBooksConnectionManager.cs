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

        public static List<BookSearchResultsModel> SearchVolumes(String title, String author, String publisher, IsbnModel isbn)
        {
            // Ensure we have some search parameter
            if ((title == null || title == "") && (author == null || author == "")
                && (publisher == null || publisher == "") && isbn == null)
                    throw new ArgumentNullException();

            String jsonString;

            using (WebClient wc = new WebClient())
            {
                // Get and parse JSON
                String reqUri;

                if (isbn != null) reqUri = BuildRequestURI("", "", "", isbn.Number);
                else reqUri = BuildRequestURI(title, author, publisher, "");

                jsonString = wc.DownloadString(reqUri);
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
            if (title != null && title != "")
            {
                requestUri += "intitle:" + title;
                append = true;
            }

            // Append author if not empty
            if (author != null && author != "")
            {
                if (append)
                    requestUri += "+";

                requestUri += "inauthor:" + author;
                append = true;
            }

            // Append publisher if not empty
            if (publisher != null && publisher != "")
            {
                if (append)
                    requestUri += "+";

                requestUri += "inpublisher:" + publisher;
                append = true;
            }

            // Append ISBN if not empty
            if (isbn != null && isbn != "")
            {
                if (append)
                    requestUri += "+";

                requestUri += "isbn:" + isbn;
                append = true;
            }

            return requestUri;
        }

        private static List<BookSearchResultsModel> SearchResultsFromJSON(String jsonString)
        {
            JObject resultObject = JObject.Parse(jsonString);

            // Check if any results were found
            if (Int32.Parse(resultObject["totalItems"].ToString()) == 0)
            {
                return new List<BookSearchResultsModel>();
            }

            // Parse JSON into item list
            List<JToken> resultTokens = resultObject["items"].Children().ToList();

            List<BookSearchResultsModel> results = new List<BookSearchResultsModel>();
            foreach (JToken result in resultTokens)
            {
                String titlePath = ".volumeInfo.title";
                String authorsPath = ".volumeInfo.authors";
                String publishDatePath = ".volumeInfo.publishedDate";
                String publisherPath = ".volumeInfo.publisher";
                String identifiersPath = ".volumeInfo.industryIdentifiers";
                String coverUriPath = ".volumeInfo.imageLinks.smallThumbnail";

                String title = "";
                String authors = "";
                String publishDate = "";
                String publisher = "";
                String isbn = "";
                String coverUri = "";

                JToken tok;

                tok = result.SelectToken(titlePath);
                if (tok != null) title = tok.ToString();

                tok = result.SelectToken(authorsPath);
                if (tok != null) authors = authorsFromJToken(tok);

                tok = result.SelectToken(publishDatePath);
                if (tok != null) publishDate = tok.ToString();

                tok = result.SelectToken(publisherPath);
                if (tok != null) publisher = tok.ToString();

                tok = result.SelectToken(identifiersPath);
                if (tok != null) isbn = IsbnFromJToken(tok);

                tok = result.SelectToken(coverUriPath);
                if (tok != null) coverUri = tok.ToString();

                results.Add(new BookSearchResultsModel(title, authors, publishDate, publisher, isbn, coverUri));
            }

            return results;
        }

        private static String authorsFromJToken(JToken authorsToken)
        {
            String authors = "";
            List<JToken> authorList = authorsToken.ToList<JToken>();
            for (int i = 0; i < authorList.Count - 1; i++)
            {
                authors += authorList[i].ToString() + ", ";
            }

            if (authorList.Count > 0)
                authors += authorList[authorList.Count - 1].ToString();

            return authors;
        }

        private static String IsbnFromJToken(JToken industryIdentifiers)
        {
            // Look for ISBN 13
            foreach (JToken identifier in industryIdentifiers.ToList<JToken>())
            {
                if (identifier.SelectToken("type").ToString() == "ISBN_13")
                {
                    return identifier.SelectToken("identifier").ToString();
                }
            }

            // Look for ISBN 10
            foreach (JToken identifier in industryIdentifiers.ToList<JToken>())
            {
                if (identifier.SelectToken("type").ToString() == "ISBN_10")
                {
                    return identifier.SelectToken("identifier").ToString();
                }
            }

            // No ISBN found.
            return null;
        }
    }
}