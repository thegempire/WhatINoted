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

        /// <summary>
        /// ISBN Type
        /// </summary>
        public readonly IsbnType Type;

        /// <summary>
        /// Construct an IsbnModel with the given ISBN.
        /// </summary>
        /// <param name="number">String representation of the ISBN</param>
        public IsbnModel(String Number)
        {
            this.Number = Number;

            if (Number.Length == 10)
                this.Type = IsbnType.ISBN_10;

            else if (Number.Length == 13)
                this.Type = IsbnType.ISBN_13;

            else throw new ArgumentException("IsbnModel: Number is not 10 or 13 characters in length.");

            if (!IsValid())
                throw new ArgumentException("IsbnModel: Validation failed.");

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

        /// <summary>
        /// ISBN 10 validation
        /// </summary>
        /// <returns>True if Number is a valid ISBN 10</returns>
        private bool isValidIsbn10()
        {
            int sum = 0;
            int digit;

            for (int i = 0; i < 9; i++)
            {
                if (Int32.TryParse(Number.Substring(i, i + 1), out digit))
                {
                    sum += (10 - i) * digit;
                }

                else
                {
                    throw new ArgumentException("IsbnModel: String contains an invalid character.");
                }
            }

            if (Int32.TryParse(Number.Substring(9, 10), out digit))
            {
                sum += 10 * digit;
            }

            else if (Number[9] == 'X')
            {
                sum += 10;
            }

            else
            {
                throw new ArgumentException("IsbnModel: String contains an invalid character.");
            }

            return (sum % 11) == 0;
        }
        
        /// <summary>
        /// ISBN 13 validation
        /// </summary>
        /// <returns>True if Number is a valid ISBN 13</returns>
        private bool isValidIsbn13()
        {
            int sum = 0;
            int digit;

            for (int i = 0; i < 13; i++)
            {
                if (Int32.TryParse(Number.Substring(i, i + 1), out digit))
                {
                    if (i % 2 == 1)
                    {
                        sum += digit;
                    }

                    else
                    {
                        sum += 3 * digit;
                    }
                }
                else
                {
                    throw new ArgumentException("IsbnModel: String contains an invalid character.");
                }
            }

            return sum % 10 == 0;
        }
    }
}