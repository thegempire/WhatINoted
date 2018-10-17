using System;
using System.Drawing;
namespace WhatINoted
{
    /// <summary>
    /// A View that contains functionality to generate text from an image.
    /// </summary>
    public abstract class TextGenerationView : View
    {
        /// <summary>
        /// The image.
        /// </summary>
        private Image Image;

        /// <summary>
        /// The top left point of the crop rectangle.
        /// </summary>
        private Point CropRectTopLeft;

        /// <summary>
        /// The bottom right point of the crop rectangle.
        /// </summary>
        private Point CropRectBotRight;

        /// <summary>
        /// The number of degrees by which the image has
        /// been rotated clockwise.
        /// </summary>
        private int RotationDegrees;

        public TextGenerationView()
        {

        }

        /// <summary>
        /// Allows the user to select a file to serve as the image.
        /// </summary>
        protected void SelectImage()
        {

        }

        /// <summary>
        /// Selects the region of the image to send to OCR engine.
        /// </summary>
        /// <param name="topLeft">Top left of the region.</param>
        /// <param name="botRight">Bot right of the region.</param>
        protected void SelectRegion(Point topLeft, Point botRight)
        {

        }

        /// <summary>
        /// Rotates the image in the direction specified by the parameter.
        /// </summary>
        /// <param name="clockwise">If set to <c>true</c> clockwise.</param>
        protected void RotateImage(bool clockwise)
        {

        }

        /// <summary>
        /// Generates the text from the image using the OCR engine.
        /// </summary>
        protected abstract void GenerateText();
    }
}
