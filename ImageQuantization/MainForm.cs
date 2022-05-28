using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace ImageQuantization
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        RGBPixel[,] ImageMatrix;
        RGBPixel[,] image;
        static Stopwatch timer;

        private void btnOpen_Click(object sender, EventArgs e)
        {
            // clear everything before opening a new image
            Global_Variables.clearOpen();
            
            // time taken
            timer = new Stopwatch();
            timer.Start();


            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                // Open the browsed image and display it
                string OpenedFilePath = openFileDialog1.FileName;
                ImageMatrix = ImageOperations.OpenImage(OpenedFilePath);
                ImageOperations.DisplayImage(ImageMatrix, pictureBox1);
            }
            txtWidth.Text = ImageOperations.GetWidth(ImageMatrix).ToString();
            txtHeight.Text = ImageOperations.GetHeight(ImageMatrix).ToString();
            Distinct_Colors.Text = Global_Variables.distinctColors.Count.ToString();

            
            //uncomment both lines to activate bonus 1
            //MST_Graph.MST_Construction(); //O(N^2)
            //K_Number.Text = KCluster.getK().ToString(); //O(N)

        }



        private void btnGaussSmooth_Click(object sender, EventArgs e)
        {
            // K must be entered if not calculated automatically
            if (int.Parse(K_Number.Text) > Global_Variables.distinctColors.Count)
            {
                MessageBox.Show("Please enter K between [0 : " + Global_Variables.distinctColors.Count + " ]");
                K_Number.Text = " ";
            }
            else
            {

                Global_Variables.K = int.Parse(K_Number.Text);

                // comment to activate bonus 1
                MST_Graph.MST_Construction(); //O(N^2)

                if (Global_Variables.K != 0)
                {  
                    //comment to activate bonus 2
                    KCluster.createCluster(Global_Variables.K); //O(N^2)

                    // uncomment to activate bonus 2
                    //KCluster.createKClusters(Global_Variables.K);
                    //Global_Variables.distinctColors = Global_Variables.dupDistinctColors;
                }

                image = Image_Quantization.ImageQuantization(ImageMatrix); //O(N^2)
                MST_Sum.Text = Global_Variables.MST_sum.ToString(); //O(1)

                timer.Stop(); //O(1)
                TimeSpan timeTaken = timer.Elapsed; //O(1)
                time_taken.Text = timeTaken.ToString(@"m\:ss\.fff"); //O(1)

                ImageOperations.DisplayImage(image, pictureBox2); //O(N^2)

                // clear everything before opening a new image
                Global_Variables.clearQuantized(); //O(1)

            }


        }


    }
}