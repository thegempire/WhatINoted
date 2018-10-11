using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WhatINoted.Models
{
    /// <summary>
    /// UserModel stores information representing an application user.
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

        /// <summary>
        /// List of user's Notebooks
        /// </summary>
        private List<NotebookModel> Notebooks { get; set; }

        /// <summary>
        /// Construct a UserModel given a display name and uid. Initialize Notebooks to empty list.
        /// </summary>
        /// <param name="Name">display name</param>
        /// <param name="Uid">uid</param>
        public UserModel(String Name, String Uid)
        {
            this.Name = Name;
            this.Uid = Uid;
            this.Notebooks = new List<NotebookModel>();
        }

        /// <summary>
        /// Construct a UserModel with the given characteristics.
        /// </summary>
        /// <param name="Name">display name</param>
        /// <param name="Uid">uid</param>
        /// <param name="Notebooks">list of notebooks</param>
        public UserModel(String Name, String Uid, List<NotebookModel> Notebooks)
        {
            this.Name = Name;
            this.Uid = Uid;
            this.Notebooks = Notebooks;
        }
    }
}