using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GA.BDC.Web.MGP.Models.Views
{
    public class TopSeller
    {
        public decimal Amount { get; set; }
        public decimal Donation { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public DateTime Date { get; set; }
    }
}