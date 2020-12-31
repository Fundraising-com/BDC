using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Data;
using System.Data.Objects;

namespace BusinessServices
{
    public class TaxService : ITaxService
    {
        public Data.TaxResult CalculateTax(DateTime date, decimal amount, int sectionID, string productcode, string ordertype, int campaignid, int magprice_instance, string province)
        {
            TaxResult tResult = null;

            using(QSPCanadaOrderManagementEntities _context = new QSPCanadaOrderManagementEntities())
            {
                ObjectResult<TaxResult> results = _context.CalculateTax(date.ToString(), amount, sectionID, productcode, ordertype, campaignid, magprice_instance, province);

                tResult = results.FirstOrDefault();
            }

            return tResult;
        }
    }
}
