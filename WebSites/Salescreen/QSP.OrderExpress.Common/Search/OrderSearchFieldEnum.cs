using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QSP.OrderExpress.Common.Search
{
    public enum OrderSearchFieldEnum : int
    {
        Any = 0,
        QSPOrderId = 1,
        EDSOrderId = 2,
        QSPAccountId = 3,
        EDSAccountId = 4,
        QSPProgramAgreementId = 5,
        Name = 6,
        NameBeginingWith = 7,
        City = 8,
        ZipCode = 9
    }
}
