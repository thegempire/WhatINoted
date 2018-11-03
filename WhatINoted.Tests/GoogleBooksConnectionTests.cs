using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using WhatINoted.ConnectionManagers;
using WhatINoted.Models;

namespace WhatINoted.Tests
{
    class GoogleBooksConnectionTests
    {
        /// <summary>
        /// get a list containing all Google Books tests.
        /// </summary>
        /// <returns>list of Google Books tests</returns>
        public static List<Test> GetTests()
        {
            List<Test> tests = new List<Test>();
            tests.Add(new GoogleBooksTestConnection());
            tests.Add(new GoogleBooksTestSearchVolumes());
            return tests;
        }

        /// <summary>
        /// Tests the connection to the Google Books API. Returns true if any response is received.
        /// </summary>
        private class GoogleBooksTestConnection : Test
        {
            public bool Run(StreamWriter sw)
            {
                try
                {
                    GoogleBooksConnectionManager.SearchVolumes("Any Title", "Any Author", "Any Publisher", null);
                }
                catch
                {
                    sw.WriteLine("GoogleBooksTestConnection failed: No response from API.");
                    return false;
                }

                return true;
            }
        }

        /// <summary>
        /// Tests the SearchVolumes method using the Title as a parameter,
        /// the Author as a parameter, the Publisher as a parameter,
        /// the Isbn as a parameter, null as all of the parameters, and
        /// empty as all of the parameters.
        /// Returns true if all of the method specific tests pass.
        /// </summary>
        private class GoogleBooksTestSearchVolumes : Test
        {
            public bool Run(StreamWriter sw)
            {
                bool passed = SearchVolumesTitle(sw);
                passed = passed && SearchVolumesAuthor(sw);
                passed = passed && SearchVolumesPublisher(sw);
                passed = passed && SearchVolumesIsbn(sw);
                passed = passed && SearchVolumesTitleAuthor(sw);
                passed = passed && SearchVolumesTitlePublisher(sw);
                passed = passed && SearchVolumesTitleIsbn(sw);
                passed = passed && SearchVolumesAuthorPublisher(sw);
                passed = passed && SearchVolumesAuthorIsbn(sw);
                passed = passed && SearchVolumesPublisherIsbn(sw);
                passed = passed && SearchVolumesTitleAuthorPublisher(sw);
                passed = passed && SearchVolumesTitleAuthorIsbn(sw);
                passed = passed && SearchVolumesTitlePublisherIsbn(sw);
                passed = passed && SearchVolumesAuthorPublisherIsbn(sw);
                passed = passed && SearchVolumesAll(sw);
                passed = passed && SearchVolumesNullParams(sw);
                passed = passed && SearchVolumesEmptyParams(sw);

                return passed;
            }

            /// <summary>
            /// Tests the SearchVolumes method using the Title as the parameter. Returns true if any result is returned.
            /// </summary>
            private bool SearchVolumesTitle(StreamWriter sw)
            {
                try
                {
                    List<BookSearchResultsModel> results = GoogleBooksConnectionManager.SearchVolumes("Harry Potter", null, null, null);
                    if (results.Count <= 0)
                    {
                        sw.WriteLine("GoogleBooksTestSearchVolumes - SearchVolumesTitle failed: No results.");
                        return false;
                    }
                }
                catch
                {
                    sw.WriteLine("GoogleBooksTestSearchVolumes - SearchVolumesTitle failed: Unexpected exception.");
                    return false;
                }

                return true;
            }


            /// <summary>
            /// Tests the SearchVolumes method using the Author as a parameter. Returns true if any result is returned.
            /// </summary>
            private bool SearchVolumesAuthor(StreamWriter sw)
            {
                try
                {
                    List<BookSearchResultsModel> results = GoogleBooksConnectionManager.SearchVolumes(null, "J.K. Rowling", null, null);
                    if (results.Count <= 0)
                    {
                        sw.WriteLine("GoogleBooksTestSearchVolumes - SearchVolumesAuthor failed: No results.");
                        return false;
                    }
                }
                catch
                {
                    sw.WriteLine("GoogleBooksTestSearchVolumes - SearchVolumesAuthor failed: Unexpected exception.");
                    return false;
                }

                return true;
            }


            /// <summary>
            /// Tests the SearchVolumes method using the Publisher as a parameter. Returns true if any result is returned.
            /// </summary>
            private bool SearchVolumesPublisher(StreamWriter sw)
            {
                try
                {
                    List<BookSearchResultsModel> results = GoogleBooksConnectionManager.SearchVolumes(null, null, "Pottermore Publishing", null);
                    if (results.Count <= 0)
                    {
                        sw.WriteLine("GoogleBooksTestSearchVolumes - SearchVolumesPublisher failed: No results.");
                        return false;
                    }
                }
                catch
                {
                    sw.WriteLine("GoogleBooksTestSearchVolumes - SearchVolumesPublisher failed: Unexpected exception.");
                    return false;
                }

                return true;
            }


            /// <summary>
            /// Tests the SearchVolumes method using the Isbn as a parameter. Returns true if any result is returned.
            /// </summary>
            private bool SearchVolumesIsbn(StreamWriter sw)
            {
                try
                {
                    List<BookSearchResultsModel> results = GoogleBooksConnectionManager.SearchVolumes(null, null, null, new IsbnModel("9781781100226"));
                    if (results.Count <= 0)
                    {
                        sw.WriteLine("GoogleBooksTestSearchVolumes - SearchVolumesIsbn failed: No results.");
                        return false;
                    }
                }
                catch (ArgumentException e)
                {
                    sw.WriteLine("GoogleBooksTestSearchVolumes - SearchVolumesIsbn failed: ISBN invalid.");
                }
                catch
                {
                    sw.WriteLine("GoogleBooksTestSearchVolumes - SearchVolumesIsbn failed: Unexpected exception.");
                    return false;
                }

                return true;
            }


            /// <summary>
            /// Tests the SearchVolumes method using the Title and the Author as parameters.
            /// Returns true if any result is returned.
            /// </summary>
            private bool SearchVolumesTitleAuthor(StreamWriter sw)
            {
                try
                {
                    List<BookSearchResultsModel> results = GoogleBooksConnectionManager.SearchVolumes("Harry Potter", "J.K. Rowling", null, null);
                    if (results.Count <= 0)
                    {
                        sw.WriteLine("GoogleBooksTestSearchVolumes - SearchVolumesTitleAuthor failed: No results.");
                        return false;
                    }
                }
                catch
                {
                    sw.WriteLine("GoogleBooksTestSearchVolumes - SearchVolumesTitleAuthor failed: Unexpected exception.");
                    return false;
                }

                return true;
            }


            /// <summary>
            /// Tests the SearchVolumes method using the Title and the Publisher as parameters.
            /// Returns true if any result is returned.
            /// </summary>
            private bool SearchVolumesTitlePublisher(StreamWriter sw)
            {
                try
                {
                    List<BookSearchResultsModel> results = GoogleBooksConnectionManager.SearchVolumes("Harry Potter", null, "Pottermore Publishing", null);
                    if (results.Count <= 0)
                    {
                        sw.WriteLine("GoogleBooksTestSearchVolumes - SearchVolumesTitlePublisher failed: No results.");
                        return false;
                    }
                }
                catch
                {
                    sw.WriteLine("GoogleBooksTestSearchVolumes - SearchVolumesTitlePublisher failed: Unexpected exception.");
                    return false;
                }

                return true;
            }


            /// <summary>
            /// Tests the SearchVolumes method using the Title and Isbn as parameters. 
            /// Returns true if any result is returned.
            /// </summary>
            private bool SearchVolumesTitleIsbn(StreamWriter sw)
            {
                try
                {
                    List<BookSearchResultsModel> results = GoogleBooksConnectionManager.SearchVolumes("Harry Potter", null, null, new IsbnModel("9781781100226"));
                    if (results.Count <= 0)
                    {
                        sw.WriteLine("GoogleBooksTestSearchVolumes - SearchVolumesTitleIsbn failed: No results.");
                        return false;
                    }
                }
                catch
                {
                    sw.WriteLine("GoogleBooksTestSearchVolumes - SearchVolumesTitleIsbn failed: Unexpected exception.");
                    return false;
                }

                return true;
            }


            /// <summary>
            /// Tests the SearchVolumes method using the Author and Publisher as parameters. 
            /// Returns true if any result is returned.
            /// </summary>
            private bool SearchVolumesAuthorPublisher(StreamWriter sw)
            {
                try
                {
                    List<BookSearchResultsModel> results = GoogleBooksConnectionManager.SearchVolumes(null, "J.K. Rowling", "Pottermore Publishing", null);
                    if (results.Count <= 0)
                    {
                        sw.WriteLine("GoogleBooksTestSearchVolumes - SearchVolumesAuthorPublisher failed: No results.");
                        return false;
                    }
                }
                catch
                {
                    sw.WriteLine("GoogleBooksTestSearchVolumes - SearchVolumesAuthorPublisher failed: Unexpected exception.");
                    return false;
                }

                return true;
            }


            /// <summary>
            /// Tests the SearchVolumes method using the Author and Isbn as parameters. 
            /// Returns true if any result is returned.
            /// </summary>
            private bool SearchVolumesAuthorIsbn(StreamWriter sw)
            {
                try
                {
                    List<BookSearchResultsModel> results = GoogleBooksConnectionManager.SearchVolumes(null, "J.K. Rowling", null, new IsbnModel("9781781100226"));
                    if (results.Count <= 0)
                    {
                        sw.WriteLine("GoogleBooksTestSearchVolumes - SearchVolumesAuthorIsbn failed: No results.");
                        return false;
                    }
                }
                catch
                {
                    sw.WriteLine("GoogleBooksTestSearchVolumes - SearchVolumesAuthorIsbn failed: Unexpected exception.");
                    return false;
                }

                return true;
            }


            /// <summary>
            /// Tests the SearchVolumes method using the Publisher and Isbn as parameters. 
            /// Returns true if any result is returned.
            /// </summary>
            private bool SearchVolumesPublisherIsbn(StreamWriter sw)
            {
                try
                {
                    List<BookSearchResultsModel> results = GoogleBooksConnectionManager.SearchVolumes(null, null, "Pottermore Publishing", new IsbnModel("9781781100226"));
                    if (results.Count <= 0)
                    {
                        sw.WriteLine("GoogleBooksTestSearchVolumes - SearchVolumesPublisherIsbn failed: No results.");
                        return false;
                    }
                }
                catch
                {
                    sw.WriteLine("GoogleBooksTestSearchVolumes - SearchVolumesPublisherIsbn failed: Unexpected exception.");
                    return false;
                }

                return true;
            }


            /// <summary>
            /// Tests the SearchVolumes method using the Title, Author, and Publisher as parameters. 
            /// Returns true if any result is returned.
            /// </summary>
            private bool SearchVolumesTitleAuthorPublisher(StreamWriter sw)
            {
                try
                {
                    List<BookSearchResultsModel> results = GoogleBooksConnectionManager.SearchVolumes("Harry Potter", "J.K. Rowling", "Pottermore Publishing", null);
                    if (results.Count <= 0)
                    {
                        sw.WriteLine("GoogleBooksTestSearchVolumes - SearchVolumesTitleAuthorPublisher failed: No results.");
                        return false;
                    }
                }
                catch
                {
                    sw.WriteLine("GoogleBooksTestSearchVolumes - SearchVolumesTitleAuthorPublisher failed: Unexpected exception.");
                    return false;
                }

                return true;
            }


            /// <summary>
            /// Tests the SearchVolumes method using the Title, Author, and Isbn as parameters. 
            /// Returns true if any result is returned.
            /// </summary>
            private bool SearchVolumesTitleAuthorIsbn(StreamWriter sw)
            {
                try
                {
                    List<BookSearchResultsModel> results = GoogleBooksConnectionManager.SearchVolumes("Harry Potter", "J.K. Rowling", null, new IsbnModel("9781781100226"));
                    if (results.Count <= 0)
                    {
                        sw.WriteLine("GoogleBooksTestSearchVolumes - SearchVolumesTitleAuthorIsbn failed: No results.");
                        return false;
                    }
                }
                catch
                {
                    sw.WriteLine("GoogleBooksTestSearchVolumes - SearchVolumesTitleAuthorIsbn failed: Unexpected exception.");
                    return false;
                }

                return true;
            }


            /// <summary>
            /// Tests the SearchVolumes method using the Title, Publisher, and Isbn as parameters. 
            /// Returns true if any result is returned.
            /// </summary>
            private bool SearchVolumesTitlePublisherIsbn(StreamWriter sw)
            {
                try
                {
                    List<BookSearchResultsModel> results = GoogleBooksConnectionManager.SearchVolumes("Harry Potter", null, "Pottermore Publishing", new IsbnModel("9781781100226"));
                    if (results.Count <= 0)
                    {
                        sw.WriteLine("GoogleBooksTestSearchVolumes - SearchVolumesTitlePublisherIsbn failed: No results.");
                        return false;
                    }
                }
                catch
                {
                    sw.WriteLine("GoogleBooksTestSearchVolumes - SearchVolumesTitlePublisherIsbn failed: Unexpected exception.");
                    return false;
                }

                return true;
            }


            /// <summary>
            /// Tests the SearchVolumes method using the Author, Publisher, and Isbn as parameters. 
            /// Returns true if any result is returned.
            /// </summary>
            private bool SearchVolumesAuthorPublisherIsbn(StreamWriter sw)
            {
                try
                {
                    List<BookSearchResultsModel> results = GoogleBooksConnectionManager.SearchVolumes(null, "J.K. Rowling", "Pottermore Publishing", new IsbnModel("9781781100226"));
                    if (results.Count <= 0)
                    {
                        sw.WriteLine("GoogleBooksTestSearchVolumes - SearchVolumesAuthorPublisherIsbn failed: No results.");
                        return false;
                    }
                }
                catch
                {
                    sw.WriteLine("GoogleBooksTestSearchVolumes - SearchVolumesAuthorPublisherIsbn failed: Unexpected exception.");
                    return false;
                }

                return true;
            }


            /// <summary>
            /// Tests the SearchVolumes method using all of the parameters. 
            /// Returns true if any result is returned.
            /// </summary>
            private bool SearchVolumesAll(StreamWriter sw)
            {
                try
                {
                    List<BookSearchResultsModel> results = GoogleBooksConnectionManager.SearchVolumes("Harry Potter", "J.K. Rowling", "Pottermore Publishing", new IsbnModel("9781781100226"));
                    if (results.Count <= 0)
                    {
                        sw.WriteLine("GoogleBooksTestSearchVolumes - SearchVolumesAll failed: No results.");
                        return false;
                    }
                }
                catch
                {
                    sw.WriteLine("GoogleBooksTestSearchVolumes - SearchVolumesAll failed: Unexpected exception.");
                    return false;
                }

                return true;
            }


            /// <summary>
            /// Tests the SearchVolumes method using all null parameters. Returns true if an ArgumentNullException occurs.
            /// </summary>
            private bool SearchVolumesNullParams(StreamWriter sw)
            {
                try
                {
                    GoogleBooksConnectionManager.SearchVolumes(null, null, null, null);
                }
                catch (ArgumentNullException e)
                {
                    return true;
                }
                catch
                {
                    sw.WriteLine("GoogleBooksTestSearchVolumes - SearchVolumesNullParams failed: Unexpected exception.");
                }

                sw.WriteLine("GoogleBooksTestSearchVolumes - SearchVolumesNullParams failed: Expected an ArgumentNullException.");
                return false;
            }


            /// <summary>
            /// Tests the SearchVolumes method using all empty parameters. Returns true if an ArgumentNullException occurs.
            /// </summary>
            private bool SearchVolumesEmptyParams(StreamWriter sw)
            {
                try
                {
                    GoogleBooksConnectionManager.SearchVolumes("", "", "", null);
                }
                catch (ArgumentNullException e)
                {
                    return true;
                }
                catch
                {
                    sw.WriteLine("GoogleBooksTestSearchVolumes - SearchVolumesEmptyParams failed: Unexpected exception.");
                }

                sw.WriteLine("GoogleBooksTestSearchVolumes - SearchVolumesEmptyParams failed: Expected an ArgumentNullException.");
                return false;
            }
        }
    }
}
