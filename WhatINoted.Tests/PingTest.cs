using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
namespace WhatINoted.Tests2
{
    public class PingTest : Test
    {
        /*
         * The number of times to ping the server.
         */
        private const int PINGS = 50;

        /*
         * The maximum acceptable loss percentage.
         */
        private const int MAXIMUM_LOSS_PERCENTAGE = 15;

        /*
         * The maximum acceptable average ping, in milliseconds.
         */
        private const int MAXIMUM_AVERAGE_TIME = 50;

        /*
         * Since the command to ping the server is different depending on the
         * operating system, there are two implementations of this test. This
         * method determines whether the OS is Windows or Unix-based and runs
         * the appropriate implementation.
         */
        public bool Run(StreamWriter sw)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                return RunWindows(sw);
            }
            return RunUnix(sw);
        }

        /*
         * Runs the test on Windows machines. Pings the server the right number
         * of times and extracts and processes the results.
         */
        private bool RunWindows(StreamWriter sw)
        {
            System.Diagnostics.Process proc = new System.Diagnostics.Process();
            using (StreamWriter tempsw = File.CreateText("pingtemp.txt"))
            {
                proc.StartInfo.FileName = "CMD.exe";
                proc.StartInfo.Arguments = "/C ping 173.254.206.164 -n " + PINGS;
                proc.StartInfo.UseShellExecute = false;
                proc.StartInfo.RedirectStandardOutput = true;
                proc.Start();
                while (!proc.StandardOutput.EndOfStream)
                {
                    tempsw.WriteLine(proc.StandardOutput.ReadLine());
                }
            }
            using (StreamReader sr = File.OpenText("pingtemp.txt"))
            {
                string s = "";
                int lossPercentage = -1;
                int average = -1;
                while ((s = sr.ReadLine()) != null)
                {
                    if (Regex.IsMatch(s, ".*Packets:.*"))
                    {
                        string[] split = Regex.Split(s, "\\(");
                        lossPercentage = ParseInt(split[1]);
                    }
                    else if (Regex.IsMatch(s, ".*Minimum.*"))
                    {
                        string[] split = Regex.Split(s, "Average = ");
                        average = ParseInt(split[1]);
                    }
                }
                bool passed = ProcessStatistics(sw, lossPercentage, average);
                sr.Close();
                File.Delete("pingtemp.txt");
                return passed;
            }
        }

        /*
         * Runs the test on Unix machines. Pings the server the right number
         * of times and extracts and processes the results.
         */
        private bool RunUnix(StreamWriter sw)
        {
            System.Diagnostics.Process proc = new System.Diagnostics.Process();
            using (StreamWriter tempsw = File.CreateText("pingtemp.txt"))
            {
                proc.StartInfo.FileName = "/bin/bash";
                proc.StartInfo.Arguments = "-c \" ping 173.254.206.164 -c " + PINGS + " \"";
                proc.StartInfo.UseShellExecute = false;
                proc.StartInfo.RedirectStandardOutput = true;
                proc.Start();
                while (!proc.StandardOutput.EndOfStream)
                {
                    tempsw.WriteLine(proc.StandardOutput.ReadLine());
                }
            }
            using (StreamReader sr = File.OpenText("pingtemp.txt"))
            {
                string s = "";
                int lossPercentage = -1;
                int average = -1;
                while ((s = sr.ReadLine()) != null)
                {
                    if (Regex.IsMatch(s, ".*packets transmitted.*"))
                    {
                        string[] split = Regex.Split(s, "packets received, ");
                        lossPercentage = ParseInt(split[1]);
                    }
                    else if (Regex.IsMatch(s, ".*round-trip.*"))
                    {
                        string[] split = Regex.Split(s, "/");
                        average = ParseInt(split[4]);
                    }
                }
                bool passed = ProcessStatistics(sw, lossPercentage, average);
                sr.Close();
                File.Delete("pingtemp.txt");
                return passed;
            }
        }

        /*
         * Parses an int from a string. Allows for non-numeric characters after
         * the int, unlike Int32.Parse.
         */
        private int ParseInt(string s)
        {
            string b = "";
            int i = 0;
            while (Char.IsDigit(s[i]))
            {
                b += s[i];
                i++;
            }
            return Int32.Parse(b);
        }

        /*
         * Processes the loss percentage and average ping time and writes to the
         * stream accordingly. Returns true if both the loss percentage and
         * average ping time are at acceptable levels.
         */
        private bool ProcessStatistics(StreamWriter sw, int lossPercentage, int average)
        {
            bool passed = true;
            sw.Write("Loss Percentage: ");
            if (lossPercentage == -1)
            {
                sw.WriteLine("ERROR - not found");
                passed = false;
            }
            else
            {
                sw.Write(lossPercentage + "%");
                if (lossPercentage > MAXIMUM_LOSS_PERCENTAGE)
                {
                    sw.Write("<<< FAILURE - exceeds " + MAXIMUM_LOSS_PERCENTAGE + "%");
                    passed = false;
                }
                sw.WriteLine();
            }
            sw.Write("Average Time: ");
            if (average == -1)
            {
                sw.WriteLine("ERROR - not found");
                passed = false;
            }
            else
            {
                sw.Write(average + "ms");
                if (average > MAXIMUM_AVERAGE_TIME)
                {
                    sw.Write("<<< FAILURE - exceeds " + MAXIMUM_AVERAGE_TIME + "ms");
                    passed = false;
                }
                sw.WriteLine();
            }
            return passed;
        }
    }
}
