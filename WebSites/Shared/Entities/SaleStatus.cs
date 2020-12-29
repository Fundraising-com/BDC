
namespace GA.BDC.Shared.Entities
{
    public enum SaleStatus
    {
        Unknown = 0,
        New = 1,
        Confirmed = 2,
        Cancelled = 4,
        OnHold =6,
        PendingConfirmation =7,
        Unreachable =8,
        PendingCancellation = 12,
        CreditCardToBeProcessed = 13,
        WFCOrder = 17
    }
}
