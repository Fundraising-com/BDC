using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QSP.OrderExpress.Common.Search
{
    public enum ProgramAgreementSearchFieldEnum : int
    {
        Any = 0,
        QSPProgramAgreementId = 1,
        EDSProgramAgreementId = 2,
        QSPAccountId = 3,
        EDSAccountId = 4,
        Name = 5,
        NameBeginingWith = 6,
        City = 7,
        ZipCode = 8
    }
}
