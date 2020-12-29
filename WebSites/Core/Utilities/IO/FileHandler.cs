using System;
using System.IO;

namespace GA.BDC.Core.Utilities.IO {
	/*
	 * Created by:	Jean-Francois Buist.
	 * Date:		Novembre 2004.
	 * Version:		0.0
	 * 
	 */

	/// <summary>
	/// This class provides an easy way to play with files.
	/// </summary>
	/// <remarks>Don't forget to try/catch</remarks>
	public class FileHandler {

		public FileHandler() {
			//
			// TODO: Add constructor logic here
			//
		}

		#region Static Methods
		/// <summary>
		/// Reads a file
		/// </summary>
		/// <param name="fileName">The path of the file</param>
		/// <returns>The file content</returns>
		/// <exception cref="">System.Exeption</exception>
		public static string ReadFile(string fileName) {
			FileStream file = null;
			StreamReader reader = null;
			string fileContent = "";

			try {
				file = new FileStream(fileName, System.IO.FileMode.Open);
				reader = new StreamReader(file);
				fileContent = reader.ReadToEnd();
			} catch(System.Exception ex) {
				throw ex;
			} finally {
				if(reader != null) { reader.Close(); }
				if(file != null) { file.Close(); }
			}
			return fileContent;
		}

		/// <summary>
		/// Reads a file
		/// </summary>
		/// <param name="fileName">The path of the file</param>
		/// <returns>The file content</returns>
		/// <exception cref="">System.Exeption</exception>
		public static string ReadOnlyFile(string fileName) 
		{

			FileStream file = null;
			StreamReader reader = null;
			string fileContent = "";

			try 
			{
				file = File.OpenRead(fileName);
				reader = new StreamReader(file);
				fileContent = reader.ReadToEnd();
			} 
			catch(System.Exception ex) 
			{
				throw ex;
			} 
			finally 
			{
				if(reader != null) { reader.Close(); }
				if(file != null) { file.Close(); }
			}
			return fileContent;
		}

		/// <summary>
		/// Write data into file
		/// </summary>
		/// <param name="filename">File name</param>
		/// <param name="content">Content</param>
		/// <param name="fm">File Mode</param>
		/// <exception cref="">System.Exception</exception>
		public static void WriteFile(string filename, string content, FileMode fm) {
			FileStream file = null;
			StreamWriter sw = null;

			try {
				file = new FileStream(filename, fm);
				sw = new StreamWriter(file);
				sw.Write(content);
			} catch(System.Exception ex) {
				throw ex;
			} finally {
				if(sw != null) { sw.Close(); }
				if(file != null) { file.Close(); }
			}
		}

		/// <summary>
		/// Create a file if and only if the file doesn't exists, then print data
		/// ** See CreateFileAndWriteData()
		/// </summary>
		/// <param name="filename">File path</param>
		/// <param name="content">Content</param>
		/// <exception cref="">System.Exception</exception>
		public static void CreateNewFile(string filename, string content) {
			WriteFile(filename, content, FileMode.CreateNew);
		}

		/// <summary>
		/// Create a file if and only if the file doesn't exists, then print data
		/// ** See CreateFileAndWriteData()
		/// </summary>
		/// <param name="filename">File path</param>
		/// <param name="content">Binary Content</param>
		/// <exception cref="">System.Exception</exception>
		public static void CreateNewFile(string filename, byte[] contents, FileMode fm) 
		{
			
			FileStream file = null;
			BinaryWriter sw = null;

			try 
			{
				file = new FileStream(filename, fm);
				sw = new BinaryWriter(file);
				sw.Write(contents);
			} 
			catch(System.Exception ex) 
			{
				throw ex;
			} 
			finally 
			{
				if(sw != null) { sw.Close(); }
				if(file != null) { file.Close(); }
			}
		}

		/// <summary>
		/// Create a file if and only if the file doesn't exists, then print data
		/// ** See CreateFileAndWriteData()
		/// </summary>
		/// <param name="filename">File path</param>
		/// <param name="content">Binary Content</param>
		/// <exception cref="">System.Exception</exception>
		public static void CreateNewFile(string filename, byte[] contents) 
		{
			CreateNewFile(filename, contents, FileMode.CreateNew);
		}

		/// <summary>
		/// If the file already exists, it will delete it and recreate it using the new content
		/// </summary>
		/// <param name="filename"></param>
		/// <param name="content"></param>
		/// <remarks>IF THE FILE EXISTS, IT WILL DELETE IT.</remarks>
		public static void CreateFileAndWriteData(string filename, string content) {
			WriteFile(filename, content, FileMode.Create);
		}

		/// <summary>
		/// Append text to a file
		/// </summary>
		/// <param name="filename">File name</param>
		/// <param name="content">File Content</param>
		/// <exception cref="">System.Exception</exception>
		public static void AppendToFile(string filename, string content) {
			WriteFile(filename, content, FileMode.Append);
		}

		/// <summary>
		/// Replace data into a file
		/// </summary>
		/// <param name="filename">Filename</param>
		/// <param name="oldString">Data to replace</param>
		/// <param name="newString">New data</param>
		/// <param name="caseSensitive">Is Case Sensitive?</param>
		/// <exception cref="">System.Exception</exception>
		public static void ReplaceIntoFile(string filename, string oldString, string newString,
			bool caseSensitive) {
			string content = ReadFile(filename);
			if(caseSensitive) {
				content = content.Replace(oldString, newString);
			} else {
				int index = -1;
				do {
					index = content.ToLower().IndexOf(oldString.ToLower());
					if(index > -1) {
						content = content.Remove(index, oldString.Length);
						content = content.Insert(index, newString);
					}
				}while(index > -1);
			}
			CreateFileAndWriteData(filename, content);
		}
		#endregion
	}
}
