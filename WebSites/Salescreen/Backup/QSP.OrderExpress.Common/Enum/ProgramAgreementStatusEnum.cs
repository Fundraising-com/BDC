using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QSP.OrderExpress.Common.Enum
{
    public enum ProgramAgreementStatusEnum : int
    {
        Incomplete = 0, 
        Saved = 1, 
        PendingApproval = 5, 
        Cancelled = 9, 
        TestPA = 70, 
        Inprocess = 101, 
        InprocessToBeCancelled = 109, 
        Exported = 201, 
        ExportForCancel = 209, 
        Processed = 301, 
        ProcessedFromEDS = 302, 
        CancelProcessed = 309, 
        MissingAccountId = 9002, 
        InvalidFSMNumber = 9006, 
        InvalidaccountNumber = 9013, 
        InvalidtaxExemptCode = 9119, 
        InvalidPA = 9214, 
        InvalidStartDates = 9215, 
        InvalidNumberInGroupAtLaunch = 9216, 
        InvalidestimatedNetSales = 9217, 
        InvalidBrochure = 9218, 
        InvalidAccountRetains = 9219, 
        InvalidNewOrRenewalCode = 9220, 
        InvalidholidayDates = 9223, 
        InvalidOLRCCDCode = 9229, 
        RecordNotFoundInPAMCDPP = 9302, 
        OrderNumberNotFoundInPAMCDPP = 9304, 
        InvalidEmailAddress = 9306, 
        InvalidAddress = 9400, 
        AddressisPOBoxNumber = 9408, 
        InvalidPhoneNumber = 9409, 
        InvalidMagnetBookletCode = 9449
    }
}
