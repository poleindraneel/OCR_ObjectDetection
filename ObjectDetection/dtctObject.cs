using AForge.Imaging;
using AForge.Imaging.Filters;
using AForge.Math;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using classification;

namespace ObjectDetection
{
	class dtctObject
	{
		public Bitmap detectObj1(Bitmap image)
		{
			GaussianBlur blurImg = new GaussianBlur(4, 11);
			Bitmap DstImage = blurImg.Apply(image);
			Grayscale grayScale = new Grayscale(0.2125, 0.7154, 0.0721);
			HistogramEqualization histFltr = new HistogramEqualization();
			BradleyLocalThresholding bThreshold = new BradleyLocalThresholding();
			Invert ivt = new Invert();
			BinaryErosion3x3 fltrEros = new BinaryErosion3x3();
			BlobsFiltering blobfltr = new BlobsFiltering();
			BinaryDilatation3x3 fltrDil = new BinaryDilatation3x3();
			DstImage = grayScale.Apply(image);
			histFltr.ApplyInPlace(DstImage);
			bThreshold.ApplyInPlace(DstImage);
			ivt.ApplyInPlace(DstImage);

			//OtsuThreshold oThreshold = new OtsuThreshold();
			//DstImage = oThreshold.Apply(DstImage);

			if (DstImage.PixelFormat != System.Drawing.Imaging.PixelFormat.Format8bppIndexed)
			{
				DstImage = DstImage.Clone(new Rectangle(0, 0, DstImage.Width, DstImage.Height), PixelFormat.Format8bppIndexed);
			}
			DstImage = fltrEros.Apply(DstImage);
			blobfltr.CoupledSizeFiltering = true;
			blobfltr.MinHeight = 10;
			blobfltr.MinWidth = 10;
			DstImage = blobfltr.Apply(DstImage);
			DstImage = fltrDil.Apply(DstImage);
			int black_pixels = CountPixels(DstImage, Color.FromArgb(255, 0, 0, 0));
			int white_pixels = CountPixels(DstImage, Color.FromArgb(255, 255, 255, 255));
			if (black_pixels > white_pixels)
			{
				Invert invrt = new Invert();
				invrt.ApplyInPlace(DstImage);

				black_pixels = CountPixels(DstImage, Color.FromArgb(255, 0, 0, 0));
				white_pixels = CountPixels(DstImage, Color.FromArgb(255, 255, 255, 255));
			}
			Median noiseFltr = new Median();
			noiseFltr.ApplyInPlace(DstImage);

			//Blob counting
			BlobCounterBase blbCtr = new BlobCounter();
			blbCtr.FilterBlobs = true;
			blbCtr.MinHeight = 5;
			blbCtr.MinWidth = 5;
			blbCtr.ProcessImage(DstImage);

			Blob[] blobNmr = blbCtr.GetObjects(DstImage, false);
			foreach (var blob in blobNmr)
			{
				//
			}
			var rect = from blob in blobNmr select blob.Rectangle;

			return DstImage;
		}


		public Bitmap detectObj2(Bitmap image)
		{
			Grayscale grayScale = new Grayscale(0.2125, 0.7154, 0.0721);
			HistogramEqualization histFltr = new HistogramEqualization();
			BradleyLocalThresholding bThreshold = new BradleyLocalThresholding();
			Invert ivt = new Invert();
			BinaryErosion3x3 fltrEros = new BinaryErosion3x3();
			BlobsFiltering blobfltr = new BlobsFiltering();
			BinaryDilatation3x3 fltrDil = new BinaryDilatation3x3();
			image = grayScale.Apply(image);
			Bitmap DstImage = bThreshold.Apply(image);
			DstImage = fltrEros.Apply(DstImage);
			DstImage = fltrEros.Apply(DstImage);
			blobfltr.CoupledSizeFiltering = true;
			blobfltr.MinHeight = 0;
			blobfltr.MinWidth = 0;
			DstImage = blobfltr.Apply(DstImage);
			DstImage = fltrEros.Apply(DstImage);
			DstImage = fltrDil.Apply(DstImage);
			DstImage = fltrDil.Apply(DstImage);
			DstImage = blobfltr.Apply(DstImage);
			Invert inverter = new Invert();
			inverter.ApplyInPlace(DstImage);
			int black_pixels = CountPixels(DstImage, Color.FromArgb(255, 0, 0, 0));
			int white_pixels = CountPixels(DstImage, Color.FromArgb(255, 255, 255, 255));
			if (black_pixels > white_pixels)
			{
				Invert invrt = new Invert();
				invrt.ApplyInPlace(DstImage);
				black_pixels = CountPixels(DstImage, Color.FromArgb(255, 0, 0, 0));
				white_pixels = CountPixels(DstImage, Color.FromArgb(255, 255, 255, 255));
			}
			return DstImage;
		}

		public Bitmap detectObj3(Bitmap image)
		{
			Grayscale grey = new Grayscale(0.2125, 0.7154, 0.0721);
			Bitmap DstImage = grey.Apply(image);
			Threshold thr = new Threshold(40);
			Opening fill = new Opening();
			DstImage = thr.Apply(DstImage);
			Erosion eros = new Erosion();
			eros.ApplyInPlace(DstImage);
			eros.ApplyInPlace(DstImage);
			eros.ApplyInPlace(DstImage);
			int black_pixels = CountPixels(DstImage, Color.FromArgb(255, 0, 0, 0));
			int white_pixels = CountPixels(DstImage, Color.FromArgb(255, 255, 255, 255));
			if (black_pixels > white_pixels)
			{
				Invert invrt = new Invert();
				invrt.ApplyInPlace(DstImage);
				black_pixels = CountPixels(DstImage, Color.FromArgb(255, 0, 0, 0));
				white_pixels = CountPixels(DstImage, Color.FromArgb(255, 255, 255, 255));
			}
			return DstImage;
		}

		public Bitmap detectObj4(Bitmap image)
		{
			Bitmap objImage = null;
			//Grayscale
			Grayscale _grayscale = new Grayscale(0.2125, 0.7154, 0.0721);
			//ObjectDetection
			OtsuThreshold oThreshold = new OtsuThreshold();
			Threshold _threshold = new Threshold(40);
			DifferenceEdgeDetector _differeceEdgeDetector = new DifferenceEdgeDetector();

			if (image.PixelFormat != System.Drawing.Imaging.PixelFormat.Format24bppRgb)
			{
				image = image.Clone(new Rectangle(0, 0, image.Width, image.Height), PixelFormat.Format24bppRgb);
			}
			objImage = _grayscale.Apply(image);
			HorizontalIntensityStatistics his = new HorizontalIntensityStatistics(objImage);
			Histogram histogram = his.Gray;
			int[] gray = histogram.Values;
			int max = histogram.Max;
			int min = histogram.Min;
			objImage = oThreshold.Apply(objImage);
			//objImage = _differeceEdgeDetector.Apply(objImage);
			Invert inFilter = new Invert();
			//objImage = inFilter.Apply(objImage);
			if (objImage.PixelFormat != System.Drawing.Imaging.PixelFormat.Format24bppRgb)
			{
				objImage = objImage.Clone(new Rectangle(0, 0, objImage.Width, objImage.Height), PixelFormat.Format24bppRgb);
			}
			ImageStatisticsHSL hslStat = new ImageStatisticsHSL(image);
			HistogramEqualization histo = new HistogramEqualization();
			objImage = histo.Apply(objImage);
			int[] luminanceValues = hslStat.Luminance.Values;
			ImageStatistics rgbStat = new ImageStatistics(image);
			int[] redValues = rgbStat.Red.Values;
			int[] greenValues = rgbStat.Green.Values;
			int[] blueValues = rgbStat.Blue.Values;
			return objImage;
		}

		public Bitmap getBiggestBlob(Bitmap imageWithBlob)
		{
			Invert invt = new Invert();
			invt.ApplyInPlace(imageWithBlob);

			ExtractBiggestBlob bigBlob = new ExtractBiggestBlob();
			Bitmap imageOut = bigBlob.Apply(imageWithBlob);
			int black_pixels = CountPixels(imageOut, Color.FromArgb(255, 0, 0, 0));
			int white_pixels = CountPixels(imageOut, Color.FromArgb(255, 255, 255, 255));
			if (black_pixels > white_pixels)
			{
				invt.ApplyInPlace(imageOut);
			}
			return imageOut;
		}
		public Bitmap blobDetector(Bitmap imageWithBlob)
		{
			Invert inv = new Invert();
			Bitmap blobInverted = inv.Apply(imageWithBlob);

			BlobCounter blbCounter = new BlobCounter(blobInverted);
			blbCounter.ProcessImage(blobInverted);
			Blob compareBlob = null;
			int nr = 0;
			Blob[] blobs = blbCounter.GetObjects(blobInverted, true);
			/*foreach (var blob in blobs)
			{
				if (nr == 0)
				{
					compareBlob = blob;
					nr = 1;
				}
				else
				{
					if (blob.Rectangle.Height > compareBlob.Rectangle.Height)
					{
						compareBlob = blob;
					}
				}
			}*/
			List<Blob> charBlobs = new List<Blob>();
			foreach (var blob in blobs)
			{
				if (blob.Rectangle.Height < 70 && blob.Rectangle.Height >= 50 && blob.Rectangle.Width<60 && blob.Rectangle.Width>=0)
				{
					charBlobs.Add(blob);
				}
			}
			if (charBlobs != null)
			{
				foreach (var blob in charBlobs)
				{
					if (nr == 0)
					{
						compareBlob = blob;
						nr = 1;
					}
					else
					{
						//if (blob.Rectangle.Height > compareBlob.Rectangle.Height && blob.Rectangle.Width > compareBlob.Rectangle.Width)
						//if(blob.Area>compareBlob.Area)
						if (blob.Rectangle.Height > compareBlob.Rectangle.Height && blob.Rectangle.Width > compareBlob.Rectangle.Width)
						{
							compareBlob = blob;
						}
					}
				}
			}
			if (compareBlob != null)
			{
				ExtractBiggestBlob bigBlob = new ExtractBiggestBlob();
				Bitmap processedImage = bigBlob.Apply(compareBlob.Image.ToManagedImage());
				inv.ApplyInPlace(processedImage);
				return processedImage;
			}
			else
			{
				return imageWithBlob;
			}
		}

		public bool comapreImage(Bitmap image)
		{
			Featurevector fVect1 = new Featurevector(2);
			fVect1.addfeature(0, 165.67);
			fVect1.addfeature(1, 34.25);

			Featurevector fVect2 = new Featurevector(2);
			fVect2.addfeature(0, 166.41);
			fVect2.addfeature(1, 32.71);

			Featurevector fVect3 = new Featurevector(2);
			fVect3.addfeature(0, 111.24);
			fVect3.addfeature(1, 44.55);

			Featurevector fVect4 = new Featurevector(2);
			fVect4.addfeature(0, 114.74);
			fVect4.addfeature(1, 46.34);

			Featurevector fVect5 = new Featurevector(2);
			fVect5.addfeature(0, 173.75);
			fVect5.addfeature(1, 37.15);

			Featurevector fVect6 = new Featurevector(2);
			fVect6.addfeature(0, 199.62);
			fVect6.addfeature(1, 33.9);

			Featurevector fVect7 = new Featurevector(2);
			fVect7.addfeature(0, 138.14);
			fVect7.addfeature(1, 43);

			Featurevector fVect8 = new Featurevector(2);
			fVect8.addfeature(0, 218.8);
			fVect8.addfeature(1, 13.97);

			Featurevector fVect9 = new Featurevector(2);
			fVect9.addfeature(0, 148.38);
			fVect9.addfeature(1, 30.41);

			Featurevector fVect10 = new Featurevector(2);
			fVect10.addfeature(0, 205.28);
			fVect10.addfeature(1, 39.09);

			Featurevector fVect11 = new Featurevector(2);
			fVect11.addfeature(0, 205.77);
			fVect11.addfeature(1, 37.49);

			Featurevector fVectImage = new Featurevector(2);
			Histogram hist = getHistogram(image);
			fVectImage.addfeature(0, hist.Mean);
			fVectImage.addfeature(1, hist.StdDev);

			double[] distArray = new double[11];
			distArray[0] = fVectImage.getDistance(fVect1);
			distArray[1] = fVectImage.getDistance(fVect2);
			distArray[2] = fVectImage.getDistance(fVect3);
			distArray[3] = fVectImage.getDistance(fVect4);
			distArray[4] = fVectImage.getDistance(fVect5);
			distArray[5] = fVectImage.getDistance(fVect6);
			distArray[6] = fVectImage.getDistance(fVect7);
			distArray[7] = fVectImage.getDistance(fVect8);
			distArray[8] = fVectImage.getDistance(fVect9);
			distArray[9] = fVectImage.getDistance(fVect10);
			distArray[10] = fVectImage.getDistance(fVect11);

			int indexofDist = Array.IndexOf(distArray, distArray.Min());
			if(indexofDist<4)
			{
				return true;
			}
			return false;
		}
		private Histogram getHistogram(Bitmap image)
		{
			int[] hist = new int[256];
			for (int x = 0; x < image.Width; x++)
			{
				for (int y = 0; y < image.Height; y++)
				{
					var rColor = image.GetPixel(x, y).R;
					var gColor = image.GetPixel(x, y).G;
					var bColor = image.GetPixel(x, y).B;
					int intensity = ((rColor + gColor + bColor) / 3);
					hist[intensity]++;
				}
			}
			Histogram histogram = new Histogram(hist);
			var mean = histogram.Mean;
			return histogram;
		}
		private int CountPixels(Bitmap bm, Color target_color)
		{
			// Loop through the pixels.
			int matches = 0;
			for (int y = 0; y < bm.Height; y++)
			{
				for (int x = 0; x < bm.Width; x++)
				{
					if (bm.GetPixel(x, y) == target_color) matches++;
				}
			}
			return matches;
		}
	}
}
