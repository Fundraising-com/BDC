//
//	November 30, 2004	-	Louis Turmel	Class Implementation Added
//

using System;
using System.IO;
using GA.BDC.Core.AddressBooks.Types;
using System.Data.OleDb; 
using System.Data;

namespace GA.BDC.Core.AddressBooks.From.Excel {
	/// <summary>
	/// Classe d'analyse de contactlist de fichier d'excel
	/// </summary>
	internal class ExcelAnalyser : GenericEmailImporter {
		
		#region public constructors
	
		public ExcelAnalyser(Stream pStream) {
			this.Analyser(pStream);
		}

		public ExcelAnalyser(string FileName) 
		{
			this.AnalyserWithoutStream(FileName);
		}
		#endregion

		#region public override functions

		public override bool IsValidEmail(string pEmailToValid) {
			return base.IsValidEmail (pEmailToValid);
		}

		public override int GetNumberContact(System.Collections.IEnumerator pIEnumList) {
			return base.GetNumberContact (pIEnumList);
		}

		public override string GetEmail(string pCrtLine) {
			return base.GetEmail (pCrtLine);
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
							for(int j=0;j<oBlockLine.Length;j++) {
							
								if(this.IsValidEmail(oBlockLine[j]) && this.ContactManager.NotInTheList(oBlockLine[j])) {
									this.ContactManager.Add(new ContactInfo(oBlockLine[j].Substring(0,oBlockLine[j].IndexOf('@')),"",this.GetEmail(oBlockLine[j])));
									i++;
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

		#region public override attributes

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

		public override GA.BDC.Core.AddressBooks.Types.ContactInfo[] ContactList {
			get{ return base.ContactList; }
		}

		#endregion

		#region Private functions
		
		private string[] GetSheetName(OleDbConnection ExcelConnection)
		{
			DataTable ExcelSheets = ExcelConnection.GetOleDbSchemaTable(System.Data.OleDb.OleDbSchemaGuid.Tables,new object[] {null, null, null, "TABLE"});
			string[] result = new string[ExcelSheets.Rows.Count];
			for (int i=0; i< ExcelSheets.Rows.Count; i++)
			{
				result[i] = "["+ExcelSheets.Rows[i]["TABLE_NAME"].ToString()+"]";
			}
			return result;
		}

		private DataTable CreateDTFromASheet(OleDbConnection ExcelConnection, string query)
		{
			OleDbCommand oleCm = null;
			OleDbDataAdapter DA = null;
			DataSet DS = new DataSet ();
			try
			{
				oleCm = new OleDbCommand (query, ExcelConnection);
				oleCm.CommandType = CommandType.Text ;
				DA = new OleDbDataAdapter (oleCm);
				DA.Fill (DS);
			}
			catch (Exception)
			{
				return null;
			}
			finally
			{
				oleCm.Dispose ();
				DA.Dispose ();
			}
			if (DS.Tables.Count >0)
				return DS.Tables [0];
			return null;
		}

		private void AnalyserWithoutStream(string FileName) 
		{
			string resultFileName = DateTime.Now.Ticks.ToString();
            string connectionString = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=Excel 12.0;", FileName);
			OleDbConnection conn = new OleDbConnection (connectionString);
			conn.Open ();
			DataSet DS = new DataSet ();
			//
			try 
			{
				string[] tableNames =  GetSheetName(conn) ;//CreateSQLStatement(conn);
				for (int outerLoop=0; outerLoop <tableNames.Length ; outerLoop++)
				{
					DataTable dtTmp = CreateDTFromASheet(conn,string.Format ( "Select * from {0}", tableNames[outerLoop]));
					if (dtTmp != null)
					{
						DataTable dt = dtTmp.Copy ();
						dt.TableName = tableNames[outerLoop];
						for (int j=0; j < dt.Rows.Count; j++)
						{							
							string firstName = string.Empty;
							string lastName = string.Empty;
							string email = string.Empty;
							//
							if (dt.Columns.Count > 0 && !IsDBNull(dt.Rows[j][0]))
								firstName = (string)dt.Rows[j][0];
							if (dt.Columns.Count > 1 && !IsDBNull(dt.Rows[j][1]))
								lastName = (string)dt.Rows[j][1];
							if (dt.Columns.Count > 2 && !IsDBNull(dt.Rows[j][2]))
								email = (string)dt.Rows[j][2];

							if (firstName != string.Empty || lastName != string.Empty || email != string.Empty)
								this.ContactManager.Add (firstName ,lastName,email);
						}
					}
				}
			}
			catch(Exception ex){ throw ex; }
			finally
			{
				DS.Dispose ();
				conn.Close ();
			}
		}

		private bool IsDBNull(object obj)
		{
			return DBNull.Equals (obj, DBNull.Value );
		}

		#endregion
	}
}
