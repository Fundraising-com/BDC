namespace Common
{
	using System;
	using System.Collections;
	using System.Configuration;
	using System.Xml;
	using System.Collections.Specialized;  
	using System.Diagnostics;
	/// <summary>
	///     This class handles specific configuration settings in
	///     Config.Web.
	///     <remarks>
	///         Special considerations:
	///         The application's configuaration settings are kept in 
	///         the Configuration section of the Config.web file. A new
	///         instance of this class is created automatically whenever the
	///         settings file changes, so there is no need to cache any values.
	///     </remarks>
	/// </summary>
	public class ApplicationConfiguration: IConfigurationSectionHandler
	{
		//
		// Constant values for all expected entries in the Configuration section
		//
		
		private const String WEB_ENABLEPAGECACHE           = "QSP.Fulfillment.WebApp.EnablePageCache";
		private const String WEB_PAGECACHEEXPIRESINSECONDS = "QSP.Fulfillment.WebApp.PageCacheExpiresInSeconds";
		private const String DATAACCESS_CONNECTIONSTRING   = "QSP.Fulfillment.Data.ConnectionString";
		private const String WEB_ENABLESSL                 = "QSP.Fulfillment.WebApp.EnableSsl";
		private const String IO_EXPORTPATH				   = "QSP.Fulfillment.IO.ExportPath";
		private const String IO_IMPORTPATH                 = "QSP.Fulfillment.IO.ImportPath";
		private const String IO_REPORTPATH                 = "QSP.Fulfillment.IO.ReportPath";
		private const String IO_IMPORT_FILENAME            = "QSP.Fulfillment.IO.ImportFileName";
		private const String IO_IMPORT_FILE_PREFIX         = "QSP.Fulfillment.IO.ImportFilePrefix";
		private const String IO_EXPORT_FILE_PREFIX         = "QSP.Fulfillment.IO.ExportFilePrefix";
		private const String DATAACCESS_EXCEL_CONNECTIONSTRING   = "QSP.Fulfillment.Data.ExcelConnectionString";
		private const String DATAACCESS_TXT_CONNECTIONSTRING   = "QSP.Fulfillment.Data.TxtConnectionString";
		private const String REPORT_CONNECTIONSTRING = "QSP.Fulfillment.Report.ConnectionString";
		private const String REPORT_USERID ="QSP.Fulfillment.Report.UserID";
		private const String REPORT_PASSWORD = "QSP.Fulfillment.Report.Password";
		private const String REPORT_DEFAUTLURL = "QSP.Fulfillment.Report.DefautlUrl";
		private const String WEB_ERROR_MAIL_FROM = "QSP.Fulfillment.Mail.From";
		private const String WEB_ERROR_MAIL_TO = "QSP.Fulfillment.Mail.To";
		private const String WEB_ERROR_SMTP = "QSP.Fulfillment.Mail.Smtp";
		private const String WEB_ERROR_CONNECTION_STRING = "QSP.Fulfillment.Error.ConnectionString";
		private const String WEB_ERROR_MAIL_ENABLED = "QSP.Fulfillment.Error.SendMail.Enabled";
		private const String WEB_ERROR_BDINSERT_ENABLED = "QSP.Fulfillment.Error.DBInsert.Enabled";
		private const string WEB_ERROR_BDINSERT_LINK = "QSP.Fulfillment.Error.DBInsert.LinkError";
		//
		// Static member variables. These contain the application settings
		//   from Config.Web, or the default values.
		//
		private static String dbConnectionString;
		private static String dbExcelConnectionString;
		private static String dbTxtConnectionString;
		private static bool   enablePageCache;
		private static int    pageCacheExpiresInSeconds;
		private static bool   enableSsl;
		private static String importPath;
		private static String exportPath;
		private static String reportPath;
		private static String importFileName;
		private static String importFilePrefix;
		private static String exportFilePrefix;
		private static string defaultURl;
		private static string reportPassword;
		private static string reportUserID;
		private  static string reportConnectionString;
		private static string errorWebfrom;
		private static string errorWebto;
		private static string errorWebsmtp;
		private static string errorWebconnectionString;
		private static bool errorWebMailEnabled;
		private static bool errorWebdbInsertEnabled;
		private static string errorWebdbInsertLink;
		//
		// Constant values for all of the default settings.
		//
		private const bool   WEB_ENABLEPAGECACHE_DEFAULT           = true;
		private const int    WEB_PAGECACHEEXPIRESINSECONDS_DEFAULT = 3600;
		private const String DATAACCESS_CONNECTIONSTRING_DEFAULT   = @"server=161.230.158.127;database=[DataBase];uid=qspcafulfillment;pwd=fi11m3nt;Connect Timeout=3600";
		private const bool   WEB_ENABLESSL_DEFAULT                 = false;
		private const String IO_EXPORTPATH_DEFAULT         = @"C:\Temp\";
		private const String IO_IMPORTPATH_DEFAULT         = @"C:\Temp\";
		private const String IO_REPORTPATH_DEFAULT                 = @"Reports/";
		private const String IO_IMPORT_FILENAME_DEFAULT   = "ImportFile";
		private const String IO_IMPORT_FILE_PREFIX_DEFAULT   = "Import";
		private const String IO_EXPORT_FILE_PREFIX_DEFAULT   = "Export";
		private const String DATAACCESS_EXCEL_CONNECTIONSTRING_DEFAULT    = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=[DataSource];Extended Properties=&quot;Excel 8.0; IMEX=1; HDR=no&quot;";
		private const String DATAACCESS_TXT_CONNECTIONSTRING_DEFAULT    = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=[DataSource];Extended Properties=&quot;text;HDR=Yes;FMT=Delimited&quot;";
		private  static string appRoot;																	 
		/// <summary>
		///     Called by ASP+ before the application starts to initialize
		///     settings from the Config.Web file(s). 
		///     <remarks>
		///         The app domain will restart if these settings change, so 
		///         there is no reason to read these values more than once. This 
		///         function uses the NameValueSectionHandler base class to generate 
		///         a hashtable from the XML, which is then used to store the current 
		///         settings. Because all settings are read here, we do not actually 
		///         store the generated hashtable object for later retrieval by
		///         Context.GetConfig. The application should use the accessor
		///         functions directly.
		///     </remarks>
		///     <param name="parent">
		///         An object created by processing a section with this name
		///         in a Config.Web file in a parent directory.
		///     </param>
		///     <param name="configContext">An array of Xml information.</param>
		///     <param name="section">
		///         The Path of the Config.Web file relative to the root
		///         of the web server.
		///     </param>
		///     <returns>
		///		    <para>
		///             A ConfigOutput object, which we leave empty because all settings
		///             are stored at this point.
		///		    </para>
		///	    </returns>
		/// </summary>
		public Object Create(Object parent, object configContext, XmlNode section)
		{
            
			NameValueCollection settings;
            
			try
			{
				NameValueSectionHandler baseHandler = new NameValueSectionHandler();
				settings = (NameValueCollection)baseHandler.Create(parent, configContext, section);
			}
			catch
			{
				settings = null;
			}
            
			if ( settings == null )
			{
				dbConnectionString        = DATAACCESS_CONNECTIONSTRING_DEFAULT;
				pageCacheExpiresInSeconds = WEB_PAGECACHEEXPIRESINSECONDS_DEFAULT;
				enablePageCache           = WEB_ENABLEPAGECACHE_DEFAULT;
				enableSsl                 = WEB_ENABLESSL_DEFAULT;
				dbExcelConnectionString   = DATAACCESS_EXCEL_CONNECTIONSTRING_DEFAULT;
				dbTxtConnectionString     = DATAACCESS_TXT_CONNECTIONSTRING_DEFAULT;
				importPath				  = IO_IMPORTPATH_DEFAULT;
				exportPath				  = IO_EXPORTPATH_DEFAULT;
				reportPath				  = IO_REPORTPATH_DEFAULT;
				importFileName			  = IO_IMPORT_FILENAME_DEFAULT;
				importFilePrefix    	  = IO_IMPORT_FILE_PREFIX_DEFAULT;
				exportFilePrefix		  = IO_EXPORT_FILE_PREFIX_DEFAULT;

			}
			else
			{
				dbConnectionString        = ReadSetting(settings, DATAACCESS_CONNECTIONSTRING, DATAACCESS_CONNECTIONSTRING_DEFAULT);
				defaultURl = ReadSetting(settings, REPORT_DEFAUTLURL, "");
				reportPassword = ReadSetting(settings, REPORT_PASSWORD, "");
				reportUserID = ReadSetting(settings, REPORT_USERID, "");
				reportConnectionString = ReadSetting(settings, ReportConnectionString, "");
				
				errorWebfrom = ReadSetting(settings,WEB_ERROR_MAIL_FROM,"");
				errorWebto= ReadSetting(settings,WEB_ERROR_MAIL_TO,"");
				errorWebsmtp= ReadSetting(settings,WEB_ERROR_SMTP,"");
				errorWebconnectionString= ReadSetting(settings,WEB_ERROR_CONNECTION_STRING,"");
				errorWebMailEnabled= Convert.ToBoolean(ReadSetting(settings,WEB_ERROR_MAIL_ENABLED,""));
				errorWebdbInsertEnabled= Convert.ToBoolean(ReadSetting(settings,WEB_ERROR_BDINSERT_ENABLED,""));
				errorWebdbInsertLink = ReadSetting(settings,WEB_ERROR_BDINSERT_LINK,"");
				/*pageCacheExpiresInSeconds = ApplicationConfiguration.ReadSetting(settings, WEB_PAGECACHEEXPIRESINSECONDS, WEB_PAGECACHEEXPIRESINSECONDS_DEFAULT);*/
			}
            
			return settings;
		}
        
		/// <value>
		///     Property EnablePageCache is used to get QCAP's page cache setting. 
		///     <remarks>Returns true if page caching is enabled, false otherwise.</remarks>
		/// </value>
		public static bool EnablePageCache
		{
			get
			{
				return enablePageCache;
			}
		}
        
		/// <value>
		///     Property PageCacheExpiresInSeconds is used to get QCAP's page cache expiration timeout setting.  
		///     <remarks>The number of seconds before a page cache should expire.</remarks>
		/// </value>
		public static int PageCacheExpiresInSeconds
		{
			get
			{
				return pageCacheExpiresInSeconds;
			}
		}
        
		/// <value>
		///     Property ConnectionString is used to get QCAP's database connection string.
		///     <remarks>The database connection string.</remarks>
		/// </value>
		public static String ConnectionString
		{
			get
			{
				return dbConnectionString;
			}
		}

		public static String ExcelConnectionString
		{
			get
			{
				return dbExcelConnectionString;
			}
		}

		public static String TxtConnectionString
		{
			get
			{
				return dbTxtConnectionString;
			}
		}
    
		/// <value>
		///     Property EnableSsl is used to get QCAP's SSL setting. 
		///     <remarks>True if SSL is enabled, false otherwise.</remarks>
		/// </value>
		public static bool EnableSsl
		{
			get
			{
				return enableSsl;
			}
		}

		public static String ImportPath
		{
			get
			{
				return importPath;
			}
		}

		public static String ExportPath
		{
			get
			{
				return exportPath;
			}
		}

		public static String ReportPath
		{
			get
			{
				return reportPath;
			}
		}

		public static String ImportFileName
		{
			get
			{
				return importFileName;
			}
		}

		public static String ImportFilePrefix
		{
			get
			{
				return importFilePrefix;
			}
		}

		public static String ExportFilePrefix
		{
			get
			{
				return exportFilePrefix;
			}
		}
		public static String ReportDefaultURL
		{
			get
			{
				return defaultURl;
			}
		}
		public static String ReportConnectionString
		{
			get
			{
				return reportConnectionString;
			}
		}
		public static String ReportUserID
		{
			get
			{
				return reportUserID;
			}
		}
		public static String ReportPassword
		{
			get
			{
				return reportPassword;
			}
		}
		public static String ErrorWebFrom
		{
			get
			{
				return errorWebfrom;
			}
		}
		public static string ErrorWebTo
		{
			get
			{
				return errorWebto;
			}
		}
		public static String ErrorWebSmtp
		{
			get
			{
				return errorWebsmtp;
			}
		}
		public static String ErrorWebConnectionString
		{
			get
			{
				return errorWebconnectionString;
			}
		}
		public static bool ErrorWebMailEnabled
		{
			get
			{
				return errorWebMailEnabled;
			}
		}
		public static bool ErrorWebDbInsertEnabled
		{
			get
			{
				return errorWebdbInsertEnabled;
			}
		}
		public static string ErrorWebLink
		{
			get
			{
				return errorWebdbInsertLink;
			}
		}

		public static String ReadSetting(NameValueCollection settings, String key, String defaultValue)
		{
			try
			{
				Object setting = settings[key];
                
				return (setting == null) ? defaultValue : (String)setting;
			}
			catch
			{
				return defaultValue;
			}
		}
        
		/// <summary>
		///     Boolean version of ReadSetting.
		///     <remarks>
		///         Reads a setting from a hashtable and converts it to the correct
		///         type. One of these functions is provided for each type
		///         expected in the hash table. These are public so that other
		///         classes don't have to duplicate them to read settings from
		///         a hash table.
		///     </remarks>
		///     <param name="settings">The Hashtable to read from.</param>
		///     <param name="key">A key for the value in the Hashtable.</param>
		///     <param name="defaultValue">The default value if the item is not found.</param>
		///     <retvalue>
		///		    <para>value: from the hash table</para>
		///         <para>
		///             default: if the item is not in the table or cannot be case to the expected type.
		///		    </para>
		///	    </retvalue>
		/// </summary>
		public static bool ReadSetting(NameValueCollection settings, String key, bool defaultValue)
		{
			try
			{
				Object setting = settings[key];
                
				return (setting == null) ? defaultValue : Convert.ToBoolean((String)setting);
			}
			catch
			{
				return defaultValue;
			}
		}
        
		/// <summary>
		///     int version of ReadSetting.
		///     <remarks>
		///         Reads a setting from a hashtable and converts it to the correct
		///         type. One of these functions is provided for each type
		///         expected in the hash table. These are public so that other
		///         classes don't have to duplicate them to read settings from
		///         a hash table.
		///     </remarks>
		///     <param name="settings">The Hashtable to read from.</param>
		///     <param name="key">A key for the value in the Hashtable.</param>
		///     <param name="defaultValue">The default value if the item is not found.</param>
		///     <retvalue>
		///		    <para>value: from the hash table</para>
		///         <para>
		///             default: if the item is not in the table or cannot be case to the expected type.
		///		    </para>
		///	    </retvalue>
		/// </summary>
		public static int ReadSetting(NameValueCollection settings, String key, int defaultValue)
		{
			try
			{
				Object setting = settings[key];
                
				return (setting == null) ? defaultValue : Convert.ToInt32((String)setting);
			}
			catch
			{
				return defaultValue;
			}
		}
        
		/// <summary>
		///     TraceLevel version of ReadSetting.
		///     <remarks>
		///         Reads a setting from a hashtable and converts it to the correct
		///         type. One of these functions is provided for each type
		///         expected in the hash table. These are public so that other
		///         classes don't have to duplicate them to read settings from
		///         a hash table.
		///     </remarks>
		///     <param name="settings">The Hashtable to read from.</param>
		///     <param name="key">A key for the value in the Hashtable.</param>
		///     <param name="defaultValue">The default value if the item is not found.</param>
		///     <retvalue>
		///		    <para>value: from the hash table</para>
		///         <para>
		///             default: if the item is not in the table or cannot be case to the expected type.
		///		    </para>
		///	    </retvalue>
		/// </summary>
		public static TraceLevel ReadSetting(NameValueCollection settings, String key, TraceLevel defaultValue)
		{
			try
			{
				Object setting = settings[key];
                
				return (setting == null) ? defaultValue : (TraceLevel)Convert.ToInt32((String)setting);
			}
			catch
			{
				return defaultValue;
			}
		}
		/// <summary>
		///     Function to be called by Application_OnStart as described in the
		///     class description. Initializes the application root.
		///     <param name="myAppPath">The path of the running application.</param>
		/// </summary>
		public static void OnApplicationStart(String myAppPath)
		{
			appRoot = myAppPath;
			//System.Configuration.ConfigurationSettings.GetConfig("ApplicationConfiguration");
			System.Configuration.ConfigurationSettings.GetConfig("ApplicationConfiguration");
			//System.Configuration.ConfigurationSettings.GetConfig("SourceViewer");
            
		}
        
	} 
    
}