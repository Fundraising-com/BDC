using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QSP.OrderExpress.Common.Enum
{
    public enum UserTypeEnum : int
    {
        SynchronizationProcess = -2, 
        SystemProcess = -1, 
        User = 0, 
        FieldSaleManager = 1, 
        FieldSupport = 2, 
        AccountingManager = 3, 
        Admin = 4, 
        SuperUser = 5
    }
}
