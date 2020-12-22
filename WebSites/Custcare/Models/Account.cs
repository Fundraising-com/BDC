using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GA.BDC.Web.Custcare.Models
{
    public class Account
    {
        public Group Group { get; set; }
        public Partner Partner { get; set; }
        public PostalAddress ShippingAddress { get; set; }
        public PaymentInfo PaymentInfo { get; set; }
        public List<AddressState> ShippingStates { get; set; }
        public int? LinkToEventId { get; set; }
    }
}