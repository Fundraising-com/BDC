using System;
using System.Data;

using efundraising.Database.efundraising;

namespace efundraising.efundraisingCore.FormObject {

	public class OrganizationCollections : System.Collections.CollectionBase {
	
		public OrganizationCollections() {}
		
		#region IList Members

		public Organization this[int index] {
			get{ return (Organization)List[index]; }
		}

		public Organization this[string descriptionName] 
		{
			get 
			{
				for(int i=0;i<List.Count;i++) 
				{
					if(((Organization)List[i]).OrganizationDescription == descriptionName)
						return (Organization)List[i];
				}
				return null;
			}
		}

		public Organization GetOrganizationById(int OrganizationId) {
			foreach(Organization feOrg in (Organization[])List) {
				if(feOrg.OrganizationId == OrganizationId)
					return feOrg;
			}
			return new Organization();			
		}

		public void Insert(int index, Organization value) {
			List.Insert(index, value);
		}

		public void Remove(Organization value) {
			List.Remove(value);
		}

		public bool Contains(Organization value) {
			return List.Contains(value);
		}

		public int IndexOf(Organization value) {
			return List.IndexOf(value);
		}

		public int Add(Organization value) {
			return List.Add(value);
		}


		#endregion

		#region public static methods

		private static OrganizationCollections Create() {
			OrganizationCollections oColl = new OrganizationCollections();
			DatabaseObject oDb = new DatabaseObject();
			//organization_type_id, organization_type_desc
			foreach(DataRow feRow in oDb.GetOrganizationType().Rows)
				oColl.Add(new Organization(int.Parse(feRow["organization_type_id"].ToString()),
					feRow["organization_type_desc"].ToString()));
			System.Web.HttpContext.Current.Application["OrganizationCollections"] = oColl;
			return oColl;
		}

		public static OrganizationCollections Load() {
			OrganizationCollections oColl = new OrganizationCollections();
			if(System.Web.HttpContext.Current.Application != null) {
				if(System.Web.HttpContext.Current.Application["OrganizationCollections"] != null)
					return (OrganizationCollections)System.Web.HttpContext.Current.Application["OrganizationCollections"];
				else
					return Create();
			}
			return oColl;
		}

		#endregion

	}
}
