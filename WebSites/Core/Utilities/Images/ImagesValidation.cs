//
//	Mars 1, 2005	-	Louis Turmel	Code Comments
//

using System;
using GA.BDC.Core.Utilities;
using GA.BDC.Core.Utilities.Images;
using GA.BDC.Core.Utilities.SizeConverter;

namespace GA.BDC.Core.Utilities.Images
{

	#region public enum

	/// <summary>
	/// DataType using for the Validation Code Status
	/// </summary>
	[Serializable()]
	public enum ImagesValidationCode : byte {
		/// <summary>
		/// The File isn't the propers format
		/// </summary>
		BadFileType = 0, 
		/// <summary>
		/// The Dimension of the Image isn't fit
		/// </summary>
		DimensionNotFit = 1, 
		/// <summary>
		/// The Image file Size exceed the maximum permit
		/// </summary>
		ExceedFileSize = 2,
	}

	/// <summary>
	/// DataType used to give the Status of Validation
	/// </summary>
	[Serializable()]
	public enum ValidationStatus : byte {
		/// <summary>
		/// Validation have been failed
		/// </summary>
		Reject = 0,
		/// <summary>
		/// Validation have been accepted
		/// </summary>
		Accept = 1
	}

	/// <summary>
	/// DataType used to determine the Image format used
	/// </summary>
	[Serializable()]
	public enum ImageType : byte { 
		none = 0,
		jpg = 1,
		jpeg = 2,
		gif = 3,
		tiff = 4,
		psd = 5, 
		bmp = 6
	}

	#endregion

	#region public struct

	/// <summary>
	/// class struct object Containing the validation values. 
	/// It's used in the validation process
	/// </summary>
	[Serializable()]
	public struct ImagesValidationSpec {
		/// <summary>
		/// Maximum Image Width dimension
		/// </summary>
		public int Width;

		/// <summary>
		/// Maximum Image Height Dimension
		/// </summary>
		public int Height;

		/// <summary>
		/// Maximum Image File Size 
		/// </summary>
		public Utilities.SizeConverter.SizeValue Size;

		/// <summary>
		/// Image Extention permit
		/// </summary>
		public ImageType[] ImagesType;
	}

	#endregion

	/// <summary>
	/// This class contain some static method and function to provide
	/// a simple way to valide an image
	/// </summary>
	/// <remarks>This class cannot be Inherit from others</remarks>
	public sealed class ImagesValidation {
	
		/// <summary>
		/// Static function for extract ImageType[] from string
		/// </summary>
		/// <param name="oImageTypeContainer"></param>
		/// <returns></returns>
		public static ImageType[] GetImageType(string oImageTypeContainer) {
			ImageType[] oType = new ImageType[0];
			if(oImageTypeContainer.IndexOf(',') != -1) {
				string[] oTypeStr = oImageTypeContainer.Split(',');
				oType = new ImageType[oTypeStr.Length];
				for(byte i=0;i<oType.Length;i++) {
					try {
						oType[i] = (ImageType)Enum.Parse(typeof(ImageType),oTypeStr[i].ToLower());	
					} catch(Exception ex) {
						oType[i] = ImageType.none;
					}
				}
			} else if(oImageTypeContainer.Length > 0) {
				oType = new Images.ImageType[1];
				oType[0] = (ImageType)Enum.Parse(typeof(ImageType),oImageTypeContainer);
			}		
			return oType;
		}

		/// <summary>
		/// static function for gettting the validation report of the submit
		/// file from the web user
		/// </summary>
		/// <param name="pFilename">Filename (image path)</param>
		/// <param name="pSpec"></param>
		/// <returns></returns>
		public static ValidationStatus[] GetImagesValidation(string pFilename, ImagesValidationSpec pSpec) {		
			// Report object
			ValidationStatus[] oValidation = new ValidationStatus[3];
			bool[] oCase = new bool[Enum.GetValues(typeof(ImagesValidationCode)).Length];
			// Validation of the file format of the image file
			oCase[0] = FormatFileIsGood(pFilename,pSpec.ImagesType);
			// Validation of the dimension of the image file
			if(oCase[0])
				oCase[1] = DimensionImageIsGood(pFilename, pSpec.Width, pSpec.Height);
			else
				oCase[1] = false;
			// Validation of the file size of the image file
			oCase[2] = FileSizeIsGood(pFilename,pSpec.Size,pSpec.Size.SizeType);
			// loop containing the status of each validation process
			for(short i=0;i<oCase.Length;i++) {
				if(oCase[i])
					oValidation[i] = ValidationStatus.Accept;
				else
					oValidation[i] = ValidationStatus.Reject;
			}		
			return oValidation;
		}	

		/// <summary>
		/// static function for validating the file format
		/// </summary>
		/// <param name="pFilename">File name of the image to validate</param>
		/// <param name="pFileFormat">Type of format for this Image</param>
		/// <returns></returns>
		private static bool FormatFileIsGood(string pFilename, ImageType[] pFileFormat) {
			bool oIsGood = false;
			// Get a FileInfo for the File object from the pFileName parameter
			System.IO.FileInfo oFile = new System.IO.FileInfo(pFilename);
			string oFileExt = oFile.Extension.Remove(0,1).ToString();
			if(pFileFormat != null && pFileFormat.Length > 0) {
				foreach(object feExt in pFileFormat) {
					if(oFileExt == feExt.ToString() && feExt.ToString() != ImageType.none.ToString())
						oIsGood = true;
				}
			}
			return oIsGood;
		}

		/// <summary>
		/// static function for validating the dimension of the image
		/// </summary>
		/// <param name="pFilename">File name of the image to validate</param>
		/// <param name="pWidth">Maximum Image Width</param>
		/// <param name="pHeight">Maximum Image Height</param>
		/// <returns></returns>
		private static bool DimensionImageIsGood(string pFilename,int pWidth, int pHeight) {
			bool oIsGood = false;
			// Get an System.Drawing.Image from the pFilename Parameter
			System.Drawing.Image oImage = System.Drawing.Image.FromFile(pFilename);
			// CHECK THE IMAGE DIMENSION
			if(oImage.Height <= pHeight && oImage.Width <= pWidth)
				oIsGood = true;
			oImage.Dispose();
			return oIsGood;
		}

		/// <summary>
		/// static function for validating the file size of the file
		/// </summary>
		/// <param name="pFilename">File name of the image to validate</param>
		/// <param name="pSize">Maximum File size format to verify</param>
		/// <returns></returns>
		private static bool FileSizeIsGood(string pFilename, Utilities.SizeConverter.SizeValue pSize, Utilities.SizeConverter.ByteDefinition pByteDef) {
			bool oIsGood = false;
			System.IO.FileInfo oFile = new System.IO.FileInfo(pFilename);
			// Test the byte size with the specified file size from the specification
			if(Utilities.SizeConverter.BytesConverter.Compare(oFile.Length,pSize) != Utilities.SizeConverter.CompareResult.GreatherThan)
				oIsGood = true;
			return oIsGood;
		}
	}
}
