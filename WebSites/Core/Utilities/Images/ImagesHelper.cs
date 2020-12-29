//
//	March 04, 2005	-	Louis Turmel	Class added
//	March 23, 2005	-	Louis Turmel	New Methods and Function Overloading
//	March 30, 2005	-	Louis Turmel	Bug Fix on Auto-Resizing
//

using System;
using System.Drawing;

namespace GA.BDC.Core.Utilities.Images
{

	/// <summary>
	/// This class provide some static methods and function for work
	/// more easier with System.Drawing.Image object
	/// </summary>
	/// <example>
	///	<code>
	///		using System;
	///		using System.Drawing;
	///		
	///		using efundraising.Utitilies.Images;
	///		
	///		namespace myAppImage {
	///			
	///			[STAThread]
	///			public class myAppImage() {
	///			
	///				public static void main(string[] args) {
	///					try {
	///						ImageHelper.ImageReducer(@"c:\\ImageFolder\myVacation.jpg", "c:\\ImageTarget\small_myVacation.jpg",200);
	///						Console.WriteLine("Image resizing successfully");
	///					} catch(Exception ex) {
	///						Console.WriteLine(ex.Message);
	///					}
	///					// End of program
	///					Console.ReadLine();
	///				}			
	///			}			
	///		}	
	///	</code>
	/// </example>
	/// <remarks>This class cannot be inherit from another class</remarks>
	 public sealed class ImagesHelper {
	
		#region class constructors

		/// <summary>
		/// Class constructors
		/// </summary>
		public ImagesHelper() {
			
		}

		#endregion

		#region private static functions

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		private static bool ThumbnailCallback() {
			return false; 
		}

		#endregion

		#region public static method

		/// <summary>
		/// Static Method creating a new Image and resizing them from a specified Source Image.
		/// This new Image is resizing from a new Width
		/// </summary>
		/// <param name="pSourceImage">File path of the Souce Image that we want the new Image</param>
		/// <param name="pSaveAs">File name of the new Image from the Source Image</param>
		/// <param name="pMaxWidth">Width value for the New Image related of Source Image</param>
		public static void ImageReducer(string pSourceImage, string pSaveAs, short pMaxWidth, short pMaxHeight) {
			#region ArgumentNullException Checker

			if(pSourceImage == null)
				throw new ArgumentNullException("pSourceImage","You given an null parameter value - null Not Allowed");
			if(pSaveAs == null)
				throw new ArgumentNullException("pSaveAs","You given an null parameter value - null Not Allowed");
			
			#endregion

			ImageReducer(pSourceImage, pSaveAs, pMaxWidth, pMaxHeight, false);
		}

		/// <summary>
		/// Static Method creating a new Image and resizing them from a specified Source Image.
		/// This new Image is resizing from a new Width
		/// </summary>
		/// <param name="pSourceImage">File path of the Souce Image that we want the new Image</param>
		/// <param name="pSaveAs">File name of the new Image from the Source Image</param>
		/// <param name="pMaxWidth">Width value for the New Image related of Source Image</param>
		/// <param name="pUseEmbeddedColorMng">Specified the value if we want an automatic Embedded Color Management</param>
		public static void ImageReducer(string pSourceImage, string pSaveAs, short pMaxWidth, short pMaxHeight, bool pUseEmbeddedColorMng) {
			
			#region ArgumentNullException Checker

			if(pSourceImage == null)
				throw new ArgumentNullException("pSourceImage","You given an null parameter value - null Not Allowed");
			if(pSaveAs == null)
				throw new ArgumentNullException("pSaveAs","You given an null parameter value - null Not Allowed");

			#endregion

			Image oSmall = ImageReducer(pSourceImage, pMaxWidth, pMaxHeight, pUseEmbeddedColorMng);
			
			try {
				oSmall.Save(pSaveAs);
			} catch(System.IO.IOException iex) {
				throw;
			} catch(Exception ex) {
				throw;
			}
		}

		#endregion
		
		#region public static functions

		/// <summary>
		/// Static Method creating a new Image and resizing them from a specified Source Image.
		/// This new Image is resizing from a new Width
		/// </summary>
		/// <param name="pSourceImage">File path of the Souce Image that we want the new Image</param>
		/// <param name="pMaxWidth">Width value for the New Image related of Source Image</param>
		/// <returns>Return the New Image generated from the Source Image and new Resizing Parameters</returns>
		public static Image ImageReducer(Image pSourceImage, short pMaxWidth, short pMaxHeight) {
			
			#region ArgumentNullException Checker

			if(pSourceImage == null)
				throw new ArgumentNullException("pSourceImage","You given an null parameter value - null Not Allowed");
			
			#endregion

			Image.GetThumbnailImageAbort myCallback = new Image.GetThumbnailImageAbort(ThumbnailCallback);
			
			short oFinalWidth = 200;
			short oFinalHeight = 200;
			float factor1 = 1, factor2 = 1;
			
			if(pSourceImage.Width > pMaxWidth && pSourceImage.Height > pMaxHeight) {
				if(pSourceImage.Width > pSourceImage.Height) {
					factor1 = (float)pMaxWidth;
					factor2 = (float)pSourceImage.Width;
				} else{
					factor1 = (float)pMaxHeight;
					factor2 = (float)pSourceImage.Height;
				}
			} else if(pSourceImage.Width > pMaxWidth) {
				factor1 = (float)pMaxWidth;
				factor2 = (float)pSourceImage.Width;
			} else if(pSourceImage.Height > pMaxHeight) {
				factor1 = (float)pMaxHeight;
				factor2 = (float)pSourceImage.Height;
			}
			
			// We calculate the new pourcentage for resizing the Source Image to get the New Image			
			float oFactor = 1;

			try {
				oFactor = factor1 / factor2;
			} catch(DivideByZeroException ex) {
				throw;
			}

			#region New Width and Height of Images
			if(oFactor < 1) {
				oFinalWidth = (short)(pSourceImage.Width * oFactor);
				oFinalHeight = (short)(pSourceImage.Height * oFactor);
			} else {
				oFinalWidth = (short)pSourceImage.Width;
				oFinalHeight = (short)pSourceImage.Height;
			}
			#endregion	

			return pSourceImage.GetThumbnailImage(oFinalWidth, oFinalHeight, myCallback, IntPtr.Zero);
		}

		/// <summary>
		/// Static Method creating a new Image and resizing them from a specified Source Image.
		/// This new Image is resizing from a new Width
		/// </summary>
		/// <param name="pStream">Stream of Image as playing the role of Source Image</param>
		/// <param name="pMaxWidth">Width value for the New Image related of Source Image</param>
		/// <returns>Return the New Image generated from the Source Image and new Resizing Parameters</returns>
		public static Image ImageReducer(System.IO.Stream pStream, short pMaxWidth, short pMaxHeight) {
			
			#region ArgumentNullException Checker

			if(pStream == null)
				throw new ArgumentNullException("pStream", "You given an null parameter value - null Not Allowed");

			#endregion
			
			return ImageReducer(pStream, pMaxWidth, pMaxHeight, false);
		}

		/// <summary>
		/// Static Method creating a new Image and resizing them from a specified Source Image.
		/// This new Image is resizing from a new Width
		/// </summary>
		/// <param name="pStream">Stream of Image as playing the role of Source Image</param>
		/// <param name="pMaxWidth">Width value for the New Image related of Source Image</param>
		/// <param name="pUseEmbeddedColorMng">Specified the value if we want an automatic Embedded Color Management</param>
		/// <returns>Return the New Image generated from the Source Image and new Resizing Parameters</returns>
		public static Image ImageReducer(System.IO.Stream pStream, short pMaxWidth, short pMaxHeight, bool pUseEmbeddedColorMng) {
			#region ArgumentNullException Checker

			if(pStream == null)
				throw new ArgumentNullException("pStream", "You given an null parameter value - null Not Allowed");

			#endregion

			return ImageReducer(Image.FromStream(pStream, pUseEmbeddedColorMng), pMaxWidth, pMaxHeight);
		}

		/// <summary>
		/// Static Method creating a new Image and resizing them from a specified Source Image.
		/// This new Image is resizing from a new Width
		/// </summary>
		/// <param name="pSourceImage">File path of the Souce Image that we want the new Image</param>
		/// <param name="pMaxWidth">Width value for the New Image related of Source Image</param>
		/// <returns>Return the New Image generated from the Source Image and new Resizing Parameters</returns>
		public static Image ImageReducer(string pSourceImage, short pMaxWidth, short pMaxHeight) {
			
			#region ArgumentNullException Checker

			if(pSourceImage == null)
				throw new ArgumentNullException("pSourceImage", "You given an null parameter value - null Not Allowed");

			#endregion

			return ImageReducer(pSourceImage, pMaxWidth, pMaxHeight, false);		
		}

		/// <summary>
		/// Static Method creating a new Image and resizing them from a specified Source Image.
		/// This new Image is resizing from a new Width
		/// </summary>
		/// <param name="pSourceImage">File path of the Souce Image that we want the new Image</param>
		/// <param name="pMaxWidth">Width value for the New Image related of Source Image</param>
		/// <param name="pUseEmbeddedColorMng">Specified the value if we want an automatic Embedded Color Management</param>
		/// <returns>Return the New Image generated from the Source Image and new Resizing Parameters</returns>
		public static Image ImageReducer(string pSourceImage, short pMaxWidth, short pMaxHeight, bool pUseEmbeddedColorMng) {
			
			#region ArgumentNullException Checker

			if(pSourceImage == null)
				throw new ArgumentNullException("pSourceImage", "You given an null parameter value - null Not Allowed");

			#endregion

			return ImageReducer(Image.FromFile(pSourceImage,true), pMaxWidth, pMaxHeight);
		}

		#endregion

		// Obsolete Code here!
		/// <summary>
		/// Method to reduce an image and save the result of this image in new image file
		/// </summary>
		/// <param name="pSourceImage">Image source to resize</param>
		/// <param name="pSaveAs">Image name of the result</param>
		/// <param name="pMaxWidth"></param>
		public static void ImageReducer(string pSourceImage, string pSaveAs, short pMaxWidth) {
			System.Drawing.Image oSmall = ImageReducer(pSourceImage,pMaxWidth);
			try 
			{
				oSmall.Save(pSaveAs);	
				oSmall.Dispose();
				//	System.IO.File.Delete(pSourceImage);
			} 
			catch(Exception ex) 
			{
				throw ex;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="pSourceImage"></param>
		/// <param name="pMaxWidth"></param>
		/// <returns></returns>
		public static Image ImageReducer(Image pSourceImage, short pMaxWidth) {
			Image.GetThumbnailImageAbort myCallback = new Image.GetThumbnailImageAbort(ThumbnailCallback);
			short oFinalWidth = 200;
			short oFinalHeight = 200;
			float factor1 = (float)pMaxWidth;
			float factor2 = 0;
			if(pSourceImage.Width >= pSourceImage.Height)
				factor2 = (float)pSourceImage.Width;
			else if(pSourceImage.Width < pSourceImage.Height)
				factor2 = (float)pSourceImage.Height;
			float oFactor = factor1 / factor2;
			if(oFactor < 1) 
			{
				oFinalWidth = (short)(pSourceImage.Width * oFactor);
				oFinalHeight = (short)(pSourceImage.Height * oFactor);
			}
			return pSourceImage.GetThumbnailImage(oFinalWidth, oFinalHeight, myCallback, IntPtr.Zero);            
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="pSourceImage"></param>
		/// <param name="pMaxWidth"></param>
		/// <returns></returns>
		public static Image ImageReducer(string pSourceImage, short pMaxWidth) {
			return ImageReducer(Image.FromFile(pSourceImage),pMaxWidth);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="pSourceIamge"></param>
		/// <param name="pMaxWidth"></param>
		/// <returns></returns>
		public static Image ImageReducer(System.IO.Stream pSourceImage, short pMaxWidth) {
			return ImageReducer(Image.FromStream(pSourceImage), pMaxWidth);
		}
	}
}
