using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QSP.OrderExpress.Common.Search
{
    public enum SearchFSMHierarchyOptionEnum : int
    {
        All = 0,
        Own = 1,            // Own  accounts
        Children = 2,       // Direct reports
        OwnAndChildren = 3  // Own accounts and direct reports
    }
}
