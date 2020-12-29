using System;
using System.Collections.Generic;
using System.Text;

namespace GA.BDC.Core.eFundraisingStore
{
   public  class packageInterest
    {
        public int Package_interest_id { get; set; }
        public int PackageId { get; set; }
        

        public static List<packageInterest> GetPackageInterestByPackageID(int id)
        {
            DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
            return dbo.GetPackageInterestByPackageID(id);
        }
    }


}
