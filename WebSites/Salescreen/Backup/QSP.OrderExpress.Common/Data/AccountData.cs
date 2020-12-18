using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QSP.OrderExpress.Common.Data
{
    [Serializable]
    public class AccountData
    {
        public int Id { get; set; }
        public int? EdsId { get; set; }
        public string Name { get; set; }
        public string TaxExemptionNumber { get; set; }
        public DateTime? TaxExemptionExpirationDate { get; set; }
        public string Comments { get; set; }

        public int? StatusId { get; set; }
        public string StatusName { get; set; }
        public string StatusColor { get; set; }

        public string FsmId { get; set; }
        public string FsmFirstName { get; set; }
        public string FsmLastName { get; set; }

        public bool IsDeleted { get; set; }
        public DateTime CreateDate { get; set; }
        public int CreateUserId { get; set; }
        public string CreateUserFirstName { get; set; }
        public string CreateUserLastName { get; set; }
        public DateTime? UpdateDate { get; set; }
        public int? UpdateUserId { get; set; }
        public string UpdateUserFirstName { get; set; }
        public string UpdateUserLastName { get; set; }

        public decimal? CollectionAmouunt { get; set; }
        public DateTime? CollectionDate { get; set; }

        public OrganizationData Organization { get; set; }
        public CampaignData LastCampaign { get; set; }
        public List<BusinessExceptionData> AccountNotes { get; set; }
        public AddressData BillingAddress { get; set; }
        public AddressData ShippingAddress { get; set; }
        public List<StatusHistoryData> StatusHistory { get; set; }
    }
}
