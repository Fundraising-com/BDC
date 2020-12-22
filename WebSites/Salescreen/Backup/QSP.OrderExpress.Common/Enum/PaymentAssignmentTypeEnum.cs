using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QSPForm.Common.Enum
{
    public enum PaymentAssignmentTypeEnum : int
    {
        // This comes from the payment_assignment_type table

        None = 0,
        FSMToPay = 1,
        QSPToPay = 2
    }
}
