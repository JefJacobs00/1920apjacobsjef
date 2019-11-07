using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Project1.logica
{
    class MedianCut
    {
        private Bitmap bmp;
        private List<Color> pallet;
        public MedianCut(Bitmap bitmap)
        {
            bmp = bitmap;
        }

        public (Bitmap, double) Convert(int palletSize, bool dithering , ProgressBar p,Label l)
        {
            this.pallet = CreatePallet(bmp, palletSize,p);
            
            return Convertor.ReplaceToClosest(bmp, pallet, dithering , p);
        }

        public Bitmap CreatePaletteMap(int x, int y,int page)
        {
            VisualColorPallet visual = new VisualColorPallet(pallet);
            return visual.CreatePalletBitmap(x, y, page);
        }

        private List<Color> CreatePallet(Bitmap bitmap, int PalletSize, ProgressBar p)
        {
            List<Color> pixelList = GetPixelList(bitmap);

            pixelList = orderByGreatestRange(pixelList);

            List<List<Color>> buckets = new List<List<Color>>();
            buckets.Add(pixelList);
            p.Maximum = PalletSize;
            p.Value = 1;
            do
            {
                List<Color> colorGR = (findGreatestRange(buckets));
                var itemsCut = Cut(colorGR);
                buckets.Add(itemsCut.Item1);
                buckets.Add(itemsCut.Item2);
                buckets.Remove(colorGR);
                p.PerformStep();
            } while (buckets.Count < PalletSize);

            List<Color> pallet = new List<Color>();
            for (int i = 0; i < buckets.Count; i++)
            {
                pallet.Add(BerekenGemiddelde(buckets[i]));
            }

            return pallet;
          
        }

        private List<Color> findGreatestRange(List<List<Color>> buckets)
        {
            
            int indexGR = -1;
            int rangeAmount = 0;
            for (int i = 0; i < buckets.Count; i++)
            {
                var range = GetRange(buckets[i], "R");
                int[] rangeR = new int[2] { range.Item1, range.Item2 };
                range = GetRange(buckets[i], "G");
                int[] rangeG = new int[2] { range.Item1, range.Item2 };
                range = GetRange(buckets[i], "B");
                int[] rangeB = new int[2] { range.Item1, range.Item2 };

                if (rangeAmount < (rangeR[0] - rangeR[1]))
                {
                    rangeAmount = rangeR[0] - rangeR[1];
                    indexGR = i;
                }
                if (rangeAmount < (rangeG[0] - rangeG[1]))
                {
                    rangeAmount = rangeG[0] - rangeG[1];
                    indexGR = i;
                }
                if (rangeAmount < (rangeB[0] - rangeB[1]))
                {
                    rangeAmount = rangeB[0] - rangeB[1];
                    indexGR = i;
                }

            }

            if ((indexGR >= 0) && (buckets[indexGR].Count > 2))
            {
                return buckets[indexGR];
            }
            else
            {
                return null;
            }
        }

        private (int,int) GetRange(List<Color> c,string color)
        {
            if (!(c.Count > 2))
            {
                return (0, 0);
            }
            switch(color){
                case "R":
                    int rG = 0;
                    int rL = 256;
                    for (int i = 0; i < c.Count; i++)
                    {
                        if (c[i].R > rG)
                        {
                            rG = c[i].R;
                        }
                        else if(c[i].R < rL)
                        {
                            rL = c[i].R;
                        }
                    }
                    return (rG, rL);
                case "G":
                    int gG = 0;
                    int gL = 256;
                    for (int i = 0; i < c.Count; i++)
                    {
                        if (c[i].G > gG)
                        {
                            gG = c[i].G;
                        }
                        else if (c[i].G < gL)
                        {
                            gL = c[i].G;
                        }
                    }
                    return (gG, gL);
                case "B":
                    int bG = 0;
                    int bL = 256;
                    for (int i = 0; i < c.Count; i++)
                    {
                        if (c[i].B > bG)
                        {
                            bG = c[i].B;
                        }
                        else if (c[i].B < bL)
                        {
                            bL = c[i].B;
                        }
                    }
                    return (bG, bL);
                default:
                    return (0, 0);


            }
            
        }

        private List<Color> orderByGreatestRange(List<Color> pixelList)
        {
            int rGroot = int.MinValue;
            int rKlein = int.MaxValue;

            int gGroot = int.MinValue;
            int gKlein = int.MaxValue;

            int bGroot = int.MinValue;
            int bKlein = int.MaxValue;

            for (int i = 0; i < pixelList.Count; i++)
            {
                if (pixelList[i].R > rGroot)
                {
                    rGroot = pixelList[i].R;
                }
                if (pixelList[i].R < rKlein)
                {
                    rKlein = pixelList[i].R;
                }

                if (pixelList[i].G > gGroot)
                {
                    gGroot = pixelList[i].G;
                }
                if (pixelList[i].G < gKlein)
                {
                    gKlein = pixelList[i].G;
                }

                if (pixelList[i].B > bGroot)
                {
                    bGroot = pixelList[i].B;
                }
                if (pixelList[i].B < bKlein)
                {
                    bKlein = pixelList[i].B;
                }
            }

            if (((rGroot - rKlein) > (bGroot - bKlein)) && ((rGroot - rKlein) > (gGroot - gKlein)))
            {
                pixelList.Sort(delegate (System.Drawing.Color left, System.Drawing.Color right)
                {
                    return right.R.CompareTo(left.R);
                });
            }
            else if (((rGroot - rKlein) < (gGroot - gKlein)) && ((gGroot - gKlein) > (bGroot - bKlein)))
            {
                pixelList.Sort(delegate (System.Drawing.Color left, System.Drawing.Color right)
                {
                    return right.G.CompareTo(left.G);
                });
            }
            else
            {
                pixelList.Sort(delegate (System.Drawing.Color left, System.Drawing.Color right)
                {
                    return right.B.CompareTo(left.B);
                });
            }
            return pixelList;
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

        private (List<Color>, List<Color>) Cut(List<Color> c) 
        {
            c = orderByGreatestRange(c);

            List<Color> list1 = new List<Color>();
            List<Color> list2 = new List<Color>();
            for (int i = 0; i < c.Count; i++)
            {
                if (i < (c.Count / 2))
                {
                    list1.Add(c[i]);
                }
                else if (i > (c.Count / 2))
                {
                    list2.Add(c[i]);
                }
            }

            return (list1, list2);
        }

        private Color BerekenGemiddelde(List<Color> c)
        {
            double r = 0;
            double g = 0;
            double b = 0;

            for (int i = 0; i < c.Count; i++)
            {
                r += c[i].R;
                g += c[i].G;
                b += c[i].B;

            }
            return Color.FromArgb(255, (int)(r / c.Count), (int)(g / c.Count), (int)(b / c.Count));
        }


    }
}
