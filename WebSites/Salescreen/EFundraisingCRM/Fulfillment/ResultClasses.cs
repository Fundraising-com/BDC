using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace efundraising.EFundraisingCRM.Fulfillment
{
    
    public class ShipmentGroupsResult
    {
        public System.DateTime? ShipDate
        { get; set; }
        public System.Int32 OrderId
        { get; set; }
        public System.String TrackingNo
        { get; set; }
    }


    public class PaymentInvoiceResult
    {
        public System.Int32 PaymentId
        { get; set; }
        public System.DateTime? PaymentDate
        { get; set; }
        public System.Int32? PaymentTypeId
        { get; set; }
        public System.Decimal PaymentAmount
        { get; set; }
        public System.Int32 OrderId
        { get; set; }
        public System.Int32 AccountId
        { get; set; }
        public System.Int32 SequenceNo
        { get; set; }
        
    }


    public class InvoiceResult
    {
        public System.Int32 InvocieId
        { get; set; }
        public System.DateTime? InvoiceDate
        { get; set; }
        public System.Decimal? InvoiceAmount
        { get; set; }
        public System.Int32 OrderId
        { get; set; }
    }


    public class AdjustmentInvoiceResult
    {
        public System.Int32 AdjustmentId
        { get; set; }
        public System.DateTime? AdjustmentDate
        { get; set; }
        public System.Int32? AdjustmentTypeId
        { get; set; }
        public System.Decimal AdjustmentAmount
        { get; set; }
        public System.Int32 OrderId
        { get; set; }
        public System.Int32 AccountId
        { get; set; }
        public System.Int32 SequenceNo
        { get; set; }


    }
}
