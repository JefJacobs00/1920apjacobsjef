
using Project1.logica;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project1
{
    /*
     * Toevoegen gemiddelde Euclidische kleurafstand
     * kleurpallet naast elkaar
     * kleur histogram
     * interactieve instellingen
     */
    public partial class Form1 : Form
    {
        private Bitmap image;
        public Form1()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                string file = openFileDialog1.FileName;
                Image img = Image.FromFile(file);
                image = new Bitmap(img, new Size(pictureBox1.Width, pictureBox1.Height));

                pictureBox1.Image = image;
                
            }


        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //TODO toevoegen van beveiling
            saveFileDialog1.ShowDialog();
            
            switch (saveFileDialog1.FilterIndex)
            {
                case 1:
                    image.Save(saveFileDialog1.FileName, System.Drawing.Imaging.ImageFormat.Jpeg);
                    break;

                case 2:
                    image.Save(saveFileDialog1.FileName, System.Drawing.Imaging.ImageFormat.Bmp);
                    break;

                case 3:
                    image.Save(saveFileDialog1.FileName, System.Drawing.Imaging.ImageFormat.Gif);
                    break;
            }
        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Convertor c = new Convertor(image);
            Dictionary<double, Bitmap> f = c.convert(256, Algorythm.MedianCut);
            Bitmap b2 = f.Values.First();
            label1.Text = "" + f.Keys.First();
            this.pictureBox2.Image = b2;
            
            label1.Text += ""; 

        }
    }
}
