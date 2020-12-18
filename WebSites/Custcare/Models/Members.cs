using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GA.BDC.Web.Custcare.Models
{
    public class Members
    {
        public string Name { get; set; }
        public int EmailsSent { get; set; }
        public int? NumOfSubs { get; set; }
        public decimal? AmountSold { get; set; }
        public double Profit { get; set; }
        public int EventParticipationId { get; set; }
        public int MemberHierarchyId { get; set; }
        public int? ParentMemberHierarchyId { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public int BouncedCount { get; set; }
        public bool Bounced { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? OrderDate { get; set; } 
        public string MovieTicket { get; set; }
        public bool Active { get; set; }
        public bool Unsubscribed { get; set; }
        public bool Deleted { get; set; }
    }
}