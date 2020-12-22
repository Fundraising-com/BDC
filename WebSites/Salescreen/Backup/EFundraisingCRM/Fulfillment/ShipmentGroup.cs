using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace efundraising.EFundraisingCRM.Fulfillment
{
    public class ShipmentGroup
    {

        #region Methods

        public static List<ShipmentGroupsResult> GetShipmentGroupsUpdated(DateTime lastRunDate)
        {

            DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase(true);
            return dbo.GetShipmentGroupsUpdated(lastRunDate);
        }


        #endregion



    }
}
