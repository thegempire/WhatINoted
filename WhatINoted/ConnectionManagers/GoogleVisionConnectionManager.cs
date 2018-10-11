using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing;

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
        private static readonly String API_KEY = ""; // TODO -- Get an API key. Either put it here, or read it from a config file?

        /// <summary>
        /// Send a request to Google Vision to extract extract text from an image.
        /// </summary>
        /// <param name="image">the image to analyze</param>
        /// <returns>A string extracted from the image, or null if the call failed.</returns>
        public static String ExtractText(Image image)
        {
            // TODO: OCR API interaction

            return null;
        }
    }
}