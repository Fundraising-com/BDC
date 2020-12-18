using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GA.BDC.Web.Custcare.Models
{
    public class AddressState
    {
        public string StateName { get; set; }
        public string CountryCode { get; set; }        
        public string SubdivisionCode { get; set; }
        public string StateCode
        {
            get
            {
                if (string.IsNullOrEmpty(SubdivisionCode))
                    return null;
                else if (SubdivisionCode.Length == 2)
                    return SubdivisionCode;
                else
                    return SubdivisionCode.Substring(SubdivisionCode.Length - 2);
            }
        }        
    }
}