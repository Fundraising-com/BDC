using System;
using System.IO;
using System.Diagnostics;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;

namespace QSP.WebControl
{
	/// <summary>
	/// Zip/Unzip files.
	/// </summary>
	public class Zip
	{
        #region Constants
        protected const string zipPathKey = "ZipPath";
        protected const string unZipPathKey = "UnZipPath";
        #endregion

        #region Constructors
        public Zip()
		{
        }
        #endregion

        #region Properties
        public string ZipPath
		{
			get
			{
                return System.Configuration.ConfigurationManager.AppSettings[zipPathKey];
			}
		}

        public string UnZipPath
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings[unZipPathKey];
            }
        }
		#endregion

        #region Methods
        public void ZipFile(List<string> fileNames, string zipFileName)
		{
            string fileNameList = "";
            foreach (string fileName in fileNames)
            {
                fileNameList += " " + fileName;
            }
            
			Process zip = new Process();
			zip.StartInfo.FileName = ZipPath;
            zip.StartInfo.Arguments = zipFileName + " " + fileNameList;
			zip.Start();
			zip.WaitForExit();
			zip.Dispose();
		}

		public void UnZipFile(string fileName)
		{
			Process unzip = new Process();
			unzip.StartInfo.FileName = ZipPath;
			unzip.StartInfo.Arguments = fileName;
			unzip.Start();
			unzip.WaitForExit();
			unzip.Dispose();
        }
        #endregion
    }
}