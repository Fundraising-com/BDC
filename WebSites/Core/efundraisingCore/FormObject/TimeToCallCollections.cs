using System;
using System.Data;	

using GA.BDC.Core.Database.efundraising;

namespace GA.BDC.Core.efundraisingCore.FormObject {

	public class TimeToCallCollections : System.Collections.CollectionBase {

		#region public constructors

		public TimeToCallCollections() {
			
		}

		#endregion

		#region IList Members

		public TimeToCall this[int index] {
			get{ return (TimeToCall)List[index]; }
		}

		public TimeToCall this[string descriptionName] 
		{
			get 
			{
				for(int i=0;i<List.Count;i++) 
				{
					if(((TimeToCall)List[i]).TimeToCallDescription == descriptionName)
						return (TimeToCall)List[i];
				}
				return null;
			}
		}

		public void Insert(int index, TimeToCall value) {
			List.Insert(index, value);
		}

		public void Remove(TimeToCall value) {
			List.Remove(value);
		}

		public bool Contains(TimeToCall value) {
			return List.Contains(value);
		}

		public void Clear() {
			List.Clear();
		}

		public int IndexOf(TimeToCall value) {
			return List.IndexOf(value);
		}

		public int Add(TimeToCall value) {
			return List.Add(value);
		}

		#endregion

		#region public static methods

		private static TimeToCallCollections Create() {
			TimeToCallCollections oColl = new TimeToCallCollections();
			DatabaseObject oDb = new DatabaseObject();
			foreach(DataRow feRow in oDb.GetBestTimeToCall().Rows)
				oColl.Add(new TimeToCall(feRow["best_time_call"].ToString(),
					feRow["best_time_call_desc"].ToString()));
			System.Web.HttpContext.Current.Application["TimeToCall"] = oColl;
			return oColl;
		}

		public static TimeToCallCollections Load() {
			TimeToCallCollections oColl = new TimeToCallCollections();
			if(System.Web.HttpContext.Current.Application != null) {
				if(System.Web.HttpContext.Current.Application["TimeToCall"] != null)
					return (TimeToCallCollections)System.Web.HttpContext.Current.Application["TimeToCall"];
				else
					return Create();
			}
			return oColl;
		}

		#endregion
	}
}
