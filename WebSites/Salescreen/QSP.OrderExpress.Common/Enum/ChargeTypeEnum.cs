using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QSP.OrderExpress.Common.Enum
{
    public enum ChargeTypeEnum : int
    {
        ExpeditedFreight = 1,
        SaturdayDelivery = 2,
        ResidentialDelivery = 3,
        Fuel = 4,
        DSDDelivery = 5,
        NonDSDDelivery = 6,
        LiftGate = 7,
        MinimumOrder = 8,
        RemoteDelivery = 9
    }
}
