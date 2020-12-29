using System;
using System.Collections.Generic;
using System.Linq;
using GA.BDC.Core.ESubsGlobal;
using GA.BDC.Core.ESubsGlobal.DataAccess;

namespace GA.BDC.Core.ESubsGlobal.Donation
{
    public class Comments
    {
        #region Members
        public int EventParticipationID { get; set; }
        public int EventID { get; set; }
        public int MemberHierarchyID { get; set; }
        public string DonorName { get; set; }
        public decimal DonationAmount { get; set; }
        public string DonorComments { get; set; }
        #endregion

        #region Methods
        public static List<Comments> GetDonorCommentsByPartnerID(Int32 partner_id)
        {
            ESubsGlobalDatabase dbo = new ESubsGlobalDatabase();
            return dbo.GetDonorCommentsByPartnerID(partner_id);
        }
        #endregion
    }
}
