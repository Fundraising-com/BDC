using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QSP.OrderExpress.Common.Data
{
    [Serializable]
    public class OrganizationData
    {
        public int Id { get; set; }
        public BusinessDivisionData BusinessDivision { get; set; }
        public OrganizationTypeData Type { get; set; }
        public OrganizationLevelData Level { get; set; }
        public int? StatusId { get; set; }
        public string Name { get; set; }
        public string FmId { get; set; }
        public string TaxExemptionNumber { get; set; }
        public DateTime? TaxExemptionExpirationDate { get; set; }
        public string Comments { get; set; }

        public AddressData BillingAddress { get; set; }
        public AddressData ShippingAddress { get; set; }

        public bool IsDeleted { get; set; }
        public DateTime CreateDate { get; set; }
        public int CreateUserId { get; set; }
        public DateTime? UpdateDate { get; set; }
        public int? UpdateUserId { get; set; }

        public string MDRPID { get; set; }
        public string ARNMBL { get; set; }
        public int? FlagpoleInstance { get; set; }
        public int? CAccountId { get; set; }
    }
}
