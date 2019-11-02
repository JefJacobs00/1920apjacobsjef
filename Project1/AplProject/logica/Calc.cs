using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1.logica
{
    class Calc
    {
        public static double GemiddeldeKleurAfstand(Bitmap b1,Bitmap b2 )
        {
            double totKleurAfstand = 0.0;
            for (int i = 0; i < b1.Width; i++)
            {
                for (int j = 0; j < b1.Height; j++)
                {
                    Color p1 = b1.GetPixel(i, j);
                    Color p2 = b2.GetPixel(i, j);
                    totKleurAfstand += Math.Sqrt(Math.Pow(p1.R - p2.R, 2) + Math.Pow(p1.G - p2.G, 2) + Math.Pow(p1.B - p2.B, 2));
                }
            }
            return (totKleurAfstand / (b1.Width * b1.Height));
        }
    }
}
