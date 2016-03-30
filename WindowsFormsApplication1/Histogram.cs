using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Histogram : Form
    {
        private Form1 mainForm = null;
        private Bitmap image = null;

        private List<int> R = new List<int>();
        private List<int> G = new List<int>();
        private List<int> B = new List<int>();

        private List<Array> RGBList = new List<Array>();

        public Histogram()
        {
            InitializeComponent();
        }

        public Histogram(Form callingForm, Bitmap image)
        {
            mainForm = callingForm as Form1;
            InitializeComponent();

            this.image = image;
            //ReadRGBValues();
            DrawHistograms();
            DrawAverageHistogram();
   
        }

        public void RefreshHistograms(Bitmap image)
        {
            this.image = image;
            //ReadRGBValues();
            DrawHistograms();
            DrawAverageHistogram();
        }

        //public void ReadRGBValues()
        //{
        //    int imageWidth = image.Width;
        //    int imageHeight = image.Height;

        //    int[,] Rarr = new int[imageWidth, imageHeight];
        //    int[,] Garr = new int[imageWidth, imageHeight];
        //    int[,] Barr = new int[imageWidth, imageHeight];

        //    for (int i=0; i < imageWidth; i++)
        //    {
        //        for(int j=0; j < imageHeight; j++)
        //        {
        //            Rarr[i, j] = image.GetPixel(i, j).R;
        //            Garr[i, j] = image.GetPixel(i, j).G;
        //            Barr[i, j] = image.GetPixel(i, j).B;

        //            R.Add(image.GetPixel(i, j).R);
        //            G.Add(image.GetPixel(i, j).G);
        //            B.Add(image.GetPixel(i, j).B);
        //        }
        //    }

        //    RGBList.Add(Rarr);
        //    RGBList.Add(Garr);
        //    RGBList.Add(Barr);
        //}

        //private void SortRGB()
        //{
        //    R.Sort();
        //    B.Sort();
        //    G.Sort();
        //}

        //public void debug()
        //{
        //    int[,] siema = (int[,])RGBList.ElementAt(0);
        //    foreach(int val in siema)
        //    {
        //        Console.WriteLine(val);
        //    }
        //}

        private Bitmap DrawHistogram(int[] histogram_r, float max)
        {
            int histHeight = 128;
            Bitmap img = new Bitmap(256, histHeight + 10);
            using (Graphics g = Graphics.FromImage(img))
            {
                for (int i = 0; i < histogram_r.Length; i++)
                {
                    float pct = histogram_r[i] / max;   // What percentage of the max is this value?
                    g.DrawLine(Pens.Black,
                        new Point(i, img.Height - 5),
                        new Point(i, img.Height - 5 - (int)(pct * histHeight))  // Use that percentage of the height
                        );
                }
            }

            return img;
        }

        private void DrawHistograms()
        {
            Bitmap bmp = image;
            int[] histogram_r = new int[256];
            int[] histogram_g = new int[256];
            int[] histogram_b = new int[256];
            float maxR = 0;
            float maxG = 0;
            float maxB = 0;

            for (int i = 0; i < bmp.Width; i++)
            {
                for (int j = 0; j < bmp.Height; j++)
                {
                    Color pixel = bmp.GetPixel(i, j);
                    histogram_r[pixel.R]++;
                    histogram_g[pixel.G]++;
                    histogram_b[pixel.B]++;

                    if (maxR < histogram_r[pixel.R])
                        maxR = histogram_r[pixel.R];

                    if (maxG < histogram_g[pixel.G])
                        maxG = histogram_g[pixel.G];

                    if (maxB < histogram_b[pixel.B])
                        maxB = histogram_b[pixel.B];
                }
            }

            pictureBox1.Image = DrawHistogram(histogram_r, maxR);
            pictureBox2.Image = DrawHistogram(histogram_g, maxG);
            pictureBox3.Image = DrawHistogram(histogram_b, maxB);
        }

        private void DrawAverageHistogram()
        {
            Bitmap bmp = image;
            int[] histogram_r = new int[256];
            int[] histogram_g = new int[256];
            int[] histogram_b = new int[256];
            float maxR = 0;
            float maxG = 0;
            float maxB = 0;

            for (int i = 0; i < bmp.Width; i++)
            {
                for (int j = 0; j < bmp.Height; j++)
                {
                    Color pixel = bmp.GetPixel(i, j);
                    int average = (pixel.R + pixel.G + pixel.B) / 3;
                    histogram_r[average]++;

                    if (maxR < histogram_r[pixel.R])
                        maxR = histogram_r[pixel.R];

                    if (maxG < histogram_g[pixel.G])
                        maxG = histogram_g[pixel.G];

                    if (maxB < histogram_b[pixel.B])
                        maxB = histogram_b[pixel.B];


                }
            }

            float max = new List<float> (new float[] { maxR, maxG, maxB }).Max();

            pictureBox4.Image = DrawHistogram(histogram_r, maxR);
            //pictureBox2.Image = DrawHistogram(histogram_g, maxG);
            //pictureBox3.Image = DrawHistogram(histogram_b, maxB);
        }
    }
}
