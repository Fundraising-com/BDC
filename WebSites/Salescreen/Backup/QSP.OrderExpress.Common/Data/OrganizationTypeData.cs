using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QSP.OrderExpress.Common.Data
{
    [Serializable]
    public class OrganizationTypeData
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ARSTYP { get; set; }
    }
}
