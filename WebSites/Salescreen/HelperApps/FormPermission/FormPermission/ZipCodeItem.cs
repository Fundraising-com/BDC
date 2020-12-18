using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FormPermission
{
    public class ZipCodeItem
    {
        public string ZipCode { get; set; }
        public string Description { get; set; }
        public bool IsAllowed { get; set; }
        public bool IsDenied { get; set; }
    }
}
