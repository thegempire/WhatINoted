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
        /// Get a list of Google Vision tests.
        /// </summary>
        /// <returns>list of Google Vision tests</returns>
        public static List<Test> GetTests()
        {
            List<Test> tests = new List<Test>();
            tests.Add(new GoogleVisionTestConnection());
            tests.Add(new GoogleVisionTestExtractText1());
            tests.Add(new GoogleVisionTestExtractText2());
            tests.Add(new GoogleVisionTestExtractText3());
            tests.Add(new GoogleVisionTestExtractIsbn1());
            tests.Add(new GoogleVisionTestExtractIsbn2());
            return tests;
        }

        private class GoogleVisionTestConnection : Test
        {
            public bool Run(StreamWriter sw)
            {
                if (false /* TODO: Test connection */)
                {
                    sw.WriteLine("GoogleVisionTestConnection failed: Could not connect.");
                    return false;
                }

                return true;
            }
        }

        private class GoogleVisionTestExtractText1 : Test
        {

            private static readonly System.Drawing.Image TEST_INPUT_IMAGE;
            private static readonly String TEST_RESULT_TEXT = "";

            public bool Run(StreamWriter sw)
            {
                if (!GoogleVisionConnectionManager.ExtractText(TEST_INPUT_IMAGE).Equals(TEST_RESULT_TEXT))
                {
                    sw.WriteLine("GoogleVisionTestExtractText1 failed: Extracted text does not match expected result.");
                    return false;
                }

                return true;
            }
        }

        private class GoogleVisionTestExtractText2 : Test
        {

            private static readonly Image TEST_INPUT_IMAGE;
            private static readonly String TEST_RESULT_TEXT = "";

            public bool Run(StreamWriter sw)
            {
                if (!GoogleVisionConnectionManager.ExtractText(TEST_INPUT_IMAGE).Equals(TEST_RESULT_TEXT))
                {
                    sw.WriteLine("GoogleVisionTestExtractText2 failed: Extracted text does not match expected result.");
                    return false;
                }

                return true;
            }
        }

        private class GoogleVisionTestExtractText3 : Test
        {

            private static readonly Image TEST_INPUT_IMAGE;
            private static readonly String TEST_RESULT_TEXT = "";

            public bool Run(StreamWriter sw)
            {
                if (!GoogleVisionConnectionManager.ExtractText(TEST_INPUT_IMAGE).Equals(TEST_RESULT_TEXT))
                {
                    sw.WriteLine("GoogleVisionTestExtractText3 failed: Extracted text does not match expected result.");
                    return false;
                }

                return true;
            }
        }

        private class GoogleVisionTestExtractIsbn1 : Test
        {

            private static readonly Image TEST_INPUT_IMAGE;
            private static readonly string TEST_RESULT_TEXT = "";

            public bool Run(StreamWriter sw)
            {
                if (!GoogleVisionConnectionManager.ExtractText(TEST_INPUT_IMAGE).equals(TEST_RESULT_TEXT))
                {
                    sw.WriteLine("GoogleVisionTestExtractIsbn1 failed: Extracted ISBN does not match expected result.");
                    return false;
                }

                return true;
            }
        }

        private class GoogleVisionTestExtractIsbn2 : Test
        {

            private static readonly Image TEST_INPUT_IMAGE;
            private static readonly string TEST_RESULT_TEXT = "";

            public bool Run(StreamWriter sw)
            {
                if (!GoogleVisionConnectionManager.ExtractText(TEST_INPUT_IMAGE).equals(TEST_RESULT_TEXT))
                {
                    sw.WriteLine("GoogleVisionTestExtractIsbn2 failed: Extracted ISBN does not match expected result.");
                    return false;
                }

                return true;
            }
        }
    }
}
