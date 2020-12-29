//
//	November 30, 2004	-	Louis Turmel	Class Implementation Added
//	February 24, 2005	-	Louis Turmel	Code Comments
//

using System;
using GA.BDC.Core.AddressBooks.Types;

namespace GA.BDC.Core.AddressBooks.From {
	
	/// <summary>
	/// Class d'interface
	/// </summary>
	interface iEmailImporter {

		/// <summary>
		/// Property given the Number of ContactInfo object
		/// </summary>
		int ContactNumber{get;}

		/// <summary>
		/// Array Of ContactInfo[] object
		/// </summary>
		ContactInfo[] ContactList{get; set;}

		/// <summary>
		/// Property given the File Type of the contact list
		/// </summary>
		FileType GetContactFileType{get;}

		/// <summary>
		/// Contact List Filename
		/// </summary>
		string FileName{get; set;}
	}
}
