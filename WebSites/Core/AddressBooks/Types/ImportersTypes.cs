//
//	November 30, 2004	-	Louis Turmel	Class Implementation Added
//	February 24, 2005	-	Louis Turmel	Code Comments
//

using System;

namespace GA.BDC.Core.AddressBooks.Types {

	#region public enum

	/// <summary>
	/// Enum des types de liste de contacts possible
	/// </summary>
	/// <remarks>This DataType is Serizalizable, therefore, we can store this
	/// DataType in Application, Session, ViewState object and doing portable from XML</remarks>
	[Serializable()]
	public enum ContactListType : byte {
		Yahoo = 0, 
		Outlook = 1,
		Outlook97 = 2, 
		Outlook2000 = 3, 
		OutlookXP = 4, 
		Outlook2003 = 5, 
		LotusNotes6 = 6,
		Excel = 7,
		Excel97 = 8, 
		Excel2000 = 9, 
		ExcelXP = 10, 
		Excel2003 = 11,
		MsnMessenger = 12,
		MsnMessenger62 = 13,
		TextFile = 14,
		Csv = 15,
		Access = 16,
		Access97 = 17, 
		Access2000 = 18, 
		AccessXP = 19, 
		Access2003 = 20,
		UnknowType = 21,
		Hotmail = 22
	}

	/// <summary>
	/// Extension of contact list file
	/// </summary>
	[Serializable()]
	public enum FileType : byte { 
		pst = 0, 
		csv = 1, 
		ctt = 2,
		xml = 3, 
		LotusText = 4, 
		Html = 5, 
		txt = 6,
		xls = 7, 
		mdb = 8,
		None = 9,
        xlsx = 10 
	}
	
	/// <summary>
	/// Name of ContactList column file
	/// </summary>
	public enum ColumnType : int { FirstName = 0, LastName = 1, Email = 2 };

	#endregion

	#region public struct

	/// <summary>
	/// Class Struct containing the informations of One Contact Object
	/// For storing FirstName, LastName and Email of the Contact Person
	/// </summary>
	/// <example>
	///		using System;
	///		using GA.BDC.Core.AddressBooks.Types;
	///		
	///		namespace MyContact {
	///			[STAThread()]
	///			public class mySample {
	///				
	///				public void Main(string[] args) {
	///					
	///					ContactInfo[] oGroupContact = new ContactInfo[2];
	///					oGroupContact[0] = new ContactInfo("Louis", "Turmel", "lturmel@agfying.net");
	///					oGroupContact[1] = new ContactInfo("Bill", "Gates", "bGates@microsoft.com");
	///					
	///					foreach(ContactInfo feCI in oGroupContact) {
	///						Console.WriteLine("{0} - {1} - {2}", feCI.FirstName, feCI.LastName, feCI.EmailAddr);
	///					}										
	///					Console.ReadLine();
	///				}
	///			}
	///		} 
	///	</example>
	///	<remarks>
	///		For using this Struct, we must call ContactInfo Constructor to Inialize a new object
	///	</remarks>
	[System.Serializable()]
	public struct ContactInfo {
		
		/// <summary>
		/// Default and Only One Constructor of Class Struct for Initialize 
		/// Contact Info properties
		/// </summary>
		/// <param name="pFrst"></param>
		/// <param name="pLst"></param>
		/// <param name="pEmail"></param>
		public ContactInfo(string pFrst, string pLst, string pEmail) {
			this.FirstName = pFrst.Trim();
			this.LastName = pLst.Trim();
			this.EmailAddr = pEmail;
		}


		public string FirstName;
		public string LastName;
		public string EmailAddr;

		#region public attributes

		/// <summary>
		/// Property given the Full Name of this person. By combining the First Name and Last Name
		/// </summary>
		public string FullName {
			get{ 
				// The Contact Object doesn't have First Name and Last Name
				if(this.FirstName.Trim().Length == 0 && this.LastName.Trim().Length == 0)
					return "";
				else
					return this.FirstName + " " + this.LastName; 
			}
		}

		/// <summary>
		/// Property given the FirstName of this contact
		/// </summary>
		public string FName {
			get{ return this.FirstName; }
		}

		/// <summary>
		/// Property given the LastName of this contact
		/// </summary>
		public string LName {
			get{ return this.LastName; }
		}
        
		/// <summary>
		/// Property given the Email of this contact
		/// </summary>
		public string Email {
			get{ return this.EmailAddr; }
		}

		#endregion
	}


	/// <summary>
	/// 
	/// </summary>
	[Serializable()]
	[Obsolete("Remove ContactListTypeContainer object from all projects",true)]
	public struct ContactListTypeContainer {

		private string _Name;
		private ContactListType _TypeList;

		/// <summary>
		/// Property for the Name of ContactListType
		/// </summary>
		public string Name {
			get{ return this._Name; }
			set{ this._Name = value; }
		}

		/// <summary>
		/// Property for the ContactListType object
		/// </summary>
		public ContactListType TypeList {
			get{ return this._TypeList; }
			set{ this._TypeList = value; }
		}
	}

	#endregion

	/// <summary>
	///	Class for manage ContactList Object
	/// </summary>
	/// <example>
	///	using System;
	///	
	///	namespace myManagerContactList {
	///		
	///		[STAThread]
	///		public class myManagerConsoleTest {
	///			
	///			public void Main(string[] args) {
	///				
	///				ContactListManager oMyContactManager = new ContactListManager();
	///				
	///				// Add Somes Contact Members
	///				oMyContactManager.Add(new ContactInfo("Louis","Turmel","lturmel@agfying.net"));
	///				oMyContactManager.Add("Bill","Gates","bGates@microsoft.com");
	///				oMyContactManager.Add(new ContactInfo("Alfred","Wallace","awallace@msn.com"));
	///				
	///				// Display the ContactInfo List
	///				foreach(ContactInfo feCT in oMyContactManager.ContactList) {
	///					Console.WriteLine("{0}	-	{1}	-	{2}", feCT.FirstName, feCT.LastName, feCT.EmailAddr);
	///				}
	///				
	///				// Remove an Contact
	///				oMyContactManager.Remove(1);			
	///				
	///				// Display the ContactInfo List
	///				foreach(ContactInfo feCT in oMyContactManager.ContactList) {
	///					Console.WriteLine("{0}	-	{1}	-	{2}", feCT.FirstName, feCT.LastName, feCT.EmailAddr);
	///				}
	///											
	///				Console.ReadLine();			
	///			}			
	///		}
	///	}
	/// </example>
	/// <remarks>This class cannot be Inheritable from others</remarks>
	public sealed class ContactListManager {
	
		#region private variables

		/// <summary>
		/// ContactInfo array containing all Contact Object
		/// </summary>
		private ContactInfo[] _contactList;

		#endregion

		#region public constructor

		/// <summary>
		/// Default Constructor of ContactListManager
		/// </summary>
		public ContactListManager(){
			this._contactList = new ContactInfo[0];
		}

		#endregion

		#region public methods

		/// <summary>
		///	Add new ContactInfo in the current Contact Info List
		/// </summary>
		/// <param name="pCtcInfo">ContactInfo object to add</param>
		public void Add(ContactInfo pCtcInfo) {
			if(this.ContactList == null)
				this._contactList = new ContactInfo[1];
			// Copy temporaly the current Contact List array
			ContactInfo[] oTempCtc = this._contactList;
			// Create the new Contact List Array
			ContactInfo[] oCtcNew = new ContactInfo[this.Count+1];
			// Copy all current content to the new Contact List Array
			for(int i=0;i<oTempCtc.Length;i++) {
				oCtcNew[i] = oTempCtc[i];
			}
			// Add the new ContactList Member
            oCtcNew[this.Count] = pCtcInfo;
			// Assign the new ContactList Array
			this._contactList = oCtcNew;
		}

		/// <summary>
		/// Add new ContactInfo in the current Contact Info List
		/// </summary>
		/// <param name="pFirstName">First Name of new Contact</param>
		/// <param name="pLastName">Last Name of new Contact</param>
		/// <param name="pEmail">Email of new Contact</param>
		public void Add(string pFirstName, string pLastName, string pEmail) {
			this.Add(new ContactInfo(pFirstName, pLastName, pEmail));
		}

		/// <summary>
		/// Remove an Contact Info from his index
		/// </summary>
		/// <param name="pIndex">Index to remove the Contact Info object</param>
		public void Remove(int pIndex) {
			if(this._contactList != null) {
				// Copy temporaly the current Contact List array
				ContactInfo[] oTempCtc = this._contactList;
				// Create the new Contact List Array
				ContactInfo[] oCtcNew = new ContactInfo[this.Count-1];
				// Loop to retrieve the ContactInfo index to remove from
				// the ContactInfo list
				for(int i=0;i<oTempCtc.Length;i++) {
					if(i < pIndex)
						oCtcNew[i] = oTempCtc[i];
					else if(i > pIndex)
						oCtcNew[i-1] = oTempCtc[i];
				}
				this._contactList = oCtcNew;
			}
		}

		
		#endregion
		
		#region public functions

		/// <summary>
		/// The current Email is in the list ?
		/// </summary>
		/// <param name="pEmail">Email to search if in the ContactInfo list</param>
		/// <returns>true / false</returns>
		public bool NotInTheList(string pEmail) {
			bool oIsExist = true;
			if(this.Count > 0) {
				foreach(ContactInfo frCtc in this._contactList) {
					if(frCtc.EmailAddr.ToLower() == pEmail.ToLower())
						oIsExist = false;
				}
			}
			return oIsExist;
		}

		#endregion

		#region public attributes

		/// <summary>
		/// ContactInfo List Object
		/// </summary>
		public ContactInfo[] ContactList {
			get{ return this._contactList; }
			set{
				this._contactList = new ContactInfo[value.Length];
				this._contactList = value; 
			}
		}

		/// <summary>
		/// Number of ContactInfo in the current list
		/// </summary>
		public int Count {
			get{ return this._contactList.Length; }
		}

		#endregion
	
	}
}
