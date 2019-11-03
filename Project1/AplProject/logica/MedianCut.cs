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

        public (Bitmap, double) convert(int palletSize, ProgressBar p)
        {
            this.pallet = CreatePallet(bmp, palletSize);
            return Convertor.ReplaceToClosest(bmp, pallet, p);
        }

        public Bitmap CreatePalletMap(int x, int y,int page)
        {
            VisualColorPallet visual = new VisualColorPallet(pallet);
            return visual.CreatePalletBitmap(x, y, page);
            //Bitmap b = new Bitmap(x, y);
            //if (pallet.Count <= 64)
            //{
            //    for (int i = 0; i < pallet.Count; i++)
            //    {
            //        for (int j = 1; j < y / pallet.Count; j++)
            //        {
            //            for (int k = 0; k < x; k++)
            //            {
            //                if (j + (i * ((y / pallet.Count))) >= y)
            //                {
            //                    break;
            //                }
            //                b.SetPixel(k, j + (i * ((y / pallet.Count))), pallet[i]);
            //            }
            //        }
            //    }
            //}
            //else
            //{
            //    int teller = 0;
            //    for (int i = 0; i < pallet.Count; i++)
            //    {
            //        if ((i % 2) == 0)
            //        {
            //            for (int j = 1; j < (y / (pallet.Count/2)); j++)
            //            {
            //                for (int k = 0; k < x / 2; k++)
            //                {
            //                    if (j + (i / 2 * ((y / (pallet.Count / 2)))) >= y)
            //                    {
            //                        break;
            //                    }
            //                    b.SetPixel(k, j + (i / 2 * ((y / (pallet.Count / 2)))), pallet[i]);
            //                }
            //            }
            //        }
            //        else
            //        {
            //            for (int j = 1; j < y / (pallet.Count / 2); j++)
            //            {
            //                for (int k = x/2+1; k < x ; k++)
            //                {
            //                    if (j + (i/2 * ((y / (pallet.Count / 2)))) >= y)
            //                    {
            //                        break;
            //                    }
            //                    b.SetPixel(k, j + (i/2 * ((y / (pallet.Count / 2)))), pallet[i]);
            //                }
            //            }
                       
            //        }

            //    }
            //}

            //return b;
        }

        private List<Color> CreatePallet(Bitmap bitmap, int PalletSize)
        {
            List<Color> pixelList = GetPixelList(bitmap);
            //Sorting the colors
            //int rGroot = int.MinValue;
            //int rKlein = int.MaxValue;

            //int gGroot = int.MinValue;
            //int gKlein = int.MaxValue;

            //int bGroot = int.MinValue;
            //int bKlein = int.MaxValue;

            //for (int i = 0; i < pixelList.Count; i++)
            //{
            //    if (pixelList[i].R > rGroot)
            //    {
            //        rGroot = pixelList[i].R ;
            //    }
            //    else if(pixelList[i].R < rKlein)
            //    {
            //        rKlein = pixelList[i].R; 
            //    }

            //    if (pixelList[i].G > rGroot)
            //    {
            //        gGroot = pixelList[i].G;
            //    }
            //    else if (pixelList[i].G < rKlein)
            //    {
            //        gKlein = pixelList[i].G;
            //    }

            //    if (pixelList[i].B > rGroot)
            //    {
            //        bGroot = pixelList[i].B;
            //    }
            //    else if (pixelList[i].B < rKlein)
            //    {
            //        bKlein = pixelList[i].B;
            //    }
            //}

            //if (((rGroot - rKlein) > (bGroot - bKlein)) && ((rGroot - rKlein) > (gGroot - gKlein)))
            //{
            //    pixelList.Sort(delegate (System.Drawing.Color left, System.Drawing.Color right)
            //    {
            //        return right.R.CompareTo(left.R);
            //    });
            //}
            //else if (((rGroot - rKlein) < (gGroot - gKlein)) && ((gGroot - gKlein) > (bGroot - bKlein)))
            //{
            //    pixelList.Sort(delegate (System.Drawing.Color left, System.Drawing.Color right)
            //    {
            //        return right.G.CompareTo(left.G);
            //    });
            //}
            //else
            //{
            //    pixelList.Sort(delegate (System.Drawing.Color left, System.Drawing.Color right)
            //    {
            //        return right.B.CompareTo(left.B);
            //    });
            //}

            int r = 0;
            int g = 0;
            int b = 0;

            for (int i = 0; i < pixelList.Count; i++)
            {
                r += pixelList[i].R;
                g += pixelList[i].G;
                b += pixelList[i].B;
            }

            if ((r > b) && (r > g))
            {
                pixelList.Sort(delegate (System.Drawing.Color left, System.Drawing.Color right)
                {
                    return right.R.CompareTo(left.R);
                });
            }
            else if ((g > r) && (g > b))
            {
                pixelList.Sort(delegate (System.Drawing.Color left, System.Drawing.Color right)
                {
                    return right.G.CompareTo(left.G);
                });
            }
            else if ((b > r) && (b > g))
            {
                pixelList.Sort(delegate (System.Drawing.Color left, System.Drawing.Color right)
                {
                    return right.B.CompareTo(left.B);
                });
            }

            List<Color[]> buckets = new List<Color[]>();
            List<Color[]> bucketsHelp = new List<Color[]>();
            buckets.Add(pixelList.ToArray());
            int size = (int)Math.Log(PalletSize, 2);
            for (int i = 0; i < size; i++)
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
                else if (i > (c.Length / 2))
                {
                    arr2[i - (c.Length / 2) - 1] = c[i];
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
                r += c[i].R;
                g += c[i].G;
                b += c[i].B;

            }
            return Color.FromArgb(255, (int)(r / c.Length), (int)(g / c.Length), (int)(b / c.Length));
        }


    }
}
