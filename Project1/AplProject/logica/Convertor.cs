using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Project1.logica
{
    /**
     * TODO
     * Dithering alg
     * quanization alg
     */
     /**
      * BUG
      * double calculate geeft problemen 
      */
      /*
       * Splitsen Median cut
       * Octree 
       * converten pic
       */
    class Convertor
    {

        public static (Bitmap,double) ReplaceToClosest(Bitmap b, List<Color> palette, bool dithering , ProgressBar p)
        {
            double totKleurAfstand = 0.0;
            p.Maximum = b.Width * b.Height;
            p.Value = 1;
            for (int i = 0; i < b.Width; i++)
            {
                for (int j = 0; j < b.Height; j++)
                {
                    Color p2 = b.GetPixel(i, j);
                    Color p1 = ClosestColor(p2, palette);
                    
                    totKleurAfstand += Math.Sqrt(Math.Pow(p1.R - p2.R, 2) + Math.Pow(p1.G - p2.G, 2) + Math.Pow(p1.B - p2.B, 2));
                    PreformStep(p);
                    b.SetPixel(i, j, p1);

                    if (dithering)
                    {
                        Dithering(b, i, j, (int) Math.Sqrt(Math.Pow(p1.R - p2.R, 2)), (int)Math.Sqrt(Math.Pow(p1.G - p2.G, 2)), (int)Math.Sqrt(Math.Pow(p1.B - p2.B, 2)));
                    }
                }
            }
            totKleurAfstand = (totKleurAfstand / (b.Width * b.Height));

            return (b,totKleurAfstand);
        }

        private static Color ClosestColor(Color c, List<Color> palette)
        {
            int colorIndex = -1;
            double kleinsteKleurAfstand = int.MaxValue;
            for (int i = 0; i < palette.Count; i++)
            {

                double kleurAfstand = Math.Sqrt(Math.Pow(palette[i].R - c.R, 2) + Math.Pow(palette[i].G - c.G, 2) + Math.Pow(palette[i].B - c.B, 2));
                if (kleinsteKleurAfstand > kleurAfstand)
                {
                    colorIndex = i;
                    kleinsteKleurAfstand = kleurAfstand;
                }
            }

            return palette[colorIndex];
        }

        private static void Dithering(Bitmap b , int x , int y , int errorR , int errorG, int errorB)
        {
            if (!(x + 1 >= b.Width))
            {
                Color pixel = b.GetPixel(x + 1, y);
                Color ditherdPixel = MakeDitherdPixel(pixel, errorR, errorG, errorB, (7 / 16.0));
                b.SetPixel(x + 1, y, ditherdPixel);
            }
            

            if ((x != 0) && !(y + 1 >= b.Height))
            {
                Color pixel = b.GetPixel(x - 1, y + 1);
                Color ditherdPixel = MakeDitherdPixel(pixel, errorR, errorG, errorB, (3 / 16.0));
                b.SetPixel(x - 1, y + 1, ditherdPixel);
            }

            if (!(y+1 >= b.Height))
            {
                Color pixel = b.GetPixel(x, y + 1);
                Color ditherdPixel = MakeDitherdPixel(pixel, errorR, errorG, errorB, (5 / 16.0));
                b.SetPixel(x, y + 1, ditherdPixel);
            }

            if (!(y + 1 >= b.Height) && !(x + 1 >= b.Width))
            {
                Color pixel = b.GetPixel(x + 1, y + 1);
                Color ditherdPixel = MakeDitherdPixel(pixel, errorR, errorG, errorB, (1 / 16.0));
                b.SetPixel(x + 1, y + 1, ditherdPixel);
            }
                
        }

        private static Color MakeDitherdPixel(Color pixel, int errorR, int errorB, int errorG, double ratio)
        {

            int newR = (int)(pixel.R + (errorR * ratio));
            int newG = (int)(pixel.G + (errorG * ratio));
            int newB = (int)(pixel.B + (errorB * ratio));

            if (newR > 255)
            {
                newR = 255;
            }
            if (newG > 255)
            {
                newG = 255;
            }
            if (newB > 255)
            {
                newB = 255;
            }

            Color newPix = Color.FromArgb(pixel.A,newR,newG,newB);
            
            return newPix;
        }

        public static void PreformStep(ProgressBar b)
        {
            b.PerformStep();
        }

    }
}
