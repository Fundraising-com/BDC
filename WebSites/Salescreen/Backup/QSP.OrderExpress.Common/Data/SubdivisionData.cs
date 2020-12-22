using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QSP.OrderExpress.Common.Data
{
    [Serializable]
    public class SubdivisionData
    {
        public string Code { get; set; }
        public CountryData Country { get; set; }
        public string Name1 { get; set; }
        public string Name2 { get; set; }
        public string Name3 { get; set; }
        public string RegionalDivision { get; set; }
        public string Category { get; set; }
    }
}
