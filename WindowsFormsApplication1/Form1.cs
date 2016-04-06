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
        public ImageForm imageForm;
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
            dialog.InitialDirectory = @"C:\Users\lukasz\Desktop\b_kon\zad2";
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

        private void button5_Click(object sender, EventArgs e)
        {
            imageDisplayed = imageForm.getImage();
            Histogram histogram = new Histogram(this, imageDisplayed);
            histogram.Show();
        }

        private void ChangeBrightness(int b)
        {
            imageDisplayed = imageForm.getImage();
            byte[] LUT = new byte[256];
            for (int i = 0; i < 256; i++)
            {
                if ((b + i) > 255)
                {
                    LUT[i] = 255;
                }
                else if ((b + i) < 0)
                {
                    LUT[i] = 0;
                }
                else
                {
                    LUT[i] = (byte)(b + i);
                }
            }

            Bitmap bitmap = (Bitmap)imageDisplayed.Clone();

            BitmapData bmpData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadWrite, bitmap.PixelFormat);
            byte[] pixelValues = new byte[Math.Abs(bmpData.Stride) * bitmap.Height];
            System.Runtime.InteropServices.Marshal.Copy(bmpData.Scan0, pixelValues, 0, pixelValues.Length);

            for (int i = 0; i < pixelValues.Length; i++)
            {
                pixelValues[i] = LUT[pixelValues[i]];
            }

            System.Runtime.InteropServices.Marshal.Copy(pixelValues, 0, bmpData.Scan0, pixelValues.Length);
            bitmap.UnlockBits(bmpData);
            imageForm.ChangeBitmap(bitmap);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            ChangeBrightness(15);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            ChangeBrightness(-15);
        }

        private void Stretch()
        {
            imageDisplayed = imageForm.getImage();

            byte[] LUT = new byte[256];
            byte vMaxR = (byte)Int32.Parse(textBox2.Text);
            byte vMinR = (byte)Int32.Parse(textBox1.Text);
            byte iMax = 255;
            for (int i = 0; i < 256; i++)
            {
                if (i > vMaxR)
                {
                    LUT[i] = 255;
                }
                else if (i < vMinR)
                {
                    LUT[i] = 0;
                }
                else
                {
                    LUT[i] = (byte)(iMax / (vMaxR - vMinR) * (i - vMinR));
                }
            }
            Bitmap bitmap = (Bitmap)imageDisplayed.Clone();

            BitmapData bmpData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadWrite, bitmap.PixelFormat);
            byte[] pixelValues = new byte[Math.Abs(bmpData.Stride) * bitmap.Height];
            System.Runtime.InteropServices.Marshal.Copy(bmpData.Scan0, pixelValues, 0, pixelValues.Length);

            for (int i = 0; i < pixelValues.Length; i++)
            {
                pixelValues[i] = LUT[pixelValues[i]];
            }

            System.Runtime.InteropServices.Marshal.Copy(pixelValues, 0, bmpData.Scan0, pixelValues.Length);
            bitmap.UnlockBits(bmpData);
            imageForm.ChangeBitmap(bitmap);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Stretch();
        }

        private int[] createRHistogram(Bitmap bmp)
        {
            int[] histogram_r = new int[256];
            float max = 0;

            for (int i = 0; i < bmp.Width; i++)
            {
                for (int j = 0; j < bmp.Height; j++)
                {
                    int redValue = bmp.GetPixel(i, j).R;
                    histogram_r[redValue]++;
                    if (max < histogram_r[redValue])
                        max = histogram_r[redValue];
                }
            }

            return histogram_r;
        }

        private int[] createGHistogram(Bitmap bmp)
        {
            int[] histogram_g = new int[256];
            float max = 0;

            for (int i = 0; i < bmp.Width; i++)
            {
                for (int j = 0; j < bmp.Height; j++)
                {
                    int GValue = bmp.GetPixel(i, j).G;
                    histogram_g[GValue]++;
                    if (max < histogram_g[GValue])
                        max = histogram_g[GValue];
                }
            }

            return histogram_g;
        }

        private int[] createBHistogram(Bitmap bmp)
        {
            int[] histogram_b = new int[256];
            float max = 0;

            for (int i = 0; i < bmp.Width; i++)
            {
                for (int j = 0; j < bmp.Height; j++)
                {
                    int BValue = bmp.GetPixel(i, j).R;
                    histogram_b[BValue]++;
                    if (max < histogram_b[BValue])
                        max = histogram_b[BValue];
                }
            }
           
            return histogram_b;
        }
        private void histogramEqualization()
        {
            Bitmap renderedImage = imageDisplayed;

            uint pixels = (uint)renderedImage.Height * (uint)renderedImage.Width;
            decimal Const = 255 / (decimal)pixels;

            int x, y, R, G, B;

            int[] cdfR = createRHistogram(imageDisplayed);
            int[] cdfG = createGHistogram(imageDisplayed);
            int[] cdfB = createBHistogram(imageDisplayed);

            for (int r = 1; r <= 255; r++)
            {
                cdfR[r] = cdfR[r] + cdfR[r - 1];
                cdfG[r] = cdfG[r] + cdfG[r - 1];
                cdfB[r] = cdfB[r] + cdfB[r - 1];
            }

            for (y = 0; y < renderedImage.Height; y++)
            {
                for (x = 0; x < renderedImage.Width; x++)
                {
                    Color pixelColor = renderedImage.GetPixel(x, y);

                    R = (int)((decimal)cdfR[pixelColor.R] * Const);
                    G = (int)((decimal)cdfG[pixelColor.G] * Const);
                    B = (int)((decimal)cdfB[pixelColor.B] * Const);

                    Color newColor = Color.FromArgb(R, G, B);
                    renderedImage.SetPixel(x, y, newColor);
                }
            }

            imageForm.ChangeBitmap(renderedImage);
        }
    

        private void button9_Click(object sender, EventArgs e)
        {
           histogramEqualization();
        }
    }
    
}
