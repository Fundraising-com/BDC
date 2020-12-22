using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QSP.OrderExpress.Common.Data
{
    [Serializable]
    public class ProgramAgreementData
    {
        public int Id { get; set; }
        public string EdsId { get; set; }
        public int? FormId { get; set; }
        public string FormName { get; set; }
        public int? StatusId { get; set; }
        public string StatusName { get; set; }
        public string StatusColor { get; set; }
        public string Comments { get; set; }

        public string TaxExemptionNumber { get; set; }
        public DateTime? TaxExemptionExpirationDate { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime? HolidayStartDate { get; set; }
        public DateTime? HolidayEndDate { get; set; }
        public decimal GoalEstimatedGross { get; set; }
        public int Enrollment { get; set; }
        public decimal AccountProfitRate { get; set; }
        public int RenewalSignupTerm { get; set; }
        public bool IsPriced { get; set; }

        public bool IsDeleted { get; set; }
        public DateTime CreateDate { get; set; }
        public int CreateUserId { get; set; }
        public string CreateUserFirstName { get; set; }
        public string CreateUserLastName { get; set; }
        public DateTime? UpdateDate { get; set; }
        public int? UpdateUserId { get; set; }
        public string UpdateUserFirstName { get; set; }
        public string UpdateUserLastName { get; set; }

        public CampaignData Campaign { get; set; }
        public AddressData ShippingAddress { get; set; }
        public List<CatalogData> Catalogs { get; set; }
        public List<StatusHistoryData> StatusHistory { get; set; }
    }
}
