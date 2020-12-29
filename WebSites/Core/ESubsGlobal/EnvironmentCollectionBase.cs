using System;
using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using GA.BDC.Core.Xml.Serialization;


namespace GA.BDC.Core.ESubsGlobal
{
	/// <summary>
	/// Summary description for EnvironmentCollectionBase.
	/// </summary>
	[Serializable]
	public class EnvironmentCollectionBase : CollectionBase, ICloneable
	{
		public EnvironmentCollectionBase()
		{

		}

		/// <summary>
		/// Return XML representation for this instance.
		/// </summary>
		/// <returns>XML string.</returns>
		public virtual string ToXmlString()
		{
			try 
			{
				XmlSerializer xs = new XmlSerializer(this.GetType());
				using(StringWriter sw = new StringWriter())
				{
					xs.SerializeFields(sw, this);
					sw.Flush();
					return sw.ToString();
				}
			}
			catch 
			{
				return "";
			}
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
