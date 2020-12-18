using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Dynamic;

using QSP.OrderExpress.Business.Context;
using QSP.OrderExpress.Business.Entity;
using QSP.OrderExpress.Business.Validation;
using QSP.OrderExpress.Common.Comunication;
using QSP.OrderExpress.Common.Data;
using QSP.OrderExpress.Common.Enum;
using QSP.OrderExpress.Common.Search;

namespace QSPForm.Business
{
    public class StatusSystem
    {
        #region Version 2 code

        public StatusCategory GetStatusCategory(int statusCategoryId)
        {
            StatusCategory result = new StatusCategory();

            OrderExpressDataContext db = new OrderExpressDataContext();

            result = (from sc in db.StatusCategories
                      where sc.StatusCategoryId == statusCategoryId
                      select sc
                      ).SingleOrDefault();

            return result;
        }
        public List<StatusCategory> GetStatusCategories()
        {
            List<StatusCategory> result = new List<StatusCategory>();

            OrderExpressDataContext db = new OrderExpressDataContext();

            result = (from sc in db.StatusCategories
                      select sc
                      ).ToList();

            return result;
        }

        public List<StatusHistoryData> GetStatusHistoryFromAccount(int accountId)
        {
            List<StatusHistoryData> result = new List<StatusHistoryData>();

            OrderExpressDataContext db = new OrderExpressDataContext();

            result = (from asc in db.AccountStatusChanges
                        join user in db.Users on asc.CreateUserId equals user.UserId
                      where asc.AccountId == accountId
                        && asc.IsDeleted == false
                      select new StatusHistoryData
                      {
                          QSPId = asc.AccountId,
                          StatusId = asc.AccountStatusId,
                          StatusCategoryId = asc.AccountStatus.StatusCategoryId,
                          StatusColorCode = asc.AccountStatus.ColorCode,
                          StatusShortDescription = asc.AccountStatus.ShortDescription,
                          Reason = asc.StatusChangeReason,
                          CreateDate = asc.CreateDate,
                          CreatorFirstName = user.FirstName,
                          CreatorLastName = user.LastName
                      }
                      ).ToList();

            return result;
        }
        public List<StatusHistoryData> GetStatusHistoryFromProgramAgreement(int programAgreementId)
        {
            List<StatusHistoryData> result = new List<StatusHistoryData>();

            OrderExpressDataContext db = new OrderExpressDataContext();

            result = (from pasc in db.ProgramAgreementStatusChanges
                      join user in db.Users on pasc.CreateUserId equals user.UserId
                      where pasc.ProgramAgreementId == programAgreementId
                        && pasc.IsDeleted == false
                      select new StatusHistoryData
                      {
                          QSPId = pasc.ProgramAgreementId,
                          StatusId = pasc.ProgramAgreementStatusId,
                          StatusCategoryId = pasc.ProgramAgreementStatus.StatusCategoryId,
                          StatusColorCode = pasc.ProgramAgreementStatus.ColorCode,
                          StatusShortDescription = pasc.ProgramAgreementStatus.ShortDescription,
                          Reason = pasc.StatusChangeReason,
                          CreateDate = pasc.CreateDate,
                          CreatorFirstName = user.FirstName,
                          CreatorLastName = user.LastName
                      }
                      ).ToList();

            return result;
        }
        public List<StatusHistoryData> GetStatusHistoryFromOrder(int orderId)
        {
            List<StatusHistoryData> result = new List<StatusHistoryData>();

            OrderExpressDataContext db = new OrderExpressDataContext();

            result = (from osc in db.OrderStatusChanges
                      join user in db.Users on osc.CreateUserId equals user.UserId
                      where osc.OrderId == orderId
                        && osc.IsDeleted == false
                      select new StatusHistoryData
                      {
                          QSPId = osc.OrderId,
                          StatusId = osc.OrderStatusId,
                          StatusCategoryId = osc.OrderStatus.StatusCategoryId,
                          StatusColorCode = osc.OrderStatus.ColorCode,
                          StatusShortDescription = osc.OrderStatus.ShortDescription,
                          Reason = osc.StatusChangeReason,
                          CreateDate = osc.CreateDate,
                          CreatorFirstName = user.FirstName,
                          CreatorLastName = user.LastName
                      }
                      ).ToList();

            return result;
        }

        #endregion
    }
}
