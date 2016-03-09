using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        private Bitmap imageDisplayed = null;
        private Color color;
        Point point;
        ImageForm imageForm;
        public bool zoomed = false;

        public String RboxText
        {
            get { return RBox.Text; }
            set { RBox.Text = value; }
        }

        public String GboxText
        {
            get { return GBox.Text; }
            set { GBox.Text = value; }
        }
        public String BboxText
        {
            get { return BBox.Text; }
            set { BBox.Text = value; }
        }
        public Form1()
        {
            InitializeComponent();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            int size = -1;
            OpenFileDialog dialog = new OpenFileDialog();
            //dialog.Filter = "BMP Files (*.bmp)|*.bmp|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif | TIF Files(*.tif) | *.tif";
            dialog.InitialDirectory = @"C:\Users\lukasz\Desktop\b_kon";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string file = dialog.FileName;
                try
                {
                    imageDisplayed = new Bitmap(dialog.FileName);
                    ImageForm imageForm = new ImageForm(this, imageDisplayed);
                    this.imageForm = imageForm;
                    imageForm.Show();
                    //pictureBox1.Image = imageDisplayed;
                }
                catch (IOException)
                {
                }
            }                      
            Console.WriteLine(size); // <-- Shows file size in debugging mode.
            Console.WriteLine(dialog); // <-- For debugging use.
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {

        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            
        }


        private void pictureBox1_MouseEnter(object sender, EventArgs e)
        {
        }

        private void pictureBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!zoomed)
            {
                imageForm.ZoomIn();
                button1.Text = "Zoom out";
                zoomed = true;
            }
            else if(zoomed)
            {
                imageForm.ZoomOut();
                button1.Text = "Zoom in";
                zoomed = false;
            }
            imageForm.SetImage();
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Color rgb = Color.FromArgb(int.Parse(RBox.Text), int.Parse(GBox.Text), int.Parse(BBox.Text));
            imageForm.ChangePixel(rgb);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            saveFileDialog1.Filter = "Images|*.bmp;*.png;*.jpg";
            ImageFormat format = ImageFormat.Bmp;
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.RestoreDirectory = true;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string ext = System.IO.Path.GetExtension(saveFileDialog1.FileName);
                switch (ext)
                {
                    case ".jpg":
                        format = ImageFormat.Jpeg;
                        break;
                    case ".png":
                        format = ImageFormat.Png;
                        break;
                }
                imageForm.GetImage().Save(saveFileDialog1.FileName, format);
            }
            if (zoomed)
                imageForm.ZoomIn();
        }
    }
    
}
