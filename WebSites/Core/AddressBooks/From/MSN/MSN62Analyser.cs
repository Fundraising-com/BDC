//
//	November 30, 2004	-	Louis Turmel	Class Implementation Added
//

using System;
using System.IO;
using System.Xml;
using System.Collections;
using GA.BDC.Core.AddressBooks.Types;

namespace GA.BDC.Core.AddressBooks.From.MSN62 {
	
	/// <summary>
	/// class for analysing Contact List file from MsnMessenger 6.2
	/// </summary>
	/// <remarks>The class cannot be inheritable and use from another namespace</remarks>
	internal sealed class MSN62Analyser : GenericEmailImporter {

		#region private const variables
		
		/// <summary>
		/// XPath query to get all Contact Object from the MsnMessenger file
		/// </summary>
		private const string _xpathRetrieveENG = "//messenger/service/contactlist/contact";

		#endregion

		#region public constructors
		
		/// <summary>
		/// class constructor
		/// </summary>
		/// <param name="pStream">Stream containing all entries to analyze</param>
		public MSN62Analyser(Stream pStream) {
			this.Analyser(pStream);
		}

		#endregion

		#region public override attributes

		/// <summary>
		/// Filename of the current
		/// </summary>
		[Obsolete("I Need to remove ?",false)]
		public override string FileName {
			get{ return base.FileName; }
			set{ base.FileName = value; }
		}

		/// <summary>
		/// Contact List array from the ContactList stream
		/// </summary>
		public override ContactInfo[] ContactList {
			get{ return base.ContactList; }
		}

		/// <summary>
		/// Get the ContactFileType
		/// </summary>
		public override FileType GetContactFileType	{
			get{ return base.GetContactFileType; }
		}

		/// <summary>
		/// Get the Contact Number found inside the Stream
		/// </summary>
		public override int ContactNumber {
			get{ return base.ContactNumber; }
		}
		
		#endregion

		#region public override functions
	
		/// <summary>
		/// Function returing the number of contact from IEnumerator object
		/// </summary>
		/// <param name="pIEnumList"></param>
		/// <returns></returns>
		public override int GetNumberContact(IEnumerator pIEnumList) {
			return base.GetNumberContact (pIEnumList);
		}

		#endregion

		#region public override methods

		/// <summary>
		/// Method analyzing the Stream containing the entries of ContactList
		/// </summary>
		/// <param name="pStream"></param>
		public override void Analyser(Stream pStream) {
			
			XmlDocument oCttFile = new XmlDocument();
			try {
				oCttFile.Load(pStream);
				XmlNodeList oCtcList = oCttFile.SelectNodes(_xpathRetrieveENG);
				int oCount = this.GetNumberContact(oCttFile.SelectNodes(_xpathRetrieveENG).GetEnumerator());
				for(int i=0;i<oCount;i++) {
					string fEmail = oCtcList[i].InnerText;
					// Check if the email is valid and Not existing in the current
					// contact List object
					if(this.IsValidEmail(fEmail) && this.ContactManager.NotInTheList(fEmail)) {
						this.ContactManager.Add(new ContactInfo(fEmail.Substring(0,fEmail.IndexOf('@')),"",oCtcList[i].InnerText));
					}
				}
			} catch {
				throw new Exception("The file is not a valid Msn Messenger file");
			}
		}

		#endregion
	}
}
