using System.Collections.Generic;
using System.Linq;
using Google.Cloud.Vision.V1;
using System.IO;
using System;

namespace WhatINoted.ConnectionManagers
{
    /// <summary>
    /// Connection Manager to handle interactions with the Google Vision image
    /// analysis API. Specifically this class is used to connect to the API
    /// and extract text from a given image.
    /// </summary>
    public class GoogleVisionConnectionManager
    {
        /// <summary>
        /// ***This is the location of the json credentials file for the Google Vision service account. Change this if the path is wrong or changes.***
        /// </summary>

        private static readonly string JsonPath = AppDomain.CurrentDomain.BaseDirectory + "Resources\\WhatINoted-5bba0c1ecaf8.json";
        public const string GOOGLE_APPLICATION_CREDENTIALS = "GOOGLE_APPLICATION_CREDENTIALS";

        /// <summary>
        /// Send a request to Google Vision to extract extract text from an image.
        /// </summary>
        /// <param name="image">the image to analyze</param>
        /// <returns>A string extracted from the image, or null if the call failed.</returns>
        public static string ExtractText(System.Drawing.Image originalImage)
        {
            if (originalImage == null)
                throw new NullReferenceException("GoogleVisionConnectionManager.ExtractText(Image): Image provided is null");

            //Convert originalImage to bytes
            byte[] imageBytes = ImageToByteArray(originalImage);

            Image image = Image.FromBytes(imageBytes);
            System.Environment.SetEnvironmentVariable(GOOGLE_APPLICATION_CREDENTIALS, JsonPath);
            ImageAnnotatorClient client = ImageAnnotatorClient.Create();
            IReadOnlyList<EntityAnnotation> textAnnotations = client.DetectText(image);
            string text = textAnnotations.First().Description;

            if (text.Last() == '\n')
                text = text.Substring(0, text.Length - 1);
            return text.Replace('\n', ' ');
        }

        private static byte[] ImageToByteArray(System.Drawing.Image imageIn)
        {
            using (var ms = new MemoryStream())
            {
                imageIn.Save(ms, imageIn.RawFormat);
                return ms.ToArray();
            }
        }
    }
}