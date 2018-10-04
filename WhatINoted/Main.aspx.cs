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
                notebookDivs[i].Attributes["class"] = "mainNotebooksDiv notebookColor";

                HtmlGenericControl titleDiv = new HtmlGenericControl("div");
                titleDiv.Attributes["class"] = "mainNotebookInnerDiv mainNotebookTitleDiv";
                titleDiv.InnerHtml = /*Notebook Title*/"Notebook for Science, 8th Edition";
                notebookDivs[i].Controls.Add(titleDiv);

                HtmlGenericControl imageDiv = new HtmlGenericControl("div");
                imageDiv.Attributes["class"] = "mainNotebookInnerDiv mainNotebookImageDiv";
                HtmlGenericControl image = new HtmlGenericControl("img");
                image.Attributes["src"] = "https://books.google.com/books?id=zyTCAlFPjgYC&printsec=frontcover&img=1&zoom=1&edge=curl&source=gbs_api";
                image.Attributes["alt"] = "";
                image.Attributes["class"] = "mainNotebookImage";
                imageDiv.Controls.Add(image);
                notebookDivs[i].Controls.Add(imageDiv);

                HtmlGenericControl numNotesDiv = new HtmlGenericControl("div");
                numNotesDiv.Attributes["class"] = "mainNotebookInnerDiv mainNotebookNumNotesDiv";
                numNotesDiv.InnerHtml = /*Number of Notes*/"13 Notes";
                notebookDivs[i].Controls.Add(numNotesDiv);
            }
            return notebookDivs;
        }
    }
}