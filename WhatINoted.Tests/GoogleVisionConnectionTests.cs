using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Drawing;
using WhatINoted.Tests2;
using Image = Google.Cloud.Vision.V1.Image;

namespace WhatINoted.Tests
{
    class GoogleVisionConnectionTests : Test
    {
        private static readonly int NUM_TESTS = 6;

        private static readonly Image TEST_TEXT_1_IMAGE;
        private static readonly String TEST_TEXT_1_RESULT = "";

        private static readonly Image TEST_TEXT_2_IMAGE;
        private static readonly String TEST_TEXT_2_RESULT = "";

        private static readonly Image TEST_TEXT_3_IMAGE;
        private static readonly String TEST_TEXT_3_RESULT = "";

        private static readonly Image TEST_ISBN_1_IMAGE;
        private static readonly String TEST_ISBN_1_RESULT = "";

        private static readonly Image TEST_ISBN_2_IMAGE;
        private static readonly String TEST_ISBN_2_RESULT = "";

        public bool Run(StreamWriter sw)
        {
            int failed = 0;

            //// TEST CONNECTION ////

            if (!TestConnection(sw))
            {
                sw.WriteLine("GoogleVisionConnectionTests: Could not connect.");
                ++failed;
            }
            


            //// TEST TEXT RESOLUTION ////

            if (!TestText1(sw))
            {
                sw.WriteLine("GoogleVisionConnectionTests: Text resolution test 1 failed.");
                ++failed;
            }

            if (!TestText2(sw))
            {
                sw.WriteLine("GoogleVisionConnectionTests: Text resolution test 2 failed.");
                ++failed;
            }

            if (!TestText3(sw))
            {
                sw.WriteLine("GoogleVisionConnectionTests: Text resolution test 3 failed.");
                ++failed;
            }



            //// TEST ISBN RESOLUTION ////

            if (!TestIsbn1(sw))
            {
                sw.WriteLine("GoogleVisionConnectionTests: ISBN resolution test 1 failed.");
                ++failed;
            }

            if (!TestIsbn1(sw))
            {
                sw.WriteLine("GoogleVisionConnectionTests: ISBN resolution test 2 failed.");
                ++failed;
            }

            sw.WriteLine("GoogleVisionConnectionTests: (" + failed + "/" + NUM_TESTS + ") tests failed.");
            return (failed == 0);
        }

        private bool TestConnection(StreamWriter sw)
        {
            

            return true;
        }

        private bool TestText1(StreamWriter sw)
        {
            String resolved = GoogleVisionConnectionManager.ExtractText(TEST_TEXT_1_IMAGE);
            return resolved.Equals(TEST_TEXT_1_RESULT);
        }

        private bool TestText2(StreamWriter sw)
        {
            String resolved = GoogleVisionConnectionManager.ExtractText(TEST_TEXT_2_IMAGE);
            return resolved.Equals(TEST_TEXT_2_RESULT);
        }

        private bool TestText3(StreamWriter sw)
        {
            String resolved = GoogleVisionConnectionManager.ExtractText(TEST_TEXT_3_IMAGE);
            return resolved.Equals(TEST_TEXT_3_RESULT);
        }

        private bool TestIsbn1(StreamWriter sw)
        {
            String resolved = GoogleVisionConnectionManager.ExtractText(TEST_ISBN_1_IMAGE);
            return resolved.Equals(TEST_ISBN_1_RESULT);
        }

        private bool TestIsbn2(StreamWriter sw)
        {
            String resolved = GoogleVisionConnectionManager.ExtractText(TEST_ISBN_2_IMAGE);
            return resolved.Equals(TEST_ISBN_2_RESULT);
        }
    }
}
