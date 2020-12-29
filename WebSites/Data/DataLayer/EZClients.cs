using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;
using System.Transactions;
using System.Configuration;

namespace GA.BDC.Data.DataLayer
{
    public class EZClients
    {
        public static ORG_CTCT_TBL GetClient(int? orgID)
        {

            using (EZMainCloudDataContext dc = new EZMainCloudDataContext(ConfigurationManager.ConnectionStrings["GA.BDC.Console.TaskExecutor.Properties.Settings.EZMainConnectionString"].ConnectionString))
            {
                return (from s in dc.ORG_CTCT_TBLs
                        where s.ORG_ID == orgID && s.CTCT_SEQ_NBR == 1
                        select s).FirstOrDefault();
            }
        }
    }
}
