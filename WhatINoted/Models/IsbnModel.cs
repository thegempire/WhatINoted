using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WhatINoted.Models
{
    /// <summary>
    /// IsbnModel stores information representing an ISBN.
    /// </summary>
    public class IsbnModel
    {
        /// <summary>
        /// String representation of the ISBN
        /// </summary>
        public readonly String Number;

        /// <summary>
        /// Construct an IsbnModel with the given ISBN.
        /// </summary>
        /// <param name="number">String representation of the ISBN</param>
        public IsbnModel(String Number)
        {
            this.Number = Number;
        }

        /// <summary>
        /// Validate the ISBN.
        /// </summary>
        /// <returns>true if and only if the ISBN is valid</returns>
        public bool IsValid()
        {
            // TODO: validation
            return false;
        }
    }
}