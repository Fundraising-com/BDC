using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GA.BDC.Web.Custcare.Models
{
    public class Order
    {
        public int MemberHierarchyId { get; set; }
        public string MemberName { get; set; }
        public int? NbSubs { get; set; }
        public decimal? Amount { get; set; }
        public string CustomerName { get; set; }
        public string CatalogItemTitle { get; set; }
        public DateTime? OrderDate { get; set; }
        public int? OrderID { get; set; }
        public int? EdsId { get; set; }
        public int? ParentMemberHierarchy_id { get; set; }
        public string ParentName { get; set; }
        public int? UserType { get; set; }
        public int EventParticipationId { get; set; }
    }
}