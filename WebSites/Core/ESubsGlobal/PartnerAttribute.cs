/* Title:	PartnerAttribute
 * Author:	Jean-Francois Buist
 * Summary:	Partners can have many different attributes, these are called by the 
 *			Partner object as Attributes, please document these attributes every
 *			time one is added.
 * 
 * Create Date:	August 1, 2005
 * 
 */

using System;

namespace GA.BDC.Core.ESubsGlobal {
	/// <summary>
	/// Summary description for PartnerAttribute.
	/// </summary>
    [Serializable]
	public class PartnerAttribute : EnvironmentBase {
        private int partnerAttributeID = int.MinValue;
		private string name;
		private string val;

		public PartnerAttribute() {

		}

		public PartnerAttribute(int _id, string _name, string _val) {
			partnerAttributeID = _id;
			name = _name;
			val = _val;
		}

		#region Properties
		public int PartnerAttributeID {
			set { partnerAttributeID = value; }
			get { return partnerAttributeID; }
		}

		public string Name {
			set { name = value; }
			get { return name; }
		}

		public string Value {
			set { val = value; }
			get { return val; }
		}
		#endregion
	}
}
