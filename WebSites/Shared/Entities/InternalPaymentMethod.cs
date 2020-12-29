namespace GA.BDC.Shared.Entities
{
    public enum InternalPaymentMethod
    {
        Unknown = 0,
        Check = 1,
        VISA = 2,
        MASTERCARD = 3,
        AMEX = 8,
        DISCOVER = 9,
        CheckByPhone = 10,
        Other = 11,
        NoPayment = 13,
        Adjustment = 14,
        Paypal = 15,
        PurchaseOrder = 16
    }
}
