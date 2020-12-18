using System;

namespace efundraising.efundraisingCore.Culture 
{

	[Serializable()]
	public class CultureCaptionCollections : System.Collections.CollectionBase {
		
		#region public properties

		public CultureCaption this[Culture culture] {
			get	{
				CultureCaption currentGet = new CultureCaption();
				for(int i=0;i<List.Count;i++) {
					if(((CultureCaption)List[i]).culture.CultureCode == culture.CultureCode)
						currentGet = (CultureCaption)List[i];
				}
				return currentGet;
			} 
		}

		public CultureCaption this[string cultureCode] {
			get {
				CultureCaption currentGet = new CultureCaption();
				for(int i=0;i<List.Count;i++) {
					if(((CultureCaption)List[i]).culture.CultureCode == cultureCode)
						currentGet = (CultureCaption)List[i];
				}
				return currentGet;
			}
		}

		#endregion


		#region IList Members

		public CultureCaption this[int index] {
			get{ return (CultureCaption)List[index]; }
		}

		public void Insert(int index, CultureCaption value) {
			List.Insert(index, value);
		}

		public void Remove(CultureCaption value) {
			List.Remove(value);
		}

		public bool Contains(CultureCaption value) {
			return List.Contains(value);
		}	

		public int IndexOf(CultureCaption value) {
			return List.IndexOf(value);
		}

		public int Add(CultureCaption value) {
			bool cultureExist = false;
			//foreach(CultureCaption feCaption in (CultureCaption[])List) {
			for(int i=0;i<List.Count;i++) {
				if(((CultureCaption)List[i]).culture == value.culture)
					cultureExist = true;
			}
			if(cultureExist)
				return -1;
			else
				return List.Add(value);
		}


		#endregion
	}
}
