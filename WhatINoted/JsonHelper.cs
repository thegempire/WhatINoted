using System;
using System.Collections.Generic;

namespace WhatINoted
{
    public class JsonUserWrapper
    {
        public JsonUser document { get; set; }
    }
    public class JsonNotebookWrapper
    {
        public JsonNotebook document { get; set; }
    }
    public class JsonNoteWrapper
    {
        public JsonNote document { get; set; }
    }

    public class JsonUserCollection
    {
        public JsonUser[] Users { get; set; }
    }
    public class JsonNotebookCollection
    {
        public JsonNotebook[] Notebooks { get; set; }
    }
    public class JsonNoteCollection
    {
        public JsonNote[] Notes { get; set; }
    }

    public class JsonEntity
    {
        public string name { get; set; }
        public string createTime { get; set; }
        public string updateTime { get; set; }
        private JsonUserFields fields { get; set; }
        public DateTime Created
        {
            get
            {
                return fields.created.timeStampValue;
            }

            set
            {
                fields.created.timeStampValue = value;
            }
        }
        public DateTime Modified
        {
            get
            {
                return fields.modified.timeStampValue;
            }

            set
            {
                fields.modified.timeStampValue = value;
            }
        }
    }

    public class JsonUser : JsonEntity
    {
        public JsonUserFields fields { get; set; }

        public string DisplayName
        {
            get
            {
                return fields.displayName.stringValue;
            }

            set
            {
                fields.displayName.stringValue = value;
            }
        }
        public string Email
        {
            get
            {
                return fields.email.stringValue;
            }

            set
            {
                fields.email.stringValue = value;
            }
        }
    }

    public class JsonNotebook : JsonEntity
    {
        public JsonNotebookFields fields { get; set; }

        public string UserID
        {
            get
            {
                return fields.userID.stringValue;
            }

            set
            {
                fields.userID.stringValue = value;
            }
        }
        public string ISBN
        {
            get
            {
                return fields.isbn.stringValue;
            }

            set
            {
                fields.isbn.stringValue = value;
            }
        }
        public string Title
        {
            get
            {
                return fields.title.stringValue;
            }

            set
            {
                fields.title.stringValue = value;
            }
        }
        public string Author
        {
            get
            {
                return fields.author.stringValue;
            }

            set
            {
                fields.author.stringValue = value;
            }
        }
        public string Publisher
        {
            get
            {
                return fields.publisher.stringValue;
            }

            set
            {
                fields.publisher.stringValue = value;
            }
        }
        public DateTime PublishDate
        {
            get
            {
                return fields.publishDate.timeStampValue;
            }

            set
            {
                fields.publishDate.timeStampValue = value;
            }
        }
        public string CoverURL
        {
            get
            {
                return fields.coverURL.stringValue;
            }

            set
            {
                fields.coverURL.stringValue = value;
            }
        }
    }

    public class JsonNote : JsonEntity
    {
        public JsonNoteFields fields { get; set; }

        public string NotebookID
        {
            get
            {
                return fields.notebookID.stringValue;
            }

            set
            {
                fields.notebookID.stringValue = value;
            }
        }
        public string UserID
        {
            get
            {
                return fields.userID.stringValue;
            }

            set
            {
                fields.userID.stringValue = value;
            }
        }
        public string Text
        {
            get
            {
                return fields.text.stringValue;
            }

            set
            {
                fields.text.stringValue = value;
            }
        }
    }

    public class JsonEntityFields
    {
        public JsonDateTimeField created { get; set; }
        public JsonDateTimeField modified { get; set; }
    }

    public class JsonUserFields : JsonEntityFields
    {
        public JsonStringField displayName { get; set; }
        public JsonStringField email { get; set; }
    }

    public class JsonNotebookFields : JsonEntityFields
    {
        public JsonStringField userID { get; set; }
        public JsonStringField isbn { get; set; }
        public JsonStringField title { get; set; }
        public JsonStringField author { get; set; }
        public JsonStringField publisher { get; set; }
        public JsonDateTimeField publishDate { get; set; }
        public JsonStringField coverURL { get; set; }
    }

    public class JsonNoteFields : JsonEntityFields
    {
        public JsonStringField notebookID { get; set; }
        public JsonStringField userID { get; set; }
        public JsonStringField text { get; set; }
    }

    public class JsonStringField
    {
        public string stringValue { get; set; }
    }

    public class JsonNumericField
    {
        public int integerValue { get; set; }
        public double doubleValue { get; set; }
    }

    public class JsonDateTimeField
    {
        public DateTime timeStampValue { get; set; }
    }
}