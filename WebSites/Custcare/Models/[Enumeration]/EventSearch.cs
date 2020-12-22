using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GA.BDC.Web.Custcare.Models
{
    public enum EventSearch : int
    {
        ByEventId = 1,
        ByLeadId = 2,
        ByEventName = 3,
        ByEmail = 4,
        BySponsorName = 5
    }
}