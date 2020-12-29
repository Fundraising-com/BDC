using System;

namespace GA.BDC.Console.PAPFeedback.TaskExecutor
{
    [Flags]
    internal enum TaskFlags
    {
        NothingToDo = 0,

        SendPAPLandedSale = 1,
        
        SendPAPEsubsSale = 2,

        CancelPAPSale = 4,

        ConfirmPAPSale = 8,

        SendPAPActivation = 16,

        SendPAPKickOff = 32,

        SendPAPAutoCreate = 64,

        VerifyPAPCampaigns = 128,

        VerifyPAPAffiliates = 256,

        SendPAPKitRequest = 512
    }
}
