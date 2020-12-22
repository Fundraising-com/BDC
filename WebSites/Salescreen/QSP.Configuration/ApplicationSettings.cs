using System;
using System.Configuration;
using QSP.Collections;

namespace QSP.Configuration
{
    /// <summary>
    /// Summary description for ApplicationSettings.
    /// </summary>
    public class ApplicationSettings
    {
        #region Constructors
        private ApplicationSettings()
        {

        }
        #endregion

        #region Methods
        /// <summary>
        /// Get instance of the ApplicationSettings
        /// </summary>
        /// <returns>MultiNameValueCollection object.</returns>
        public static MultiNameValueCollection GetConfig()
        {
            return (MultiNameValueCollection)ConfigurationSettings.GetConfig("applicationSettings");
        }
        #endregion
    }
}
