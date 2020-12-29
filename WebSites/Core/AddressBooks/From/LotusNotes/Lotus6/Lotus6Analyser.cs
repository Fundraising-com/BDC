//
//	November 30, 2004	-	Louis Turmel	Class Implementation Added
//	February 24, 2005	-	Louis Turmel	Codes Comments
//

using System;
using System.IO;
using System.Collections;
using GA.BDC.Core.AddressBooks.Types;
using System.Collections.Specialized;

namespace GA.BDC.Core.AddressBooks.From.LotusNotes.Lotus6 {
	
	/// <summary>
	/// class to get the contact list member of Lotus Notes 6 file base of stream
	/// </summary>
	/// <remarks>This class cannot be use by external namespace and cannot be
	/// inherit from another class</remarks>
	internal sealed class Lotus6Analyser : LotusNotes {
				
		#region public constructors

		/// <summary>
		/// class constructor with Stream parameter
		/// </summary>
		/// <param name="pStream"></param>
		public Lotus6Analyser(Stream pStream) {
			this.Analyser(pStream);
		}

		#endregion

		#region public override attributes

		/// <summary>
		/// Get or Set the ContactInfo array 
		/// </summary>
		public override ContactInfo[] ContactList {
			get{ return base.ContactList; }
			set{ base.ContactList = value; }
		}

		/// <summary>
		/// Get the ContactFileType of the Contact List
		/// </summary>
		public override FileType GetContactFileType {
			get{ return base.GetContactFileType; }
		}

		/// <summary>
		/// Get or Set the FileName of the current ContactList
		/// </summary>
		public override string FileName {
			get{ return base.FileName; }
			set{ base.FileName = value; }
		}

		/// <summary>
		/// Get the Contact List Number found
		/// </summary>
		public override int ContactNumber {
			get{ return base.ContactNumber; }
		}

		#endregion

		#region public override methods

		/// <summary>
		/// Method analysing a Lotus Notes contact list stream
		/// </summary>
		/// <param name="pStream"></param>
		public override void Analyser(Stream pStream) {
			base.Analyser(pStream);
		}

		#endregion

		#region public override functions

		/// <summary>
		/// Get Email from string line
		/// </summary>
		/// <param name="pCrtLine"></param>
		/// <returns></returns>
		public override string GetEmail(string pCrtLine) {
			return base.GetEmail(pCrtLine);
		}

		/// <summary>
		/// Function returning the possible number of contact 
		/// </summary>
		/// <param name="pIEnumList"></param>
		/// <returns></returns>
		public override int GetNumberContact(IEnumerator pIEnumList) {
			return base.GetNumberContact(pIEnumList);
		}

		#endregion
	}
}
