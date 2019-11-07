namespace Project1
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
                
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.button2 = new System.Windows.Forms.Button();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.button3 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.R16 = new System.Windows.Forms.RadioButton();
            this.R32 = new System.Windows.Forms.RadioButton();
            this.R64 = new System.Windows.Forms.RadioButton();
            this.R128 = new System.Windows.Forms.RadioButton();
            this.R256 = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.progressBar2 = new System.Windows.Forms.ProgressBar();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.button4 = new System.Windows.Forms.Button();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(29, 38);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(87, 28);
            this.button1.TabIndex = 0;
            this.button1.Text = "Browse";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog1_FileOk);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Location = new System.Drawing.Point(29, 87);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(460, 331);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(383, 907);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(106, 34);
            this.button2.TabIndex = 2;
            this.button2.Text = "Save";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.Filter = "JPeg Image|*.jpg|Bitmap Image|*.bmp|Gif Image|*.gif";
            this.saveFileDialog1.Title = "Save an Image File";
            // 
            // pictureBox2
            // 
            this.pictureBox2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox2.Location = new System.Drawing.Point(29, 551);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(460, 331);
            this.pictureBox2.TabIndex = 3;
            this.pictureBox2.TabStop = false;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(383, 495);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(106, 34);
            this.button3.TabIndex = 4;
            this.button3.Text = "Calculate";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(689, 87);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "label1";
            this.label1.Visible = false;
            // 
            // R16
            // 
            this.R16.AutoSize = true;
            this.R16.Location = new System.Drawing.Point(31, 512);
            this.R16.Name = "R16";
            this.R16.Size = new System.Drawing.Size(37, 17);
            this.R16.TabIndex = 7;
            this.R16.Text = "16";
            this.R16.UseVisualStyleBackColor = true;
            // 
            // R32
            // 
            this.R32.AutoSize = true;
            this.R32.Location = new System.Drawing.Point(74, 512);
            this.R32.Name = "R32";
            this.R32.Size = new System.Drawing.Size(37, 17);
            this.R32.TabIndex = 8;
            this.R32.Text = "32";
            this.R32.UseVisualStyleBackColor = true;
            // 
            // R64
            // 
            this.R64.AutoSize = true;
            this.R64.Location = new System.Drawing.Point(117, 512);
            this.R64.Name = "R64";
            this.R64.Size = new System.Drawing.Size(37, 17);
            this.R64.TabIndex = 9;
            this.R64.Text = "64";
            this.R64.UseVisualStyleBackColor = true;
            // 
            // R128
            // 
            this.R128.AutoSize = true;
            this.R128.Location = new System.Drawing.Point(160, 512);
            this.R128.Name = "R128";
            this.R128.Size = new System.Drawing.Size(43, 17);
            this.R128.TabIndex = 10;
            this.R128.Text = "128";
            this.R128.UseVisualStyleBackColor = true;
            // 
            // R256
            // 
            this.R256.AutoSize = true;
            this.R256.Checked = true;
            this.R256.Location = new System.Drawing.Point(209, 512);
            this.R256.Name = "R256";
            this.R256.Size = new System.Drawing.Size(43, 17);
            this.R256.TabIndex = 11;
            this.R256.TabStop = true;
            this.R256.Text = "256";
            this.R256.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(533, 87);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(133, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "Gemiddelde kleur afstand: ";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(533, 111);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(104, 13);
            this.label4.TabIndex = 15;
            this.label4.Text = "Convertie afbeelding";
            // 
            // progressBar2
            // 
            this.progressBar2.Location = new System.Drawing.Point(536, 136);
            this.progressBar2.Name = "progressBar2";
            this.progressBar2.Size = new System.Drawing.Size(274, 23);
            this.progressBar2.TabIndex = 16;
            this.progressBar2.Click += new System.EventHandler(this.progressBar2_Click);
            // 
            // pictureBox3
            // 
            this.pictureBox3.Location = new System.Drawing.Point(536, 200);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(274, 768);
            this.pictureBox3.TabIndex = 17;
            this.pictureBox3.TabStop = false;
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            Project1.Algorythm.MedianCut,
            Project1.Algorythm.Kmeans});
            this.comboBox1.Location = new System.Drawing.Point(29, 471);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 21);
            this.comboBox1.TabIndex = 6;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(533, 173);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(32, 13);
            this.label3.TabIndex = 18;
            this.label3.Text = "Page";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(571, 173);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(35, 13);
            this.label5.TabIndex = 19;
            this.label5.Text = "label5";
            this.label5.Visible = false;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(692, 171);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(118, 23);
            this.button4.TabIndex = 20;
            this.button4.Text = "Next page";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(271, 512);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(66, 17);
            this.checkBox1.TabIndex = 21;
            this.checkBox1.Text = "dithering";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // trackBar1
            // 
            this.trackBar1.Location = new System.Drawing.Point(178, 461);
            this.trackBar1.Minimum = 1;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(159, 45);
            this.trackBar1.TabIndex = 22;
            this.trackBar1.TabStop = false;
            this.trackBar1.Value = 1;
            this.trackBar1.Visible = false;
            this.trackBar1.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(922, 982);
            this.Controls.Add(this.trackBar1);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.progressBar2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.R256);
            this.Controls.Add(this.R128);
            this.Controls.Add(this.R64);
            this.Controls.Add(this.R32);
            this.Controls.Add(this.R16);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "   ";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;

        private void IniSaveFileDialog()
        {
            
        }

        private System.Windows.Forms.PictureBox pictureBox2;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.RadioButton R16;
        private System.Windows.Forms.RadioButton R32;
        private System.Windows.Forms.RadioButton R64;
        private System.Windows.Forms.RadioButton R128;
        private System.Windows.Forms.RadioButton R256;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ProgressBar progressBar2;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.TrackBar trackBar1;
    }
}

