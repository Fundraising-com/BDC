using System;
using System.Web;
using System.Collections.Specialized;

namespace QSP.WebControl
{
	/// <summary>
	/// Summary description for PageSwitchStateBag.
	/// </summary>
	[Serializable]
	public class PageSwitchStateBagCollection : NameObjectCollectionBase
	{
		private const string QUERYSTRING_PARAMETER_NAME = "PageSwitchStateID";

		private int currentID = 0;
		private long defaultTimeOut = Convert.ToInt64(System.Configuration.ConfigurationSettings.AppSettings["PageSwitchStateTimeout"]);

		public long DefaultTimeOut 
		{
			get 
			{
				return defaultTimeOut;
			}
			set 
			{
				defaultTimeOut = value;
			}
		}

		public PageSwitchStateBag this[int id] 
		{
			get 
			{
				return Get(id);
			}
		}

		public void Clear() 
		{
			this.BaseClear();
		}

		public PageSwitchStateBag Get(int id) 
		{
			return (PageSwitchStateBag) this.BaseGet(id.ToString());
		}

		public PageSwitchStateBag GetAt(int index) 
		{
			return (PageSwitchStateBag) this.BaseGet(index);
		}

		public int[] GetAllIDs() 
		{
			string[] keys = this.BaseGetAllKeys();
			int[] ids = new int[keys.Length];

			Array.Copy(keys, ids, keys.Length);

			return ids;
		}

		public PageSwitchStateBag[] GetAllValues() 
		{
			object[] values = this.BaseGetAllValues();
			PageSwitchStateBag[] convertedValues = new PageSwitchStateBag[values.Length];

			Array.Copy(values, convertedValues, values.Length);
			
			return convertedValues;
		}

		public void Remove(int id) 
		{
			this.BaseRemove(id.ToString());
		}

		public void RemoveAt(int index) 
		{
			this.BaseRemoveAt(index);
		}

		public int CreateNewPageSwitchState() 
		{
			CleanTimedOut();

			if(currentID < Int32.MaxValue) 
			{
				currentID++;
			} 
			else 
			{
				currentID = 1;
			}

			this.BaseAdd(currentID.ToString(), new PageSwitchStateBag(DefaultTimeOut));

			return currentID;
		}

		public string GetQueryString(int id) 
		{
			return QUERYSTRING_PARAMETER_NAME + "=" + id.ToString();
		}

		private void CleanTimedOut() 
		{
			PageSwitchStateBag pageSwitchStateBag;

			for(int i = 0; i < this.Count; i++) 
			{
				pageSwitchStateBag = GetAt(i);

				if(pageSwitchStateBag.TimeStamp.AddMinutes(Convert.ToDouble(pageSwitchStateBag.TimeOut)) > DateTime.Now) 
				{
					RemoveAt(i);
				}
			}
		}
	}
}
