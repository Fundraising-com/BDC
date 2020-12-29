//
//	November 30, 2004	-	Louis Turmel	Class Implementation Added
//

using System;
using System.IO;
using System.Collections;

using GA.BDC.Core.AddressBooks.Types;

namespace GA.BDC.Core.AddressBooks.From.Outlook {
	/// <summary>
	/// Classe d'analyse de liste de contact provenant d'Outlook
	/// </summary>
	internal sealed class OutlookAnalyser : GenericEmailImporter {
		
		#region protected internal variables

		internal string[] SpecificKeyWords = { "SMTP", "FAX" };

		#endregion

		#region public constructors

		public OutlookAnalyser(Stream pStream) {
			this.Analyser(pStream);
		}

		#endregion

		#region public override functions

		public override bool IsValidEmail(string pEmailToValid) {
			return base.IsValidEmail (pEmailToValid);
		}

		public override string GetEmail(string pCrtLine, string[] pExpKeyWord) {
				return base.GetEmail(pCrtLine, pExpKeyWord);
		}
		
		public override int GetNumberContact(System.Collections.IEnumerator pIEnumList) {
			return base.GetNumberContact (pIEnumList);
		}

		public override string GetEmail(string pCrtLine) {
			return base.GetEmail (pCrtLine);
		}

		#endregion

		#region public override attributes

		public override ContactListManager ContactManager {
			get{ return base.ContactManager; }
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

		public override GA.BDC.Core.AddressBooks.Types.ContactInfo[] ContactList 	{
			get{ return base.ContactList; }
		}

		#endregion

		#region public override methods

		public override void Analyser(Stream pStream) {
			int i = 0;
			try {
				using(System.IO.StreamReader usr = new StreamReader(pStream)) {
					try {
						string wLine;
						while((wLine = usr.ReadLine()) != null) {
							string[] oBlockLine = wLine.Split(base.InvalidChar);
							bool oEFinder = false, oLNameFinder = false, oFNameFinder = false;
							string oEMail = "", oLName = "", oFName = "";
							for(int j=0;j<oBlockLine.Length;j++) {							
								if(this.IsValidEmail(oBlockLine[j]) && this.ContactManager.NotInTheList(oBlockLine[j]) && !oEFinder) {
									this.FindFullName(oBlockLine[j]);
									this.ContactManager.Add(new ContactInfo(oBlockLine[j].Substring(0,oBlockLine[j].IndexOf('@')),"",this.GetEmail(oBlockLine[j],this.SpecificKeyWords)));
									i++;
									oEFinder = true;
								}
								if(oEFinder) {
									// Find the last name								
									oLNameFinder = true;
								}
								if(oLNameFinder) {

									// Find the First Name
									oFNameFinder = true;
								}
							}
						}			
					} catch(Exception ex) { 
						throw ex; 
					} finally {
						usr.Close();
					}
				}
			}
			catch(Exception ex){ throw ex; }
		}


		#endregion

		#region private methods

		private void FindFullName(string pLineValue) {
			string oFullName = pLineValue;
			int oIndex = pLineValue.IndexOf("SMTP");
			if(oIndex > -1) {
				oFullName = pLineValue.Substring(oIndex,pLineValue.Length - oIndex);
				string oExtractEmail = oFullName.Replace(this.GetEmail(oFullName),"");
			}
		}
		
		#endregion
	}
}
