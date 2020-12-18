using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QSP.OrderExpress.Common.Enum
{
    public enum ProgramAgreementStatusCategoryEnum : int
    {
        Saved = 1, 
        Submitted = 2, 
        Processed = 3, 
        Cancelled = 7, 
        PendingApproval = 8, 
        ErrorReported = 9, 
        TestOrderVoid = 11, 
    }
}
