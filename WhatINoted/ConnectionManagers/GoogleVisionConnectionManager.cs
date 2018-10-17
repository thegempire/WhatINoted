using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing;
using Google.Cloud.Vision.V1;
using Image = Google.Cloud.Vision.V1.Image;
using System.IO;

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
        /// API key to access Google Vision.
        /// </summary>
        private static readonly string API_KEY = ""; // TODO -- Get an API key. Either put it here, or read it from a config file?

        /// <summary>
        /// Send a request to Google Vision to extract extract text from an image.
        /// </summary>
        /// <param name="image">the image to analyze</param>
        /// <returns>A string extracted from the image, or null if the call failed.</returns>
        public static string ExtractText(System.Drawing.Image originalImage)
        {
            //Convert originalImage to bytes
            byte[] imageBytes = ImageToByteArray(originalImage);

            Image image = Image.FromBytes(imageBytes);
            System.Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", "C:\\Users\\Ian\\source\\repos\\WhatINoted\\WhatINoted-5bba0c1ecaf8.json");
            ImageAnnotatorClient client = ImageAnnotatorClient.Create();
            IReadOnlyList<EntityAnnotation> textAnnotations = client.DetectText(image);
            return textAnnotations.First().Description;
        }

        public static byte[] ImageToByteArray(System.Drawing.Image imageIn)
        {
            using (var ms = new MemoryStream())
            {
                imageIn.Save(ms, imageIn.RawFormat);
                return ms.ToArray();
            }
        }
    }
}