/* Title:	PartnerLink
 * Author:	Jiro Hidaka
 * Summary:	Partner id for one country will not be preserved for the other countries. A completely new id  
 *			will be generated per country. For instance, partner id 0 (efundraising.com) which corresponds
 *			to USA is linked to partner id 741 (efundraising.ca) for Canada.
 * 
 * Create Date:	November 14, 2008
 * 
 */

using System;

namespace GA.BDC.Core.ESubsGlobal
{
    /// <summary>
    /// Summary description for PartnerLink.
    /// </summary>
    [Serializable]
    public class PartnerLink : EnvironmentBase 
    {
        private int partnerID = int.MinValue;
		private string culture_code;
		private int linked_partnerID = int.MinValue;

		public PartnerLink() {

		}

        public PartnerLink(int _pid, string _culture, int _lpid)
        {
            partnerID = _pid;
            culture_code = _culture;
            linked_partnerID = _lpid;
		}

		#region Properties
		public int PartnerID 
        {
            set { partnerID = value; }
            get { return partnerID; }
		}

        public string CultureCode
        {
            set { culture_code = value; }
            get { return culture_code; }
		}

		public int LinkedPartnerID 
        {
            set { linked_partnerID = value; }
            get { return linked_partnerID; }
		}
		#endregion
    }
}
