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

        public static (Bitmap,double) ReplaceToClosest(Bitmap b, List<Color> palette, ProgressBar p)
        {
            double totKleurAfstand = 0.0;
            for (int i = 0; i < b.Width; i++)
            {
                for (int j = 0; j < b.Height; j++)
                {
                    Color p2 = b.GetPixel(i, j);
                    Color p1 = ClosestColor(p2, palette);

                    totKleurAfstand += Math.Sqrt(Math.Pow(p1.R - p2.R, 2) + Math.Pow(p1.G - p2.G, 2) + Math.Pow(p1.B - p2.B, 2));
                    PreformStep(p);
                    b.SetPixel(i, j, p1);
                    

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

        public static void PreformStep(ProgressBar b)
        {
            b.PerformStep();
        }

    }
}
