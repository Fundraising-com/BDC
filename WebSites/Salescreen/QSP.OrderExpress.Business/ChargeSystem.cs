using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LinqContext = QSP.OrderExpress.Business.Context;
using LinqEntity = QSP.OrderExpress.Business.Entity;
using EntityData = QSP.OrderExpress.Common.Data;

using QSP.OrderExpress.Business;
using QSP.OrderExpress.Business.Validation;
using QSP.OrderExpress.Common.Comunication;
using QSP.OrderExpress.Common.Enum;
using QSP.OrderExpress.Common.Search;

namespace QSPForm.Business
{
    public class ChargeSystem
    {

        public List<EntityData.ChargeData> GetChargesFromOrder(int orderId)
        {
            List<EntityData.ChargeData> result = new List<EntityData.ChargeData>();

            LinqContext.OrderExpressDataContext db = new LinqContext.OrderExpressDataContext();

            result =
                (from oc in db.OrderCharges
                    join user in db.Users on oc.CreateUserId equals user.UserId
                 where oc.OrderId == orderId
                 select new EntityData.ChargeData
                 {
                     OrderChargeId = oc.OrderChargeId, 
                     OrderId = oc.OrderId,
                     ChargeId = oc.ChargeId,
                     ChargeName = oc.Charge.Name,
                     ChargeToId = oc.ChargeToId,
                     ChargeToName = oc.ChargeTo.Name, 
                     AccountId = oc.AccountId,
                     EstimatedAmount = oc.EstimatedAmount,
                     Amount = oc.Amount, 
                     Comment = oc.Comment, 
                     CreateDate = oc.CreateDate, 
                     CreateUserId = oc.CreateUserId, 
                     CreateUserFirstName = user.FirstName, 
                     CreateUserLastName = user.LastName
                 }
                 ).ToList();

            return result;
        }

    }
}
