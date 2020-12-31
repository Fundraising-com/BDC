using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Data;

namespace BusinessServices
{
    public interface ITaxService : IBusinessService
    {
        TaxResult CalculateTax(DateTime date
                            , decimal amount
                            , int sectionID
                            , string productcode
                            , string ordertype
                            , int campaignid
                            , int magprice_instance
                            , string province);
    }
}
