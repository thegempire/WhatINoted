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
        /// List of User's Notebooks
        /// </summary>
        public List<Notebook> Notebooks { get; }

        /// <summary>
        /// Constructs a User from the provided information.
        /// </summary>
        /// <param name="userID">user id</param>
        /// <param name="displayName">display name</param>
        /// <param name="email">email address</param>
        /// <param name="notebooks">list of user's notebooks</param>
        public User(string userID = "", string displayName = "", string email = "", List<Notebook> notebooks = null)
        {
            ID = userID;
            DisplayName = displayName;
            Email = email;
            Notebooks = notebooks ?? new List<Notebook>();
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
            Notebooks = new List<Notebook>();
        }

        /// <summary>
        /// Checks for equality between the calling User and the passed object.
        /// </summary>
        /// <param name="other">other object</param>
        /// <returns>true if the calling and passed objects are equal ignoring notebook list</returns>
        public override bool Equals(object other)
        {
            var model = other as User;
            return model != null
                && (ID == null || model.ID == null || ID == model.ID)
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
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(ID);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(DisplayName);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Email);
            return hashCode;
        }

        /// <summary>
        /// Overloads the == operator.
        /// </summary>
        /// <param name="first">the first user to compare</param>
        /// <param name="second">the second user to compare</param>
        /// <returns>true if the users are equal</returns>
        public static bool operator ==(User first, User second)
        {
            return first.Equals(second);
        }

        /// <summary>
        /// Overloads the != operator.
        /// </summary>
        /// <param name="first">the first user to compare</param>
        /// <param name="second">the second user to compare</param>
        /// <returns>true if the users are not equal</returns>
        public static bool operator !=(User first, User second)
        {
            return !first.Equals(second);
        }
    }
}