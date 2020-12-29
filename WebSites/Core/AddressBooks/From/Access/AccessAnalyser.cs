//
//	November 30, 2004	-	Louis Turmel	Class Implementation Added
//

using System;
using System.IO;

using GA.BDC.Core.AddressBooks.Types;

namespace GA.BDC.Core.AddressBooks.From.Access{

	/// <summary>
	/// Classe d'analyse de liste de contact en provenance de Microsoft Access
	/// </summary>
	internal class AccessAnalyser : GenericEmailImporter {
		
		#region public constructors

		public AccessAnalyser(Stream pStream) {
			this.Analyser(pStream);
		}

		#endregion

		#region public override methods

		public override void Analyser(Stream pStream) {
			System.IO.StreamReader sr = new System.IO.StreamReader(pStream);
			int oCount = this.GetNumberContact(sr.ReadToEnd().Split(base.InvalidChar).GetEnumerator());
			sr.Close();
			int i = 0;
			try {
				using(System.IO.StreamReader usr = new StreamReader(pStream)) {
					string wLine;
					while((wLine = usr.ReadLine()) != null) {
						string[] oBlockLine = wLine.Split(base.InvalidChar);
						for(int j=0;j<oBlockLine.Length;j++) {
							if(this.IsValidEmail(oBlockLine[j]) && this.ContactManager.NotInTheList(oBlockLine[j])) {
								this.ContactManager.Add(new ContactInfo(oBlockLine[j].Substring(0,oBlockLine[j].IndexOf('@')),"",this.GetEmail(oBlockLine[j])));
								i++;
							}
						}
					}					
					usr.Close();
				}
			}
			catch{}
			finally{ sr.Close(); }
		}

		#endregion

		#region public override attributes

		public override GA.BDC.Core.AddressBooks.Types.ContactInfo[] ContactList {
			get{ return base.ContactList; }
			set{ base.ContactList = value; }
		}

		public override GA.BDC.Core.AddressBooks.Types.ContactListManager ContactManager {
			get{ return base.ContactManager; }
		}

		public override bool IsValidEmail(string pEmailToValid) {
			return base.IsValidEmail (pEmailToValid);
		}

		public override int GetNumberContact(System.Collections.IEnumerator pIEnumList) {
			return base.GetNumberContact(pIEnumList);
		}

		public override string GetEmail(string pCrtLine, string[] pExpKeyWord) {
			return base.GetEmail (pCrtLine, pExpKeyWord);
		}

		public override string GetEmail(string pCrtLine) {
			return base.GetEmail (pCrtLine);
		}

		public override GA.BDC.Core.AddressBooks.Types.FileType GetContactFileType {
			get{ return base.GetContactFileType; }
		}

		public override string FileName {
			get{ return base.FileName; }
			set{ base.FileName = value; }
		}

		public override int ContactNumber {
			get{ return base.ContactNumber; }
		}		

		#endregion
	}
}
