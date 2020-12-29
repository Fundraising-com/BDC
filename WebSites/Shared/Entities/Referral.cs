using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GA.BDC.Shared.Entities
{
    public class Referral
    {
        /// <summary>
        /// Referral Code
        /// </summary>
        public string ReferralCode { get; set; }
        /// <summary>
        /// Referral Sequence Number
        /// </summary>
        public int ReferralSequenceNumber { get; set; }
        /// <summary>
        /// Referral Display Name
        /// </summary>
        public string ReferralTxt { get; set; }
        /// <summary>
        /// Referral Lead Flag
        /// </summary>
        public bool ReferralLeadFlag { get; set; }
        /// <summary>
        /// Referral Active Flag
        /// </summary>
        public bool ReferralActiveFlag { get; set; }
    }
}
