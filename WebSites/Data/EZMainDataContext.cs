using System;
using GA.BDC.Data.Properties;

namespace GA.BDC.Data
{
    public partial class EZMainDataContext : System.Data.Linq.DataContext
    {
        partial void OnCreated()
        {
            this.CommandTimeout = Settings.Default.CommandTimeout;
        }
    }
}

