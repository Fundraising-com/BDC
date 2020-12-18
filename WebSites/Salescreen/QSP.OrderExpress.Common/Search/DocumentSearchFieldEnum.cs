using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QSP.OrderExpress.Common.Search
{
    public enum DocumentSearchFieldEnum : int
    {
        Any = 0,
        QSPAccountId = 1,
        EDSAccountId = 2,
        AccountName = 3,
        AccountNameBeginingWith = 4,
        QSPDocumentId = 5
    }
}
