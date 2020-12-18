using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GA.BDC.Web.Custcare.Models
{
    public class Prize
    {
        public int PrizeId { get; set; }
        public int? EventParticipationId { get; set; }
        public string PrizeItemCode { get; set; }
        public DateTime? DateIssued { get; set; }
        public DateTime ExpirationDate { get; set; }
        public bool NewMovieCode { get; set; }
    }
}