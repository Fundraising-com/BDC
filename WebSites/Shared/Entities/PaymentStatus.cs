namespace GA.BDC.Shared.Entities
{
    public enum PaymentStatus
    {
        NotAssigned = 0,
        InProcessToSAP = 1,
        SendToSAP = 2,
        ConfirmedBySAP = 3,
        SendFromSAP = 4
    }
}
