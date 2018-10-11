using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WhatINoted.Models
{
    public class NoteModel
    {
        private String Text { get; set; }

        private NotebookModel ParentNotebook { get; set; }

        private DateTime DateTimeModified { get; set; }

        private DateTime DateTimeCreated { get; set; }

        private String Id { get; set; }

        public NoteModel()
        {
            this.Text = "";
            this.ParentNotebook = null;
            this.DateTimeCreated = DateTime.Now;
            this.DateTimeModified = DateTime.Now;
            this.Id = null;
        }

        public NoteModel(String Text, NotebookModel ParentNotebook, DateTime DateTimeModified, DateTime DateTimeCreated, String Id)
        {
            this.Text = Text;
            this.ParentNotebook = ParentNotebook;
            this.DateTimeModified = DateTimeModified;
            this.DateTimeCreated = DateTimeCreated;
            this.Id = Id;
        }
    }
}