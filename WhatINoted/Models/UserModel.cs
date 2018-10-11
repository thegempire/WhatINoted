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
        public readonly String Name;

        /// <summary>
        /// User ID
        /// </summary>
        public readonly String Uid;

        private List<NotebookModel> Notebooks { get; set; }

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

            Notebooks = new List<NotebookModel>();
        }

        public UserModel(String Name, String Uid, List<NotebookModel> Notebooks)
        {
            this.Name = Name;
            this.Uid = Uid;
            this.Notebooks = Notebooks;
        }
    }
}