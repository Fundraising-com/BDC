using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GA.BDC.Web.Custcare.Models
{
    public class PaymentInfo
    {
        public int PaymentInfoId { get; set; }
        public int? GroupId { get; set; }
        public int? EventId { get; set; }
        public int? PostalAddressId { get; set; }
        public int? PhoneNumberId { get; set; }
        public string PaymentName { get; set; }
        public string OnBehalfOfName { get; set; }
        public string ShipToName { get; set; }
        public string SSN { get; set; }
        public bool Active { get; set; }
        public DateTime CreateDate { get; set; }
    }
}