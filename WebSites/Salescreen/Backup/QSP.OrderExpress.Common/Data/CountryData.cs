using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QSP.OrderExpress.Common.Data
{
    [Serializable]
    public class CountryData
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string DescriptiveInformation { get; set; }
    }
}
