using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GA.BDC.Web.Custcare.Models
{
    public class Partner
    {
        public int PartnerId { get; set; }
        public string PartnerName { get; set; }
        public int PartnerTypeId { get; set; }
        public string PartnerTypeName { get; set; }
        public int PartnerPromotionId { get; set; }
        public int PartnerPromotionName { get; set; }
        public bool HasCollectionSite { get; set; }
        public Guid Guid { get; set; }
        public DateTime CreateDate { get; set; }
        public bool IsActive { get; set; }
    }
}