using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using QSP.OrderExpress.Business.Context;
using QSP.OrderExpress.Business.Entity;

namespace QSP.OrderExpress.System
{
    public class OrganizationSystem
    {
        
        public OrganizationType GetOrganizationType(int organizationTypeId)
        {
            OrganizationType result = new OrganizationType();

            OrderExpressDataContext db = new OrderExpressDataContext();

            result = (from ot in db.OrganizationTypes
                      where ot.OrganizationTypeId == organizationTypeId
                      select ot
                      ).SingleOrDefault();

            return result;
        }
        public OrganizationLevel GetOrganizationLevel(int organizationLevelId)
        {
            OrganizationLevel result = new OrganizationLevel();

            OrderExpressDataContext db = new OrderExpressDataContext();

            result = (from ol in db.OrganizationLevels
                      where ol.OrganizationLevelId == organizationLevelId
                      select ol
                      ).SingleOrDefault();

            return result;
        }
    }
}
