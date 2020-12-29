using GA.BDC.Data.Fundraising.EFundraisingProd.Tables;
using GA.BDC.Shared.Entities;

namespace GA.BDC.Data.Fundraising.EFundraisingProd.Mappers
{
    public static class PaymentMapper
    {
        public static payment Dehydrate(Payment payment)
        {
            var asteriscs = "";
            for (var i = 0; i < payment.CreditCard.Number.Length - 4; i++)
            {
                asteriscs += "*";
            }
            var result = new payment
            {
                sales_id = payment.SaleId,
                payment_no = payment.Number,
                credit_card_no = !string.IsNullOrEmpty(payment.CreditCard.Number) ? string.Concat(asteriscs, payment.CreditCard.Number.Substring(payment.CreditCard.Number.Length-4)) : string.Empty,
                authorization_number = payment.AuthorizationNumber,
                cashable_date = payment.CashableOn,
                commission_paid = payment.IsComissionPaid,
                create_date = payment.CreatedOn,
                expiry_date = payment.CreditCard.ExpirationDate,
                ext_payment_id = payment.ExternalPaymentId,
                foreign_orderid = payment.ForeignOrderId,
                name_on_card = payment.CreditCard.Holder,
                payment_amount = (decimal) payment.Amount,
                payment_entry_date = payment.EntryOn,
                payment_method_id = (byte) payment.InternalPaymentMethod,
                collection_status_id = payment.CollectionStatusId                
            };
            return result;
        }
    }
}
