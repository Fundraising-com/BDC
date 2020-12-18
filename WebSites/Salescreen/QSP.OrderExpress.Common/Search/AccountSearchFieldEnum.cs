using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QSP.OrderExpress.Common.Search
{
    public enum AccountSearchFieldEnum : int
    {
        Any = 0,
        QSPAccountId = 1,
        EDSAccountId = 2,
        Name = 3,
        NameBeginingWith = 4,
        City = 5,
        ZipCode = 6,
        QSPOrganizationId = 7
    }
}
