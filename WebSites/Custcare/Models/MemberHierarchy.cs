using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GA.BDC.Web.Custcare.Models
{
    public class MemberHierarchy
    {
        public int MemberHierarchyId { get; set; }
        public int ParentMemberHierarchyId { get; set; }
        public int MemberId { get; set; }
        public int CreationChannelId { get; set; }
        public DateTime CreateDate { get; set; }
        public bool Active { get; set; }
        public bool Unsubscribe { get; set; }
        public DateTime? UnsubscribeDate { get; set; }
    }
}