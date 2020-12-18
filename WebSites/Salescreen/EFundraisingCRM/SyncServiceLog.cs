using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace efundraising.EFundraisingCRM
{
    public class SyncServiceLog
    {
        #region Methods

        public static string GetLatestSyncDate(ref DateTime lastestSyncDate)
        {

            DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase(true);
            return dbo.GetLatestSyncDate(ref lastestSyncDate);
        }

        public static string InsertSyncLog(ref DateTime currentSyncTime)
        {

            DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase(true);
            return dbo.InsertSyncLog(ref currentSyncTime);
        }


        public static string SetSyncLogByDate(bool isSuccessful, DateTime currentSyncTime)
        {

            DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase(true);
            return dbo.SetSyncStatus(isSuccessful,currentSyncTime);
        }
        #endregion
    }
}
