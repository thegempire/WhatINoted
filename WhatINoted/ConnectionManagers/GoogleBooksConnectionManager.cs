using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing;

namespace WhatINoted.ConnectionManagers
{
    public class GoogleBooksConnectionManager
    {
        private static readonly String RedirectURI = ""; // TODO Get a redirect URI for Google Books. Maybe read this from a config file?

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