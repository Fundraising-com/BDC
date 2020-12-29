//
//	November 30, 2004	-	Louis Turmel	Class Implementation Added
//	February 24, 2005	-	Louis Turmel	Codes Comments
//

using System;
using System.IO;
using System.Collections;
using GA.BDC.Core.AddressBooks.From;
using GA.BDC.Core.AddressBooks.Types;


namespace GA.BDC.Core.AddressBooks.From.LotusNotes{
	
	/// <summary>
	///	class for extracting contact list information from LotusNotes based file
	/// </summary>
	/// <remarks>This class cannot be use from external namespace and
	/// cannot be instanciate</remarks>
	internal abstract class LotusNotes : GenericEmailImporter {

		#region private const variables

		/// <summary>
		/// First Name display in Lotus Notes ContactList
		/// </summary>
		/// <remarks>Should be set by Configuration settings</remarks>
		private const string _firstname = "FirstName";

		/// <summary>
		/// Last Name display in Lotus Notes ContactList
		/// </summary>
		/// <remarks>Should be set by Configuration settings</remarks>
		private const string _lastname = "LastName";

		/// <summary>
		/// MailAddress display in Lotus Notes ContactList
		/// </summary>
		/// <remarks>Should be set by Configuration settings</remarks>
		private const string _mailaddress = "MailAddress";
		
		#endregion

		#region public constructors

		/// <summary>
		/// Default Constructors
		/// </summary>
		public LotusNotes(){}

		#endregion

		#region public override attributes

		/// <summary>
		/// ContactInfo array Object
		/// </summary>
		public override ContactInfo[] ContactList {
			get{ return base.ContactList; }
			set{ base.ContactList = value; }
		}

		/// <summary>
		/// Get the ContactInfo number
		/// </summary>
		public override int ContactNumber { 
			get{ return base.ContactNumber; }
		}
        
		/// <summary>
		/// File name of the contact list
		/// </summary>
		public override string FileName {
			get{ return base.FileName; }
			set{ base.FileName = value; }
		}

		/// <summary>
		/// Get the FileType of the current ContactList
		/// </summary>
		public override FileType GetContactFileType {
			get{ return base.GetContactFileType; }
		}

		#endregion

		#region public override functions

		/// <summary>
		/// Function returning the number of contactList
		/// </summary>
		/// <param name="pIEnumList"></param>
		/// <returns></returns>
		public override int GetNumberContact(IEnumerator pIEnumList) {
			return base.GetNumberContact(pIEnumList);
		}
		
		/// <summary>
		/// Function returning the email inside a string
		/// </summary>
		/// <param name="pCrtLine"></param>
		/// <returns></returns>
		public override string GetEmail(string pCrtLine) {
			return base.GetEmail(pCrtLine);
		}

		#endregion		
		
		#region public override methods

		/// <summary>
		/// Method analysing the Lotus Notes contact list stream
		/// for setting contact list
		/// </summary>
		/// <param name="pStream"></param>
		public override void Analyser(Stream pStream) {
			System.IO.StreamReader sr = new StreamReader(pStream);
			string oAllPage = sr.ReadToEnd();
			sr.Close();
			string[] oPage = oAllPage.Split(base.InvalidChar);
			int oCount = this.GetNumberContact(oPage.GetEnumerator());
			ContactInfo[] oCstInfo = new ContactInfo[oCount];
			oCount = oAllPage.Split((char)13).Length;
			int j = 0;
			bool oEndDoc = false;
			sr.Close();		
			string[] oLines = oAllPage.Split((char)13);
			for(int i=0;i<oLines.Length;i++) {	
				try { 
					string oLine = oLines[i];
					if(oLine.IndexOf(_firstname) != -1)
						oCstInfo[j].FirstName = GetRowValue(oLine,ColumnType.FirstName).Trim();
					else if(oLine.IndexOf(_lastname) != -1)
						oCstInfo[j].LastName = GetRowValue(oLine,ColumnType.LastName).Trim();
					else if(oLine.IndexOf(_mailaddress) != -1) {
						oCstInfo[j].EmailAddr = GetRowValue(oLine,ColumnType.Email).Trim();
						if(oCstInfo[j].EmailAddr.Trim().Length > 0 && this.IsValidEmail(GetRowValue(oLine,ColumnType.Email).Trim())) {
							this.ContactManager.Add(oCstInfo[j]);
							j++;						
						}
					}
				} catch{ oEndDoc = true; }	
			}
		}

		#endregion
		
		#region private functions

		/// <summary>
		/// Function returning the value for the speficied ColumnType
		/// </summary>
		/// <param name="pLineValue">String Line to get the value</param>
		/// <param name="pColumnType">ColumnType Name</param>
		/// <returns></returns>
		private string GetRowValue(string pLineValue, ColumnType pColumnType) {
			string oValue = "";
			switch(pColumnType) {
				case ColumnType.FirstName:
					oValue = pLineValue.Split(':')[1].ToString();
					break;
				case ColumnType.LastName:
					oValue = pLineValue.Split(':')[1].ToString();
					break;
				case ColumnType.Email:
					oValue = pLineValue.Split(':')[1].ToString();
					break;
			}
			return oValue;
		}

		#endregion
	}
}
