using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LinqContext = QSP.OrderExpress.Business.Context;
using LinqEntity = QSP.OrderExpress.Business.Entity;

namespace QSPForm.Business
{
    public class SubdivisionSystem
    {

        public List<LinqEntity.Subdivision> GetSubdivisionCodes()
        {
            List<LinqEntity.Subdivision> result = new List<LinqEntity.Subdivision>();

            LinqContext.OrderExpressDataContext db = new LinqContext.OrderExpressDataContext();

            result = (from sc in db.Subdivisions
                      select sc
                      ).ToList();

            return result;
        }
        public List<LinqEntity.Subdivision> GetSubdivisionCodesByCountryCode(string countryCode)
        {
            List<LinqEntity.Subdivision> result = new List<LinqEntity.Subdivision>();

            LinqContext.OrderExpressDataContext db = new LinqContext.OrderExpressDataContext();

            result = (from sc in db.Subdivisions
                      where sc.CountryCode == countryCode
                      select sc
                      ).ToList();

            return result;
        }

    }
}
