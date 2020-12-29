/* Title:	Partner Link Collection
 * Author:	Jiro Hidaka
 * Summary:	Collection of partner links.
 * 
 * Create Date:	November 14, 2008
 * 
 */

using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace GA.BDC.Core.ESubsGlobal
{
    /// <summary>
    /// Summary description for PartnerLinkCollection.
    /// </summary>
    [Serializable, XmlInclude(typeof(PartnerLink))]
    public class PartnerLinkCollection : EnvironmentBase 
    {
        private List<PartnerLink> partnerLinks;

        public PartnerLinkCollection()
        {
            partnerLinks = new List<PartnerLink>();
		}

		public void Add(int _pid, string _culture, int _lpid) {
            Add(new PartnerLink(_pid, _culture, _lpid));
		}

        public void Add(PartnerLink pa)
        {
            partnerLinks.Add(pa);
		}

		public PartnerLink GetPartnerLinkByCultureCode(string culture) {
            foreach (PartnerLink p in partnerLinks)
            {
                if (p.CultureCode.Equals(culture, StringComparison.CurrentCultureIgnoreCase))
                {
					return p;
				}
			}
			return null;
		}

        public PartnerLink GetPartnerLinkByCountryCode(string country_code)
        {
            foreach (PartnerLink p in partnerLinks)
            {
                Culture c = Culture.Create(p.CultureCode);
                if (c != null)
                {
                    if (c.CountryCode.Equals(country_code, StringComparison.CurrentCultureIgnoreCase))
                    {
                        return p;
                    }
                }                
            }
            return null;
        }

        public string GetCultureCodeByPartnerID()
        {
            foreach (PartnerLink p in partnerLinks)
            {
                if (p.PartnerID == p.LinkedPartnerID)
                {
                    return p.CultureCode;
                }
            }
            return null;
        }

		public PartnerLink[] GetPartnerLink() {
			return partnerLinks.ToArray();
		}
    }
}
