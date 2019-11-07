using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Project1.logica
{
    class Kmeans
    {
        /*
         * (aan passen hoe K wordt gekozen)
         * Verander zodat de punten een min afstaand van de anderen moeten hebben
         */
        Bitmap bitmap;
        List<Color> pallet;
        public Kmeans(Bitmap bitmap)
        {
            this.bitmap = bitmap;
        }

        public (Bitmap, double) Convert(ProgressBar progressAfbeelding, bool dithering, int palletSize, int k, Label l)
        {

            List<Color> colors = GetPixels(bitmap);

            pallet = CreatePallet(colors, palletSize, k, progressAfbeelding);
            return Convertor.ReplaceToClosest(bitmap, pallet, dithering, progressAfbeelding);
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

        private List<Color> CreatePallet(List<Color> colors, int palletSize, int k, ProgressBar p)
        {

            List<Color> bestePallet = new List<Color>();
            double besteAfstand = double.MaxValue;
            for (int i = 0; i < k; i++)
            {
                // random clusters maken
                List<Color> ks = getKs(colors, palletSize);
                List<Color> prevClusterMeans = ks;
                //kleuren in een cluster steken en dan gemiddelde berekenen
                List<Color> clusterMeans = CreateClusters(ks, colors, p);
                p.Maximum = 1;
                p.Value = 1;
                //dit herhalen tot het overeen komt
                do
                {
                    prevClusterMeans = clusterMeans;
                    clusterMeans = CreateClusters(prevClusterMeans, colors, p);
                } while (clusterMeans.Equals(prevClusterMeans));

                Dictionary<Color, Color> clusterDictionary = new Dictionary<Color, Color>();
                for (int j = 0; j < clusterMeans.Count; j++)
                {
                    clusterDictionary.Add(clusterMeans[j], clusterMeans[j]);
                }
                double afstand = berekenGemAfstand(colors, clusterDictionary);
                if (afstand < besteAfstand)
                {
                    besteAfstand = afstand;
                    bestePallet = clusterMeans;
                }

                p.Maximum = 1;
                p.Value = 1;
            }


            return bestePallet;


        }

        private List<Color> CreateClusters(List<Color> ks, List<Color> colors, ProgressBar p)
        {

            Dictionary<Color, List<Color>> cluster = new Dictionary<Color, List<Color>>();
            for (int i = 0; i < ks.Count; i++)
            {
                try
                {
                    cluster.Add(ks[i], new List<Color>());
                }
                catch (Exception)
                {
                }

            }
            p.Maximum = colors.Count();
            p.Value = 1;

            //Calc closest point
            for (int i = 0; i < colors.Count; i++)
            {
                Color closestDataPoint = ClosestColor(colors[i], ks);
                cluster[closestDataPoint].Add(colors[i]);
                p.PerformStep();
            }
            List<Color> clusterMeans = new List<Color>();
            List<Color> keys = cluster.Keys.ToList();

            //means of cluster
            for (int i = 0; i < cluster.Count; i++)
            {
                clusterMeans.Add(ClusterToAvg(cluster[keys[i]]));
            }
            return clusterMeans;
        }

        private double berekenGemAfstand(List<Color> originalPixels, Dictionary<Color, Color> pallet)
        {
            List<Color> palletList = pallet.Keys.ToList();
            double totaalAfstand = 0;
            for (int i = 0; i < originalPixels.Count; i++)
            {
                Color closestDataPoint = ClosestColor(originalPixels[i], palletList);
                Color palletColor = pallet[closestDataPoint];
                totaalAfstand += Math.Sqrt(Math.Pow(originalPixels[i].R - palletColor.R, 2) + Math.Pow(originalPixels[i].G - palletColor.G, 2) + Math.Pow(originalPixels[i].B - palletColor.B, 2));
            }

            return totaalAfstand / originalPixels.Count;


        }

        private List<Color> getKs(List<Color> colors, int k)
        {
            Random r = new Random();
            List<Color> ks = new List<Color>();
            Dictionary<Color, bool> test = new Dictionary<Color, bool>();

            for (int i = 0; i < colors.Count; i++)
            {
                if (!test.ContainsKey(colors[i]))
                {
                    test.Add(colors[i], true);
                }

            }

            colors = test.Keys.ToList();

            do
            {
                int random = r.Next(0, colors.Count);

                ks.Add(colors[random]);
            } while ((ks.Count < k));
            return ks;

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

            return Color.FromArgb(255, (int)(r / colors.Count), (int)(g / colors.Count), (int)(b / colors.Count));
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
