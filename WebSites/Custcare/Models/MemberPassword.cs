using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GA.BDC.Web.Custcare.Models
{
    public class MemberPassword
    {
        public int UserId { get; set; }
        public int PartnerId { get; set; }
        public string PartnerName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }      
    }
}