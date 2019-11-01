using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AplProject
{
    public partial class Form1 : Form
    {
        private Bitmap imgage;
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
                imgage = new Bitmap(img, new Size(pictureBox1.Width, pictureBox1.Height));

                pictureBox1.Image = imgage;
                
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
                    imgage.Save(saveFileDialog1.FileName, System.Drawing.Imaging.ImageFormat.Jpeg);
                    break;

                case 2:
                    imgage.Save(saveFileDialog1.FileName, System.Drawing.Imaging.ImageFormat.Bmp);
                    break;

                case 3:
                    imgage.Save(saveFileDialog1.FileName, System.Drawing.Imaging.ImageFormat.Gif);
                    break;
            }
        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Convertor c = new Convertor(imgage);
            this.pictureBox2.Image = c.convert(256, Algorythm.MedianCut);

        }
    }
}
