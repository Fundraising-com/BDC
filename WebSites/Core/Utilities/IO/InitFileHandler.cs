/* Jean-Francois Buist
 * Utility to read and write .ini files.
 * 
 */

using System;
using System.Text;
using System.Runtime.InteropServices;

namespace GA.BDC.Core.Utilities.IO {

	public class IniFileHandler {
		private string path;

		[DllImport("kernel32")]
		private static extern long WritePrivateProfileString(string section,
			string key,string val,string filePath);
		[DllImport("kernel32")]
		private static extern int GetPrivateProfileString(string section,
			string key,string def, StringBuilder retVal,
			int size,string filePath);


		public IniFileHandler(string iniPath) {
			path = iniPath;
		}

		public void WriteValue(string section,string key,string val) {
			WritePrivateProfileString(section,key,val,this.path);
		}
        

		public string ReadValue(string section,string key) {
			StringBuilder temp = new StringBuilder(255);
			GetPrivateProfileString(section,key,"",temp, 255, this.path);
			return temp.ToString();
		}

		public string Path {
			set { path = value; }
			get { return path; }
		}
	}
}
