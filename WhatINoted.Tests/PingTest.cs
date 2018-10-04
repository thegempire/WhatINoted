using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
namespace WhatINoted.Tests2
{
    public class PingTest : Test
    {
        public bool Run(StreamWriter sw)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                return RunWindows(sw);
            }
            return RunUnix(sw);
        }

        private bool RunWindows(StreamWriter sw)
        {
            //string cmd = "/C ping 173.254.206.164 -n 50 >> pingtemp.txt";
            //System.Diagnostics.Process.Start("CMD.exe", cmd);
            System.Diagnostics.Process proc = new System.Diagnostics.Process();
            using (StreamWriter tempsw = File.CreateText("pingtemp.txt"))
            {
                proc.StartInfo.FileName = "CMD.exe";
                proc.StartInfo.Arguments = "/C ping 173.254.206.164 -n 50";
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
                        string[] split = Regex.Split(s, "(");
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

        private bool RunUnix(StreamWriter sw)
        {
            System.Diagnostics.Process proc = new System.Diagnostics.Process();
            using (StreamWriter tempsw = File.CreateText("pingtemp.txt"))
            {
                proc.StartInfo.FileName = "/bin/bash";
                proc.StartInfo.Arguments = "-c \" ping 173.254.206.164 -c 50 \"";
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
                if (lossPercentage > 15)
                {
                    sw.Write("<<< FAILURE - exceeds 15%");
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
                if (average > 50)
                {
                    sw.Write("<<< FAILURE - exceeds 50ms");
                    passed = false;
                }
                sw.WriteLine();
            }
            return passed;
        }
    }
}
