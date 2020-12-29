using System;
using System.Web;
using System.Web.UI;
using System.Drawing;

namespace GA.BDC.Core.BarCode
{
	/// <summary>
	/// Base page to handle the BarcodeWriter.
	/// </summary>
	/// <author>Benoit Nadon</author>
	/// <date>02/23/2006</date>
	/// <version>1.0</version>
	public class BarcodeWriterPage : Page
	{
		private const int DEFAULT_WIDTH = 400;
		private const int DEFAULT_HEIGHT = 40;
		private const int DEFAULT_FONT_SIZE = 10;
		private const BarcodeType DEFAULT_BARCODE_TYPE = BarcodeType.Code39;

		private BarcodeWriter barcodeWriter = null;

		/// <summary>
		/// Underlying BarcodeWriter instance.
		/// </summary>
		protected virtual BarcodeWriter BarcodeWriter 
		{
			get 
			{
				if(barcodeWriter == null) 
				{
					barcodeWriter = new BarcodeWriter(Text, Width, Height, FontSize, BarcodeType);
				}

				return barcodeWriter;
			}
		}

		/// <summary>
		/// Text to be outputted as a barcode. Required to generate the image.
		/// </summary>
		protected virtual string Text 
		{
			get 
			{
				string text = String.Empty;

				if(Request.QueryString["Text"] != null) 
				{
					text = Request.QueryString["Text"].ToString();
				}

				return text;
			}
		}

		/// <summary>
		/// Width of the outputted image.
		/// </summary>
		protected virtual int Width 
		{
			get 
			{
				int width = DEFAULT_WIDTH;

				if(Request.QueryString["Width"] != null)
				{
					width = Convert.ToInt32(Request.QueryString["Width"]);
				} 

				return width;
			}
		}

		/// <summary>
		/// Height of the outputted image.
		/// </summary>
		protected virtual int Height 
		{
			get 
			{
				int height = DEFAULT_HEIGHT;

				if(Request.QueryString["Height"] != null)
				{
					height = Convert.ToInt32(Request.QueryString["Height"]);
				} 

				return height;
			}
		}

		/// <summary>
		/// Font size (pt) of the barcode.
		/// </summary>
		protected virtual int FontSize 
		{
			get 
			{
				int fontSize = DEFAULT_FONT_SIZE;

				if(Request.QueryString["FontSize"] != null)
				{
					fontSize = Convert.ToInt32(Request.QueryString["FontSize"]);
				} 

				return fontSize;
			}
		}

		/// <summary>
		/// Barcode type to output.
		/// </summary>
		protected virtual BarcodeType BarcodeType 
		{
			get 
			{
				BarcodeType barcodeType;

				try
				{
					barcodeType = (BarcodeType) Enum.Parse(typeof(BarcodeType), Request.QueryString["BarcodeType"].ToString(), true);
				} 
				catch 
				{
					barcodeType = DEFAULT_BARCODE_TYPE;
				}

				return barcodeType;
			}
		}

		/// <summary>
		/// Generates the barcode and outputs it as an image.
		/// </summary>
		/// <returns>Bitmap containing the barcode.</returns>
		protected virtual Bitmap GenerateBarcodeBitmap() 
		{
			return BarcodeWriter.GenerateBarcodeBitmap();
		}

		/// <summary>
		/// Generates the barcode and outputs the image as a JPEG file to the specified folder.
		/// </summary>
		/// <param name="path">Path where the image will be saved, including the file name.</param>
		/// <remarks>This is added because some softwares like SQL Server Reporting Services cannot import images generated on demand by an .aspx page.</remarks>
		protected virtual void SaveBarcodeImageToDisk(string path) 
		{
			BarcodeWriter.SaveBarcodeImageToDisk(path);
		}

		/// <summary>
		/// Deletes a previously saved JPEG.
		/// </summary>
		/// <param name="path">Path where the image was saved, including the file name.</param>
		/// <remarks>This is added because some softwares like SQL Server Reporting Services cannot import images generated on demand by an .aspx page.</remarks>
		protected virtual void DeleteBarcodeImageFromDisk(string path) 
		{
			BarcodeWriter.DeleteBarcodeImageFromDisk(path);
		}
	}
}
