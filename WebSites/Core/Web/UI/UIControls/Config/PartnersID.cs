using System;
using System.Xml;
using System.Xml.Serialization;
using System.Collections;

namespace GA.BDC.Core.Web.UI.UIControls.Config
{
	/// <summary>
	/// Summary description for PartnersID.
	/// </summary>
	[Serializable, XmlInclude(typeof(PartnerID))]
	public class PartnersID {
		private ArrayList partnerIdList;

		public PartnersID() {
			partnerIdList = new ArrayList();
		}

		/// <summary>
		/// Parse xml tags
		/// </summary>
		/// <param name="node"></param>
		public void Load(XmlNode node) {
			foreach(XmlNode child in node) {
				if(child.Name.ToLower() == "PartnerID".ToLower()) {
					PartnerID partnerID = new PartnerID();
					partnerID.Load(child);
					partnerIdList.Add(partnerID);
				}
			}
		}
		
		public ArrayList PartnerIdList {
			set { partnerIdList = value; }
			get { return partnerIdList; }
		}
	}
}
