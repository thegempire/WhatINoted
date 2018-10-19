using System.Collections.Generic;

namespace WhatINoted.Models
{
    /// <summary>
    /// UserModel stores information representing an application user.
    /// </summary>
    public class UserModel
    {
        /// <summary>
        /// User's database ID
        /// </summary>
        public readonly string UserID;

        /// <summary>
        /// User's display name
        /// </summary>
        public readonly string DisplayName;

        /// <summary>
        /// User's email address
        /// </summary>
        public readonly string Email;

        /// <summary>
        /// List of User's Notebooks
        /// </summary>
        public List<NotebookModel> Notebooks { get; }

        /// <summary>
        /// Constructs a User from the provided information.
        /// </summary>
        /// <param name="userID">user id</param>
        /// <param name="displayName">display name</param>
        /// <param name="email">email address</param>
        /// <param name="notebooks">list of User's notebooks</param>
        public UserModel(string userID, string displayName = "", string email = "", List<NotebookModel> notebooks = null)
        {
            UserID = userID;
            DisplayName = displayName;
            Email = email;
            Notebooks = notebooks ?? new List<NotebookModel>();
        }

        public UserModel(JsonUser jsonUser)
        {
            UserID = jsonUser.UserID;
            DisplayName = jsonUser.DisplayName;
            Email = jsonUser.Email;
            Notebooks = new List<NotebookModel>();
        }
    }
}