namespace QSPForm.Common
{
    using System;
    using System.Collections;
    using System.Configuration;
    using System.Xml;
    using System.Web;
    using System.Diagnostics;

    using QSPForm.SystemFramework;
    using System.Collections.Specialized;
    /// <summary>
    ///     This class handles QSPForm-specific configuration settings in
    ///     Config.Web.
    ///     <remarks>
    ///         Special considerations:
    ///         The QSPForm application's configuaration settings are kept in 
    ///         the Configuration section of the Config.web file. A new
    ///         instance of this class is created automatically whenever the
    ///         settings file changes, so there is no need to cache any values.
    ///     </remarks>
    /// </summary>
    public class QSPFormConfiguration : IConfigurationSectionHandler
    {
        //
        // Constant values for all expected entries in the QSPFormConfiguration section
        //
        private const String WEB_ENABLEPAGECACHE = "QSPForm.WebApp.EnablePageCache";
        private const String WEB_PAGECACHEEXPIRESINSECONDS = "QSPForm.WebApp.PageCacheExpiresInSeconds";
        private const String DATAACCESS_CONNECTIONSTRING = "QSPForm.Data.ConnectionString";
        private const String DATAACCESS_CMD_TIMEOUT = "QSPForm.Data.CommandTimeOut";
        private const String WEB_ENABLESSL = "QSPForm.WebApp.EnableSsl";
        private const String IO_EXPORTPATH = "QSPForm.IO.ExportPath";
        private const String IO_IMPORTPATH = "QSPForm.IO.ImportPath";
        private const String IO_REPORTPATH = "QSPForm.IO.ReportPath";
        private const String IO_IMPORT_FILENAME = "QSPForm.IO.ImportFileName";
        private const String IO_IMPORT_FILE_PREFIX = "QSPForm.IO.ImportFilePrefix";
        private const String IO_EXPORT_FILE_PREFIX = "QSPForm.IO.ExportFilePrefix";
        private const String IO_PROMO_IMG_PATH = "QSPForm.IO.PromoImagePath";
        private const String IO_PROMO_IMG_PREVIEW_PATH = "QSPForm.IO.PromoImagePreviewPath";
        private const String IO_PROMO_IMG_EXTENSION_FILE = "QSPForm.IO.PromoImageExtensionFile";
        private const String IO_PROMOLOGO_TEMP_FOLDER = "QSPForm.IO.PromoLogoTempFolder";
        private const String IO_LOGO_IMG_PATH = "QSPForm.IO.LogoImagePath";
        private const String IO_LOGO_IMG_PREVIEW_PATH = "QSPForm.IO.LogoImagePreviewPath";
        private const String IO_LOGO_IMG_EXTENSION_FILE = "QSPForm.IO.LogoImageExtensionFile";
        private const String IO_PROMO_LOGO_IMG_PATH = "QSPForm.IO.PromoLogoImagePath";
        private const String IO_PROMO_LOGO_IMG_PREVIEW_PATH = "QSPForm.IO.PromoLogoImagePreviewPath";
        private const String IO_PROMO_LOGO_IMG_EXTENSION_FILE = "QSPForm.IO.PromoLogoImageExtensionFile";
        private const String IO_IMAGE_PREVIEW_FILE_EXTENSION = "QSPForm.IO.PreviewExtensionFile";

        private const String DATAACCESS_EXCEL_CONNECTIONSTRING = "QSPForm.Data.ExcelConnectionString";
        private const String DATAACCESS_TXT_CONNECTIONSTRING = "QSPForm.Data.TxtConnectionString";
        private const String DATAACCESS_SYNCH_CONNECTIONSTRING = "QSPForm.Data.SynchConnectionString";
        private const String DATAACCESS_SYNCH_DB_OWNER = "QSPForm.Data.SynchDBOwner";
        private const String DATAACCESS_SYNCH_CMD_TIMEOUT = "QSPForm.Data.SynchCommandTimeOut";

        //
        // Static member variables. These contain the application settings
        //   from Config.Web, or the default values.
        //
        private static String dbConnectionString;
        private static int dbCommandTimeOut;
        private static String dbExcelConnectionString;
        private static String dbTxtConnectionString;
        private static String dbSynchConnectionString;
        private static String dbSynchOwner;
        private static int dbSynchCommandTimeOut;
        private static bool enablePageCache;
        private static int pageCacheExpiresInSeconds;
        private static bool enableSsl;
        private static String importPath;
        private static String exportPath;
        private static String reportPath;
        private static String importFileName;
        private static String importFilePrefix;
        private static String exportFilePrefix;
        private static String promoImagePath;
        private static String promoImagePreviewPath;
        private static String promoImageExtensionFile;
        private static String logoImagePath;
        private static String logoImagePreviewPath;
        private static String logoImageExtensionFile;
        private static String promoLogoImagePath;
        private static String promoLogoImagePreviewPath;
        private static String promoLogoImageExtensionFile;
        private static String promoLogoTempFolder;
        private static String imagePreviewFileExtension;
        //
        // Constant values for all of the default settings.
        //
        private const bool WEB_ENABLEPAGECACHE_DEFAULT = true;
        private const int WEB_PAGECACHEEXPIRESINSECONDS_DEFAULT = 3600;
        private const String DATAACCESS_CONNECTIONSTRING_DEFAULT = @"data source=161.230.144.77;initial catalog=QSPForm2;password=QSPFormWebUser;persist security info=True;user id=QSPFormWebUser";
        private const int DATAACCESS_CMD_TIMEOUT_DEFAULT = 30;
        private const bool WEB_ENABLESSL_DEFAULT = false;
        private const String IO_EXPORTPATH_DEFAULT = @"C:\Temp\";
        private const String IO_IMPORTPATH_DEFAULT = @"C:\Temp\";
        private const String IO_REPORTPATH_DEFAULT = @"Reports/";
        private const String IO_IMPORT_FILENAME_DEFAULT = "QSPFormImportFile";
        private const String IO_IMPORT_FILE_PREFIX_DEFAULT = "QSPFormImport";
        private const String IO_EXPORT_FILE_PREFIX_DEFAULT = "QSPFormExport";
        private const String DATAACCESS_EXCEL_CONNECTIONSTRING_DEFAULT = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=[DataSource];Extended Properties=&quot;Excel 8.0; IMEX=1; HDR=no&quot;";
        private const String DATAACCESS_TXT_CONNECTIONSTRING_DEFAULT = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=[DataSource];Extended Properties=&quot;text;HDR=Yes;FMT=Delimited&quot;";
        private const String DATAACCESS_SYNCH_CONNECTIONSTRING_DEFAULT = @"Server=192.85.11.243:446;database=QDC400;Connect Timeout=60;user Id=ORDENT; password=password;Connection Reset=true;Connection Lifetime=60000;";
        private const String DATAACCESS_SYNCH_DB_OWNER_DEFAULT = "QALIB";
        private const int DATAACCESS_SYNCH_CMD_TIMEOUT_DEFAULT = 30;
        private const String IO_PROMO_IMG_PATH_DEFAULT = @"C:\temp\";
        private const String IO_PROMO_IMG_PREVIEW_PATH_DEFAULT = @"C:\temp\";
        private const String IO_PROMO_IMG_EXTENSION_FILE_DEFAULT = "";
        private const String IO_LOGO_IMG_PATH_DEFAULT = @"C:\temp\";
        private const String IO_LOGO_IMG_PREVIEW_PATH_DEFAULT = @"C:\temp\";
        private const String IO_LOGO_IMG_EXTENSION_FILE_DEFAULT = "";
        private const String IO_PROMO_LOGO_IMG_PATH_DEFAULT = @"C:\temp\";
        private const String IO_PROMO_LOGO_IMG_PREVIEW_PATH_DEFAULT = @"C:\temp\";
        private const String IO_PROMO_LOGO_IMG_EXTENSION_FILE_DEFAULT = "";
        private const String IO_PROMOLOGO_TEMP_FOLDER_DEFAULT = @"C:\temp\";
        private const String IO_IMAGE_PREVIEW_FILE_EXTENSION_DEFAULT = "jpg";

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

            if (settings == null)
            {
                dbConnectionString = DATAACCESS_CONNECTIONSTRING_DEFAULT;
                dbCommandTimeOut = DATAACCESS_CMD_TIMEOUT_DEFAULT;
                pageCacheExpiresInSeconds = WEB_PAGECACHEEXPIRESINSECONDS_DEFAULT;
                enablePageCache = WEB_ENABLEPAGECACHE_DEFAULT;
                enableSsl = WEB_ENABLESSL_DEFAULT;
                dbExcelConnectionString = DATAACCESS_EXCEL_CONNECTIONSTRING_DEFAULT;
                dbTxtConnectionString = DATAACCESS_TXT_CONNECTIONSTRING_DEFAULT;
                dbSynchConnectionString = DATAACCESS_SYNCH_CONNECTIONSTRING_DEFAULT;
                dbSynchOwner = DATAACCESS_SYNCH_DB_OWNER_DEFAULT;
                dbSynchCommandTimeOut = DATAACCESS_SYNCH_CMD_TIMEOUT_DEFAULT;
                importPath = IO_IMPORTPATH_DEFAULT;
                exportPath = IO_EXPORTPATH_DEFAULT;
                reportPath = IO_REPORTPATH_DEFAULT;
                importFileName = IO_IMPORT_FILENAME_DEFAULT;
                importFilePrefix = IO_IMPORT_FILE_PREFIX_DEFAULT;
                exportFilePrefix = IO_EXPORT_FILE_PREFIX_DEFAULT;
                promoImagePath = IO_PROMO_IMG_PATH_DEFAULT;
                promoImagePreviewPath = IO_PROMO_IMG_PREVIEW_PATH_DEFAULT;
                promoImageExtensionFile = IO_PROMO_IMG_EXTENSION_FILE_DEFAULT;
                logoImagePath = IO_LOGO_IMG_PATH_DEFAULT;
                logoImagePreviewPath = IO_LOGO_IMG_PREVIEW_PATH_DEFAULT;
                logoImageExtensionFile = IO_LOGO_IMG_EXTENSION_FILE_DEFAULT;
                promoLogoImagePath = IO_PROMO_LOGO_IMG_PATH_DEFAULT;
                promoLogoImagePreviewPath = IO_PROMO_LOGO_IMG_PREVIEW_PATH_DEFAULT;
                promoLogoImageExtensionFile = IO_PROMO_LOGO_IMG_EXTENSION_FILE_DEFAULT;
                promoLogoTempFolder = IO_PROMOLOGO_TEMP_FOLDER_DEFAULT;
                imagePreviewFileExtension = IO_IMAGE_PREVIEW_FILE_EXTENSION_DEFAULT;
            }
            else
            {
                dbConnectionString = ApplicationConfiguration.ReadSetting(settings, DATAACCESS_CONNECTIONSTRING, DATAACCESS_CONNECTIONSTRING_DEFAULT);
                dbCommandTimeOut = ApplicationConfiguration.ReadSetting(settings, DATAACCESS_CMD_TIMEOUT, DATAACCESS_CMD_TIMEOUT_DEFAULT);
                pageCacheExpiresInSeconds = ApplicationConfiguration.ReadSetting(settings, WEB_PAGECACHEEXPIRESINSECONDS, WEB_PAGECACHEEXPIRESINSECONDS_DEFAULT);
                enablePageCache = ApplicationConfiguration.ReadSetting(settings, WEB_ENABLEPAGECACHE, WEB_ENABLEPAGECACHE_DEFAULT);
                enableSsl = ApplicationConfiguration.ReadSetting(settings, WEB_ENABLESSL, WEB_ENABLESSL_DEFAULT);
                dbExcelConnectionString = ApplicationConfiguration.ReadSetting(settings, DATAACCESS_EXCEL_CONNECTIONSTRING, DATAACCESS_EXCEL_CONNECTIONSTRING_DEFAULT);
                dbTxtConnectionString = ApplicationConfiguration.ReadSetting(settings, DATAACCESS_TXT_CONNECTIONSTRING, DATAACCESS_TXT_CONNECTIONSTRING_DEFAULT);
                dbSynchConnectionString = ApplicationConfiguration.ReadSetting(settings, DATAACCESS_SYNCH_CONNECTIONSTRING, DATAACCESS_SYNCH_CONNECTIONSTRING_DEFAULT);
                dbSynchOwner = ApplicationConfiguration.ReadSetting(settings, DATAACCESS_SYNCH_DB_OWNER, DATAACCESS_SYNCH_DB_OWNER_DEFAULT);
                dbSynchCommandTimeOut = ApplicationConfiguration.ReadSetting(settings, DATAACCESS_SYNCH_CMD_TIMEOUT, DATAACCESS_SYNCH_CMD_TIMEOUT_DEFAULT);
                importPath = ApplicationConfiguration.ReadSetting(settings, IO_IMPORTPATH, IO_IMPORTPATH_DEFAULT);
                exportPath = ApplicationConfiguration.ReadSetting(settings, IO_EXPORTPATH, IO_EXPORTPATH_DEFAULT);
                reportPath = ApplicationConfiguration.ReadSetting(settings, IO_REPORTPATH, IO_REPORTPATH_DEFAULT);
                importFileName = ApplicationConfiguration.ReadSetting(settings, IO_IMPORT_FILENAME, IO_IMPORT_FILENAME_DEFAULT);
                importFilePrefix = ApplicationConfiguration.ReadSetting(settings, IO_IMPORT_FILE_PREFIX, IO_IMPORT_FILE_PREFIX_DEFAULT);
                exportFilePrefix = ApplicationConfiguration.ReadSetting(settings, IO_EXPORT_FILE_PREFIX, IO_EXPORT_FILE_PREFIX_DEFAULT);
                promoImagePath = ApplicationConfiguration.ReadSetting(settings, IO_PROMO_IMG_PATH, IO_PROMO_IMG_PATH_DEFAULT);
                promoImagePreviewPath = ApplicationConfiguration.ReadSetting(settings, IO_PROMO_IMG_PREVIEW_PATH, IO_PROMO_IMG_PREVIEW_PATH_DEFAULT);
                promoImageExtensionFile = ApplicationConfiguration.ReadSetting(settings, IO_PROMO_IMG_EXTENSION_FILE, IO_PROMO_IMG_EXTENSION_FILE_DEFAULT);
                logoImagePath = ApplicationConfiguration.ReadSetting(settings, IO_LOGO_IMG_PATH, IO_LOGO_IMG_PATH_DEFAULT);
                logoImagePreviewPath = ApplicationConfiguration.ReadSetting(settings, IO_LOGO_IMG_PREVIEW_PATH, IO_LOGO_IMG_PREVIEW_PATH_DEFAULT);
                logoImageExtensionFile = ApplicationConfiguration.ReadSetting(settings, IO_LOGO_IMG_EXTENSION_FILE, IO_LOGO_IMG_EXTENSION_FILE_DEFAULT);
                promoLogoImagePath = ApplicationConfiguration.ReadSetting(settings, IO_PROMO_LOGO_IMG_PATH, IO_PROMO_LOGO_IMG_EXTENSION_FILE_DEFAULT);
                promoLogoImagePreviewPath = ApplicationConfiguration.ReadSetting(settings, IO_PROMO_LOGO_IMG_PREVIEW_PATH, IO_PROMO_LOGO_IMG_PREVIEW_PATH_DEFAULT);
                imagePreviewFileExtension = ApplicationConfiguration.ReadSetting(settings, IO_IMAGE_PREVIEW_FILE_EXTENSION, IO_IMAGE_PREVIEW_FILE_EXTENSION_DEFAULT);
                promoLogoTempFolder = ApplicationConfiguration.ReadSetting(settings, IO_PROMOLOGO_TEMP_FOLDER, IO_PROMOLOGO_TEMP_FOLDER_DEFAULT);
                promoLogoTempFolder = ApplicationConfiguration.ReadSetting(settings, IO_PROMOLOGO_TEMP_FOLDER, IO_PROMOLOGO_TEMP_FOLDER_DEFAULT);
            }

            return settings;
        }

        public static bool EnablePageCache
        {
            get
            {
                return enablePageCache;
            }
        }
        public static int PageCacheExpiresInSeconds
        {
            get
            {
                return pageCacheExpiresInSeconds;
            }
        }
        public static String ConnectionString
        {
            get
            {
                return dbConnectionString;
            }
        }
        public static int CommandTimeOut
        {
            get
            {
                return dbCommandTimeOut;
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
        public static String SynchConnectionString
        {
            get
            {
                return dbSynchConnectionString;
            }
        }
        public static String SynchDBOwner
        {
            get
            {
                return dbSynchOwner;
            }
        }
        public static int SynchCommandTimeOut
        {
            get
            {
                return dbSynchCommandTimeOut;
            }
        }
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
        public static String PromoImagePath
        {
            get
            {
                return promoImagePath;
            }
        }
        public static String PromoImagePreviewPath
        {
            get
            {
                return promoImagePreviewPath;
            }
        }
        public static String PromoImageExtensionFile
        {
            get 
            { 
                return promoImageExtensionFile; 
            }
        }
        public static String LogoImagePath
        {
            get
            {
                return logoImagePath;
            }
        }
        public static String LogoImagePreviewPath
        {
            get
            {
                return logoImagePreviewPath;
            }
        }
        public static String LogoImageExtensionFile
        {
            get 
            { 
                return logoImageExtensionFile; 
            }
        }
        public static String PromoLogoTempFolder
        {
            get 
            { 
                return promoLogoTempFolder; 
            }
        }
        public static String Promo_LogoImagePath
        {
            get
            {
                return promoLogoImagePath;
            }
        }
        public static String Promo_LogoImagePreviewPath
        {
            get
            {
                return promoLogoImagePreviewPath;
            }
        }
        public static String Promo_LogoImageExtensionFile
        {
            get 
            { 
                return promoLogoImageExtensionFile; 
            }
        }
        public static String ImagePreviewFileExtension
        {
            get 
            { 
                return imagePreviewFileExtension; 
            }
        }
    }
}