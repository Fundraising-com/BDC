using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GA.BDC.Web.Custcare.Models
{
    public class Event
    {
        public int EventId { get; set; }
        public int EventTypeId { get; set; }
        public string CultureCode { get; set; }
        public string EventName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool Active { get; set; }
        public string Comments { get; set; }
        public DateTime CreateDate { get; set; }
        public string Redirect { get; set; }
        public bool Displayable { get; set; }
        public bool? WantSalesRepCall { get; set; }
        public int GroupTypeId { get; set; }
        public bool? ProcessingFee { get; set; }
        public double ProfitCalculated { get; set; }
        public int EventStatusId { get; set; }
        public int ProfitGroupId { get; set; }
        public bool Donation { get; set; }
        public DateTime? DateOfEvent { get; set; }
        public bool? DiscountSite { get; set; }
    }
}