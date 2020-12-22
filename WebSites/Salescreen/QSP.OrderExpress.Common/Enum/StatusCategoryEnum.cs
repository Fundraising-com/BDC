using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QSP.OrderExpress.Common.Enum
{
    public enum StatusCategoryEnum : int
    {
        Saved = 1, 
        Submitted = 2, 
        Processed = 3, 
        Released = 4, 
        Shipped = 5, 
        Completed = 6, 
        Cancelled = 7, 
        PendingApproval = 8, 
        ErrorReported = 9, 
        Closed = 10, 
        TestOrderVoid = 11, 
        Active = 12, 
        Inactive = 13, 
        Invoiced = 14, 
        InCollection = 15, 
        IncompletePE = 16, 
        Hold = 17
    }
}
