using System;
using System.IO;
using System.Collections.Generic;
namespace WhatINoted.Tests2
{
    class Program
    {
        static readonly Test[] tests = { new PingTest() };

        static void Main(string[] args)
        {
            DateTime now = DateTime.Now;
            string dateTimeString = now.ToShortDateString() + " " + now.ToShortTimeString();
            List<Test> failedTests = new List<Test>();
            using (StreamWriter sw = File.CreateText("temptestresults.txt"))
            {
                for (int i = 0; i < tests.Length; i++)
                {
                    Test test = tests[i];
                    sw.WriteLine("  START TEST: " + test.GetType().Name);
                    if (!test.Run(sw))
                    {
                        failedTests.Add(test);
                    }
                    sw.WriteLine("  END TEST: " + test.GetType().Name);
                    if (i != tests.Length - 1)
                    {
                        sw.WriteLine();
                    }
                }
                sw.Close();
            }
            using (StreamWriter sw = File.CreateText("TestResults" + Path.DirectorySeparatorChar + dateTimeString + " Test Results.txt"))
            {
                sw.WriteLine("--------------TEST RESULTS--------------");
                sw.WriteLine("  Tests Run: " + tests.Length);
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
