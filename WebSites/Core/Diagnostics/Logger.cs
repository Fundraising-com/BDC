//
// 2005-05-15 - Stephen Lim - New class.
//

using System;
using System.Collections;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Security.Principal;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Windows.Forms;
using GA.BDC.Core.Collections;
using GA.BDC.Core.Configuration;
using GA.BDC.Core.Email;


namespace GA.BDC.Core.Diagnostics
{
	/// <summary>
	/// Generic log facility.
	/// </summary>
	public class Logger
	{
		#region Fields
		protected static int BulkMaxInterval = 0;
		protected static int BulkMaxLog = 0;
		protected static int BulkErrorLogCount = 0;
		protected static int BulkInfoLogCount = 0;
		protected static int BulkWarnLogCount = 0;
		protected static int BulkDebugLogCount = 0;
		protected static Log LastErrorLog = null;
		protected static Log LastInfoLog = null;
		protected static Log LastDebugLog = null;
		protected static Log LastWarnLog = null;
		#endregion

		#region Helper Methods
		/// <summary>
		/// Get the full physical path for either Web or Windows application. This method also expands custom variables
		/// such as %windir%, %System%, %temp%, etc.
		/// </summary>
		/// <param name="path">Absolute or relative file path. If an absolute path is provided,
		/// then the method does nothing and simply returns this path.</param>
		/// <returns>The absolute file path that corresponds to a Web or Windows application.</returns>
		protected static string GetFullPhysicalPath(string path)
		{
			string physPath = "";

			// Expand environment variables like %windir%, %systemdrive%, %systemroot%
			path = Environment.ExpandEnvironmentVariables(path);
			
			// Expand custom variables
			path = path.Replace("%yy%", DateTime.Now.ToString("yy"));		// Year
			path = path.Replace("%yyyy%", DateTime.Now.ToString("yyyy"));	// Year
			path = path.Replace("%MM%", DateTime.Now.ToString("MM"));		// Month
			path = path.Replace("%dd%", DateTime.Now.ToString("dd"));		// Day
			path = path.Replace("%HH%", DateTime.Now.ToString("HH"));		// 24-hour
			path = path.Replace("%mm%", DateTime.Now.ToString("mm"));		// Minute
			path = path.Replace("%ss%", DateTime.Now.ToString("ss"));		// Second
			path = path.Replace("%ApplicationData%", Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData));
			path = path.Replace("%CommonApplicationData%", Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData));
			path = path.Replace("%CommonProgramFiles%", Environment.GetFolderPath(Environment.SpecialFolder.CommonProgramFiles));
			path = path.Replace("%DesktopDirectory%", Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory));
			path = path.Replace("%LocalApplicationData%", Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData));
			path = path.Replace("%Personal%", Environment.GetFolderPath(Environment.SpecialFolder.Personal));
			path = path.Replace("%ProgramFiles%", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));
			path = path.Replace("%System%", Environment.GetFolderPath(Environment.SpecialFolder.System));

			try 
			{
				// Get web application path
				physPath = HttpContext.Current.Request.PhysicalApplicationPath;
			}
			catch {}

			try 
			{
				// Get windows application path
				if (physPath == "")
					physPath = Application.StartupPath;
			}
			catch {}

			return Path.Combine(physPath, path);
		}

		/// <summary>
		/// Convert XML into nice HTML format.
		/// </summary>
		/// <param name="xmlString">Xml string.</param>
		/// <returns>Html string.</returns>
		protected static string HtmlFormat(string xmlString)
		{
			// Bold the element tags
			xmlString = HttpUtility.HtmlEncode(xmlString);
			xmlString = xmlString.Replace(" ", "&nbsp;");
			xmlString = xmlString.Replace("\r\n", "<br>");
			xmlString = Regex.Replace(xmlString, @"([.\-\w]+?)=&quot;(.*?)&quot;", @"<br><font color=""blue"">$1</font>=&quot;<b>$2</b>&quot;", RegexOptions.Singleline);
			xmlString = Regex.Replace(xmlString, @"&lt;(/?)([\-\w]+)", @"<br>&lt;$1<font color=""blue"">$2</font>", RegexOptions.Singleline);
			xmlString = "<font size=\"2\" face=\"arial, verdana\">" + xmlString + "</font>";

			// Display any xml elements inside text value.
			xmlString = xmlString.Replace("&amp;lt;", @"&lt;");
			xmlString = xmlString.Replace("&amp;gt;", @"&gt;");
			xmlString = xmlString.Replace("&amp;quot;", @"""");


			return xmlString;													 
		}


		/// <summary>
		/// Convert XML into nice text format.
		/// </summary>
		/// <param name="xmlString">Xml string.</param>
		/// <returns>Html string.</returns>
		protected static string TextFormat(string xmlString)
		{
			// Bold the element tags
			xmlString = Regex.Replace(xmlString, @"([.\-\w]+?)=""(.*?)""", "\r\n$1=\"$2\"", RegexOptions.Singleline);
			xmlString = Regex.Replace(xmlString, @"<(/?)([\-\w]+)", "\r\n<$1$2", RegexOptions.Singleline);
			return xmlString;													 
		}
		#endregion

		#region Log Methods
		/// <summary>
		/// Log to all specified targets in config file.
		/// </summary>
		/// <param name="log">Log object.</param>
		protected static void Log(Log log)
		{
            var section = ConfigurationSettings.GetConfig("applicationSettings") as MultiNameValueCollection;
		    if (section == null)
		    {
		        return; //security checkup for newer versions
		    }
			bool isBulk = Logger.IsBulk(log);

			// Log to each defined target depending on level.
			switch (log.Level)
			{
				case LogLevel.Error:
					for (int i = 0; i < ApplicationSettings.GetConfig().GetCount("Common.Log.Error"); i++)
					{
						LogTarget t = new LogTarget(ApplicationSettings.GetConfig()["Common.Log.Error", i, "logType"], 
							ApplicationSettings.GetConfig()["Common.Log.Error", i, "logSource"]);

						if (ApplicationSettings.GetConfig()["Common.Log.Error", i, "onBulk"] == null ||
							ApplicationSettings.GetConfig()["Common.Log.Error", i, "onBulk"] != "discard" ||
							! isBulk)
						{
							Log(t, log);	
						}						
					}

					// Store last log
					Logger.LastErrorLog = log;

					break;
				case LogLevel.Info:
					for (int i = 0; i < ApplicationSettings.GetConfig().GetCount("Common.Log.Info"); i++)
					{
						LogTarget t = new LogTarget(ApplicationSettings.GetConfig()["Common.Log.Info", i, "logType"], 
							ApplicationSettings.GetConfig()["Common.Log.Info", i, "logSource"]);

						if (ApplicationSettings.GetConfig()["Common.Log.Info", i, "onBulk"] == null ||
							ApplicationSettings.GetConfig()["Common.Log.Info", i, "onBulk"] != "discard" ||
							! isBulk)
						{
							Log(t, log);	
						}	
					}

					// Store last log
					Logger.LastInfoLog = log;

					break;
				case LogLevel.Warn:
					for (int i = 0; i < ApplicationSettings.GetConfig().GetCount("Common.Log.Warn"); i++)
					{
						LogTarget t = new LogTarget(ApplicationSettings.GetConfig()["Common.Log.Warn", i, "logType"], 
							ApplicationSettings.GetConfig()["Common.Log.Warn", i, "logSource"]);

						if (ApplicationSettings.GetConfig()["Common.Log.Warn", i, "onBulk"] == null ||
							ApplicationSettings.GetConfig()["Common.Log.Warn", i, "onBulk"] != "discard" ||
							! isBulk)
						{
							Log(t, log);	
						}	
					}

					// Store last log
					Logger.LastWarnLog = log;

					break;
				case LogLevel.Debug:
					for (int i = 0; i < ApplicationSettings.GetConfig().GetCount("Common.Log.Debug"); i++)
					{
						LogTarget t = new LogTarget(ApplicationSettings.GetConfig()["Common.Log.Debug", i, "logType"], 
							ApplicationSettings.GetConfig()["Common.Log.Debug", i, "logSource"]);

						if (ApplicationSettings.GetConfig()["Common.Log.Debug", i, "onBulk"] == null ||
							ApplicationSettings.GetConfig()["Common.Log.Debug", i, "onBulk"] != "discard" ||
							! isBulk)
						{
							Log(t, log);	
						}	
					}

					// Store last log
					Logger.LastDebugLog = log;

					break;
			}
		}


		/// <summary>
		/// Log to specify target.
		/// </summary>
		/// <param name="target">Target object.</param>
		/// <param name="log">Log object.</param>
		protected static void Log(LogTarget target, Log log)
		{
			// Call the appropriate publishing facility.
			switch (target.LogType)
			{
				case LogType.Email:
					PublishToEmail(target.LogSource, log);
					break;
				case LogType.EventLog:
					PublishToEventLog(target.LogSource, log);
					break;
				case LogType.File:
					PublishToFile(target.LogSource, log);
					break;
			}
		}
		#endregion

		#region File Methods
		/// <summary>
		/// Publish log to an XML file.
		/// </summary>
		/// <param name="filename">Filename.</param>
		/// <param name="log">Log object.</param>
		protected static void PublishToFile(string filename, Log log)
		{
			FileStream fs = null;

			try 
			{
				// Get full path
				fs = File.Open(GetFullPhysicalPath(filename), FileMode.OpenOrCreate, FileAccess.ReadWrite);
			
				// Read backwards from EOF and look for </Logs> closing tag
				long maxPos = fs.Seek(0, SeekOrigin.End);
				long currentPos = fs.Position;
				bool foundEndTag = false;
				byte[] endTagHolder = new byte[7];

				if (currentPos > 7)
				{
					currentPos = fs.Seek(-7, SeekOrigin.Current);
			
					while (currentPos >= 0)
					{				
						if (fs.Read(endTagHolder, 0, 7) == 7)
						{
							// Move pointer back after reading
							currentPos = fs.Seek(-7, SeekOrigin.Current);

							if (endTagHolder[0] == 0x3C &&	// <
								endTagHolder[1] == 0x2F &&	// /
								endTagHolder[2] == 0x4C &&	// L
								endTagHolder[3] == 0x6F &&	// o
								endTagHolder[4] == 0x67 &&	// g
								endTagHolder[5] == 0x73 &&	// s
								endTagHolder[6] == 0x3E)	// >
							{
								foundEndTag = true;
								break;
							}	
						}

						// Do not read past 1KB worth of data looking for the end tag.
						if (currentPos == 0 || maxPos - currentPos > 1024)
							break;				
						else
						{
							// Still not found move pointer one byte backward
							currentPos = fs.Seek(-1, SeekOrigin.Current);
						}
					}
				}

				byte[] bText = null;
				string text = log.ToXmlString(false) + "\r\n</Logs>\r\n";

				if (! foundEndTag)
				{
					// In case there is some content in file, we just want to append
					// to avoid overwriting existing data. So just move pointer to EOF here.
					fs.Seek(0, SeekOrigin.End);

					// Write XML header since this appears to be a new file.
					text = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>\r\n<Logs>\r\n" + text;
				}

				// Write log entry
				bText = Encoding.UTF8.GetBytes(text);
				fs.Write(bText, 0, bText.Length);
			}
			catch {}
			finally 
			{
				try 
				{
					if (fs != null)
						fs.Close();
				}
				catch {}
			}
		}
		#endregion

		#region EventLog Methods
		/// <summary>
		/// Publish to Windows EventLog.
		/// </summary>
		/// <param name="source">Source name. The default source name is "Application".</param>
		/// <param name="log">Log object.</param>
		/// <remarks>
		/// ASP.NET, by default, cannot write to EventLog if the source name does not correspond
		/// to an existing source that already exists in the EventLog because ASP.NET does not 
		/// have the permission to write to the registry.
		/// <see cref="http://support.microsoft.com/default.aspx?scid=KB;EN-US;Q329291"/>
		/// </remarks>
		protected static void PublishToEventLog(string source, Log log)
		{
			try 
			{
				if (source == "")
					source = "Application";

				switch (log.Level)
				{
					case LogLevel.Error:
						EventLog.WriteEntry(source, "\r\n" + TextFormat(log.ToXmlString(true)), EventLogEntryType.Error);
						break;
					case LogLevel.Info:
						EventLog.WriteEntry(source, "\r\n" + TextFormat(log.ToXmlString(true)), EventLogEntryType.Information);
						break;
					case LogLevel.Warn:
						EventLog.WriteEntry(source, "\r\n" + TextFormat(log.ToXmlString(true)), EventLogEntryType.Warning);
						break;
					case LogLevel.Debug:
						EventLog.WriteEntry(source, "\r\n" + TextFormat(log.ToXmlString(true)), EventLogEntryType.Information);
						break;
				}
			}
			catch {}
		}
		#endregion

		#region Email Methods
		/// <summary>
		/// Publish to email.
		/// </summary>
		/// <param name="email">Email address</param>
		/// <param name="log">Log object.</param>
		protected static void PublishToEmail(string email, Log log)
		{
			bool useArtefactFormat = false;

			string description = "";
			string xmlString = "";

			try 
			{
				
				if(log.Exception != null) {
					if(log.Exception.Message != null) {
						if(log.Exception.Message.IndexOf("$artefact{REPORT}") > 0) {
							useArtefactFormat = true;
						}
					}
				}

				if(useArtefactFormat) {
					if(log.Exception.Message.IndexOf("$artefact{START}") > 0) {
						if(log.Exception.Message.IndexOf("$artefact{END}") > 0) {
							int start = log.Exception.Message.IndexOf("$artefact{START}") + "$artefact{START}".Length;
							int len = log.Exception.Message.LastIndexOf("$artefact{END}");
							int end = len - start;
							xmlString = log.Exception.Message.Substring(start, end);

							description = log.Exception.Message.Substring(0, start - "$artefact{START}".Length);
							description = description.Replace("$artefact{REPORT}", "");
						}
					}

					if(xmlString != "") {
						System.Xml.XmlDocument xmlDoc =
							new System.Xml.XmlDocument();
						xmlDoc.LoadXml(xmlString);

                  
						GA.BDC.Core.Reporting.Artefact.SeeThat seeThat =
							new GA.BDC.Core.Reporting.Artefact.SeeThat(
							"<strong> Error: (" + log.Application.MachineName + ") <br> " + description + "</strong>",
							"<i>" + log.Exception.InnerException.StackTrace.Replace("\r\n", "<br>") + "</i>", "");

						seeThat.AddTable(xmlDoc, true, System.Drawing.Color.LightGray,
							"&nbsp;&nbsp;&nbsp;&nbsp;", "", false, "", false);

						string systemInformation = "<SystemInformation>\r\n" +
							"	<UserMessage>[UserMessage]</UserMessage>\r\n" +
							"	<StackTrace>[StackTrace]</StackTrace>\r\n" +
							"	<MachineName>[MachineName]</MachineName>\r\n" +
							"	<URL>[URL]</URL>\r\n" +
							"	<RemoteIP>[RemoteIP]</RemoteIP>\r\n" +
							"	<NewSession>[NewSession]</NewSession>\r\n" +
							"	<HttpMethod>[HttpMethod]</HttpMethod>\r\n" +
							"	<PostInformation>[PostInformation]</PostInformation>\r\n" +
							"	<Host>[Host]</Host>\r\n" +
							"	<Referer>[Referer]</Referer>\r\n" +
							"	<UserAgent>[UserAgent]</UserAgent>\r\n" +
							"	<Cookies>[Cookies]</Cookies>\r\n" +
							"</SystemInformation>\r\n";

						string userMessage = description;
						string stackTrace = System.Web.HttpUtility.HtmlEncode(log.Exception.InnerException.StackTrace.Replace("\r\n", "<br>"));
						string machineName = log.Application.MachineName;
						string url = log.Web.Url;
						url = "<a href='" + url + "'>" + url + "</a>";
						url = System.Web.HttpUtility.HtmlEncode(url);
						string remoteIP = log.Web.RemoteIp;
						string newSession = log.Web.NewSession.ToString();
						string httpMethod = log.Web.HttpMethod;
						string postInformation = log.Web.GeneratePostData();
						string host = log.Web.Headers["Host"];
						string referer = log.Web.Headers["Referer"];
						if(referer != null) {
							referer = "<a href='" + referer + "'>" + referer + "</a>";
							referer = System.Web.HttpUtility.HtmlEncode(referer);
						}
						string cookies = log.Web.Headers["Cookie"];
						string userAgent = log.Web.Headers["User-Agent"];

						systemInformation = systemInformation.Replace("[UserMessage]", userMessage);
						systemInformation = systemInformation.Replace("[StackTrace]", stackTrace);
						systemInformation = systemInformation.Replace("[MachineName]", machineName);
						systemInformation = systemInformation.Replace("[URL]", url);
						systemInformation = systemInformation.Replace("[RemoteIP]", remoteIP);
						systemInformation = systemInformation.Replace("[NewSession]", newSession);
						systemInformation = systemInformation.Replace("[HttpMethod]", httpMethod);
						systemInformation = systemInformation.Replace("[PostInformation]", postInformation);
						systemInformation = systemInformation.Replace("[Host]", host);
						systemInformation = systemInformation.Replace("[Referer]", referer);
						systemInformation = systemInformation.Replace("[Cookies]", cookies);
						systemInformation = systemInformation.Replace("[UserAgent]", userAgent);

						System.Xml.XmlDocument xmlDoc2 =
							new System.Xml.XmlDocument();
						xmlDoc2.LoadXml(systemInformation);

						seeThat.AddTable(xmlDoc2, true, System.Drawing.Color.LightGray,
							"&nbsp;&nbsp;&nbsp;&nbsp;", "", false, "", false);

						xmlString = seeThat.ToHtmlString();
					}
				}

				switch (log.Level)
				{
					case LogLevel.Error:
						if(useArtefactFormat) {
							SendMail.AsyncSend(ConfigurationManager.AppSettings["Common.Email.SmtpServer"],
                                "online@fundraising.com", email, "", "", "", "", "Error: " + log.Source + " (" + log.Application.MachineName + ")", TextFormat(log.ToXmlString(true)), xmlString);
						} else {
							SendMail.AsyncSend(ConfigurationManager.AppSettings["Common.Email.SmtpServer"],
                                "online@fundraising.com", email, "", "", "", "", "Error: " + log.Source + " (" + log.Application.MachineName + ")", TextFormat(log.ToXmlString(true)), HtmlFormat(log.ToXmlString(true)));
						}
						break;
					case LogLevel.Info:
						SendMail.AsyncSend(ConfigurationManager.AppSettings["Common.Email.SmtpServer"],
							"info@efundraising.com", email, "", "", "", "", "Info: " + log.Source + " (" + log.Application.MachineName + ")", TextFormat(log.ToXmlString(true)), HtmlFormat(log.ToXmlString(true)));
						break;
					case LogLevel.Warn:
						SendMail.AsyncSend(ConfigurationManager.AppSettings["Common.Email.SmtpServer"],
							"warn@efundraising.com", email, "", "", "", "", "Warn: " + log.Source + " (" + log.Application.MachineName + ")", TextFormat(log.ToXmlString(true)), HtmlFormat(log.ToXmlString(true)));
						break;
					case LogLevel.Debug:
						SendMail.AsyncSend(ApplicationSettings.GetConfig()["Common.Email.SmtpServer", "host"],
							"debug@efundraising.com", email, "", "", "", "", "Debug: " + log.Source + " (" + log.Application.MachineName + ")", TextFormat(log.ToXmlString(true)), HtmlFormat(log.ToXmlString(true)));
						break;
				}
			}
			catch(System.Exception ex) { string s= ex.Message;}
		}
		#endregion
	
		#region LogError Methods
		/// <summary>
		/// Log Error.
		/// </summary>
		/// <param name="type"></param>
		/// <param name="logSource"></param>
		/// <param name="msg"></param>
		/// <param name="ex"></param>
		public static void LogError(LogType type, string logSource, string msg, Exception ex)
		{
			LogError(new LogTarget(type, logSource), new Log(DateTime.UtcNow, msg, "", ex));
		}

		/// <summary>
		/// Log Error
		/// </summary>
		/// <param name="target"></param>
		/// <param name="msg"></param>
		/// <param name="ex"></param>
		public static void LogError(LogTarget target, string msg, Exception ex)
		{
			LogError(target, new Log(DateTime.UtcNow, msg, "", ex));
		}

		/// <summary>
		/// Log Error
		/// </summary>
		/// <param name="target"></param>
		/// <param name="log"></param>
		public static void LogError(LogTarget target, Log log)
		{
			log.Level = LogLevel.Error;
			Log(target, log);
		}

		/// <summary>
		/// Log Error
		/// </summary>
		/// <param name="msg"></param>
		public static void LogError(string msg)
		{
			LogError(msg, null);
		}

		/// <summary>
		/// Log Error.
		/// </summary>
		/// <param name="msg"></param>
		/// <param name="ex"></param>
		public static void LogError(Exception ex)
		{
			LogError("", "", ex);
		}

		/// <summary>
		/// Log Error.
		/// </summary>
		/// <param name="msg"></param>
		/// <param name="ex"></param>
		public static void LogError(string msg, Exception ex)
		{
			LogError("", msg, ex);
		}

		/// <summary>
		/// Log Error.
		/// </summary>
		/// <param name="source"></param>
		/// <param name="msg"></param>
		/// <param name="ex"></param>
		public static void LogError(string source, string msg, Exception ex)
		{
			Log(new Log(LogLevel.Error, DateTime.UtcNow, msg, source, ex));
		}
		#endregion

		#region LogWarn Methods
		/// <summary>
		/// Log Warn.
		/// </summary>
		/// <param name="type"></param>
		/// <param name="logSource"></param>
		/// <param name="msg"></param>
		/// <param name="ex"></param>
		public static void LogWarn(LogType type, string logSource, string msg, Exception ex)
		{
			LogWarn(new LogTarget(type, logSource), new Log(DateTime.UtcNow, msg, "", ex));
		}

		/// <summary>
		/// Log Warn
		/// </summary>
		/// <param name="target"></param>
		/// <param name="msg"></param>
		/// <param name="ex"></param>
		public static void LogWarn(LogTarget target, string msg, Exception ex)
		{
			LogWarn(target, new Log(DateTime.UtcNow, msg, "", ex));
		}

		/// <summary>
		/// Log Warn
		/// </summary>
		/// <param name="target"></param>
		/// <param name="log"></param>
		public static void LogWarn(LogTarget target, Log log)
		{
			log.Level = LogLevel.Warn;
			Log(target, log);
		}

		/// <summary>
		/// Log Warn
		/// </summary>
		/// <param name="msg"></param>
		public static void LogWarn(string msg)
		{
			LogWarn(msg, null);
		}

		/// <summary>
		/// Log Warn.
		/// </summary>
		/// <param name="msg"></param>
		/// <param name="ex"></param>
		public static void LogWarn(Exception ex)
		{
			LogWarn("", "", ex);
		}

		/// <summary>
		/// Log Warn.
		/// </summary>
		/// <param name="msg"></param>
		/// <param name="ex"></param>
		public static void LogWarn(string msg, Exception ex)
		{
			LogWarn("", msg, ex);
		}

		/// <summary>
		/// Log Warn.
		/// </summary>
		/// <param name="source"></param>
		/// <param name="msg"></param>
		/// <param name="ex"></param>
		public static void LogWarn(string source, string msg, Exception ex)
		{
			Log(new Log(LogLevel.Warn, DateTime.UtcNow, msg, source, ex));
		}
		#endregion

		#region LogInfo Methods
		/// <summary>
		/// Log Info.
		/// </summary>
		/// <param name="type"></param>
		/// <param name="logSource"></param>
		/// <param name="msg"></param>
		/// <param name="ex"></param>
		public static void LogInfo(LogType type, string logSource, string msg, Exception ex)
		{
			LogInfo(new LogTarget(type, logSource), new Log(DateTime.UtcNow, msg, "", ex));
		}

		/// <summary>
		/// Log Info
		/// </summary>
		/// <param name="target"></param>
		/// <param name="msg"></param>
		/// <param name="ex"></param>
		public static void LogInfo(LogTarget target, string msg, Exception ex)
		{
			LogInfo(target, new Log(DateTime.UtcNow, msg, "", ex));
		}

		/// <summary>
		/// Log Info.
		/// </summary>
		/// <param name="target"></param>
		/// <param name="log"></param>
		public static void LogInfo(LogTarget target, Log log)
		{
			log.Level = LogLevel.Info;
			Log(target, log);
		}

		/// <summary>
		/// Log Info.
		/// </summary>
		/// <param name="msg"></param>
		public static void LogInfo(string msg)
		{
			LogInfo(msg, null);
		}

		/// <summary>
		/// Log Info.
		/// </summary>
		/// <param name="msg"></param>
		/// <param name="ex"></param>
		public static void LogInfo(Exception ex)
		{
			LogInfo("", "", ex);
		}

		/// <summary>
		/// Log Info.
		/// </summary>
		/// <param name="msg"></param>
		/// <param name="ex"></param>
		public static void LogInfo(string msg, Exception ex)
		{
			LogInfo("", msg, ex);
		}

		/// <summary>
		/// Log Info.
		/// </summary>
		/// <param name="source"></param>
		/// <param name="msg"></param>
		/// <param name="ex"></param>
		public static void LogInfo(string source, string msg, Exception ex)
		{
			Log(new Log(LogLevel.Info, DateTime.UtcNow, msg, source, ex));
		}
		#endregion

		#region LogDebug Methods
		/// <summary>
		/// Log Debug.
		/// </summary>
		/// <param name="type"></param>
		/// <param name="logSource"></param>
		/// <param name="msg"></param>
		/// <param name="ex"></param>
		public static void LogDebug(LogType type, string logSource, string msg, Exception ex)
		{
			LogDebug(new LogTarget(type, logSource), new Log(DateTime.UtcNow, msg, "", ex));
		}

		/// <summary>
		/// Log Debug
		/// </summary>
		/// <param name="target"></param>
		/// <param name="msg"></param>
		/// <param name="ex"></param>
		public static void LogDebug(LogTarget target, string msg, Exception ex)
		{
			LogDebug(target, new Log(DateTime.UtcNow, msg, "", ex));
		}

		/// <summary>
		/// Log Debug
		/// </summary>
		/// <param name="target"></param>
		/// <param name="log"></param>
		public static void LogDebug(LogTarget target, Log log)
		{
			log.Level = LogLevel.Debug;
			Log(target, log);
		}

		/// <summary>
		/// Log Debug
		/// </summary>
		/// <param name="msg"></param>
		public static void LogDebug(string msg)
		{
			LogDebug(msg, null);
		}

		/// <summary>
		/// Log Debug.
		/// </summary>
		/// <param name="msg"></param>
		/// <param name="ex"></param>
		public static void LogDebug(Exception ex)
		{
			LogDebug("", "", ex);
		}

		/// <summary>
		/// Log Debug.
		/// </summary>
		/// <param name="msg"></param>
		/// <param name="ex"></param>
		public static void LogDebug(string msg, Exception ex)
		{
			LogDebug("", msg, ex);
		}

		/// <summary>
		/// Log Debug.
		/// </summary>
		/// <param name="source"></param>
		/// <param name="msg"></param>
		/// <param name="ex"></param>
		public static void LogDebug(string source, string msg, Exception ex)
		{
			Log(new Log(LogLevel.Debug, DateTime.UtcNow, msg, source, ex));
		}
		#endregion

		#region Bulk Detection
		protected static bool IsBulk(Log log)
		{
			try 
			{
				// Only check if interval is greater than zero milliseconds and if max log is valid.
				if (Logger.BulkMaxInterval > 0 && Logger.BulkMaxLog > 0)
				{
					switch (log.Level)
					{
						case LogLevel.Error:
							if (Logger.LastErrorLog != null &&
								log.GmtDate.Subtract(Logger.LastErrorLog.GmtDate) <= TimeSpan.FromMilliseconds(Logger.BulkMaxInterval) &&
								Logger.BulkErrorLogCount >= Logger.BulkMaxLog)
								return true;
							else if (Logger.LastErrorLog != null &&
								log.GmtDate.Subtract(Logger.LastErrorLog.GmtDate) > TimeSpan.FromMilliseconds(Logger.BulkMaxInterval))
								Logger.BulkErrorLogCount = 0;
							else
								Logger.BulkErrorLogCount++;
							break;
						case LogLevel.Info:
							if (Logger.LastInfoLog != null &&
								log.GmtDate.Subtract(Logger.LastInfoLog.GmtDate) <= TimeSpan.FromMilliseconds(Logger.BulkMaxInterval) &&
								Logger.BulkInfoLogCount >= Logger.BulkMaxLog)
								return true;
							else if (Logger.LastInfoLog != null &&
								log.GmtDate.Subtract(Logger.LastInfoLog.GmtDate) > TimeSpan.FromMilliseconds(Logger.BulkMaxInterval))
								Logger.BulkInfoLogCount = 0;
							else
								Logger.BulkInfoLogCount++;
							break;
						case LogLevel.Warn:
							if (Logger.LastWarnLog != null &&
								log.GmtDate.Subtract(Logger.LastWarnLog.GmtDate) <= TimeSpan.FromMilliseconds(Logger.BulkMaxInterval) &&
								Logger.BulkWarnLogCount >= Logger.BulkMaxLog)
								return true;
							else if (Logger.LastWarnLog != null &&
								log.GmtDate.Subtract(Logger.LastWarnLog.GmtDate) > TimeSpan.FromMilliseconds(Logger.BulkMaxInterval))
								Logger.BulkWarnLogCount = 0;
							else
								Logger.BulkWarnLogCount++;
							break;
						case LogLevel.Debug:
							if (Logger.LastDebugLog != null &&
								log.GmtDate.Subtract(Logger.LastDebugLog.GmtDate) <= TimeSpan.FromMilliseconds(Logger.BulkMaxInterval) &&
								Logger.BulkDebugLogCount >= Logger.BulkMaxLog)
								return true;
							else if (Logger.LastDebugLog != null &&
								log.GmtDate.Subtract(Logger.LastDebugLog.GmtDate) > TimeSpan.FromMilliseconds(Logger.BulkMaxInterval))
								Logger.BulkDebugLogCount = 0;
							else
								Logger.BulkDebugLogCount++;
							break;
					}
				}
			}
			catch {}

			return false;
		}
		#endregion
	}
}
