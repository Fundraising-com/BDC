using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Dynamic;
using System.Diagnostics;
using System.Transactions;

using QSPForm.Business.Properties;
using QSPForm.Common;
using QSPForm.Common.DataDef;
using dataDef = QSPForm.Common.DataDef.OrderHeaderTable;
using dataAccessRef = QSPForm.Data.Order;

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

    /// <summary>
    ///     This class contains the business rules that are used for an Order
    /// </summary>
    public class OrderSystem : BusinessSystem
    {

        #region Version 2 code

        public List<OrderSearchItem> Search(OrderSearchParameters parameters)
        {
            List<OrderSearchItem> result = new List<OrderSearchItem>();

            LinqContext.OrderExpressDataContext db = new LinqContext.OrderExpressDataContext();
            //using (TransactionScope t = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted }))
            //{

                #region Base query

                var query = from o in db.Orders
                            join paa in db.PostalAddressAccounts on o.Campaign.Account.AccountId equals paa.AccountId
                            join fsm in db.FieldSalesManagers on o.Campaign.Account.FmId equals fsm.FmId
                            where o.Campaign.IsDeleted == false
                                && o.Campaign.Account.IsDeleted == false
                                //&& fsm.Deleted == false //it is commented because at line #265 it is already handled
                                && o.Campaign.ProgramType.Enabled == true
                                && (o.Campaign.Account.Organization.BusinessDivisionId == (int)BusinessDivisionEnum.US
                                    || o.Campaign.Account.Organization.BusinessDivisionId == (int)BusinessDivisionEnum.EFR)
                                && paa.IsDeleted == false
                                && paa.PostalAddressTypeId == 1
                            select new
                            {
                                StatusId = o.OrderStatusId,
                                StatusCategoryId = o.OrderStatus.StatusCategoryId,
                                StatusColorCode = o.OrderStatus.ColorCode,
                                StatusShortDescription = o.OrderStatus.ShortDescription,
                                OrderId = o.OrderId,
                                EDSOrderId = o.FulfOrderId,
                                OrderTypeId = o.OrderTypeId,
                                OrderTypeName = o.OrderType.OrderTypeName,
                                OrderDate = o.OrderDate,
                                OrderSourceId = o.SourceId,
                                AccountId = o.Campaign.AccountId,
                                EDSAccountId = o.Campaign.Account.FulfAccountId,
                                AccountName = o.Campaign.Account.AccountName,
                                FsmIsDeleted = fsm.Deleted,
                                FsmId = fsm.FmId,
                                FsmFirstName = fsm.FirstName,
                                FsmLastName = fsm.LastName,
                                ProgramTypeId = o.Campaign.ProgramTypeId,
                                ProgramTypeName = o.Campaign.ProgramType.ProgramTypeName,
                                FormId = o.FormId,
                                FormName = o.Form.FormName,
                                Address1 = paa.PostalAddress.Address1,
                                AddressCity = paa.PostalAddress.City,
                                AddressSubdivisionCode = paa.PostalAddress.SubdivisionCode,
                                AddressZip = paa.PostalAddress.Zip,
                                CreateDate = o.CreateDate,
                                CreateUserId = o.CreateUserId,
                                FiscalYear = o.Campaign.FiscalYear
                            };

                #endregion

                #region Filters

                if (parameters.SearchValue.Length > 0)
                {
                    if (parameters.SearchField == OrderSearchFieldEnum.Any)
                    {
                        int number = 0;
                        bool isNumber = int.TryParse(parameters.SearchValue, out number);

                        if (isNumber)
                        {
                            query = from q in query
                                    where q.AddressCity.Contains(parameters.SearchValue)
                                    || q.AccountName.Contains(parameters.SearchValue)
                                    || q.AddressZip.Contains(parameters.SearchValue)
                                    || q.AccountId == number
                                    || q.EDSAccountId == number
                                    || q.OrderId == number
                                    || q.EDSOrderId == parameters.SearchValue
                                    select q;
                        }
                        else
                        {
                            query = from q in query
                                    where q.AddressCity.Contains(parameters.SearchValue)
                                    || q.AccountName.Contains(parameters.SearchValue)
                                    || q.AddressZip.Contains(parameters.SearchValue)
                                    || q.EDSOrderId == parameters.SearchValue
                                    select q;
                        }
                    }
                    else if (parameters.SearchField == OrderSearchFieldEnum.City)
                    {
                        query = from q in query
                                where q.AddressCity.Contains(parameters.SearchValue)
                                select q;
                    }
                    else if (parameters.SearchField == OrderSearchFieldEnum.Name)
                    {
                        query = from q in query
                                where q.AccountName.Contains(parameters.SearchValue)
                                select q;
                    }
                    else if (parameters.SearchField == OrderSearchFieldEnum.NameBeginingWith)
                    {
                        query = from q in query
                                where q.AccountName.StartsWith(parameters.SearchValue)
                                select q;
                    }
                    else if (parameters.SearchField == OrderSearchFieldEnum.QSPOrderId)
                    {
                        int number = 0;
                        bool isNumber = int.TryParse(parameters.SearchValue, out number);

                        if (isNumber)
                        {
                            query = from q in query
                                    where q.OrderId == number
                                    select q;
                        }
                    }
                    else if (parameters.SearchField == OrderSearchFieldEnum.EDSOrderId)
                    {
                        query = from q in query
                                where q.EDSOrderId == parameters.SearchValue
                                select q;
                    }
                    else if (parameters.SearchField == OrderSearchFieldEnum.QSPAccountId)
                    {
                        int number = 0;
                        bool isNumber = int.TryParse(parameters.SearchValue, out number);

                        if (isNumber)
                        {
                            query = from q in query
                                    where q.AccountId == number
                                    select q;
                        }
                    }
                    else if (parameters.SearchField == OrderSearchFieldEnum.EDSAccountId)
                    {
                        int number = 0;
                        bool isNumber = int.TryParse(parameters.SearchValue, out number);

                        if (isNumber)
                        {
                            query = from q in query
                                    where q.EDSAccountId == number
                                    select q;
                        }
                    }
                    else if (parameters.SearchField == OrderSearchFieldEnum.ZipCode)
                    {
                        query = from q in query
                                where q.AddressZip.Contains(parameters.SearchValue)
                                select q;
                    }
                    else if (parameters.SearchField == OrderSearchFieldEnum.QSPProgramAgreementId)
                    {
                        int number = 0;
                        bool isNumber = int.TryParse(parameters.SearchValue, out number);

                        if (isNumber)
                        {
                            List<int> orderIdList = (from pao in db.ProgramAgreementOrders
                                                     where pao.IsDeleted == false
                                                        && pao.ProgramAgreementId == number
                                                     select pao.OrderId).ToList();

                            query = from q in query
                                    where orderIdList.Contains(q.OrderId)
                                    select q;
                        }
                    }
                }

                if (parameters.SourceId.HasValue)
                {
                    query = from q in query
                            where q.OrderSourceId == parameters.SourceId.Value
                            select q;
                }

                if (parameters.FormId.HasValue)
                {
                    query = from q in query
                            where q.FormId == parameters.FormId.Value
                            select q;
                }

                if (parameters.ProgramTypeId.HasValue)
                {
                    query = from q in query
                            where q.ProgramTypeId == parameters.ProgramTypeId.Value
                            select q;
                }

                if (parameters.OrderTypeId.HasValue)
                {
                    query = from q in query
                            where q.OrderTypeId == parameters.OrderTypeId.Value
                            select q;
                }

                if (parameters.StatusCategoryId.HasValue)
                {
                    query = from q in query
                            where q.StatusCategoryId == parameters.StatusCategoryId.Value
                            select q;
                }

                if (parameters.SubdivisionCode.Length > 0)
                {
                    query = from q in query
                            where q.AddressSubdivisionCode == parameters.SubdivisionCode
                            select q;
                }

                if (parameters.StartDate.HasValue)
                {
                    query = from q in query
                            where q.CreateDate >= parameters.StartDate.Value
                            select q;
                }

                if (parameters.EndDate.HasValue)
                {
                    query = from q in query
                            where q.CreateDate <= parameters.EndDate.Value
                            select q;
                }

                #endregion

                #region Handle deleted FSM data

                if (parameters.LoggedUserType == UserTypeEnum.SuperUser ||
                    parameters.LoggedUserType == UserTypeEnum.Admin ||
                    parameters.LoggedUserType == UserTypeEnum.FieldSupport ||
                    parameters.LoggedUserType == UserTypeEnum.AccountingManager)
                {
                    // Allow deleted fsm data
                }
                else
                {
                    // Remove deleted fsm data
                    query = from q in query
                            where q.FsmIsDeleted == false
                            select q;
                }

                #endregion

                #region Handle FSM hierarchy

                // All applies to admin
                // Own, Children, OwnAndChildren apply to FSMs

                if (parameters.SearchFSMOption == SearchFSMHierarchyOptionEnum.All)
                {
                    if (parameters.FSMId.Length > 0)
                    {
                        query = from q in query
                                where q.FsmId.Contains(parameters.FSMId)
                                select q;
                    }
                    if (parameters.FSMName.Length > 0)
                    {
                        query = from q in query
                                where (q.FsmFirstName + " " + q.FsmLastName).Contains(parameters.FSMName)
                                select q;
                    }
                }
                else if (parameters.SearchFSMOption == SearchFSMHierarchyOptionEnum.Own)
                {
                    query = query.Where(q => q.FsmId == parameters.FSMId);
                }
                else if (parameters.SearchFSMOption == SearchFSMHierarchyOptionEnum.Children)
                {
                    LinqContext.QSPCommonDataContext dbCommon = new LinqContext.QSPCommonDataContext();
                    List<string> FmTree = (from u in dbCommon.fnc_FMHierarchyList_FMID(parameters.FSMId) select u.FMNumber).ToList();
                    query = query.Where(q => FmTree.Contains(q.FsmId));
                    query = query.Where(q => q.FsmId != parameters.FSMId);
                }
                else if (parameters.SearchFSMOption == SearchFSMHierarchyOptionEnum.OwnAndChildren)
                {
                    LinqContext.QSPCommonDataContext dbCommon = new LinqContext.QSPCommonDataContext();
                    List<string> FmTree = (from u in dbCommon.fnc_FMHierarchyList_FMID(parameters.FSMId) select u.FMNumber).ToList();
                    query = query.Where(q => FmTree.Contains(q.FsmId));
                }

                #endregion

                #region Left join to get create user

                var finalQuery = from q in query
                                 join user in db.Users on q.CreateUserId equals user.UserId into temp
                                 from userData in temp.DefaultIfEmpty()
                                 select new
                                 {
                                     StatusId = q.StatusId,
                                     StatusCategoryId = q.StatusCategoryId,
                                     StatusColorCode = q.StatusColorCode,
                                     StatusShortDescription = q.StatusShortDescription,
                                     OrderId = q.OrderId,
                                     EDSOrderId = q.EDSOrderId,
                                     OrderTypeId = q.OrderTypeId,
                                     OrderTypeName = q.OrderTypeName,
                                     OrderDate = q.OrderDate,
                                     OrderSourceId = q.OrderSourceId,
                                     AccountId = q.AccountId,
                                     EDSAccountId = q.EDSAccountId,
                                     AccountName = q.AccountName,
                                     FsmId = q.FsmId,
                                     FsmFirstName = q.FsmFirstName,
                                     FsmLastName = q.FsmLastName,
                                     ProgramTypeId = q.ProgramTypeId,
                                     ProgramTypeName = q.ProgramTypeName,
                                     FormId = q.FormId,
                                     FormName = q.FormName,
                                     Address1 = q.Address1,
                                     AddressCity = q.AddressCity,
                                     AddressSubdivisionCode = q.AddressSubdivisionCode,
                                     AddressZip = q.AddressZip,
                                     CreateDate = q.CreateDate,
                                     CreateUserId = q.CreateUserId,
                                     CreateUserFirstName = (userData.FirstName == null) ? "" : userData.FirstName,
                                     CreateUserLastName = (userData.LastName == null) ? "" : userData.LastName,
                                     FiscalYear = q.FiscalYear
                                 };

                #endregion

                #region Sort

                finalQuery = finalQuery.OrderBy(parameters.SortField);

                #endregion`

                if (parameters.IsPagingEnabled)
                {
                    Properties.Settings settings = new Properties.Settings();

                    if (settings.UseDatabasePaging)
                    {
                        #region Paging

                        finalQuery = (
                            from q in finalQuery
                            select q
                            ).Skip((parameters.RequestedPage - 1) * parameters.ItemsPerPage).Take(parameters.ItemsPerPage);

                        #endregion

                        #region Get final result

                        result = (
                            from q in finalQuery
                            select new OrderSearchItem
                            {
                                StatusId = q.StatusId,
                                StatusCategoryId = q.StatusCategoryId,
                                StatusColorCode = q.StatusColorCode,
                                StatusShortDescription = q.StatusShortDescription,
                                OrderId = q.OrderId,
                                EDSOrderId = q.EDSOrderId,
                                OrderTypeId = q.OrderTypeId,
                                OrderTypeName = q.OrderTypeName,
                                OrderDate = q.OrderDate,
                                OrderSourceId = q.OrderSourceId,
                                AccountId = q.AccountId,
                                EDSAccountId = q.EDSAccountId,
                                AccountName = q.AccountName,
                                FmId = q.FsmId,
                                FmFirstName = q.FsmFirstName,
                                FmLastName = q.FsmLastName,
                                ProgramTypeId = q.ProgramTypeId,
                                ProgramTypeName = q.ProgramTypeName,
                                FormId = q.FormId ?? 0,
                                FormName = q.FormName,
                                Address1 = q.Address1,
                                City = q.AddressCity,
                                SubdivisionCode = q.AddressSubdivisionCode,
                                Zip = q.AddressZip,
                                CreateDate = q.CreateDate,
                                CreatorFirstName = q.CreateUserFirstName,
                                CreatorLastName = q.CreateUserLastName,
                                FiscalYear = q.FiscalYear
                            }
                            ).ToList();

                        #endregion
                    }
                    else
                    {
                        #region Paging and final result

                        finalQuery = finalQuery.Take((parameters.RequestedPage + 1) * parameters.ItemsPerPage);

                        var temp = finalQuery.AsEnumerable().Skip(
                            (parameters.RequestedPage - 1) * parameters.ItemsPerPage).Select(q =>
                                new OrderSearchItem
                                {
                                    StatusId = q.StatusId,
                                    StatusCategoryId = q.StatusCategoryId,
                                    StatusColorCode = q.StatusColorCode,
                                    StatusShortDescription = q.StatusShortDescription,
                                    OrderId = q.OrderId,
                                    EDSOrderId = q.EDSOrderId,
                                    OrderTypeId = q.OrderTypeId,
                                    OrderTypeName = q.OrderTypeName,
                                    OrderDate = q.OrderDate,
                                    OrderSourceId = q.OrderSourceId,
                                    AccountId = q.AccountId,
                                    EDSAccountId = q.EDSAccountId,
                                    AccountName = q.AccountName,
                                    FmId = q.FsmId,
                                    FmFirstName = q.FsmFirstName,
                                    FmLastName = q.FsmLastName,
                                    ProgramTypeId = q.ProgramTypeId,
                                    ProgramTypeName = q.ProgramTypeName,
                                    FormId = q.FormId ?? 0,
                                    FormName = q.FormName,
                                    Address1 = q.Address1,
                                    City = q.AddressCity,
                                    SubdivisionCode = q.AddressSubdivisionCode,
                                    Zip = q.AddressZip,
                                    CreateDate = q.CreateDate,
                                    CreatorFirstName = q.CreateUserFirstName,
                                    CreatorLastName = q.CreateUserLastName,
                                    FiscalYear = q.FiscalYear
                                });

                        result = temp.ToList();

                        #endregion
                    }
                }
                else
                {
                    #region Get final result

                    result = (
                        from q in finalQuery
                        select new OrderSearchItem
                        {
                            StatusId = q.StatusId,
                            StatusCategoryId = q.StatusCategoryId,
                            StatusColorCode = q.StatusColorCode,
                            StatusShortDescription = q.StatusShortDescription,
                            OrderId = q.OrderId,
                            EDSOrderId = q.EDSOrderId,
                            OrderTypeId = q.OrderTypeId,
                            OrderTypeName = q.OrderTypeName,
                            OrderDate = q.OrderDate,
                            OrderSourceId = q.OrderSourceId,
                            AccountId = q.AccountId,
                            EDSAccountId = q.EDSAccountId,
                            AccountName = q.AccountName,
                            FmId = q.FsmId,
                            FmFirstName = q.FsmFirstName,
                            FmLastName = q.FsmLastName,
                            ProgramTypeId = q.ProgramTypeId,
                            ProgramTypeName = q.ProgramTypeName,
                            FormId = q.FormId ?? 0,
                            FormName = q.FormName,
                            Address1 = q.Address1,
                            City = q.AddressCity,
                            SubdivisionCode = q.AddressSubdivisionCode,
                            Zip = q.AddressZip,
                            CreateDate = q.CreateDate,
                            CreatorFirstName = q.CreateUserFirstName,
                            CreatorLastName = q.CreateUserLastName,
                            FiscalYear = q.FiscalYear
                        }
                        ).ToList();

                    #endregion
                }
            //}
            return result;
        }
        public int SearchTotalRowCount(OrderSearchParameters parameters)
        {
            int result = 0;

            LinqContext.OrderExpressDataContext db = new LinqContext.OrderExpressDataContext();

            #region Base query

            var query = from o in db.Orders
                        join paa in db.PostalAddressAccounts on o.Campaign.Account.AccountId equals paa.AccountId
                        join fsm in db.FieldSalesManagers on o.Campaign.Account.FmId equals fsm.FmId
                        where o.Campaign.IsDeleted == false
                            && o.Campaign.Account.IsDeleted == false
                            && o.Campaign.ProgramType.Enabled == true
                            && (o.Campaign.Account.Organization.BusinessDivisionId == (int)BusinessDivisionEnum.US
                                || o.Campaign.Account.Organization.BusinessDivisionId == (int)BusinessDivisionEnum.EFR)
                            && paa.IsDeleted == false
                            && paa.PostalAddressTypeId == 1
                        select new
                        {
                            StatusId = o.OrderStatusId,
                            StatusCategoryId = o.OrderStatus.StatusCategoryId,
                            StatusColorCode = o.OrderStatus.ColorCode,
                            StatusShortDescription = o.OrderStatus.ShortDescription,
                            OrderId = o.OrderId,
                            EDSOrderId = o.FulfOrderId,
                            OrderTypeId = o.OrderTypeId,
                            OrderTypeName = o.OrderType.OrderTypeName,
                            OrderDate = o.OrderDate,
                            OrderSourceId = o.SourceId,
                            AccountId = o.Campaign.AccountId,
                            EDSAccountId = o.Campaign.Account.FulfAccountId,
                            AccountName = o.Campaign.Account.AccountName,
                            FsmIsDeleted = fsm.Deleted,
                            FsmId = fsm.FmId,
                            FsmFirstName = fsm.FirstName,
                            FsmLastName = fsm.LastName,
                            ProgramTypeId = o.Campaign.ProgramTypeId,
                            ProgramTypeName = o.Campaign.ProgramType.ProgramTypeName,
                            FormId = o.FormId,
                            FormName = o.Form.FormName,
                            Address1 = paa.PostalAddress.Address1,
                            AddressCity = paa.PostalAddress.City,
                            AddressSubdivisionCode = paa.PostalAddress.SubdivisionCode,
                            AddressZip = paa.PostalAddress.Zip,
                            CreateDate = o.CreateDate,
                            CreateUserId = o.CreateUserId,
                            FiscalYear = o.Campaign.FiscalYear
                        };

            #endregion

            #region Filters

            if (parameters.SearchValue.Length > 0)
            {
                if (parameters.SearchField == OrderSearchFieldEnum.Any)
                {
                    int number = 0;
                    bool isNumber = int.TryParse(parameters.SearchValue, out number);

                    if (isNumber)
                    {
                        query = from q in query
                                where q.AddressCity.Contains(parameters.SearchValue)
                                || q.AccountName.Contains(parameters.SearchValue)
                                || q.AddressZip.Contains(parameters.SearchValue)
                                || q.AccountId == number
                                || q.EDSAccountId == number
                                || q.OrderId == number
                                || q.EDSOrderId == parameters.SearchValue
                                select q;
                    }
                    else
                    {
                        query = from q in query
                                where q.AddressCity.Contains(parameters.SearchValue)
                                || q.AccountName.Contains(parameters.SearchValue)
                                || q.AddressZip.Contains(parameters.SearchValue)
                                || q.EDSOrderId == parameters.SearchValue
                                select q;
                    }
                }
                else if (parameters.SearchField == OrderSearchFieldEnum.City)
                {
                    query = from q in query
                            where q.AddressCity.Contains(parameters.SearchValue)
                            select q;
                }
                else if (parameters.SearchField == OrderSearchFieldEnum.Name)
                {
                    query = from q in query
                            where q.AccountName.Contains(parameters.SearchValue)
                            select q;
                }
                else if (parameters.SearchField == OrderSearchFieldEnum.NameBeginingWith)
                {
                    query = from q in query
                            where q.AccountName.StartsWith(parameters.SearchValue)
                            select q;
                }
                else if (parameters.SearchField == OrderSearchFieldEnum.QSPOrderId)
                {
                    int number = 0;
                    bool isNumber = int.TryParse(parameters.SearchValue, out number);

                    if (isNumber)
                    {
                        query = from q in query
                                where q.OrderId == number
                                select q;
                    }
                }
                else if (parameters.SearchField == OrderSearchFieldEnum.EDSOrderId)
                {
                    query = from q in query
                            where q.EDSOrderId == parameters.SearchValue
                            select q;
                }
                else if (parameters.SearchField == OrderSearchFieldEnum.QSPAccountId)
                {
                    int number = 0;
                    bool isNumber = int.TryParse(parameters.SearchValue, out number);

                    if (isNumber)
                    {
                        query = from q in query
                                where q.AccountId == number
                                select q;
                    }
                }
                else if (parameters.SearchField == OrderSearchFieldEnum.EDSAccountId)
                {
                    int number = 0;
                    bool isNumber = int.TryParse(parameters.SearchValue, out number);

                    if (isNumber)
                    {
                        query = from q in query
                                where q.EDSAccountId == number
                                select q;
                    }
                }
                else if (parameters.SearchField == OrderSearchFieldEnum.ZipCode)
                {
                    query = from q in query
                            where q.AddressZip.Contains(parameters.SearchValue)
                            select q;
                }
                else if (parameters.SearchField == OrderSearchFieldEnum.QSPProgramAgreementId)
                {
                    int number = 0;
                    bool isNumber = int.TryParse(parameters.SearchValue, out number);

                    if (isNumber)
                    {
                        List<int> orderIdList = (from pao in db.ProgramAgreementOrders
                                                 where pao.IsDeleted == false
                                                    && pao.ProgramAgreementId == number
                                                 select pao.OrderId).ToList();

                        query = from q in query
                                where orderIdList.Contains(q.OrderId)
                                select q;
                    }
                }
            }

            if (parameters.SourceId.HasValue)
            {
                query = from q in query
                        where q.OrderSourceId == parameters.SourceId.Value
                        select q;
            }

            if (parameters.FormId.HasValue)
            {
                query = from q in query
                        where q.FormId == parameters.FormId.Value
                        select q;
            }

            if (parameters.ProgramTypeId.HasValue)
            {
                query = from q in query
                        where q.ProgramTypeId == parameters.ProgramTypeId.Value
                        select q;
            }

            if (parameters.OrderTypeId.HasValue)
            {
                query = from q in query
                        where q.OrderTypeId == parameters.OrderTypeId.Value
                        select q;
            }

            if (parameters.StatusCategoryId.HasValue)
            {
                query = from q in query
                        where q.StatusCategoryId == parameters.StatusCategoryId.Value
                        select q;
            }

            if (parameters.SubdivisionCode.Length > 0)
            {
                query = from q in query
                        where q.AddressSubdivisionCode == parameters.SubdivisionCode
                        select q;
            }

            if (parameters.StartDate.HasValue)
            {
                query = from q in query
                        where q.CreateDate >= parameters.StartDate.Value
                        select q;
            }

            if (parameters.EndDate.HasValue)
            {
                query = from q in query
                        where q.CreateDate <= parameters.EndDate.Value
                        select q;
            }

            #endregion

            #region Handle deleted FSM data

            if (parameters.LoggedUserType == UserTypeEnum.SuperUser ||
                parameters.LoggedUserType == UserTypeEnum.Admin ||
                parameters.LoggedUserType == UserTypeEnum.FieldSupport ||
                parameters.LoggedUserType == UserTypeEnum.AccountingManager)
            {
                // Allow deleted fsm data
            }
            else
            {
                // Remove deleted fsm data
                query = from q in query
                        where q.FsmIsDeleted == false
                        select q;
            }

            #endregion

            #region Handle FSM hierarchy

            // All applies to admin
            // Own, Children, OwnAndChildren apply to FSMs

            if (parameters.SearchFSMOption == SearchFSMHierarchyOptionEnum.All)
            {
                if (parameters.FSMId.Length > 0)
                {
                    query = from q in query
                            where q.FsmId.Contains(parameters.FSMId)
                            select q;
                }
                if (parameters.FSMName.Length > 0)
                {
                    query = from q in query
                            where (q.FsmFirstName + " " + q.FsmLastName + " " + q.FsmFirstName).Contains(parameters.FSMName)
                            select q;
                }
            }
            else if (parameters.SearchFSMOption == SearchFSMHierarchyOptionEnum.Own)
            {
                query = query.Where(q => q.FsmId == parameters.FSMId);
            }
            else if (parameters.SearchFSMOption == SearchFSMHierarchyOptionEnum.Children)
            {
                LinqContext.QSPCommonDataContext dbCommon = new LinqContext.QSPCommonDataContext();
                List<string> FmTree = (from u in dbCommon.fnc_FMHierarchyList_FMID(parameters.FSMId) select u.FMNumber).ToList();
                query = query.Where(q => FmTree.Contains(q.FsmId));
                query = query.Where(q => q.FsmId != parameters.FSMId);
            }
            else if (parameters.SearchFSMOption == SearchFSMHierarchyOptionEnum.OwnAndChildren)
            {
                LinqContext.QSPCommonDataContext dbCommon = new LinqContext.QSPCommonDataContext();
                List<string> FmTree = (from u in dbCommon.fnc_FMHierarchyList_FMID(parameters.FSMId) select u.FMNumber).ToList();
                query = query.Where(q => FmTree.Contains(q.FsmId));
            }

            #endregion

            #region Get final count

            //using (TransactionScope t = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted }))
            //{
                result = query.Count();
            //}

            #endregion

            return result;
        }

        public EntityData.OrderData GetOrder(int orderId)
        {
            return this.GetOrder(orderId, false);
        }
        public EntityData.OrderData GetOrder(int orderId, bool loadChildrenObjects)
        {
            EntityData.OrderData result = new EntityData.OrderData();

            LinqContext.OrderExpressDataContext db = new LinqContext.OrderExpressDataContext();

            #region Load order data

            LinqEntity.Order order =
                (from o in db.Orders
                 where o.OrderId == orderId
                 select o
                 ).SingleOrDefault();

            result.Id = order.OrderId;
            result.EdsId = order.FulfOrderId;
            result.StatusId = order.OrderStatusId;
            result.FsmId = order.FmId;
            result.OrderDate = order.OrderDate;
            result.OrderTypeId = order.OrderTypeId;
            result.CustomerPONumber = order.CustomerPoNumber;
            result.ProfitRate = order.ProfitRate;
            result.Comments = order.Comments;
            result.IsDeleted = order.IsDeleted;
            result.CreateDate = order.CreateDate;
            result.CreateUserId = order.CreateUserId;
            result.UpdateDate = order.UpdateDate;
            result.UpdateUserId = order.UpdateUserId;
            result.FormId = order.FormId;

            #endregion

            #region Load order detail data

            result.OrderDetails = new List<EntityData.OrderDetailData>();

            int i = 0;
            foreach (LinqEntity.OrderDetail orderDetail in order.OrderDetails)
            {
                i++;

                EntityData.OrderDetailData newOrderDetailDataItem = new EntityData.OrderDetailData();

                newOrderDetailDataItem.Number = i;
                newOrderDetailDataItem.ItemNumber = orderDetail.CatalogItemDetail.CatalogItemDetailCode;
                newOrderDetailDataItem.ItemName = orderDetail.CatalogItemDetail.CatalogItemDetailName;
                newOrderDetailDataItem.UnitsPerCase = orderDetail.CatalogItemDetail.Units;
                newOrderDetailDataItem.OrderedProCodeCases = orderDetail.AdjustmentQuantity * -1;
                newOrderDetailDataItem.OrderedCases = orderDetail.Quantity;
                newOrderDetailDataItem.CasePrice = orderDetail.Price;

                #region Load order detail tax data

                newOrderDetailDataItem.OrderDetailTaxes = new List<EntityData.OrderDetailTaxData>();

                foreach (LinqEntity.OrderDetailTax orderDetailTax in orderDetail.OrderDetailTaxes)
                {
                    EntityData.OrderDetailTaxData newOrderDetailTaxDataItem = new EntityData.OrderDetailTaxData();

                    newOrderDetailTaxDataItem.Id = orderDetailTax.OrderDetailTaxId;
                    newOrderDetailTaxDataItem.TaxRate = orderDetailTax.TaxRate;
                    newOrderDetailTaxDataItem.TaxAmount = orderDetailTax.TaxAmount;

                    newOrderDetailDataItem.OrderDetailTaxes.Add(newOrderDetailTaxDataItem);
                }

                #endregion

                result.OrderDetails.Add(newOrderDetailDataItem);
            }

            #endregion

            #region Load shipment group

            bool isShipmentGroupAvailable = false;
            LinqEntity.ShipmentGroup shipmentGroup = null;

            if (order.OrderDetails.Count > 0)
            {
                if (order.OrderDetails[0].ShipmentGroup != null)
                {
                    isShipmentGroupAvailable = true;
                    shipmentGroup = order.OrderDetails[0].ShipmentGroup;

                    result.DeliveryMethodId = shipmentGroup.DeliveryMethodId;
                    result.DeliveryWarehouseId = shipmentGroup.DeliveryWarehouseId;
                    result.ReqestedDeliveryDate = shipmentGroup.RequestedDeliveryDate;
                    result.ReqestedDeliveryTime = shipmentGroup.RequestedDeliveryTime;
                    result.ShippingDate = shipmentGroup.ShipmentDate;

                    if (result.DeliveryMethodId.HasValue)
                    {
                        switch (result.DeliveryMethodId.Value)
                        {
                            case (int)DeliveryDateTypeEnum.ChooseDate:
                                result.ReqestedDeliveryDateText = "";
                                if (result.ReqestedDeliveryDate.HasValue)
                                {
                                    result.ReqestedDeliveryDateText = result.ReqestedDeliveryDate.Value.ToLongDateString();
                                }
                                break;
                            case (int)DeliveryDateTypeEnum.ChooseDateAndTime:
                                result.ReqestedDeliveryDateText = "";
                                if (result.ReqestedDeliveryDate.HasValue && result.ReqestedDeliveryTime.HasValue)
                                {
                                    result.ReqestedDeliveryDateText = result.ReqestedDeliveryDate.Value.ToLongDateString() + " " + result.ReqestedDeliveryTime.Value.ToLongTimeString();
                                }
                                break;
                            case (int)DeliveryDateTypeEnum.ChooseWeekStartingOnMonday:
                                if (result.FormId.HasValue && result.ReqestedDeliveryDate.HasValue)
                                {
                                    FormSystem formSystem = new FormSystem();
                                    QSPForm.Common.Entity.WeekOfItem weekOfItem = formSystem.GetWeekOfItem(result.FormId.Value, result.ReqestedDeliveryDate.Value);
                                    result.ReqestedDeliveryDateText = weekOfItem.Description;
                                }
                                break;
                            case (int)DeliveryDateTypeEnum.ChooseWeekStartingOnMondayOtisLogic:
                                if (result.FormId.HasValue && result.ReqestedDeliveryDate.HasValue)
                                {
                                    FormSystem formSystem = new FormSystem();
                                    QSPForm.Common.Entity.WeekOfItem weekOfItem = formSystem.GetWeekOfItem(result.FormId.Value, result.ReqestedDeliveryDate.Value);
                                    result.ReqestedDeliveryDateText = weekOfItem.Description;
                                }
                                break;
                            case (int)DeliveryDateTypeEnum.ChooseWeekStartingOnSunday:
                                if (result.FormId.HasValue && result.ReqestedDeliveryDate.HasValue)
                                {
                                    FormSystem formSystem = new FormSystem();
                                    QSPForm.Common.Entity.WeekOfItem weekOfItem = formSystem.GetWeekOfItem(result.FormId.Value, result.ReqestedDeliveryDate.Value);
                                    result.ReqestedDeliveryDateText = weekOfItem.Description;
                                }
                                break;
                            case (int)DeliveryDateTypeEnum.ChooseWeekStartingOnSundayOtisLogic:
                                if (result.FormId.HasValue && result.ReqestedDeliveryDate.HasValue)
                                {
                                    FormSystem formSystem = new FormSystem();
                                    QSPForm.Common.Entity.WeekOfItem weekOfItem = formSystem.GetWeekOfItem(result.FormId.Value, result.ReqestedDeliveryDate.Value);
                                    result.ReqestedDeliveryDateText = weekOfItem.Description;
                                }
                                break;
                        }
                    }
                }
            }

            #endregion

            if (loadChildrenObjects)
            {
                #region Load extended organization data

                result.OrderTypeName = order.OrderType.OrderTypeName;


                if (order.FormId.HasValue)
                {
                    result.FormName = order.Form.FormName;
                }


                result.StatusName = order.OrderStatus.OrderStatusName;
                result.StatusColor = order.OrderStatus.ColorCode;


                LinqEntity.FieldSalesManager fsm =
                    (from f in db.FieldSalesManagers
                     where f.FmId == order.FmId
                     select f
                     ).FirstOrDefault();

                if (fsm != null)
                {
                    result.FsmFirstName = fsm.FirstName;
                    result.FsmLastName = fsm.LastName;
                }


                LinqEntity.User createUser =
                    (from u in db.Users
                     where u.UserId == order.CreateUserId
                     select u
                     ).SingleOrDefault();

                if (createUser != null)
                {
                    result.CreateUserFirstName = createUser.FirstName;
                    result.CreateUserLastName = createUser.LastName;
                }


                if (order.UpdateUserId.HasValue)
                {
                    LinqEntity.User updateUser =
                        (from u in db.Users
                         where u.UserId == order.UpdateUserId.Value
                         select u
                         ).SingleOrDefault();

                    if (updateUser != null)
                    {
                        result.UpdateUserFirstName = updateUser.FirstName;
                        result.UpdateUserLastName = updateUser.LastName;
                    }
                }


                if (shipmentGroup.DeliveryMethodId.HasValue)
                {
                    LinqEntity.DeliveryMethod deliveryMethod =
                        (from dm in db.DeliveryMethods
                         where dm.DeliveryMethodId == shipmentGroup.DeliveryMethodId.Value
                         select dm
                         ).SingleOrDefault();

                    if (deliveryMethod != null)
                    {
                        result.DeliveryMethodName = deliveryMethod.DeliveryMethodName;
                    }
                }


                if (shipmentGroup.DeliveryWarehouseId.HasValue)
                {
                    LinqEntity.Warehouse warehouse =
                        (from w in db.Warehouses
                         where w.WarehouseId == shipmentGroup.DeliveryWarehouseId.Value
                         select w
                         ).SingleOrDefault();

                    if (warehouse != null)
                    {
                        result.DeliveryWarehouseName = warehouse.WarehouseName;
                    }
                }

                #endregion

                #region Load shipping addresses

                result.ShippingAddress = new EntityData.AddressData();
                result.ShippingAddress.Subdivision = new EntityData.SubdivisionData();
                result.ShippingAddress.Subdivision.Country = new EntityData.CountryData();

                if (isShipmentGroupAvailable)
                {
                    result.ShippingAddress.Address1 = shipmentGroup.PostalAddress.Address1;
                    result.ShippingAddress.Address2 = shipmentGroup.PostalAddress.Address2;
                    result.ShippingAddress.City = shipmentGroup.PostalAddress.City;
                    result.ShippingAddress.County = shipmentGroup.PostalAddress.County;
                    result.ShippingAddress.CreateDate = shipmentGroup.PostalAddress.CreateDate;
                    result.ShippingAddress.CreateUserId = shipmentGroup.PostalAddress.CreateUserId;
                    result.ShippingAddress.FirstName = shipmentGroup.PostalAddress.FirstName;
                    result.ShippingAddress.Id = shipmentGroup.PostalAddress.PostalAddressId;
                    result.ShippingAddress.IsDeleted = (shipmentGroup.PostalAddress.IsDeleted == 1);
                    result.ShippingAddress.IsResidentialArea = shipmentGroup.PostalAddress.IsResidentialArea ?? false;
                    result.ShippingAddress.LastName = shipmentGroup.PostalAddress.LastName;
                    result.ShippingAddress.Name = shipmentGroup.PostalAddress.Name;
                    result.ShippingAddress.UpdateDate = shipmentGroup.PostalAddress.UpdateDate;
                    result.ShippingAddress.UpdateUserId = shipmentGroup.PostalAddress.UpdateUserId;
                    result.ShippingAddress.Zip = shipmentGroup.PostalAddress.Zip;
                    result.ShippingAddress.Zip4 = shipmentGroup.PostalAddress.Zip4;

                    result.ShippingAddress.Subdivision.Category = shipmentGroup.PostalAddress.Subdivision.SubdivisionCategory;
                    result.ShippingAddress.Subdivision.Code = shipmentGroup.PostalAddress.Subdivision.SubdivisionCode;
                    result.ShippingAddress.Subdivision.Name1 = shipmentGroup.PostalAddress.Subdivision.SubdivisionName1;
                    result.ShippingAddress.Subdivision.Name2 = shipmentGroup.PostalAddress.Subdivision.SubdivisionName2;
                    result.ShippingAddress.Subdivision.Name3 = shipmentGroup.PostalAddress.Subdivision.SubdivisionName3;
                    result.ShippingAddress.Subdivision.RegionalDivision = shipmentGroup.PostalAddress.Subdivision.RegionalDivision;
                    result.ShippingAddress.Subdivision.Country.Code = shipmentGroup.PostalAddress.Subdivision.CountryCode;

                    if (shipmentGroup.PhoneNumber != null)
                    {
                        result.ShippingAddress.Phone = shipmentGroup.PhoneNumber.Number;
                    }

                    if (shipmentGroup.FaxNumber != null)
                    {
                        result.ShippingAddress.Phone = shipmentGroup.FaxNumber.Number;
                    }

                    if (shipmentGroup.Email != null)
                    {
                        result.ShippingAddress.Email = shipmentGroup.Email.EmailAddress;
                    }
                }

                #endregion

                #region Campaign data

                result.Campaign = new EntityData.CampaignData();

                result.Campaign.Id = order.Campaign.CampaignId;
                result.Campaign.AccountId = order.Campaign.AccountId;
                result.Campaign.FiscalYear = order.Campaign.FiscalYear;
                result.Campaign.StartDate = order.Campaign.StartDate;
                result.Campaign.EndDate = order.Campaign.EndDate;
                result.Campaign.Enrollment = order.Campaign.Enrollment;
                result.Campaign.GoalEstimatedGross = order.Campaign.GoalEstimatedGross;

                result.Campaign.ProgramTypeId = order.Campaign.ProgramTypeId;
                result.Campaign.ProgramTypeName = order.Campaign.ProgramType.ProgramTypeName;

                result.Campaign.TradeClassId = order.Campaign.TradeClassId;
                if (order.Campaign.TradeClassId.HasValue)
                {
                    result.Campaign.TradeClassName = order.Campaign.TradeClass.TradeClassName;
                }

                result.Campaign.WarehouseId = order.Campaign.WarehouseId;
                if (order.Campaign.WarehouseId.HasValue)
                {
                    LinqEntity.Warehouse warehouse =
                        (from w in db.Warehouses
                         where w.WarehouseId == order.Campaign.WarehouseId.Value
                         select w
                         ).SingleOrDefault();

                    if (warehouse != null)
                    {
                        result.Campaign.WarehouseName = warehouse.WarehouseName;
                    }
                }

                LinqEntity.Order lastOrder =
                    (from o in db.Orders
                     where o.CampaignId == order.Campaign.CampaignId
                        && o.IsDeleted == false
                     orderby o.CreateDate descending
                     select o
                     ).FirstOrDefault();

                if (lastOrder != null)
                {
                    result.Campaign.LastOrderDate = lastOrder.CreateDate;

                    DateTime dateSpan = new DateTime(DateTime.Now.Ticks - lastOrder.CreateDate.Ticks);
                    int numberOfMonths = ((dateSpan.Year - DateTime.MinValue.Year) * 12) + dateSpan.Month;
                    result.Campaign.InactiveMonths = numberOfMonths.ToString();
                }
                else
                {
                    result.Campaign.LastOrderDate = null;
                    result.Campaign.InactiveMonths = "";
                }

                #endregion

                #region Order charges

                ChargeSystem chargeSystem = new ChargeSystem();
                result.OrderCharges = chargeSystem.GetChargesFromOrder(result.Id);

                #endregion

                #region Order summary

                result.OrderSummary = new EntityData.OrderSummaryData();


                result.OrderSummary.SubTotal = 0;
                foreach (EntityData.OrderDetailData orderDetailData in result.OrderDetails)
                {
                    result.OrderSummary.SubTotal += orderDetailData.Total;
                }


                result.OrderSummary.Credits = 0;
                result.OrderSummary.Surcharges = 0;
                foreach (EntityData.ChargeData chargeData in result.OrderCharges)
                {
                    if (chargeData.Amount.HasValue)
                    {
                        if (chargeData.Amount > 0)
                        {
                            result.OrderSummary.Surcharges += chargeData.Amount.Value;
                        }
                        if (chargeData.Amount < 0)
                        {
                            result.OrderSummary.Credits += chargeData.Amount.Value;
                        }
                    }
                }


                result.OrderSummary.ShippingCharges = 0;
                if (isShipmentGroupAvailable)
                {
                    if (shipmentGroup.ShippingCharges.HasValue)
                    {
                        result.OrderSummary.ShippingCharges += shipmentGroup.ShippingCharges.Value;
                    }
                    if (shipmentGroup.ShippingExpeditedCharges.HasValue)
                    {
                        result.OrderSummary.ShippingCharges += shipmentGroup.ShippingExpeditedCharges.Value;
                    }
                    if (shipmentGroup.ShippingExpeditedFreightCharges.HasValue)
                    {
                        result.OrderSummary.ShippingCharges += shipmentGroup.ShippingExpeditedFreightCharges.Value;
                    }

                    //shipmentGroup.ExpeditedFreightChargePaymentAssignmentTypeId;
                    //shipmentGroup.ShippingCharges
                    //shipmentGroup.ShippingExpeditedCharges;
                    //shipmentGroup.ShippingExpeditedFreightCharges;
                }


                if (result.OrderDetails.Count > 0)
                {
                    result.OrderSummary.TaxRate = Convert.ToDecimal(result.OrderDetails[0].TaxRate);
                }
                else
                {
                    result.OrderSummary.TaxRate = 0;
                }
                result.OrderSummary.TaxAmount = result.OrderSummary.TaxRate * result.OrderSummary.SubTotal;


                result.OrderSummary.GrandTotal =
                    result.OrderSummary.SubTotal +
                    result.OrderSummary.TaxAmount +
                    result.OrderSummary.ShippingCharges +
                    result.OrderSummary.Surcharges +
                    result.OrderSummary.Credits;

                #endregion

                #region Status history

                StatusSystem statusSystem = new StatusSystem();
                result.StatusHistory = statusSystem.GetStatusHistoryFromOrder(result.Id);

                #endregion
            }

            return result;
        }


        public void MapOrderToProgramAgreement(int orderId, int userId)
        {
            LinqContext.OrderExpressDataContext db = new LinqContext.OrderExpressDataContext();

            LinqEntity.Order order = (from o in db.Orders
                                      where o.OrderId == orderId
                                      select o).Single();

            List<int> requiredFormIdList = (from frf in db.FormRequiresForms
                                            where frf.FormId == order.FormId
                                                && frf.IsDeleted == false
                                            select frf.RequiredFormId).ToList();

            List<int> programAgreementIdList = (from pac in db.ProgramAgreementCampaigns
                                                where pac.CampaignId == order.CampaignId
                                                    && requiredFormIdList.Contains(pac.ProgramAgreement.FormId ?? 0)
                                                    && pac.IsDeleted == false
                                                select pac.ProgramAgreementId).ToList();

            foreach (int programAgreementId in programAgreementIdList)
            {
                this.MapOrderToProgramAgreement(orderId, programAgreementId, userId);
            }
        }
        public void MapOrderToProgramAgreement(int orderId, int programAgreementId, int userId)
        {
            LinqContext.OrderExpressDataContext db = new LinqContext.OrderExpressDataContext();

            int count = (from pao in db.ProgramAgreementOrders
                         where pao.OrderId == orderId
                            && pao.ProgramAgreementId == programAgreementId
                         select pao).Count();

            if (count == 0)
            {
                LinqEntity.ProgramAgreementOrder pao = new LinqEntity.ProgramAgreementOrder();

                pao.OrderId = orderId;
                pao.ProgramAgreementId = programAgreementId;
                pao.IsDeleted = false;
                pao.CreateDate = DateTime.Now;
                pao.CreateUserId = userId;
                pao.UpdateDate = pao.CreateDate;
                pao.UpdateUserId = pao.CreateUserId;

                db.ProgramAgreementOrders.InsertOnSubmit(pao);
                db.SubmitChanges();
            }
        }

        public LinqEntity.OrderType GetOrderType(int orderTypeId)
        {
            LinqEntity.OrderType result = new LinqEntity.OrderType();

            LinqContext.OrderExpressDataContext db = new LinqContext.OrderExpressDataContext();

            result = (from ot in db.OrderTypes
                      where ot.OrderTypeId == orderTypeId
                      select ot
                      ).SingleOrDefault();

            return result;
        }
        public List<LinqEntity.OrderType> GetOrderTypes()
        {
            List<LinqEntity.OrderType> result = new List<LinqEntity.OrderType>();

            LinqContext.OrderExpressDataContext db = new LinqContext.OrderExpressDataContext();

            result = (from ot in db.OrderTypes
                      select ot
                      ).ToList();

            return result;
        }
        public LinqEntity.Source GetSource(int sourceId)
        {
            LinqEntity.Source result = new LinqEntity.Source();

            LinqContext.OrderExpressDataContext db = new LinqContext.OrderExpressDataContext();

            result = (from s in db.Sources
                      where s.SourceId == sourceId
                      select s
                      ).SingleOrDefault();

            return result;
        }
        public List<LinqEntity.Source> GetSources()
        {
            List<LinqEntity.Source> result = new List<LinqEntity.Source>();

            LinqEntity.Source source1 = new LinqEntity.Source();
            source1.SourceId = 1;
            source1.SourceGroupId = 1;
            source1.SourceName = "Order Express";
            result.Add(source1);

            LinqEntity.Source source2 = new LinqEntity.Source();
            source2.SourceId = 20;
            source2.SourceGroupId = 2;
            source2.SourceName = "efundraising.com";
            result.Add(source2);

            //LinqContext.OrderExpressDataContext db = new LinqContext.OrderExpressDataContext();

            //result = (from s in db.Sources
            //          select s
            //          ).ToList();

            return result;
        }

        public bool IsOrderEditable(int orderId)
        {
            bool result = false;

            EntityData.OrderData orderData = this.GetOrder(orderId);
            result = this.IsOrderEditable(orderData);

            return result;
        }
        public bool IsOrderEditable(EntityData.OrderData orderData)
        {
            bool result = false;

            if (orderData.StatusId >= 0 && orderData.StatusId < 101)
            {
                result = true;
            }
            else if (orderData.StatusId >= 800)
            {
                result = true;
            }

            return result;
        }


        public List<LinqEntity.QCAPOrderDetail> GetQCAPOrderDetails(int qCAPOrderId)
        {
            List<LinqEntity.QCAPOrderDetail> result = new List<LinqEntity.QCAPOrderDetail>();

            LinqContext.OrderExpressDataContext db = new LinqContext.OrderExpressDataContext();


            result =
                (from o in db.QCAPOrderDetails
                 where o.qcap_order_id == qCAPOrderId
                 select o
                 ).ToList <LinqEntity.QCAPOrderDetail>();


            return result;
        }

        /// <summary>
        /// Soft delete the temporary qcap order
        /// </summary>
        /// <param name="qCapOrderId"></param>
        /// <param name="loggedInUserId"></param>
        public void DeleteQCAPOrder(int qCapOrderId, int loggedInUserId)
        {
            try
            {
                LinqContext.OrderExpressDataContext db = new LinqContext.OrderExpressDataContext();

                var result =
                    (from o in db.QCAPOrders
                     where o.qcap_order_id == qCapOrderId
                     select o
                     ).Single();

                result.deleted = true;
                result.update_date = DateTime.Now;
                result.update_user_id = loggedInUserId;

                db.SubmitChanges();
            }
            catch
            {
                //do nothing
            }
        }

        /// <summary>
        /// retrieve all valid temporary orders against the user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<QSP.OrderExpress.Common.Data.QCAPOrderData> GetQCAPOrdersForUser(int userId)
        {
            List<QSP.OrderExpress.Common.Data.QCAPOrderData> result = new List<QSP.OrderExpress.Common.Data.QCAPOrderData>();

            LinqContext.OrderExpressDataContext db = new LinqContext.OrderExpressDataContext();


            result = (from o in db.QCAPOrders
                    join c in db.Campaigns on o.campaign_id equals c.CampaignId
                    join ac in db.Accounts on c.AccountId equals ac.AccountId
                    join f in db.Forms on o.form_id equals f.FormId
                    join od in db.QCAPOrderDetails on o.qcap_order_id equals od.qcap_order_id
                    join ci in db.CatalogItemDetails on od.catalog_item_detail_id equals ci.CatalogItemDetailId
                    //group od.quantity by o.qcap_order_id into odgroup
                    where o.user_id == userId && o.deleted == false && f.IsDeleted == false && f.IsEnabled == true
                        && od.deleted == false
                    group od.quantity * ci.Price by new {o.qcap_order_id,
                        c.AccountId, ac.AccountName, 
                        o.form_id, 
                        f.FormName,
                        o.campaign_id,
                        o.order_date} into t
                    select new QSP.OrderExpress.Common.Data.QCAPOrderData
                    {
                        Id = t.Key.qcap_order_id,
                        AccountId = t.Key.AccountId,
                        AccountName = t.Key.AccountName,
                        FormId = t.Key.form_id,
                        FormName = t.Key.FormName,
                        CampaignId = t.Key.campaign_id,
                        OrderDate = t.Key.order_date,
                        OrderTotal = t.Sum()
                    }).ToList() as List<QSP.OrderExpress.Common.Data.QCAPOrderData>;

                 


            return result;
        }

        #endregion

        #region Version 1 code

        QSPForm.Common.DataDef.OrderHeaderTable orderHeader;
        dataAccessRef objDataAccess;
        const string ERR01 = "orderHeader is not initiated";

        public OrderSystem()
        {
            objDataAccess = new dataAccessRef();
        }

        public bool Delete(dataDef Table)
        {
            //We call a method from the inherit class, but the
            //validation with the overriden Validate Method 
            //is in the current class
            return this.Delete(Table, objDataAccess);
        }

        protected override bool Validate(DataRow row)
        {
            bool isValid = true;

            //Clear all errors
            row.ClearErrors();

            if ((row.RowState == DataRowState.Modified) || (row.RowState == DataRowState.Added))
            {
                isValid = IsValid_RequiredFields(row);
            }

            return isValid;
        }
        private bool IsValid_RequiredFields(DataRow row)
        {
            bool IsValid = true;
            //Order
            IsValid &= IsValid_RequiredField(row, dataDef.FLD_CAMPAIGN_ID, "Campaign");
            //Order Number
            //IsValid &= IsValid_RequiredField(row, dataDef.FLD_ORDER_GROUP_ID, "Order Group ID");
            //FM Number
            //IsValid &= IsValid_RequiredField(row, dataDef.FLD_ORDER_DATE, "Order Date");

            if (!IsValid)
            {
                messageManager.ValidationExceptionType = QSPFormExceptionType.RequiredFields;
            }

            return IsValid;
        }

        public int CountAllByFMID(string FMID, int OrderStatusID)
        {
            int count = 0;
            //
            // Get the user DataTable from the DataLayer
            //
            count = objDataAccess.CountAllWfm_idLogic(FMID, OrderStatusID);

            return count;
        }
        public dataDef SelectAllByCampaignID(int CampaignID)
        {
            dataDef dTbl;
            //
            // Get the user DataTable from the DataLayer
            //
            dTbl = objDataAccess.SelectAllWcampaign_idLogic(CampaignID);

            return dTbl;
        }
        public dataDef SelectAllByCampaignID(int CampaignID, string sFMID, int FSM_DisplayMode)
        {
            dataDef dTbl;
            //
            // Get the user DataTable from the DataLayer
            //
            dTbl = objDataAccess.SelectAllWcampaign_idLogic(CampaignID, sFMID, FSM_DisplayMode);

            return dTbl;
        }
        private int GetFulfOrderStatus(int FulfOrderID)
        {
            int OrderStatusID = 0;
            OEHORDPTable dTbl = new OEHORDPTable();
            //
            // Get the user DataTable from the DataLayer
            //
            QSPForm.DataRepository.OEHORDP synch_excord = new QSPForm.DataRepository.OEHORDP();

            //***CHECK IF IT'S RELEASE IN AS400
            DateTime dateStartProcess = DateTime.Now;
            System.Diagnostics.Debug.WriteLine("Time Begin Check in AS400 " + dateStartProcess.ToLongTimeString());

            dTbl = synch_excord.SelectOne(FulfOrderID);

            DateTime dateEndProcess = DateTime.Now;
            System.Diagnostics.Debug.WriteLine("Time End Check in AS400 " + dateEndProcess.ToLongTimeString());
            TimeSpan NbMsec = dateEndProcess - dateStartProcess;
            System.Diagnostics.Debug.WriteLine("Time Duration Check in AS400 " + NbMsec.TotalMilliseconds.ToString());


            if (dTbl.Rows.Count == 0)
            {
                //Try another time to see if it's moved to the invoice order table
                return 401;
                //dTbl = synch_excord.SelectOne_InvoicedOrder(FulfOrderID);
            }

            if (dTbl.Rows.Count > 0)
            {
                string sFulfOrderStatus = "";
                int ShipMonth = 0;
                int ShipDay = 0;
                int ShipYear = 0;

                DataRow row = dTbl.Rows[0];
                if (!row.IsNull(OEHORDPTable.FLD_STATUS))
                {
                    sFulfOrderStatus = row[OEHORDPTable.FLD_STATUS].ToString();
                }
                if (!row.IsNull(OEHORDPTable.FLD_SHIP_MONTH))
                    ShipMonth = Convert.ToInt32(row[OEHORDPTable.FLD_SHIP_MONTH]);
                if (!row.IsNull(OEHORDPTable.FLD_SHIP_DAY))
                    ShipDay = Convert.ToInt32(row[OEHORDPTable.FLD_SHIP_DAY]);
                if (!row.IsNull(OEHORDPTable.FLD_SHIP_YEAR))
                    ShipYear = Convert.ToInt32(row[OEHORDPTable.FLD_SHIP_YEAR]);
                OrderStatusID = objDataAccess.GetOrderStatusID(sFulfOrderStatus, ShipMonth, ShipDay, ShipYear);

            }

            return OrderStatusID;
        }
        public dataDef SelectOne(int ID)
        {
            dataDef dTbl;
            //
            // Get the user DataTable from the DataLayer
            //
            dTbl = objDataAccess.SelectOne(ID);

            return dTbl;
        }
        public dataDef SelectAll_Search(int SearchType, String Criteria, string FM_ID, int StatusCategoryID, int ProgramType, string SubdivisionCode, DateTime StartDate, DateTime EndDate, string FMName)
        {
            dataDef dTbl;

            //
            // Get the user DataTable from the DataLayer
            //				
            dTbl = objDataAccess.SelectAll_Search(SearchType, Criteria, FM_ID, StatusCategoryID, ProgramType, SubdivisionCode, StartDate, EndDate, FMName);

            return dTbl;
        }
        public OrderStatusChangeTable SelectAllOrderStatusChangeByOrderID(int OrderID)
        {
            OrderStatusChangeTable dTbl;
            //
            // Get the user DataTable from the DataLayer
            //
            Data.Order_status_change ordStatusDataAccess = new Data.Order_status_change();
            dTbl = ordStatusDataAccess.SelectAllWorder_idLogic(OrderID);

            return dTbl;

        }

        public OLHORDPTable ExchangeTable_SelectAll()
        {
            OLHORDPTable dTbl = new OLHORDPTable();
            //
            // Get the user DataTable from the DataLayer
            //
            QSPForm.DataRepository.OLHORDP synch_excord = new QSPForm.DataRepository.OLHORDP();
            dTbl = synch_excord.SelectAll();
            //Proceed the remainig information needed like status name and fsm name
            //Account Status
            CommonSystem comSys = new CommonSystem();
            DataTable dTblStatus = new DataTable();
            dTblStatus = comSys.SelectAllOrderStatus();
            DataView dvStatus = new DataView(dTblStatus);
            dvStatus.Sort = dTblStatus.Columns[0].ColumnName;

            CUserSystem cuserSys = new CUserSystem();
            CUserTable dTblFM = new CUserTable();
            dTblFM = cuserSys.SelectAllFM();
            DataView dvFM = new DataView(dTblFM);
            dvFM.Sort = CUserTable.FLD_FM_ID;

            foreach (DataRow row in dTbl.Rows)
            {
                //Find Status
                if (!row.IsNull(OLHORDPTable.FLD_STATUS))
                {
                    int iStatus = Convert.ToInt32(row[OLHORDPTable.FLD_STATUS]);
                    int iIndex = dvStatus.Find(iStatus);
                    if (iIndex > -1)
                    {
                        row[OLHORDPTable.FLD_STATUS_NAME] = dvStatus[iIndex][1];
                        if (!dvStatus[iIndex].Row.IsNull(4))
                            row[OLHORDPTable.FLD_STATUS_COLOR_CODE] = dvStatus[iIndex][4];
                        else
                            row[OLHORDPTable.FLD_STATUS_COLOR_CODE] = "White";
                    }
                }

                //Find Status
                if (!row.IsNull(OLHORDPTable.FLD_FSM))
                {
                    string sFMID = row[OLHORDPTable.FLD_FSM].ToString();
                    sFMID = sFMID.Substring(2);
                    int iIndex = dvFM.Find(sFMID);
                    if (iIndex > -1)
                    {
                        row[OLHORDPTable.FLD_FSM_NAME] = dvFM[iIndex][CUserTable.FLD_FSM_NAME];
                    }
                }
            }

            return dTbl;

        }
        public OrderData SelectAllDetail(int ID)
        {
            return SelectAllDetail(ID, false);
        }
        public OrderData SelectAllDetail(int ID, bool IncludeAllFormProduct)
        {
            return objDataAccess.SelectAllDetail(ID, IncludeAllFormProduct);
        }
        public bool InsertAllDetail(OrderData dtsOrderData, int UserID)
        {

            //Variable to handle the operation in One transaction transaction
            String TransactionName = "Order_InsertAllDetail";
            Data.ConnectionProvider connProvider = new Data.ConnectionProvider();
            OrderData dts = (OrderData)dtsOrderData;
            bool IsSuccess = true;
            try
            {
                //This method fill the All Data needed for an organization
                //into a predefined DataSet	

                //Data Object Instanciation
                Data.Order_group ordGrpDataAccess = new Data.Order_group();
                Data.Order ordDataAccess = new Data.Order();
                Data.OrderDetail ordDetailDataAccess = new Data.OrderDetail();
                Data.OrderDetailTax ordTaxDataAccess = new Data.OrderDetailTax();
                Data.Shipment_group shipDataAccess = new Data.Shipment_group();
                Data.Postal_address_entity postDataAccess = new Data.Postal_address_entity();
                Data.Phone_number_entity phoneDataAccess = new Data.Phone_number_entity();
                Data.Email_entity emailDataAccess = new Data.Email_entity();
                Data.Order_status_change ordStatusDataAccess = new Data.Order_status_change();

                // Pass the created ConnectionProvider object to the data-access objects.
                ordGrpDataAccess.MainConnectionProvider = connProvider;
                ordDataAccess.MainConnectionProvider = connProvider;
                ordDetailDataAccess.MainConnectionProvider = connProvider;
                ordTaxDataAccess.MainConnectionProvider = connProvider;
                shipDataAccess.MainConnectionProvider = connProvider;
                postDataAccess.MainConnectionProvider = connProvider;
                phoneDataAccess.MainConnectionProvider = connProvider;
                emailDataAccess.MainConnectionProvider = connProvider;
                ordStatusDataAccess.MainConnectionProvider = connProvider;

                ordGrpDataAccess.AcceptChangesDuringUpdate = false;
                ordDataAccess.AcceptChangesDuringUpdate = false;
                ordDetailDataAccess.AcceptChangesDuringUpdate = false;
                ordTaxDataAccess.AcceptChangesDuringUpdate = false;
                shipDataAccess.AcceptChangesDuringUpdate = false;
                postDataAccess.AcceptChangesDuringUpdate = false;
                phoneDataAccess.AcceptChangesDuringUpdate = false;
                emailDataAccess.AcceptChangesDuringUpdate = false;
                ordStatusDataAccess.AcceptChangesDuringUpdate = false;


                connProvider.OpenConnection();
                connProvider.BeginTransaction(TransactionName);

                int OrderID = 0;
                int ShipGrpID = 0;

                //This method fill the All Data needed for an order
                //into a predefined DataSet	
                //Do a Clone of the DataSet to keep it if error occured

                OrderHeaderTable orderHeader = dts.OrderHeader;

                DataRow ordRow = orderHeader.Rows[0];
                DataRow shipRow = dts.ShipmentGroup.Rows[0];
                OrderID = Convert.ToInt32(ordRow[OrderHeaderTable.FLD_PKID]);
                ShipGrpID = Convert.ToInt32(shipRow[ShipmentGroupTable.FLD_PKID]);


                // Check if we need to wait for approval
                bool isApprovalNeeded = false;
                if ((int)ordRow[8] == 5)
                {
                    isApprovalNeeded = true;
                }


                //Manage the Shipping Address
                //We have to delete the shipping Address if 
                //this pick up at warehouse this info have been copied
                //as default when creating the order
                if (!shipRow.IsNull(ShipmentGroupTable.FLD_DELIVERY_METHOD_ID))
                {
                    int deliveryMethodID = Convert.ToInt32(shipRow[ShipmentGroupTable.FLD_DELIVERY_METHOD_ID]);
                    if (deliveryMethodID != DeliveryMethod.PICK_UP_AT_WAREHOUSE)
                    {
                        //We have to delete the previous info on warehouse if exist.
                        if (!shipRow.IsNull(ShipmentGroupTable.FLD_DELIVERY_WAREHOUSE_ID))
                            shipRow[ShipmentGroupTable.FLD_DELIVERY_WAREHOUSE_ID] = DBNull.Value;
                    }
                }
                //We have to begin with Postal address, phone Number...
                if (dts.OrderPostalAddress.GetChanges() != null)
                {
                    postDataAccess.UpdateBatch(dts.OrderPostalAddress);
                }
                if (dts.OrderPhoneNumber.GetChanges() != null)
                {
                    phoneDataAccess.UpdateBatch(dts.OrderPhoneNumber);
                }
                if (dts.OrderEmailAddress.GetChanges() != null)
                {
                    emailDataAccess.UpdateBatch(dts.OrderEmailAddress);
                }
                //Reassign All Postal Address, Phone Number and Email Address
                ReassignBillingInformation(dts.OrderHeader,
                                            dts.OrderPostalAddress,
                                            dts.OrderPhoneNumber,
                                            dts.OrderEmailAddress);
                //Reassign All Postal Address, Phone Number and Email Address
                ReassignShippingInformation(dts.ShipmentGroup,
                                            dts.OrderPostalAddress,
                                            dts.OrderPhoneNumber,
                                            dts.OrderEmailAddress);

                //Create the Order Group
                ordGrpDataAccess.UpdateBatch(dts.OrderGroup);

                //Replace the Order groupID in the Order Header
                DataRow ordGrpRow = dts.OrderGroup.Rows[0];
                ordRow[OrderHeaderTable.FLD_ORDER_GROUP_ID] = ordGrpRow[OrderGroupTable.FLD_PKID];

                //Submit information for Order Header
                ordDataAccess.UpdateBatch(dts.OrderHeader);

                //Submit information for ShipmentGroup
                shipDataAccess.UpdateBatch(dts.ShipmentGroup);

                //Rematch Order Detail for OrderID and ShipmentGroupID
                //Is the same for all
                OrderID = Convert.ToInt32(ordRow[OrderHeaderTable.FLD_PKID]);
                //Redo the reference
                shipRow = dts.ShipmentGroup.Rows[0];
                int ShipID = Convert.ToInt32(shipRow[ShipmentGroupTable.FLD_PKID]);

                //Remove detail with quantity 0
                dts.OrderDetail.CleanData();
                //Reaffect ID
                PrepareTransactionWithNewID(dts.OrderHeader,
                                            dts.ShipmentGroup,
                                            dts.OrderDetail,
                                            dts.OrderException);

                //Submit information for Order Detail
                //ordDetailDataAccess.AcceptChangesDuringUpdate = false;
                ordDetailDataAccess.UpdateBatch(dts.OrderDetail);

                //Tax Calculation
                //CalculateTax(dts, UserID);
                if (dts.OrderPhoneNumber.GetChanges() != null)
                {
                    dts.OrderDetailTax.CleanData();
                    foreach (DataRow detRow in dts.OrderDetail)
                    {
                        if (detRow.RowState != DataRowState.Deleted)
                        {
                            int OldID = Convert.ToInt32(detRow[OrderDetailTable.FLD_PKID, DataRowVersion.Original]);
                            int NewID = Convert.ToInt32(detRow[OrderDetailTable.FLD_PKID, DataRowVersion.Current]);
                            foreach (DataRow detTaxRow in dts.OrderDetailTax)
                            {
                                if (detTaxRow.RowState != DataRowState.Deleted)
                                {
                                    if (detTaxRow[OrderDetailTaxTable.FLD_ORDER_DETAIL_ID].ToString() == OldID.ToString())
                                    {
                                        detTaxRow[OrderDetailTaxTable.FLD_ORDER_DETAIL_ID] = NewID;
                                    }
                                }
                            }
                        }
                    }
                    ordTaxDataAccess.UpdateBatch(dts.OrderDetailTax);
                }
                //ordTaxDataAccess.CalculateWorder_idLogic(OrderID);

                dts.OrderSupply.CleanData();

                if (dts.OrderSupply.TotalQuantity > 0)
                {
                    InsertSupplyOrder(dts, connProvider);
                }


                //// CHR CODE
                //if (isApprovalNeeded)
                //{
                //    int orderId = Convert.ToInt32(dts.OrderHeader.Rows[0][0]);
                //    QSP.Business.Fulfillment.Order.SetOrderToWaitForApproval(orderId);
                //}


                //Submit information for Exception and Task
                Refresh(dts, UserID, DataOperation.INSERT, connProvider);

                //Register the add in the order_status_change table.
                OrderStatusChangeTable dTblChange = new OrderStatusChangeTable();
                DataRow newChangerRow = dTblChange.NewRow();

                newChangerRow[OrderStatusChangeTable.FLD_ORDER_ID] = ordRow[dataDef.FLD_PKID];
                newChangerRow[OrderStatusChangeTable.FLD_ORDER_STATUS_ID] = ordRow[dataDef.FLD_ORDER_STATUS_ID];
                newChangerRow[OrderStatusChangeTable.FLD_STATUS_CHANGE_REASON] = "New Insert from Order Express";
                newChangerRow[OrderStatusChangeTable.FLD_CREATE_USER_ID] = UserID;
                dTblChange.Rows.Add(newChangerRow);
                //Execute the chnage to DB
                ordStatusDataAccess.Insert(dTblChange);


                //Commit transaction 
                connProvider.CommitTransaction();
                dtsOrderData.Merge(dts, false);
                dtsOrderData.AcceptChanges();
                IsSuccess = true;


                // CHR CODE
                if (isApprovalNeeded)
                {
                    int orderId = Convert.ToInt32(dts.OrderHeader.Rows[0][0]);
                    QSP.Business.Fulfillment.Order.SetOrderToWaitForApproval(orderId);
                }

            }
            catch (Exception ex)
            {
                if (connProvider != null)
                {
                    if (connProvider.DBConnection.State != ConnectionState.Closed)
                        connProvider.RollbackTransaction(TransactionName);
                }
                IsSuccess = false;
                throw ex;
            }
            finally
            {
                if (connProvider != null)
                {
                    if (connProvider.DBConnection.State != ConnectionState.Closed)
                        connProvider.CloseConnection(false);
                }
            }

            return IsSuccess;
        }

        public bool UpdateAllDetail(OrderData dtsOrderData, int UserID)
        {
            //Variable to handle the operation in One transaction transaction
            String TransactionName = "Order_UpdateAllDetail";
            OrderData dts = (OrderData)dtsOrderData;
            Data.ConnectionProvider connProvider = new Data.ConnectionProvider();

            bool IsSuccess = true;
            try
            {
                //This method fill the All Data needed for an organization
                //into a predefined DataSet	

                //Data Object Instanciation
                Data.Order_group ordGrpDataAccess = new Data.Order_group();
                Data.Order ordDataAccess = new Data.Order();
                Data.OrderDetail ordDetailDataAccess = new Data.OrderDetail();
                Data.OrderDetailTax ordTaxDataAccess = new Data.OrderDetailTax();
                Data.Shipment_group shipDataAccess = new Data.Shipment_group();
                Data.Postal_address_entity postDataAccess = new Data.Postal_address_entity();
                Data.Phone_number_entity phoneDataAccess = new Data.Phone_number_entity();
                Data.Email_entity emailDataAccess = new Data.Email_entity();
                Data.Order_status_change ordStatusDataAccess = new Data.Order_status_change();


                // Pass the created ConnectionProvider object to the data-access objects.
                ordGrpDataAccess.MainConnectionProvider = connProvider;
                ordDataAccess.MainConnectionProvider = connProvider;
                ordDetailDataAccess.MainConnectionProvider = connProvider;
                ordTaxDataAccess.MainConnectionProvider = connProvider;
                shipDataAccess.MainConnectionProvider = connProvider;
                postDataAccess.MainConnectionProvider = connProvider;
                phoneDataAccess.MainConnectionProvider = connProvider;
                emailDataAccess.MainConnectionProvider = connProvider;
                ordStatusDataAccess.MainConnectionProvider = connProvider;

                ordGrpDataAccess.AcceptChangesDuringUpdate = false;
                ordDataAccess.AcceptChangesDuringUpdate = false;
                ordDetailDataAccess.AcceptChangesDuringUpdate = false;
                ordTaxDataAccess.AcceptChangesDuringUpdate = false;
                shipDataAccess.AcceptChangesDuringUpdate = false;
                postDataAccess.AcceptChangesDuringUpdate = false;
                phoneDataAccess.AcceptChangesDuringUpdate = false;
                emailDataAccess.AcceptChangesDuringUpdate = false;
                ordStatusDataAccess.AcceptChangesDuringUpdate = false;

                connProvider.OpenConnection();
                connProvider.BeginTransaction(TransactionName);

                int OrderID = 0;
                int OrderStatusID = 0;
                int ShipGrpID = 0;
                bool HasChanges = false;

                //This method fill the All Data needed for an order
                //into a predefined DataSet	
                OrderHeaderTable orderHeader = dts.OrderHeader;
                DataRow ordRow = orderHeader.Rows[0];
                DataRow shipRow = dts.ShipmentGroup.Rows[0];
                OrderID = Convert.ToInt32(ordRow[OrderHeaderTable.FLD_PKID]);
                OrderStatusID = Convert.ToInt32(ordRow[OrderHeaderTable.FLD_ORDER_STATUS_ID]);
                ShipGrpID = Convert.ToInt32(shipRow[ShipmentGroupTable.FLD_PKID]);

                //***CHECK FOR CONCURENTIAL MODIFICATION
                OrderHeaderTable ordVersion = ordDataAccess.SelectOne(OrderID);
                if (ordVersion.Rows.Count > 0)
                {
                    DataRow ordVersionRow = orderHeader.Rows[0];
                    if (Convert.ToDateTime(ordVersionRow[OrderHeaderTable.FLD_UPDATE_DATE]) != Convert.ToDateTime(ordRow[OrderHeaderTable.FLD_UPDATE_DATE]))
                    {
                        messageManager.HeaderText = "System Error";
                        messageManager.ValidationExceptionType = QSPFormExceptionType.RecordIsDeleted;
                        messageManager.SetErrorMessage(messageManager.FormatErrorMessage(QSPFormMessage.CONCURENT_RECORD_MODIFIED, "Order"));

                        throw new QSPFormValidationException(messageManager);
                    }
                }
                else
                {
                    messageManager.HeaderText = "System Error";
                    messageManager.ValidationExceptionType = QSPFormExceptionType.RecordIsDeleted;
                    messageManager.SetErrorMessage(messageManager.FormatErrorMessage(QSPFormMessage.CONCURENT_RECORD_DELETED, "Order"));

                    throw new QSPFormValidationException(messageManager);
                }

                //***CHECK IF IT'S RELEASE IN AS400
                DateTime dateStartProcess = DateTime.Now;
                System.Diagnostics.Debug.WriteLine("Time Begin Check in AS400 " + dateStartProcess.ToLongTimeString());

                if (!ordRow.IsNull(dataDef.FLD_FULF_ORDER_ID) && (OrderStatusID != Common.OrderStatus.ERROR_WAITING_ROLLBACK))
                {
                    int FulfOrderID = Convert.ToInt32(ordRow[dataDef.FLD_FULF_ORDER_ID]);
                    int AS400OrderStatusID = GetFulfOrderStatus(FulfOrderID);

                    if (AS400OrderStatusID >= OrderStatus.RELEASED)
                    {
                        messageManager.HeaderText = "System Error";
                        messageManager.ValidationExceptionType = QSPFormExceptionType.RecordIsModified;
                        messageManager.SetErrorMessage("The modification cannot be made.  The Order has been released.");

                        throw new QSPFormValidationException(messageManager);
                    }
                }
                DateTime dateEndProcess = DateTime.Now;
                System.Diagnostics.Debug.WriteLine("Time End Check in AS400 " + dateEndProcess.ToLongTimeString());
                TimeSpan NbMsec = dateEndProcess - dateStartProcess;
                System.Diagnostics.Debug.WriteLine("Time Duration Check in AS400 " + NbMsec.TotalMilliseconds.ToString());

                //Manage the Shipping Address
                //We have to delete the shipping Address if 
                //this pick up at warehouse
                if (!shipRow.IsNull(ShipmentGroupTable.FLD_DELIVERY_METHOD_ID))
                {
                    int deliveryMethodID = Convert.ToInt32(shipRow[ShipmentGroupTable.FLD_DELIVERY_METHOD_ID]);
                    if (deliveryMethodID != DeliveryMethod.PICK_UP_AT_WAREHOUSE)
                    {
                        //We have to delete the previous info on warehouse if exist.
                        if (!shipRow.IsNull(ShipmentGroupTable.FLD_DELIVERY_WAREHOUSE_ID))
                            shipRow[ShipmentGroupTable.FLD_DELIVERY_WAREHOUSE_ID] = DBNull.Value;
                    }
                }
                //We have to begin with Postal address, phone Number...
                if (dts.OrderPostalAddress.GetChanges() != null)
                {
                    postDataAccess.UpdateBatch(dts.OrderPostalAddress);
                    HasChanges = true;
                }
                if (dts.OrderPhoneNumber.GetChanges() != null)
                {
                    phoneDataAccess.UpdateBatch(dts.OrderPhoneNumber);
                    HasChanges = true;
                }
                if (dts.OrderEmailAddress.GetChanges() != null)
                {
                    emailDataAccess.UpdateBatch(dts.OrderEmailAddress);
                    HasChanges = true;
                }

                //Reassign All Postal Address, Phone Number and Email Address
                ReassignBillingInformation(dts.OrderHeader,
                    dts.OrderPostalAddress,
                    dts.OrderPhoneNumber,
                    dts.OrderEmailAddress);

                //Reassign All Postal Address, Phone Number and Email Address
                ReassignShippingInformation(dts.ShipmentGroup,
                    dts.OrderPostalAddress,
                    dts.OrderPhoneNumber,
                    dts.OrderEmailAddress);

                //Submit information for ShipmentGroup
                if (dts.ShipmentGroup.GetChanges() != null)
                {
                    shipDataAccess.UpdateBatch(dts.ShipmentGroup);
                    HasChanges = true;
                }

                //----------------------------------------------------//
                //					ORDER DETAIL					  //
                //----------------------------------------------------//
                //Rematch Order Detail for OrderID and ShipmentGroupID
                //Is the same for all
                OrderID = Convert.ToInt32(ordRow[OrderHeaderTable.FLD_PKID]);

                //Remove detail with quantity 0
                dts.OrderDetail.CleanData();
                //Reaffect ID
                PrepareTransactionWithNewID(dts.OrderHeader,
                    dts.ShipmentGroup,
                    dts.OrderDetail,
                    dts.OrderException);

                //Submit information for Order Detail
                if (dts.OrderDetail.GetChanges() != null)
                {
                    ordDetailDataAccess.UpdateBatch(dts.OrderDetail);
                    //Do that only if there is an update
                    //Tax Calculation

                    HasChanges = true;
                }
                if (dts.OrderDetailTax.GetChanges() != null)
                {
                    dts.OrderDetailTax.CleanData();
                    foreach (DataRow detRow in dts.OrderDetail)
                    {
                        if (detRow.RowState != DataRowState.Deleted)
                        {
                            int OldID = Convert.ToInt32(detRow[OrderDetailTable.FLD_PKID, DataRowVersion.Original]);
                            int NewID = Convert.ToInt32(detRow[OrderDetailTable.FLD_PKID, DataRowVersion.Current]);
                            if (OldID != NewID)
                            {
                                foreach (DataRow detTaxRow in dts.OrderDetailTax)
                                {
                                    if (detTaxRow.RowState != DataRowState.Deleted)
                                    {
                                        if (detTaxRow[OrderDetailTaxTable.FLD_ORDER_DETAIL_ID].ToString() == OldID.ToString())
                                        {
                                            detTaxRow[OrderDetailTaxTable.FLD_ORDER_DETAIL_ID] = NewID;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    ordTaxDataAccess.UpdateBatch(dts.OrderDetailTax);
                    //ordTaxDataAccess.CalculateWorder_idLogic(OrderID);
                }

                dts.OrderSupply.CleanData();
                //ToDo
                if (ordRow.IsNull(OrderHeaderTable.FLD_SUPPLY_ORDER_ID))
                {
                    if (dts.OrderSupply.TotalQuantity > 0)
                    {
                        InsertSupplyOrder(dts, connProvider);
                        HasChanges = true;
                    }
                }
                else
                {
                    UpdateSupplyOrder(dts, connProvider);
                    HasChanges = true;
                }

                //******SAVING THE GLOBAL UPDATE DATE IN ORDER HEADER TABLE
                //If no change have been made in the main table
                //but in at least one sattelite table
                //a change have been made, we have to update the main table
                ordRow[OrderHeaderTable.FLD_UPDATE_USER_ID] = UserID;
                ordDataAccess.Update(dts.OrderHeader);

                //*****REFRESH *************************
                //Resubmit for Task and Exception
                Refresh(dts, UserID, DataOperation.UPDATE, connProvider);

                //Register the update in the order_status_change table.
                OrderStatusChangeTable dTblChange = new OrderStatusChangeTable();
                DataRow newChangerRow = dTblChange.NewRow();

                newChangerRow[OrderStatusChangeTable.FLD_ORDER_ID] = ordRow[dataDef.FLD_PKID];
                newChangerRow[OrderStatusChangeTable.FLD_ORDER_STATUS_ID] = ordRow[dataDef.FLD_ORDER_STATUS_ID];
                newChangerRow[OrderStatusChangeTable.FLD_STATUS_CHANGE_REASON] = "Update from Order Express";
                newChangerRow[OrderStatusChangeTable.FLD_CREATE_USER_ID] = UserID;
                dTblChange.Rows.Add(newChangerRow);
                //Execute the chnage to DB
                ordStatusDataAccess.Insert(dTblChange);

                //Commit transaction 
                connProvider.CommitTransaction();
                dtsOrderData.Merge(dts, false);
                dtsOrderData.AcceptChanges();
                IsSuccess = true;

            }
            catch (Exception ex)
            {
                if (connProvider != null)
                {
                    if (connProvider.DBConnection.State != ConnectionState.Closed)
                        connProvider.RollbackTransaction(TransactionName);
                }
                IsSuccess = false;
                throw ex;
            }
            finally
            {
                if (connProvider != null)
                {
                    if (connProvider.DBConnection.State != ConnectionState.Closed)
                        connProvider.CloseConnection(false);
                }
            }

            return IsSuccess;
        }

        private void ReassignBillingInformation(OrderHeaderTable dTblOrder,
                                                PostalAddressEntityTable dTblAddress,
                                                PhoneNumberEntityTable dTblPhone,
                                                EmailEntityTable dTblEmail)
        {
            DataRow ordRow = dTblOrder.Rows[0];
            int OrderID = Convert.ToInt32(ordRow[dataDef.FLD_PKID]);
            DataRow rowToFind;

            //--------------------------------------//
            //Rematch with Order Header -- Billing
            //-------------------------------------//

            //Billing Address
            PostalAddressSystem addrSys = new PostalAddressSystem();

            rowToFind = addrSys.FindRow(dTblAddress, EntityType.TYPE_ORDER_BILLING,
                OrderID, PostalAddressType.TYPE_BILLING);
            if (rowToFind != null)
            {
                ordRow[OrderHeaderTable.FLD_BILLING_POSTAL_ADDRESS_ID] = rowToFind[PostalAddressEntityTable.FLD_PKID];
            }

            PhoneNumberSystem phoneSys = new PhoneNumberSystem();

            //Billing Telephone
            rowToFind = phoneSys.FindRow(dTblPhone, EntityType.TYPE_ORDER_BILLING,
                OrderID, PhoneNumberType.TYPE_BILLING_PHONE);
            if (rowToFind != null)
            {
                ordRow[OrderHeaderTable.FLD_BILLING_PHONE_NUMBER_ID] = rowToFind[PhoneNumberEntityTable.FLD_PKID];
            }

            //Billing Fax
            rowToFind = phoneSys.FindRow(dTblPhone, EntityType.TYPE_ORDER_BILLING,
                OrderID, PhoneNumberType.TYPE_BILLING_FAX);
            if (rowToFind != null)
            {
                ordRow[OrderHeaderTable.FLD_BILLING_FAX_NUMBER_ID] = rowToFind[PhoneNumberEntityTable.FLD_PKID];
            }

            EmailAddressSystem emailSys = new EmailAddressSystem();

            //Billing  Email Address
            rowToFind = emailSys.FindRow(dTblEmail, EntityType.TYPE_ORDER_BILLING,
                OrderID, EmailType.TYPE_BILLING);
            if (rowToFind != null)
            {
                ordRow[OrderHeaderTable.FLD_BILLING_EMAIL_ADDRESS_ID] = rowToFind[EmailEntityTable.FLD_PKID];
            }
        }
        private void ReassignShippingInformation(ShipmentGroupTable dTblShip,
                                                PostalAddressEntityTable dTblAddress,
                                                PhoneNumberEntityTable dTblPhone,
                                                EmailEntityTable dTblEmail)
        {
            DataRow rowToFind;
            //Shipping Address
            DataRow shipRow = dTblShip.Rows[0];
            int ShipGrpID = Convert.ToInt32(shipRow[ShipmentGroupTable.FLD_PKID]);
            //------------------------------------------//
            //Rematch in the Shipment Group -- Shipping
            //------------------------------------------//

            PostalAddressSystem addrSys = new PostalAddressSystem();

            rowToFind = addrSys.FindRow(dTblAddress, EntityType.TYPE_ORDER_SHIPPING,
                ShipGrpID, PostalAddressType.TYPE_SHIPPING);
            if (rowToFind != null)
            {
                shipRow[ShipmentGroupTable.FLD_SHIPPING_POSTAL_ADDRESS_ID] = rowToFind[PostalAddressEntityTable.FLD_PKID];
            }

            PhoneNumberSystem phoneSys = new PhoneNumberSystem();

            //Shipping Telephone
            rowToFind = phoneSys.FindRow(dTblPhone, EntityType.TYPE_ORDER_SHIPPING,
                ShipGrpID, PhoneNumberType.TYPE_SHIPPING_PHONE);
            if (rowToFind != null)
            {
                shipRow[ShipmentGroupTable.FLD_SHIPPING_PHONE_NUMBER_ID] = rowToFind[PhoneNumberEntityTable.FLD_PKID];
            }

            //Shipping Fax
            rowToFind = phoneSys.FindRow(dTblPhone, EntityType.TYPE_ORDER_SHIPPING,
                ShipGrpID, PhoneNumberType.TYPE_SHIPPING_FAX);
            if (rowToFind != null)
            {
                shipRow[ShipmentGroupTable.FLD_SHIPPING_FAX_NUMBER_ID] = rowToFind[PhoneNumberEntityTable.FLD_PKID];
            }

            EmailAddressSystem emailSys = new EmailAddressSystem();

            //Shipping  Email Address
            rowToFind = emailSys.FindRow(dTblEmail, EntityType.TYPE_ORDER_SHIPPING,
                ShipGrpID, EmailType.TYPE_SHIPPING);
            if (rowToFind != null)
            {
                shipRow[ShipmentGroupTable.FLD_SHIPPING_EMAIL_ADDRESS_ID] = rowToFind[EmailEntityTable.FLD_PKID];
            }

        }
        private void PrepareTransactionWithNewID(OrderHeaderTable dTblOrder,
                                                 ShipmentGroupTable dTblShip,
                                                 OrderDetailTable dTblOrderDetail,
                                                 EntityExceptionTable dTblException)
        {
            DataRow ordRow = dTblOrder.Rows[0];
            int OrderID = Convert.ToInt32(ordRow[dataDef.FLD_PKID]);
            int ShipID = Convert.ToInt32(dTblShip.Rows[0][ShipmentGroupTable.FLD_PKID]);
            CommonSystem comSys = new CommonSystem();

            if (dTblOrderDetail.Rows.Count > 0)
            {
                foreach (DataRow row in dTblOrderDetail.Rows)
                {
                    if (row.RowState != DataRowState.Deleted)
                    {
                        comSys.UpdateRow(row, OrderDetailTable.FLD_SHIPMENT_GROUP_ID, ShipID.ToString());
                        comSys.UpdateRow(row, OrderDetailTable.FLD_ORDER_ID, OrderID.ToString());
                        if (row.RowState == DataRowState.Added)
                        {
                            row[OrderDetailTable.FLD_SOURCE_ID] = ordRow[OrderHeaderTable.FLD_SOURCE_ID];
                            row[OrderDetailTable.FLD_ORDER_STATUS_ID] = ordRow[OrderHeaderTable.FLD_ORDER_STATUS_ID];
                            row[OrderDetailTable.FLD_STATUS_REASON_ID] = ordRow[OrderHeaderTable.FLD_STATUS_REASON_ID];
                        }
                    }
                }
            }
            if (dTblException != null)
            {
                if (dTblException.Rows.Count > 0)
                {
                    foreach (DataRow ordExcRow in dTblException.Rows)
                    {
                        if (ordExcRow.RowState == DataRowState.Added)
                        {
                            ordExcRow[EntityExceptionTable.FLD_ENTITY_ID] = OrderID;
                            ordExcRow[EntityExceptionTable.FLD_ENTITY_TYPE_ID] = EntityType.TYPE_ORDER_BILLING;
                        }
                    }

                }
            }
        }

        public OrderData InitializeOrder(int UserID, String FMID, int FormID, int CampaignID)
        {
            //We prepare the DataSet for all step
            //Add a new Row
            OrderData dtsOrderData = new OrderData();

            //Create a new Order Group row a start
            OrderGroupTable orderGroup = dtsOrderData.OrderGroup;
            DataRow rowGrp;
            rowGrp = orderGroup.NewRow();
            rowGrp[OrderGroupTable.FLD_CREATE_USER_ID] = UserID;
            orderGroup.Rows.Add(rowGrp);

            //Create a new Order row a start
            OrderHeaderTable orderHeader = dtsOrderData.OrderHeader;
            DataRow row;
            row = orderHeader.NewRow();
            row[OrderHeaderTable.FLD_ORDER_GROUP_ID] = rowGrp[OrderGroupTable.FLD_PKID];
            row[OrderHeaderTable.FLD_FM_ID] = FMID;
            row[OrderHeaderTable.FLD_FORM_ID] = FormID;
            row[OrderHeaderTable.FLD_ORDER_STATUS_ID] = OrderStatus.IN_PROCESS;
            row[OrderHeaderTable.FLD_CAMPAIGN_ID] = CampaignID;
            row[OrderHeaderTable.FLD_SOURCE_ID] = 1; //Internal
            row[OrderHeaderTable.FLD_CREATE_USER_ID] = UserID;
            row[OrderHeaderTable.FLD_ORDER_DATE] = DateTime.Now;
            orderHeader.Rows.Add(row);

            //Create One Row for the Sipment Group
            ShipmentGroupTable dTblShipmentGroup = dtsOrderData.ShipmentGroup;
            DataRow rowShipGrp = dTblShipmentGroup.NewRow();
            rowShipGrp[ShipmentGroupTable.FLD_CREATE_USER_ID] = UserID;
            rowShipGrp[ShipmentGroupTable.FLD_SHIP_SUPPLY_ID] = -1;

            #region Set warehouse

            //Get the warehouse information
            CampaignTable dTblCamp = new CampaignTable();
            CampaignSystem campSys = new CampaignSystem();
            dTblCamp = campSys.SelectOne(CampaignID);
            if (dTblCamp.Rows.Count > 0)
            {
                DataRow rowCamp = dTblCamp.Rows[0];
                rowShipGrp[ShipmentGroupTable.FLD_DELIVERY_WAREHOUSE_ID] = rowCamp[CampaignTable.FLD_WAREHOUSE_ID];
                rowShipGrp[ShipmentGroupTable.FLD_DELIVERY_WAREHOUSE_NAME] = rowCamp[CampaignTable.FLD_WAREHOUSE_NAME];
            }

            // Apply custom hard-coded default warehouse logic for Otis and Pine Valley forms.
            // Normally, default warehouse is set through the campaign but Otis and Pine Valley both belong to the same campaign.
            FormSystem formSys = new FormSystem();
            if (formSys.IsOtisForm(FormID))
            {
                QSP.Business.Fulfillment.Form form = QSP.Business.Fulfillment.Form.GetForm(FormID);
                QSP.Business.Fulfillment.Warehouse warehouse = QSP.Business.Fulfillment.Warehouse.GetWarehouse(Convert.ToInt32(form.DefaultWarehouseId));

                rowShipGrp[ShipmentGroupTable.FLD_DELIVERY_WAREHOUSE_ID] = warehouse.WarehouseId;
                rowShipGrp[ShipmentGroupTable.FLD_DELIVERY_WAREHOUSE_NAME] = warehouse.WarehouseName;
            }
            if (formSys.IsPineValleyForm(FormID))
            {
                QSP.Business.Fulfillment.Form form = QSP.Business.Fulfillment.Form.GetForm(FormID);
                QSP.Business.Fulfillment.Warehouse warehouse = QSP.Business.Fulfillment.Warehouse.GetWarehouse(Convert.ToInt32(form.DefaultWarehouseId));

                rowShipGrp[ShipmentGroupTable.FLD_DELIVERY_WAREHOUSE_ID] = warehouse.WarehouseId;
                rowShipGrp[ShipmentGroupTable.FLD_DELIVERY_WAREHOUSE_NAME] = warehouse.WarehouseName;
            }

            #endregion

            dTblShipmentGroup.Rows.Add(rowShipGrp);

            //Create One row for Validation
            ValidationTable dTblOrderVal = dtsOrderData.OrderValidation;
            if (dTblOrderVal.Rows.Count == 0)
            {
                dTblOrderVal.Rows.Add(dTblOrderVal.NewRow());
            }

            return dtsOrderData;

        }
        public void SetDefaultFormProduct(OrderData dtsOrderData, int UserID, int QCAPOrderID, bool IncludeAllFormProduct)
        {
            int FormID = Convert.ToInt32(dtsOrderData.OrderHeader.Rows[0][OrderHeaderTable.FLD_FORM_ID]);
            int OrderID = Convert.ToInt32(dtsOrderData.OrderHeader.Rows[0][OrderHeaderTable.FLD_PKID]);
            int ShipGrpID = Convert.ToInt32(dtsOrderData.ShipmentGroup.Rows[0][ShipmentGroupTable.FLD_PKID]);
            QSPForm.Business.CatalogItemDetailSystem catSys = new QSPForm.Business.CatalogItemDetailSystem();
            //Catalog Item Detail (Product and multiple price)			
            CatalogItemDetailTable dtblCatalogItemDetail = catSys.SelectAllByFormID(FormID);
            OrderDetailTable dtblOrderDetail = dtsOrderData.OrderDetail;
            List<LinqEntity.QCAPOrderDetail> qCAPOrderDetailList = null;

            if (QCAPOrderID != 0)
                qCAPOrderDetailList = GetQCAPOrderDetails(QCAPOrderID);


            if (dtblOrderDetail.Rows.Count == 0)
            {
                foreach (DataRow catRow in dtblCatalogItemDetail.Rows)
                {
                    //DataRow catRow = dtblCatalogItemDetail.Rows[0];
                    //Add a new Product as default					
                    DataRow row = dtblOrderDetail.NewRow();
                    row[OrderDetailTable.FLD_ORDER_ID] = OrderID;
                    row[OrderDetailTable.FLD_CATALOG_ITEM_DETAIL_ID] = catRow[CatalogItemDetailTable.FLD_PKID];
                    row[OrderDetailTable.FLD_CATALOG_ITEM_CODE] = catRow[CatalogItemDetailTable.FLD_CATALOG_ITEM_CODE];
                    row[OrderDetailTable.FLD_CATALOG_ITEM_NAME] = catRow[CatalogItemDetailTable.FLD_CATALOG_ITEM_NAME];
                    row[OrderDetailTable.FLD_CATALOG_ITEM_NB_UNITS] = catRow[CatalogItemDetailTable.FLD_CATALOG_ITEM_NB_UNITS];
                    row[OrderDetailTable.FLD_CATALOG_ITEM_DESC] = catRow[CatalogItemDetailTable.FLD_CATALOG_ITEM_DESC];
                    row[OrderDetailTable.FLD_CATALOG_ITEM_DETAIL_PRICE] = catRow[CatalogItemDetailTable.FLD_PRICE];
                    row[OrderDetailTable.FLD_CATALOG_ITEM_DETAIL_PROFIT_RATE] = catRow[CatalogItemDetailTable.FLD_PROFIT_RATE];
                    row[OrderDetailTable.FLD_ADJUSTMENT_QUANTITY] = 0;
                    row[OrderDetailTable.FLD_SHIPMENT_GROUP_ID] = ShipGrpID;
                    row[OrderDetailTable.FLD_PRICE] = catRow[CatalogItemDetailTable.FLD_PRICE];
                    row[OrderDetailTable.FLD_DISPLAY_ORDER] = catRow[CatalogItemDetailTable.FLD_DISPLAY_ORDER];
                    row[OrderDetailTable.FLD_NB_DAY_LEAD_TIME] = catRow[CatalogItemDetailTable.FLD_NB_DAY_LEAD_TIME];
                    row[OrderDetailTable.FLD_PROFIT_RATE] = catRow[CatalogItemDetailTable.FLD_PROFIT_RATE];
                    row[OrderDetailTable.FLD_FORM_SECTION_NUMBER] = catRow[CatalogItemDetailTable.FLD_FORM_SECTION_NUMBER];
                    row[OrderDetailTable.FLD_FORM_SECTION_TYPE_ID] = catRow[CatalogItemDetailTable.FLD_FORM_SECTION_TYPE_ID];

                    row[OrderDetailTable.FLD_CREATE_USER_ID] = UserID;

                    int quantity = 0;
                    if (QCAPOrderID != 0 && qCAPOrderDetailList != null && qCAPOrderDetailList.Count > 0)
                    {
                        int catalogItemDetailID = int.Parse(catRow[CatalogItemDetailTable.FLD_PKID].ToString());

                        LinqEntity.QCAPOrderDetail result = (from o in qCAPOrderDetailList
                                                             where o.catalog_item_detail_id == catalogItemDetailID
                                                             select o).SingleOrDefault<LinqEntity.QCAPOrderDetail>();

                        if (result != null)
                            quantity = result.quantity;
                    }

                    row[OrderDetailTable.FLD_QUANTITY] = quantity;

                    dtblOrderDetail.Rows.Add(row);

                    //Stop after the first row
                    if (!IncludeAllFormProduct)
                        break;

                }
            }

        }

        #region Supply orders

        private bool InsertSupplyOrder(OrderData dts, Data.ConnectionProvider connProvider)
        {

            bool IsSuccess = true;
            try
            {

                Data.Order ordDataAccess = new Data.Order();
                Data.OrderDetail ordDetailDataAccess = new Data.OrderDetail();
                Data.Shipment_group shipDataAccess = new Data.Shipment_group();

                // Pass the created ConnectionProvider object to the data-access objects.
                ordDataAccess.MainConnectionProvider = connProvider;
                ordDetailDataAccess.MainConnectionProvider = connProvider;
                shipDataAccess.MainConnectionProvider = connProvider;


                DataRow ordRow = dts.OrderHeader.Rows[0];
                DataRow shipRow = dts.ShipmentGroup.Rows[0];

                OrderHeaderTable dtblOrderSupply = new OrderHeaderTable();
                DataRow suppRow = dtblOrderSupply.NewRow();

                //Copy information for the Order Header
                suppRow[OrderHeaderTable.FLD_FORM_ID] = ordRow[OrderHeaderTable.FLD_FORM_ID];
                suppRow[OrderHeaderTable.FLD_ORDER_GROUP_ID] = ordRow[OrderHeaderTable.FLD_ORDER_GROUP_ID];
                suppRow[OrderHeaderTable.FLD_CUSTOMER_ID] = ordRow[OrderHeaderTable.FLD_CUSTOMER_ID];
                suppRow[OrderHeaderTable.FLD_CAMPAIGN_ID] = ordRow[OrderHeaderTable.FLD_CAMPAIGN_ID];
                suppRow[OrderHeaderTable.FLD_ORDER_TYPE_ID] = 3;//Order of Supply
                suppRow[OrderHeaderTable.FLD_FM_ID] = ordRow[OrderHeaderTable.FLD_FM_ID];

                suppRow[OrderHeaderTable.FLD_SOURCE_ID] = ordRow[OrderHeaderTable.FLD_SOURCE_ID];
                suppRow[OrderHeaderTable.FLD_ORDER_STATUS_ID] = ordRow[OrderHeaderTable.FLD_ORDER_STATUS_ID];
                suppRow[OrderHeaderTable.FLD_STATUS_REASON_ID] = ordRow[OrderHeaderTable.FLD_STATUS_REASON_ID];
                suppRow[OrderHeaderTable.FLD_BILLING_POSTAL_ADDRESS_ID] = ordRow[OrderHeaderTable.FLD_BILLING_POSTAL_ADDRESS_ID];
                suppRow[OrderHeaderTable.FLD_BILLING_PHONE_NUMBER_ID] = ordRow[OrderHeaderTable.FLD_BILLING_PHONE_NUMBER_ID];
                suppRow[OrderHeaderTable.FLD_BILLING_FAX_NUMBER_ID] = ordRow[OrderHeaderTable.FLD_BILLING_FAX_NUMBER_ID];
                suppRow[OrderHeaderTable.FLD_BILLING_EMAIL_ADDRESS_ID] = ordRow[OrderHeaderTable.FLD_BILLING_EMAIL_ADDRESS_ID];
                suppRow[OrderHeaderTable.FLD_ORDER_DATE] = ordRow[OrderHeaderTable.FLD_ORDER_DATE];
                suppRow[OrderHeaderTable.FLD_CREATE_USER_ID] = ordRow[OrderHeaderTable.FLD_CREATE_USER_ID];

                dtblOrderSupply.Rows.Add(suppRow);

                //Submit information for Order Header
                ordDataAccess.UpdateBatch(dtblOrderSupply);
                //Set the new Order Id in the Standard Order header
                ordRow[OrderHeaderTable.FLD_SUPPLY_ORDER_ID] = suppRow[OrderHeaderTable.FLD_PKID];


                //Treatement of the Shipping Supply Information
                ShipmentGroupTable dtblShipmentGroup = new ShipmentGroupTable();
                DataRow suppShipRow = dtblShipmentGroup.NewRow();
                int ShipGrpId = -1;
                //Copy all data from the Other to the New One
                suppShipRow[ShipmentGroupTable.FLD_DELIVERY_METHOD_ID] = shipRow[ShipmentGroupTable.FLD_DELIVERY_METHOD_ID];
                suppShipRow[ShipmentGroupTable.FLD_REQUESTED_DELIVERY_DATE] = shipRow[ShipmentGroupTable.FLD_SUPPLY_REQUESTED_DELIVERY_DATE];
                suppShipRow[ShipmentGroupTable.FLD_DELIVERY_NLT] = shipRow[ShipmentGroupTable.FLD_SUPPLY_DELIVERY_NLT];
                suppShipRow[ShipmentGroupTable.FLD_SHIP_SUPPLY_TO] = shipRow[ShipmentGroupTable.FLD_SHIP_SUPPLY_TO];

                int ShipTo = Convert.ToInt32(shipRow[ShipmentGroupTable.FLD_SHIP_SUPPLY_TO]);
                suppShipRow[ShipmentGroupTable.FLD_CREATE_USER_ID] = shipRow[ShipmentGroupTable.FLD_CREATE_USER_ID];
                suppShipRow[ShipmentGroupTable.FLD_PKID] = ShipGrpId;
                //Add new Row and insert in DB
                dtblShipmentGroup.Rows.Add(suppShipRow);

                if (ShipTo == 2) //Same Address than previously
                {
                    suppShipRow[ShipmentGroupTable.FLD_DELIVERY_WAREHOUSE_ID] = shipRow[ShipmentGroupTable.FLD_DELIVERY_WAREHOUSE_ID];
                    suppShipRow[ShipmentGroupTable.FLD_SHIPPING_POSTAL_ADDRESS_ID] = shipRow[ShipmentGroupTable.FLD_SHIPPING_POSTAL_ADDRESS_ID];
                    suppShipRow[ShipmentGroupTable.FLD_SHIPPING_PHONE_NUMBER_ID] = shipRow[ShipmentGroupTable.FLD_SHIPPING_PHONE_NUMBER_ID];
                    suppShipRow[ShipmentGroupTable.FLD_SHIPPING_FAX_NUMBER_ID] = shipRow[ShipmentGroupTable.FLD_SHIPPING_FAX_NUMBER_ID];
                    suppShipRow[ShipmentGroupTable.FLD_SHIPPING_EMAIL_ADDRESS_ID] = shipRow[ShipmentGroupTable.FLD_SHIPPING_EMAIL_ADDRESS_ID];
                }
                else  // Ship to a New Address OR Copy FSM Address
                {
                    suppShipRow[ShipmentGroupTable.FLD_DELIVERY_WAREHOUSE_ID] = DBNull.Value;
                    //------------------------------------------//
                    //Rematch in a new Shipment Group -- Shipping
                    //------------------------------------------//
                    //For an insert we have put -1 as default
                    //Reassign All Postal Address, Phone Number and Email Address
                    ReassignShippingInformation(dtblShipmentGroup,
                        dts.OrderPostalAddress,
                        dts.OrderPhoneNumber,
                        dts.OrderEmailAddress);

                }

                shipDataAccess.UpdateBatch(dtblShipmentGroup);

                //Redo the reference
                int OrderID = Convert.ToInt32(suppRow[OrderHeaderTable.FLD_PKID]);
                ShipGrpId = Convert.ToInt32(suppShipRow[ShipmentGroupTable.FLD_PKID]);

                PrepareTransactionWithNewID(dtblOrderSupply,
                    dtblShipmentGroup,
                    dts.OrderSupply,
                    null);

                //Submit information for Order Supply
                ordDetailDataAccess.UpdateBatch(dts.OrderSupply);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return IsSuccess;
        }
        private bool UpdateSupplyOrder(OrderData dts, Data.ConnectionProvider connProvider)
        {
            bool IsSuccess = true;

            Data.Order ordDataAccess = new Data.Order();
            Data.OrderDetail ordDetailDataAccess = new Data.OrderDetail();
            Data.Shipment_group shipDataAccess = new Data.Shipment_group();

            // Pass the created ConnectionProvider object to the data-access objects.
            ordDataAccess.MainConnectionProvider = connProvider;
            ordDetailDataAccess.MainConnectionProvider = connProvider;
            shipDataAccess.MainConnectionProvider = connProvider;

            CommonSystem comSys = new CommonSystem();
            DataRow ordRow = dts.OrderHeader.Rows[0];
            DataRow shipRow = dts.ShipmentGroup.Rows[0];

            int SupplyOrderID = Convert.ToInt32(ordRow[OrderHeaderTable.FLD_SUPPLY_ORDER_ID]);
            int SupplyShipGrpID = Convert.ToInt32(shipRow[ShipmentGroupTable.FLD_SHIP_SUPPLY_ID]);
            OrderHeaderTable dtblOrderHeaderSupply = SelectOne(SupplyOrderID);

            ShipmentGroupSystem shipSys = new ShipmentGroupSystem();
            ShipmentGroupTable dtblShipGrpSupply = shipSys.SelectOne(SupplyShipGrpID);

            if (dtblShipGrpSupply.Rows.Count == 0)
            {
                //Insert a new row for Shipment Group of the Supply Order
                dtblShipGrpSupply = new ShipmentGroupTable();
                DataRow suppShipRow = dtblShipGrpSupply.NewRow();
                int ShipGrpId = -1;
                suppShipRow[ShipmentGroupTable.FLD_CREATE_USER_ID] = 0;
                suppShipRow[ShipmentGroupTable.FLD_PKID] = ShipGrpId;
                //Add new Row and insert in DB
                dtblShipGrpSupply.Rows.Add(suppShipRow);
            }

            DataRow ordSuppRow = dtblOrderHeaderSupply.Rows[0];
            DataRow shipSuppRow = dtblShipGrpSupply.Rows[0];

            //Verify that at least one Supply remains
            DataView dvSupply = new DataView(dts.OrderSupply);
            dvSupply.RowStateFilter = DataViewRowState.Deleted;
            //Check if all have been deleted
            if (dvSupply.Count == dts.OrderSupply.Rows.Count)
            {
                //Delete All Supply and the Order behind
                ordSuppRow[OrderHeaderTable.FLD_UPDATE_USER_ID] = 0;
                ordSuppRow.Delete();
                IsSuccess = Delete(dtblOrderHeaderSupply);

                shipSuppRow[OrderHeaderTable.FLD_UPDATE_USER_ID] = 0;
                shipSuppRow.Delete();
                shipDataAccess.Delete(dtblShipGrpSupply);

            }
            else
            {
                comSys.UpdateRow(ordSuppRow, OrderHeaderTable.FLD_FM_ID, ordRow);
                comSys.UpdateRow(ordSuppRow, OrderHeaderTable.FLD_ORDER_STATUS_ID, ordRow);
                comSys.UpdateRow(ordSuppRow, OrderHeaderTable.FLD_BILLING_POSTAL_ADDRESS_ID, ordRow);
                comSys.UpdateRow(ordSuppRow, OrderHeaderTable.FLD_BILLING_PHONE_NUMBER_ID, ordRow);
                comSys.UpdateRow(ordSuppRow, OrderHeaderTable.FLD_BILLING_FAX_NUMBER_ID, ordRow);
                comSys.UpdateRow(ordSuppRow, OrderHeaderTable.FLD_BILLING_EMAIL_ADDRESS_ID, ordRow);
                if (ordSuppRow.RowState != DataRowState.Unchanged)
                    comSys.UpdateRow(ordSuppRow, OrderHeaderTable.FLD_UPDATE_USER_ID, ordRow);


                int OldShipTo = 0;
                if (!shipRow.IsNull(ShipmentGroupTable.FLD_SHIP_SUPPLY_TO))
                    OldShipTo = Convert.ToInt32(shipRow[ShipmentGroupTable.FLD_SHIP_SUPPLY_TO]);

                //Update the Shipment Group Supply Row
                comSys.UpdateRow(shipSuppRow, ShipmentGroupTable.FLD_DELIVERY_WAREHOUSE_ID, shipRow);
                comSys.UpdateRow(shipSuppRow, ShipmentGroupTable.FLD_DELIVERY_METHOD_ID, shipRow);
                comSys.UpdateRow(shipSuppRow, ShipmentGroupTable.FLD_REQUESTED_DELIVERY_DATE, shipRow[ShipmentGroupTable.FLD_SUPPLY_REQUESTED_DELIVERY_DATE].ToString());
                comSys.UpdateRow(shipSuppRow, ShipmentGroupTable.FLD_DELIVERY_NLT, shipRow[ShipmentGroupTable.FLD_SUPPLY_DELIVERY_NLT].ToString());
                comSys.UpdateRow(shipSuppRow, ShipmentGroupTable.FLD_SHIP_SUPPLY_TO, shipRow);

                int ShipTo = 0;
                if (!shipRow.IsNull(ShipmentGroupTable.FLD_SHIP_SUPPLY_TO))
                    ShipTo = Convert.ToInt32(shipRow[ShipmentGroupTable.FLD_SHIP_SUPPLY_TO]);
                bool IsSame_ShipTo = (ShipTo == OldShipTo);

                //Update the information on the address to ship the supply

                if (ShipTo == 2) //Same Address than previously
                {
                    comSys.UpdateRow(shipSuppRow, ShipmentGroupTable.FLD_DELIVERY_WAREHOUSE_ID, shipRow);
                    comSys.UpdateRow(shipSuppRow, ShipmentGroupTable.FLD_SHIPPING_POSTAL_ADDRESS_ID, shipRow);
                    comSys.UpdateRow(shipSuppRow, ShipmentGroupTable.FLD_SHIPPING_PHONE_NUMBER_ID, shipRow);
                    comSys.UpdateRow(shipSuppRow, ShipmentGroupTable.FLD_SHIPPING_FAX_NUMBER_ID, shipRow);
                    comSys.UpdateRow(shipSuppRow, ShipmentGroupTable.FLD_SHIPPING_EMAIL_ADDRESS_ID, shipRow);
                }
                else  // Ship to a New Address OR Copy FSM Address
                {
                    //------------------------------------------//
                    //Rematch in a new Shipment Group -- Shipping
                    //------------------------------------------//
                    if (!shipSuppRow.IsNull(ShipmentGroupTable.FLD_DELIVERY_WAREHOUSE_ID))
                        shipSuppRow[ShipmentGroupTable.FLD_DELIVERY_WAREHOUSE_ID] = DBNull.Value;
                    //Reassign All Postal Address, Phone Number and Email Address
                    ReassignShippingInformation(dtblShipGrpSupply,
                        dts.OrderPostalAddress,
                        dts.OrderPhoneNumber,
                        dts.OrderEmailAddress);

                }

                //Submit information for Supply Order Header
                if (dtblOrderHeaderSupply.GetChanges() != null)
                {
                    ordDataAccess.UpdateBatch(dtblOrderHeaderSupply);
                }

                //Submit information for Supply Shipment Group
                if (dtblShipGrpSupply.GetChanges() != null)
                {
                    shipDataAccess.UpdateBatch(dtblShipGrpSupply);
                }

                //Proceed to the change of the Supply Order Detail
                PrepareTransactionWithNewID(dtblOrderHeaderSupply,
                    dtblShipGrpSupply,
                    dts.OrderSupply,
                    null);

                //Submit information for Order Supply
                if (dts.OrderSupply.GetChanges() != null)
                {
                    ordDetailDataAccess.UpdateBatch(dts.OrderSupply);
                }

            }

            return IsSuccess;
        }
        public void SetDefaultFormSupply(OrderData dtsOrderData, int UserID, bool IncludeAllFormProduct)
        {
            int FormID = Convert.ToInt32(dtsOrderData.OrderHeader.Rows[0][OrderHeaderTable.FLD_FORM_ID]);
            int OrderID = Convert.ToInt32(dtsOrderData.OrderHeader.Rows[0][OrderHeaderTable.FLD_PKID]);
            int ShipmentGroupID = Convert.ToInt32(dtsOrderData.ShipmentGroup.Rows[0][ShipmentGroupTable.FLD_SHIP_SUPPLY_ID]);
            SetDefaultFormSupply(dtsOrderData.OrderSupply, OrderID, ShipmentGroupID, FormID, UserID, IncludeAllFormProduct);
        }
        public void SetDefaultFormSupply(OrderDetailTable dtblOrderSupply, int OrderID, int ShipmentGroupID, int FormID, int UserID, bool IncludeAllFormProduct)
        {
            QSPForm.Business.CatalogItemDetailSystem catSys = new QSPForm.Business.CatalogItemDetailSystem();
            //Catalog Item Detail (Product and multiple price)			
            CatalogItemDetailTable dtblCatalogItemDetail = catSys.SelectAllSupplyByFormID(FormID);

            int DefaultSupplyQty = 0;

            dtblOrderSupply.Columns[OrderDetailTable.FLD_QUANTITY].DefaultValue = DefaultSupplyQty;
            if (dtblOrderSupply.Rows.Count == 0)
            {
                foreach (DataRow catRow in dtblCatalogItemDetail.Rows)
                {
                    //Add a new Shipping Address as default
                    //int AddrNewID = dtblAddress.Rows.Count;
                    //DataRow catRow = dtblCatalogItemDetail.Rows[0];

                    DataRow row = dtblOrderSupply.NewRow();
                    row[OrderDetailTable.FLD_ORDER_ID] = OrderID;
                    row[OrderDetailTable.FLD_CATALOG_ITEM_DETAIL_ID] = catRow[CatalogItemDetailTable.FLD_PKID];
                    row[OrderDetailTable.FLD_CATALOG_ITEM_CODE] = catRow[CatalogItemDetailTable.FLD_CATALOG_ITEM_CODE];
                    row[OrderDetailTable.FLD_CATALOG_ITEM_NAME] = catRow[CatalogItemDetailTable.FLD_CATALOG_ITEM_NAME];
                    row[OrderDetailTable.FLD_CATALOG_ITEM_NB_UNITS] = catRow[CatalogItemDetailTable.FLD_CATALOG_ITEM_NB_UNITS];
                    row[OrderDetailTable.FLD_CATALOG_ITEM_DESC] = catRow[CatalogItemDetailTable.FLD_CATALOG_ITEM_DESC];
                    row[OrderDetailTable.FLD_CATALOG_ITEM_DETAIL_PRICE] = catRow[CatalogItemDetailTable.FLD_PRICE];
                    row[OrderDetailTable.FLD_CATALOG_ITEM_DETAIL_PROFIT_RATE] = catRow[CatalogItemDetailTable.FLD_PROFIT_RATE];
                    row[OrderDetailTable.FLD_QUANTITY] = DefaultSupplyQty;
                    row[OrderDetailTable.FLD_ADJUSTMENT_QUANTITY] = 0;
                    row[OrderDetailTable.FLD_SHIPMENT_GROUP_ID] = ShipmentGroupID;
                    row[OrderDetailTable.FLD_PRICE] = catRow[CatalogItemDetailTable.FLD_PRICE];
                    row[OrderDetailTable.FLD_DISPLAY_ORDER] = catRow[CatalogItemDetailTable.FLD_DISPLAY_ORDER];
                    row[OrderDetailTable.FLD_NB_DAY_LEAD_TIME] = catRow[CatalogItemDetailTable.FLD_NB_DAY_LEAD_TIME];
                    row[OrderDetailTable.FLD_PROFIT_RATE] = catRow[CatalogItemDetailTable.FLD_PROFIT_RATE];
                    row[OrderDetailTable.FLD_FORM_SECTION_NUMBER] = catRow[CatalogItemDetailTable.FLD_FORM_SECTION_NUMBER];
                    row[OrderDetailTable.FLD_FORM_SECTION_TYPE_ID] = catRow[CatalogItemDetailTable.FLD_FORM_SECTION_TYPE_ID];

                    row[OrderDetailTable.FLD_CREATE_USER_ID] = UserID;

                    dtblOrderSupply.Rows.Add(row);

                    //Stop after the first row
                    if (!IncludeAllFormProduct)
                        break;

                }
            }

        }
        public void SetDefaultShippingSupplyPostalAddress(OrderData dtsOrderData, int UserID)
        {
            //Copy the Address info from the Standard Order Shipping address			
            int ShipGrpID = Convert.ToInt32(dtsOrderData.ShipmentGroup.Rows[0][ShipmentGroupTable.FLD_PKID]);
            int ShipGrpSupplyID = -1;

            PostalAddressEntityTable dtblAddress = dtsOrderData.OrderPostalAddress;

            PostalAddressSystem postSys = new PostalAddressSystem();
            postSys.CopyToEntity(dtblAddress, UserID,
                EntityType.TYPE_ORDER_SHIPPING, ShipGrpID, PostalAddressType.TYPE_SHIPPING,
                EntityType.TYPE_ORDER_SHIPPING, ShipGrpSupplyID, PostalAddressType.TYPE_SHIPPING);

        }
        public void SetDefaultShippingSupplyPhoneNumber(OrderData dtsOrderData, int UserID)
        {
            PhoneNumberEntityTable dtblPhoneNumber = dtsOrderData.OrderPhoneNumber;
            ShipmentGroupTable dTblShipmentGroup = dtsOrderData.ShipmentGroup;
            DataRow rowShipGrp = dTblShipmentGroup.Rows[0];

            int ShipGrpSupplyID = -1;
            int ShipGrpID = Convert.ToInt32(rowShipGrp[ShipmentGroupTable.FLD_PKID]);

            //Phone Number
            PhoneNumberSystem phoneSys = new PhoneNumberSystem();
            phoneSys.CopyToEntity(dtblPhoneNumber, UserID,
                EntityType.TYPE_ORDER_SHIPPING, ShipGrpID, PhoneNumberType.TYPE_SHIPPING_PHONE,
                EntityType.TYPE_ORDER_SHIPPING, ShipGrpSupplyID, PhoneNumberType.TYPE_SHIPPING_PHONE);

            phoneSys.CopyToEntity(dtblPhoneNumber, UserID,
                EntityType.TYPE_ORDER_SHIPPING, ShipGrpID, PhoneNumberType.TYPE_SHIPPING_FAX,
                EntityType.TYPE_ORDER_SHIPPING, ShipGrpSupplyID, PhoneNumberType.TYPE_SHIPPING_FAX);

        }
        public void SetDefaultShippingSupplyEmailAddress(OrderData dtsOrderData, int UserID)
        {
            ShipmentGroupTable dTblShipmentGroup = dtsOrderData.ShipmentGroup;
            DataRow rowShipGrp = dTblShipmentGroup.Rows[0];

            int ShipGrpSupplyID = -1;
            int ShipGrpID = Convert.ToInt32(rowShipGrp[ShipmentGroupTable.FLD_PKID]);

            EmailEntityTable dTblEmailAddress = dtsOrderData.OrderEmailAddress;

            EmailAddressSystem emailSys = new EmailAddressSystem();
            emailSys.CopyToEntity(dTblEmailAddress, UserID,
                EntityType.TYPE_ORDER_SHIPPING, ShipGrpID, EmailType.TYPE_SHIPPING,
                EntityType.TYPE_ORDER_SHIPPING, ShipGrpSupplyID, EmailType.TYPE_SHIPPING);


        }
        public void SetFMShippingSupplyPostalAddress(OrderData dtsOrderData, int UserID)
        {
            //Copy the Address info from the Standard Order Shipping address			
            int ShipGrpSupplyID = -1;

            QSPForm.Business.CUserSystem fmSys = new CUserSystem();
            CUserTable cuser = fmSys.SelectOne(dtsOrderData.OrderHeader.Rows[0][OrderHeaderTable.FLD_FM_ID].ToString());

            PostalAddressEntityTable dtblAddress = dtsOrderData.OrderPostalAddress;
            DataView DVShippingSupply = new DataView(dtblAddress);

            DVShippingSupply.RowFilter = PostalAddressEntityTable.FLD_ENTITY_TYPE_ID + " = " + EntityType.TYPE_ORDER_SHIPPING.ToString()
                + " AND " + PostalAddressEntityTable.FLD_TYPE + " = " + PostalAddressType.TYPE_SHIPPING.ToString()
                + " AND " + PostalAddressEntityTable.FLD_ENTITY_ID + " = " + ShipGrpSupplyID.ToString();


            if (DVShippingSupply.Count == 0)
            {
                if (cuser.Rows.Count > 0)
                {
                    //Add a new Shipping Address as default
                    int AddrNewID = dtblAddress.Rows.Count;
                    DataRow row = dtblAddress.NewRow();

                    row[PostalAddressEntityTable.FLD_ADDRESS_ID] = AddrNewID;
                    row[PostalAddressEntityTable.FLD_ENTITY_ID] = ShipGrpSupplyID;
                    row[PostalAddressEntityTable.FLD_ENTITY_TYPE_ID] = EntityType.TYPE_ORDER_SHIPPING;
                    row[PostalAddressEntityTable.FLD_TYPE] = PostalAddressType.TYPE_SHIPPING; //Shipping
                    row[PostalAddressEntityTable.FLD_FIRST_NAME] = cuser.Rows[0][CUserTable.FLD_FIRST_NAME];
                    row[PostalAddressEntityTable.FLD_LAST_NAME] = cuser.Rows[0][CUserTable.FLD_LAST_NAME];
                    row[PostalAddressEntityTable.FLD_ADDRESS1] = cuser.Rows[0][CUserTable.FLD_ADDR1];
                    row[PostalAddressEntityTable.FLD_ADDRESS2] = cuser.Rows[0][CUserTable.FLD_ADDR2];
                    row[PostalAddressEntityTable.FLD_CITY] = cuser.Rows[0][CUserTable.FLD_CITY];
                    row[PostalAddressEntityTable.FLD_ZIP] = cuser.Rows[0][CUserTable.FLD_POSTAL_CODE];
                    row[PostalAddressEntityTable.FLD_SUBDIVISION_CODE] = "US-" + cuser.Rows[0][CUserTable.FLD_STATE].ToString();

                    row[PostalAddressEntityTable.FLD_CREATE_USER_ID] = UserID;

                    dtblAddress.Rows.Add(row);

                }
            }
            SetFMShippingSupplyPhoneNumber(dtsOrderData, UserID, cuser);
            SetFMShippingSupplyEmailAddress(dtsOrderData, UserID, cuser);


        }
        public void SetFMShippingSupplyPhoneNumber(OrderData dtsOrderData, int UserID, CUserTable cuser)
        {
            ShipmentGroupTable dTblShipmentGroup = dtsOrderData.ShipmentGroup;
            DataRow rowShipGrp = dTblShipmentGroup.Rows[0];

            PhoneNumberEntityTable dtblPhoneNumber = dtsOrderData.OrderPhoneNumber;
            DataView DVShippingSupply = new DataView(dtblPhoneNumber);

            int ShipGrpSupplyID = -1;

            //Phone Number			
            DVShippingSupply.RowFilter = PhoneNumberEntityTable.FLD_ENTITY_TYPE_ID + " = " + EntityType.TYPE_ORDER_SHIPPING
                + " AND " + PhoneNumberEntityTable.FLD_TYPE + " = " + PhoneNumberType.TYPE_SHIPPING_PHONE.ToString()
                + " AND " + PhoneNumberEntityTable.FLD_ENTITY_ID + " = " + ShipGrpSupplyID.ToString();

            if (DVShippingSupply.Count == 0) //Do the operation if never done
            {
                if (cuser.Rows.Count > 0)
                {
                    //Add a new Shipping Phone Number as default
                    int PhoneNewID = dtblPhoneNumber.Rows.Count;
                    DataRow row = dtblPhoneNumber.NewRow();

                    row[PhoneNumberEntityTable.FLD_PHONE_NUMBER_ID] = PhoneNewID;
                    row[PhoneNumberEntityTable.FLD_ENTITY_ID] = ShipGrpSupplyID;
                    row[PhoneNumberEntityTable.FLD_ENTITY_TYPE_ID] = EntityType.TYPE_ORDER_SHIPPING; //Shipping;
                    row[PhoneNumberEntityTable.FLD_TYPE] = PhoneNumberType.TYPE_SHIPPING_PHONE; //Shipping Phone Number
                    row[PhoneNumberEntityTable.FLD_PHONE_NUMBER] = cuser.Rows[0][CUserTable.FLD_WORK_PHONE];

                    row[PhoneNumberEntityTable.FLD_CREATE_USER_ID] = UserID;

                    dtblPhoneNumber.Rows.Add(row);



                }
            }
            //Phone Number
            DVShippingSupply.RowFilter = PhoneNumberEntityTable.FLD_ENTITY_TYPE_ID + " = " + EntityType.TYPE_ORDER_SHIPPING
                + " AND " + PhoneNumberEntityTable.FLD_TYPE + " = " + PhoneNumberType.TYPE_SHIPPING_FAX.ToString()
                + " AND " + PhoneNumberEntityTable.FLD_ENTITY_ID + " = " + ShipGrpSupplyID.ToString();

            if (DVShippingSupply.Count == 0) //Do the operation if never done
            {
                if (cuser.Rows.Count > 0)
                {
                    //Add a new Shipping Phone Number as default
                    int PhoneNewID = dtblPhoneNumber.Rows.Count;
                    DataRow row = dtblPhoneNumber.NewRow();

                    row[PhoneNumberEntityTable.FLD_PHONE_NUMBER_ID] = PhoneNewID;
                    row[PhoneNumberEntityTable.FLD_ENTITY_ID] = ShipGrpSupplyID;
                    row[PhoneNumberEntityTable.FLD_ENTITY_TYPE_ID] = EntityType.TYPE_ORDER_SHIPPING; //Shipping;
                    row[PhoneNumberEntityTable.FLD_TYPE] = PhoneNumberType.TYPE_SHIPPING_FAX; //Fax
                    row[PhoneNumberEntityTable.FLD_PHONE_NUMBER] = cuser.Rows[0][CUserTable.FLD_FAX_PHONE];

                    row[PhoneNumberEntityTable.FLD_CREATE_USER_ID] = UserID;

                    dtblPhoneNumber.Rows.Add(row);



                }
            }

        }
        public void SetFMShippingSupplyEmailAddress(OrderData dtsOrderData, int UserID, CUserTable cuser)
        {
            ShipmentGroupTable dTblShipmentGroup = dtsOrderData.ShipmentGroup;
            DataRow rowShipGrp = dTblShipmentGroup.Rows[0];

            int ShipGrpSupplyID = -1;
            EmailEntityTable dTblEmailAddress = dtsOrderData.OrderEmailAddress;
            DataView DVShippingSupply = new DataView(dTblEmailAddress);
            DVShippingSupply.RowFilter = EmailEntityTable.FLD_ENTITY_TYPE_ID + " = " + EntityType.TYPE_ORDER_BILLING
                + " AND " + EmailEntityTable.FLD_TYPE + " = " + EmailType.TYPE_SHIPPING.ToString()
                + " AND " + PhoneNumberEntityTable.FLD_ENTITY_ID + " = " + ShipGrpSupplyID.ToString();

            if (DVShippingSupply.Count == 0)
            {
                if (cuser.Rows.Count > 0)
                {
                    //Add a new Shipping Address as default
                    int EmailNewID = dTblEmailAddress.Rows.Count;
                    DataRow row = dTblEmailAddress.NewRow();

                    DataRow newRow = dTblEmailAddress.NewRow();
                    newRow[EmailEntityTable.FLD_TYPE] = EmailType.TYPE_SHIPPING;
                    newRow[EmailEntityTable.FLD_EMAIL_ADDRESS] = cuser.Rows[0][CUserTable.FLD_CORP_EMAIL];
                    newRow[EmailEntityTable.FLD_ENTITY_TYPE_ID] = EntityType.TYPE_ORDER_SHIPPING; //Shipping
                    newRow[EmailEntityTable.FLD_ENTITY_ID] = ShipGrpSupplyID;
                    newRow[EmailEntityTable.FLD_CREATE_USER_ID] = UserID;
                    dTblEmailAddress.Rows.Add(newRow);


                }
            }


        }

        #endregion

        public void SetDefaultBillingPostalAddress(OrderData dtsOrderData, int UserID)
        {
            int CampaignID = Convert.ToInt32(dtsOrderData.OrderHeader.Rows[0][OrderHeaderTable.FLD_CAMPAIGN_ID]);
            int OrderID = Convert.ToInt32(dtsOrderData.OrderHeader.Rows[0][OrderHeaderTable.FLD_PKID]);
            QSPForm.Business.PostalAddressSystem addrSys = new QSPForm.Business.PostalAddressSystem();
            PostalAddressEntityTable dTblCampaignAddress;// = (PostalAddressEntityTable)((OrderForm_Step)this.Page).DataSource.BillingPostalAddress;			
            dTblCampaignAddress = addrSys.SelectAllByCampaignID(CampaignID);
            PostalAddressEntityTable dtblAddress = dtsOrderData.OrderPostalAddress;

            PostalAddressSystem postSys = new PostalAddressSystem();
            postSys.CopyToEntity(dTblCampaignAddress, dtblAddress, UserID,
                EntityType.TYPE_CAMPAIGN, CampaignID, PostalAddressType.TYPE_BILLING,
                EntityType.TYPE_ORDER_BILLING, OrderID, PostalAddressType.TYPE_BILLING);

        }
        public void SetDefaultBillingPhoneNumber(OrderData dtsOrderData, int UserID)
        {
            int CampaignID = Convert.ToInt32(dtsOrderData.OrderHeader.Rows[0][OrderHeaderTable.FLD_CAMPAIGN_ID]);
            int OrderID = Convert.ToInt32(dtsOrderData.OrderHeader.Rows[0][OrderHeaderTable.FLD_PKID]);
            //Phone Number
            QSPForm.Business.PhoneNumberSystem phoneSys = new QSPForm.Business.PhoneNumberSystem();
            PhoneNumberEntityTable dTblCampaignPhone;
            PhoneNumberEntityTable dTblBillingPhoneNumber;
            dTblCampaignPhone = phoneSys.SelectAllByCampaignID(CampaignID);
            dTblBillingPhoneNumber = dtsOrderData.OrderPhoneNumber;

            phoneSys.CopyToEntity(dTblCampaignPhone, dTblBillingPhoneNumber, UserID,
                EntityType.TYPE_CAMPAIGN, CampaignID, PhoneNumberType.TYPE_BILLING_PHONE,
                EntityType.TYPE_ORDER_BILLING, OrderID, PhoneNumberType.TYPE_BILLING_PHONE);

            phoneSys.CopyToEntity(dTblCampaignPhone, dTblBillingPhoneNumber, UserID,
                EntityType.TYPE_CAMPAIGN, CampaignID, PhoneNumberType.TYPE_BILLING_FAX,
                EntityType.TYPE_ORDER_BILLING, OrderID, PhoneNumberType.TYPE_BILLING_FAX);


        }
        public void SetDefaultBillingEmailAddress(OrderData dtsOrderData, int UserID)
        {
            int CampaignID = Convert.ToInt32(dtsOrderData.OrderHeader.Rows[0][OrderHeaderTable.FLD_CAMPAIGN_ID]);
            int OrderID = Convert.ToInt32(dtsOrderData.OrderHeader.Rows[0][OrderHeaderTable.FLD_PKID]);
            //Email Address
            QSPForm.Business.EmailAddressSystem emailSys = new QSPForm.Business.EmailAddressSystem();
            EmailEntityTable dTblCampaignEmail;
            EmailEntityTable dTblBillingEmail;
            dTblCampaignEmail = emailSys.SelectAllByCampaignID(CampaignID);
            dTblBillingEmail = dtsOrderData.OrderEmailAddress;

            emailSys.CopyToEntity(dTblCampaignEmail, dTblBillingEmail, UserID,
                EntityType.TYPE_CAMPAIGN, CampaignID, EmailType.TYPE_BILLING,
                EntityType.TYPE_ORDER_BILLING, OrderID, EmailType.TYPE_BILLING);

        }
        public void SetDefaultShippingPostalAddress(OrderData dtsOrderData, int UserID)
        {
            //Create One Row for the Sipment Group
            ShipmentGroupTable dTblShipmentGroup = dtsOrderData.ShipmentGroup;
            DataRow rowShipGrp = dTblShipmentGroup.Rows[0];
            //			rowShipGrp[ShipmentGroupTable.FLD_CREATE_USER_ID] = UserID;
            //			dTblShipmentGroup.Rows.Add(rowShipGrp);	

            int OrderID = Convert.ToInt32(dtsOrderData.OrderHeader.Rows[0][OrderHeaderTable.FLD_PKID]);
            int ShipGrpID = Convert.ToInt32(dtsOrderData.ShipmentGroup.Rows[0][ShipmentGroupTable.FLD_PKID]);

            int CampaignID = Convert.ToInt32(dtsOrderData.OrderHeader.Rows[0][OrderHeaderTable.FLD_CAMPAIGN_ID]);
            QSPForm.Business.PostalAddressSystem addrSys = new QSPForm.Business.PostalAddressSystem();
            PostalAddressEntityTable dTblCampaignAddress;// = (PostalAddressEntityTable)((OrderForm_Step)this.Page).DataSource.BillingPostalAddress;			
            dTblCampaignAddress = addrSys.SelectAllByCampaignID(CampaignID);
            PostalAddressEntityTable dTblPostalAddress = dtsOrderData.OrderPostalAddress;

            PostalAddressSystem postSys = new PostalAddressSystem();
            postSys.CopyToEntity(dTblCampaignAddress, dTblPostalAddress, UserID,
                EntityType.TYPE_CAMPAIGN, CampaignID, PostalAddressType.TYPE_SHIPPING,
                EntityType.TYPE_ORDER_SHIPPING, ShipGrpID, PostalAddressType.TYPE_SHIPPING);

        }
        public void SetDefaultShippingPhoneNumber(OrderData dtsOrderData, int UserID)
        {
            int CampaignID = Convert.ToInt32(dtsOrderData.OrderHeader.Rows[0][OrderHeaderTable.FLD_CAMPAIGN_ID]);
            int OrderID = Convert.ToInt32(dtsOrderData.OrderHeader.Rows[0][OrderHeaderTable.FLD_PKID]);
            //Phone Number
            QSPForm.Business.PhoneNumberSystem phoneSys = new QSPForm.Business.PhoneNumberSystem();
            PhoneNumberEntityTable dTblCampaignPhone;
            dTblCampaignPhone = phoneSys.SelectAllByCampaignID(CampaignID);

            PhoneNumberEntityTable dtblPhoneNumber = dtsOrderData.OrderPhoneNumber;
            DataView DVShipping = new DataView(dtblPhoneNumber);
            DataView DVCamp = new DataView(dTblCampaignPhone);
            ShipmentGroupTable dTblShipmentGroup = dtsOrderData.ShipmentGroup;
            DataRow rowShipGrp = dTblShipmentGroup.Rows[0];

            int ShipGrpID = Convert.ToInt32(rowShipGrp[ShipmentGroupTable.FLD_PKID]);

            //Phone Number
            phoneSys.CopyToEntity(dTblCampaignPhone, dtblPhoneNumber, UserID,
                EntityType.TYPE_CAMPAIGN, CampaignID, PhoneNumberType.TYPE_SHIPPING_PHONE,
                EntityType.TYPE_ORDER_SHIPPING, ShipGrpID, PhoneNumberType.TYPE_SHIPPING_PHONE);

            phoneSys.CopyToEntity(dTblCampaignPhone, dtblPhoneNumber, UserID,
                EntityType.TYPE_CAMPAIGN, CampaignID, PhoneNumberType.TYPE_SHIPPING_FAX,
                EntityType.TYPE_ORDER_SHIPPING, ShipGrpID, PhoneNumberType.TYPE_SHIPPING_FAX);

        }
        public void SetDefaultShippingEmailAddress(OrderData dtsOrderData, int UserID)
        {
            ShipmentGroupTable dTblShipmentGroup = dtsOrderData.ShipmentGroup;
            DataRow rowShipGrp = dTblShipmentGroup.Rows[0];

            int OrderID = Convert.ToInt32(dtsOrderData.OrderHeader.Rows[0][OrderHeaderTable.FLD_PKID]);
            int ShipGrpID = Convert.ToInt32(rowShipGrp[ShipmentGroupTable.FLD_PKID]);
            int CampaignID = Convert.ToInt32(dtsOrderData.OrderHeader.Rows[0][OrderHeaderTable.FLD_CAMPAIGN_ID]);

            //Email Address
            QSPForm.Business.EmailAddressSystem emailSys = new QSPForm.Business.EmailAddressSystem();
            EmailEntityTable dTblCampaignEmail = emailSys.SelectAllByCampaignID(CampaignID);
            EmailEntityTable dTblEmailAddress = dtsOrderData.OrderEmailAddress;

            emailSys.CopyToEntity(dTblCampaignEmail, dTblEmailAddress, UserID,
                EntityType.TYPE_CAMPAIGN, CampaignID, EmailType.TYPE_SHIPPING,
                EntityType.TYPE_ORDER_SHIPPING, ShipGrpID, EmailType.TYPE_SHIPPING);

        }

        private bool Refresh(OrderData dts, AccountData dtsAccount, int UserID, int dataOperation, Data.ConnectionProvider connProvider)
        {
            bool IsSuccess = false;

            FormData dtsForm = new FormData();
            FormSystem frmSys = new FormSystem();

            DataRow ordRow = dts.OrderHeader.Rows[0];
            int FormID = Convert.ToInt32(ordRow[dataDef.FLD_FORM_ID]);
            dtsForm = frmSys.SelectAllDetail(FormID, true);

            IsSuccess = RefreshValidation(dts, dtsAccount, dtsForm, UserID, dataOperation, connProvider);
            IsSuccess = RefreshTask(dts, dtsAccount, dtsForm, UserID, dataOperation, connProvider);

            return IsSuccess;

        }
        private bool Refresh(OrderData dts, int UserID, int dataOperation, Data.ConnectionProvider connProvider)
        {
            bool IsSuccess = false;
            AccountData dtsAccount = new AccountData();
            if (dts.OrderHeader.Rows.Count > 0)
            {
                DataRow ordRow = dts.OrderHeader.Rows[0];
                if (!ordRow.IsNull(OrderHeaderTable.FLD_CAMPAIGN_ID))
                {
                    int CampaignID = Convert.ToInt32(ordRow[OrderHeaderTable.FLD_CAMPAIGN_ID]);
                    Data.Account accDataAccess = new Data.Account();
                    if (connProvider != null)
                        accDataAccess.MainConnectionProvider = connProvider;
                    dtsAccount = accDataAccess.SelectAllDetailByCampaignID(CampaignID);
                    AccountSystem accSys = new AccountSystem();
                    accSys.PerformValidation(dtsAccount, UserID, Common.DataOperation.UPDATE);
                }
            }
            IsSuccess = Refresh(dts, dtsAccount, UserID, dataOperation, connProvider);

            return IsSuccess;

        }

        public bool PrePerformValidation(OrderData dtsOrder, AccountData dtsAccount, int UserID, int dataOperation)
        {
            bool IsValid = true;

            try
            {
                FormData dtsForm = new FormData();
                FormSystem frmSys = new FormSystem();

                DataRow ordRow = dtsOrder.OrderHeader.Rows[0];
                int FormID = Convert.ToInt32(ordRow[dataDef.FLD_FORM_ID]);
                dtsForm = frmSys.SelectAllDetail(FormID, true);
                //Handle Rules and Exception on Nb Day Lead Time.



                IsValid = PerformValidation(dtsOrder, dtsAccount, dtsForm, UserID, dataOperation, null);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return IsValid;
        }
        private bool PerformValidation(OrderData dtsOrder, AccountData dtsAccount, FormData dtsForm, int UserID, int dataOperation, Data.ConnectionProvider connProvider)
        {
            bool IsValid = true;

            try
            {
                //Check for the status before....
                if (!IsRefreshable(dtsOrder))
                {
                    IsValid = true;
                }
                else
                {

                    QSPForm.Business.FormSystem formSys = new QSPForm.Business.FormSystem();
                    IsValid = formSys.PerformValidation(dtsOrder, dtsAccount, dtsForm, UserID, dataOperation, connProvider);

                    //Sum the Total Fees Amount in the Shipment Groups.
                    //We need the creation of Exception to calculate the fees amt
                    decimal feesAmt = 0;
                    foreach (DataRow rowExc in dtsOrder.OrderException.Rows)
                    {
                        if (rowExc.RowState != DataRowState.Deleted)
                        {
                            if (rowExc[EntityExceptionTable.FLD_FEES_VALUE_AMOUNT] != System.DBNull.Value)
                                feesAmt = feesAmt + Convert.ToDecimal(rowExc[EntityExceptionTable.FLD_FEES_VALUE_AMOUNT]);
                        }
                    }
                    DataRow shipRow = dtsOrder.ShipmentGroup.Rows[0];
                    CommonSystem comSys = new CommonSystem();
                    comSys.UpdateRow(shipRow, ShipmentGroupTable.FLD_SHIPPING_CHARGES, feesAmt.ToString());
                    if (shipRow.RowState != DataRowState.Unchanged)
                    {
                        if (shipRow.RowState != DataRowState.Added)
                            shipRow[ShipmentGroupTable.FLD_CREATE_USER_ID] = UserID;
                        else
                            shipRow[ShipmentGroupTable.FLD_UPDATE_USER_ID] = UserID;
                    }
                    SetStatus(dtsAccount, dtsOrder, UserID);
                }
                DataRow ordRow = dtsOrder.OrderHeader.Rows[0];
                ordRow[dataDef.FLD_IS_VALIDATION_PERFORMED] = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return IsValid;
        }
        private bool RefreshValidation(OrderData dtsOrder, AccountData dtsAccount, FormData dtsForm, int UserID, int dataOperation, Data.ConnectionProvider connProvider)
        {
            bool IsSuccess = false;

            DataRow ordRow = dtsOrder.OrderHeader.Rows[0];
            //1 - Perform Order Exception
            bool IsValidationPerformed = false;
            if (!ordRow.IsNull(dataDef.FLD_IS_VALIDATION_PERFORMED))
                IsValidationPerformed = Convert.ToBoolean(ordRow[dataDef.FLD_IS_VALIDATION_PERFORMED]);
            if (!IsValidationPerformed)
                IsSuccess = PerformValidation(dtsOrder, dtsAccount, dtsForm, UserID, dataOperation, connProvider);

            if (dtsOrder.OrderException.GetChanges() != null)
            {
                Data.Entity_exception excDataAccess = new Data.Entity_exception();
                if (connProvider != null)
                    excDataAccess.MainConnectionProvider = connProvider;

                excDataAccess.UpdateBatch(dtsOrder.OrderException);
            }

            CommonSystem comSys = new CommonSystem();
            //Determine the Order Status
            SetStatus(dtsAccount, dtsOrder, UserID);
            if (dtsOrder.OrderHeader.GetChanges() != null)
            {
                //Update if it's the case
                Data.Order ordDataAccess = new Data.Order();
                if (connProvider != null)
                    ordDataAccess.MainConnectionProvider = connProvider;
                ordDataAccess.Update(dtsOrder.OrderHeader);
                string sStatusID = ordRow[OrderHeaderTable.FLD_ORDER_STATUS_ID].ToString();

                //Synchronize the status for the Order Supply related
                //for now we only change the "Wait for approval" (5) to "In process" (101)
                if (!ordRow.IsNull(OrderHeaderTable.FLD_SUPPLY_ORDER_ID))
                {
                    int OrderSupplyID = Convert.ToInt32(ordRow[OrderHeaderTable.FLD_SUPPLY_ORDER_ID]);
                    OrderHeaderTable dTblOrderSupply = ordDataAccess.SelectOne(OrderSupplyID);
                    if (dTblOrderSupply.Rows.Count > 0)
                    {
                        DataRow suppRow = dTblOrderSupply.Rows[0];
                        int SupplyStatus = 0;
                        if (!suppRow.IsNull(OrderHeaderTable.FLD_ORDER_STATUS_ID))
                            SupplyStatus = Convert.ToInt32(suppRow[OrderHeaderTable.FLD_ORDER_STATUS_ID]);
                        if (SupplyStatus < OrderStatus.IN_PROCESS)
                        {
                            comSys.UpdateRow(suppRow, OrderHeaderTable.FLD_ORDER_STATUS_ID, sStatusID);
                            if (dTblOrderSupply.GetChanges() != null)
                            {
                                ordDataAccess.Update(dTblOrderSupply);
                            }
                        }
                        //Synchronize for Supply Detail Items
                        //						foreach (DataRow ordSuppRow in dtsOrder.OrderSupply.Rows)
                        //						{
                        //							if (ordSuppRow.RowState != DataRowState.Deleted)
                        //								comSys.UpdateRow(ordSuppRow, OrderDetailTable.FLD_ORDER_STATUS_ID, sStatusID);						
                        //						}
                        //						if (dtsOrder.OrderSupply.GetChanges() != null)
                        //						{
                        //							ordDetailDataAccess.UpdateBatch(dtsOrder.OrderSupply);
                        //						}
                    }
                }

            }


            return IsSuccess;
        }

        private bool PerformTask(OrderData dtsOrder, AccountData dtsAccount, FormData dtsForm, int UserID, int dataOperation, Data.ConnectionProvider connProvider)
        {
            bool IsValid = true;

            try
            {
                if (!IsRefreshable(dtsOrder))
                {
                    IsValid = true;
                }
                else
                {
                    DataRow ordRow = dtsOrder.OrderHeader.Rows[0];
                    if (!ordRow.IsNull(OrderHeaderTable.FLD_FORM_ID))
                    {
                        int FormID = Convert.ToInt32(ordRow[OrderHeaderTable.FLD_FORM_ID]);
                        QSPForm.Business.FormSystem formSys = new QSPForm.Business.FormSystem();
                        IsValid = formSys.PerformTask(dtsOrder, dtsAccount, dtsForm, UserID, dataOperation, connProvider);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return IsValid;
        }
        private bool RefreshTask(OrderData dtsOrder, AccountData dtsAccount, FormData dtsForm, int UserID, int dataOperation, Data.ConnectionProvider connProvider)
        {
            bool IsSuccess = false;

            //1 - Perform Order Task						
            IsSuccess = PerformTask(dtsOrder, dtsAccount, dtsForm, UserID, dataOperation, connProvider);

            return IsSuccess;
        }
        private bool IsRefreshable(OrderData dts)
        {
            bool isRefreshable = false;
            int StatusID = 0;
            DataRow ordRow = dts.OrderHeader.Rows[0];
            if (!ordRow.IsNull(dataDef.FLD_ORDER_STATUS_ID))
            {
                StatusID = Convert.ToInt32(ordRow[dataDef.FLD_ORDER_STATUS_ID]);
            }

            //Check for the status before....
            if ((StatusID != OrderStatus.SAVED_FOR_LATER) && (StatusID != OrderStatus.IN_PROCESS_CANCELLED) &&
                !((StatusID >= Common.OrderStatus.RELEASED) && (StatusID < Common.OrderStatus.ERROR_UNSPECIFIED)) &&
                (StatusID != OrderStatus.ERROR_WAITING_ROLLBACK) &&
                (StatusID != OrderStatus.WAIT_FOR_PERSONALIZATION))
            {
                isRefreshable = true;
            }

            return isRefreshable;
        }

        public void SetStatus(int orderId, int orderStatusId, string changeReason, int userId)
        {
            #region Update order status

            QSP.Business.Fulfillment.Order order = QSP.Business.Fulfillment.Order.GetOrder(orderId);

            order.OrderStatusId = orderStatusId;
            order.UpdateDate = DateTime.Now;
            order.UpdateUserId = userId;

            QSP.Business.Fulfillment.Order.UpdateOrder(order);

            #endregion

            #region Add order status change record

            QSP.Business.Fulfillment.OrderStatusChange osc = new QSP.Business.Fulfillment.OrderStatusChange();

            //osc.OrderStatusChangeId;
            osc.OrderId = orderId;
            osc.OrderStatusId = orderStatusId;
            osc.StatusChangeReason = changeReason;
            osc.Deleted = false;
            osc.CreateDate = DateTime.Now;
            osc.CreateUserId = userId;
            osc.UpdateDate = DateTime.Now;
            osc.UpdateUserId = userId;

            QSP.Business.Fulfillment.OrderStatusChange.InsertOrderStatusChange(osc);

            #endregion
        }
        private void SetStatus(AccountData dtsAccount, OrderData dtsOrder, int UserID)
        {
            //if it's refreshable, otherwise don't do anything
            if (IsRefreshable(dtsOrder))
            {
                DataRow ordRow = dtsOrder.OrderHeader.Rows[0];
                EntityExceptionTable ordExc = dtsOrder.OrderException;
                EntityExceptionTable accExc = dtsAccount.AccountException;

                int OrderStatusID = Convert.ToInt32(ordRow[OrderHeaderTable.FLD_ORDER_STATUS_ID]);
                //Means that no exception have to be done
                //ToDo Pre-Sales Estimates
                if ((OrderStatusID != OrderStatus.SAVED_FOR_LATER) && (OrderStatusID != OrderStatus.CANCELLED) && (OrderStatusID != OrderStatus.IN_PROCESS_CANCELLED))
                {
                    bool IsValid = false;
                    //Apply Validation on the account Level First
                    DataView dvException = new DataView(accExc);
                    string sFilter = EntityExceptionTable.FLD_EXCEPTION_TYPE_ID + " >= " + Convert.ToInt32(BusinessExceptionType.Standard_Exception).ToString() +
                        " OR ((" + EntityExceptionTable.FLD_EXCEPTION_TYPE_ID + " >= " + Convert.ToInt32(BusinessExceptionType.Approved_Exception).ToString() +
                        " AND " + EntityExceptionTable.FLD_EXCEPTION_TYPE_ID + " < " + Convert.ToInt32(BusinessExceptionType.Standard_Exception).ToString() + ") " +
                        " AND ISNULL(" + EntityExceptionTable.FLD_APPROVED + ",FALSE) = FALSE)";
                    dvException.RowFilter = sFilter;
                    if (dvException.Count == 0)
                    {
                        //Do the same for the Order Level
                        dvException.Table = ordExc;
                        dvException.RowFilter = sFilter;
                        if (dvException.Count == 0)
                            IsValid = true;
                    }
                    dvException = null;

                    if (IsValid)
                    {
                        OrderStatusID = Common.OrderStatus.IN_PROCESS;
                    }
                    else
                        OrderStatusID = Common.OrderStatus.WAIT_FOR_APPROVAL;

                    CommonSystem comSys = new CommonSystem();


                    comSys.UpdateRow(ordRow, OrderHeaderTable.FLD_ORDER_STATUS_ID, OrderStatusID.ToString());
                    if (ordRow.RowState != DataRowState.Unchanged)
                    {
                        DataTable dTblStatus = comSys.SelectOneOrderStatus(OrderStatusID);
                        if (dTblStatus.Rows.Count > 0)
                        {
                            DataRow statusRow = dTblStatus.Rows[0];
                            ordRow[OrderHeaderTable.FLD_ORDER_STATUS_NAME] = statusRow[1].ToString();
                            //Short description
                            if (!statusRow.IsNull("short_description"))
                                ordRow[OrderHeaderTable.FLD_ORDER_STATUS_SHORT_DESCRIPTION] = statusRow["short_description"];
                            else
                                ordRow[OrderHeaderTable.FLD_ORDER_STATUS_SHORT_DESCRIPTION] = statusRow[1].ToString();

                            //Description
                            if (!statusRow.IsNull("description"))
                                ordRow[OrderHeaderTable.FLD_ORDER_STATUS_DESCRIPTION] = statusRow["description"];
                            else
                                ordRow[OrderHeaderTable.FLD_ORDER_STATUS_DESCRIPTION] = statusRow[1].ToString();


                            //Color
                            if (!statusRow.IsNull("color_code"))
                                ordRow[OrderHeaderTable.FLD_ORDER_STATUS_COLOR_CODE] = statusRow["color_code"];
                            else
                                ordRow[OrderHeaderTable.FLD_ORDER_STATUS_COLOR_CODE] = "white";

                            if (!statusRow.IsNull("status_category_id"))
                            {
                                ordRow[OrderHeaderTable.FLD_ORDER_STATUS_CATEGORY_ID] = Convert.ToInt32(statusRow["status_category_id"]);
                                ordRow[OrderHeaderTable.FLD_ORDER_STATUS_CATEGORY_NAME] = statusRow["status_category_name"].ToString();
                            }
                            else
                            {
                                ordRow[OrderHeaderTable.FLD_ORDER_STATUS_CATEGORY_ID] = DBNull.Value;
                                ordRow[OrderHeaderTable.FLD_ORDER_STATUS_CATEGORY_NAME] = DBNull.Value;
                            }

                        }
                        ordRow[OrderHeaderTable.FLD_UPDATE_USER_ID] = UserID;
                    }

                }
            }
        }
        public bool SetCancelStatus(OrderData dts, int UserID)
        {
            bool isChanged = false;
            int StatusID = 0;
            DataRow ordRow = dts.OrderHeader.Rows[0];
            if (!ordRow.IsNull(dataDef.FLD_ORDER_STATUS_ID))
            {
                StatusID = Convert.ToInt32(ordRow[dataDef.FLD_ORDER_STATUS_ID]);
            }

            //Check for the status before....
            if (IsCancelable(dts))
            {
                //if (ordSys.is
                if (ordRow.IsNull(OrderHeaderTable.FLD_FULF_ORDER_ID))
                {
                    //if ((Convert.ToInt32(ordRow[dataDef.FLD_ORDER_STATUS_ID].ToString()) < 300) && (Convert.ToInt32(ordRow[dataDef.FLD_ORDER_STATUS_ID].ToString()) >= 200))
                    if (Convert.ToInt32(ordRow[dataDef.FLD_ORDER_STATUS_ID].ToString()) >= 200 && Convert.ToInt32(ordRow[dataDef.FLD_ORDER_STATUS_ID].ToString()) < 9000)
                    {
                        ordRow[OrderHeaderTable.FLD_ORDER_STATUS_ID] = Common.OrderStatus.IN_PROCESS_CANCELLED;
                    }
                    else
                    {
                        ordRow[OrderHeaderTable.FLD_ORDER_STATUS_ID] = Common.OrderStatus.CANCELLED;
                    }
                }
                else
                    ordRow[OrderHeaderTable.FLD_ORDER_STATUS_ID] = Common.OrderStatus.IN_PROCESS_CANCELLED;
                isChanged = true;
            }

            return isChanged;
        }
        public bool IsCancelable(OrderData dts)
        {
            bool isCancelable = false;
            int StatusID = 0;
            DataRow ordRow = dts.OrderHeader.Rows[0];
            if (!ordRow.IsNull(dataDef.FLD_ORDER_STATUS_ID))
            {
                StatusID = Convert.ToInt32(ordRow[dataDef.FLD_ORDER_STATUS_ID]);
            }

            //Check for the status before....
            if (!((StatusID >= Common.OrderStatus.RELEASED) &&
                (StatusID < Common.OrderStatus.ERROR_UNSPECIFIED)) &&
                (StatusID != OrderStatus.ERROR_WAITING_ROLLBACK))
            {
                isCancelable = true;
            }

            return isCancelable;
        }

        public decimal CalculateTax(OrderData dtsOrder, int UserID)
        {
            AccountSystem accSys = new AccountSystem();

            int CampaignID = 0;

            if (dtsOrder.OrderHeader.Rows.Count > 0)
            {
                CampaignID = Convert.ToInt32(dtsOrder.OrderHeader.Rows[0][OrderHeaderTable.FLD_CAMPAIGN_ID]);
            }

            AccountData dtsAccount = accSys.SelectAllDetailByCampaignID(CampaignID);

            return CalculateTax(dtsOrder, dtsAccount, UserID);
        }
        public decimal CalculateTax(OrderData dtsOrder, AccountData dtsAccount, int UserID)
        {
            //Get the Tax Rate
            decimal taxRate = 0;
            int OrgType = 0;
            int FormID = 0;
            OrderHeaderTable dTblOrder = dtsOrder.OrderHeader;

            if (dTblOrder.Rows.Count > 0)
            {
                DataRow ordRow = dtsOrder.OrderHeader.Rows[0];
                OrderDetailTable dTblOrderDetail = dtsOrder.OrderDetail;
                OrderDetailTaxTable dTblOrderDetailTax = dtsOrder.OrderDetailTax;
                OrganizationSystem orgSys = new OrganizationSystem();
                OrganizationTable dtOrganization = dtsAccount.Organization;
                EntityExceptionTable accExc = dtsAccount.AccountException;


                if (ordRow.RowState == DataRowState.Added)
                {
                    dTblOrderDetailTax.Clear();
                }

                DataRow accountRow = dtsAccount.Account.Rows[0];
                int accountId = Convert.ToInt32(accountRow[AccountTable.FLD_PKID]);

                AccountSystem accSys = new AccountSystem();
                bool IsTaxExempted = accSys.IsAccountCurrentlyTaxExempt(accountId);

                if (!IsTaxExempted)
                {

                    if (dtOrganization.Rows.Count > 0)
                    {
                        DataRow orgRow = dtOrganization.Rows[0];
                        OrgType = Convert.ToInt32(orgRow[OrganizationTable.FLD_ORG_TYPE_ID]);
                    }

                    if (!ordRow.IsNull(OrderHeaderTable.FLD_FORM_ID))
                    {
                        FormID = Convert.ToInt32(ordRow[OrderHeaderTable.FLD_FORM_ID]);
                    }
                    Business.CommonSystem comSys = new QSPForm.Business.CommonSystem();
                    //Change about the tax calculation		
                    TAXFLETable dTblTax = new TAXFLETable();
                    try
                    {
                        dTblTax = comSys.GetTAXFLEInfo(dtsOrder, FormID, EntityType.TYPE_ORDER_BILLING);// 0.05M;
                    }
                    catch (Exception ex)
                    {
                        string s = ex.Message;

                    }
                    decimal taxRateCity = 0;
                    decimal taxRateCounty = 0;
                    decimal taxRateState = 0;
                    if (dTblTax.Rows.Count > 0)
                    {
                        DataRow row = dTblTax.Rows[0];
                        if (!row.IsNull(TAXFLETable.FLD_FDSTTX)) //State Rate 
                        {
                            taxRateState = Convert.ToDecimal(row[TAXFLETable.FLD_FDSTTX]);
                            taxRate = taxRate + Convert.ToDecimal(row[TAXFLETable.FLD_FDSTTX]);
                        }
                        if (!row.IsNull(TAXFLETable.FLD_FDCOTX)) //County Rate 
                        {
                            taxRateCounty = Convert.ToDecimal(row[TAXFLETable.FLD_FDCOTX]);
                            taxRate = taxRate + Convert.ToDecimal(row[TAXFLETable.FLD_FDCOTX]);
                        } if (!row.IsNull(TAXFLETable.FLD_FDCITX)) //City Rate 
                        {
                            taxRateCity = Convert.ToDecimal(row[TAXFLETable.FLD_FDCITX]);
                            taxRate = taxRate + Convert.ToDecimal(row[TAXFLETable.FLD_FDCITX]);
                        }
                    }

                    foreach (DataRow ordDetailRow in dTblOrderDetail.Rows)
                    {
                        if (ordDetailRow.RowState != DataRowState.Deleted)
                        {
                            decimal amount = 0;
                            if (!ordDetailRow.IsNull(OrderDetailTable.FLD_AMOUNT))
                                amount = Convert.ToDecimal(ordDetailRow[OrderDetailTable.FLD_AMOUNT]);
                            decimal taxAmount = taxRate * amount;
                            ordRow[OrderDetailTable.FLD_TAX_RATE] = taxRate;

                            DataView dv = new DataView(dTblOrderDetailTax);
                            int iIndex = -1;
                            dv.Sort = OrderDetailTaxTable.FLD_ORDER_DETAIL_ID;
                            iIndex = dv.Find(ordDetailRow[OrderDetailTable.FLD_PKID]);
                            if (iIndex == -1)
                            {
                                if (amount > 0)
                                {
                                    DataRow orderTaxRow = dTblOrderDetailTax.NewRow();
                                    orderTaxRow[OrderDetailTaxTable.FLD_TAX_TYPE_ID] = 1; //
                                    orderTaxRow[OrderDetailTaxTable.FLD_TAX_RATE] = taxRate;
                                    orderTaxRow[OrderDetailTaxTable.FLD_AMOUNT] = taxAmount;
                                    orderTaxRow[OrderDetailTaxTable.FLD_CREATE_USER_ID] = UserID;
                                    orderTaxRow[OrderDetailTaxTable.FLD_ORDER_DETAIL_ID] = ordDetailRow[OrderDetailTable.FLD_PKID];
                                    dTblOrderDetailTax.Rows.Add(orderTaxRow);
                                }
                            }
                            else
                            {
                                //Clean order tax table for missing line item
                                if (amount == 0)
                                {
                                    dv[iIndex].Delete();
                                }
                                else
                                {
                                    DataRow orderTaxRow = dv[iIndex].Row;
                                    orderTaxRow[OrderDetailTaxTable.FLD_TAX_TYPE_ID] = 1; //
                                    orderTaxRow[OrderDetailTaxTable.FLD_TAX_RATE] = taxRate;
                                    orderTaxRow[OrderDetailTaxTable.FLD_AMOUNT] = taxAmount;
                                    orderTaxRow[OrderDetailTaxTable.FLD_CREATE_USER_ID] = UserID;
                                    orderTaxRow[OrderDetailTaxTable.FLD_ORDER_DETAIL_ID] = ordDetailRow[OrderDetailTable.FLD_PKID];
                                }
                            }
                        }
                        else
                        {//Clean Orphans
                            int orderDetailID = 0;
                            if (!ordDetailRow.IsNull(dTblOrderDetailTax.Columns[OrderDetailTable.FLD_PKID], DataRowVersion.Original))
                                orderDetailID = Convert.ToInt32(ordDetailRow[dTblOrderDetailTax.Columns[OrderDetailTable.FLD_PKID], DataRowVersion.Original]);
                            if (orderDetailID > 0)
                            {
                                DataView dv = new DataView(dTblOrderDetailTax);
                                int iIndex = -1;
                                dv.Sort = OrderDetailTaxTable.FLD_ORDER_DETAIL_ID;
                                iIndex = dv.Find(orderDetailID);
                                if (iIndex != -1)
                                {
                                    dv[iIndex].Delete();
                                }
                            }
                        }
                    }
                }
                else //When Tax Exempted
                {
                    foreach (DataRow ordDetailRow in dTblOrderDetail.Rows)
                    {
                        ordRow[OrderDetailTable.FLD_TAX_RATE] = 0;
                    }
                }
                ordRow[OrderHeaderTable.FLD_TAX_RATE] = 0;
            }

            return taxRate;

        }
        public void CalculateOrder(OrderData dtsOrder)
        {
            //Get the Tax Rate
            if (dtsOrder.OrderDetail.Rows.Count > 0)
            {
                int totalQty = 0;
                decimal totalAmt = 0;
                decimal totalAmtAdj = 0;
                decimal totalTaxAmt = 0;
                decimal totalTaxRate = 0;
                decimal totalShipFees = 0;

                OrderHeaderTable dTblOrderHeader = dtsOrder.OrderHeader;
                OrderDetailTable dTblOrderDetail = dtsOrder.OrderDetail;
                OrderDetailTaxTable dTblOrderDetailTax = dtsOrder.OrderDetailTax;

                DataRow ordRow = dTblOrderHeader.Rows[0];
                int iCount = 0;
                foreach (DataRow ordDetailRow in dTblOrderDetail.Rows)
                {
                    if (ordDetailRow.RowState != DataRowState.Deleted)
                    {
                        #region for each detail row

                        #region Get item quantity

                        int qty = 0;
                        if (ordDetailRow[OrderDetailTable.FLD_QUANTITY] != DBNull.Value)
                        {
                            qty = Convert.ToInt32(ordDetailRow[OrderDetailTable.FLD_QUANTITY]);
                            totalQty = totalQty + qty;
                        }

                        #endregion

                        if (qty > 0)
                        {
                            #region Get total amount

                            if (ordDetailRow[OrderDetailTable.FLD_AMOUNT] != DBNull.Value)
                            {
                                decimal amount = Convert.ToDecimal(ordDetailRow[OrderDetailTable.FLD_AMOUNT]);
                                totalAmt = totalAmt + amount;
                            }

                            #endregion

                            #region Get total adjustment amount

                            if (ordDetailRow[OrderDetailTable.FLD_ADJUSTMENT_AMOUNT] != DBNull.Value)
                            {
                                decimal adjAmount = Convert.ToDecimal(ordDetailRow[OrderDetailTable.FLD_ADJUSTMENT_AMOUNT]);
                                totalAmtAdj = totalAmtAdj + adjAmount;
                            }

                            #endregion

                            //Just do it one time
                            if ((iCount == 0))
                            {
                                DataView dvTax = new DataView(dTblOrderDetailTax);

                                //strinf Base
                                dvTax.RowFilter = OrderDetailTaxTable.FLD_ORDER_DETAIL_ID + " = " + ordDetailRow[OrderDetailTable.FLD_PKID].ToString();

                                //Look for each level of tax
                                foreach (DataRowView taxDVRow in dvTax)
                                {
                                    if (!taxDVRow.Row.IsNull(OrderDetailTaxTable.FLD_TAX_RATE))
                                    {
                                        decimal taxRate = Convert.ToDecimal(taxDVRow[OrderDetailTaxTable.FLD_TAX_RATE]);
                                        totalTaxRate = totalTaxRate + taxRate;
                                    }
                                }
                                iCount = 1;
                            }
                        }

                        #endregion
                    }
                }

                //foreach(DataRow ordDetailTaxRow in dTblOrderDetailTax.Rows)
                //{
                //    if (ordDetailTaxRow.RowState != DataRowState.Deleted)
                //    {
                //        if(ordDetailTaxRow[OrderDetailTaxTable.FLD_AMOUNT] != DBNull.Value)
                //        {
                //            decimal taxAmount = Convert.ToDecimal(ordDetailTaxRow[OrderDetailTaxTable.FLD_AMOUNT]);
                //            totalTaxAmt = totalTaxAmt + taxAmount;
                //        }
                //    }
                //}

                totalTaxAmt = (totalTaxRate * totalAmt);

                //REASSIGNATION
                ordRow[OrderHeaderTable.FLD_TOTAL_QTY] = totalQty;
                ordRow[OrderHeaderTable.FLD_TOTAL_AMOUNT] = totalAmt;
                ordRow[OrderHeaderTable.FLD_TOTAL_ADJ_AMOUNT] = totalAmtAdj;
                //ordRow[OrderHeaderTable.FLD_TOTAL_TAX_AMT] = totalTaxAmt;
                ordRow[OrderHeaderTable.FLD_TAX_RATE] = totalTaxRate;

                if (dtsOrder.ShipmentGroup.Rows.Count > 0)
                {
                    DataRow shipRow = dtsOrder.ShipmentGroup.Rows[0];
                    if (!shipRow.IsNull(ShipmentGroupTable.FLD_SHIPPING_CHARGES))
                        totalShipFees = Convert.ToDecimal(shipRow[ShipmentGroupTable.FLD_SHIPPING_CHARGES]);
                }
                ordRow[OrderHeaderTable.FLD_TOTAL_SHIP_FEES] = totalShipFees;


            }
        }

        public bool IsOrderContainsPEProduct(OrderData dts)
        {
            bool isOrderContainsPEProduct = false;
            OrderDetailTable tbl = dts.OrderDetail;
            if (tbl.Rows.Count > 0)
            {
                DataView dv = new DataView(tbl);
                dv.RowFilter = OrderDetailTable.FLD_CATALOG_ITEM_CODE + " LIKE '*PE*'";
                //If the data contains PE Product
                isOrderContainsPEProduct = (dv.Count > 0);
            }

            return isOrderContainsPEProduct;
        }
        public void SetExpeditedFreightChargeRequirement(OrderData dtsOrder, int RoleID)
        {
            DataRow shipRow = dtsOrder.ShipmentGroup.Rows[0];

            if (dtsOrder.OrderException.IsContainExceptionType((int)Common.BusinessExceptionType.Expedited_Freight_Charges))
            {
                if (RoleID == AuthSystem.ROLE_FM)
                {

                    if (shipRow.IsNull(ShipmentGroupTable.FLD_SHIPPING_EXPEDITED_FREIGHT_CHARGES_PAYMENT_ASSIGNMENT_TYPE_ID))
                    {
                        //Put the FSM as default				
                        shipRow[ShipmentGroupTable.FLD_SHIPPING_EXPEDITED_FREIGHT_CHARGES_PAYMENT_ASSIGNMENT_TYPE_ID] = PaymentAssignmentType.PAY_BY_FSM;
                        shipRow[ShipmentGroupTable.FLD_SHIPPING_EXPEDITED_FREIGHT_CHARGES_PAYMENT_ASSIGNMENT_TYPE_NAME] = "Pay by FSM";
                    }
                }

            }
            else
            {
                //Reset the value if no exception of this type
                if (!shipRow.IsNull(ShipmentGroupTable.FLD_SHIPPING_EXPEDITED_FREIGHT_CHARGES_PAYMENT_ASSIGNMENT_TYPE_ID))
                {
                    shipRow[ShipmentGroupTable.FLD_SHIPPING_EXPEDITED_FREIGHT_CHARGES_PAYMENT_ASSIGNMENT_TYPE_ID] = PaymentAssignmentType.NONE;
                    shipRow[ShipmentGroupTable.FLD_SHIPPING_EXPEDITED_FREIGHT_CHARGES_PAYMENT_ASSIGNMENT_TYPE_NAME] = "None";
                }
            }

        }
        public void SetDefaultWarehouse(OrderData dts)
        {
            try
            {
                int OrderID = 0;
                OrderID = Convert.ToInt32(dts.OrderHeader.Rows[0][OrderHeaderTable.FLD_PKID]);
                //Get the Default Warehouse associated to the shipping Zip Code
                PostalAddressSystem postSys = new PostalAddressSystem();
                DataRow postRow = postSys.FindRow(dts.OrderPostalAddress, EntityType.TYPE_ORDER_SHIPPING, OrderID, PostalAddressType.TYPE_SHIPPING);
                if (postRow != null)
                {
                    string ZipCode = postRow[PostalAddressEntityTable.FLD_ZIP].ToString().Trim();

                    if (ZipCode.Length > 0)
                    {
                        WarehouseSystem wareSys = new WarehouseSystem();
                        WarehouseTable dTblWare = new WarehouseTable();
                        dTblWare = wareSys.SelectDefaultWarehouseByZipCode(ZipCode);
                        if (dTblWare.Rows.Count > 0)
                        {
                            DataRow wareRow = dTblWare.Rows[0];
                            int WareID = Convert.ToInt32(wareRow[WarehouseTable.FLD_PKID]);
                            int FulfWareID = Convert.ToInt32(wareRow[WarehouseTable.FLD_FULF_WAREHOUSE_ID]);
                            if (dts.ShipmentGroup.Rows.Count > 0)
                            {
                                CommonSystem comSys = new CommonSystem();
                                DataRow shipRow = dts.ShipmentGroup.Rows[0];
                                comSys.UpdateRow(shipRow, ShipmentGroupTable.FLD_DELIVERY_WAREHOUSE_ID, WareID.ToString());
                                comSys.UpdateRow(shipRow, ShipmentGroupTable.FLD_DELIVERY_FULF_WAREHOUSE_ID, FulfWareID.ToString());
                                comSys.UpdateRow(shipRow, ShipmentGroupTable.FLD_DELIVERY_WAREHOUSE_NAME, wareRow[WarehouseTable.FLD_NAME].ToString());
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
        public void FetchByCampaingAndForm(int CampaignID, int FormID)
        {
            orderHeader = objDataAccess.SelectAllByCampaignAndForm(CampaignID, FormID);
        }
        public bool HasOrder
        {
            get
            {
                if (orderHeader != null)
                {
                    if (orderHeader.Rows.Count > 0)
                        return true;
                    else
                        return false;
                }
                else
                {
                    //throw new NullReferenceException(ERR01);
                    return false;
                }
            }
        }
        public decimal ProfitRate
        {
            get
            {
                if (orderHeader != null)
                {
                    decimal profitRate = -1;
                    foreach (DataRow r in orderHeader.Rows)
                    {
                        if (!r.IsNull(dataDef.FLD_PROFIT_RATE))
                        {
                            profitRate = Convert.ToDecimal(r[dataDef.FLD_PROFIT_RATE].ToString());
                            break;
                        }
                    }

                    return profitRate;

                }
                else
                {
                    //throw new NullReferenceException(ERR01);
                    return -1;
                }
            }
        }

        #endregion

    }
}
