using System;
using GA.BDC.Data.Properties;

namespace GA.BDC.Data
{
    public partial class EZMainCloudDataContext : System.Data.Linq.DataContext
    {
        partial void OnCreated()
        {
            this.CommandTimeout = Settings.Default.CommandTimeout;
        }
    }
}