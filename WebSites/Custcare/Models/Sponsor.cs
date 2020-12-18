using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GA.BDC.Web.Custcare.Models
{
    public class Sponsor
    {
        public string PartnerName { get; set; }
        public string GroupName { get; set; }
        public bool EventActive { get; set; }
        public DateTime? EventCreateDate { get; set; }
        public DateTime? EventEndDate { get; set; }
        public string EventName { get; set; }
        public int EventId { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int EventParticipationId { get; set; }
        public int Unsubscribed { get; set; }
        public int GroupId { get; set; }
        public string MovieTicket { get; set; }
        public Models.Prize EarnedPrize { get; set; }
        public string Comments { get; set; }
    }
}