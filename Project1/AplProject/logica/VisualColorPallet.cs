using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1.logica
{
    class VisualColorPallet
    {

        List<Color> pallet;
        public VisualColorPallet(List<Color> palette)
        {
            pallet = palette;
        }

        public Bitmap CreatePalletBitmap(int x,int y,int page)
        {
            int amountPerPage = 32;
            Bitmap b = new Bitmap(x, y);
            //up to 64 colors get shown on the screen from 128 it gets split
            if (pallet.Count <= 64)
            {
                SetPixels(b, x, y, x, y / pallet.Count, pallet);
            }
            else
            {
                if ((amountPerPage * page)>pallet.Count)
                {
                    throw new ArgumentOutOfRangeException($"Page {page} bestaat niet");
                }
                List<Color> colorsPerPage = new List<Color>();
                //get 32 (ammountPerPage var) depending on the page e.g if page is 1 first 32 colors, page is 2 second 32 colors ,....
                for (int i = (amountPerPage * (page-1)); i <= (amountPerPage * page)-1; i++)
                {
                    colorsPerPage.Add(pallet[i]); 
                }

                SetPixels(b, x, y, x, y / amountPerPage, colorsPerPage);
            }

            return b;
        }

        private Bitmap SetPixels(Bitmap b,int x , int y, int xTimes , int yTimes,List<Color> colors)
        {
            //xTimes the ammount of x pixels that get set
            //yTimes the ammount of y pixels that get set
            //x-y dimentions of the color palette map

            for (int i = 0; i < colors.Count; i++)
            {
                for (int j = 1; j < yTimes; j++)
                {
                    for (int k = 0; k < xTimes; k++)
                    {
                        if (j + (i * yTimes) >= y)
                        {
                            break;
                        }
                        //set de correcte pixel
                        //j + (i * ytimes) calculates what y pixel its on
                        b.SetPixel(k, j + (i * yTimes), colors[i]);
                    }
                }
            }

            return b;
        }
    }
}
