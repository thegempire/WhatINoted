using System;
using System.IO;
using NUnit.Framework;
using SeleniumTests;

namespace WhatINoted.Tests
{
	public class ExampleKatalonTest : Test
    {

        public bool Run(StreamWriter sw)
        {
            TestCase1 testCase = new TestCase1();

            testCase.SetupTest();
            try
            {
                testCase.TheCase1Test();
            }
            catch (AssertionException e)
            {
                sw.WriteLine("Example test failed: " + e.Message);

                return false;
            }
            finally
            {
                testCase.TeardownTest();
            }
            return true;
        }
    }
}
