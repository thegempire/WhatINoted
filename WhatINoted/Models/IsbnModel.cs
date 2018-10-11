using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WhatINoted.Models
{
    public class IsbnModel
    {
        public readonly String number;

        public IsbnModel(String number)
        {
            this.number = number;
        }

        public bool IsValid()
        {
            // TODO: validation
            return false;
        }
    }
}