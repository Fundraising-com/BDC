using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
    public enum   BatchStatus
    { 
        New	=40001
        ,InProcess=40002
        ,UnderReview=40003
        ,Approved=	40004
        ,Cancelled=	40005
        ,CCPending=	40006
        ,Housekeeping=	40007
        ,HousekeepingC=	40008
        ,Pickable=	40009
        ,AtWarehouse=	40010
        ,Shipped=	40011
        ,SenttoTPL=	40012
        ,Fulfilled=	40013
        ,PartiallyFulfilled=40014
        ,MagnetLoaded=	40015
    }

    public enum BatchOrderQualifier
    {
        Main=39001
        ,Supplement
        ,Staff
        ,Test
        ,ProblemSolver
        ,Kanata
        ,FieldSupplies
        ,CustomerService
        ,Internet
        ,GiftFix
        ,InternetFix
        ,OrderCorrection
        ,CreditCardReprocess
        ,CCReprocessCourtesy
        ,CCReprocessedtoinvoice
        ,FMGiftSample
        ,WFCSigningBonus
        ,KanataPSolver
        ,GiftPSolver
        ,CustomerServicetoInvoice
        ,TimeMain
        ,FMBulkSupply
        ,BookProblemSolver
   
    }
    public enum BatchOrderTypeCode
    {
        CA =41001
        ,CAFS
        ,CREDITCM
        ,DEBITCM
        ,EMP
        ,FM
        ,FMBULK
        ,GROUP
        ,MAGNET
        ,POS
        ,FMCLOSEOUT
        ,FreeSubs
    }

    public enum CustomerStatus
    {
        Good = 300
        ,Error = 301
    }
    public enum CustomerOrderHeaderStatus
    {
        Good = 400
        ,Error = 401
        ,Paid = 402
    }
    public enum CustomerOrderDetailStatus
    {
        Good = 500
        ,Error
        ,Paid
        ,CrossedBridge
        ,PaidPending
        ,RemovePayment
        ,VoidedDueToError
        ,SentToRemit
        ,PendingToTPL
        ,Pickable
        ,Picked
        ,Unremittable
        ,UnShipable
        ,Library
        ,Overpaid
        ,Incomplete
        ,NoOrder
    }

    public enum CustomerPaymentHeaderStatus
    {
        Good = 600
        ,Error
        ,Voided
        ,Dirty
        ,GoodBadOrder
    }

    public enum CreditCardPaymentStatus
    {
        Good=               19000
        ,Error
        ,HouseLimit        
        ,NeedsToBeSent   
        ,PaidPending       
        ,ZeroDollar        

    }
    public enum PaymentMethodInstance
    {
        Other = 50001
        ,Cash
        ,Visa
        ,MC
        ,Error
    }
    public enum PriceOverride
    {
        Coupon=45001
        ,InvalidPrice
        ,ClosestMatchingOffer
        ,None
        ,Replacement
    }

    public enum CustomerOrderHeaderType
    {
        Paylater  =             900
        ,Internet               
        ,Regular                
        ,Switch                 
        ,Cancel                 
        ,Comps                  
        ,Replace                
        ,AltSwitch              
        ,Corrections            
        ,RenewalTime            
        ,RenewalQSP             
        ,InternetInvoice        
    }
}
