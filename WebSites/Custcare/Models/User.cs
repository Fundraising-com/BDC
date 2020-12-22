using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GA.BDC.Web.Custcare.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string CultureCode { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int? PartnerId { get; set; }
        public DateTime CreateDate { get; set; }
        public int? MemberId { get; set; }
        public int? CoppaMonth { get; set; }
        public int? CoppaYear { get; set; }
        public bool? AgreeTermServices { get; set; }
        public bool? Unsubscribe { get; set; }
        public DateTime? UnsubscribeDate { get; set; }
        public bool OptStatusId { get; set; }
        public bool? IsFirstLogin { get; set; }
    }
}