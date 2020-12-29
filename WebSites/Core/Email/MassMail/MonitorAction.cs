using System;
using System.Collections.Generic;
using System.Text;

namespace GA.BDC.Core.Email.MassMail
{
    public enum MonitorAction : short
    {
        InsertedIntoTouch = 1,

        InsertedIntoMM = 127,

        ReceivedByRecipient = 255
    }
}
