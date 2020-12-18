using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Text;

using QSP.OrderExpress.Business.Context;
using QSP.OrderExpress.Business.Entity;
using QSP.OrderExpress.Business.Validation;
using QSP.OrderExpress.Common.Comunication;
using QSP.OrderExpress.Common.Data;
using QSP.OrderExpress.Common.Enum;
using QSP.OrderExpress.Common.Search;

namespace QSP.OrderExpress.Business
{
    public class TaxSystem
    {
        #region Version 2 code

        public List<TaxCalculationMethod> GetTaxCalculationMethods(int organizationTypeId, int productTypeId, string subdivisionCode, TaxLevelEnum taxLevel)
        {
            List<TaxCalculationMethod> result = new List<TaxCalculationMethod>();

            OrderExpressDataContext db = new OrderExpressDataContext();

            result = (from tcm in db.TaxCalculationMethods
                      where     tcm.OrganizationTypeId == organizationTypeId
                            &&  tcm.ProductTypeId == productTypeId
                            &&  tcm.SubdivisionCode == subdivisionCode
                            &&  tcm.TaxLevelId == (int)taxLevel
                      select tcm
                      ).ToList();

            return result;
        }

        #endregion
    }
}
