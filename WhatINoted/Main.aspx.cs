using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace WhatINoted
{
    public partial class Main : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //get all (or some) notebooks from database

            //create a div for each notebook
            HtmlGenericControl[] notebookDivs = CreateNotebookDivs(/*Notebooks*/);

            for (int i = 0; i < notebookDivs.Length; i++)
            {
                MainNotebooks.Controls.Add(notebookDivs[i]);

            }
        }

        private HtmlGenericControl[] CreateNotebookDivs(/*Notebooks*/)
        {
            HtmlGenericControl[] notebookDivs = new HtmlGenericControl[/*Notebooks.Count*/8];
            /*foreach(Notebook n in Notebooks)
             * {
             * 
             * }
             */
            //temp
            for (int i = 0; i < 8; i++)
            {
                notebookDivs[i] = new HtmlGenericControl("div");
                notebookDivs[i].Attributes["class"] = "mainNotebooksDiv";
                switch (i % 3)
                {
                    case 0:
                        notebookDivs[i].Attributes["class"] += " mainNotebooksColumn1";
                        break;
                    case 1:
                        notebookDivs[i].Attributes["class"] += " mainNotebooksColumn2";
                        break;
                    case 2:
                        notebookDivs[i].Attributes["class"] += " mainNotebooksColumn3";
                        break;
                }
            }
            return notebookDivs;
        }
    }
}