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
        private String _text;

        /// <summary>
        ///  Note text accessor.
        /// </summary>
        public String Text { get { return _text; } set { _text = value; } }

        /// <summary>
        /// The Notebook under which this Note is filed.
        /// </summary>
        private NotebookModel _parentNotebook;

        /// <summary>
        /// Parent notebook accessor.
        /// </summary>
        public NotebookModel ParentNotebook { get { return _parentNotebook; } set { _parentNotebook = value; } }

        /// <summary>
        /// The date and time at which this Note was last modified.
        /// </summary>
        private DateTime _dateTimeModified;

        /// <summary>
        /// Modification time accessor.
        /// </summary>
        public DateTime DateTimeModified { get { return _dateTimeModified; } set { _dateTimeModified = value; } }

        /// <summary>
        /// The date and time at which this Note was created.
        /// </summary>
        private DateTime _dateTimeCreated;

        /// <summary>
        /// Creation time accessor.
        /// </summary>
        public DateTime DateTimeCreated { get { return _dateTimeCreated; } set { _dateTimeCreated = value; } }

        /// <summary>
        /// Unique id generated by Firebase.
        /// </summary>
        private String _id;

        /// <summary>
        /// Id accessor.
        /// </summary>
        public String Id { get { return _id; } set { _id = value; } }


        /// <summary>
        /// Construct an empty NoteModel.
        /// </summary>
        public NoteModel()
        {
            _text = "";
            _parentNotebook = null;
            _dateTimeCreated = DateTime.Now;
            _dateTimeModified = DateTime.Now;
            _id = "";
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
            _text = Text;
            _parentNotebook = ParentNotebook;
            _dateTimeModified = DateTimeModified;
            _dateTimeCreated = DateTimeCreated;
            _id = Id;
        }

        public NoteModel(JsonNote jsonNote)
        {
            this.Text = jsonNote.Text;
            this.DateTimeCreated = jsonNote.Created;
            this.DateTimeModified = jsonNote.Modified;
        }
        
        /// <summary>
        /// Equality check.
        /// </summary>
        /// <param name="obj">Other model.</param>
        /// <returns>true if models are equal ignoring id</returns>
        public override bool Equals(object obj)
        {
            var model = obj as NoteModel;
            return model != null &&
                   _text == model._text &&
                   EqualityComparer<NotebookModel>.Default.Equals(_parentNotebook, model._parentNotebook) &&
                   _dateTimeModified == model._dateTimeModified &&
                   _dateTimeCreated == model._dateTimeCreated;
        }
    }
}