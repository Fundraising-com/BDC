using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PostalAddressFix.Logic
{
    public class FixWorkState
    {
        public int Count { set; get; }
        public int Total { set; get; }
        public int? PostalAddressId { set; get; }
        public string OriginalZip { set; get; }
        public string NewZip { set; get; }
        public bool IsZipValid { set; get; }
    }
}
