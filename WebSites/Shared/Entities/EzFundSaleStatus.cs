using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GA.BDC.Shared.Entities
{
    public enum EzFundSaleStatus
    {
        [Description("OPEN")]
        OPEN = 10,
        [Description("NEW")]
        NEW	= 15,
        [Description("WAIT-CHK")]
        WAITCHK = 20,
        [Description("WAIT-PO")]
        WAITPO = 30,
        [Description("WAIT-CCRD")]
        WAITCCRD =	40,
        [Description("WAIT-CCRD-AUTH")]
        WAITCCRDAUTH = 45,
        [Description("INCONSISTENT")]
        INCONSISTENT = 50,
        [Description("VEND-TABN")]
        VENDTABN = 55,
        [Description("RELEASED")]
        RELEASED = 60,
        [Description("INVOICED")]
        INVOICED = 90,
        [Description("CANCELLED")]
        CANCELLED = 95,
        [Description("PREVIEW")]
        PREVIEW	= 100,
        [Description("ORDER-PEND")]
        ORDERPEND = 110,
        [Description("ENTERED")]
        ENTERED	= 120,
        [Description("PRELIM-ORDER")]
        PRELIMORDER = 800,
        [Description("PO")]
        PO = 900,
        [Description("PO-CONFIRMED")]
        POCONFIRMED = 910,
        [Description("CNCL-NTFY-PEND")]
        CNCLNTFYPEND = 920,
        [Description("CNCL-CFRM-PEND")]
        CNCLCFRMPEND = 930,
        [Description("PO-CANCELLED")]
        POCANCELLED = 940,
        [Description("PO-NOT-REQD")]
        PONOTREQD = 950
    }
}
