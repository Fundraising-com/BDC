//
//	November 30, 2004	-	Louis Turmel	Class Implementation		Added
//	January 14, 2005	-	Louis Turmel	UploadErrorEventArgs		Added
//											UploadedErrorEventHandler	Added
//	February 23, 2005	-	Louis Turmel	Code Comments
//

using System;

namespace GA.BDC.Core.Web.UI.Uploader
{
	#region public class - EventArgs
	
	/// <summary>
	/// class use as event object, used when a file have been successful
	/// uploaded on a specified server path
	/// </summary>
	public class UploadFileEventArgs : EventArgs {

		/// <summary>
		/// Default class Contructors
		/// </summary>
		public UploadFileEventArgs() {}
		
		/// <summary>
		/// class contructor with file name as parameter
		/// </summary>
		/// <param name="pFilename">FileName uploaded</param>
		public UploadFileEventArgs(string pFilename) {
			this.Filename = pFilename;
		}
		
		/// <summary>
		/// class contructor with file name and is Accepted parameters
		/// </summary>
		/// <param name="pFilename">Filename uploaded</param>
		/// <param name="pAccepted">If the file has been accepted</param>
		public UploadFileEventArgs(string pFilename, bool pAccepted) : this(pFilename) {
			this.FileAccepted = pAccepted;
		}	
		
		/// <summary>
		/// The file have been successful uploaded and save
		/// </summary>
		public bool FileAccepted = true;
		
		/// <summary>
		/// Filename uploaded
		/// </summary>
		public string Filename;

	}

	/// <summary>
	/// class use as event, used when a file upload wasn't be
	/// completed successful
	/// </summary>
	public class UploadErrorEventArgs : EventArgs {

		/// <summary>
		/// Default class constructor
		/// </summary>
		public UploadErrorEventArgs() {}
		
		/// <summary>
		/// class constructor with FileName parameter
		/// </summary>
		/// <param name="pFilename"></param>
		public UploadErrorEventArgs(string pFilename) {
			this.Filename = pFilename;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="pException"></param>
		public UploadErrorEventArgs(Exception pException) {
			this.ExceptionRaise = pException;
		}

		/// <summary>
		/// Filename when the uploaderError event have been raise
		/// </summary>
		public string Filename;

		/// <summary>
		/// 
		/// </summary>
		public Exception ExceptionRaise;
	}
	
	#endregion

	#region public delegate - EventHandler

	/// <summary>
	/// FileUploadedEventHandler raise when a file have been upload
	/// </summary>
	public delegate void FileUploadedEventHandler(object sender, UploadFileEventArgs e);

	/// <summary>
	/// UploadedErrorEventHandler when the upload process have been failed
	/// </summary>
	public delegate void UploadedErrorEventHandler(object sender, UploadErrorEventArgs e);

	#endregion
}
