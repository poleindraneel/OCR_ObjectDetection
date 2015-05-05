using AForge.Imaging;
using AForge.Imaging.Filters;
using AForge.Math;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace ObjectDetection
{
	public partial class Form1 : Form
	{
		
		dtctObject detectObject;
		Bitmap srcImage = null;
		PictureBox pictBox = null;
		public Form1()
		{
			InitializeComponent();
		}

		private void loadBttn_Click(object sender, EventArgs e)
		{
			this.Controls.Remove(pictBox);
			OpenFileDialog dlg = new OpenFileDialog();

			dlg.Title = "Open image";
			dlg.Filter = "bmp files (*.bmp)|*.bmp";

			if (dlg.ShowDialog() == DialogResult.OK)
			{
				srcImage = new Bitmap(dlg.FileName);
			}
		}


		private void objDtct_Click(object sender, EventArgs e)
		{
			if (srcImage != null)
			{
				Bitmap objImage = null;
				BackgroundExtractor bckgrndExctrct = new BackgroundExtractor();
				//detectObject = new dtctObject();
				//objImage = objectDetect.binary(srcImage);
				//objImage = detectObject.detectObj1(srcImage);
				objImage = bckgrndExctrct.removeBackground(srcImage);
				pictBox = new PictureBox();
				pictBox.Size = new Size(1600, 1600);
				pictBox.Image = objImage;
				this.Controls.Add(pictBox);
			}
		}
	}
}
