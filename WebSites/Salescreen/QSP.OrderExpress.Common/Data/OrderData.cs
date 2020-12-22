using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QSP.OrderExpress.Common.Data
{
    [Serializable]
    public class OrderData
    {
        public int Id { get; set; }
        public string EdsId { get; set; }
        public int? FormId { get; set; }
        public string FormName { get; set; }

        public DateTime OrderDate { get; set; }
        public int OrderTypeId { get; set; }
        public string OrderTypeName { get; set; }
        public int? DeliveryMethodId { get; set; }
        public string DeliveryMethodName { get; set; }
        public DateTime? ReqestedDeliveryDate { get; set; }
        public DateTime? ReqestedDeliveryTime { get; set; }
        public string ReqestedDeliveryDateText { get; set; }
        public int RequestedLeadTimeBusinessDays { get; set; }
        public int? DeliveryWarehouseId { get; set; }
        public string DeliveryWarehouseName { get; set; }
        public DateTime? ShippingDate { get; set; }
        public string CustomerPONumber { get; set; }
        public decimal ProfitRate { get; set; }
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

        public CampaignData Campaign { get; set; }
        public AddressData ShippingAddress { get; set; }
        public OrderSummaryData OrderSummary { get; set; }
        public List<OrderDetailData> OrderDetails { get; set; }
        public List<ChargeData> OrderCharges { get; set; }
        public List<StatusHistoryData> StatusHistory { get; set; }
    }
}
