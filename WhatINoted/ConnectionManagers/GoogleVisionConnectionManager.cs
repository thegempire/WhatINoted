using Google.Cloud.Vision.V1;
using System;
using System.Collections.Generic;
using System.Linq;

namespace WhatINoted.ConnectionManagers
{
    /// <summary>
    /// Connection Manager to handle interactions with the Google Vision image
    /// analysis API. Specifically this class is used to connect to the API
    /// and extract text from a given image.
    /// 
    /// Notes:
    /// OCR limitations:
    ///    Image Size = 20MB
    ///    JSON object request size = 10MB
    ///    Images per request = 16
    /// Base64-encoded images may exceed the JSON size limit, even if they are within the image file size limit.Larger images should be hosted on Cloud Storage or at a publicly-accessible URL. Note that base64-encoded images can have a larger file size than the original image file (usually about 37% larger).
    /// Requests per minute	 = 1,800
    /// Images per feature per month = 20,000,000
    /// </summary>
    public class GoogleVisionConnectionManager
    {
        /// <summary>
        /// ***This is the location of the json credentials file for the Google Vision service account. Change this if the path is wrong or changes.***
        /// </summary>

        /* 7/11/2018
         * Ian Younghusband:
         * The path to this resource is being calculated in a hack-y way to allow for both the main project
         * and the test project to access the resource. There should be a better way to do this...
         */
        private static readonly string JsonPath = (AppDomain.CurrentDomain.BaseDirectory.IndexOf("\\bin\\") >= 0 ?
            AppDomain.CurrentDomain.BaseDirectory.Remove(AppDomain.CurrentDomain.BaseDirectory.IndexOf("\\bin\\")) :
            AppDomain.CurrentDomain.BaseDirectory)
            + "\\Resources\\WhatINoted-5bba0c1ecaf8.json";
        public const string GOOGLE_APPLICATION_CREDENTIALS = "GOOGLE_APPLICATION_CREDENTIALS";

        /// <summary>
        /// Send a request to Google Vision to extract extract text from an image represented as a series of bytes.
        /// </summary>
        /// <param name="imageBytes">the image to analyze represented as a series of bytes</param>
        /// <returns>A string extracted from the image, or null if the call failed.</returns>
        public static string ExtractText(byte[] imageBytes)
        {
            Image image = Image.FromBytes(imageBytes);
            System.Environment.SetEnvironmentVariable(GOOGLE_APPLICATION_CREDENTIALS, JsonPath);
            ImageAnnotatorClient client = ImageAnnotatorClient.Create();
            IReadOnlyList<EntityAnnotation> textAnnotations = client.DetectText(image);

            if (textAnnotations.Count <= 0)
                return "";
            string text = textAnnotations.First().Description;

            if (text.Last() == '\n')
                text = text.Substring(0, text.Length - 1);
            return text.Replace('\n', ' ');
        }
    }
}