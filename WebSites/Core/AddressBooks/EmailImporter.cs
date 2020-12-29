//
//	November 30, 2004	-	Louis Turmel	Class Implementation Added
//	December 01, 2004	-	Louis Turmel	Comments added with example
//	February 24, 2005	-	Louis Turmel	Comments added and Final Code Review
//											before first release on Monday, February 28
//

using System;
using System.IO;

using GA.BDC.Core.AddressBooks.From;
using GA.BDC.Core.AddressBooks.Types;

namespace GA.BDC.Core.AddressBooks {
	/// <summary>
	/// Class of email importer from ContactList
	/// </summary>
	/// <example>
	///	<code>
	///	//	Objects declaration
	///	private EmailImporters.EmailImporter _myImporter;
	///	private ContactInfo[] _myContactList;
	///	
	///	// Some Codes Heres...
	///	
	///	//	Use this objects
	///	this._myImporter = new EmailImporters.EmailImporter("c:\\MACONTACTLIST.csv,ContactListType.Csv);
	///	
	///	//	Return ContactInfo array - Serializable and can be use as DataSource
	///	this._myContactList = this._myImporter.GetContactList;
	///	
	///	</code>
	/// </example>
	/// <remarks>Not be inheritable<br>
	///		<b>File Provider not implemented</b>
	///		<list type="">
	///			<item>Hotmail .html</item>
	///			<item>Outlook .Pst</item>
	///			<item>Excel .xls</item>
	///			<item>Access .mdb</item>
	///		</list> 
	///	</remarks>
	public sealed class EmailImporter : iEmailImporters {

		#region private variables

		private FileType _filetype;
		private ContactInfo[] _contactinfo;
		private ContactListType _ctclsttp;

		private Stream _Stream;
		private string _filename;

		/// <summary>
		/// Interface to store the derivable instance of GenericEmailImporter Class
		/// </summary>
		private iEmailImporter _iemailimporter;

		#endregion

		#region public constructor

		/// <summary>
		/// Class Constructor
		/// </summary>
		/// <param name="pFilename">Contact List Filename to import data</param>
		/// <param name="pCtcType">Contact List Type - What is the Software Source File ?</param>
		public EmailImporter(string pFilename, ContactListType pCtcType)  
		{
			switch(pCtcType) 
			{
				// Using OleDBCommand to Open a Excel file so there is no need to use Stream.
				case ContactListType.Excel:
				case ContactListType.Excel97:
				case ContactListType.ExcelXP:
				case ContactListType.Excel2003:
				case ContactListType.Excel2000:
					this._filename = pFilename;
					this._ctclsttp = pCtcType;
					this.SetContactList(pCtcType);
					break;
				default:
					DoConstructor(new StreamReader(pFilename).BaseStream,pCtcType);
					break;
			}
			
		}		
		// In this case, the constructor call the EmailImporter(Stream, ContactListType) constructor
		// It's for this reason, I used, [ : this(Stream, ContactListType) ] before the declaration
		// of the EmailImporter(string, ContactListType) constructor


		/// <summary>
		/// Class Constructor
		/// </summary>
		/// <param name="pStream">Stream containing the Contact List File Stream</param>
		/// <param name="pCtcType">Contact List Type - What is the Software Source File</param>
		public EmailImporter(Stream pStream, ContactListType pCtcType) {
			DoConstructor(pStream, pCtcType);
		}

		#endregion

		#region private methods

		private void DoConstructor(Stream pStream, ContactListType pCtcType)
		{
			this._ctclsttp = pCtcType;
			this._Stream = pStream;
			this.SetContactList(pCtcType);
		}

		/// <summary>
		/// Point d'entre assignant la bonne classe selon le format de liste de contact choisi par
		/// l'utilisateur
		/// </summary>
		/// <param name="pCtcType"></param>
		private void SetContactList(ContactListType pCtcType) {
			// Conditional group, base on ContactListType parameter
			// The IEmailImporter is used to store GenericEmailImporter class
			switch(pCtcType) {
				case ContactListType.MsnMessenger:
				case ContactListType.MsnMessenger62:
					this._iemailimporter = new From.MSN62.MSN62Analyser(this._Stream);
					break;
				case ContactListType.Hotmail:
					throw new NotImplementedException("Hotmail format have not been implement in this release");
					break;
				case ContactListType.Access:
				case ContactListType.Access97:
				case ContactListType.Access2000:
				case ContactListType.AccessXP:
				case ContactListType.Access2003:
					throw new NotImplementedException("Access format have not been implement in this release");
					//this._iemailimporter = new From.Access.AccessAnalyser(this._Stream);
					break;
				case ContactListType.Excel:
				case ContactListType.Excel97:
				case ContactListType.ExcelXP:
				case ContactListType.Excel2003:
				case ContactListType.Excel2000:				
					//throw new NotImplementedException("Excel analyser format have not been implement in this release");
					//this._iemailimporter = new From.Excel.ExcelAnalyser(this._Stream);
					this._iemailimporter = new From.Excel.ExcelAnalyser (this._filename );
					break;
				case ContactListType.LotusNotes6:
					this._iemailimporter = new From.LotusNotes.Lotus6.Lotus6Analyser(this._Stream);
					break;	
				case ContactListType.Outlook:
				case ContactListType.Outlook97:
				case ContactListType.OutlookXP:
				case ContactListType.Outlook2000:
				case ContactListType.Outlook2003:
					throw new NotImplementedException("Outlook Pst based file analyse have not been implement in this release");
					//	this._iemailimporter = new From.Outlook.OutlookAnalyser(this._Stream);					
					break;
				case ContactListType.Yahoo:
					this._iemailimporter = new From.Csv.Yahoo.YahooAnalyser(this._Stream);
					break;
				case ContactListType.Csv:
					this._iemailimporter = new From.Csv.CsvAnalyser(this._Stream);
					break;
				case ContactListType.TextFile:
					this._iemailimporter = new From.Text.TextAnalyser(this._Stream);
					break;
				case ContactListType.UnknowType:
					this._iemailimporter = new From.Text.TextAnalyser(this._Stream);
					break;
			}
			this._contactinfo = this._iemailimporter.ContactList;
		}

		#endregion

		#region public attributes

		/// <summary>
		/// Contact List Filename
		/// </summary>
		public string ContactFileName {
			get{ return this._filename; }
		}
		
		/// <summary>
		/// Contact List Content, extracted from the File or stream used
		/// </summary>
		public ContactInfo[] GetContactList {
			get{ return this._contactinfo; }
		}

		/// <summary>
		/// File type of the contact List
		/// </summary>
		public FileType GetFileType {
			get{ return this._filetype; }
		}

		/// <summary>
		/// Get or Set the CurrentContactListType format
		/// </summary>
		public ContactListType CurrentContactListType {
			get{ return this._ctclsttp; }
			set{ this._ctclsttp = value; }
		}

		#endregion
	}
}
