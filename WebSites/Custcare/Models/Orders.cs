using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GA.BDC.Web.Custcare.Models
{
    public class Orders
    {
        public List<Models.Order> SalesInfo { get; set; }
        public List<Models.ParentUser> ParentUsers { get; set; }
        public string ProfitStatementUrl { get; set; }
    }
}