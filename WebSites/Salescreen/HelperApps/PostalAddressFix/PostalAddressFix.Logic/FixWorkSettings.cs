using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PostalAddressFix.Logic
{
    public class FixWorkSettings
    {
        public string ConnectionString { set; get; }
        public int? Page { set; get; }
        public int PageSize { set; get; }
        public bool UpdateData { set; get; }
        public int DelayInMiliseconds { set; get; }
    }
}
