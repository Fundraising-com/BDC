using System;
using System.IO;
using System.Diagnostics;

namespace FileStore
{
	/// <summary>
	/// Summary description for PDFStore.
	/// </summary>
	[Serializable]
	public class PDFStore : Store
	{
		private const string MERGED_PDF_FILENAME = "MergedPDFs.pdf";
		private const int TIME_OUT_MS = 180000;

		public PDFStore() : base() { }

		public virtual string Merge() 
		{
			Process oProcess = new Process();
			//StreamReader oStreamOutput;

			InitializeProcess(oProcess);
			//oStreamOutput = oProcess.StandardOutput;

			try 
			{
				if(oProcess.Start()) 
				{
					oProcess.WaitForExit(TIME_OUT_MS);
					
					if(!oProcess.HasExited) 
					{
						throw new PDFMergeTimeOutException();
					}
				}
			} 
			catch
			{
				throw new PDFMergeFailedException();
				//TODO: Manage the process's output
			}

			return MERGED_PDF_FILENAME;
		}

		private void InitializeProcess(Process process) 
		{
			process.StartInfo.Arguments = this.StoreDirectory.FullName + "\\*.pdf cat output " + this.StoreDirectory.FullName + "\\" + MERGED_PDF_FILENAME;
			process.StartInfo.CreateNoWindow = true;
			process.StartInfo.FileName = System.Configuration.ConfigurationSettings.AppSettings["PDFTKPath"];
			//process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.UseShellExecute = false;
		}
	}
}
