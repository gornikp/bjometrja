using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Windows.Input;

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
        List<Point> elo = new List<Point>();

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
            pictureBox1.MouseMove -= new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseMove);
            MouseEventArgs me = (MouseEventArgs)e;
            Point = me.Location;

            if (mainForm.zoomed)
            {
                ZoomOut();
                Point = me.Location;
                Color = image.GetPixel((int)Point.X/8, (int)Point.Y/8);
                ZoomIn();
            }
            else if (!mainForm.zoomed)
            {
                Point = me.Location;
                Color = image.GetPixel((int)Point.X, (int)Point.Y);
            }
            //Point point = me.GetPosition(pict);


            //Debug.WriteLine("X: " + point.X + "\n Y: " + point.Y);

            //Color = image.GetPixel(e.X, e.Y);

            //string siema = Color.ToString();
            //Debug.WriteLine("Siema: {0}", siema);

            mainForm.RboxText = Color.R.ToString();
            mainForm.GboxText = Color.G.ToString();
            mainForm.BboxText = Color.B.ToString();
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            //MouseEventArgs me = (MouseEventArgs)e;
            Point p = pictureBox1.PointToClient(Cursor.Position);
            Color c = Color.Black;
            //Point = e;

            if (mainForm.zoomed)
            {
                ZoomOut();
                //Point = p;
                c = image.GetPixel((int)p.X / 8, (int)p.Y / 8);
                ZoomIn();
            }
            else if (!mainForm.zoomed)
            {
                //Point = p;
                c = image.GetPixel((int)p.X, (int)p.Y);
            }
            elo.Add(Point);
            //Point point = me.GetPosition(pict);


            //Debug.WriteLine("X: " + point.X + "\n Y: " + point.Y);

            //Color = image.GetPixel(e.X, e.Y);

            //string siema = Color.ToString();
            //Debug.WriteLine("Siema: {0}", siema);

            mainForm.RboxText = c.R.ToString();
            mainForm.GboxText = c.G.ToString();
            mainForm.BboxText = c.B.ToString();
        }

        private void pictureBox1_MouseEnter(object sender, EventArgs e)
        {
            pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseMove);
        }
    }
}
