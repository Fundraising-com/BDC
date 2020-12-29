using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.IO;

namespace GA.BDC.Core.BarCode
{
	/// <summary>
	/// Enumerates the different barcode types that can be printed.
	/// </summary>
	public enum BarcodeType 
	{
		Code39,
		Code39FullAscii
	}

	/// <summary>
	/// Tool to output a barcode as an image in the Code39 or Code39FullAscii format.
	/// </summary>
	/// <author>Benoit Nadon</author>
	/// <date>02/23/2006</date>
	/// <version>1.0</version>
	public class BarcodeWriter
	{
		private const int DEFAULT_WIDTH = 400;
		private const int DEFAULT_HEIGHT = 32;
		private const int DEFAULT_FONT_SIZE = 10;
		private const BarcodeType DEFAULT_BARCODE_TYPE = BarcodeType.Code39;

		private string text = String.Empty;
		private int width = DEFAULT_WIDTH;
		private int height = DEFAULT_HEIGHT;
		private int fontSize = DEFAULT_FONT_SIZE;
		private BarcodeType barcodeType = DEFAULT_BARCODE_TYPE;

		public BarcodeWriter() { }

		public BarcodeWriter(string text, int width, int height, int fontSize, BarcodeType barcodeType)
		{
			Text = text;
			Width = width;
			Height = height;
			FontSize = fontSize;
			BarcodeType = barcodeType;
		}

		/// <summary>
		/// Text to be outputted as a barcode. Required to generate the image.
		/// </summary>
		public virtual string Text 
		{
			get 
			{
				return text;
			}
			set 
			{
				text = value;
			}
		}

		/// <summary>
		/// Width of the outputted image.
		/// </summary>
		public virtual int Width 
		{
			get 
			{
				return width;
			}
			set 
			{
				width = value;
			}
		}

		/// <summary>
		/// Height of the outputted image.
		/// </summary>
		public virtual int Height 
		{
			get 
			{
				return height;
			}
			set 
			{
				height = value;
			}
		}

		/// <summary>
		/// Font size (pt) of the barcode.
		/// </summary>
		public virtual int FontSize 
		{
			get 
			{
				return fontSize;
			}
			set 
			{
				fontSize = value;
			}
		}

		/// <summary>
		/// Barcode type to output.
		/// </summary>
		public virtual BarcodeType BarcodeType 
		{
			get 
			{
				return barcodeType;
			}
			set 
			{
				barcodeType = value;
			}
		}

		/// <summary>
		/// FontFamily to be used according to the barcode type.
		/// </summary>
		protected virtual FontFamily FontFamily 
		{
			get 
			{
				FontFamily fontFamily = null;

				switch(BarcodeType)
				{
					case BarcodeType.Code39 :
					{
						fontFamily = Code39FontManager.Instance.FontFamilyCode39;
						break;
					}
					case BarcodeType.Code39FullAscii : 
					{
						fontFamily = Code39FontManager.Instance.FontFamilyCode39FullAscii;
						break;
					}
				}

				return fontFamily;
			}
		}

		/// <summary>
		/// Generates the barcode and outputs it as an image.
		/// </summary>
		/// <returns>Bitmap containing the barcode.</returns>
		public virtual Bitmap GenerateBarcodeBitmap() 
		{
			return GenerateBarcodeBitmap(false);
		}

		/// <summary>
		/// Generates the barcode and outputs it as an image.
		/// </summary>
		/// <returns>Bitmap containing the barcode.</returns>
		public virtual Bitmap GenerateBarcodeBitmap(bool addCheckSum) 
		{
			if(Text == String.Empty) 
			{
				throw new ArgumentException("The text to output as a barcode has not been specified", "Text");
			} 

			Bitmap bitmap = new Bitmap(Width, Height, PixelFormat.Format24bppRgb);
			bitmap.SetResolution(300f, 300f);
			Graphics graphics = Graphics.FromImage(bitmap);
			Font barcodeFont = new Font(this.FontFamily, this.FontSize, FontStyle.Regular, GraphicsUnit.Point);
			string checkSum = String.Empty;

			StringFormat stringFormat = new StringFormat();

			graphics.SmoothingMode = SmoothingMode.None;
			graphics.TextRenderingHint = TextRenderingHint.SingleBitPerPixel;
			graphics.Clear(Color.White);

			stringFormat.Alignment = StringAlignment.Center;

			if(addCheckSum) 
			{
				checkSum = GetCheckSum().ToString();
			}

			graphics.DrawString("*" + this.Text + checkSum + "*", barcodeFont, Brushes.Black, new Rectangle(0, 0, Width, Height), stringFormat);

			return bitmap;
		}

		/// <summary>
		/// Generates the barcode and outputs the image as a JPEG file to the specified folder.
		/// </summary>
		/// <param name="path">Path where the image will be saved, including the file name.</param>
		/// <remarks>This is added because some softwares like SQL Server Reporting Services cannot import images generated on demand by an .aspx page.</remarks>
		public void SaveBarcodeImageToDisk(string path) 
		{
			SaveBarcodeImageToDisk(path, false);
		}

		/// <summary>
		/// Generates the barcode and outputs the image as a JPEG file to the specified folder.
		/// </summary>
		/// <param name="path">Path where the image will be saved, including the file name.</param>
		/// <remarks>This is added because some softwares like SQL Server Reporting Services cannot import images generated on demand by an .aspx page.</remarks>
		public void SaveBarcodeImageToDisk(string path, bool addCheckSum) 
		{
			Bitmap bitmap = GenerateBarcodeBitmap(addCheckSum);
			EncoderParameters encoderParameters = new EncoderParameters(2);
			encoderParameters.Param[0] = new EncoderParameter(Encoder.Quality, 100L);
			encoderParameters.Param[1] = new EncoderParameter(Encoder.Compression, 0L);
			
			bitmap.Save(path, GetEncoderInfo("image/jpeg"), encoderParameters);
			bitmap.Dispose();
		}

		/// <summary>
		/// Deletes a previously saved JPEG.
		/// </summary>
		/// <param name="path">Path where the image was saved, including the file name.</param>
		/// <remarks>This is added because some softwares like SQL Server Reporting Services cannot import images generated on demand by an .aspx page.</remarks>
		public void DeleteBarcodeImageFromDisk(string path) 
		{
			File.Delete(path);
		}

		/// <summary>
		/// Returns an ImageCodecInfo to output bitmap to the specified Mime-Type.
		/// </summary>
		/// <param name="mimeType">Mime-Type to get the ImageCodecInfo for.</param>
		/// <returns>ImageCodecInfo for the specified Mime-Type.</returns>
		private ImageCodecInfo GetEncoderInfo(String mimeType)
		{
			ImageCodecInfo[] encoders = ImageCodecInfo.GetImageEncoders();
			ImageCodecInfo imageCodecInfo = null;

			for(int i = 0; i < encoders.Length; i++)
			{
				if(encoders[i].MimeType == mimeType) 
				{
					imageCodecInfo = encoders[i];
				}
			}

			return imageCodecInfo;
		}

		private char GetCheckSum() 
		{
			int sum = 0;
			char[] charArray = Text.ToCharArray();

			foreach(char c in charArray)
			{
				sum += Code39Table.Instance.GetCharacterValue(c);
			}

			return Code39Table.Instance.GetCharacter(sum % 43);
		}
	}
}
