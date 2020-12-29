using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using GA.BDC.Core.Xml.Serialization;
using System.Collections;
using System.Reflection;

namespace GA.BDC.Core.BusinessBase
{
	/// <summary>
	/// Summary description for BusinessBase.
	/// </summary>
	[Serializable]
	public abstract class BusinessBase : ICloneable
	{
		#region Constructors
		public BusinessBase()
		{

		}
		#endregion

		#region Methods
		/// <summary>
		/// Return object representation as XML string.
		/// </summary>
		/// <returns></returns>
		public virtual string ToXmlString()
		{
			try 
			{
				XmlSerializer xs = new XmlSerializer(this.GetType());
				using(StringWriter sw = new StringWriter())
				{
					xs.SerializeFields(sw, this);
					sw.Flush();
					string xmlSerialized = sw.ToString();
					xmlSerialized = "<" + this.GetType().Name + ">" + xmlSerialized + "</" + this.GetType().Name + ">";
					return xmlSerialized;
				}
			}
			catch 
			{
				return "";
			}
		}

		
		/// <summary>
		/// Return true if it is different.
		/// </summary>
		/// <returns></returns>
		public virtual bool IsBusinessBaseDifferent(BusinessBase theObject)
		{	
			bool result = (this.ToXmlString() != theObject.ToXmlString());
			return result;
		}
		#endregion

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

		

		#region Reflection

		public string sortByPropertyName = string.Empty;
		public bool sortByAscending = true;

		virtual protected Hashtable GetPropertyInfoHashtable(Type t)
		{

			string propertyName = "PropertyInfoHashtable";
			int iFound = -1;
			FieldInfo[] myFieldInfo = t.GetFields(BindingFlags.NonPublic | BindingFlags.Static);
			for(int i = 0; i < myFieldInfo.Length; i++)
			{
				if (myFieldInfo[i].Name == propertyName)
				{
					iFound = i;
				}
			}

			if (iFound > -1)
			{
				return (Hashtable)myFieldInfo[iFound].GetValue(null);
			}
			return null;
		}
		
		virtual protected object GetPropertyinfoObjectCacheLock(Type t)
		{
			string propertyName = "PROPERTYINFO_OBJECT_CACHE_LOCK";

			int iFound = -1;
			FieldInfo[] myFieldInfo = t.GetFields(BindingFlags.NonPublic | BindingFlags.Static);
			for(int i = 0; i < myFieldInfo.Length; i++)
			{
				if (myFieldInfo[i].Name == propertyName)
				{
					iFound = i;
				}
			}

			if (iFound > -1)
				return myFieldInfo[iFound].GetValue(null);
			return null;
		}


		protected PropertyInfo GetPropertyInfo(Type t, string propertyName)
		{

			Hashtable hasTable = GetPropertyInfoHashtable(t);
			object theObjectLock = GetPropertyinfoObjectCacheLock(t);

			if (hasTable != null && theObjectLock != null)
			{
				PropertyInfo proInfo = hasTable[propertyName] as PropertyInfo;
				if (proInfo == null)
				{
					PropertyInfo p = null;
					lock (theObjectLock)
					{
						PropertyInfo[] pInfo = t.GetProperties();
						for (int i=0; i < pInfo.Length; i++)
						{
							if (pInfo[i].Name == propertyName)
							{
								p = pInfo[i];
								break;
							}
						}
						if (p != null)
							hasTable[propertyName] = p;
					}
					return p;
				}
				else
					return proInfo;
			}
			else
			{
				PropertyInfo[] pInfo = t.GetProperties();
				for (int i=0; i < pInfo.Length; i++)
				{
					if (pInfo[i].Name == propertyName)
					{
						return pInfo[i];
					}
				}
			}
			return null;
		}


		virtual protected int DoCompare(object obj)
		{
			if (sortByPropertyName == string.Empty)
			{
				return 0;
			}

			PropertyInfo proInfo = GetPropertyInfo(this.GetType(), sortByPropertyName);
			if (proInfo != null)
			{
				if (sortByAscending)
				{
					IComparable theCompare =  proInfo.GetValue(this, new object[] {}) as IComparable;
					if (theCompare != null)
						return theCompare.CompareTo(proInfo.GetValue(obj, new object[] {}));
					else
					{
						throw new Exception("Can not sorting because the class is not IComparable");
					}
				}
				else
				{
					IComparable theCompare =  proInfo.GetValue(obj, new object[] {}) as IComparable;
					if (theCompare != null)
						return theCompare.CompareTo(proInfo.GetValue(this, new object[] {}));
					else
					{
						throw new Exception("Can not sorting because the class is not IComparable");
					}
				}

//				switch (proInfo.PropertyType.ToString())
//				{
//					case "System.String":
//						return string.Compare(proInfo.GetValue(this, new object[] {}).ToString(),proInfo.GetValue(obj, new object[] {}).ToString(),true);
//					case "System.Int32":
//						return (Convert.ToInt32(proInfo.GetValue(this, new object[] {})) > Convert.ToInt32(proInfo.GetValue(obj, new object[] {})) ? 1 : -1);
//					case "System.Double":
//						return (Convert.ToDouble(proInfo.GetValue(this, new object[] {})) > Convert.ToDouble(proInfo.GetValue(obj, new object[] {})) ? 1 : -1);
//					case "System.DateTime":
//						return System.DateTime.Compare(Convert.ToDateTime(proInfo.GetValue(this, new object[] {})), Convert.ToDateTime(proInfo.GetValue(obj, new object[] {})) );
//					case "System.Decimal":
//						return (Convert.ToDecimal(proInfo.GetValue(this, new object[] {})) > Convert.ToDecimal(proInfo.GetValue(obj, new object[] {})) ? 1 : -1);
//					case "System.Single":
//						return (Convert.ToSingle(proInfo.GetValue(this, new object[] {})) > Convert.ToSingle(proInfo.GetValue(obj, new object[] {})) ? 1 : -1);
//					case "System.Byte":
//						return (Convert.ToByte(proInfo.GetValue(this, new object[] {})) > Convert.ToByte(proInfo.GetValue(obj, new object[] {})) ? 1 : -1);
//					case "System.UInt64":
//						return (Convert.ToUInt64(proInfo.GetValue(this, new object[] {})) > Convert.ToUInt64(proInfo.GetValue(obj, new object[] {})) ? 1 : -1);
//					case "System.UInt32":
//						return (Convert.ToUInt32(proInfo.GetValue(this, new object[] {})) > Convert.ToUInt32(proInfo.GetValue(obj, new object[] {})) ? 1 : -1);	
//					case "System.UInt16":
//						return (Convert.ToUInt16(proInfo.GetValue(this, new object[] {})) > Convert.ToUInt16(proInfo.GetValue(obj, new object[] {})) ? 1 : -1);
//					case "System.Int16":
//						return (Convert.ToInt16(proInfo.GetValue(this, new object[] {})) > Convert.ToInt16(proInfo.GetValue(obj, new object[] {})) ? 1 : -1);
//					case "System.Int64":
//						return (Convert.ToInt64(proInfo.GetValue(this, new object[] {})) > Convert.ToInt64(proInfo.GetValue(obj, new object[] {})) ? 1 : -1);
//					case "System.Char":
//						return (Convert.ToChar(proInfo.GetValue(this, new object[] {})) > Convert.ToChar(proInfo.GetValue(obj, new object[] {})) ? 1 : -1);
//					default:
//						return string.Compare(proInfo.GetValue(this, new object[] {}).ToString(),proInfo.GetValue(obj, new object[] {}).ToString(),true);
//
//				}
			}
			return 0;
		}



		#endregion

	}
}
