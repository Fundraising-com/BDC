using System;
using System.Web;
using System.Collections.Specialized;

namespace QSP.WebControl
{
	/// <summary>
	/// Summary description for PageSwitchStateBag.
	/// </summary>
	[Serializable]
	public class PageSwitchStateBag : NameObjectCollectionBase
	{
		private DateTime timeStamp;
		private long timeOut;

		internal PageSwitchStateBag(long timeOut)
		{
			TimeOut = timeOut;
		}

		public DateTime TimeStamp 
		{
			get 
			{
				return timeStamp;
			}
		}

		public long TimeOut 
		{
			get 
			{
				return timeOut;
			}
			set 
			{
				timeStamp = DateTime.Now;
				timeOut = value;
			}
		}

		public object this[int index] 
		{
			get 
			{
				return Get(index);
			}
			set 
			{
				Set(index, value);
			}
		}

		public object this[string name] 
		{
			get 
			{
				return Get(name);
			}
			set 
			{
				Set(name, value);
			}
		}

		public void Add(string name, object value) 
		{
			this.BaseAdd(name, value);
		}

		public void Clear() 
		{
			this.BaseClear();
		}

		public object Get(int index) 
		{
			return this.BaseGet(index);
		}

		public object Get(string name) 
		{
			return this.BaseGet(name);
		}

		public string[] GetAllKeys() 
		{
			return this.BaseGetAllKeys();
		}

		public object[] GetAllValues() 
		{
			return this.BaseGetAllValues();
		}

		public object[] GetAllValues(System.Type type) 
		{
			return this.BaseGetAllValues(type);
		}

		public string GetKey(int index) 
		{
			return this.BaseGetKey(index);
		}

		public bool HasKeys() 
		{
			return this.BaseHasKeys();
		}

		public void Remove(string name) 
		{
			this.BaseRemove(name);
		}

		public void RemoveAt(int index) 
		{
			this.BaseRemoveAt(index);
		}

		public void Set(int index, object value) 
		{
			this.BaseSet(index, value);
		}

		public void Set(string name, object value) 
		{
			this.BaseSet(name, value);
		}
	}
}
