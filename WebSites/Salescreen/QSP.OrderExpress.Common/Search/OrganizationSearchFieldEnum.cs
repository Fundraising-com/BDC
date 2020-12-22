using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QSP.OrderExpress.Common.Search
{
    public enum OrganizationSearchFieldEnum : int
    {
        Any = 0,
        QSPOrganizationId = 1,
        Name = 2,
        NameBeginingWith = 3,
        City = 4,
        ZipCode = 5
    }
}
