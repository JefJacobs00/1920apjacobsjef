using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace AplProject
{
    
    class Convertor
    {
        private Bitmap bmp;

        public Convertor(Bitmap bitmap)
        {
            bmp = bitmap;
        }

        public Bitmap convert(int pallet,Algorythm alg)
        {
            if ((alg.Equals(Algorythm.MedianCut)))
            {
                //return pallet
                List<Color> palette = MedianCut(bmp);
                return ReplaceToClosest(bmp, palette);
            }
            return bmp;
        }

        private List<Color> MedianCut(Bitmap bitmap)
        {
            List<Color> pixelList = GetPixelList(bitmap);
            //Sorting the colors
            int r = 0;
            int g = 0;
            int b = 0;
            for (int i = 0; i < pixelList.Count; i++)
            {
                r += pixelList[i].R;
                g += pixelList[i].G;
                b += pixelList[i].B;
            }
            if ((r>b) && (r>g))
            {
                pixelList.Sort(delegate (System.Drawing.Color left, System.Drawing.Color right)
                {
                    return right.R.CompareTo(left.R);
                });
            }
            else if ((g>r) && (g>b))
            {
                pixelList.Sort(delegate (System.Drawing.Color left, System.Drawing.Color right)
                {
                    return right.G.CompareTo(left.G);
                });
            }
            else if((b>r) && (b>g)){
                pixelList.Sort(delegate (System.Drawing.Color left, System.Drawing.Color right)
                {
                    return right.B.CompareTo(left.B);
                });
            }
            


            List<Color[]> buckets = new List<Color[]>();
            List<Color[]> bucketsHelp = new List<Color[]>();
            buckets.Add(pixelList.ToArray());
            for (int i = 0; i < 8; i++) 
            {
                for (int j = 0; j < buckets.Count; j++)
                {
                    List<Color[]> c = Cut(buckets[j]);
                    bucketsHelp.Add(c[0]);
                    bucketsHelp.Add(c[1]);
                }

                buckets.Clear();
                for (int j = 0; j < bucketsHelp.Count; j++)
                {
                    buckets.Add(bucketsHelp[j]);
                }
                bucketsHelp.Clear();
            }

            //https://sighack.com/post/averaging-rgb-colors-the-right-way
            List<Color> pallet = new List<Color>();
            for (int i = 0; i < buckets.Count; i++)
            {
                pallet.Add(BerekenGemiddelde(buckets[i]));
            }
            return pallet;
        }

        private List<Color> GetPixelList(Bitmap bmp) 
        {
            List<Color> pixelList = new List<Color>();
            for (int i = 0; i < bmp.Width; i++)
            {
                for (int j = 0; j < bmp.Height; j++)
                {
                    pixelList.Add(bmp.GetPixel(i, j));
                }
            }
            return pixelList;
        }

        private List<Color[]> Cut(Color[] c)
        {
            List<Color[]> split = new List<Color[]>();
            Color[] arr1 = new Color[c.Length / 2];
            Color[] arr2 = new Color[c.Length / 2];
            for (int i = 0; i < c.Length; i++)
            {
                if (i < (c.Length / 2))
                {
                    arr1[i] = c[i];
                }
                else if(i > (c.Length / 2))
                {
                    arr2[i-(c.Length/2)-1] = c[i];
                }
            }
            split.Add(arr1);
            split.Add(arr2);
            return split;
        }

        private Color BerekenGemiddelde(Color[] c)
        {
            double r = 0;
            double g = 0;
            double b = 0;

            for (int i = 0; i < c.Length; i++)
            {
                r += Math.Pow(c[i].R, 2);
                g += Math.Pow(c[i].G, 2);
                b += Math.Pow(c[i].B, 2);
            }
            
            return Color.FromArgb(255, (int) Math.Sqrt(r / c.Length), (int)Math.Sqrt(g / c.Length), (int)Math.Sqrt(b / c.Length) );
        }

        private Color ClosestColor(Color c,List<Color> palette)
        {
            int colorIndex = -1;
            double kleinsteKleurAfstand = int.MaxValue;
            for (int i = 0; i < palette.Count; i++)
            {
                
                double kleurAfstand = Math.Sqrt(Math.Pow( palette[i].R - c.R, 2) + Math.Pow(palette[i].G - c.G, 2) + Math.Pow(palette[i].B - c.B, 2));
                if (kleinsteKleurAfstand > kleurAfstand)
                {
                    colorIndex = i;
                    kleinsteKleurAfstand = kleurAfstand;
                }
            }

            return palette[colorIndex]; 
        }

        private Bitmap ReplaceToClosest(Bitmap b , List<Color> palette)
        {
            for (int i = 0; i < b.Width; i++)
            {
                for (int j = 0; j < b.Height; j++)
                {
                    b.SetPixel(i, j, ClosestColor(b.GetPixel(i, j), palette));
                }
            }
            return b;
            
        }
    }
}
