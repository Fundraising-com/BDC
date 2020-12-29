//
// 2005-07-12 - Stephen Lim - New class.
//


using System;
using System.Collections;
using System.Collections.Specialized;

namespace GA.BDC.Core.Collections
{
	/// <summary>
	/// MultiNameValueCollection. This collection can
	/// </summary>
	/// <remarks>
	/// This collection is based on the NameObjectCollectionBase class. However, unlike the NameObjectCollectionBase, this class stores multiple NameValueCollection values under a single key.
	/// </remarks>
	[Serializable]
	public class MultiNameValueCollection : NameObjectCollectionBase
	{
		#region Constructors

		public MultiNameValueCollection()
		{
		}
		#endregion


		#region Properties
		/// <summary>
		/// Gets or sets the last value associated with the specified key, default last index associated with this key and default attribute "value".
		/// </summary>
		public string this[string key]  
		{
			get  
			{
				return this[key, GetLastIndex(key)];
			}
			set  
			{
				this[key, GetLastIndex(key)] = value;
			}
		}

		/// <summary>
		/// Gets or sets the value associated with the specified key, index and default attribute "value".
		/// </summary>
		public string this[string key, int index]  
		{
			get  
			{
				return this[key, index, "value"];
			}
			set  
			{
				this[key, index, "value"] = value;
			}
		}

		/// <summary>
		/// Gets or sets the value associated with the specified key, attribute, default last index associated with the key.
		/// </summary>
		public string this[string key, string attribute]  
		{
			get  
			{
				return this[key, GetLastIndex(key), attribute];
			}
			set  
			{
				this[key, GetLastIndex(key), attribute] = value;
			}
		}


		/// <summary>
		/// Gets or sets the value associated with the specified key, index and attribute.
		/// </summary>
		public string this[string key, int index, string attribute]  
		{
			get  
			{
				NameValueCollection col = (NameValueCollection) BaseGet(key + " " + index);
				return col[attribute];
			}
			set  
			{
				if (index >= 0 && index <= GetLastIndex(key))
				{
					NameValueCollection col = (NameValueCollection) BaseGet(key + " " + index);
					col["attribute"] = value;
				}
			}
		}


		/// <summary>
		/// Gets a String array that contains all the keys in the collection.
		/// </summary>
		public string[] AllKeys  
		{
			get  
			{
				string[] keys = BaseGetAllKeys();
				for (int i = 0; i < keys.Length; i++)
				{
					keys[i] = keys[i].Substring(0, keys[i].LastIndexOf(" "));
				}
				return keys;
			}
		}

		/// <summary>
		/// Gets a value indicating if the collection contains keys that are not null.
		/// </summary>
		public Boolean HasKeys  
		{
			get  
			{
				return BaseHasKeys();
			}
		}
		#endregion

		#region Methods

		/// <summary>
		/// Get number items stored with the same key name.
		/// </summary>
		/// <param name="key">key.</param>
		/// <returns>Number of items stored under the same key name.</returns>
		public int GetCount(string key)
		{
			string[] keys = this.AllKeys;
			int index = 0;
			for (int i = 0; i < keys.Length; i++)
			{
				if (keys[i] == key)
					index++;
			}
			return index;
		}


		/// <summary>
		/// Get the last item index stored with the same key name.
		/// </summary>
		/// <param name="key">key.</param>
		/// <returns>Item index starting from zero stored under the same key name.</returns>
		public int GetLastIndex(string key)
		{
			return GetCount(key) - 1;
		}

		/// <summary>
		/// Adds an entry to the collection.
		/// </summary>
		/// <param name="key">Key.</param>
		/// <param name="value">A ListDictionary object.</param>
		public void Add(String key, NameValueCollection value)  
		{			
			BaseAdd(key + " " + GetCount(key), value);
		}

		/// <summary>
		/// Removes an entry with the specified key from the collection.
		/// </summary>
		/// <param name="key">Key.</param>
		public void Remove(string key)  
		{
			string[] keys = AllKeys;
			int index = 0;
			for (int i = 0; i < keys.Length; i++)
			{
				if (keys[i] == key)
				{
					BaseRemove(keys + " " + index);	
					index++;
				}
			}			
		}

		/// <summary>
		/// Removes an entry with the specified key from the collection.
		/// </summary>
		/// <param name="key">Key.</param>
		/// <param name="index">Specified index within key.</param>
		public void Remove(string key, int index)  
		{
			BaseRemove(key + " " + index);	
		}

		/// <summary>
		/// Clears all the elements in the collection.
		/// </summary>
		public void Clear()  
		{
			BaseClear();
		}
		#endregion

	}
}
