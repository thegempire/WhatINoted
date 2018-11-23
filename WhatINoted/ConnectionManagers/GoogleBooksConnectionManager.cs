using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WhatINoted.Models;
using System.Net;
using System.Text;

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

        private static readonly String TITLE_JSON_PATH = ".volumeInfo.title";

        private static readonly String AUTHORS_JSON_PATH = ".volumeInfo.authors";

        private static readonly String PUBLISHER_JSON_PATH = ".volumeInfo.publisher";

        private static readonly String PUBLISH_DATE_JSON_PATH = ".volumeInfo.publishedDate";

        private static readonly String IDENTIFIERS_JSON_PATH = ".volumeInfo.industryIdentifiers";

        private static readonly String COVER_IMAGE_JSON_PATH = ".volumeInfo.imageLinks.smallThumbnail";


        /// <summary>
        /// Search Google Books for a list of volumes matching the given parameters.
        /// </summary>
        /// 
        /// <param name="title">title to search for</param>
        /// <param name="author">author to search for</param>
        /// <param name="publisher">publisher to search for</param>
        /// <param name="isbn">isbn to search for (will override other parameters)</param>
        /// 
        /// <returns>A list of volumes retrieved from Google Books</returns>
        public static List<BookSearchResultsModel> SearchVolumes(String title, String author, String publisher, String isbn)
        {
            // Ensure we have some search parameter
            if ((title == null || title == "") && (author == null || author == "")
                && (publisher == null || publisher == "") && (isbn == null || isbn == ""))
                    throw new ArgumentNullException();

            // Build a request based on the provided parameters
            String reqUri = (isbn == null) ? BuildRequestURI(title, author, publisher, null)
                : BuildRequestURI("", "", "", isbn);

            // Retrieve and parse the response JSON
            using (WebClient wc = new WebClient())
            {
                return SearchResultsFromJSON(wc.DownloadString(reqUri));
            }

        }

        /// <summary>
        /// Build a URI which can be queried to retrieve a JSON string from
        /// Google Books containing a list of relevant volumes.
        /// </summary>
        /// 
        /// <param name="title">title to search for</param>
        /// <param name="author">author to search for</param>
        /// <param name="publisher">publisher to search for</param>
        /// <param name="isbn">isbn to search for, this will override other parameters</param>
        /// 
        /// <returns>the URI with which to query Google Books</returns>
        private static String BuildRequestURI(String title, String author, String publisher, String isbn)
        {
            bool append = false;
            String requestUri = REQUEST_BASE;

            // Append isbn and return if provided
            if (isbn != null && isbn != "")
                return requestUri + "isbn:" + isbn;

            // Append title if provided
            if (title != null && title != "")
            {
                requestUri += "intitle:" + title;
                append = true;
            }

            // Append author if provided
            if (author != null && author != "")
            {
                if (append)
                    requestUri += "+";

                requestUri += "inauthor:" + author;
                append = true;
            }

            // Append publisher if provided
            if (publisher != null && publisher != "")
            {
                if (append)
                    requestUri += "+";

                requestUri += "inpublisher:" + publisher;
                append = true;
            }

            return requestUri;
        }


        private static List<BookSearchResultsModel> SearchResultsFromJSON(String jsonString)
        {
            //Change the character encoding
            jsonString = Encoding.UTF8.GetString(Encoding.Convert(Encoding.UTF8, Encoding.Default, Encoding.UTF8.GetBytes(jsonString)));

            JObject resultObject = JObject.Parse(jsonString);

            // Check if any results were found
            if (Int32.Parse(resultObject["totalItems"].ToString()) == 0)
            {
                return new List<BookSearchResultsModel>();
            }

            // Parse JSON into list of tokens
            List<JToken> resultTokens = resultObject["items"].Children().ToList();

            // Generate a list of search results
            List<BookSearchResultsModel> results = new List<BookSearchResultsModel>();
            foreach (JToken result in resultTokens)
            {
                String title = "";
                String authors = "";
                String publishDate = "";
                String publisher = "";
                String isbn = "";
                String coverUri = "";

                JToken token;

                token = result.SelectToken(TITLE_JSON_PATH);
                if (token != null) title = token.ToString();

                token = result.SelectToken(AUTHORS_JSON_PATH);
                if (token != null) authors = authorsFromJToken(token);

                token = result.SelectToken(PUBLISH_DATE_JSON_PATH);
                if (token != null) publishDate = token.ToString();

                token = result.SelectToken(PUBLISHER_JSON_PATH);
                if (token != null) publisher = token.ToString();

                token = result.SelectToken(IDENTIFIERS_JSON_PATH);
                if (token != null) isbn = IsbnFromJToken(token);

                token = result.SelectToken(COVER_IMAGE_JSON_PATH);
                if (token != null) coverUri = token.ToString();

                results.Add(new BookSearchResultsModel(title, authors, publisher, isbn, publishDate, coverUri));
            }

            return results;
        }

        /// <summary>
        /// Parse a string describing a list of authors from a given JSON token.
        /// </summary>
        /// 
        /// <param name="authorsToken"></param>
        /// 
        /// <returns>a string listing authors</returns>
        private static String authorsFromJToken(JToken authorsToken)
        {
            String authors = "";
            List<JToken> authorList = authorsToken.ToList<JToken>();

            for (int i = 0; i < authorList.Count - 1; i++)
                authors += authorList[i].ToString() + ", ";

            if (authorList.Count > 0)
                authors += authorList[authorList.Count - 1].ToString();

            return authors;
        }

        /// <summary>
        /// Parse an ISBN (10-digit or 13-digit) from a given JSON token.
        /// </summary>
        /// 
        /// <param name="industryIdentifiers">JSON token to parse</param>
        /// 
        /// <returns>ISBN as string</returns>
        private static String IsbnFromJToken(JToken industryIdentifiers)
        {
            foreach(JToken token in industryIdentifiers.ToList<JToken>())
            {
                if (token.SelectToken("type").ToString() == "ISBN_13")
                    return token.SelectToken("identifier").ToString();

                if (token.SelectToken("type").ToString() == "ISBN_10")
                    return token.SelectToken("identifier").ToString();
            }

            // No ISBN found
            return null;
        }
    }
}