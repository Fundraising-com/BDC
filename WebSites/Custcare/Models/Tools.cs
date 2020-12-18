using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GA.BDC.Web.Custcare.Models
{
    public class Tools
    {
        public List<MemberPassword> MemberPasswords { get; set; }
        public int SelectedPartnerId { get; set; }
        public int SelectedUserId { get; set; }
        public string NewPassword { get; set; }
    }
}