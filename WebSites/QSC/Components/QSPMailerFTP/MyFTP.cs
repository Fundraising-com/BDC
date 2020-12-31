using System;
using System.IO;
using System.Diagnostics;
using QSPMailerFTP;
using QSPMailerFTP.Utility;
using System.Threading;

namespace QSPMailerFTP
{
	/// <summary>
	/// Summary description for MyFTP.
	/// </summary>
	public class MyFTP
	{
		private string local_download_to_dir;
		private string local_upload_from_dir;
		private string remote_download_from_dir;
		private string remote_upload_to_dir;
		private string archieve_dir;
		private string file_ext;
		private string del_after_download;
		private string zip_before_upload;
		private string unzip_after_download;
		private string winzip_dir;

		private string error_mail_from;
		private string success_mail_from;
		private string error_mail_to;
		private string success_mail_to;
		private string smtp;

		public MyFTP(string config)
		{
			read_ini_settings(config);
		}

		private void read_ini_settings(string config)
		{
			// get the configuration file
			FileInfo config_file = new FileInfo(config);
			
			if(config_file.Exists)
			{
				// read configuration values
				// Create an instance of StreamReader to read from a file.
				// The using statement also closes the StreamReader.
				using(StreamReader stream_reader = new StreamReader(config_file.FullName)) 
				{

					string section = "[]";
					string line;
					// Read lines from the file until the end of 

					// the file is reached.
					while((line = stream_reader.ReadLine()) != null)
					{

						// set the current section name
						if(line.StartsWith("[") && line.EndsWith("]") && line != section)
						{

							section = line.ToUpper();
						}
						if(section == "[APPLICATIONINFO]")
						{

							// assign keywords from this section
							if(line.ToUpper().StartsWith("LOCAL_DOWNLOAD_TO_DIR=") && line.Length > 22)
							{
								
								local_download_to_dir = line.Substring(22);
							}
							else if(line.ToUpper().StartsWith("LOCAL_UPLOAD_FROM_DIR=") && line.Length > 22)
							{

								local_upload_from_dir = line.Substring(22);
							}
							else if(line.ToUpper().StartsWith("REMOTE_DOWNLOAD_FROM_DIR=") && line.Length > 25)
							{

								remote_download_from_dir = line.Substring(25);
							}
							else if(line.ToUpper().StartsWith("REMOTE_UPLOAD_TO_DIR=") && line.Length > 21)
							{

								remote_upload_to_dir = line.Substring(21);
							}
							else if(line.ToUpper().StartsWith("ARCHIEVE_DIR=") && line.Length > 13)
							{
								DateTime today;
								today = System.DateTime.Today;
								archieve_dir = line.Substring(13);
								if(!archieve_dir.Trim().EndsWith(@"\"))
									archieve_dir = archieve_dir.Trim() + @"\";
								archieve_dir = archieve_dir	+ String.Format("{0:yyyy_MM_dd}", today) + @"\";
							}
							else if(line.ToUpper().StartsWith("FILE_EXT=") && line.Length > 9)
							{

								file_ext = line.Substring(9);
							}
							else if(line.ToUpper().StartsWith("DELETE_AFTER_DOWNLOAD=") && line.Length > 22)
							{

								del_after_download = line.Substring(22);
							}
							else if(line.ToUpper().StartsWith("ZIP_BEFORE_UPLOAD=") && line.Length > 18)
							{

								zip_before_upload = line.Substring(18);
							}
							else if(line.ToUpper().StartsWith("UNZIP_AFTER_DOWNLOAD=") && line.Length > 21)
							{

								unzip_after_download = line.Substring(21);
							}
							else if(line.ToUpper().StartsWith("WINZIP_DIR=") && line.Length > 11)
							{

								winzip_dir = line.Substring(11);
							}
							else if(line.ToUpper().StartsWith("ERROR_MAIL_FROM=") && line.Length > 16) 
							{
								error_mail_from = line.Substring(16);
							}
							else if(line.ToUpper().StartsWith("SUCCESS_MAIL_FROM=") && line.Length > 18) 
							{
								success_mail_from = line.Substring(18);
							}
							else if(line.ToUpper().StartsWith("ERROR_MAIL_TO=") && line.Length > 14) 
							{
								error_mail_to = line.Substring(14);
							}
							else if(line.ToUpper().StartsWith("SUCCESS_MAIL_TO=") && line.Length > 16) 
							{
								success_mail_to = line.Substring(16);
							}
							else if(line.ToUpper().StartsWith("SMTP=") && line.Length > 5) 
							{
								smtp = line.Substring(5);
							}
						}
					}
				}
			}
			else
			{
				throw new FileNotFoundException("INI file is missing.", config);
			}
		}

		public string getLocalFromDir()
		{
			if(!local_upload_from_dir.Trim().EndsWith(@"\"))
				local_upload_from_dir = local_upload_from_dir.Trim() + @"\";
			return local_upload_from_dir;
		}

		public string getLocalToDir()
		{
			if(!local_download_to_dir.Trim().EndsWith(@"\"))
				local_download_to_dir = local_download_to_dir.Trim() + @"\";
			return local_download_to_dir;
		}
		public string getRemoteFromDir()
		{
			return remote_upload_to_dir.Trim();
		}

		public string getRemoteToDir()
		{
			return remote_download_from_dir.Trim();
		}

		public string getArchieveDir()
		{
			
			return archieve_dir;
		}

		public string getFileExt()
		{
			if (file_ext == null)
				file_ext = "";
			if (file_ext.Trim()=="")
				file_ext="*.*";
			return file_ext.Trim();
		}

		public bool deleteAfterDowload()
		{
			if(del_after_download.Trim() =="1")
				return true;
			return false;
		}

		public bool zipBeforeUpload()
		{
			if(zip_before_upload.Trim() =="1")
			 return true;
			return false;
		}

		public bool unzipAfterDownload()
		{
			if(unzip_after_download.Trim() =="1")
				return true;
			return false;
		}

		public string getWinzipDir()
		{
			if(!winzip_dir.Trim().EndsWith(@"\"))
				winzip_dir = winzip_dir.Trim() + @"\";
			return winzip_dir;
		}

		public string ErrorMailFrom 
		{
			get 
			{
				return error_mail_from.Trim();
			}
		}

		public string SuccessMailFrom 
		{
			get 
			{
				return success_mail_from.Trim();
			}
		}

		public string ErrorMailTo 
		{
			get 
			{
				return error_mail_to.Trim();
			}
		}

		public string SuccessMailTo 
		{
			get 
			{
				return success_mail_to.Trim();
			}
		}

		public string Smtp 
		{
			get 
			{
				return smtp.Trim();
			}
		}

		public static void appKill(Object state) 
		{
			SystemException ex = new SystemException("Application timed out.");
			ApplicationError.ManageError(ex);
			Console.WriteLine("Caught Error : Application timed out.");
			Process.GetCurrentProcess().Kill();
		}

		public static void Main(string[] Args) 
		{
			// If the application hangs, then kill the application after 5 minutes
			TimerCallback tc = new TimerCallback(appKill);
			Timer appKillTimer = new Timer(tc, null, 300000, 300000);

			//	Args = new string[] {@"--config", @"e:\projects\paylater\apps\QSPCanadaFTP.ini","--host", "10.100.106.25", "--user", "qsp", "--pass","qspp@y"};
			string zip_file;

			try 
			{
				Arguments CommandLine = new Arguments(Args);
				if(CommandLine["config"] == null)
				{		
					Console.WriteLine("ERROR: Parameter(s) missing");
					Console.WriteLine(@"usage: QSPMailerFTP.exe --host 10.100.106.25 --user myusername --pass mypassword --config d:\mydir\QSPMailerFTP.ini");
					throw new ArgumentException("\"config\" parameter missing.");
				}
				else
				{
					MyFTP ftp_config = new MyFTP(CommandLine["config"]);
				
					ApplicationError.MailFrom = ftp_config.ErrorMailFrom;
					ApplicationError.MailTo = ftp_config.ErrorMailTo;
					ApplicationError.Smtp = ftp_config.Smtp;

					if(CommandLine["host"] == null || CommandLine["user"] == null  || CommandLine["pass"] == null || CommandLine["config"] == null) 
					{
						Console.WriteLine("ERROR: Parameter(s) missing");
						Console.WriteLine(@"usage: QSPMailerFTP.exe --host 10.100.106.25 --user myusername --pass mypassword --config d:\mydir\QSPMailerFTP.ini");

						if(CommandLine["host"] == null) 
						{
							throw new ArgumentException("\"host\" parameter missing.");
						} 
						else if(CommandLine["user"] == null) 
						{
							throw new ArgumentException("\"user\" parameter missing.");
						} 
						else if(CommandLine["pass"] == null) 
						{
							throw new ArgumentException("\"pass\" parameter missing.");
						} 
						else if(CommandLine["config"] == null) 
						{
							throw new ArgumentException("\"config\" parameter missing.");
						}
					}
					//Check for Winzip dir if "zip" or "unzip" feature is set to true
					else if((ftp_config.zipBeforeUpload() || ftp_config.unzipAfterDownload()) && (ftp_config.getWinzipDir() == null ||ftp_config.getWinzipDir() == ""))
					{
						Console.WriteLine("ERROR: If you want to zip or unzip you need to provide the winzip directoy in the INI file.");
						throw new ArgumentException("The WinZip directory is needed but is not provided in the INI file.");
					}
					else
					{
						

						Console.WriteLine("Creating archieve directory: " + ftp_config.getArchieveDir() + "\n");
						Directory.CreateDirectory(ftp_config.getArchieveDir());

						Console.WriteLine("Connecting...");

						FTPFactory ff = new FTPFactory();
						ff.setDebug(true);
						ff.setRemoteHost(CommandLine["host"]);
						ff.setRemoteUser(CommandLine["user"]);
						ff.setRemotePass(CommandLine["pass"]);
						ff.login();
					
						//Change directory to "download from" folder on ftp server
						ff.chdir(ftp_config.getRemoteToDir());

						//List all files in directory
						ff.setBinaryMode(true);
						string[] fileNames = ff.getFileList(ftp_config.getFileExt());
						ff.setBinaryMode(true);
						//ff.download("*.xml");
						for(int i=0;i < fileNames.Length;i++) 
						{
							Console.WriteLine(fileNames.Length.ToString());
							Console.WriteLine('=' + fileNames[i] + '=');

							if(fileNames[i] != "" && fileNames[i] !="\n")
							{
								//Dowload file
								ff.download(fileNames[i].Replace("\r",""), ftp_config.getLocalToDir() + fileNames[i]);
								
								//Delete file on server after download if necessary
								if(fileNames[i] != "" && fileNames[i] !="\n")
									ff.deleteRemoteFile(fileNames[i].Replace("\r",""));

								//Unzip the file if necessary
								if(fileNames[i].Length > 4) //needs to be more than 4 char long cause ".zip"=4 char
								{
									if(ftp_config.unzipAfterDownload() && fileNames[i].Substring(fileNames[i].Length -4,4) == ".zip")
									{
										Process unzip = new Process();

										unzip.StartInfo.FileName   = ftp_config.getWinzipDir()+ "wzunzip.exe";
										unzip.StartInfo.Arguments = ftp_config.getLocalToDir() + fileNames[i] + " " + ftp_config.getLocalToDir();
										unzip.Start();
										unzip.WaitForExit();
										unzip.Dispose(); 
									}
								}
							}

							
						}
					
						

						//Change direcotry to "upload to" folder on ftp server
						ff.chdir(ftp_config.getRemoteFromDir());

						//Get files list on local machine
						string[] files = Directory.GetFiles("E:\\", "*.*");
						foreach (string file in files) 
						{
							//Zip the files if need to be before upload
							if(ftp_config.zipBeforeUpload())
							{
								zip_file = file + ".zip";
								//zip
								Process zip = new Process();

								zip.StartInfo.FileName   = ftp_config.getWinzipDir()+ "wzzip.exe";
								zip.StartInfo.Arguments = zip_file + " " + file;
								zip.Start();
								zip.WaitForExit();
								zip.Dispose(); 

								//upload
								ff.upload(zip_file);
								
								//Once uploaded archieve the file
								if(Directory.Exists(ftp_config.getArchieveDir() + zip_file.Substring(ftp_config.getLocalToDir().Length - 2)))
								{
									Directory.Delete(ftp_config.getArchieveDir() + zip_file.Substring(ftp_config.getLocalToDir().Length - 2));
								}

								Directory.Move(zip_file,ftp_config.getArchieveDir() + zip_file.Substring(ftp_config.getLocalToDir().Length - 2));
							}
							else
							{
								//upload all files
								ff.upload(file);
							}
							//Once uploaded archieve the file
							if(Directory.Exists(ftp_config.getArchieveDir() + file.Substring(ftp_config.getLocalToDir().Length - 2)))
							{
								Directory.Delete(ftp_config.getArchieveDir() + file.Substring(ftp_config.getLocalToDir().Length - 2));
							}

							Directory.Move(file,ftp_config.getArchieveDir() + file.Substring(ftp_config.getLocalToDir().Length - 2));

						}
		
					
						ff.close();

						new SendMail(MailTypes.CmdLineApplication, String.Empty, ftp_config.SuccessMailFrom, ftp_config.SuccessMailTo, ftp_config.Smtp, "QSPMailerFTP has run with success.");
					}
				}

			}
			catch(Exception ex) 
			{
				ApplicationError.ManageError(ex);
				Console.WriteLine("Caught Error :"+ex.Message);
			}
		}
	}
}