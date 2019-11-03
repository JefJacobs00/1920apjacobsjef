using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Project1.logica
{
    class Kmeans
    {
        Bitmap bitmap;
        List<Color> pallet;
        public Kmeans(Bitmap bitmap)
        {
            this.bitmap = bitmap;
        }

        public (Bitmap, double) convert(ProgressBar progressAfbeelding, int palletSize)
        {
            Dictionary<Color, int> colors = GetMostUsedColors(GetPixels(bitmap));
            pallet = CreatePallet(colors, palletSize);
            
            
            return Convertor.ReplaceToClosest(bitmap, pallet, progressAfbeelding);
        }
        public Bitmap CreatePalletMap(int x, int y, int page)
        {
            pallet.Sort(delegate (System.Drawing.Color left, System.Drawing.Color right)
            {
                return right.R.CompareTo(left.R);
            });
            VisualColorPallet visual = new VisualColorPallet(pallet);
            return visual.CreatePalletBitmap(x, y, page);
        }

            private List<Color> GetPixels(Bitmap b)
        {
            List<Color> pixelList = new List<Color>();
            for (int i = 0; i < b.Width; i++)
            {
                for (int j = 0; j < b.Height; j++)
                {
                    pixelList.Add(b.GetPixel(i, j));
                }
            }
            return pixelList;
        }

        private Dictionary<Color, int> GetMostUsedColors(List<Color> c)
        {
            Dictionary<Color, int> colors = new Dictionary<Color, int>();

            
            for (int i = 0; i < c.Count; i++)
            {

                if (colors.ContainsKey(c[i]))
                {
                    colors[c[i]]++;
                }
                else
                {
                    colors.Add(c[i], 0);
                }
                
            }
            //https://stackoverflow.com/questions/289/how-do-you-sort-a-dictionary-by-value
            var sorted = colors.OrderByDescending(x => x.Value);
            colors = sorted.ToDictionary(x => x.Key, x => x.Value);
            return colors;
        }

        private List<Color> CreatePallet(Dictionary<Color, int> colors, int palletSize)
        {
            List<Color> colorList = colors.Keys.ToList();
            List<Color> pallet = new List<Color>();

            Dictionary<Color, List<Color>> cluster = new Dictionary<Color, List<Color>>();
            
            for (int i = 0; i <= (palletSize - 1); i++)
            {
                cluster.Add(colorList[i], new List<Color>());


            }
            List<Color> keys = cluster.Keys.ToList();


            for (int i = 0; i < colorList.Count; i++)
            {
                Color closestDataPoint = ClosestColor(colorList[i], keys);
                cluster[closestDataPoint].Add(colorList[i]);
            }

            for (int i = 0; i < cluster.Count; i++)
            {
                pallet.Add(ClusterToAvg(cluster[keys[i]]));
                

            }
            return pallet;
        }
        private Color ClusterToAvg(List<Color> colors)
        {
            double r = 0;
            double g = 0;
            double b = 0;

            
            for (int i = 0; i < colors.Count; i++)
            {
                r += colors[i].R;
                g += colors[i].G;
                b += colors[i].B;
            }

            return Color.FromArgb(255, (int) (r / colors.Count), (int) (g / colors.Count), (int) (b / colors.Count));
        }
        private Color ClosestColor(Color c, List<Color> colorList)
        {
            int colorIndex = -1;
            double kleinsteKleurAfstand = int.MaxValue;
            
            for (int i = 0; i < colorList.Count; i++)
            {

                double kleurAfstand = Math.Sqrt(Math.Pow(colorList[i].R - c.R, 2) + Math.Pow(colorList[i].G - c.G, 2) + Math.Pow(colorList[i].B - c.B, 2));
                if (kleinsteKleurAfstand > kleurAfstand)
                {
                    colorIndex = i;
                    kleinsteKleurAfstand = kleurAfstand;
                }
                
            }

            return colorList[colorIndex];
        }


    }


}
