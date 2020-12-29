//
// 2005-07-12 - Stephen Lim - New class.
//

using System;
using System.Configuration;
using GA.BDC.Core.Collections;

namespace GA.BDC.Core.Configuration
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
