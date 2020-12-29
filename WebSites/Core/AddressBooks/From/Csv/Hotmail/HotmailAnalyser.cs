using System;
using System.IO;
using GA.BDC.Core.AddressBooks.Types;

namespace GA.BDC.Core.AddressBooks.From.Csv.Hotmail
{
    internal sealed class HotmailAnalyser : CsvAnalyser
    {
        #region private variables

		/// <summary>
		/// string array containing somes specifics FirstName columns name
		/// </summary>
		/// <remarks>Should be assignable from configuration settings</remarks>
        private string[] _FirstNameField = { "first name", "firstname", "members", "names", "fname" };	
	
		/// <summary>
		/// string array containing somes specifics Lastname columns name
		/// </summary>
		/// <remarks>Should be assignable from configuration settings</remarks>
        private string[] _LastNameField = { "last name", "lname", "lastname" };

		/// <summary>
		/// string array containing somes specifics Email columns name
		/// </summary>
		/// <remarks>Should be assignable from configuration settings</remarks>
		private string[] _EmailField = { "E-mail Address", "Email Address", "email", "e-mail" };

		#endregion

		#region public constructors

		/// <summary>
		/// class Constructor to analyse Yahoo csv based Stream content
		/// </summary>
		/// <param name="pStream"></param>
        public HotmailAnalyser(Stream pStream)
        {
			this.Analyser(pStream);
		}

		#endregion

		#region private functions
		
		/// <summary>
		/// Function returning the index of Columns Index of ColumnType specified by the pReadLine parameter
		/// </summary>
		/// <param name="pReadLine">Line to check if contain ColumnName</param>
		/// <param name="pColumnType">ColumnType to get the Index Number</param>
		/// <returns>Index of the ColumnType Name</returns>
		private int GetColumnIndex(string pReadLine, ColumnType pColumnType) {
			string[] oColumnName = pReadLine.Split(',',';');
			string[] oListHeaderName = null;
	
			#region switch on ColumnType

			switch(pColumnType) {
				case ColumnType.FirstName:
					oListHeaderName = _FirstNameField;
					break;
				case ColumnType.LastName:
					oListHeaderName = _LastNameField;
					break;
				case ColumnType.Email:
					oListHeaderName = _EmailField;
					break;
				default:
					oListHeaderName = _EmailField;
					break;
			}
			
			#endregion

			int oColumnNumber = -1;
			bool oFind = false;
			
			for(int i=0;i<oColumnName.Length;i++) {
				if(!oFind) {
					for(int j=0;j<oListHeaderName.Length;j++) 	{
						if(oColumnName[i].ToLower().IndexOf(oListHeaderName[j].ToLower().ToString(),0,oColumnName[i].Length) != -1) {
							oColumnNumber = i;
							oFind = true;
						}
					}
				}
			}			
			return oColumnNumber;
		}

		/// <summary>
		/// function returning the value from the parameter column Index
		/// specified
		/// </summary>
		/// <param name="pCrtLine">Line where we want the indexed value</param>
		/// <param name="pColumnNbr">Index to get the value</param>
		/// <returns></returns>
		private string GetColumnValue(string pCrtLine, int pColumnNbr) {
			string[] oField = pCrtLine.Split(';',',');
			if(pColumnNbr > -1)
				return oField[pColumnNbr].Replace('"',' ').Trim();
			else
				return "";
		}

		#endregion
		
		#region public override methods

		/// <summary>
		/// Method analysing a specified Stream containing csv contact list
		/// </summary>
		/// <param name="pStream">Stream of ContactList content</param>
		public override void Analyser(Stream pStream) {
			int i = 0;
			try {
				using(System.IO.StreamReader usr = new StreamReader(pStream)) {
					try {
						string wLine = usr.ReadLine().Replace('"',' ');
						int idxColumnFirst = GetColumnIndex(wLine,ColumnType.FirstName);
						int idxColumnLast = GetColumnIndex(wLine,ColumnType.LastName);
						int idxColumnEmail = GetColumnIndex(wLine, ColumnType.Email);
						while((wLine = usr.ReadLine()) != null) {
							string[] oLine = wLine.Replace('"',' ').Split(',',';');
							
							// Check if the Email is valid and the email doesn't exist in the 
							// current contact list
							if(this.IsValidEmail(oLine[idxColumnEmail].Trim()) && this.ContactManager.NotInTheList(oLine[idxColumnEmail].Trim())) {

                                this.ContactManager.Add(new ContactInfo(GetColumnValue(wLine, idxColumnFirst), 
                                                                        GetColumnValue(wLine, idxColumnLast),
                                                                        GetColumnValue(wLine, idxColumnEmail)));
								i++;
							}
						}
					} catch(Exception ex) {
						throw ex; 
					} finally {
						usr.Close();
					}
				}
			} catch(Exception ex){ throw ex; }
		}

		#endregion		
    }
}
