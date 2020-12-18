using System;
using System.Data;
using efundraising.Database.efundraising;

namespace efundraising.efundraisingCore.FormObject{
	
	public class TitleCollections : System.Collections.CollectionBase {

		public TitleCollections() {}

		public static TitleCollections Create() {
			TitleCollections oColl = new TitleCollections();
			DatabaseObject oDb = new DatabaseObject();
			foreach(DataRow feRow in oDb.GetTitle().Rows)
				oColl.Add(new Title(int.Parse(feRow["Title_ID"].ToString()),
					feRow["Title_Desc"].ToString()));
			System.Web.HttpContext.Current.Application["TitleCollections"] = oColl;
			return oColl;
		}

		public static TitleCollections Load() {
			if(System.Web.HttpContext.Current.Application["TitleCollections"] != null)
				return (TitleCollections)System.Web.HttpContext.Current.Application["TitleCollections"];
			else
				return Create();
		}

		public Title this[string descriptionName] {
			get {
				for(int i=0;i<List.Count;i++) {
					if(((Title)List[i]).TitleDescription == descriptionName)
						return (Title)List[i];
				}
				return null;
			}
		}

		#region IList Members

		public Title this[int index] 
		{
			get{ return (Title)List[index]; }
		}

		public void Insert(int index, Title value) 
		{
			List.Insert(index, value);
		}

		public void Remove(Title value) 
		{
			List.Remove(value);
		}

		public bool Contains(Title value) 
		{
			return List.Contains(value);
		}

		public int IndexOf(Title value) 
		{
			return List.IndexOf(value);
		}

		public int Add(Title value) 
		{
			return List.Add(value);
		}

		#endregion
	}
}
