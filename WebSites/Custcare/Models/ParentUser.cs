using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GA.BDC.Web.Custcare.Models
{
    public class ParentUser : User
    {
        public int MemberHierarchyId { get; set; }
        public int EventParticipationId { get; set; }
        public string SelectDisplay
        {
            get
            {
                return string.Concat(this.FirstName,
                                     " ",
                                     this.LastName,
                                     " - ",
                                     this.EmailAddress);
            }
        }
    }
}