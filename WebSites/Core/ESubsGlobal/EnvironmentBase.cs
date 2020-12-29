/* Title:	Environment (Abstract class)
 * Author:	Jean-Francois Buist
 * Summary:	This is the base class of every environment object.  This is used to pass
 *			env object through classes (eg.  eSubsException gets any Enviromnet object
 *			and is able to generate information from it.)
 * 
 * Create Date:	August 1, 2005
 * 
 */

using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using GA.BDC.Core.Xml.Serialization;


namespace GA.BDC.Core.ESubsGlobal {
	/// <summary>
	/// Summary description for EnvironmentBase.
	/// </summary>
	[Serializable]
   public abstract class EnvironmentBase : GA.BDC.Core.BusinessBase.BusinessBase, ICloneable
   {

		public EnvironmentBase() {

		}

		#region ICloneable Members

		/// <summary>
		/// Creates a clone of the object.
		/// </summary>
		/// <returns>A new object containing the exact data of the original object.</returns>
		public object Clone()
		{
			MemoryStream buffer = new MemoryStream();
			BinaryFormatter formatter = new BinaryFormatter();

			formatter.Serialize(buffer, this);
			buffer.Position = 0;
			return formatter.Deserialize(buffer);
		}
		#endregion
	}
}
