using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Drawing;
using WhatINoted.ConnectionManagers;

namespace WhatINoted.Tests
{
    class GoogleVisionConnectionTests
    {
        /// <summary>
        /// get a list containing all Google Vision tests.
        /// </summary>
        /// <returns>list of Google Vision tests</returns>
        public static List<Test> GetTests()
        {
            List<Test> tests = new List<Test>();
            tests.Add(new GoogleVisionTestConnection());
            tests.Add(new GoogleVisionTestNullImage());
            tests.Add(new GoogleVisionTestExtractText1());
            tests.Add(new GoogleVisionTestExtractText2());
            tests.Add(new GoogleVisionTestExtractText3());
            return tests;
        }

        /// <summary>
        /// Tests the connection the the Google Vision API. Returns true as
        /// long as any response is received.
        /// </summary>
        private class GoogleVisionTestConnection : Test
        {
            private static readonly System.Drawing.Image TEST_INPUT_IMAGE = Image.FromFile("Resources\\test_text_1.jpg");

            public bool Run(StreamWriter sw)
            {
                using (MemoryStream mStream = new MemoryStream()) {
                    TEST_INPUT_IMAGE.Save(mStream, System.Drawing.Imaging.ImageFormat.Png);
                    if (GoogleVisionConnectionManager.ExtractText(mStream.ToArray()) == null)
                    {
                        sw.WriteLine("GoogleVisionTestConnection failed: No response from API.");
                        return false;
                    }
                }

                return true;
            }
        }

        /// <summary>
        /// Tests the behaviour of the ExtractText method when the input image
        /// is null. Test passes if a NullReferenceException is thrown and
        /// caught. Fails otherwise.
        /// </summary>
        private class GoogleVisionTestNullImage : Test
        {
            public bool Run(StreamWriter sw)
            {
                try
                {
                    GoogleVisionConnectionManager.ExtractText(null);
                }
                catch (NullReferenceException e)
                {
                    return true;
                }

                sw.WriteLine("GoogleVisionTestNullImage: Call to extractText with null image did not result in exception.");
                return false;
            }
        }

        /// <summary>
        /// Tests the extraction of text from a sample image.
        /// </summary>
        private class GoogleVisionTestExtractText1 : Test
        {

            private static readonly System.Drawing.Image TEST_INPUT_IMAGE = Image.FromFile("Resources\\test_text_1.jpg");
            private static readonly String TEST_RESULT_TEXT = "The quick brown fox jumps over the lazy dog.";

            public bool Run(StreamWriter sw)
            {
                using (MemoryStream mStream = new MemoryStream()) {
                    TEST_INPUT_IMAGE.Save(mStream, System.Drawing.Imaging.ImageFormat.Png);
                    if (GoogleVisionConnectionManager.ExtractText(mStream.ToArray()) != TEST_RESULT_TEXT)
                    {
                        sw.WriteLine("GoogleVisionTestExtractText1 failed: Extracted text does not match expected result.");
                        return false;
                    }
                }

                return true;
            }
        }

        /// <summary>
        /// Tests the extraction of text from a sample image.
        /// </summary>
        private class GoogleVisionTestExtractText2 : Test
        {

            private static readonly Image TEST_INPUT_IMAGE = Image.FromFile("Resources\\test_text_2.jpg");
            private static readonly String TEST_RESULT_TEXT = "OUT OF STOCK";

            public bool Run(StreamWriter sw)
            {
                using (MemoryStream mStream = new MemoryStream()) {
                    TEST_INPUT_IMAGE.Save(mStream, System.Drawing.Imaging.ImageFormat.Png);
                    if (GoogleVisionConnectionManager.ExtractText(mStream.ToArray()) != TEST_RESULT_TEXT)
                    {
                        sw.WriteLine("GoogleVisionTestExtractText2 failed: Extracted text does not match expected result.");
                        return false;
                    }
                }

                return true;
            }
        }

        /// <summary>
        /// Tests the extraction of text from a sample image.
        /// </summary>
        private class GoogleVisionTestExtractText3 : Test
        {

            private static readonly Image TEST_INPUT_IMAGE = Image.FromFile("Resources\\test_text_3.png");
            private static readonly String TEST_RESULT_TEXT = "Hello World!";

            public bool Run(StreamWriter sw)
            {
                using (MemoryStream mStream = new MemoryStream()) {
                    TEST_INPUT_IMAGE.Save(mStream, System.Drawing.Imaging.ImageFormat.Png);
                    if (GoogleVisionConnectionManager.ExtractText(mStream.ToArray()) != TEST_RESULT_TEXT)
                    {
                        sw.WriteLine("GoogleVisionTestExtractText3 failed: Extracted text does not match expected result.");
                        return false;
                    }
                }

                return true;
            }
        }
    }
}
