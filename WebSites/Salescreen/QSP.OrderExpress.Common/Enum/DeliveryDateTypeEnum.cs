using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QSP.OrderExpress.Common.Enum
{
    public enum DeliveryDateTypeEnum : int
    {
        ChooseDate = 1,
        ChooseDateAndTime = 2,
        ChooseWeekStartingOnSunday = 3,
        ChooseWeekStartingOnMonday = 4,
        ChooseWeekStartingOnSundayOtisLogic = 5,
        ChooseWeekStartingOnMondayOtisLogic = 6,
    }
}
