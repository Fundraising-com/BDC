using System;
using System.Collections.Generic;
using System.Linq;
using GA.BDC.Data.Fundraising.EFundraisingProd.Mappers;
using GA.BDC.Data.Fundraising.EFundraisingProd.Tables;
using GA.BDC.Shared.Data.Repositories;
using GA.BDC.Shared.Entities;

namespace GA.BDC.Data.Fundraising.EFundraisingProd.Repositories
{
    public class PaymentsRepository :IPaymentsRepository
    {
        private readonly DataProvider _dataProvider;
        public PaymentsRepository(DataProvider dataProvider)
        {
            _dataProvider = dataProvider;
        }

       public IList<Payment> GetAll()
       {
          throw new NotImplementedException();
       }

       public int Save(Payment payment)
        {
            var payments = _dataProvider.payments.Where(p => p.sales_id == payment.SaleId).ToList();
            var nextNumber = payments.Count == 0 ? 1 : payments.Max(p => p.payment_no) + 1;
            payment.Number = nextNumber;
            payment.CreatedOn = DateTime.Now;
            payment.CashableOn = DateTime.Now;
            payment.EntryOn = DateTime.Now;
            var paymentToBePersisted = PaymentMapper.Dehydrate(payment);
            _dataProvider.payments.Add(paymentToBePersisted);
            _dataProvider.SaveChanges();
            return payment.SaleId;
        }

       public void Update(Payment model)
       {
          throw new NotImplementedException();
       }

       public void Delete(Payment model)
       {
          throw new NotImplementedException();
       }

       public Payment GetById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
