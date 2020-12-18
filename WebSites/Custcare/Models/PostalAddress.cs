using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GA.BDC.Web.Custcare.Models
{
    public class PostalAddress
    {
        public int PostalAddressId { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public string CountryCode { get; set; }
        public string SubdivisionCode { get; set; }
    }
}