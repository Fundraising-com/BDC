using GA.BDC.Data.Fundraising.EFundraisingProd.Tables;
using GA.BDC.Shared.Entities;

namespace GA.BDC.Data.Fundraising.EFundraisingProd.Mappers
{
   public static class TaxMapper
   {
      public static Tax Hydrate(Tax_Table taxTable)
      {
         return new Tax
         {
            Code = taxTable.Tax_Code,
            Account = taxTable.Tax_Account_Number,
            DescriptionEnglish = taxTable.Description,
            DescriptionFrench = taxTable.Description_francaise
         };
      }

      public static Tax_Table Dehydrate(Tax tax)
      {
         return new Tax_Table
         {
            Tax_Code = tax.Code,
            Tax_Account_Number = tax.Account,
            Description = tax.DescriptionEnglish,
            Description_francaise = tax.DescriptionFrench
         };
      }
   }
}
