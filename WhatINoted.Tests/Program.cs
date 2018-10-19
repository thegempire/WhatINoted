using System;
using System.IO;
using System.Collections.Generic;
namespace WhatINoted.Tests2
{
    class Program
    {
        /*
         * Every time you create a test class, add an instance of that test
         * here in order to get the test to run.
         */
        static readonly List<Test> tests = new List<Test>();

        /*
         * Runs every test and creates a file in TestResults.
         */ 
        static void Main(string[] args)
        {
            // Populate test list
            tests.Add(new PingTest());

            DateTime now = DateTime.Now;
            string dateTimeString = now.ToString("yyyy-MM-dd HH\\hmm");
            List<Test> failedTests = new List<Test>();
            using (StreamWriter sw = File.CreateText("temptestresults.txt"))
            {
                for (int i = 0; i < tests.Count; i++)
                {
                    Test test = tests[i];
                    string name = test.GetType().Name;
                    sw.WriteLine("  START TEST: " + name);
                    Console.Write("Running " + name + "(" + (i+1) + "/" + tests.Count + ")...");
                    if (!test.Run(sw))
                    {
                        failedTests.Add(test);
                        Console.WriteLine("failed");
                    } else {
                        Console.WriteLine("passed");
                    }
                    sw.WriteLine("  END TEST: " + name);
                    if (i != tests.Count - 1)
                    {
                        sw.WriteLine();
                    }
                }
                sw.Close();
            }
            using (StreamWriter sw = File.CreateText("TestResults" + Path.DirectorySeparatorChar + dateTimeString + " Test Results.txt"))
            {
                sw.WriteLine("--------------TEST RESULTS--------------");
                sw.WriteLine("  Tests Run: " + tests.Count);
                sw.WriteLine("  Tests Failed: " + failedTests.Count);
                sw.WriteLine("----------------------------------------");
                sw.WriteLine();
                if (failedTests.Count > 0)
                {
                    sw.WriteLine("----------------FAILURES----------------");
                    foreach (Test test in failedTests)
                    {
                        sw.WriteLine("  " + test.GetType().Name);
                    }
                    sw.WriteLine("----------------------------------------");
                    sw.WriteLine();
                }
                sw.WriteLine("-----------------OUTPUT-----------------");
                using (StreamReader sr = File.OpenText("temptestresults.txt"))
                {
                    String s = "";
                    while ((s = sr.ReadLine()) != null)
                    {
                        sw.Write("  ");
                        sw.WriteLine(s);
                    }
                    sr.Close();
                }
                sw.WriteLine("----------------------------------------");
                File.Delete("temptestresults.txt");
                sw.Close();
            }
        }
    }
}
