using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LinqContext = QSP.OrderExpress.Business.Context;
using LinqEntity = QSP.OrderExpress.Business.Entity;
using QSP.OrderExpress.Common.Enum;

namespace QSPForm.Business
{
    public class FormPermissionRegionSystem
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="zip"></param>
        /// <returns></returns>
        public List<LinqEntity.FormPermissionRegion> SelectByZip(int formId, string zip)
        {
            List<LinqEntity.FormPermissionRegion> result = new List<LinqEntity.FormPermissionRegion>();

            LinqContext.OrderExpressDataContext context = new LinqContext.OrderExpressDataContext();

            var query = from    fpr in context.FormPermissionRegions
                        where   fpr.Zip == zip
                                && fpr.FormId == formId
                        select  fpr;

            result = query.ToList<LinqEntity.FormPermissionRegion>();

            if (result.Count == 0 && zip.Length > 1)
            {
                string shorterZip = zip.Substring(0, zip.Length - 1);

                FormPermissionRegionSystem fprs = new FormPermissionRegionSystem();
                result = fprs.SelectByZip(formId, shorterZip);
            }

            return result;
        }

    }
}
