using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using WhatINoted.Models;

namespace WhatINoted.Tests
{
    class IsbnModelTest : Test
    {
        public bool Run(StreamWriter sw)
        {
            return ValidIsbn1(sw) && ValidIsbn2(sw)
                && InvalidIsbn1(sw) && InvalidIsbn2(sw)
                && InvalidIsbn3(sw) && InvalidIsbn4(sw)
                && EmptyIsbn(sw) && NullIsbn(sw);
        }

        private bool ValidIsbn1(StreamWriter sw)
        {
            try
            {
                new IsbnModel("097522980X");
                return true;
            }
            catch
            {
                sw.WriteLine("IsbnModelTest - ValidIsbn1 failed: Unexpected exception.");
                return false;
            }
        }

        private bool ValidIsbn2(StreamWriter sw)
        {
            try
            {
                new IsbnModel("9780975229804");
                return true;
            }
            catch
            {
                sw.WriteLine("IsbnModelTest - ValidIsbn2 failed: Unexpected exception.");
                return false;
            }
        }

        private bool InvalidIsbn1(StreamWriter sw)
        {
            try
            {
                new IsbnModel("123");
            }
            catch
            {
                return true;
            }

            sw.WriteLine("IsbnModelTest - InvalidIsbn1 failed: Expected an exception.");
            return false;
        }


        private bool InvalidIsbn2(StreamWriter sw)
        {
            try
            {
                new IsbnModel("123abc789t");
            }
            catch
            {
                return true;
            }

            sw.WriteLine("IsbnModelTest - InvalidIsbn2 failed: Expected an exception.");
            return false;
        }


        private bool InvalidIsbn3(StreamWriter sw)
        {
            try
            {
                new IsbnModel("0975229809");
            }
            catch
            {
                return true;
            }

            sw.WriteLine("IsbnModelTest - InvalidIsbn3 failed: Expected an exception.");
            return false;
        }


        private bool InvalidIsbn4(StreamWriter sw)
        {
            try
            {
                new IsbnModel("9780975229803");
            }
            catch
            {
                return true;
            }

            sw.WriteLine("IsbnModelTest - InvalidIsbn4 failed: Expected an exception.");
            return false;
        }


        private bool EmptyIsbn(StreamWriter sw)
        {
            try
            {
                new IsbnModel("");
            }
            catch
            {
                return true;
            }

            sw.WriteLine("IsbnModelTest - EmptyIsbn failed: Expected an exception.");
            return false;
        }


        private bool NullIsbn(StreamWriter sw)
        {
            try
            {
                new IsbnModel(null);
            }
            catch
            {
                return true;
            }

            sw.WriteLine("IsbnModelTest - NullIsbn failed: Expected an exception.");
            return false;
        }
    }
}
