using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WhatINoted.Models
{
    public enum IsbnType { ISBN_10, ISBN_13 };

    /// <summary>
    /// IsbnModel stores information representing an ISBN.
    /// </summary>
    public class IsbnModel
    {
        /// <summary>
        /// String representation of the ISBN
        /// </summary>
        public readonly String Number;

        public readonly IsbnType Type;

        /// <summary>
        /// Construct an IsbnModel with the given ISBN.
        /// </summary>
        /// <param name="number">String representation of the ISBN</param>
        public IsbnModel(String Number, IsbnType Type)
        {
            this.Number = Number;
            this.Type = Type;
        }

        /// <summary>
        /// Validate the ISBN.
        /// </summary>
        /// <returns>true if and only if the ISBN is valid</returns>
        private bool IsValid()
        {
            switch(Type)
            {
                case IsbnType.ISBN_10:
                    return isValidIsbn10();

                case IsbnType.ISBN_13:
                    return isValidIsbn13();

                default:
                    return false;
            }
        }

        private bool isValidIsbn10()
        {
            int sum = 0;
            int digit;

            for (int i = 0; i < 9; i++)
            {
                if (Int32.TryParse(Number.Substring(i, i + 1), out digit))
                    sum += (10 - i) * digit;

                else
                    throw new ArgumentException("");
            }

            if (Int32.TryParse(Number.Substring(9, 10), out digit))
                sum += 10 * digit;

            else if (Number[9] == 'X')
                sum += 10;

            else
                ;// throw an exception

            return (sum % 11) == 0;
        }

        private bool isValidIsbn13()
        {
            int sum = 0;
            int digit;

            for (int i = 0; i < 13; i++)
            {
                if (Int32.TryParse(Number.Substring(i, i+1), out digit))
                {
                    if (i % 2 == 1)
                        sum += digit;

                    else
                        sum += 3 * digit;
                }
                else
                {
                    // throw an exception
                }
            }

            return sum % 10 == 0;
        }
    }
}