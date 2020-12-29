﻿using GA.BDC.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GA.BDC.Shared.Data.Repositories
{
    public interface IReferralRepository : IRepository<Referral>
    {
        IList<Referral> GetLeadActiveReferrals();
    }
}
