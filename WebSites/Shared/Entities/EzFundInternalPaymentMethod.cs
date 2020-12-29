using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GA.BDC.Shared.Entities
{
    public enum EzFundInternalPaymentMethod
    {
        Unknown = 0,
        Check = 1,
        VISA = 2,
        MASTERCARD = 3,
        AMEX = 8,
        DISCOVER = 9,
        CheckPhone = 10,
        Other = 11,
        NoPayment = 13,
        Adjustment = 14,
        Paypal = 15,
        PO = 16
    }
}
