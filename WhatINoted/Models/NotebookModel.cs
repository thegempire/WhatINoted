using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WhatINoted.Models
{
    public class NotebookModel
    {
        String Title { get; set; }

        String Author { get; set; }

        IsbnModel Isbn { get; set; }

        List<NoteModel> Notes { get; set; }

        String Id { get; set; }

        public NotebookModel()
        {
            this.Title = null;
            this.Author = null;
            this.Isbn = null;
            this.Notes = new List<NoteModel>();
            this.Id = null;
        }

        public NotebookModel(String Title, String Author, IsbnModel Isbn, List<NoteModel> Notes, String Id)
        {
            this.Title = Title;
            this.Author = Author;
            this.Isbn = Isbn;
            this.Notes = Notes;
            this.Id = Id;
        }
    }
}