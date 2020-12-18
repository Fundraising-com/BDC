using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GA.BDC.Web.Custcare.Models
{
    public class Group
    {
        public int GroupId { get; set; }
        public int SponsorId { get; set; }
        public int PartnerId { get; set; }
        public int? LeadId { get; set; }
        public string ExternalGroupId { get; set; }
        public string GroupName { get; set; }
        public int? ExpectedMemberShip { get; set; }
        public string Comments { get; set; }
        public DateTime CreateDate { get; set; }
    }
}