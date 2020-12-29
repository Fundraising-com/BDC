//
//	November 30, 2004	-	Louis Turmel	Class Implementation Added
//	February 24, 2005	-	Louis Turmel	Code Comments
//

using System;
using System.IO;
using System.Xml;
using System.Collections;
using System.Text.RegularExpressions;

using GA.BDC.Core.AddressBooks.Types;

namespace GA.BDC.Core.AddressBooks.From {	

	/// <summary>
	/// GenericEmailImporter is the base class for any Type of Contact List Type
	/// It provide an base Implementation and Definition of somes methods, properties
	/// and functions. 
	/// </summary>
	/// <remarks>This class cannot be instanciate or use from another namespace</remarks>
	internal abstract class GenericEmailImporter : iEmailImporter {
		
		#region private variables


		private int _contactnumber = 0;	

		private ContactListManager _contactinfo;

		private FileType _filetype;
		private string _filename;

		private Regex regEx;

		#endregion

		#region protected variables

		/// <summary>
		/// List of invalid character to exclude from the text stream of the File or Stream object content
		/// </summary>
		protected char[] InvalidChar = { (char)0,(char)1,(char)2,(char)3,(char)4,(char)5,(char)6,(char)7,(char)8,(char)9,
			(char)10,(char)11,(char)12,(char)13,(char)14,(char)15,(char)16,(char)17,(char)18,(char)19,
			(char)20,(char)21,(char)22,(char)23,(char)24,(char)25,(char)26,(char)27,(char)28,(char)29,
			(char)30,(char)31,(char)32,(char)33,(char)34,(char)35,(char)36,(char)37,(char)38,(char)39,
			(char)40,(char)41,(char)42,(char)43,(char)45,(char)47,(char)58,(char)59,(char)60,(char)61,(char)62,
			(char)63,(char)91,(char)92,(char)93,(char)94,(char)96,(char)123,(char)124,(char)125,(char)126,
			(char)127,',' };

		#endregion

		#region public constructors

		/// <summary>
		/// Class Constructor.
		/// </summary>
		public GenericEmailImporter() {
			// WHY I DOESN'T USE A CENTER POINT TO GET THE REGEX KEY FOR EMAIL VALIDATION
			this.regEx = new Regex(@"^[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]{1,6}$");	
			this._contactinfo = new ContactListManager();
		}	

		#endregion

		#region destructors

		/// <summary>
		/// Destructor of the class
		/// </summary>
		~GenericEmailImporter() {}
	
		#endregion
        
		#region public virtual attributes

		/// <summary>
		/// Property given the Number of ContactNumber
		/// </summary>
		public virtual int ContactNumber {
			get{ return this._contactinfo.Count; }
		}

		/// <summary>
		/// Get or Set the ContactInfo List
		/// </summary>
		public virtual ContactInfo[] ContactList {
			get{ return this._contactinfo.ContactList; }
			set{ this._contactinfo.ContactList = value; }
		}

		/// <summary>
		/// Get the ContactFileType of the Current Contact List used
		/// </summary>
		public virtual FileType GetContactFileType {
			get{ return this._filetype; }
		}

		/// <summary>
		/// Get or Set the FileName oe the current Contact List used
		/// </summary>
		public virtual string FileName {
			get{ return this._filename; }
			set{ this._filename = value; }
		}

		/// <summary>
		/// Get the ContactListManager to Manage the current ContactList
		/// </summary>
		public virtual ContactListManager ContactManager {
			get{ return this._contactinfo; }
		}

		#endregion

		#region public abstract methods

		/// <summary>
		/// Definition of Analyser Method for the Derivable class
		/// </summary>
		/// <param name="pStream">Stream containing an ContactList Entry to analyse</param>
		public abstract void Analyser(Stream pStream);

		#endregion

		#region public virtual functions

		/// <summary>
		/// See if the Email is Valid
		/// </summary>
		/// <param name="pEmailToValid">Email to check if it valid</param>
		/// <returns>true/false</returns>
		public virtual bool IsValidEmail(string pEmailToValid) {
			return this.regEx.IsMatch(pEmailToValid);
		}
		
		/// <summary>
		/// Get the Email String in the line expressed by specified list of KeyWords
		/// </summary>
		/// <param name="pCrtLine">Line containing some values, words and characters</param>
		/// <param name="pExpKeyWord"></param>
		/// <returns></returns>
		public virtual string GetEmail(string pCrtLine, string[] pExpKeyWord) {
			string oEmail = this.GetEmail(pCrtLine);
			foreach(string frKeyWord in pExpKeyWord) {
				oEmail = oEmail.Replace(frKeyWord," ");
			}
			string[] oSplit = oEmail.Split(' ');

			foreach(string frSplit in oSplit) {
				if(this.IsValidEmail(frSplit))
					oEmail = this.GetEmail(frSplit);
			}
			return oEmail;
		}

		/// <summary>
		/// Get the number of Possible Contact entry inside an
		/// IEnumerable Object
		/// </summary>
		/// <param name="pIEnumList"></param>
		/// <returns></returns>
		public virtual int GetNumberContact(IEnumerator pIEnumList) {
			int oCount = 0;
			int oCountTotal = 0;
			
			while(pIEnumList.MoveNext()) {
				// Conditional on the base type was sent the 
				// IEnumerable Object
				switch(pIEnumList.Current.GetType().ToString()) {
					
					case "System.Xml.XmlElement":
						XmlElement oElmt = (XmlElement)pIEnumList.Current;
						if(this.IsValidEmail(oElmt.InnerText))
							oCount++;
						oCountTotal++;
						break;
					
					case "System.String":
						if(this.IsValidEmail(pIEnumList.Current.ToString()))
							oCount++;
						oCountTotal++;
						break;
				}
			}
			return oCount;						
		}

		[Obsolete("Should trap more than 1 mail address in the line...",false)]
		public virtual string GetEmail(string pCrtLine) {
			string oEmail = pCrtLine;
			if(this.IsValidEmail(oEmail))
				oEmail = oEmail.Replace('"',' ').Trim();
			return oEmail;
		}

		#endregion
	}
}
