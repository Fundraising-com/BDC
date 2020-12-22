using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GA.BDC.Web.MGP.Models.Views
{
    public class TopParticipant
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal AmountRaised { get; set; }
        public decimal Goal { get; set; }
        public string Image { get; set; }
        public int EventId { get; set; }
        public string EventName { get; set; }
    }
}