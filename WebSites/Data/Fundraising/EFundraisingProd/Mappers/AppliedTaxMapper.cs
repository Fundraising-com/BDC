using GA.BDC.Data.Fundraising.EFundraisingProd.Tables;
using GA.BDC.Shared.Entities;

namespace GA.BDC.Data.Fundraising.EFundraisingProd.Mappers
{
   public static class AppliedTaxMapper
   {
      public static Applicable_Tax Dehydrate(AppliedTax appliedTax)
      {
         return new Applicable_Tax
         {
            Sales_ID = appliedTax.SaleId,
            Tax_Amount = (decimal) appliedTax.Amount,
            Tax_Code = appliedTax.TaxCode
         };
      }

      public static AppliedTax Hydrate(Applicable_Tax applicableTax)
      {
         return new AppliedTax
         {
            Amount = (double) applicableTax.Tax_Amount,
            SaleId = applicableTax.Sales_ID,
            TaxCode = applicableTax.Tax_Code
         };
      }
   }
}
