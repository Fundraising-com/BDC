using System;
using System.Data.SqlClient;
using System.IO;
using System.Diagnostics;
using QSPPDFMerger.Utility;

public class Merger
{
	private int orderid;
	private int rrbatchid;
	private bool allFilesFound;
	private string path;
	private string filename;
	private string concatFiles;
	private SqlCommand  cmdReports;
	private SqlDataReader myFileReader;
	private SqlCommand  cmdUpdate;
	

	public static void Main(string[] Args)
	{
		Merger m = new Merger();
		Arguments CommandLine = new Arguments(Args);
		if(CommandLine["orderid"] == null || CommandLine["moveto"] == null  || CommandLine["db"] == null) 
		{
			Console.WriteLine("ERROR: Parameter(s) missing");
			Console.WriteLine(@"usage: qsppdfmerger.exe --orderid 9999 --moveto D:\MyFolder\ --db [dev|3k117|test]");
		}
		else
			m.Run(CommandLine["orderid"], CommandLine["moveto"], CommandLine["db"]);
	}
	public void Run(string _orderid, string _moveto, string _db)
	{
		SqlConnection conn = new SqlConnection();
		SqlConnection conn2 = new SqlConnection();
		SqlConnection conn3 = new SqlConnection();
		
		
		if (_db == "dev")
		{
			conn.ConnectionString = "server=uspvl3k42-dev\QSPDEV.us.rdigest.com;uid=nhamel;pwd=nhamel;database=QSPCanadaOrderManagement;Persist Security Info=true;";
			conn2.ConnectionString = "server=uspvl3k42-dev\QSPDEV.us.rdigest.com;uid=nhamel;pwd=nhamel;database=QSPCanadaOrderManagement;Persist Security Info=true;";
			conn3.ConnectionString = "server=uspvl3k42-dev\QSPDEV.us.rdigest.com;uid=nhamel;pwd=nhamel;database=QSPCanadaOrderManagement;Persist Security Info=true;";
		} 
		else if (_db == "3k117")
		{
			conn.ConnectionString = "server=uspvl3k117.us.rdigest.com;uid=nhamel;pwd=nick;database=QSPCanadaOrderManagement;Persist Security Info=true;";
			conn2.ConnectionString = "server=uspvl3k117.us.rdigest.com;uid=nhamel;pwd=nick;database=QSPCanadaOrderManagement;Persist Security Info=true;";
			conn3.ConnectionString = "server=uspvl3k117.us.rdigest.com;uid=nhamel;pwd=nick;database=QSPCanadaOrderManagement;Persist Security Info=true;";
		
		} 
		else
		{
			conn.ConnectionString = "server=uspvl3k42-dev\QSPTEST.us.rdigest.com;uid=nhamel;pwd=nhamel;database=QSPCanadaOrderManagement;Persist Security Info=true;";
			conn2.ConnectionString = "server=uspvl3k42-dev\QSPDEV.us.rdigest.com;uid=nhamel;pwd=nhamel;database=QSPCanadaOrderManagement;Persist Security Info=true;";
			conn3.ConnectionString = "server=uspvl3k42-dev\QSPDEV.us.rdigest.com;uid=nhamel;pwd=nhamel;database=QSPCanadaOrderManagement;Persist Security Info=true;";
		} 

		SqlCommand  cmd = new SqlCommand("SELECT ID,BatchOrderID FROM ReportRequestBatch WHERE TypeID=1 AND BatchOrderID="+_orderid, conn);
		Console.WriteLine("connecting...");
		try
		{    	
			conn.Open();
			conn2.Open();
			conn3.Open();
			Console.WriteLine("connected...");
			SqlDataReader myReader = cmd.ExecuteReader();
			while (myReader.Read())
			{
				rrbatchid = myReader.GetInt32(0);
				orderid = myReader.GetInt32(1);
				allFilesFound = true;
				concatFiles = "";

				Console.WriteLine(orderid);
				cmdReports = new  SqlCommand("SELECT Path, Filename FROM vw_ReportRequestBatchFilename,reportrequestbatch where reportrequestbatchid=id and ReportRequestBatchID=" + rrbatchid, conn2);
				myFileReader = cmdReports.ExecuteReader();
				if(myFileReader.HasRows)
				{
					
					while (myFileReader.Read() && allFilesFound)
					{
						path = myFileReader.GetString(0);
						filename = myFileReader.GetString(1);

						if(!doesFileExist(path,filename))
						{
							allFilesFound = false;
						} 
						else 
						{
							concatFiles = concatFiles + " " + path + filename;

						}
					}
					
					
				}
				else{
					allFilesFound = false;
				}
				
				myFileReader.Close();

				if(allFilesFound)
				{
					//kick the job to merge

					Process pdftk = new Process();

					pdftk.StartInfo.FileName   = @"E:\projects\paylater\apps\pdftk.exe";
					pdftk.StartInfo.Arguments = concatFiles + @" cat output " + _moveto + orderid + ".pdf dont_ask";
					Console.WriteLine(@"E:\projects\paylater\apps\pdftk.exe " + concatFiles + @" cat output " + _moveto + orderid + ".pdf");
					pdftk.Start();
					
					cmdUpdate = new SqlCommand("UPDATE ReportRequestBatch SET IsMerged=1 WHERE ID=" + rrbatchid, conn3);
					cmdUpdate.ExecuteNonQuery();
					//Console.WriteLine();
				}
				
			}
		
			myReader.Close();
			conn.Close();
			conn2.Close();
			conn3.Close();
			Console.WriteLine("closed...");
		}
		catch(Exception e)
		{
			//Console.WriteLine("error...");
			conn.Close();
			conn2.Close();
			conn3.Close();
			Console.WriteLine("Exception Occured -->> {0}",e);
		}    	
	}

	private bool doesFileExist(string path, string filename)
	{
		string[] files = Directory.GetFiles(path);
		bool found;
		string fullpath;
		found=false;
		foreach (string file in files) 
		{

			fullpath = path + filename;
			if(file.ToUpper()==fullpath.ToUpper())
				found = true;
			
				
		}
		if (!found)
			Console.WriteLine( filename.ToUpper() +" not found");
		return found;
	}
}

