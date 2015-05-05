using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using AForge.Imaging.Filters;
using AForge.Imaging;

namespace ObjectDetection
{
	class ObjExtractorNee
	{

		private Bitmap image;
		private Bitmap image_original;
		private Bitmap image_thres;
		private Bitmap image_eros;
		private Bitmap image_blobFilter;
		private IFilter grayscaleFilter = new Grayscale(0.2125, 0.7154, 0.0721);
		//private Bitmap image_binary;
		public Bitmap extractor(Bitmap image_original)
		{
			// create filter
			image = grayscaleFilter.Apply(image_original);
			// create filter
			HistogramEqualization filter_histEq = new HistogramEqualization();// histogram equalization of grayscale image
			//process image
			image = filter_histEq.Apply(image);
			BradleyLocalThresholding filter_thres = new BradleyLocalThresholding();
			image = filter_thres.Apply(image);
			Invert filter_invert = new Invert();
			image = filter_invert.Apply(image);
			// applying erosion
			BinaryErosion3x3 filter_eros = new BinaryErosion3x3();
			image = filter_eros.Apply(image);


			// applying blob filtering
			BlobsFiltering filter_blobFil = new BlobsFiltering();
			filter_blobFil.CoupledSizeFiltering = true;
			filter_blobFil.MinHeight = 50;
			filter_blobFil.MinWidth = 50;
			image = filter_blobFil.Apply(image);

			// applying erosion
			image = filter_eros.Apply(image);

			//applying blob filtering again
			image = filter_blobFil.Apply(image);

			// applying dilation 
			BinaryDilatation3x3 filter_dil = new BinaryDilatation3x3();
			image = filter_dil.Apply(image);

			// blob count and comparing to extract correct blob
			BlobCounterBase bc = new BlobCounter();
			bc.FilterBlobs = true;
			bc.MinHeight = 0;
			bc.MinWidth = 0;
			bc.ObjectsOrder = ObjectsOrder.Size;
			bc.ProcessImage(image);
			Blob[] blobs = bc.GetObjectsInformation();
			Blob bigBlob = null;
			int blob_height = 0;

			if (blobs.Length == 1) // checking only one blob if blob filter min height and min width is set high( in this case 50)
			{
				image = filter_dil.Apply(image);
				// extracting biggest blob
				image = Extract_BiggestBlob(image);
				image = filter_invert.Apply(image);
				return image;
				// pictureBox1.Image = (image);
			}

			else
			{
				// create filter
				image = grayscaleFilter.Apply(image_original);
				// create filter
				//  HistogramEqualization filter_histEq = new HistogramEqualization();// histogram equalization of grayscale image
				//process image
				image = filter_histEq.Apply(image);
				// BradleyLocalThresholding filter_thres = new BradleyLocalThresholding();
				image = filter_thres.Apply(image);
				// Invert filter_invert = new Invert();
				image = filter_invert.Apply(image);
				// applying erosion
				//  BinaryErosion3x3 filter_eros = new BinaryErosion3x3();
				image = filter_eros.Apply(image);


				// applying blob filtering
				//  BlobsFiltering filter_blobFil = new BlobsFiltering();
				filter_blobFil.CoupledSizeFiltering = true;
				filter_blobFil.MinHeight = 2;
				filter_blobFil.MinWidth = 2;
				image = filter_blobFil.Apply(image);

				// applying erosion
				image = filter_eros.Apply(image);

				//applying blob filtering again
				//  image = filter_blobFil.Apply(image);

				// applying dilation 
				// BinaryDilatation3x3 filter_dil = new BinaryDilatation3x3();
				image = filter_dil.Apply(image);

				// blob count and comparing to extract correct blob
				// BlobCounterBase bc = new BlobCounter();
				bc.FilterBlobs = true;
				bc.MinHeight = 0;
				bc.MinWidth = 0;
				bc.ObjectsOrder = ObjectsOrder.Size;
				bc.ProcessImage(image);
				blobs = bc.GetObjectsInformation();
				bigBlob = null;
				// int blob_height;

				if (blobs.Length > 1)// if there are more than one blob then we extract the proper blob by assuming  some area and if not 
				{

					for (int i = 0; i < blobs.Length; i++)
					{
						if (blobs[i].Area < 800) //&& blobs[i].Rectangle.Width < 90 && blobs[i].Rectangle.Height < 90)// && blobs[i].Area > 800)// assuming target object has white pixel range between 1000 and 2000
						{
							bigBlob = blobs[i];
							bc.ExtractBlobsImage(image, bigBlob, true);
							i = blobs.Length;
							blob_height = bigBlob.Rectangle.Height;
						}

					}


					// this condition is checked if all the blob area are still greater than 800 so need one more erosion
					if (bigBlob == null)
					{
						// apply one more erosion to reduce the area
						BinaryErosion3x3 filter_erosion = new BinaryErosion3x3();
						image = filter_erosion.Apply(image);
						//image = filter_blobFil.Apply(image); 

						bc.ProcessImage(image);
						blobs = bc.GetObjectsInformation();
						for (int i = 0; i < blobs.Length; i++)
						{
							if (blobs[i].Area < 800) // && blobs[i].Rectangle.Width < 90 && blobs[i].Rectangle.Height < 90)// && blobs[i].Area > 800)// assuming target object has white pixel range between 1000 and 2000
							{
								bigBlob = blobs[i];
								bc.ExtractBlobsImage(image, bigBlob, true);
								i = blobs.Length;
								blob_height = bigBlob.Rectangle.Height;
							}

						}


					}
					if (bigBlob == null)
					{
						// apply one more erosion to reduce the area
						BinaryErosion3x3 filter_erosion = new BinaryErosion3x3();
						image = filter_erosion.Apply(image);
						// image = filter_blobFil.Apply(image); 
						bc.ProcessImage(image);
						blobs = bc.GetObjectsInformation();
						for (int i = 0; i < blobs.Length; i++)
						{
							if (blobs[i].Area < 810) //&& blobs[i].Rectangle.Width < 90 && blobs[i].Rectangle.Height < 90)// && blobs[i].Area > 800)// assuming target object has white pixel range between 1000 and 2000
							{
								bigBlob = blobs[i];
								bc.ExtractBlobsImage(image, bigBlob, true);
								i = blobs.Length;
								blob_height = bigBlob.Rectangle.Height;
							}

						}


					}
					// for picture no need to invert in the beginning // concept is height 
					if (blob_height < 50)
					{
						// applying grayscale
						image = grayscaleFilter.Apply(image_original);
						HistogramEqualization filter = new HistogramEqualization();// histogram equalization of grayscale image
						image = filter.Apply(image);


						// applying thresholding
						// BradleyLocalThresholding filter_thres = new BradleyLocalThresholding();
						image_thres = filter_thres.Apply(image);


						// applying erosion
						// BinaryErosion3x3 filter_eros = new BinaryErosion3x3();
						image_eros = filter_eros.Apply(image_thres);
						// image_eros = filter_eros.Apply(image_eros);


						// applying blob filter // delete the all the blobs greater or less than specified blob's size
						BlobsFiltering filter_bloblsFiltering = new BlobsFiltering();
						filter_bloblsFiltering.CoupledSizeFiltering = true;
						filter_bloblsFiltering.MinHeight = 20;
						filter_bloblsFiltering.MinWidth = 20;
						image_blobFilter = filter_bloblsFiltering.Apply(image_eros); // applied blob filter

						// applying erosion again
						BinaryErosion3x3 filter_erosion = new BinaryErosion3x3();
						image_eros = filter_erosion.Apply(image_blobFilter);

						//  BlobCounterBase bc = new BlobCounter();
						bc.FilterBlobs = true;
						bc.MinHeight = 0;
						bc.MinWidth = 0;
						bc.ObjectsOrder = ObjectsOrder.Size;
						bc.ProcessImage(image_eros);
						blobs = bc.GetObjectsInformation();
						bigBlob = null;

						for (int i = 0; i < blobs.Length; i++)
						{
							if (blobs[i].Area < 900) //&& blobs[i].Rectangle.Width < 90 && blobs[i].Rectangle.Height < 90)// && blobs[i].Area > 800)// assuming target object has white pixel range between 1000 and 2000
							{
								bigBlob = blobs[i];
								bc.ExtractBlobsImage(image, bigBlob, true);
								i = blobs.Length;
							}

						}
						image = bigBlob.Image.ToManagedImage();
						image = filter_dil.Apply(image);
						image = filter_dil.Apply(image);
						image = filter_dil.Apply(image);
						// extracting biggest blob
						image = Extract_BiggestBlob(image);
						image = filter_invert.Apply(image);
						return image;
						// pictureBox1.Image = (image);

					}

					else
					{
						image = bigBlob.Image.ToManagedImage();
						image = filter_dil.Apply(image);
						image = filter_dil.Apply(image);
						// extracting biggest blob
						image = Extract_BiggestBlob(image);
						image = filter_invert.Apply(image);
						return image;
						// pictureBox1.Image = (image);
					}

				}
				else
				{
					image = filter_dil.Apply(image);
					// extracting biggest blob
					image = Extract_BiggestBlob(image);
					image = filter_invert.Apply(image);
					return image;
					//pictureBox1.Image = (image);
				}
			}
		}
		private Bitmap Extract_BiggestBlob(Bitmap image)
		{

			ExtractBiggestBlob filter = new ExtractBiggestBlob();
			image = filter.Apply(image);
			// pictureBox1.Image = (image);
			return image;
		}
	}
}


