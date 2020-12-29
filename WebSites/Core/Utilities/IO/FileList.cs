using System;
using System.Collections;

namespace GA.BDC.Core.FileFinder {
	/// <summary>
	/// Summary description for FileList.
	/// </summary>
	public class FileList {
		public FileList() {
			//
			// TODO: Add constructor logic here
			//
		}

		private void GetFilesFromDirectory(ref ArrayList files, string directory) {
			string[] filesInDir = System.IO.Directory.GetFiles(directory);
			foreach(string file in filesInDir) {
				files.Add(file);
			}
			string[] directories = System.IO.Directory.GetDirectories(directory);
			foreach(string dir in directories) {
				GetFilesFromDirectory(ref files, dir);
			}
		}

		public string[] GetFilesFromDirectory(string directory) {
			ArrayList files = new ArrayList();
			GetFilesFromDirectory(ref files, directory);
			string[] rfiles = new string[files.Count];
			for(int i=0;i<files.Count;i++) {
				rfiles[i] = (string)files[i];
			}
			return rfiles;
		}
	}
}
