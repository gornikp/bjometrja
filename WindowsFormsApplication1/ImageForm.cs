using System;
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

        public ImageForm()
        {
            InitializeComponent();
        }

        public ImageForm(Form callingForm, Bitmap image)
        {
            mainForm = callingForm as Form1;
            InitializeComponent();
           
            this.image = image;
            SetImage();
            
        }
        private void SetImage()
        {
            pictureBox1.Width = this.image.Width;
            pictureBox1.Height = this.image.Height;

            this.Width = image.Width;
            this.Height = image.Height;

            panel1.AutoScroll = true;
            panel1.Width = this.Width;
            panel1.Height = this.Height;

            pictureBox1.Image = this.image;
        }

        public void ZoomIn()
        {
            int zoomFactor = 8;
            Size newSize = new Size((int)(image.Width * zoomFactor), (int)(image.Height * zoomFactor));
            Bitmap zoomed = new Bitmap(image, newSize);
            this.image = zoomed;
            SetImage();
        }

        public void ZoomOut()
        {
            int zoomFactor = 8;
            Size newSize = new Size((int)(image.Width / zoomFactor), (int)(image.Height / zoomFactor));
            Bitmap zoomed = new Bitmap(image, newSize);
            this.image = zoomed;
            SetImage();
        }

        public void ChangePixel(Color rgb)
        {
            image.SetPixel((int)Point.X, (int)Point.Y, rgb);

        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            MouseEventArgs me = (MouseEventArgs)e;
            Point = me.Location;

            Color = image.GetPixel((int)Point.X, (int)Point.Y);
            mainForm.RboxText = Color.R.ToString();
            mainForm.GboxText = Color.G.ToString();
            mainForm.BboxText = Color.B.ToString();
        }
    }
}
