using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QSP.OrderExpress.Common.Data
{
    [Serializable]
    public class OrganizationLevelData
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ARSLEV { get; set; }
    }
}
