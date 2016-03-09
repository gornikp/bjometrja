﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class ImageForm : Form
    {
        private Form1 mainForm = null;
        Point Point;
        private Color Color;
        private Bitmap image = null;
        private Bitmap imageZoomCache = null;
        private Bitmap zoomedImage = null;

        public ImageForm()
        {
            InitializeComponent();
        }

        public ImageForm(Form callingForm, Bitmap image)
        {
            mainForm = callingForm as Form1;
            InitializeComponent();

            mainForm.RboxText = "0";
            mainForm.GboxText = "0";
            mainForm.BboxText = "0";

            Bitmap IndexedImage = image;
            Bitmap bitmap = IndexedImage.Clone(new Rectangle(0, 0, IndexedImage.Width, IndexedImage.Height), System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            this.image = bitmap;

            this.Width = this.image.Width;
            this.Height = this.image.Height;
            SetImage();
            
        }
        public void SetImage()
        {
            pictureBox1.Width = this.image.Width;
            pictureBox1.Height = this.image.Height;

            panel1.AutoScroll = true;
            panel1.Width = this.Width;
            panel1.Height = this.Height;

            pictureBox1.Image = this.image;
        }

        public Bitmap GetImage()
        {
            if (mainForm.zoomed)
                ZoomOut();
            return image;
        }

        public void ZoomIn()
        {
            if (zoomedImage != null) zoomedImage.Dispose();
            imageZoomCache = this.image;
            int zoomFactor = 8;
            Size newSize = new Size((int)(image.Width * zoomFactor), (int)(image.Height * zoomFactor));
            zoomedImage = new Bitmap(image, newSize);
            this.image = zoomedImage;
            //zoomed.Dispose();
        }

        public void ZoomOut()
        {
            //int zoomFactor = 8;
            //Size newSize = new Size((int)(image.Width / zoomFactor), (int)(image.Height / zoomFactor));
            //Bitmap zoomed = new Bitmap(image, newSize);
            this.image = this.imageZoomCache;
            //this.image = zoomed;
        }

        public void ChangePixel(Color rgb)
        {
            if(mainForm.zoomed)
            {
                ZoomOut();
                image.SetPixel((int)Point.X / 8, (int)Point.Y / 8, rgb);
                ZoomIn();
            }
            else if (!mainForm.zoomed)
                image.SetPixel((int)Point.X, (int)Point.Y, rgb);

            SetImage();
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            MouseEventArgs me = (MouseEventArgs)e;
            Point = me.Location;

            if (mainForm.zoomed)
            {
                ZoomOut();
                Color = image.GetPixel((int)Point.X, (int)Point.Y);
                ZoomIn();
            } else if (!mainForm.zoomed)
                Color = image.GetPixel((int)Point.X, (int)Point.Y);

            mainForm.RboxText = Color.R.ToString();
            mainForm.GboxText = Color.G.ToString();
            mainForm.BboxText = Color.B.ToString();
        }
    }
}
