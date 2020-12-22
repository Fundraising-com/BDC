using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QSP.OrderExpress.Common.Enum
{
    public enum ExceptionTypeEnum : int
    {
        Note = 100, 
        TaxExemptionFormRequired = 101, 
        FreightCharges = 102, 
        ExpeditedFreightCharges = 103, 
        TaxExemption = 104, 
        ApprovableException = 200, 
        StandardException = 300, 
        CreditApplication = 301, 
        Mandatory = 900
    }
}
