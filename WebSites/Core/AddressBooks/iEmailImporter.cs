//
//	November 30, 2004	-	Louis Turmel	Class Implementation Added
//	February 24, 2005	-	Louis Turmel	Code Comments
//

using System;

namespace GA.BDC.Core.AddressBooks {
	
	/// <summary>
	/// Class Interface containing the object definition for the class derivable
	/// </summary>
	public interface iEmailImporters {

		/// <summary>
		/// Property of the current Contact List Type used
		/// </summary>
		Types.ContactListType CurrentContactListType{get;set;}
	
		/// <summary>
		/// Property given the File Type of the contact list
		/// </summary>
		Types.FileType GetFileType{get;}

		/// <summary>
		/// Array Of ContactInfo[] object
		/// </summary>
		Types.ContactInfo[] GetContactList{get;}

		/// <summary>
		/// Contact List Filename
		/// </summary>
		string ContactFileName{get;}
	}

}
