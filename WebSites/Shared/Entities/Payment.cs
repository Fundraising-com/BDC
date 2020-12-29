using System;

namespace GA.BDC.Shared.Entities
{
    public class Payment
    {
        public Payment()
        {
            CreditCard = new CreditCard();
        }
        /// <summary>
        /// Sale Id
        /// </summary>
        public int SaleId { get; set; }
        /// <summary>
        /// Payment Number
        /// </summary>
        public int Number { get; set; }
        /// <summary>
        /// Payment Method Id
        /// </summary>
        public InternalPaymentMethod InternalPaymentMethod { get; set; }
        /// <summary>
        /// Collection Status Id
        /// </summary>
        public int CollectionStatusId { get; set; }
        /// <summary>
        /// Created On
        /// </summary>
        public DateTime CreatedOn { get; set; }
        /// <summary>
        /// Entry On
        /// </summary>
        public DateTime EntryOn { get; set; }
        /// <summary>
        /// Cashable On
        /// </summary>
        public DateTime CashableOn { get; set; }
        /// <summary>
        /// Credit Card Information
        /// </summary>
        public CreditCard CreditCard { get; set; }
        /// <summary>
        /// Authorization Number
        /// </summary>
        public string AuthorizationNumber { get; set; }
        /// <summary>
        /// Amount Paid
        /// </summary>
        public double Amount { get; set; }
        /// <summary>
        /// Comission Paid
        /// </summary>
        public bool IsComissionPaid { get; set; }
        /// <summary>
        /// Foreign Order Id
        /// </summary>
        public int ForeignOrderId { get; set; }
        /// <summary>
        /// External Payment Id
        /// </summary>
        public int ExternalPaymentId { get; set; }
        /// <summary>
        /// Status
        /// </summary>
        public PaymentStatus Status { get; set; }
        
    }
}
