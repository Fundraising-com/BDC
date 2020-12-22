namespace QSPForm.Data
{
	using System;
	using System.Data.OleDb;
	using System.Data;
	using System.Data.Common;
	using System.IO;
	
	/// <summary>
	/// Summary description for ImportData.
	/// </summary>
	public class DataTransfer : DBInteractionBase
	{
		
		public enum TransferType
		{
			Excel,
			Text
			// Add more here (check the comma's!)
		}
		
		public DataTransfer()
		{
			//
			// TODO: Add constructor logic here
			//
		}		

		public DataTable ImportFromExcel(String sFilePath, String sheetName, bool FirstRowIsHeader)
		{			
			String sConnStr_Excel = QSPForm.Common.QSPFormConfiguration.ExcelConnectionString;
			String sIsHeader = "Yes"; //By Default
			sConnStr_Excel = sConnStr_Excel.Replace("[DataSource]", sFilePath);
			if (!FirstRowIsHeader)
				sIsHeader = "No";
			sConnStr_Excel = sConnStr_Excel.Replace("[Header]", sIsHeader);

			DataTable dt = new DataTable(sheetName);
			try
			{				
				OleDbConnection cnnExcel = new OleDbConnection(sConnStr_Excel);
				String cmdTextExcel= "Select *  FROM ["+ sheetName +"$]";
				OleDbCommand cmdExcel = new OleDbCommand(cmdTextExcel, cnnExcel);
				OleDbDataAdapter da = new OleDbDataAdapter(cmdExcel);
				da.Fill(dt);				
			}
			catch (Exception ex)
			{
				throw ex;
			}
			return dt;
		}

		public DataTable ImportFromText(string sFilePath, bool FirstRowIsHeader)
		{	
		
			String sConnStr_Text = QSPForm.Common.QSPFormConfiguration.TxtConnectionString;
			String sIsHeader = "Yes"; 
			string sTempFileName = sFilePath;
			string sFileName = System.IO.Path.GetFileName(sFilePath);
			string sPath =sTempFileName.Remove(sTempFileName.LastIndexOf("\\")+1,sFileName.Length);
			
			sConnStr_Text =ReplaceFMT(sFileName,sConnStr_Text);

			sConnStr_Text = sConnStr_Text.Replace("[DataSource]",sPath);
			
			if (!FirstRowIsHeader)
				sIsHeader = "No";
			
			sConnStr_Text = sConnStr_Text.Replace("[Header]", sIsHeader);
						
			CreateSchemaINI(sFileName,sPath);
			DataTable dt = new DataTable(sFileName);
			try
			{				
				OleDbConnection cnnText = new OleDbConnection(sConnStr_Text);
				String cmdTextSelect= "Select *  FROM " + sFileName;
				OleDbCommand cmdText = new OleDbCommand(cmdTextSelect, cnnText);
				OleDbDataAdapter da = new OleDbDataAdapter();
				da.SelectCommand = cmdText;
				da.Fill(dt);				
			}
			catch
			{
				
			}
			finally
			{
				DeleteSchemaINI(sPath);
			}
			
			
			return dt;
		}

		private void CreateSchemaINI(string FileName,string Path)
		{
			StreamWriter FileOutput = new StreamWriter(Path+"schema.ini");
			FileOutput.WriteLine("["+FileName+"]");
			FileOutput.WriteLine("ColNameHeader=False");
			
			if(System.IO.Path.GetExtension(FileName).ToLower().Equals(".csv"))
				FileOutput.WriteLine("Format=CSVDelimited");
			else
				FileOutput.WriteLine("Format=TabDelimited");
			FileOutput.WriteLine("MaxScanRows=1");
			FileOutput.WriteLine("CharacterSet=ANSI");
			FileOutput.Close();

		}
		private void DeleteSchemaINI(string Path)
		{
			System.IO.File.Delete(Path+"schema.ini");
		}

		private string ReplaceFMT(string sFileName,string sConnStr_Text)
		{
			if(System.IO.Path.GetExtension(sFileName).ToLower().Equals(".csv"))
				return sConnStr_Text.Replace("[Type]","Delimited");

			else
				return sConnStr_Text.Replace("[Type]","TabDelimited");
		}

	}
}
