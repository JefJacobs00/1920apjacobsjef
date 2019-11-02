
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
            image = new Bitmap(pictureBox1.Image, new Size(pictureBox1.Width, pictureBox1.Height));
            Convertor c = new Convertor(image);
            int colorSize = 256;
            if (R16.Checked)
            {
                colorSize = 16;
            }
            else if (R32.Checked)
            {
                colorSize = 32;
            }
            else if (R64.Checked)
            {
                colorSize = 64;
            }
            else if (R128.Checked)
            {
                colorSize = 128;
            }
            else if (R256.Checked)
            {
                colorSize = 256;
            }
            progressBar1.Value = 1;
            progressBar1.Minimum = 1;
            progressBar1.Maximum = pictureBox1.Width * pictureBox1.Height;
            progressBar1.Step = 1;
            
            if (comboBox1.SelectedItem.ToString().Equals(Algorythm.MedianCut.ToString()))
            {
                Dictionary<double, Bitmap> f = c.convert(colorSize, Algorythm.MedianCut, progressBar1);
                Bitmap b2 = f.Values.First();
                label1.Visible = true;
                label1.Text = "" + Math.Round(f.Keys.First(), 5);
                this.pictureBox2.Image = b2;
            }
            
            

        }

    }
}
