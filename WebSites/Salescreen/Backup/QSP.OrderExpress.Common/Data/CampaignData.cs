using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QSP.OrderExpress.Common.Data
{
    [Serializable]
    public class CampaignData
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public int ProgramTypeId { get; set; }
        public string ProgramTypeName { get; set; }
        public int? TradeClassId { get; set; }
        public string TradeClassName { get; set; }
        public int? WarehouseId { get; set; }
        public string WarehouseName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal? GoalEstimatedGross { get; set; }
        public int Enrollment { get; set; }
        public int FiscalYear { get; set; }
        public DateTime? LastOrderDate { get; set; }
        public string InactiveMonths { get; set; }
    }
}
