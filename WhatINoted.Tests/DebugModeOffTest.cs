using System;
using System.IO;

namespace WhatINoted.Tests
{
    public class DebugModeOffTest : Test
    {
        public DebugModeOffTest()
        {

        }

        public bool Run(StreamWriter sw)
        {
            bool result = !DebugMode.DEBUG;
            if (!result) 
            {
                string alert = "***** DO NOT DEPLOY - DEBUG MODE IS ON! *****";
                sw.WriteLine(alert);
                Console.WriteLine(alert);
                return false;
            }
            return true;
        }
    }
}
