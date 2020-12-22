using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GA.BDC.Web.Custcare.Models
{
    public class Member
    {
        public int MemberId { get; set; }
        public string CultureCode { get; set; }
        public int OptStatusId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public bool Bounced { get; set; }
        public string ExternalMemberId { get; set; }
        public string Comments { get; set; }
        public DateTime CreateDate { get; set; }
        public string ParentFirstName { get; set; }
        public string ParentLastName { get; set; }
        public int? PartnerId { get; set; }
        public int? LeadId { get; set; }
        public bool? Unsubscribe { get; set; }
        public DateTime? UnsubscribeDate { get; set; }
        public int? FacebookId { get; set; }
        public int? UserId { get; set; }
        public bool Deleted { get; set; }
        public string Greeting { get; set; }
        public bool? EmailValidated { get; set; }
        public int? EmailValidationResponseCode { get; set; }
        public string EmailValidationResponseMessage { get; set; }

        List<MemberHierarchy> MemberHierarchyIds { get; set; }
    }
}