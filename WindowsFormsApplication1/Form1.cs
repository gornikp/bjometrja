using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
            dialog.Filter = "BMP Files (*.bmp)|*.bmp|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif | TIF Files(*.tif) | *.tif";
            dialog.InitialDirectory = @"C:\";
            dialog.Title = "Please select an image file to encrypt.";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string file = dialog.FileName;
                try
                {
                imageDisplayed = new Bitmap(dialog.FileName);
                    pictureBox1.Image = imageDisplayed;
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
            Rbox.Text = "";
            Gbox.Text = "";
            Bbox.Text = "";
            MouseEventArgs me = (MouseEventArgs)e;
            point = me.Location;
            if (imageDisplayed != null)
            {
                color = imageDisplayed.GetPixel((int)point.X, (int)point.Y);
                Rbox.Text = color.R.ToString();
                Gbox.Text = color.G.ToString();
                Bbox.Text = color.B.ToString();
            }
        }

        private void pictureBox1_MouseEnter(object sender, EventArgs e)
        {
        }

        private void pictureBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            MouseEventArgs me = (MouseEventArgs)e;
            point = me.Location;
            if (imageDisplayed != null)
            {
                color = Color.FromArgb(0, 0, 0, 0);
                imageDisplayed.SetPixel((int)point.X, (int)point.Y, color);
                pictureBox1.Image = imageDisplayed;
                Rbox.Text = color.R.ToString();
                Gbox.Text = color.G.ToString();
                Bbox.Text = color.B.ToString();
            }
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
        }
    }
    
}
