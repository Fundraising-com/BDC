using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using GA.BDC.Data.MGP.Helpers;

namespace GA.BDC.Data.MGP.esubs_global_v2.LINQ
{
    public partial class DataProviderDataContext : System.Data.Linq.DataContext
    {
        partial void OnCreated()
        {
            CommandTimeout = 60 * 5;
            Log = new Log4NetWriter();
        }
    }
}
