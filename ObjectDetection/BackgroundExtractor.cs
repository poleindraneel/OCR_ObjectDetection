using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AForge.Imaging.Filters;


namespace ObjectDetection
{
	class BackgroundExtractor
	{
		public Bitmap resultmethod1 = null;
		public Bitmap resultmethod2 = null;
		public Bitmap resultmethod3 = null;
		public Bitmap resultmethod4 = null;
		public Bitmap resultmethod5 = null;

		public Bitmap removeBackground(Bitmap bitmap)
		{



			Bitmap processedBitmap = null;


			//ImageProcessor imageProcessor = new ImageProcessor();
			int count = 0;
			bool whiledone = false;



			while (true)
			{
				processedBitmap = (Bitmap)bitmap.Clone();
				dtctObject dtc = new dtctObject();
				if (dtc.comapreImage(processedBitmap))
				{
					Console.WriteLine("entered if");
					processedBitmap = method5(processedBitmap);
					whiledone = true;
				}
				else
				{
					Console.WriteLine("Entered else");
					switch (count)
					{
						case 0: processedBitmap = method2(processedBitmap);
							resultmethod1 = processedBitmap;
							break;
						case 1: processedBitmap = method3(processedBitmap);
							resultmethod2 = processedBitmap;
							break;
						case 2: processedBitmap = method5(processedBitmap);
							resultmethod3 = processedBitmap;
							break;
						case 3: processedBitmap = method4(processedBitmap);
							resultmethod4 = processedBitmap;
							break;
						case 4: processedBitmap = method1(processedBitmap);
							break;
						case 5: processedBitmap = method6(processedBitmap);
							resultmethod5 = processedBitmap;
							break;
						default: processedBitmap = processedBitmap;

							whiledone = true;
							break;
					}
				}
				int j = processedBitmap.Width;
				count++;
				if (processedBitmap.Height < 70 && processedBitmap.Height >= 50)
				{
					break;
				}
				if (whiledone) break;
			}
			return processedBitmap;
			//return resultmethod2;
		}



		// stony and gras background
		private Bitmap method1(Bitmap processedBitmap)
		{
			//ImageProcessor imageProcessor = new ImageProcessor();
			//processedBitmap = imageProcessor.grayscale(processedBitmap);
			//processedBitmap = imageProcessor.differenceEdgedetection(processedBitmap);
			//processedBitmap = imageProcessor.crop(processedBitmap);
			//processedBitmap = imageProcessor.simplethreshold(processedBitmap, 20);
			//processedBitmap = imageProcessor.openingFilter(processedBitmap);
			//processedBitmap = imageProcessor.findbiggestBlob(processedBitmap);
			//Console.WriteLine(processedBitmap.Width + "x"+ processedBitmap.Height);
			//Bitmap returnbitmap = processedBitmap;

			//processedBitmap = imageProcessor.grayscale(processedBitmap);
			//processedBitmap = imageProcessor.adaptivethreshold(processedBitmap);
			//processedBitmap = imageProcessor.findbiggestBlob(processedBitmap);
			//processedBitmap.Save(@"C:\Users\Kersten\Desktop\out\output.bmp");
			dtctObject obj = new dtctObject();
			Grayscale gray = new Grayscale(0.2125, 0.7154, 0.0721);
			OtsuThreshold oThre = new OtsuThreshold();
			processedBitmap = gray.Apply(processedBitmap);
			oThre.ApplyInPlace(processedBitmap);
			Bitmap processedImage = obj.getBiggestBlob(processedBitmap);
			processedImage.Save(@"E:\Study\Third Semester\AutomationLab\Image Data\ImageOutput\outnoblob0_" + DateTime.Now.Second.ToString() + ".bmp");
			return processedImage;
		}

		private Bitmap method2(Bitmap processedBitmap)
		{
			//ImageProcessor imageProcessor = new ImageProcessor();
			//processedBitmap = imageProcessor.grayscale(processedBitmap);
			//processedBitmap = imageProcessor.simplethreshold(processedBitmap, 40);
			//processedBitmap = imageProcessor.openingFilter(processedBitmap);
			//processedBitmap = imageProcessor.findbiggestBlob(processedBitmap);


			dtctObject objectdetecter = new dtctObject();
			processedBitmap = objectdetecter.detectObj1(processedBitmap);
			processedBitmap.Save(@"E:\Study\Third Semester\AutomationLab\Image Data\ImageOutput\outnoblob1_" + DateTime.Now.Second.ToString() + ".bmp");
			Bitmap processedImage = objectdetecter.blobDetector(processedBitmap);
			//processedImage.Save(@"E:\Study\Third Semester\AutomationLab\Image Data\ImageOutput\outnoblob1_" + DateTime.Now.Millisecond.ToString() + ".bmp");
			return processedImage;
		}

		// solidblue
		private Bitmap method3(Bitmap processedBitmap)
		{
			dtctObject objectdetecter = new dtctObject();
			processedBitmap = objectdetecter.detectObj2(processedBitmap);
			//processedBitmap.Save(@"E:\Study\Third Semester\AutomationLab\Image Data\ImageOutput\outnoblob2_" + DateTime.Now.Second.ToString() + ".bmp");
			Bitmap processedImage = objectdetecter.blobDetector(processedBitmap);
			processedImage.Save(@"E:\Study\Third Semester\AutomationLab\Image Data\ImageOutput\outnoblob2_" + DateTime.Now.Millisecond.ToString() + ".bmp");
			return processedImage;
		}

		// nobigdifference
		private Bitmap method4(Bitmap processedBitmap)
		{
			// ImageProcessor imageProcessor = new ImageProcessor();

			//// processedBitmap = (Bitmap)bitmap.Clone();
			// processedBitmap = imageProcessor.grayscale(processedBitmap);
			// processedBitmap = imageProcessor.differenceEdgedetection(processedBitmap);
			// processedBitmap = imageProcessor.crop(processedBitmap);
			// processedBitmap = imageProcessor.simplethreshold(processedBitmap,10);
			// processedBitmap = imageProcessor.invertFilter(processedBitmap);
			// processedBitmap = imageProcessor.findbiggestBlob(processedBitmap);
			dtctObject objectdetecter = new dtctObject();
			processedBitmap = objectdetecter.detectObj3(processedBitmap);
			//processedBitmap.Save(@"E:\Study\Third Semester\AutomationLab\Image Data\ImageOutput\outnoblob3_" + DateTime.Now.Second.ToString() + ".bmp");
			Bitmap processedImage = objectdetecter.blobDetector(processedBitmap);
			processedImage.Save(@"E:\Study\Third Semester\AutomationLab\Image Data\ImageOutput\outnoblob3_" + DateTime.Now.Millisecond.ToString() + ".bmp");
			return processedImage;
		}


		private Bitmap method5(Bitmap processedBitmap)
		{
			ObjExtractorNee objExt = new ObjExtractorNee();
			Bitmap processedImage = objExt.extractor(processedBitmap);
			processedImage.Save(@"E:\Study\Third Semester\AutomationLab\Image Data\ImageOutput\outnoblob4_" + DateTime.Now.Millisecond.ToString() + ".bmp");
			return processedImage;
		}
		//rectangles
		private Bitmap method6(Bitmap processedBitmap)
		{
			//ImageProcessor imageProcessor = new ImageProcessor();

			////processedBitmap = (Bitmap)bitmap.Clone();
			//processedBitmap = imageProcessor.grayscale(processedBitmap);
			//processedBitmap = imageProcessor.simplethreshold(processedBitmap, 20);
			//processedBitmap = imageProcessor.erosionFilter(processedBitmap);
			//processedBitmap = imageProcessor.openingFilter(processedBitmap);
			//processedBitmap = imageProcessor.findbiggestBlob(processedBitmap);
			dtctObject objectdetecter = new dtctObject();
			processedBitmap = objectdetecter.detectObj4(processedBitmap);
			//processedBitmap.Save(@"E:\Study\Third Semester\AutomationLab\Image Data\ImageOutput\outnoblob4_" + DateTime.Now.Second.ToString() + ".bmp");
			Bitmap processedImage = objectdetecter.blobDetector(processedBitmap);
			processedImage.Save(@"E:\Study\Third Semester\AutomationLab\Image Data\ImageOutput\outnoblob5_" + DateTime.Now.Millisecond.ToString() + ".bmp");
			return processedImage;
		}

		//TODO.. 
		//Check if biggest blob fits in the range of height. If not, Get the smaller blob and check again.
	}
}

