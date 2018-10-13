﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WhatINoted.Models
{
    /// <summary>
    /// NoteModel stores information representing a Note.
    /// </summary>
    public class NoteModel
    {
        /// <summary>
        /// The Note's body text.
        /// </summary>
        private String Text { get; set; }

        /// <summary>
        /// The Notebook under which this Note is filed.
        /// </summary>
        private NotebookModel ParentNotebook { get; set; }

        /// <summary>
        /// The date and time at which this Note was last modified.
        /// </summary>
        private DateTime DateTimeModified { get; set; }

        /// <summary>
        /// The date and time at which this Note was created.
        /// </summary>
        private DateTime DateTimeCreated { get; set; }

        /// <summary>
        /// Unique id generated by Firebase.
        /// </summary>
        private String Id { get; set; }

        /// <summary>
        /// Construct an empty NoteModel.
        /// </summary>
        public NoteModel()
        {
            this.Text = "";
            this.ParentNotebook = null;
            this.DateTimeCreated = DateTime.Now;
            this.DateTimeModified = DateTime.Now;
            this.Id = "";
        }

        /// <summary>
        /// Construct a NoteModel with the given characteristics.
        /// </summary>
        /// <param name="Text">note body text</param>
        /// <param name="ParentNotebook">notebook under which this note is filed</param>
        /// <param name="DateTimeModified">date/time of last modification</param>
        /// <param name="DateTimeCreated">date/time of creation</param>
        /// <param name="Id">unique firebase id</param>
        public NoteModel(String Text, NotebookModel ParentNotebook, DateTime DateTimeModified, DateTime DateTimeCreated, String Id)
        {
            this.Text = Text;
            this.ParentNotebook = ParentNotebook;
            this.DateTimeModified = DateTimeModified;
            this.DateTimeCreated = DateTimeCreated;
            this.Id = Id;
        }

        public override bool Equals(object obj)
        {
            var model = obj as NoteModel;
            return model != null &&
                   Text == model.Text &&
                   EqualityComparer<NotebookModel>.Default.Equals(ParentNotebook, model.ParentNotebook) &&
                   DateTimeModified == model.DateTimeModified &&
                   DateTimeCreated == model.DateTimeCreated;
        }
    }
}