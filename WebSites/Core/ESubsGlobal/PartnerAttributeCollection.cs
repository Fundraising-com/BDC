/* Title:	Partner Attribute Collection
 * Author:	Jean-Francois Buist
 * Summary:	Collection of partner attributes.
 * 
 * Create Date:	August 1, 2005
 * 
 */

using System;
using System.Collections;
using System.Xml.Serialization;

namespace GA.BDC.Core.ESubsGlobal {
	/// <summary>
	/// Summary description for PartnerAttributeCollection.
	/// </summary>
	[Serializable, XmlInclude(typeof(PartnerAttribute))]
	public class PartnerAttributeCollection : EnvironmentBase {
		private ArrayList partnerAttributes;

		public PartnerAttributeCollection() {
			partnerAttributes = new ArrayList();
		}

		public void Add(int _id, string _name, string _val) {
			Add(new PartnerAttribute(_id, _name, _val));
		}

		public void Add(PartnerAttribute pa) {
			partnerAttributes.Add(pa);
		}

		public PartnerAttribute GetPartnerAttributeByID(int id) {
			foreach(PartnerAttribute p in partnerAttributes) {
				if(p.PartnerAttributeID == id) {
					return p;
				}
			}
			return null;
		}

		public PartnerAttribute GetPartnerAttributeByName(string name) {
			foreach(PartnerAttribute p in partnerAttributes) {
				if(p.Name.ToLower() == name.ToLower()) {
					return p;
				}
			}
			return null;
		}

		public PartnerAttribute[] GetPartnerAttribute() {
			PartnerAttribute[] p = new PartnerAttribute[partnerAttributes.Count];
			for(int i=0;i<partnerAttributes.Count;i++) {
				p[i] = (PartnerAttribute)partnerAttributes[i];
			}
			return p;
		}
	}
}
