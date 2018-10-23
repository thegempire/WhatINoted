using System;
using System.IO;
namespace WhatINoted.Tests
{
    public interface Test
    {

        bool Run(StreamWriter sw);

    }
}
