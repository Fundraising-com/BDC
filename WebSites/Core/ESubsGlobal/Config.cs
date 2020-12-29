/* Title:	Configuration
 * Author:	Jean-Francois Buist
 * Summary:	All configurations are hold in this class.
 *			This is not a web/app config class.
 *			Even if the configuration setting is not in the .config,
 *			please use this object in order to respect layers.
 * 
 * Create Date:	August 1, 2005
 * 
 */

using System;
using GA.BDC.Core.ESubsGlobal.DataAccess;

namespace GA.BDC.Core.ESubsGlobal {
	/// <summary>
	/// This class holds all application configurations.
	/// </summary>
	public class Config : AppConfig
	{
		public Config() {

		}
	}
}
