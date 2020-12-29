//
//	November 30, 2004	-	Louis Turmel	Class Implementation Added
//	February 25, 2005	-	Louis Turmel	Code Comments
//

using System;

namespace GA.BDC.Core.Web.UI.Uploader.ContactList.Events
{

	/// <summary>
	/// class use when Web Page Use have successfully upload his contact list
	/// on the server
	/// </summary>
	public class ContactListUploadEventArgs : Web.UI.Uploader.UploadFileEventArgs {
		public ContactListUploadEventArgs() {}
	}
	
	/// <summary>
	/// class use when user Web Page change the Contact List Type of his file pref
	/// </summary>
	public class ContactListTypeChangeEventArgs : Web.UI.Uploader.UploadFileEventArgs {
		public ContactListTypeChangeEventArgs(){}
	}

	/// <summary>
	///	 class use when the upload contact list process throw an error
	/// </summary>
	public class ContactListErrorEventArgs : System.EventArgs {
		/// <summary>
		/// Message of the base error raised
		/// </summary>
		public string Message;
		
		/// <summary>
		/// Default class Constructor
		/// </summary>
		public ContactListErrorEventArgs(){}

		/// <summary>
		/// class constructor with message to decribing the error
		/// </summary>
		/// <param name="pMessage"></param>
		public ContactListErrorEventArgs(string pMessage) {
			this.Message = pMessage;
		}
	}

	/// <summary>
	/// Event raise when the contact list file have been uploaded
	/// </summary>
	public delegate void ContactListUploadedEventHandler(object sender, ContactListUploadEventArgs e);

	/// <summary>
	/// Event raise when the contact list file type have been changed
	/// </summary>
	public delegate void ContactListTypeChangeEventHandler(object sender, ContactListTypeChangeEventArgs e);

	/// <summary>
	/// Event raise when the contact list object have throw an error
	/// </summary>
	public delegate void ContactListErrorEventHandler(object sender, ContactListErrorEventArgs e);
}
