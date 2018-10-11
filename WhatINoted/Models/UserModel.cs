using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WhatINoted.Models
{
    /// <summary>
    /// UserModel: Stores user information
    /// </summary>
    public class UserModel
    {
        /// <summary>
        /// User's display name
        /// </summary>
        private readonly String Name;

        /// <summary>
        /// User ID
        /// </summary>
        private readonly String Uid;

        // TODO: Add List<NotebookModel>

        /// <summary>
        /// Construct a UserModel given a display name and uid.
        /// 
        /// </summary>
        /// <param name="Name">display name</param>
        /// <param name="Uid">uid</param>
        public UserModel(String Name, String Uid)
        {
            this.Name = Name;
            this.Uid = Uid;
        }

        public String GetName()
        {
            return Name;
        }

        public String GetUid()
        {
            return Uid;
        }
    }
}