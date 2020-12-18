//
//	Louis Turmel	-	June 27, 2005	-	class creation and documentation
//

using System;
using System.Data;

namespace efundraising.efundraisingCore {

	/// <summary>
	/// class factory for efundraising.efundraising.Partner object
	/// </summary>
	public sealed class PartnerCtrl {

		#region internal constructor

		internal PartnerCtrl() {

		}

		#endregion

		#region public static methods

		/// <summary>
		/// Method to get the Partner Business Object from an DataTable object
		/// </summary>
		/// <param name="pPartnerTable">DataTable containing the Partner Information</param>
		/// <returns>Partner Business Object</returns>
		public static Partner ExtractPartnerInfo(DataTable pPartnerTable) {
			Partner oPartner = new Partner();
			foreach(DataRow feRow in pPartnerTable.Rows) {
				#region Set the Partner Fields values
				oPartner.PartnerID = int.Parse(feRow["partner_id"].ToString());
				oPartner.PartnerGroupTypeID = int.Parse(feRow["partner_group_type_id"].ToString());
				oPartner.PartnerName = feRow["partner_name"].ToString();
				oPartner.PartnerPath = feRow["partner_path"].ToString();
				oPartner.ESubsUrl = feRow["eSubs_url"].ToString();
				oPartner.EStoreUrl = feRow["eStore_url"].ToString();
				oPartner.PhoneNumber = feRow["phone_number"].ToString();
				oPartner.Url = feRow["url"].ToString();
				oPartner.GUID = feRow["guid"].ToString();
				if(bool.Parse(feRow["prize_eligible"].ToString()))
					oPartner.PrizeEligible = true;
				if(bool.Parse(feRow["has_collection_site"].ToString()))
					oPartner.HasCollectionSite = true;
				#endregion
			}
			return oPartner;
		}

		#endregion
	}
}
