
using Project1.logica;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Project1
{
    
    public partial class Form1 : Form
    {
        private Bitmap image;
        private MedianCut c;
        private Kmeans k;
        public Form1()
        {
            InitializeComponent();
            this.comboBox1.SelectedIndex = 0;
            this.label5.Text = "" + 1;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Load selected file
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
            //Zorgen dat er geen lege file namen worden aanvaard
            saveFileDialog1.ShowDialog();
            if (saveFileDialog1.FileName == null)
            {
                return;
            }
            Image img = pictureBox2.Image;
            switch (saveFileDialog1.FilterIndex)
            {
                case 1:
                    //jpg
                    img.Save(saveFileDialog1.FileName, System.Drawing.Imaging.ImageFormat.Jpeg);
                    break;

                case 2:
                    //bmp
                    img.Save(saveFileDialog1.FileName, System.Drawing.Imaging.ImageFormat.Bmp);
                    break;

                case 3:
                    //gif
                    img.Save(saveFileDialog1.FileName, System.Drawing.Imaging.ImageFormat.Gif);
                    break;
            }
        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image == null)
            {
                return;
            }
            image = new Bitmap(pictureBox1.Image, new Size(pictureBox1.Width, pictureBox1.Height));


            int colorSize = GetSelectedSize();

            progressBar2.Value = 1;
            progressBar2.Minimum = 1;
            progressBar2.Maximum = pictureBox1.Width * pictureBox1.Height;
            progressBar2.Step = 1;

            var result = (image, -1.0);
            if (comboBox1.SelectedItem.ToString().Equals(Algorythm.MedianCut.ToString()))
            {
                //median cut
                label4.Text = "Creating color palette using median cut method";
                c = new MedianCut(image);
                
                result = c.Convert(colorSize, checkBox1.Checked , progressBar2, label4);
                Bitmap b2 = result.Item1;
                this.pictureBox3.Image = c.CreatePaletteMap(pictureBox3.Width, pictureBox3.Height, 1);
            }
            else if (comboBox1.SelectedItem.ToString().Equals(Algorythm.Kmeans.ToString()))
            {
                //kmeans
                k = new Kmeans(image);
                result = k.Convert(progressBar2, checkBox1.Checked, colorSize, trackBar1.Value  , label4);
                this.pictureBox3.Image = k.CreatePalletMap(pictureBox3.Width, pictureBox3.Height, 1);
            }

            if (result.Item2 != -1.0)
            {
                label1.Visible = true;
                label1.Text = "" + Math.Round(result.Item2, 5);
                this.pictureBox2.Image = result.Item1;
                this.label5.Visible = true;
            }





        }

        private void button4_Click(object sender, EventArgs e)
        {
            int page = int.Parse(this.label5.Text);
            this.label5.Text = "" + (page + 1);

            if ((GetSelectedSize() / 32) < (page + 1))
            {
                this.label5.Text = "" + 1;
            }

            if (comboBox1.SelectedItem.ToString().Equals(Algorythm.MedianCut.ToString()))
            {
                //color pal
                this.pictureBox3.Image = c.CreatePaletteMap(pictureBox3.Width, pictureBox3.Height, ((int.Parse(this.label5.Text))));
            }
            else if (comboBox1.SelectedItem.ToString().Equals(Algorythm.Kmeans.ToString()))
            {
                //color pal
                this.pictureBox3.Image = k.CreatePalletMap(pictureBox3.Width, pictureBox3.Height, ((int.Parse(this.label5.Text))));
            }
        }

        private int GetSelectedSize()
        {
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
            return colorSize;
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem.ToString().Equals(Algorythm.Kmeans.ToString()))
            {
                trackBar1.Visible = true;
            }
            else
            {
                trackBar1.Visible = false;
            }
        }

        private void progressBar2_Click(object sender, EventArgs e)
        {

        }
    }
}
