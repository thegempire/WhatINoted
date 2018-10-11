using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing;

namespace WhatINoted.ConnectionManagers
{
    public class GoogleVisionConnectionManager
    {
        private static readonly String ApiKey = ""; // TODO -- Get an API key. Either put it here, or read it from a config file?

        public static String ExtractText(Image image)
        {
            // TODO: OCR API interaction

            return null;
        }
    }
}