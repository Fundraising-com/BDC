using System;
using System.Data;	

using efundraising.Database.efundraising;

namespace efundraising.efundraisingCore.Geography
{
	
	public class StateCollections : System.Collections.CollectionBase {
		
		public StateCollections() { 
			
		}

		#region IList Members

		public State this[int index] {
			get{ return (State)List[index]; }
		}

		public State this[string pStateCode] {
			get
			{
				foreach (State feSt in this) 
				{
					if(feSt.StateCode == pStateCode)
						return feSt;
				}
				return null;
			}
		}

		public void Insert(int index, State value) {
			List.Insert(index, value);
		}

		public void Remove(State value) {
			List.Remove(value);
		}

		public bool Contains(State value) {
			return List.Contains(value);
		}

		public int IndexOf(State value){
			return List.IndexOf(value);
		}

		public int Add(State value){
			return List.Add(value);
		}

		#endregion

		#region public static methods

		public static StateCollections Create(string pCountryCode) {
			StateCollections oStColl = new StateCollections();
			DatabaseObject oDb = new DatabaseObject();
			foreach(DataRow feRow in oDb.GetStateByCountryCode(pCountryCode).Rows) 
				oStColl.Add(new State(feRow["state_code"].ToString(), feRow["state_name"].ToString()));
			return oStColl;
		}

		#endregion
	}
}
