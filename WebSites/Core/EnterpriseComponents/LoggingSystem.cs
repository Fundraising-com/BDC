//
// 2005-05-24 - Stephen Lim - Use Email.SendMail instead of Helper.SendMail to remove dependency on Cdosys.dll.
// 2005-06-17 - Stephen Lim - Logging System no longer throws exception.
//

using System;
using System.Web;
using System.Web.SessionState;
using System.Data;
using System.Diagnostics;
using System.Configuration;
using System.Collections;
using GA.BDC.Core.Email;

namespace GA.BDC.Core.EnterpriseComponents {
	/// <summary>
	/// This class will record an event into it's proper place specified into the web.config.
	/// </summary>
	public sealed class LoggingSystem {
		
		/// <summary>
		/// Log Types Levels
		/// </summary>
		public enum LogType {
			/// <summary>
			/// Critical Error
			/// </summary>
			Panic = 2,	
			/// <summary>
			/// Error Event
			/// </summary>
			Error = 3,
			/// <summary>
			/// Warning Event
			/// </summary>
			Warning = 4,
			/// <summary>
			/// Information Event
			/// </summary>
			Info = 5
		};

		/// <summary>
		/// Log Locations (DB, Emails, Files, Event Viewer...)
		/// </summary>
		public enum LogLocation {
			/// <summary>
			/// Notify by email
			/// </summary>
			Email	= 1,
			/// <summary>
			/// Notify into the event viewer
			/// </summary>
			EventViewer = 2,
			/// <summary>
			/// Notify into a file
			/// </summary>
			File = 4,
			/// <summary>
			/// Notify into a database
			/// </summary>
			Database = 8
		}

		# region Constructor
		/// <summary>
		/// LoginSytem constructor.
		/// </summary>
		public LoggingSystem() {
			//
			// TODO: Add constructor logic here
			//
			
		}
		#endregion Constructor

		#region Log Errors/Events

		#region Log Errors Type (Panic, Error, Warning, Information)

		/// <summary>
		/// Logs Critical Error
		/// </summary>
		/// <param name="message"></param>
		public static void LogPanic(string message) {
			Log(LogType.Panic, message);
		}

		/// <summary>
		/// Logs Critical Error
		/// </summary>
		/// <param name="applicationName"></param>
		/// <param name="message"></param>
		public static void LogPanic(string applicationName, string message) {
			Log(LogType.Panic, applicationName, message);
		}

		/// <summary>
		/// Logs Critical Error using internal id
		/// </summary>
		/// <param name="applicationName"></param>
		/// <param name="message"></param>
		public static void LogPanic(string applicationName, string message, int internalID) {
			Log(LogType.Panic, applicationName, message, internalID);
		}

		/// <summary>
		/// Log Error
		/// </summary>
		/// <param name="message"></param>
		public static void LogError(string message) {
			Log(LogType.Error, message);
		}

		/// <summary>
		/// Log Error
		/// </summary>
		/// <param name="applicationName"></param>
		/// <param name="message"></param>
		public static void LogError(string applicationName, string message) {
			Log(LogType.Error, applicationName, message);
		}

		/// <summary>
		/// Log Error using internal id
		/// </summary>
		/// <param name="applicationName"></param>
		/// <param name="message"></param>
		public static void LogError(string applicationName, string message, int internalID) {
			Log(LogType.Error, applicationName, message, internalID);
		}

		/// <summary>
		/// Log Warning
		/// </summary>
		/// <param name="message"></param>
		public static void LogWarning(string message) {
			Log(LogType.Warning, message);
		}

		/// <summary>
		/// Log Warning
		/// </summary>
		/// <param name="applicationName"></param>
		/// <param name="message"></param>
		public static void LogWarning(string applicationName, string message) {
			Log(LogType.Warning, applicationName, message);
		}
		
		/// <summary>
		/// Log Warning using internal id
		/// </summary>
		/// <param name="applicationName"></param>
		/// <param name="message"></param>
		public static void LogWarning(string applicationName, string message, int internalID) {
			Log(LogType.Warning, applicationName, message, internalID);
		}

		/// <summary>
		/// Log Information Event
		/// </summary>
		/// <param name="message"></param>
		public static void LogInformation(string message) {
			Log(LogType.Info, message);
		}

		/// <summary>
		/// Log Information Event
		/// </summary>
		/// <param name="applicationName"></param>
		/// <param name="message"></param>
		public static void LogInformation(string applicationName, string message) {
			Log(LogType.Info, applicationName, message);
		}

		/// <summary>
		/// Log Information Event using internal id
		/// </summary>
		/// <param name="applicationName"></param>
		/// <param name="message"></param>
		public static void LogInformation(string applicationName, string message, int internalID) {
			Log(LogType.Info, applicationName, message,internalID);
		}

		#endregion

		/// <summary>
		/// Log Event
		/// Default AppName specified into WebConfig
		/// Default internal Id set to -1
		/// </summary>
		/// <param name="logType">Type of event</param>
		/// <param name="message">Event Message</param>
		public static void Log(LogType logType, string message) {
			Log(logType, message, -1);
		}

		/// <summary>
		/// Log Event
		/// Default internal Id set to -1
		/// </summary>
		/// <param name="logType">Type of event</param>
		/// <param name="applicationName">Application Name</param>
		/// <param name="message">Event Message</param>
		public static void Log(LogType logType, string applicationName, string message) {
			Log(logType, applicationName, message, -1);
		}

		/// <summary>
		/// Log Event using internal id
		/// Default AppName specified into WebConfig
		/// </summary>
		/// <param name="logType">Type of event</param>
		/// <param name="message">Message</param>
		/// <param name="internalID">Internal Message ID</param>
		public static void Log(LogType logType, string message, int internalID) {
			LogLocation[] logLocation = GetLogLocation(logType);

			foreach(LogLocation logLoc in logLocation) {
				SaveLog(logLoc, logType, message, internalID);
			}
		}

		/// <summary>
		/// Log Event using internal id
		/// </summary>
		/// <param name="logType">Type of event</param>
		/// <param name="applicationName">Application Name</param>
		/// <param name="message">Message</param>
		/// <param name="internalID">Internal Message ID</param>
		public static void Log(LogType logType, string applicationName, string message, int internalID) {
			LogLocation[] logLocation = GetLogLocation(logType);

			foreach(LogLocation logLoc in logLocation) {
				SaveLog(logLoc, logType, applicationName, message, internalID);
			}

		}

		/// <summary>
		/// Log an Event at the specified Location
		/// * application name specified into webconfig
		/// * internal id is by default -1
		/// </summary>
		/// <param name="logLocation">Location</param>
		/// <param name="logType">Log Level</param>
		/// <param name="message">Message</param>
		public static void Log(LogLocation logLocation, LogType logType, string message) {
			SaveLog(logLocation, logType, message, -1);
		}

		/// <summary>
		/// Log an Event at the specified Location
		/// * internal id is by default -1
		/// </summary>
		/// <param name="logLocation">Location</param>
		/// <param name="logType">Log Level</param>
		/// <param name="applicationName">App Name</param>
		/// <param name="message">Message</param>
		public static void Log(LogLocation logLocation, LogType logType, string applicationName, string message) {
			SaveLog(logLocation, logType, applicationName, message, -1);
		}

		/// <summary>
		/// Log an event at the specified location
		/// App Name specified into WebConfig
		/// </summary>
		/// <param name="logLocation">Location</param>
		/// <param name="logType">Log Level</param>
		/// <param name="message">Message</param>
		/// <param name="internalID">Internal ID</param>
		public static void Log(LogLocation logLocation, LogType logType, string message, int internalID) {
			SaveLog(logLocation, logType, message, internalID);
		}

		/// <summary>
		/// Log an event at the specified location
		/// </summary>
		/// <param name="logLocation">Location</param>
		/// <param name="logType">Log Level</param>
		/// <param name="applicationName">App Name</param>
		/// <param name="message">Message</param>
		/// <param name="internalID">Internal ID</param>
		public static void Log(LogLocation logLocation, LogType logType, string applicationName, string message, int internalID) {
			SaveLog(logLocation, logType, applicationName, message, internalID);
		}

		#endregion

		#region Internal Functions

		public static string GetLogTypeDefinition(LogType logType) {
			string logTypeDefinition = null;

			switch(logType) {
				case LogType.Panic:
					logTypeDefinition = "Critical Error";
					break;
				case LogType.Error:
					logTypeDefinition = "Error";
					break;
				case LogType.Warning:
					logTypeDefinition = "Warning";
					break;
				case LogType.Info:
					logTypeDefinition = "Information";
					break;
				default:
					// TODO: Handle error
					break;
			}
			return logTypeDefinition;
		}

		public static string[] GetEmailList(LogType logType) {
			string emailListAttribute = null;

			switch(logType) {
				case LogType.Panic:
					emailListAttribute = "LogNotificationEmailPanic";
					break;
				case LogType.Error:
					emailListAttribute = "LogNotificationEmailError";
					break;
				case LogType.Warning:
					emailListAttribute = "LogNotificationEmailWarning";
					break;
				case LogType.Info:
					emailListAttribute = "LogNotificationEmailInformation";
					break;
				default:
					// TODO: Handle error
					break;
			}

			string emailList = ConfigurationSettings.AppSettings[emailListAttribute];

			if(emailList == string.Empty) {
				// set default emails address
				Debug.Write("Error: WebConfig doesn't have default email addresses!!");
				emailList = "Jean-Francois_Buist@rd.com,Jean-Francois_Lavigne@rd.com, Jean-Francois_Lavigne@rd.com, sam.abdel-malek@rd.com";
			}

			return emailList.Split(',');
		}

		public static string GetLogFileName(LogType logType) {
			string logFileNameAttribute = null;

			switch(logType) {
				case LogType.Panic:
					logFileNameAttribute = "LogNotificationFilePanic";
					break;
				case LogType.Error:
					logFileNameAttribute = "LogNotificationFileError";
					break;
				case LogType.Warning:
					logFileNameAttribute = "LogNotificationFileWarning";
					break;
				case LogType.Info:
					logFileNameAttribute = "LogNotificationFileInformation";
					break;
				default:
					// TODO: Handle error
					break;
			}
            
			string logFileName = ConfigurationSettings.AppSettings[logFileNameAttribute];

			if(logFileName != string.Empty) {
				return logFileName;
			} else {
				// TODO: Handle error
				return null;
			}
		}

		/// <summary>
		/// Get the location depending of the log type
		/// These values are stored into the web config file
		/// </summary>
		/// <param name="logType"></param>
		/// <returns></returns>
		private static LogLocation[] GetLogLocation(LogType logType) {
			// name of the key on the web config
			string logTypeConfigName = null;
			// location array to fill out from the web config
			LogLocation[] logLocation = new LogLocation[5];
			int logLocationIndex = 0;

			//			ArrayList list = new ArrayList();
			//			list.Add(LogLocation.Database);
			//			foreach(LogLocation log1 in list)
			//			{
			//			}

			// get the attribute name of the web config where the log should be written
			switch(logType) {
				case LogType.Panic:
					logTypeConfigName = "LoggingSystemEventMethodPanic";
					break;
				case LogType.Error:
					logTypeConfigName = "LoggingSystemEventMethodError";
					break;
				case LogType.Warning:
					logTypeConfigName = "LoggingSystemEventMethodWarning";
					break;
				case LogType.Info:
					logTypeConfigName = "LoggingSystemEventMethodInformation";
					break;
				default:
					// TODO: Handle error
					break;
			}

			// get the locations where the events are gonna be sent
			string logTypeConfigValue = ConfigurationSettings.AppSettings[logTypeConfigName];

			if(Helper.IsInteger(logTypeConfigValue)) {
				int logLocationId = int.Parse(logTypeConfigValue);
				
				// Email = 1
				// EventViewer = 2
				// File = 4
				// Database = 8
				if((logLocationId & 8) == 8) {
					logLocation[logLocationIndex++] = LogLocation.Database;
				}

				if((logLocationId & 4) == 4) {
					logLocation[logLocationIndex++] = LogLocation.File;
				}

				if((logLocationId & 2) == 2) {
					logLocation[logLocationIndex++] = LogLocation.EventViewer;
				}

				if((logLocationId & 1) == 1) {
					logLocation[logLocationIndex++] = LogLocation.Email;
				}
				
				// TODO: Handle error


			} else {
				// TODO: Handle error
			}
            
			// won't create a class extended by ICollection for this enumeration
			LogLocation[] logLocationReturnVal = new LogLocation[logLocationIndex];
			for(int i=0;i<logLocationIndex;i++)
				logLocationReturnVal[i] = logLocation[i];
			//			logLocationReturnVal.CopyTo(logLocation, logLocationIndex)

			// return LogLocation[] with LogLocation.Lenght adequate (**foreach capable**)
			return logLocationReturnVal;
		}
		#endregion

		#region Log Visit 
		public static Decimal LogVisit(HttpSessionState session, HttpRequest request) {
			return LogVisit(session.SessionID, request);
		}

		public static Decimal LogVisit(string session_id, HttpRequest request) {

			string referrer = null;
			if(request.UrlReferrer == null) {
				referrer = "";
			} else {
				referrer = request.UrlReferrer.AbsolutePath;
			}

			return LogVisit(session_id, referrer, request.Browser.Browser, request.Browser.Version, 
				request.Browser.Platform, request.UserHostAddress);
		}

		public static Decimal LogVisit(string session_id, string referrer, string browserName,
			string browserVersion, string platform, string ip) {
/*
				@strVisitorsSession VARCHAR(40),
				@strVisitorsReferrer VARCHAR(125),
				@strVisitorsBrowserName VARCHAR(25),
				@strVisitorsBrowserVersion VARCHAR(15),
				@strVisitorsPlatform VARCHAR(25),
				@strVisitorsIPAddress CHAR(15),
				@dteVisitorsLogDate DATETIME */

			DataParameters[] parameters = new DataParameters[7];

			parameters[0] = new DataParameters();
			parameters[0].ParameterName = "@strVisitorsSession";
			parameters[0].DataType = DbType.String;
			parameters[0].ParamDirection = ParameterDirection.Input;
			parameters[0].Value = session_id;

			parameters[1] = new DataParameters();
			parameters[1].ParameterName = "@strVisitorsReferrer";
			parameters[1].DataType = DbType.String;
			parameters[1].ParamDirection = ParameterDirection.Input;
			parameters[1].Value = referrer;

			parameters[2] = new DataParameters();
			parameters[2].ParameterName = "@strVisitorsBrowserName";
			parameters[2].DataType = DbType.String;
			parameters[2].ParamDirection = ParameterDirection.Input;
			parameters[2].Value = browserName;

			parameters[3] = new DataParameters();
			parameters[3].ParameterName = "@strVisitorsBrowserVersion";
			parameters[3].DataType = DbType.String;
			parameters[3].ParamDirection = ParameterDirection.Input;
			parameters[3].Value = browserVersion;
			
			parameters[4] = new DataParameters();
			parameters[4].ParameterName = "@strVisitorsPlatform";
			parameters[4].DataType = DbType.String;
			parameters[4].ParamDirection = ParameterDirection.Input;
			parameters[4].Value = platform;

			parameters[5] = new DataParameters();
			parameters[5].ParameterName = "@strVisitorsIPAddress";
			parameters[5].DataType = DbType.String;
			parameters[5].ParamDirection = ParameterDirection.Input;
			parameters[5].Value = ip;

			parameters[6] = new DataParameters();
			parameters[6].ParameterName = "@dteVisitorsLogDate";
			parameters[6].DataType = DbType.DateTime;
			parameters[6].ParamDirection = ParameterDirection.Input;
			parameters[6].Value = DateTime.Now;

			DatabaseInterface dbi = new DatabaseInterface();

			Decimal userID = -1;
			try {
				userID = (Decimal)dbi.ExecuteScalar(CommandType.StoredProcedure, "insert_visitors_log", parameters,DataProviders.SqlServer);
			} catch(System.Exception ex) {
				LoggingSystem.LogError(ex.Message);
				userID = -1;
			}

			return userID;

		}

		public static void LogVisitTrail(Decimal userID, HttpRequest request) {
			string currentUrl = null;
			string referrerUrl = null;

			if(request.Url == null) {
				currentUrl = "";
			} else {
				currentUrl = request.Url.AbsolutePath;
			}

			if(request.UrlReferrer == null) {
				referrerUrl = "";
			} else {
				referrerUrl = request.UrlReferrer.AbsolutePath;
			}

			LogVisitTrail(userID, currentUrl, referrerUrl);
		}

		public static void LogVisitTrail(Decimal userID, string currentUrl, string previousUrl) {
			
			DataParameters[] parameters = new DataParameters[4];

			parameters[0] = new DataParameters();
			parameters[0].ParameterName = "@intVisitorsLogID";
			parameters[0].DataType = DbType.Int64;
			parameters[0].ParamDirection = ParameterDirection.Input;
			parameters[0].Value = Int64.Parse(userID.ToString());

			parameters[1] = new DataParameters();
			parameters[1].ParameterName = "@strCurrentUrl";
			parameters[1].DataType = DbType.String;
			parameters[1].ParamDirection = ParameterDirection.Input;
			parameters[1].Value = currentUrl;

			parameters[2] = new DataParameters();
			parameters[2].ParameterName = "@dteTrailDate";
			parameters[2].DataType = DbType.DateTime;
			parameters[2].ParamDirection = ParameterDirection.Input;
			parameters[2].Value = DateTime.Now;

			parameters[3] = new DataParameters();
			parameters[3].ParameterName = "@strPreviousUrl";
			parameters[3].DataType = DbType.String;
			parameters[3].ParamDirection = ParameterDirection.Input;
			parameters[3].Value = previousUrl;

			DatabaseInterface dbi = new DatabaseInterface();
			try {
				dbi.ExecuteNonQuery(CommandType.StoredProcedure, "insert_visitors_trail", 
					parameters, DataProviders.SqlServer);
			} catch(System.Exception ex) {
				throw new Exception(ex.Message);
			}
		}


		#endregion

		#region Log methods

		private static void SaveLog(LogLocation logLocation, LogType logType, string message) {
			string applicationName = Helper.GetWebConfigValue("ApplicationName");
			if(applicationName == string.Empty) {
				applicationName = "Application Name Not Specified Into WebConfig.";
			}
				
			SaveLog(logLocation, logType, applicationName, message, -1);

		}

		/// <summary>
		/// Generic version that will save the error into its appropriate place
		/// </summary>
		private static void SaveLog(LogLocation logLocation, LogType logType, string message, int internalID) {
			string applicationName = Helper.GetWebConfigValue("ApplicationName");
			if(applicationName == string.Empty) {
				applicationName = "Application Name Not Specified Into WebConfig.";
			}
				
			SaveLog(logLocation, logType, applicationName, message, internalID);

		}

		/// <summary>
		/// Generic version that will save the error into its appropriate place
		/// </summary>
		private static void SaveLog(LogLocation logLocation, LogType logType, string applicationName, string message) {
			SaveLog(logLocation, logType, applicationName, message, -1);
		}


		/// <summary>
		/// Generic version that will save the error into its appropriate place using internal ID
		/// </summary>
		private static void SaveLog(LogLocation logLocation, LogType logType, string applicationName, string message, int internalID ) {
			switch(logLocation) {
				case LogLocation.Database:
					AddToDB(logType, applicationName, message, internalID);
					break;
				case LogLocation.File:
					AddToFile(logType, applicationName, message, internalID);
					break;
				case LogLocation.Email:
					AddToEmail(logType, applicationName, message, internalID);
					break;
				case LogLocation.EventViewer:
					AddToEventViewer(logType, applicationName, message, internalID);
					break;
			}
		}

		/// <summary>
		/// Log an event to a file
		/// </summary>
		private static void AddToFile(LogType logType, string applicationName, string message, int internalID) {

			try 
			{
				string fileName = GetLogFileName(logType);
				Helper.AppendToFile(fileName, 
					"[" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString() + // date
					"] " + applicationName + // app name
					(internalID!=-1?"(" + internalID.ToString() + ")":"") + // if internal id, show it
					"\t " + message);	// print the message
			}
			catch {
				// Ignore exception since LoggingSystem is normally used to log an exception inside a catch clause.
				// We don't want to throw a second unexpected exception inside someone's catch clause.
			}
		}

		/// <summary>
		/// Send an email to the emails specified into the webconfig
		/// </summary>
		/// <param name="logType">Type of event</param>
		/// <param name="applicationName">Application name</param>
		/// <param name="message">Message</param>
		/// <param name="internalID">internal id</param>
		private static void AddToEmail(LogType logType, string applicationName, string message, int internalID) {

			try 
			{
				string[] emails = GetEmailList(logType);
				string smtpServer = ConfigurationSettings.AppSettings["SmtpServer"];
				string fromEmailAddr = ConfigurationSettings.AppSettings["FromEmail"];
				string subject = GetLogTypeDefinition(logType);
			
				foreach(string email in emails) 
				{
					// Send email
					SendMail.Send(smtpServer, fromEmailAddr, email.Trim(), "", "", "", "", applicationName + ": " + subject, message, "");
				}
			}
			catch {
				// Ignore exception since LoggingSystem is normally used to log an exception inside a catch clause.
				// We don't want to throw a second unexpected exception inside someone's catch clause.
			}
		}

		/// <summary>
		/// Log an event into the event viewer
		/// </summary>
		private static void AddToEventViewer(LogType logType, string applicationName, string message, int internalID) {

			try {
				EventLogEntryType eventLogType;

				switch(logType) {
					case LogType.Panic:
					case LogType.Error:
						eventLogType = EventLogEntryType.Error;
						break;
					case LogType.Warning:
						eventLogType = EventLogEntryType.Warning;
						break;
					case LogType.Info:
						eventLogType = EventLogEntryType.Information;
						break;
					default:
						eventLogType = EventLogEntryType.Error;
						break;
				}
			
				EventLog.WriteEntry(applicationName, message, eventLogType, (internalID<0?-internalID:internalID));
			} 
			catch {
				// Ignore exception since LoggingSystem is normally used to log an exception inside a catch clause.
				// We don't want to throw a second unexpected exception inside someone's catch clause.
			}
		}

		/// <summary>
		/// Log an event into the database
		/// </summary>
		private static void AddToDB(LogType logType, string applicationName, string message, int internalID) {

			try 
			{
				DataParameters[] parameters = new DataParameters[5];

				parameters[0] = new DataParameters();
				parameters[0].ParameterName = "@intEventCategoryID";
				parameters[0].DataType = DbType.Int16;
				parameters[0].ParamDirection = ParameterDirection.Input;
				parameters[0].Value = (int)logType;

				parameters[1] = new DataParameters();
				parameters[1].ParameterName = "@strEventType";
				parameters[1].DataType = DbType.String;
				parameters[1].ParamDirection = ParameterDirection.Input;
				parameters[1].Value = internalID.ToString();

				parameters[2] = new DataParameters();
				parameters[2].ParameterName = "@strEventSource";
				parameters[2].DataType = DbType.String;
				parameters[2].ParamDirection = ParameterDirection.Input;
				parameters[2].Value = applicationName;

				parameters[3] = new DataParameters();
				parameters[3].ParameterName = "@strEventMessage";
				parameters[3].DataType = DbType.String;
				parameters[3].ParamDirection = ParameterDirection.Input;
				parameters[3].Value = message;

				parameters[4] = new DataParameters();
				parameters[4].ParameterName = "@dteEventDate";
				parameters[4].DataType = DbType.DateTime;
				parameters[4].ParamDirection = ParameterDirection.Input;
				parameters[4].Value = DateTime.Now;

				DatabaseInterface dbi = new DatabaseInterface();

			
				dbi.ExecuteNonQuery(CommandType.StoredProcedure, "insert_application_events", parameters,DataProviders.SqlServer);
			} 
			catch {
				// Ignore exception since LoggingSystem is normally used to log an exception inside a catch clause.
				// We don't want to throw a second unexpected exception inside someone's catch clause.
			}
		}

		#endregion

	}
}
