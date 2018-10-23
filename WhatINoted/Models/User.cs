using System;
using System.Collections.Generic;

namespace WhatINoted.Models
{
    /// <summary>
    /// An application user.
    /// </summary>
    public class User
    {
        /// <summary>
        /// User's database ID
        /// </summary>
        public readonly string ID;

        /// <summary>
        /// User's display name
        /// </summary>
        public readonly string DisplayName;

        /// <summary>
        /// User's email address
        /// </summary>
        public readonly string Email;

        /// <summary>
        /// The time that this User was last modified.
        /// </summary>
        public DateTime Modified { get; private set; }

        /// <summary>
        /// The time that this User was created.
        /// </summary>
        public DateTime Created { get; private set; }

        /// <summary>
        /// List of User's Notebooks
        /// </summary>
        public List<Notebook> Notebooks { get; }

        /// <summary>
        /// Constructs a User from the provided information.
        /// </summary>
        /// <param name="userID">user id</param>
        /// <param name="displayName">display name</param>
        /// <param name="email">email address</param>
        /// <param name="modified">when this user was last modified</param>
        /// <param name="created">when this user was created</param>
        public User(string userID, string displayName, string email, DateTime modified, DateTime created)
        {
            ID = userID;
            DisplayName = displayName;
            Email = email;
            Modified = modified;
            Created = created;
            Notebooks = new List<Notebook>();
        }

        /// <summary>
        /// Constructs a User from the provided Json object.
        /// </summary>
        /// <param name="jsonUser">Json object containing information related to a User</param>
        public User(JsonUser jsonUser)
        {
            ID = jsonUser.ID;
            DisplayName = jsonUser.DisplayName;
            Email = jsonUser.Email;
            Created = jsonUser.Created;
            Modified = jsonUser.Modified;
            Notebooks = new List<Notebook>();
        }

        /// <summary>
        /// Checks for equality between the calling User and the passed object.
        /// 
        /// Currently compares DisplayName and Email.
        /// </summary>
        /// <param name="other">other object</param>
        /// <returns>true if the calling and passed objects are equal</returns>
        public override bool Equals(object other)
        {
            var model = other as User;
            return model != null
                && DisplayName == model.DisplayName
                && Email == model.Email;
        }

        /// <summary>
        /// Calculates hash code for the object.
        /// </summary>
        /// <returns>the calculated hash code value</returns>
        public override int GetHashCode()
        {
            var hashCode = -464330532;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(DisplayName);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Email);
            return hashCode;
        }
    }
}