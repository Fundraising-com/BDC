using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Dynamic;
using System.Diagnostics;
using System.Transactions;

using AccountFinderService = QSPForm.Business.com.qsp.ws.AccountFinderService;
using QSPForm.Common.DataDef;
using dataDef = QSPForm.Common.DataDef.AccountTable;
using dataAccessRef = QSPForm.Data.Account;
using QSPForm.Business.Properties;
using QSPForm.Common;

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
    /// This class contains the business rules that are used for an Account
	/// </summary>
	public class AccountSystem : BusinessSystem
	{

        #region Refactored code

        // This method will be deprecated, you should use the Search method instead
        public IEnumerable<LinqEntity.AccountList> SelectAll_Search(SearchSettings settings, ref int count) 
        {

            IEnumerable<LinqEntity.AccountList> result;

            LinqContext.OrderExpressDataContext db = new LinqContext.OrderExpressDataContext();
            //using (TransactionScope t = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted }))
            //{

                #region Get base query

                var baseQuery = from c in db.Campaigns
                                join paa in db.PostalAddressAccounts on c.Account.AccountId equals paa.AccountId
                                join fsm in db.FieldSalesManagers on c.Account.FmId equals fsm.FmId
                                join programtype in db.ProgramTypes on c.ProgramTypeId equals programtype.ProgramTypeId
                                where c.IsDeleted == false
                                    && c.Account.IsDeleted == false
                                    && c.Account.Organization.IsDeleted == false
                                    && paa.IsDeleted == false
                                    && paa.PostalAddressTypeId == 1
                                    && c.Account.Organization.BusinessDivisionId == 1
                                group c.FiscalYear by new
                                {
                                    c.Account.AccountId,
                                    c.Account.FulfAccountId,
                                    c.Account.AccountName,
                                    c.Account.AccountStatus.AccountStatusId,
                                    c.Account.AccountStatus.StatusCategoryId,
                                    c.Account.AccountStatus.ColorCode,
                                    c.Account.AccountStatus.ShortDescription,
                                    fsm.FmId,
                                    FmFirstName = fsm.FirstName,
                                    FmLastName = fsm.LastName,
                                    c.ProgramTypeId,
                                    programtype.ProgramTypeName,
                                    c.Account.CreateDate,
                                    c.Account.CreateUserId,
                                    paa.PostalAddress.City,
                                    paa.PostalAddress.Zip,
                                    paa.PostalAddress.SubdivisionCode,
                                } into groupedData
                                select new
                                {
                                    AccountId = groupedData.Key.AccountId,
                                    FulfAccountId = groupedData.Key.FulfAccountId,
                                    AccountName = groupedData.Key.AccountName,
                                    AccountStatusId = groupedData.Key.AccountStatusId,
                                    AccountStatusCategoryId = groupedData.Key.StatusCategoryId,
                                    AccountStatusColorCode = groupedData.Key.ColorCode,
                                    AccountStatusShortDescription = groupedData.Key.ShortDescription,
                                    ProgramTypeId = groupedData.Key.ProgramTypeId,
                                    ProgramTypeName = groupedData.Key.ProgramTypeName,
                                    CreateDate = groupedData.Key.CreateDate,
                                    CreateUserId = groupedData.Key.CreateUserId,
                                    AddressCity = groupedData.Key.City,
                                    AddressZip = groupedData.Key.Zip,
                                    AddressSubdivisionCode = groupedData.Key.SubdivisionCode,
                                    FsmId = groupedData.Key.FmId,
                                    FsmFirstName = groupedData.Key.FmFirstName,
                                    FsmLastName = groupedData.Key.FmLastName,
                                    FiscalYear = groupedData.Max()
                                };

                #endregion

                #region Add search criteria

                if (settings.AccountId.Length > 0)
                {
                    baseQuery = baseQuery.Where(a => a.AccountId.ToString().Contains(settings.AccountId));
                }
                if (settings.EdsAccountId.Length > 0)
                {
                    baseQuery = baseQuery.Where(a => a.FulfAccountId.ToString().Contains(settings.EdsAccountId));
                }
                if (settings.AccountName.Length > 0)
                {
                    baseQuery = baseQuery.Where(a => a.AccountName.Contains(settings.AccountName));
                }
                if (settings.FirstChar.Length > 0)
                {
                    baseQuery = baseQuery.Where(a => a.AccountName.StartsWith(settings.FirstChar));
                }
                if (settings.StatusCategoryId.HasValue)
                {
                    baseQuery = baseQuery.Where(a => a.AccountStatusCategoryId == settings.StatusCategoryId);
                }
                if (settings.FsmName.Length > 0)
                {
                    baseQuery = baseQuery.Where(a => (a.FsmFirstName + " " + a.FsmLastName).Contains(settings.FsmName));
                }
                if (settings.ProgramTypeId.HasValue)
                {
                    baseQuery = baseQuery.Where(a => a.ProgramTypeId == settings.ProgramTypeId);
                }
                if (settings.City.Length > 0)
                {
                    baseQuery = baseQuery.Where(a => a.AddressCity.Contains(settings.City));
                }
                if (settings.ZipCode.Length > 0)
                {
                    baseQuery = baseQuery.Where(a => a.AddressZip.Contains(settings.ZipCode));
                }
                if (settings.SubdivisionCode.Length > 0)
                {
                    baseQuery = baseQuery.Where(a => a.AddressSubdivisionCode == settings.SubdivisionCode);
                }

                #endregion

                #region Handle FSM hierarchy

                if (settings.DisplayMode == DisplayMode.All)
                {
                    if (settings.FsmId.Length > 0)
                    {
                        baseQuery = baseQuery.Where(a => a.FsmId.Contains(settings.FsmId));
                    }
                }
                else if (settings.DisplayMode == DisplayMode.Current)
                {
                    baseQuery = baseQuery.Where(a => a.FsmId == settings.FsmId);
                }
                else if (settings.DisplayMode == DisplayMode.ChildrenOnly)
                {
                    LinqContext.QSPCommonDataContext dbCommon = new LinqContext.QSPCommonDataContext();
                    List<string> FmTree = (from u in dbCommon.fnc_FMHierarchyList_FMID(settings.FsmId) select u.FMNumber).ToList();
                    baseQuery = baseQuery.Where(a => FmTree.Contains(a.FsmId));
                    baseQuery = baseQuery.Where(a => a.FsmId != settings.FsmId);
                }
                else if (settings.DisplayMode == DisplayMode.CurrentAndChildren)
                {
                    LinqContext.QSPCommonDataContext dbCommon = new LinqContext.QSPCommonDataContext();
                    List<string> FmTree = (from u in dbCommon.fnc_FMHierarchyList_FMID(settings.FsmId) select u.FMNumber).ToList();
                    baseQuery = baseQuery.Where(a => FmTree.Contains(a.FsmId));
                }

                #endregion

                #region Left join to get create user

                var finalQuery = from bq in baseQuery
                                 join user in db.Users on bq.CreateUserId equals user.UserId into temp
                                 from userData in temp.DefaultIfEmpty()
                                 select new
                                 {
                                     CreateUserFirstName = (userData.FirstName == null) ? "NA" : userData.FirstName,
                                     CreateUserLastName = (userData.LastName == null) ? "NA" : userData.LastName,
                                     AccountId = bq.AccountId,
                                     FulfAccountId = bq.FulfAccountId,
                                     AccountName = bq.AccountName,
                                     AccountStatusId = bq.AccountStatusId,
                                     AccountStatusCategoryId = bq.AccountStatusCategoryId,
                                     AccountStatusColorCode = bq.AccountStatusColorCode,
                                     AccountStatusShortDescription = bq.AccountStatusShortDescription,
                                     ProgramTypeId = bq.ProgramTypeId,
                                     ProgramTypeName = bq.ProgramTypeName,
                                     CreateDate = bq.CreateDate,
                                     CreateUserId = bq.CreateUserId,
                                     AddressCity = bq.AddressCity,
                                     AddressZip = bq.AddressZip,
                                     AddressSubdivisionCode = bq.AddressSubdivisionCode,
                                     FsmId = bq.FsmId,
                                     FsmFirstName = bq.FsmFirstName,
                                     FsmLastName = bq.FsmLastName,
                                     FiscalYear = bq.FiscalYear
                                 };

                #endregion

                #region Sorting

                if (settings.Sort.Length > 0)
                {
                    finalQuery = finalQuery.OrderBy(settings.Sort);
                }

                #endregion

                #region Get count

                count = finalQuery.Count();

                #endregion

                #region Get final result

                Properties.Settings localsettings = new Properties.Settings();
                if (localsettings.UseDatabasePaging)
                {
                    finalQuery = finalQuery.Skip(settings.PageIndex * settings.PageSize).Take(settings.PageSize);
                    result = finalQuery.AsEnumerable().Select(a => new LinqEntity.AccountList(a.AccountId,
                                                                                    a.AccountName,
                                                                                    a.FsmId,
                                                                                    a.FulfAccountId,
                                                                                    a.AccountStatusId,
                                                                                    a.AccountStatusCategoryId,
                                                                                    a.AccountStatusColorCode,
                                                                                    a.AccountStatusShortDescription,
                                                                                    a.FsmFirstName,
                                                                                    a.FsmLastName,
                                                                                    a.FiscalYear,
                                                                                    a.ProgramTypeId,
                                                                                    a.ProgramTypeName,
                                                                                    a.AddressCity,
                                                                                    a.AddressZip,
                                                                                    a.AddressSubdivisionCode,
                                                                                    a.CreateDate,
                                                                                    a.CreateUserFirstName,
                                                                                    a.CreateUserLastName));
                }
                else
                {
                    finalQuery = finalQuery.Take((settings.PageIndex + 1) * settings.PageSize);
                    result = finalQuery.AsEnumerable().Skip(settings.PageIndex * settings.PageSize).Select(a => new LinqEntity.AccountList(a.AccountId,
                                                                                                                                    a.AccountName,
                                                                                                                                    a.FsmId,
                                                                                                                                    a.FulfAccountId,
                                                                                                                                    a.AccountStatusId,
                                                                                                                                    a.AccountStatusCategoryId,
                                                                                                                                    a.AccountStatusColorCode,
                                                                                                                                    a.AccountStatusShortDescription,
                                                                                                                                    a.FsmFirstName,
                                                                                                                                    a.FsmLastName,
                                                                                                                                    a.FiscalYear,
                                                                                                                                    a.ProgramTypeId,
                                                                                                                                    a.ProgramTypeName,
                                                                                                                                    a.AddressCity,
                                                                                                                                    a.AddressZip,
                                                                                                                                    a.AddressSubdivisionCode,
                                                                                                                                    a.CreateDate,
                                                                                                                                    a.CreateUserFirstName,
                                                                                                                                    a.CreateUserLastName));
                }

                #endregion
            //}

            return result;
        }

        private int CurrentFiscalYear {
            get {
                return DateTime.Now.AddMonths(6).Year;
            }
        }

        #endregion

        #region Version 2 code

        public List<AccountSearchItem> Search(AccountSearchParameters parameters)
        {
            List<AccountSearchItem> result = new List<AccountSearchItem>();

            LinqContext.OrderExpressDataContext db = new LinqContext.OrderExpressDataContext();

            #region Base query

            var query = from c in db.Campaigns
                        join paa in db.PostalAddressAccounts on c.Account.AccountId equals paa.AccountId
                        join fsm in db.FieldSalesManagers on c.Account.FmId equals fsm.FmId
                        join programtype in db.ProgramTypes on c.ProgramTypeId equals programtype.ProgramTypeId
                        where c.IsDeleted == false
                            && c.Account.IsDeleted == false
                            //&& fsm.Deleted == false //it is commented because at line #467 it is already handled
                            && c.Account.Organization.IsDeleted == false
                            && paa.IsDeleted == false
                            && c.ProgramType.Enabled == true
                            && paa.PostalAddressTypeId == 1
                            && (c.Account.Organization.BusinessDivisionId == (int)BusinessDivisionEnum.US
                                || c.Account.Organization.BusinessDivisionId == (int)BusinessDivisionEnum.EFR)
                        group c.FiscalYear by new
                        {
                            c.Account.AccountId,
                            c.Account.FulfAccountId,
                            c.Account.AccountName,
                            c.Account.AccountStatus.AccountStatusId,
                            c.Account.AccountStatus.StatusCategoryId,
                            c.Account.AccountStatus.ColorCode,
                            c.Account.AccountStatus.ShortDescription,
                            FmIsDeleted = fsm.Deleted,
                            fsm.FmId,
                            FmFirstName = fsm.FirstName,
                            FmLastName = fsm.LastName,
                            c.ProgramTypeId,
                            programtype.ProgramTypeName,
                            c.Account.CreateDate,
                            c.Account.CreateUserId,
                            paa.PostalAddress.City,
                            paa.PostalAddress.Zip,
                            paa.PostalAddress.SubdivisionCode, 
                            c.Account.OrganizationId
                        } into groupedData
                        select new
                        {
                            AccountId = groupedData.Key.AccountId,
                            FulfAccountId = groupedData.Key.FulfAccountId,
                            AccountName = groupedData.Key.AccountName,
                            AccountStatusId = groupedData.Key.AccountStatusId,
                            AccountStatusCategoryId = groupedData.Key.StatusCategoryId,
                            AccountStatusColorCode = groupedData.Key.ColorCode,
                            AccountStatusShortDescription = groupedData.Key.ShortDescription,
                            ProgramTypeId = groupedData.Key.ProgramTypeId,
                            ProgramTypeName = groupedData.Key.ProgramTypeName,
                            CreateDate = groupedData.Key.CreateDate,
                            CreateUserId = groupedData.Key.CreateUserId,
                            AddressCity = groupedData.Key.City,
                            AddressZip = groupedData.Key.Zip,
                            AddressSubdivisionCode = groupedData.Key.SubdivisionCode,
                            FsmIsDeleted = groupedData.Key.FmIsDeleted, 
                            FsmId = groupedData.Key.FmId,
                            FsmFirstName = groupedData.Key.FmFirstName,
                            FsmLastName = groupedData.Key.FmLastName,
                            FiscalYear = groupedData.Max(), 
                            OrganizationId = groupedData.Key.OrganizationId
                        };

            #endregion

            #region Filters

            if (parameters.SearchValue.Length > 0)
            {
                if (parameters.SearchField == AccountSearchFieldEnum.Any)
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
                                select q;
                    }
                    else
                    {
                        query = from q in query
                                where q.AddressCity.Contains(parameters.SearchValue)
                                || q.AccountName.Contains(parameters.SearchValue)
                                || q.AddressZip.Contains(parameters.SearchValue)
                                select q;
                    }
                }
                else if (parameters.SearchField == AccountSearchFieldEnum.City)
                {
                    query = from q in query
                            where q.AddressCity.Contains(parameters.SearchValue)
                            select q;
                }
                else if (parameters.SearchField == AccountSearchFieldEnum.Name)
                {
                    query = from q in query
                            where q.AccountName.Contains(parameters.SearchValue)
                            select q;
                }
                else if (parameters.SearchField == AccountSearchFieldEnum.NameBeginingWith)
                {
                    query = from q in query
                            where q.AccountName.StartsWith(parameters.SearchValue)
                            select q;
                }
                else if (parameters.SearchField == AccountSearchFieldEnum.QSPAccountId)
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
                else if (parameters.SearchField == AccountSearchFieldEnum.EDSAccountId)
                {
                    int number = 0;
                    bool isNumber = int.TryParse(parameters.SearchValue, out number);

                    if (isNumber)
                    {
                        query = from q in query
                                where q.FulfAccountId == number
                                select q;
                    }
                }
                else if (parameters.SearchField == AccountSearchFieldEnum.ZipCode)
                {
                    query = from q in query
                            where q.AddressZip.Contains(parameters.SearchValue)
                            select q;
                }
                else if (parameters.SearchField == AccountSearchFieldEnum.QSPOrganizationId)
                {
                    int number = 0;
                    bool isNumber = int.TryParse(parameters.SearchValue, out number);

                    if (isNumber)
                    {
                        query = from q in query
                                where q.OrganizationId == number
                                select q;
                    }
                }
            }

            if (parameters.ProgramTypeId.HasValue)
            {
                query = from q in query
                        where q.ProgramTypeId == parameters.ProgramTypeId.Value
                        select q;
            }

            if (parameters.StatusCategoryId.HasValue)
            {
                query = from q in query
                        where q.AccountStatusCategoryId == parameters.StatusCategoryId.Value
                        select q;
            }

            if (parameters.SubdivisionCode.Length > 0)
            {
                query = from q in query
                        where q.AddressSubdivisionCode == parameters.SubdivisionCode
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

            #region Left join to get create user

            var finalQuery = from q in query
                             join user in db.Users on q.CreateUserId equals user.UserId into temp
                             from userData in temp.DefaultIfEmpty()
                             select new
                             {
                                 CreateUserFirstName = (userData.FirstName == null) ? "" : userData.FirstName,
                                 CreateUserLastName = (userData.LastName == null) ? "" : userData.LastName,
                                 AccountId = q.AccountId,
                                 FulfAccountId = q.FulfAccountId,
                                 AccountName = q.AccountName,
                                 AccountStatusId = q.AccountStatusId,
                                 AccountStatusCategoryId = q.AccountStatusCategoryId,
                                 AccountStatusColorCode = q.AccountStatusColorCode,
                                 AccountStatusShortDescription = q.AccountStatusShortDescription,
                                 ProgramTypeId = q.ProgramTypeId,
                                 ProgramTypeName = q.ProgramTypeName,
                                 CreateDate = q.CreateDate,
                                 CreateUserId = q.CreateUserId,
                                 AddressCity = q.AddressCity,
                                 AddressZip = q.AddressZip,
                                 AddressSubdivisionCode = q.AddressSubdivisionCode,
                                 FsmId = q.FsmId,
                                 FsmFirstName = q.FsmFirstName,
                                 FsmLastName = q.FsmLastName,
                                 FiscalYear = q.FiscalYear
                             };

            #endregion

            #region Sort

            finalQuery = finalQuery.OrderBy(parameters.SortField);

            #endregion`

            //using (TransactionScope t = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted }))
            //{
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
                            select new AccountSearchItem
                            {
                                AccountId = q.AccountId,
                                AccountName = q.AccountName,
                                EDSAccountId = q.FulfAccountId,
                                FiscalYear = q.FiscalYear,
                                ProgramTypeId = q.ProgramTypeId,
                                ProgramTypeName = q.ProgramTypeName,
                                StatusCategoryId = q.AccountStatusCategoryId,
                                StatusColorCode = q.AccountStatusColorCode,
                                StatusId = q.AccountStatusId,
                                StatusShortDescription = q.AccountStatusShortDescription,
                                FmId = q.FsmId,
                                FmFirstName = q.FsmFirstName,
                                FmLastName = q.FsmLastName,
                                Address1 = "",
                                City = q.AddressCity,
                                SubdivisionCode = q.AddressSubdivisionCode,
                                Zip = q.AddressZip,
                                CreateDate = q.CreateDate,
                                CreatorFirstName = q.CreateUserFirstName,
                                CreatorLastName = q.CreateUserLastName
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
                                new AccountSearchItem
                                {
                                    AccountId = q.AccountId,
                                    AccountName = q.AccountName,
                                    EDSAccountId = q.FulfAccountId,
                                    FiscalYear = q.FiscalYear,
                                    ProgramTypeId = q.ProgramTypeId,
                                    ProgramTypeName = q.ProgramTypeName,
                                    StatusCategoryId = q.AccountStatusCategoryId,
                                    StatusColorCode = q.AccountStatusColorCode,
                                    StatusId = q.AccountStatusId,
                                    StatusShortDescription = q.AccountStatusShortDescription,
                                    FmId = q.FsmId,
                                    FmFirstName = q.FsmFirstName,
                                    FmLastName = q.FsmLastName,
                                    Address1 = "",
                                    City = q.AddressCity,
                                    SubdivisionCode = q.AddressSubdivisionCode,
                                    Zip = q.AddressZip,
                                    CreateDate = q.CreateDate,
                                    CreatorFirstName = q.CreateUserFirstName,
                                    CreatorLastName = q.CreateUserLastName
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
                        select new AccountSearchItem
                        {
                            AccountId = q.AccountId,
                            AccountName = q.AccountName,
                            EDSAccountId = q.FulfAccountId,
                            FiscalYear = q.FiscalYear,
                            ProgramTypeId = q.ProgramTypeId,
                            ProgramTypeName = q.ProgramTypeName,
                            StatusCategoryId = q.AccountStatusCategoryId,
                            StatusColorCode = q.AccountStatusColorCode,
                            StatusId = q.AccountStatusId,
                            StatusShortDescription = q.AccountStatusShortDescription,
                            FmId = q.FsmId,
                            FmFirstName = q.FsmFirstName,
                            FmLastName = q.FsmLastName,
                            Address1 = "",
                            City = q.AddressCity,
                            SubdivisionCode = q.AddressSubdivisionCode,
                            Zip = q.AddressZip,
                            CreateDate = q.CreateDate,
                            CreatorFirstName = q.CreateUserFirstName,
                            CreatorLastName = q.CreateUserLastName
                        }
                        ).ToList();

                    #endregion
                }
            //}
            return result;
        }
        public int SearchTotalRowCount(AccountSearchParameters parameters)
        {
            int result = 0;

            LinqContext.OrderExpressDataContext db = new LinqContext.OrderExpressDataContext();

            #region Base query

            var query = from c in db.Campaigns
                        join paa in db.PostalAddressAccounts on c.Account.AccountId equals paa.AccountId
                        join fsm in db.FieldSalesManagers on c.Account.FmId equals fsm.FmId
                        join programtype in db.ProgramTypes on c.ProgramTypeId equals programtype.ProgramTypeId
                        where c.IsDeleted == false
                            && c.Account.IsDeleted == false
                            && c.Account.Organization.IsDeleted == false
                            && paa.IsDeleted == false
                            && c.ProgramType.Enabled == true
                            && paa.PostalAddressTypeId == 1
                            && (c.Account.Organization.BusinessDivisionId == (int)BusinessDivisionEnum.US
                                || c.Account.Organization.BusinessDivisionId == (int)BusinessDivisionEnum.EFR)
                        group c.FiscalYear by new
                        {
                            c.Account.AccountId,
                            c.Account.FulfAccountId,
                            c.Account.AccountName,
                            c.Account.AccountStatus.AccountStatusId,
                            c.Account.AccountStatus.StatusCategoryId,
                            c.Account.AccountStatus.ColorCode,
                            c.Account.AccountStatus.ShortDescription,
                            FmIsDeleted = fsm.Deleted,
                            fsm.FmId,
                            FmFirstName = fsm.FirstName,
                            FmLastName = fsm.LastName,
                            c.ProgramTypeId,
                            programtype.ProgramTypeName,
                            c.Account.CreateDate,
                            c.Account.CreateUserId,
                            paa.PostalAddress.City,
                            paa.PostalAddress.Zip,
                            paa.PostalAddress.SubdivisionCode,
                            c.Account.OrganizationId
                        } into groupedData
                        select new
                        {
                            AccountId = groupedData.Key.AccountId,
                            FulfAccountId = groupedData.Key.FulfAccountId,
                            AccountName = groupedData.Key.AccountName,
                            AccountStatusId = groupedData.Key.AccountStatusId,
                            AccountStatusCategoryId = groupedData.Key.StatusCategoryId,
                            AccountStatusColorCode = groupedData.Key.ColorCode,
                            AccountStatusShortDescription = groupedData.Key.ShortDescription,
                            ProgramTypeId = groupedData.Key.ProgramTypeId,
                            ProgramTypeName = groupedData.Key.ProgramTypeName,
                            CreateDate = groupedData.Key.CreateDate,
                            CreateUserId = groupedData.Key.CreateUserId,
                            AddressCity = groupedData.Key.City,
                            AddressZip = groupedData.Key.Zip,
                            AddressSubdivisionCode = groupedData.Key.SubdivisionCode,
                            FsmIsDeleted = groupedData.Key.FmIsDeleted,
                            FsmId = groupedData.Key.FmId,
                            FsmFirstName = groupedData.Key.FmFirstName,
                            FsmLastName = groupedData.Key.FmLastName,
                            FiscalYear = groupedData.Max(),
                            OrganizationId = groupedData.Key.OrganizationId
                        };

            #endregion

            #region Filters

            if (parameters.SearchValue.Length > 0)
            {
                if (parameters.SearchField == AccountSearchFieldEnum.Any)
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
                                select q;
                    }
                    else
                    {
                        query = from q in query
                                where q.AddressCity.Contains(parameters.SearchValue)
                                || q.AccountName.Contains(parameters.SearchValue)
                                || q.AddressZip.Contains(parameters.SearchValue)
                                select q;
                    }
                }
                else if (parameters.SearchField == AccountSearchFieldEnum.City)
                {
                    query = from q in query
                            where q.AddressCity.Contains(parameters.SearchValue)
                            select q;
                }
                else if (parameters.SearchField == AccountSearchFieldEnum.Name)
                {
                    query = from q in query
                            where q.AccountName.Contains(parameters.SearchValue)
                            select q;
                }
                else if (parameters.SearchField == AccountSearchFieldEnum.NameBeginingWith)
                {
                    query = from q in query
                            where q.AccountName.StartsWith(parameters.SearchValue)
                            select q;
                }
                else if (parameters.SearchField == AccountSearchFieldEnum.QSPAccountId)
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
                else if (parameters.SearchField == AccountSearchFieldEnum.EDSAccountId)
                {
                    int number = 0;
                    bool isNumber = int.TryParse(parameters.SearchValue, out number);

                    if (isNumber)
                    {
                        query = from q in query
                                where q.FulfAccountId == number
                                select q;
                    }
                }
                else if (parameters.SearchField == AccountSearchFieldEnum.ZipCode)
                {
                    query = from q in query
                            where q.AddressZip.Contains(parameters.SearchValue)
                            select q;
                }
                else if (parameters.SearchField == AccountSearchFieldEnum.QSPOrganizationId)
                {
                    int number = 0;
                    bool isNumber = int.TryParse(parameters.SearchValue, out number);

                    if (isNumber)
                    {
                        query = from q in query
                                where q.OrganizationId == number
                                select q;
                    }
                }
            }

            if (parameters.ProgramTypeId.HasValue)
            {
                query = from q in query
                        where q.ProgramTypeId == parameters.ProgramTypeId.Value
                        select q;
            }

            if (parameters.StatusCategoryId.HasValue)
            {
                query = from q in query
                        where q.AccountStatusCategoryId == parameters.StatusCategoryId.Value
                        select q;
            }

            if (parameters.SubdivisionCode.Length > 0)
            {
                query = from q in query
                        where q.AddressSubdivisionCode == parameters.SubdivisionCode
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

        public EntityData.AccountData GetAccount(int accountId)
        {
            return this.GetAccount(accountId, false);
        }
        public EntityData.AccountData GetAccount(int accountId, bool loadChildrenObjects)
        {
            EntityData.AccountData result = new EntityData.AccountData();

            LinqContext.OrderExpressDataContext db = new LinqContext.OrderExpressDataContext();

            #region Load account data

            LinqEntity.Account account =
                (from a in db.Accounts
                 where a.AccountId == accountId
                 select a
                 ).SingleOrDefault();


            result.Id = account.AccountId;
            result.EdsId = account.FulfAccountId;
            result.Name = account.AccountName;
            result.TaxExemptionNumber = account.TaxExemptionNumber;
            result.TaxExemptionExpirationDate = account.TaxExemptionExpirationDate;
            result.Comments = account.Comments;
            result.StatusId = account.AccountStatusId;
            result.FsmId = account.FmId;
            result.IsDeleted = account.IsDeleted;
            result.CreateDate = account.CreateDate;
            result.CreateUserId = account.CreateUserId;
            result.UpdateDate = account.UpdateDate;
            result.UpdateUserId = account.UpdateUserId;

            result.Organization = new EntityData.OrganizationData();
            result.Organization.Id = account.OrganizationId;

            result.AccountNotes = new List<EntityData.BusinessExceptionData>();

            #endregion

            if (loadChildrenObjects)
            {
                #region Load extended organization data

                result.StatusName = account.AccountStatus.AccountStatusName;
                result.StatusColor = account.AccountStatus.ColorCode;


                LinqEntity.FieldSalesManager fsm =
                    (from f in db.FieldSalesManagers
                     where f.FmId == account.FmId
                     select f
                     ).FirstOrDefault();

                if (fsm != null)
                {
                    result.FsmFirstName = fsm.FirstName;
                    result.FsmLastName = fsm.LastName;
                }


                LinqEntity.User createUser =
                    (from u in db.Users
                     where u.UserId == account.CreateUserId
                     select u
                     ).SingleOrDefault();

                if (createUser != null)
                {
                    result.CreateUserFirstName = createUser.FirstName;
                    result.CreateUserLastName = createUser.LastName;
                }


                if (account.UpdateUserId.HasValue)
                {
                    LinqEntity.User updateUser =
                        (from u in db.Users
                         where u.UserId == account.UpdateUserId.Value
                         select u
                         ).SingleOrDefault();

                    if (updateUser != null)
                    {
                        result.UpdateUserFirstName = updateUser.FirstName;
                        result.UpdateUserLastName = updateUser.LastName;
                    }
                }

                List<LinqEntity.AccountCollection> accountCollectionList =
                    (from ac in db.AccountCollections
                     where ac.AccountId == account.AccountId
                     select ac
                     ).ToList();

                if (accountCollectionList.Count > 0)
                {
                    result.CollectionAmouunt = accountCollectionList[0].AccountCollectionAmount;
                    result.CollectionDate = accountCollectionList[0].AccountCollectionDate;
                }


                #endregion

                #region Organization data

                result.Organization.Name = account.Organization.OrganizationName;

                result.Organization.Type = new EntityData.OrganizationTypeData();
                result.Organization.Type.Id = account.Organization.OrganizationTypeId;
                result.Organization.Type.Name = account.Organization.OrganizationType.OrganizationTypeName;
                result.Organization.Type.ARSTYP = account.Organization.OrganizationType.Arstyp;

                if (account.Organization.OrganizationLevelId.HasValue)
                {
                    result.Organization.Level = new EntityData.OrganizationLevelData();
                    result.Organization.Level.Id = account.Organization.OrganizationLevelId.Value;
                    result.Organization.Level.Name = account.Organization.OrganizationLevel.OrganizationLevelName;
                    result.Organization.Level.ARSLEV = account.Organization.OrganizationLevel.ARSLEV;
                }

                #endregion

                #region Campaign data

                result.LastCampaign = new EntityData.CampaignData();

                LinqEntity.Campaign lastCampaign =
                    (from c in db.Campaigns
                     where c.AccountId == account.AccountId
                        && c.IsDeleted == false
                     orderby c.CreateDate descending
                     select c
                     ).FirstOrDefault();

                if (lastCampaign != null)
                {
                    result.LastCampaign.Id = lastCampaign.CampaignId;
                    result.LastCampaign.FiscalYear = lastCampaign.FiscalYear;
                    result.LastCampaign.StartDate = lastCampaign.StartDate;
                    result.LastCampaign.EndDate = lastCampaign.EndDate;
                    result.LastCampaign.Enrollment = lastCampaign.Enrollment;
                    result.LastCampaign.GoalEstimatedGross = lastCampaign.GoalEstimatedGross;

                    result.LastCampaign.ProgramTypeId = lastCampaign.ProgramTypeId;
                    result.LastCampaign.ProgramTypeName = lastCampaign.ProgramType.ProgramTypeName;

                    result.LastCampaign.TradeClassId = lastCampaign.TradeClassId;
                    if (lastCampaign.TradeClassId.HasValue)
                    {
                        result.LastCampaign.TradeClassName = lastCampaign.TradeClass.TradeClassName;
                    }

                    result.LastCampaign.WarehouseId = lastCampaign.WarehouseId;
                    if (lastCampaign.WarehouseId.HasValue)
                    {
                        LinqEntity.Warehouse warehouse =
                            (from w in db.Warehouses
                             where w.WarehouseId == lastCampaign.WarehouseId.Value
                             select w
                             ).SingleOrDefault();

                        if (warehouse != null)
                        {
                            result.LastCampaign.WarehouseName = warehouse.WarehouseName;
                        }
                    }

                    LinqEntity.Order lastOrder =
                        (from o in db.Orders
                         where o.CampaignId == lastCampaign.CampaignId
                            && o.IsDeleted == false
                         orderby o.CreateDate descending
                         select o
                         ).FirstOrDefault();

                    if (lastOrder != null)
                    {
                        result.LastCampaign.LastOrderDate = lastOrder.CreateDate;

                        DateTime dateSpan = new DateTime(DateTime.Now.Ticks - lastOrder.CreateDate.Ticks);
                        int numberOfMonths = ((dateSpan.Year - DateTime.MinValue.Year) * 12) + dateSpan.Month;
                        result.LastCampaign.InactiveMonths = numberOfMonths.ToString();
                    }
                    else
                    {
                        result.LastCampaign.LastOrderDate = null;
                        result.LastCampaign.InactiveMonths = "";
                    }
                }

                #endregion

                #region Account notes

                result.AccountNotes = new List<EntityData.BusinessExceptionData>();

                EntityExceptionSystem entityExceptionSystem = new EntityExceptionSystem();
                List<LinqEntity.EntityException> entityExceptionList = entityExceptionSystem.GetBusinessExceptions(EntityTypeEnum.Account, account.AccountId);
                
                foreach (LinqEntity.EntityException entityException in entityExceptionList)
                {
                    EntityData.BusinessExceptionData newItem = new EntityData.BusinessExceptionData();

                    newItem.Id = entityException.EntityExceptionId;
                    newItem.EntityType = (EntityTypeEnum)entityException.EntityTypeId;
                    newItem.EntityId = entityException.EntityId;
                    newItem.ExceptionId = entityException.BusinessExceptionId;
                    if (entityException.ExceptionTypeId.HasValue)
                    {
                        newItem.ExceptionType = (ExceptionTypeEnum)entityException.ExceptionTypeId.Value;
                    }
                    newItem.Message = entityException.Message;
                    newItem.FeesAmount = entityException.FeesValueAmount;
                    newItem.IsApproved = entityException.IsApproved;
                    newItem.ApprovedById = entityException.ApproveUserId;
                    if (entityException.ApproveUserId.HasValue)
                    {
                        LinqEntity.User aproveUser =
                            (from u in db.Users
                             where u.UserId == entityException.ApproveUserId.Value
                             select u
                             ).SingleOrDefault();

                        newItem.ApprovedByFirstName = aproveUser.FirstName;
                        newItem.ApprovedByLastName = aproveUser.LastName;
                    }
                    newItem.CreateDate = entityException.CreateDate;

                    result.AccountNotes.Add(newItem);
                }

                #endregion

                #region Load shipping addresses

                LinqEntity.PostalAddress shippingAddress = (
                    from paa in account.PostalAddressAccounts
                    where paa.PostalAddressTypeId == (int)PostalAddressTypeEnum.Shipping
                        && paa.IsDeleted == false
                        && paa.PostalAddress.IsDeleted == 0
                    select paa.PostalAddress
                    ).SingleOrDefault();

                result.ShippingAddress = new EntityData.AddressData();
                result.ShippingAddress.Subdivision = new EntityData.SubdivisionData();
                result.ShippingAddress.Subdivision.Country = new EntityData.CountryData();

                if (shippingAddress != null)
                {
                    result.ShippingAddress.Address1 = shippingAddress.Address1;
                    result.ShippingAddress.Address2 = shippingAddress.Address2;
                    result.ShippingAddress.City = shippingAddress.City;
                    result.ShippingAddress.County = shippingAddress.County;
                    result.ShippingAddress.CreateDate = shippingAddress.CreateDate;
                    result.ShippingAddress.CreateUserId = shippingAddress.CreateUserId;
                    result.ShippingAddress.FirstName = shippingAddress.FirstName;
                    result.ShippingAddress.Id = shippingAddress.PostalAddressId;
                    result.ShippingAddress.IsDeleted = (shippingAddress.IsDeleted == 1);
                    result.ShippingAddress.IsResidentialArea = shippingAddress.IsResidentialArea ?? false;
                    result.ShippingAddress.LastName = shippingAddress.LastName;
                    result.ShippingAddress.Name = shippingAddress.Name;
                    result.ShippingAddress.UpdateDate = shippingAddress.UpdateDate;
                    result.ShippingAddress.UpdateUserId = shippingAddress.UpdateUserId;
                    result.ShippingAddress.Zip = shippingAddress.Zip;
                    result.ShippingAddress.Zip4 = shippingAddress.Zip4;

                    result.ShippingAddress.Subdivision.Category = shippingAddress.Subdivision.SubdivisionCategory;
                    result.ShippingAddress.Subdivision.Code = shippingAddress.Subdivision.SubdivisionCode;
                    result.ShippingAddress.Subdivision.Name1 = shippingAddress.Subdivision.SubdivisionName1;
                    result.ShippingAddress.Subdivision.Name2 = shippingAddress.Subdivision.SubdivisionName2;
                    result.ShippingAddress.Subdivision.Name3 = shippingAddress.Subdivision.SubdivisionName3;
                    result.ShippingAddress.Subdivision.RegionalDivision = shippingAddress.Subdivision.RegionalDivision;
                    result.ShippingAddress.Subdivision.Country.Code = shippingAddress.Subdivision.CountryCode;
                }


                LinqEntity.PhoneNumber shippingPhone = (
                    from pna in account.PhoneNumberAccounts
                    where pna.PhoneNumberTypeId == (int)PhoneNumberTypeEnum.ShippingPhone
                        && pna.IsDeleted == false
                        && pna.PhoneNumber.IsDeleted == false
                    select pna.PhoneNumber
                    ).SingleOrDefault();

                if (shippingPhone != null)
                {
                    result.ShippingAddress.Phone = shippingPhone.Number;
                }


                LinqEntity.PhoneNumber shippingFax = (
                    from pna in account.PhoneNumberAccounts
                    where pna.PhoneNumberTypeId == (int)PhoneNumberTypeEnum.ShippingFax
                        && pna.IsDeleted == false
                        && pna.PhoneNumber.IsDeleted == false
                    select pna.PhoneNumber
                    ).SingleOrDefault();

                if (shippingFax != null)
                {
                    result.ShippingAddress.Fax = shippingFax.Number;
                }


                LinqEntity.Email shippingEmail = (
                    from ea in account.EmailAccounts
                    where ea.EmailTypeId == (int)EmailTypeEnum.Shipping
                        && ea.IsDeleted == false
                        && ea.Email.IsDeleted == 0
                    select ea.Email
                    ).SingleOrDefault();

                if (shippingEmail != null)
                {
                    result.ShippingAddress.Email = shippingEmail.EmailAddress;
                }

                #endregion

                #region Load billing addresses

                LinqEntity.PostalAddress billingAddress = (
                    from paa in account.PostalAddressAccounts
                    where paa.PostalAddressTypeId == (int)PostalAddressTypeEnum.Billing
                        && paa.IsDeleted == false
                        && paa.PostalAddress.IsDeleted == 0
                    select paa.PostalAddress
                    ).SingleOrDefault();

                result.BillingAddress = new EntityData.AddressData();
                result.BillingAddress.Subdivision = new EntityData.SubdivisionData();
                result.BillingAddress.Subdivision.Country = new EntityData.CountryData();

                if (billingAddress != null)
                {
                    result.BillingAddress.Address1 = billingAddress.Address1;
                    result.BillingAddress.Address2 = billingAddress.Address2;
                    result.BillingAddress.City = billingAddress.City;
                    result.BillingAddress.County = billingAddress.County;
                    result.BillingAddress.CreateDate = billingAddress.CreateDate;
                    result.BillingAddress.CreateUserId = billingAddress.CreateUserId;
                    result.BillingAddress.FirstName = billingAddress.FirstName;
                    result.BillingAddress.Id = billingAddress.PostalAddressId;
                    result.BillingAddress.IsDeleted = (billingAddress.IsDeleted == 1);
                    result.BillingAddress.IsResidentialArea = billingAddress.IsResidentialArea ?? false;
                    result.BillingAddress.LastName = billingAddress.LastName;
                    result.BillingAddress.Name = billingAddress.Name;
                    result.BillingAddress.UpdateDate = billingAddress.UpdateDate;
                    result.BillingAddress.UpdateUserId = billingAddress.UpdateUserId;
                    result.BillingAddress.Zip = billingAddress.Zip;
                    result.BillingAddress.Zip4 = billingAddress.Zip4;

                    result.BillingAddress.Subdivision.Category = billingAddress.Subdivision.SubdivisionCategory;
                    result.BillingAddress.Subdivision.Code = billingAddress.Subdivision.SubdivisionCode;
                    result.BillingAddress.Subdivision.Name1 = billingAddress.Subdivision.SubdivisionName1;
                    result.BillingAddress.Subdivision.Name2 = billingAddress.Subdivision.SubdivisionName2;
                    result.BillingAddress.Subdivision.Name3 = billingAddress.Subdivision.SubdivisionName3;
                    result.BillingAddress.Subdivision.RegionalDivision = billingAddress.Subdivision.RegionalDivision;
                    result.BillingAddress.Subdivision.Country.Code = billingAddress.Subdivision.CountryCode;
                }


                LinqEntity.PhoneNumber billingPhone = (
                    from pna in account.PhoneNumberAccounts
                    where pna.PhoneNumberTypeId == (int)PhoneNumberTypeEnum.BillingPhone
                        && pna.IsDeleted == false
                        && pna.PhoneNumber.IsDeleted == false
                    select pna.PhoneNumber
                    ).SingleOrDefault();

                if (billingPhone != null)
                {
                    result.BillingAddress.Phone = billingPhone.Number;
                }


                LinqEntity.PhoneNumber billingFax = (
                    from pna in account.PhoneNumberAccounts
                    where pna.PhoneNumberTypeId == (int)PhoneNumberTypeEnum.BillingFax
                        && pna.IsDeleted == false
                        && pna.PhoneNumber.IsDeleted == false
                    select pna.PhoneNumber
                    ).SingleOrDefault();

                if (billingFax != null)
                {
                    result.BillingAddress.Fax = billingFax.Number;
                }


                LinqEntity.Email billingEmail = (
                    from ea in account.EmailAccounts
                    where ea.EmailTypeId == (int)EmailTypeEnum.Billing
                        && ea.IsDeleted == false
                        && ea.Email.IsDeleted == 0
                    select ea.Email
                    ).SingleOrDefault();

                if (billingEmail != null)
                {
                    result.BillingAddress.Email = billingEmail.EmailAddress;
                }

                #endregion

                #region Status history

                StatusSystem statusSystem = new StatusSystem();
                result.StatusHistory = statusSystem.GetStatusHistoryFromAccount(result.Id);

                #endregion
            }

            return result;
        }

        public bool IsAccountEditable(int accountId)
        {
            bool result = false;

            EntityData.AccountData accountData = this.GetAccount(accountId);
            result = this.IsAccountEditable(accountData);

            return result;
        }
        public bool IsAccountEditable(EntityData.AccountData accountData)
        {
            bool result = false;

            if (accountData.StatusId >= 0 && accountData.StatusId < 101)
            {
                result = true;
            }
            else if (accountData.StatusId >= 300 && accountData.StatusId < 400)
            {
                result = true;
            }
            else if (accountData.StatusId >= 900)
            {
                result = true;
            }

            return result;
        }
        public bool IsAccountCurrentlyTaxExempt(int accountId)
        {
            bool result = false;

            EntityData.AccountData accountData = this.GetAccount(accountId, true);
            result = this.IsAccountCurrentlyTaxExempt(accountData);

            return result;
        }
        public bool IsAccountCurrentlyTaxExempt(EntityData.AccountData accountData)
        {
            bool result = false;

            if (this.IsAccountAutomaticallyTaxExempt(accountData))
            {
                result = true;
            }
            else
            {
                #region Look for an active tax exemption form

                DocumentSystem documentSystem = new DocumentSystem();

                DocumentSearchParameters documentSearchParameters = new DocumentSearchParameters();
                documentSearchParameters.SearchField = DocumentSearchFieldEnum.QSPAccountId;
                documentSearchParameters.SearchValue = accountData.Id.ToString();
                documentSearchParameters.DocumentStatusId = (int)DocumentStatusEnum.Approved;
                documentSearchParameters.IsPagingEnabled = false;
                documentSearchParameters.SortField = "QSPDocumentId";

                List<DocumentSearchItem> documentSearchResults = documentSystem.Search(documentSearchParameters);

                foreach (DocumentSearchItem dsi in documentSearchResults)
                {
                    if (dsi.ExemptionStartDate <= DateTime.Now
                        && dsi.ExemptionEndDate >= DateTime.Now)
                    {
                        result = true;
                        break;
                    }
                }

                #endregion
            }

            return result;
        }
        public bool IsAccountAutomaticallyTaxExempt(int accountId)
        {
            bool result = false;

            EntityData.AccountData accountData = this.GetAccount(accountId, true);
            result = this.IsAccountAutomaticallyTaxExempt(accountData);

            return result;
        }
        public bool IsAccountAutomaticallyTaxExempt(EntityData.AccountData accountData)
        {
            bool result = false;

            int organizationTypeId = 0;
            int productTypeId = 0;
            string subdivisionCode = "";

            #region Get data

            // Get organization type id
            organizationTypeId = accountData.Organization.Type.Id;

            // Get product type from latest campaign
            CampaignSystem campaignSystem = new CampaignSystem();
            LinqEntity.Campaign campaign = campaignSystem.GetLatestCampaign(accountData.Id);

            if (campaign.FormId.HasValue)
            {
                FormSystem formSystem = new FormSystem();
                LinqEntity.Form form = formSystem.GetForm(campaign.FormId.Value);

                if (form.ProgramTypeId.HasValue)
                {
                    ProgramSystem programSystem = new ProgramSystem();
                    LinqEntity.ProductType productType = programSystem.GetProductType(form.ProgramTypeId.Value);

                    productTypeId = productType.ProductTypeId;
                }
            }

            // Get subdivision code
            subdivisionCode = accountData.BillingAddress.Subdivision.Code;

            #endregion

            #region Get tax exemption

            TaxSystem taxSystem = new TaxSystem();
            List<LinqEntity.TaxCalculationMethod> taxCalculationMethods = taxSystem.GetTaxCalculationMethods(organizationTypeId, productTypeId, subdivisionCode, TaxLevelEnum.State);

            foreach (LinqEntity.TaxCalculationMethod tcm in taxCalculationMethods)
            {
                if (tcm.TaxExemptable)
                {
                    result = true;
                    break;
                }
            }

            #endregion

            return result;
        }

        #endregion

        #region Version 1 code

        dataAccessRef accDataAccess;

        private AccountFinderService.AccountFinder accountFinder = null;
        private AccountFinderService.AccountFinder AccountFinder
        {
            get
            {
                AccountFinderService.LoginMessageRequest loginMessageRequest;

                if (accountFinder == null)
                {
                    accountFinder = new AccountFinderService.AccountFinder();
                    loginMessageRequest = new AccountFinderService.LoginMessageRequest();

                    accountFinder.CookieContainer = new System.Net.CookieContainer();

                    loginMessageRequest.UserName = Settings.Default.AccountFinderWSUserName;
                    loginMessageRequest.Password = Settings.Default.AccountFinderWSPassword;

                    accountFinder.Login(loginMessageRequest);
                }

                return accountFinder;
            }
        }

        public AccountSystem()
        {
            accDataAccess = new dataAccessRef();
        }


        public bool IsTaxExempted(AccountData dtsAccount)
        {
            //Get the Tax Exemption
            bool IsTaxExempted = false;
            int OrgType = 0;
            int FormID = 0;
            //Org Info -- Organization Type
            OrganizationTable dtOrganization = dtsAccount.Organization;
            if (dtOrganization.Rows.Count > 0)
            {
                DataRow orgRow = dtOrganization.Rows[0];
                OrgType = Convert.ToInt32(orgRow[OrganizationTable.FLD_ORG_TYPE_ID]);
            }
            //Camp Info -- Form ID
            if (!dtsAccount.IsFormIDNull)
            {
                FormID = dtsAccount.FormID;
            }
            //DataRow accRow = dtsAccount.Account.Rows[0];
            //if (accRow[AccountTable.FLD_TAX_EXEMPTION_NO].ToString().Trim().Length > 0)
            //{
            //    IsTaxExempted = true;
            //}
            //else
            //{
            Business.CommonSystem comSys = new QSPForm.Business.CommonSystem();
            IsTaxExempted = comSys.IsTaxExempted(dtsAccount, OrgType, FormID, EntityType.TYPE_ACCOUNT);
            //}
            return IsTaxExempted;

        }
        private void SetDocumentRequirement(AccountData dtsAccount, int UserID, Data.ConnectionProvider connProvider)
        {
            DataRow accRow = dtsAccount.Account.Rows[0];
            DocumentEntityTable accDoc = dtsAccount.AccountDocument;
            EntityExceptionTable accExc = dtsAccount.AccountException;
            Data.Document_entity docDataAccess = new Data.Document_entity();
            if (connProvider != null)
                docDataAccess.MainConnectionProvider = connProvider;

            bool IsTaxExemptionFormRequired = false;
            //Apply Validation on the account Level First
            IsTaxExemptionFormRequired = accExc.IsContainExceptionType(Convert.ToInt32(BusinessExceptionType.TaxExemptionForm));
            //DataView dvException = new DataView(accExc);
            //string sFilter = EntityExceptionTable.FLD_EXCEPTION_TYPE_ID + " = " + Convert.ToInt32(BusinessExceptionType.TaxExemption).ToString();

            //dvException.RowFilter =	sFilter;
            //IsTaxExemptionFormRequired = (dvException.Count != 0);

            //dvException = null;

            //If a Tax Exemption Form is required we have to add a row 
            //in the DocumentEntityTable
            if (IsTaxExemptionFormRequired)
            {
                if (dtsAccount.AccountDocument.Rows.Count == 0)
                {
                    DataRow newRow = dtsAccount.AccountDocument.NewRow();
                    int AccID = Convert.ToInt32(dtsAccount.Account.Rows[0][dataDef.FLD_PKID]);
                    newRow[DocumentEntityTable.FLD_ENTITY_ID] = AccID;
                    newRow[DocumentEntityTable.FLD_ENTITY_TYPE_ID] = EntityType.TYPE_ACCOUNT;
                    newRow[DocumentEntityTable.FLD_DOCUMENT_NAME] = "Tax Exemption Form: Account # " + AccID.ToString();
                    newRow[DocumentEntityTable.FLD_DOCUMENT_TYPE_ID] = DocumentType.TAX_EXEMPTION;
                    newRow[DocumentEntityTable.FLD_CREATE_USER_ID] = UserID;
                    dtsAccount.AccountDocument.Rows.Add(newRow);

                    DocumentEntitySystem docSys = new DocumentEntitySystem();
                    docDataAccess.Insert(dtsAccount.AccountDocument);
                }
            }
            else
            {
                //				if (dtsAccount.AccountDocument.Rows.Count > 0)
                //				{
                //					DataRow docRow = dtsAccount.AccountDocument.Rows[0];
                //					docRow[DocumentEntityTable.FLD_UPDATE_USER_ID] = UserID;
                //					docRow.Delete();
                //					DocumentEntitySystem docSys = new DocumentEntitySystem();
                //					docSys.Delete(accDoc);
                //
                //				}
            }

        }


        protected override bool Validate(DataRow row)
        {
            bool isValid = true;

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

            IsValid &= IsValid_RequiredField(row, dataDef.FLD_ORG_ID, "Organization");
            IsValid &= IsValid_RequiredField(row, dataDef.FLD_ACCOUNT_TYPE_ID, "Account Type");
            IsValid &= IsValid_RequiredField(row, dataDef.FLD_FM_ID, "FM Number");

            if (!IsValid)
            {
                messageManager.ValidationExceptionType = QSPFormExceptionType.RequiredFields;
            }

            return IsValid;
        }
        

        public bool InsertAllDetail(AccountData dtsAccountData, bool duplicateAccountOverride, List<AccountFinderService.OutputAccount> matchingAccounts, int userId)
        {
            //Variable to handle the operation in One transaction transaction
            String TransactionName = "Account_InsertAllDetail";
            Data.ConnectionProvider connProvider = new Data.ConnectionProvider();
            bool IsSuccess = true;
            AccountData dts = (AccountData)dtsAccountData;
            OrganizationSystem orgSys = new OrganizationSystem();

            if (dts.Organization.Rows[0].RowState == DataRowState.Added)
            {
                orgSys.FoundReplace(dts);
                //if ( !orgSys.IsValid_Unicity(dts) )				 
                //{
                //    //messageManager.HeaderText = "System Error";
                //    messageManager.ValidationExceptionType = QSPFormExceptionType.Unicity;
                //    messageManager.SetErrorMessage(QSPFormMessage.VALMSG_UNICITY_ORG);
                //    throw new QSPFormValidationException(messageManager);			
                //    return false;
                //}
            }
            try
            {
                // Remove duplicate accounts (different addresses)
                if (duplicateAccountOverride)
                {
                    RemoveDuplicateMatchingAccounts(matchingAccounts);
                }

                //Data Object Instanciation
                Data.Organization orgDataAccess = new Data.Organization();
                Data.Account accDataAccess = new Data.Account();
                Data.Campaign campDataAccess = new Data.Campaign();
                Data.Customer custDataAccess = new Data.Customer();
                Data.Postal_address_entity postDataAccess = new Data.Postal_address_entity();
                Data.Phone_number_entity phoneDataAccess = new Data.Phone_number_entity();
                Data.Email_entity emailDataAccess = new Data.Email_entity();
                Data.Account_status_change accStatusDataAccess = new Data.Account_status_change();
                Data.Common commonDataAccess = new QSPForm.Data.Common();

                // Pass the created ConnectionProvider object to the data-access objects.
                orgDataAccess.MainConnectionProvider = connProvider;
                accDataAccess.MainConnectionProvider = connProvider;
                campDataAccess.MainConnectionProvider = connProvider;
                custDataAccess.MainConnectionProvider = connProvider;
                postDataAccess.MainConnectionProvider = connProvider;
                phoneDataAccess.MainConnectionProvider = connProvider;
                emailDataAccess.MainConnectionProvider = connProvider;
                accStatusDataAccess.MainConnectionProvider = connProvider;
                commonDataAccess.MainConnectionProvider = connProvider;

                orgDataAccess.AcceptChangesDuringUpdate = false;
                accDataAccess.AcceptChangesDuringUpdate = false;
                campDataAccess.AcceptChangesDuringUpdate = false;
                postDataAccess.AcceptChangesDuringUpdate = false;
                phoneDataAccess.AcceptChangesDuringUpdate = false;
                emailDataAccess.AcceptChangesDuringUpdate = false;
                accStatusDataAccess.AcceptChangesDuringUpdate = false;

                //****************BEGIN OPEN TRANSACTION PROCESS
                connProvider.OpenConnection();
                connProvider.BeginTransaction(TransactionName);
                //****************END OPEN TRANSACTION PROCESS

                DataRow accRow = dts.Account.Rows[0];

                //****************BEGIN ORGANIZATION PROCESS
                if (dts.Organization.Rows[0].RowState == DataRowState.Added)
                {
                    orgDataAccess.UpdateBatch(dts.Organization);
                    int OrgID = Convert.ToInt32(dts.Organization.Rows[0][OrganizationTable.FLD_PKID]);
                    accRow[dataDef.FLD_ORG_ID] = OrgID;
                }
                //****************END ORGANIZATION PROCESS

                //****************BEGIN CUSTOMER PROCESS
                //Create a new customer for this new Account
                CustomerTable dTblCust = new CustomerTable();
                DataRow custRow = dTblCust.NewRow();
                custRow[CustomerTable.FLD_FIRST_NAME] = accRow[dataDef.FLD_NAME];
                custRow[CustomerTable.FLD_CREATE_USER_ID] = accRow[dataDef.FLD_CREATE_USER_ID];
                dTblCust.Rows.Add(custRow);
                custDataAccess.Insert(dTblCust);
                //****************BEGIN CUSTOMER PROCESS

                //****************BEGIN ACCOUNT PROCESS
                //Update withe new ID
                accRow[dataDef.FLD_CUSTOMER_ID] = custRow[CustomerTable.FLD_PKID];
                //ACCOUNT STATUS
                //On New Insert it's always in process
                //For now there is nothing than can hold a new account
                //To be transfer to the AS400
                accRow[dataDef.FLD_ACCOUNT_STATUS_ID] = QSPForm.Common.AccountStatus.IN_PROCESS;

                accDataAccess.UpdateBatch(dts.Account);

                //****************END ACCOUNT PROCESS

                //****************BEGIN WAREHOUSE PROCESS
                //Verify if a Default Warehouse can be find
                //We don't need to handle the transaction 
                //cause we don't read in the same DB
                SetDefaultWarehouse(dts);
                //****************END WAREHOUSE PROCESS

                //****************BEGIN CAMPAIGN PROCESS
                //Get the New ID and update the campaignRow
                int newId = Convert.ToInt32(dts.Account.Rows[0][AccountTable.FLD_PKID]);
                DataRow row = dts.Campaign.Rows[0];
                row[CampaignTable.FLD_ACCOUNT_ID] = newId;

                campDataAccess.UpdateBatch(dts.Campaign);
                //****************END CAMPAIGN PROCESS

                //****************BEGIN POSTAL ADDRESS PROCESS
                //Prepare the thing to see if it's the same Address	
                PrepareTransactionWithNewID(dts);
                //Postal Address
                if (dts.PostalAddress.GetChanges() != null)
                {
                    postDataAccess.UpdateBatch(dts.PostalAddress);
                }
                //****************END POSTAL ADDRESS PROCESS

                //****************BEGIN PHONE NUMBER PROCESS
                //Phone Number
                if (dts.PhoneNumber.GetChanges() != null)
                {
                    phoneDataAccess.UpdateBatch(dts.PhoneNumber);
                }
                //****************END PHONE NUMBER PROCESS

                //****************BEGIN EMAIL PROCESS
                //Email Address
                if (dts.EmailAddress.GetChanges() != null)
                {
                    emailDataAccess.UpdateBatch(dts.EmailAddress);
                }
                //****************END EMAIL PROCESS

                //****************BEGIN DOCUMENT PROCESS
                //Documentation
                SetDocumentRequirement(dts, userId, connProvider);
                //****************END DOCUMENT PROCESS

                //****************BEGIN DUPLICATE ACCOUNT OVERRIDE PROCESS
                if (duplicateAccountOverride)
                {
                    foreach (AccountFinderService.OutputAccount matchingAccount in matchingAccounts)
                    {
                        commonDataAccess.InsertDuplicateAccountOverride(newId, matchingAccount.AccountId, userId);
                    }
                }
                //****************END DUPLICATE ACCOUNT OVERRIDE PROCESS

                //****************BEGIN REFRESH PROCESS
                //Exception and Task
                Refresh(dts, userId, DataOperation.INSERT, connProvider);
                //****************END REFRESH PROCESS

                //Register the update in the order_status_change table.
                AccountStatusChangeTable dTblChange = new AccountStatusChangeTable();
                DataRow newChangerRow = dTblChange.NewRow();

                newChangerRow[AccountStatusChangeTable.FLD_ACCOUNT_ID] = accRow[dataDef.FLD_PKID];
                newChangerRow[AccountStatusChangeTable.FLD_ACCOUNT_STATUS_ID] = accRow[dataDef.FLD_ACCOUNT_STATUS_ID];
                newChangerRow[AccountStatusChangeTable.FLD_STATUS_CHANGE_REASON] = "Update from Order Express";
                newChangerRow[AccountStatusChangeTable.FLD_CREATE_USER_ID] = userId;
                dTblChange.Rows.Add(newChangerRow);
                //Execute the chnage to DB
                accStatusDataAccess.Insert(dTblChange);

                //****************BEGIN COMMIT PROCESS
                //Commit transaction 
                connProvider.CommitTransaction();
                dtsAccountData.Merge(dts, false);
                dtsAccountData.AcceptChanges();
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
        public bool UpdateAllDetail(AccountData dtsAccountData, int UserID)
        {
            //Variable to handle the operation in One transaction transaction
            String TransactionName = "Account_UpdateAllDetail";
            Data.ConnectionProvider connProvider = new Data.ConnectionProvider();
            AccountData dts = (AccountData)dtsAccountData;
            bool IsSuccess = true;
            try
            {
                //This method fill the All Data needed for an organization
                //into a predefined DataSet	

                //Data Object Instanciation
                Data.Organization orgDataAccess = new Data.Organization();
                Data.Account accDataAccess = new Data.Account();
                Data.Campaign campDataAccess = new Data.Campaign();
                Data.Customer custDataAccess = new Data.Customer();
                Data.Postal_address_entity postDataAccess = new Data.Postal_address_entity();
                Data.Phone_number_entity phoneDataAccess = new Data.Phone_number_entity();
                Data.Email_entity emailDataAccess = new Data.Email_entity();
                Data.Account_status_change accStatusDataAccess = new Data.Account_status_change();

                // Pass the created ConnectionProvider object to the data-access objects.
                orgDataAccess.MainConnectionProvider = connProvider;
                accDataAccess.MainConnectionProvider = connProvider;
                campDataAccess.MainConnectionProvider = connProvider;
                custDataAccess.MainConnectionProvider = connProvider;
                postDataAccess.MainConnectionProvider = connProvider;
                phoneDataAccess.MainConnectionProvider = connProvider;
                emailDataAccess.MainConnectionProvider = connProvider;
                accStatusDataAccess.MainConnectionProvider = connProvider;

                orgDataAccess.AcceptChangesDuringUpdate = false;
                accDataAccess.AcceptChangesDuringUpdate = false;
                campDataAccess.AcceptChangesDuringUpdate = false;
                custDataAccess.AcceptChangesDuringUpdate = false;
                postDataAccess.AcceptChangesDuringUpdate = false;
                phoneDataAccess.AcceptChangesDuringUpdate = false;
                emailDataAccess.AcceptChangesDuringUpdate = false;
                accStatusDataAccess.AcceptChangesDuringUpdate = false;

                connProvider.OpenConnection();
                connProvider.BeginTransaction(TransactionName);

                bool HasChanges = false;
                int AccountID = 0;
                DataRow accRow = dts.Account.Rows[0];
                AccountID = Convert.ToInt32(accRow[dataDef.FLD_PKID]);

                //***CHECK FOR CONCURENTIAL MODIFICATION
                AccountTable accVersion = accDataAccess.SelectOne(AccountID);
                if (accVersion.Rows.Count > 0)
                {
                    DataRow accVersionRow = accVersion.Rows[0];
                    if (Convert.ToDateTime(accVersionRow[dataDef.FLD_UPDATE_DATE]) != Convert.ToDateTime(accRow[dataDef.FLD_UPDATE_DATE]))
                    {
                        messageManager.HeaderText = "System Error";
                        messageManager.ValidationExceptionType = QSPFormExceptionType.RecordIsModified;
                        messageManager.SetErrorMessage(messageManager.FormatErrorMessage(QSPFormMessage.CONCURENT_RECORD_MODIFIED, "Account"));

                        throw new QSPFormValidationException(messageManager);
                    }
                }
                else
                {
                    messageManager.HeaderText = "System Error";
                    messageManager.ValidationExceptionType = QSPFormExceptionType.RecordIsDeleted;
                    messageManager.SetErrorMessage(messageManager.FormatErrorMessage(QSPFormMessage.CONCURENT_RECORD_DELETED, "Account"));

                    throw new QSPFormValidationException(messageManager);
                }

                //Verify if a Default Warehouse can be find
                SetDefaultWarehouse(dts);

                //Campaign
                UpdateCampaignInformation(dts, UserID);
                if (dts.Campaign.GetChanges() != null)
                {
                    //if the account tax information is changed
                    //we have to refresh the campaign information
                    campDataAccess.UpdateBatch(dts.Campaign);
                    HasChanges = true;
                }

                PrepareTransactionWithNewID(dts);

                //Postal Address
                if (dts.PostalAddress.GetChanges() != null)
                {
                    //if the account postal address is changed
                    //we have to refresh the campaign information
                    UpdateAddressInformationToEntity(dts, UserID, Common.EntityType.TYPE_CAMPAIGN);
                    postDataAccess.UpdateBatch(dts.PostalAddress);
                    HasChanges = true;

                }
                //Phone Number
                if (dts.PhoneNumber.GetChanges() != null)
                {
                    UpdatePhoneInformationToEntity(dts, UserID, Common.EntityType.TYPE_CAMPAIGN);
                    phoneDataAccess.UpdateBatch(dts.PhoneNumber);
                    HasChanges = true;
                }
                //Email Addess
                if (dts.EmailAddress.GetChanges() != null)
                {
                    UpdateEmailInformationToEntity(dts, UserID, Common.EntityType.TYPE_CAMPAIGN);
                    emailDataAccess.UpdateBatch(dts.EmailAddress);
                    HasChanges = true;
                }

                //Documentation
                SetDocumentRequirement(dts, UserID, connProvider);

                //******SAVING THE GLOBAL UPDATE DATE IN ORDER HEADER TABLE
                if (HasChanges && dts.Account.GetChanges() == null)
                {
                    //If no change have been made in the main table
                    //but in at least one sattelite table
                    //a change have been made, we have to update the main table
                    accRow[dataDef.FLD_UPDATE_USER_ID] = UserID;
                }

                //Submit information for Account at the end
                //to consider in a global way any changes from all tables
                accRow[dataDef.FLD_ACCOUNT_STATUS_ID] = QSPForm.Common.AccountStatus.IN_PROCESS;

                //This method fill the All Data needed for an organization
                //into a predefined DataSet			
                if (dts.Account.GetChanges() != null)
                {
                    accDataAccess.UpdateBatch(dts.Account);
                    HasChanges = true;
                }

                //We need to put the Refresh after
                //Exception and Task
                Refresh(dts, UserID, DataOperation.UPDATE, connProvider);

                //Register the update in the order_status_change table.
                AccountStatusChangeTable dTblChange = new AccountStatusChangeTable();
                DataRow newChangerRow = dTblChange.NewRow();

                newChangerRow[AccountStatusChangeTable.FLD_ACCOUNT_ID] = accRow[dataDef.FLD_PKID];
                newChangerRow[AccountStatusChangeTable.FLD_ACCOUNT_STATUS_ID] = accRow[dataDef.FLD_ACCOUNT_STATUS_ID];
                newChangerRow[AccountStatusChangeTable.FLD_STATUS_CHANGE_REASON] = "Update from Order Express";
                newChangerRow[AccountStatusChangeTable.FLD_CREATE_USER_ID] = UserID;
                dTblChange.Rows.Add(newChangerRow);
                //Execute the chnage to DB
                accStatusDataAccess.Insert(dTblChange);

                //Commit transaction 
                connProvider.CommitTransaction();
                dtsAccountData.Merge(dts, false);
                dtsAccountData.AcceptChanges();
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
        public void Delete(int QSPAccountID, int UserID)
        {
            accDataAccess.Delete(QSPAccountID, UserID);
        }

		public dataDef SelectAllByOrganizationID(int OrganizationID, string FMID, int FSM_DisplayMode)
		{			
			dataDef dTbl;
			//
			// Get the user DataTable from the DataLayer
			//
			dTbl = accDataAccess.SelectAllWorganization_idLogic(OrganizationID, FMID, FSM_DisplayMode);				
			
			return dTbl;			
		}
		public dataDef SelectAllByCampaignID(int CampaignID)
		{			
			dataDef dTbl;
			//
			// Get the user DataTable from the DataLayer
			//
			dTbl = accDataAccess.SelectAllWcampaign_idLogic(CampaignID);				
			
			return dTbl;			
		}
        public AccountData SelectAllDetail(int ID)
        {
            return accDataAccess.SelectAllDetail(ID);
        }
        public AccountData SelectAllDetailByCampaignID(int CampID)
        {
            return accDataAccess.SelectAllDetailByCampaignID(CampID);

        }
        public AccountData SelectAllDetailWithLastCampaign(int ID)
        {
            return accDataAccess.SelectAllDetailWithLastCampaign(ID);

        }
        public dataDef SelectAll_Search(int SearchType, String Criteria, int ProgramType, string SubdivisionCode, string FMID, int FSM_DisplayMode, int StatusCategoryID, string FMName)
        {
            dataDef dTbl;

            //
            // Get the user DataTable from the DataLayer
            //				
            dTbl = accDataAccess.SelectAll_Search(SearchType, Criteria, ProgramType, SubdivisionCode, FMID, FSM_DisplayMode, StatusCategoryID, FMName);

            return dTbl;
        }
        public AccountStatusChangeTable SelectAllAccountStatusChangeByAccountID(int AccountID)
        {
            AccountStatusChangeTable dTbl;
            //
            // Get the user DataTable from the DataLayer
            //
            Data.Account_status_change accStatusDataAccess = new Data.Account_status_change();
            dTbl = accStatusDataAccess.SelectAllWaccount_idLogic(AccountID);

            return dTbl;

        }

		public dataDef SelectOne(int ID)
		{			
			dataDef dTbl;			
			//
			// Get the user DataTable from the DataLayer
			//
			dTbl = accDataAccess.SelectOne(ID);				
			
			return dTbl;
			
		}

		public ARMCUSPTable SelectOne_FromSynch(int FulfAccountID)
		{			
			ARMCUSPTable dTbl = new ARMCUSPTable();			
			//
			// Get the user DataTable from the DataLayer
			//
			QSPForm.DataRepository.ARMCUSP synch_acc = new QSPForm.DataRepository.ARMCUSP();
			dTbl = synch_acc.SelectOne(FulfAccountID);				
			
			return dTbl;
			
		}
		public OLMCUSPTable ExchangeTable_SelectAll()
		{			
			OLMCUSPTable dTbl = new OLMCUSPTable();			
			//
			// Get the user DataTable from the DataLayer
			//
			QSPForm.DataRepository.OLMCUSP synch_excacc = new QSPForm.DataRepository.OLMCUSP();
			dTbl = synch_excacc.SelectAll();	
			//Proceed the remainig information needed like status name and fsm name
			//Account Status
			CommonSystem comSys = new CommonSystem();
			DataTable dTblStatus = new DataTable();			
			dTblStatus = comSys.SelectAllAccountStatus();
			DataView dvStatus = new DataView(dTblStatus);
			dvStatus.Sort = dTblStatus.Columns[0].ColumnName;
			
			CUserSystem cuserSys = new CUserSystem();			
			CUserTable dTblFM = new CUserTable();
			dTblFM = cuserSys.SelectAllFM();
			DataView dvFM = new DataView(dTblFM);
			dvFM.Sort = CUserTable.FLD_FM_ID;

			foreach(DataRow row in dTbl.Rows)
			{
				//Find Status
				if (!row.IsNull(OLMCUSPTable.FLD_STATUS))
				{
					int iStatus = Convert.ToInt32(row[OLMCUSPTable.FLD_STATUS]);
					int iIndex  = dvStatus.Find(iStatus);
					if (iIndex > - 1)
					{
						row[OLMCUSPTable.FLD_STATUS_NAME] = dvStatus[iIndex][1];
						if (!dvStatus[iIndex].Row.IsNull(4))
							row[OLMCUSPTable.FLD_STATUS_COLOR_CODE] = dvStatus[iIndex][4];
						else
							row[OLMCUSPTable.FLD_STATUS_COLOR_CODE] = "White";
					}
				}

				//Find Status
				if (!row.IsNull(OLMCUSPTable.FLD_FSM))
				{
					string sFMID = row[OLMCUSPTable.FLD_FSM].ToString();
					sFMID = sFMID.Substring(2);
					int iIndex  = dvFM.Find(sFMID);
					if (iIndex > - 1)
					{
						row[OLMCUSPTable.FLD_FSM_NAME] = dvFM[iIndex][CUserTable.FLD_FSM_NAME];
					}
				}
			}
			
			return dTbl;
			
		}


        public AccountData RenewAccount(int AccountID, int UserID)
        {
            //We prepare the DataSet for all step
            //Add a new Row

            BusinessCalendarSystem calSys = new BusinessCalendarSystem();
            int CurrentFY = calSys.GetFiscalYear();
            int NbStudent = 0;
            decimal EstimatedAmount = 0;
            int ProgramTypeID = 0;
            string ProgramTypeName = "";

            AccountData dts = SelectAllDetail(AccountID);
            CampaignTable dtCamp = dts.Campaign;
            DataView dvCamp = new DataView(dtCamp);
            dvCamp.Sort = CampaignTable.FLD_FISCAL_YEAR + " DESC";
            int campLastFY = 0;
            if (dvCamp.Count > 0)
            {
                DataRow campLastFYRow = dvCamp[0].Row;
                if (!campLastFYRow.IsNull(CampaignTable.FLD_GOAL_ESTIMATED_GROSS))
                    EstimatedAmount = Convert.ToDecimal(campLastFYRow[CampaignTable.FLD_GOAL_ESTIMATED_GROSS]);

                if (!campLastFYRow.IsNull(CampaignTable.FLD_ENROLLMENT))
                    NbStudent = Convert.ToInt32(campLastFYRow[CampaignTable.FLD_ENROLLMENT]);

                if (!campLastFYRow.IsNull(CampaignTable.FLD_PROG_TYPE_ID))
                {
                    ProgramTypeID = Convert.ToInt32(campLastFYRow[CampaignTable.FLD_PROG_TYPE_ID]);
                    ProgramTypeName = campLastFYRow[CampaignTable.FLD_PROG_TYPE_NAME].ToString();
                }
                campLastFY = Convert.ToInt32(campLastFYRow[CampaignTable.FLD_FISCAL_YEAR]);

            }

            if (campLastFY != CurrentFY)
            {

                DataRow campRow;
                campRow = dtCamp.NewRow();

                DataRow row;
                row = dts.Account.Rows[0];
                campRow[CampaignTable.FLD_NAME] = row[AccountTable.FLD_NAME];
                campRow[CampaignTable.FLD_FM_ID] = row[AccountTable.FLD_FM_ID];
                campRow[CampaignTable.FLD_FM_NAME] = row[AccountTable.FLD_FM_NAME];
                campRow[CampaignTable.FLD_FISCAL_YEAR] = CurrentFY; //Fiscal Year
                if (ProgramTypeID != 0)
                {
                    campRow[CampaignTable.FLD_PROG_TYPE_ID] = ProgramTypeID;
                    campRow[CampaignTable.FLD_PROG_TYPE_NAME] = ProgramTypeName;
                    int FormID = 0;
                    FormSystem formSys = new FormSystem();
                    FormTable dTblForm = formSys.SelectByEntityType(EntityType.TYPE_ACCOUNT, ProgramTypeID);
                    if (dTblForm.Rows.Count > 0)
                    {
                        FormID = Convert.ToInt32(dTblForm.Rows[0][FormTable.FLD_PKID]);
                        campRow[CampaignTable.FLD_FORM_ID] = FormID;
                    }
                }
                campRow[CampaignTable.FLD_GOAL_ESTIMATED_GROSS] = EstimatedAmount;
                campRow[CampaignTable.FLD_ENROLLMENT] = NbStudent;

                campRow[CampaignTable.FLD_ACCOUNT_ID] = row[AccountTable.FLD_PKID];
                campRow[CampaignTable.FLD_CREATE_USER_ID] = UserID;



                dtCamp.Rows.Clear();
                //dtCamp.Clear();
                //Insert row				
                dtCamp.Rows.Add(campRow);
                //Apply also the Contact Information
                SetCampaignDefaultInformation(dts, UserID);
            }
            else
            {
                //Remove campaign not in the current FY

                bool IsFound = true;
                int iIndexFound = -1;

                while (IsFound)
                {
                    iIndexFound = -1;
                    for (int i = 0; i < dtCamp.Rows.Count; i++)
                    {
                        if (!dtCamp.Rows[i].IsNull(CampaignTable.FLD_FISCAL_YEAR))
                        {
                            if (dtCamp.Rows[i][CampaignTable.FLD_FISCAL_YEAR].ToString() != CurrentFY.ToString())
                            {
                                iIndexFound = i;
                                break;
                            }
                        }
                    }
                    if (iIndexFound == -1)
                    {
                        IsFound = false;
                        break;
                    }
                    else
                    {
                        dtCamp.Rows.RemoveAt(iIndexFound);
                    }
                }
            }

            return dts;
        }
        public bool IsRenewalRequired(int accountId, int programTypeId, out int currentCampaignId)
        {
            bool result = false;
            int campaignId = 0;

            QSP.Business.Fulfillment.Account account = QSP.Business.Fulfillment.Account.GetAccount(accountId);
            //QSP.Business.Fulfillment.Form form = QSP.Business.Fulfillment.Form.GetForm(formId);
            int currentFY = FiscalYearSystem.GetFYFromDate(DateTime.Now);
            List<QSP.Business.Fulfillment.Campaign> campaignListCurrentFY = QSP.Business.Fulfillment.Campaign.GetCampaignList(currentFY, accountId, programTypeId);

            if (campaignListCurrentFY.Count > 0)
            {
                result = false;
                campaignId = campaignListCurrentFY[0].CampaignId;
            }
            else
            {
                int nextFY = currentFY + 1;
                List<QSP.Business.Fulfillment.Campaign> campaignListNextFY = QSP.Business.Fulfillment.Campaign.GetCampaignList(nextFY, accountId, programTypeId);

                if (campaignListNextFY.Count > 0)
                {
                    #region Create current campaign from next year's

                    #region Campaign

                    QSP.Business.Fulfillment.Campaign newCampaign = new QSP.Business.Fulfillment.Campaign();

                    //newCampaign.CampaignId;
                    newCampaign.AccountId = account.AccountId;
                    //newCampaign.FulfCampaignId;
                    newCampaign.ProgramTypeId = programTypeId;
                    //newCampaign.WarehouseId;
                    newCampaign.CampaignName = account.AccountName;
                    newCampaign.FmId = campaignListNextFY[0].FmId;
                    //newCampaign.TaxExemptionNumber;
                    //newCampaign.TaxExemptionExpirationDate;
                    newCampaign.StartDate = DateTime.Now;
                    newCampaign.EndDate = DateTime.Now.AddDays(1);
                    newCampaign.FiscalYear = currentFY;
                    newCampaign.Enrollment = campaignListNextFY[0].Enrollment;
                    newCampaign.GoalEstimatedGross = campaignListNextFY[0].GoalEstimatedGross;
                    //newCampaign.ARORBL;
                    //newCampaign.Comments;
                    newCampaign.Deleted = false;
                    newCampaign.CreateDate = DateTime.Now;
                    newCampaign.CreateUserId = campaignListNextFY[0].CreateUserId;
                    newCampaign.UpdateDate = DateTime.Now;
                    newCampaign.UpdateUserId = campaignListNextFY[0].CreateUserId;
                    //newCampaign.DtsCAccountId;
                    //newCampaign.DtsCCAInstance;

                    if (campaignListNextFY[0].TradeClassId != null)
                    {
                        newCampaign.TradeClassId = campaignListNextFY[0].TradeClassId;
                    }

                    List<QSP.Business.Fulfillment.Form> programFormList = QSP.Business.Fulfillment.Form.GetProgramForm(programTypeId);
                    if (programFormList.Count > 0)
                    {
                        newCampaign.FormId = programFormList[0].FormId;
                    }

                    newCampaign.Insert();

                    QSPForm.Data.Campaign campaignDataAccess = new QSPForm.Data.Campaign();
                    campaignDataAccess.CampaignLinkToCCA(newCampaign.CampaignId);

                    #endregion

                    #region Postal addresses

                    List<QSP.Business.Fulfillment.PostalAddressAccount> postalAddressList = QSP.Business.Fulfillment.PostalAddressAccount.GetAddressesByAccount(account.AccountId);
                    foreach (QSP.Business.Fulfillment.PostalAddressAccount postalAddress in postalAddressList)
                    {
                        QSP.Business.Fulfillment.PostalAddressCampaign newPostalAddress = new QSP.Business.Fulfillment.PostalAddressCampaign();

                        //newPostalAddress.PostalAddressCampaignId; 
                        newPostalAddress.PostalAddressTypeId = postalAddress.PostalAddressTypeId;
                        newPostalAddress.PostalAddressId = postalAddress.PostalAddressId;
                        newPostalAddress.CampaignId = newCampaign.CampaignId;
                        newPostalAddress.Deleted = false;
                        newPostalAddress.CreateDate = DateTime.Now;
                        newPostalAddress.CreateUserId = account.CreateUserId;
                        newPostalAddress.UpdateDate = DateTime.Now;
                        newPostalAddress.UpdateUserId = account.CreateUserId;

                        newPostalAddress.Insert();
                    }

                    #endregion

                    #region Phone number

                    List<QSP.Business.Fulfillment.PhoneNumberAccount> phoneNumberList = QSP.Business.Fulfillment.PhoneNumberAccount.GetPhoneNumberAccountList(account.AccountId);
                    foreach (QSP.Business.Fulfillment.PhoneNumberAccount phoneNumber in phoneNumberList)
                    {
                        QSP.Business.Fulfillment.PhoneNumberCampaign newPhoneNumber = new QSP.Business.Fulfillment.PhoneNumberCampaign();

                        //newPhoneNumber.PhoneNumberCampaignId;
                        newPhoneNumber.PhoneNumberTypeId = phoneNumber.PhoneNumberTypeId;
                        newPhoneNumber.PhoneNumberId = phoneNumber.PhoneNumberId;
                        newPhoneNumber.CampaignId = newCampaign.CampaignId;
                        newPhoneNumber.Deleted = false;
                        newPhoneNumber.CreateDate = DateTime.Now;
                        newPhoneNumber.CreateUserId = account.CreateUserId;
                        newPhoneNumber.UpdateDate = DateTime.Now;
                        newPhoneNumber.UpdateUserId = account.CreateUserId;

                        newPhoneNumber.Insert();
                    }

                    #endregion

                    #region Email

                    List<QSP.Business.Fulfillment.EmailAccount> emailList = QSP.Business.Fulfillment.EmailAccount.GetEmailAccountListByAccount(account.AccountId);
                    foreach (QSP.Business.Fulfillment.EmailAccount email in emailList)
                    {
                        QSP.Business.Fulfillment.EmailCampaign newEmail = new QSP.Business.Fulfillment.EmailCampaign();

                        //newEmail.EmailCampaignId;                    
                        newEmail.EmailTypeId = email.EmailTypeId;
                        newEmail.EmailId = email.EmailId;
                        newEmail.CampaignId = newCampaign.CampaignId;
                        newEmail.Deleted = false;
                        newEmail.CreateDate = DateTime.Now;
                        newEmail.CreateUserId = account.CreateUserId;
                        newEmail.UpdateDate = DateTime.Now;
                        newEmail.UpdateUserId = account.CreateUserId;

                        newEmail.Insert();
                    }

                    #endregion

                    #endregion

                    result = false;
                    campaignId = newCampaign.CampaignId;
                }
                else
                {
                    result = true;
                }
            }

            currentCampaignId = campaignId;

            return result;
        }
        public bool IsRenew(AccountTable dt)
        {
            bool isRenew = false;

            if (dt.Rows.Count > 0)
            {
                isRenew = Convert.ToBoolean(dt.Rows[0][AccountTable.FLD_IS_RENEW]);
            }
            return isRenew;
        }

        public List<AccountFinderService.OutputAccount> GetMatchingAccounts(AccountFinderService.Account searchAccount)
        {
            if (Settings.Default.AccountFinderEnabled)
            {
                AccountFinderService.AccountFinderMessageRequest accountFinderMessageRequest = new AccountFinderService.AccountFinderMessageRequest();
                AccountFinderService.OutputAccount[] outputAccounts = null;
                List<AccountFinderService.OutputAccount> outputAccountList = null;

                accountFinderMessageRequest.Account = searchAccount;
                outputAccounts = AccountFinder.GetMatchingAccounts(accountFinderMessageRequest).MatchingAccounts;

                outputAccountList = new List<AccountFinderService.OutputAccount>();

                foreach (AccountFinderService.OutputAccount outputAccount in outputAccounts)
                {
                    outputAccountList.Add(outputAccount);
                }

                AccountFinder.Logout();

                return outputAccountList;
            }
            else
                return new List<AccountFinderService.OutputAccount>();
        }
        private void RemoveDuplicateMatchingAccounts(List<AccountFinderService.OutputAccount> matchingAccounts)
        {
            List<AccountFinderService.OutputAccount> duplicateMatchingAccounts = new List<AccountFinderService.OutputAccount>();

            foreach (AccountFinderService.OutputAccount matchingAccount in matchingAccounts)
            {
                foreach (AccountFinderService.OutputAccount duplicateMatchingAccount in matchingAccounts)
                {
                    if (!duplicateMatchingAccounts.Contains(matchingAccount) &&
                        !duplicateMatchingAccounts.Contains(duplicateMatchingAccount) &&
                        matchingAccount != duplicateMatchingAccount &&
                        matchingAccount.AccountId == duplicateMatchingAccount.AccountId)
                    {
                        duplicateMatchingAccounts.Add(duplicateMatchingAccount);
                    }
                }
            }

            foreach (AccountFinderService.OutputAccount duplicateMatchingAccount in duplicateMatchingAccounts)
            {
                matchingAccounts.Remove(duplicateMatchingAccount);
            }
        }


        private void PrepareTransactionWithNewID(AccountData dts)
        {
            int OrgID = Convert.ToInt32(dts.Organization.Rows[0][OrganizationTable.FLD_PKID]);
            string sOrgName = dts.Organization.Rows[0][OrganizationTable.FLD_NAME].ToString();
            int NewID = Convert.ToInt32(dts.Account.Rows[0][AccountTable.FLD_PKID]);
            int CampNewID = Convert.ToInt32(dts.Campaign.Rows[0][CampaignTable.FLD_PKID]);

            foreach (DataRow row in dts.PostalAddress.Rows)
            {
                if (row.RowState == DataRowState.Added)
                {
                    if (row[PostalAddressEntityTable.FLD_ENTITY_TYPE_ID].ToString() == EntityType.TYPE_ACCOUNT.ToString())
                        row[PostalAddressEntityTable.FLD_ENTITY_ID] = NewID;
                    else if (row[PostalAddressEntityTable.FLD_ENTITY_TYPE_ID].ToString() == EntityType.TYPE_CAMPAIGN.ToString())
                        row[PostalAddressEntityTable.FLD_ENTITY_ID] = CampNewID;
                    else if (row[PostalAddressEntityTable.FLD_ENTITY_TYPE_ID].ToString() == EntityType.TYPE_ORGANIZATION.ToString())
                    {
                        row[PostalAddressEntityTable.FLD_ENTITY_ID] = OrgID;
                        row[PostalAddressEntityTable.FLD_NAME] = sOrgName;
                    }
                }
            }

            foreach (DataRow row in dts.PhoneNumber.Rows)
            {
                if (row.RowState == DataRowState.Added)
                {
                    if (row[PhoneNumberEntityTable.FLD_ENTITY_TYPE_ID].ToString() == EntityType.TYPE_ACCOUNT.ToString())
                        row[PhoneNumberEntityTable.FLD_ENTITY_ID] = NewID;
                    else if (row[PostalAddressEntityTable.FLD_ENTITY_TYPE_ID].ToString() == EntityType.TYPE_CAMPAIGN.ToString())
                        row[PhoneNumberEntityTable.FLD_ENTITY_ID] = CampNewID;
                    else if (row[PhoneNumberEntityTable.FLD_ENTITY_TYPE_ID].ToString() == EntityType.TYPE_ORGANIZATION.ToString())
                        row[PhoneNumberEntityTable.FLD_ENTITY_ID] = OrgID;
                }
            }

            foreach (DataRow row in dts.EmailAddress.Rows)
            {
                if (row.RowState == DataRowState.Added)
                {
                    if (row[EmailEntityTable.FLD_ENTITY_TYPE_ID].ToString() == EntityType.TYPE_ACCOUNT.ToString())
                        row[EmailEntityTable.FLD_ENTITY_ID] = NewID;
                    else if (row[PostalAddressEntityTable.FLD_ENTITY_TYPE_ID].ToString() == EntityType.TYPE_CAMPAIGN.ToString())
                        row[EmailEntityTable.FLD_ENTITY_ID] = CampNewID;
                    else if (row[EmailEntityTable.FLD_ENTITY_TYPE_ID].ToString() == EntityType.TYPE_ORGANIZATION.ToString())
                        row[EmailEntityTable.FLD_ENTITY_ID] = OrgID;
                }
            }

            foreach (DataRow row in dts.AccountException.Rows)
            {
                if (row.RowState == DataRowState.Added)
                {
                    row[EntityExceptionTable.FLD_ENTITY_TYPE_ID] = EntityType.TYPE_ACCOUNT;
                    row[EntityExceptionTable.FLD_ENTITY_ID] = NewID;
                }
            }

        }
		public AccountData InitializeAccount(int UserID, String FMID, int OrganizationID, String MDRPID, int FormID)
		{
			//We prepare the DataSet for all step
			//Add a new Row
			AccountData dts = new AccountData();
			
			//Create a new Organization  row at start
			AccountTable accTable = dts.Account;
			DataRow row;
			row = accTable.NewRow();
            row[AccountTable.FLD_ACCOUNT_STATUS_ID] = QSPForm.Common.AccountStatus.IN_PROCESS;
			row[AccountTable.FLD_CREATE_USER_ID] = UserID;
			accTable.Rows.Add(row);

			//Create a row for campaign at init
			//Create a new Organization  row at start
			CampaignTable dtCamp = dts.Campaign;
			DataRow campRow;
			campRow = dtCamp.NewRow();			
			BusinessCalendarSystem calSys = new BusinessCalendarSystem();	
			int CurrentFY = calSys.GetFiscalYear();
			campRow[CampaignTable.FLD_FISCAL_YEAR] = CurrentFY; //Fiscal Year
			campRow[CampaignTable.FLD_GOAL_ESTIMATED_GROSS] = 0;	//Form ID
			//Get the ProgramType by the form ID
			FormSystem formSys = new FormSystem();	
			FormTable dTblForm = formSys.SelectOne(FormID);
			if (dTblForm.Rows.Count > 0)
			{
				DataRow frmRow = dTblForm.Rows[0];
				campRow[CampaignTable.FLD_PROG_TYPE_ID] = frmRow[FormTable.FLD_PROGRAM_TYPE_ID];
				campRow[CampaignTable.FLD_PROG_TYPE_NAME] = frmRow[FormTable.FLD_PROGRAM_TYPE_NAME];

				campRow[CampaignTable.FLD_FORM_ID] = FormID;
			}
				
			campRow[CampaignTable.FLD_GOAL_ESTIMATED_GROSS] = 0;				
			campRow[CampaignTable.FLD_ENROLLMENT] = 0;

			campRow[CampaignTable.FLD_ACCOUNT_ID] = row[AccountTable.FLD_PKID];
			campRow[CampaignTable.FLD_CREATE_USER_ID] = UserID;

			dtCamp.Rows.Add(campRow);

			//Retreive the Organization information
			//if not, create a new one by the MDRPID, if specified
			if (OrganizationID > 0)
			{
				row[AccountTable.FLD_ORG_ID] = OrganizationID;					
				SetDefaultInformation(dts, UserID);				
			}
			else
			{
				SetOrganizationDefaultInformation(dts, UserID, FMID, MDRPID);			
			}

			

			//FM Info -- if this is an FM
			string fm_name = "";
			if (FMID.Length > 0)
			{
				//Get the FM Name
				CUserSystem fmSys = new CUserSystem();
				CUserTable dTblfm = new CUserTable();
				dTblfm = fmSys.SelectOne(FMID);
				DataRow fmRow = dTblfm.Rows[0];
				
				fm_name = fmRow[CUserTable.FLD_LAST_NAME] + ", " + fmRow[CUserTable.FLD_FIRST_NAME];			
				//Account
				row[AccountTable.FLD_FM_ID] = FMID;				
				row[AccountTable.FLD_FM_NAME] = fm_name;
				//Campaign
				campRow[CampaignTable.FLD_FM_ID] = FMID;
				campRow[CampaignTable.FLD_FM_NAME] = fm_name;
			}
			else
			{
				if ((dts.Organization.Rows.Count > 0) && (OrganizationID > 0))
				{
					DataRow orgRow  = dts.Organization.Rows[0];
					if (!orgRow.IsNull(OrganizationTable.FLD_FM_ID))
					{
						FMID = orgRow[OrganizationTable.FLD_FM_ID].ToString();
						fm_name = orgRow[OrganizationTable.FLD_FM_NAME].ToString();
						//Account
						row[AccountTable.FLD_FM_ID] = FMID;				
						row[AccountTable.FLD_FM_NAME] = fm_name;
						//Campaign
						campRow[CampaignTable.FLD_FM_ID] = FMID;
						campRow[CampaignTable.FLD_FM_NAME] = fm_name;
					}

				}
			}
			//Get the Default Warehouse associated to the shipping Zip Code
            SetDefaultWarehouse(dts);

			return dts;
			
		}

        public void SetDefaultInformation(AccountData dtsAccountData, int UserID)
        {
            AccountTable dtblAccount = dtsAccountData.Account;
            DataRow accRow = dtblAccount.Rows[0];
            if (accRow[AccountTable.FLD_ORG_ID] != DBNull.Value)
            {
                int AccID = Convert.ToInt32(accRow[AccountTable.FLD_PKID]);
                int OrgID = Convert.ToInt32(accRow[AccountTable.FLD_ORG_ID]);
                if (OrgID > 0)
                {
                    OrganizationSystem orgSys = new OrganizationSystem();
                    dtsAccountData.Merge(orgSys.SelectAllDetail(OrgID));
                    if (dtsAccountData.Organization.Rows.Count > 0)
                    {
                        //Copy Default at the Account Level
                        DataRow orgRow = dtsAccountData.Organization.Rows[0];
                        accRow[AccountTable.FLD_NAME] = orgRow[OrganizationTable.FLD_NAME];
                        accRow[AccountTable.FLD_FM_ID] = orgRow[OrganizationTable.FLD_FM_ID];
                        accRow[AccountTable.FLD_FM_NAME] = orgRow[OrganizationTable.FLD_FM_NAME];
                        accRow[AccountTable.FLD_ACCOUNT_TYPE_ID] = 1; //Standard
                        accRow[AccountTable.FLD_TAX_EXEMPTION_NO] = orgRow[OrganizationTable.FLD_TAX_EXEMPTION_NO];
                        accRow[AccountTable.FLD_TAX_EXEMPTION_EXP_DATE] = orgRow[OrganizationTable.FLD_TAX_EXEMPTION_EXP_DATE];

                        DataRow campRow = dtsAccountData.Campaign.Rows[0];
                        campRow[CampaignTable.FLD_NAME] = accRow[AccountTable.FLD_NAME];
                        campRow[CampaignTable.FLD_FM_ID] = accRow[AccountTable.FLD_FM_ID];
                        campRow[CampaignTable.FLD_FM_NAME] = accRow[AccountTable.FLD_FM_NAME];

                        campRow[CampaignTable.FLD_TAX_EXEMPTION_NO] = accRow[AccountTable.FLD_TAX_EXEMPTION_NO];
                        campRow[CampaignTable.FLD_TAX_EXEMPTION_EXP_DATE] = accRow[AccountTable.FLD_TAX_EXEMPTION_EXP_DATE];


                    }

                    PostalAddressSystem postSys = new PostalAddressSystem();
                    postSys.CopyToEntity(dtsAccountData.PostalAddress, UserID,
                        EntityType.TYPE_ORGANIZATION, OrgID,
                        EntityType.TYPE_ACCOUNT, AccID);

                    PhoneNumberSystem phoneSys = new PhoneNumberSystem();
                    phoneSys.CopyToEntity(dtsAccountData.PhoneNumber, UserID,
                        EntityType.TYPE_ORGANIZATION, OrgID,
                        EntityType.TYPE_ACCOUNT, AccID);

                    EmailAddressSystem emailSys = new EmailAddressSystem();
                    emailSys.CopyToEntity(dtsAccountData.EmailAddress, UserID,
                        EntityType.TYPE_ORGANIZATION, OrgID,
                        EntityType.TYPE_ACCOUNT, AccID);

                }
            }

        }
        public void SetDefaultWarehouse(AccountData dts)
        {
            try
            {
                int AccountID = 0;
                int ProgramTypeID = 0;
                AccountID = Convert.ToInt32(dts.Account.Rows[0][AccountTable.FLD_PKID]);
                if (dts.Campaign.Rows.Count > 0)
                    ProgramTypeID = Convert.ToInt32(dts.Campaign.Rows[0][CampaignTable.FLD_PROG_TYPE_ID]);
                if (ProgramTypeID == 11) //Only WFC Warehouse can be Assigned
                {
                    //Get the Default Warehouse associated to the shipping Zip Code
                    PostalAddressSystem postSys = new PostalAddressSystem();
                    DataRow postRow = postSys.FindRow(dts.PostalAddress, EntityType.TYPE_ACCOUNT, AccountID, PostalAddressType.TYPE_SHIPPING);
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
                                if (dts.Campaign.Rows.Count > 0)
                                {
                                    CommonSystem comSys = new CommonSystem();
                                    DataRow campRow = dts.Campaign.Rows[0];
                                    comSys.UpdateRow(campRow, CampaignTable.FLD_WAREHOUSE_ID, WareID.ToString());
                                    comSys.UpdateRow(campRow, CampaignTable.FLD_FULF_WAREHOUSE_ID, FulfWareID.ToString());
                                    comSys.UpdateRow(campRow, CampaignTable.FLD_WAREHOUSE_NAME, wareRow[WarehouseTable.FLD_NAME].ToString());
                                }
                            }
                        }
                    }

                }
                else //We ensure that no Warehouse is selected for thos program
                {
                    CommonSystem comSys = new CommonSystem();
                    DataRow campRow = dts.Campaign.Rows[0];
                    comSys.UpdateRow(campRow, CampaignTable.FLD_WAREHOUSE_ID, "");
                }

            }
            catch (Exception ex)
            {
                //Nothing for now
            }
        }
		public void SetOrganizationDefaultInformation(AccountData dtsAccountData, int UserID, string FMID, string MDRPID)
		{
			int OrgID = 0;
			int AccID = 0;
			QSPForm.Business.OrganizationSystem orgSys = new QSPForm.Business.OrganizationSystem();	
			OrganizationData dtsOrganizationData = orgSys.InitializeOrganization(UserID, FMID);
			if (MDRPID.Length >0)
			{
				dtsOrganizationData.Organization.Rows[0][OrganizationTable.FLD_MDRPID] = MDRPID;
				
			}
			orgSys.SetDefaultInformation(dtsOrganizationData, UserID);

			dtsAccountData.Merge(dtsOrganizationData.Organization);

			DataRow orgRow = dtsAccountData.Organization.Rows[0];
			DataRow accRow = dtsAccountData.Account.Rows[0];
			OrgID = Convert.ToInt32(orgRow[OrganizationTable.FLD_PKID]);
			AccID = Convert.ToInt32(accRow[AccountTable.FLD_PKID]);
			
			if (dtsOrganizationData.PostalAddress.Rows.Count >0)
				dtsAccountData.Merge(dtsOrganizationData.PostalAddress);
			if (dtsOrganizationData.PhoneNumber.Rows.Count >0)
				dtsAccountData.Merge(dtsOrganizationData.PhoneNumber);			
			
			accRow[AccountTable.FLD_ORG_ID] = OrgID;				
			accRow[AccountTable.FLD_NAME] = orgRow[OrganizationTable.FLD_NAME];
			accRow[AccountTable.FLD_FM_ID] = orgRow[OrganizationTable.FLD_FM_ID];
			accRow[AccountTable.FLD_FM_NAME] = orgRow[OrganizationTable.FLD_FM_NAME];
			accRow[AccountTable.FLD_ACCOUNT_TYPE_ID] = 1; //Standard

			PostalAddressSystem	postSys = new PostalAddressSystem();
			postSys.CopyToEntity(dtsAccountData.PostalAddress, UserID, 
									EntityType.TYPE_ORGANIZATION,OrgID,
									EntityType.TYPE_ACCOUNT, AccID);
			
			PhoneNumberSystem phoneSys = new PhoneNumberSystem();
			phoneSys.CopyToEntity(dtsAccountData.PhoneNumber, UserID, 
									EntityType.TYPE_ORGANIZATION,OrgID,
									EntityType.TYPE_ACCOUNT, AccID);

			EmailAddressSystem emailSys = new EmailAddressSystem();
			emailSys.CopyToEntity(dtsAccountData.EmailAddress, UserID, 
									EntityType.TYPE_ORGANIZATION,OrgID,
									EntityType.TYPE_ACCOUNT, AccID);

			

		}
		public void SetCampaignDefaultInformation(AccountData dtsAccountData, int UserID)
		{
			//It's only possible to apply Account Information on a the Current Campaign
			
			BusinessCalendarSystem calSys = new BusinessCalendarSystem();	
			int CurrentFY = calSys.GetFiscalYear();

			DataView dvCamp = new DataView(dtsAccountData.Campaign);
			dvCamp.RowFilter = CampaignTable.FLD_FISCAL_YEAR + " = " + CurrentFY.ToString();
			if (dvCamp.Count == 1)
			{
				DataRow campRow = dvCamp[0].Row;
				DataRow accRow = dtsAccountData.Account.Rows[0];
				int CampID = Convert.ToInt32(campRow[CampaignTable.FLD_PKID]);
				int AccID = Convert.ToInt32(accRow[AccountTable.FLD_PKID]);

				PostalAddressSystem	postSys = new PostalAddressSystem();
				postSys.CopyToEntity(dtsAccountData.PostalAddress, UserID, 
					EntityType.TYPE_ACCOUNT,AccID,
					EntityType.TYPE_CAMPAIGN, CampID);
			
				PhoneNumberSystem phoneSys = new PhoneNumberSystem();
				phoneSys.CopyToEntity(dtsAccountData.PhoneNumber, UserID, 
					EntityType.TYPE_ACCOUNT,AccID,
					EntityType.TYPE_CAMPAIGN, CampID);

				EmailAddressSystem emailSys = new EmailAddressSystem();
				emailSys.CopyToEntity(dtsAccountData.EmailAddress, UserID, 
					EntityType.TYPE_ACCOUNT,AccID,
					EntityType.TYPE_CAMPAIGN, CampID);

			}
				
		}

        public void UpdateCampaignInformation(AccountData dtsAccountData, int UserID)
		{
			//This method is used to update all address info to the specified Level Org Campaign) when this is a 
			// Account is updated
			DataRow accRow = dtsAccountData.Account.Rows[0];
			DataRow campRow = dtsAccountData.Campaign.Rows[0];			
			CommonSystem comSys = new CommonSystem();

			comSys.UpdateRow(campRow, CampaignTable.FLD_NAME, accRow[AccountTable.FLD_NAME].ToString());
			comSys.UpdateRow(campRow, CampaignTable.FLD_FM_ID, accRow[AccountTable.FLD_FM_ID].ToString());
			comSys.UpdateRow(campRow, CampaignTable.FLD_COMMENTS, accRow[AccountTable.FLD_COMMENTS].ToString());
			
			comSys.UpdateRow(campRow, CampaignTable.FLD_TAX_EXEMPTION_NO, accRow[AccountTable.FLD_TAX_EXEMPTION_NO].ToString());
			comSys.UpdateRow(campRow, CampaignTable.FLD_TAX_EXEMPTION_EXP_DATE, accRow[AccountTable.FLD_TAX_EXEMPTION_EXP_DATE].ToString());
 
			
			
		}
        public void UpdateAddressInformationToEntity(AccountData dtsAccountData, int UserID, int ToEntityType)
        {
            //This method is used to update all address info to the specified Level Org Campaign) when this is a 
            // Account is updated
            DataRow accRow = dtsAccountData.Account.Rows[0];
            int AccID = Convert.ToInt32(accRow[AccountTable.FLD_PKID]);

            int EntityID = 0;
            switch (ToEntityType)
            {
                case Common.EntityType.TYPE_ORGANIZATION:
                    {
                        EntityID = Convert.ToInt32(dtsAccountData.Organization.Rows[0][OrganizationTable.FLD_PKID]);
                        break;
                    }
                case Common.EntityType.TYPE_CAMPAIGN:
                    {
                        EntityID = Convert.ToInt32(dtsAccountData.Campaign.Rows[0][CampaignTable.FLD_PKID]);
                        break;
                    }

            }
            PostalAddressSystem addSys = new PostalAddressSystem();
            dtsAccountData.Merge(addSys.SelectAllByEntityID(ToEntityType, EntityID));

            addSys.CopyToEntity(dtsAccountData.PostalAddress, UserID,
                                EntityType.TYPE_ACCOUNT, AccID, PostalAddressType.TYPE_BILLING,
                                ToEntityType, EntityID, PostalAddressType.TYPE_BILLING);

            addSys.CopyToEntity(dtsAccountData.PostalAddress, UserID,
                                EntityType.TYPE_ACCOUNT, AccID, PostalAddressType.TYPE_SHIPPING,
                                ToEntityType, EntityID, PostalAddressType.TYPE_SHIPPING);


        }
        public void UpdatePhoneInformationToEntity(AccountData dtsAccountData, int UserID, int ToEntityType)
        {
            //This method is used to update all address info to the specified Level Org Campaign) when this is a 
            // Account is updated
            //***** PHONE NUMBER *****************
            DataRow accRow = dtsAccountData.Account.Rows[0];
            int AccID = Convert.ToInt32(accRow[AccountTable.FLD_PKID]);

            int EntityID = 0;
            switch (ToEntityType)
            {
                case Common.EntityType.TYPE_ORGANIZATION:
                    {
                        EntityID = Convert.ToInt32(dtsAccountData.Organization.Rows[0][OrganizationTable.FLD_PKID]);
                        break;
                    }
                case Common.EntityType.TYPE_CAMPAIGN:
                    {
                        EntityID = Convert.ToInt32(dtsAccountData.Campaign.Rows[0][CampaignTable.FLD_PKID]);
                        break;
                    }

            }

            PhoneNumberSystem phoneSys = new PhoneNumberSystem();
            dtsAccountData.Merge(phoneSys.SelectAllByEntityID(ToEntityType, EntityID));

            phoneSys.CopyToEntity(dtsAccountData.PhoneNumber, UserID,
                                    EntityType.TYPE_ACCOUNT, AccID, PhoneNumberType.TYPE_BILLING_PHONE,
                                    ToEntityType, EntityID, PhoneNumberType.TYPE_BILLING_PHONE);

            phoneSys.CopyToEntity(dtsAccountData.PhoneNumber, UserID,
                EntityType.TYPE_ACCOUNT, AccID, PhoneNumberType.TYPE_BILLING_PHONE,
                ToEntityType, EntityID, PhoneNumberType.TYPE_BILLING_PHONE);


            phoneSys.CopyToEntity(dtsAccountData.PhoneNumber, UserID,
                EntityType.TYPE_ACCOUNT, AccID, PhoneNumberType.TYPE_BILLING_FAX,
                ToEntityType, EntityID, PhoneNumberType.TYPE_BILLING_FAX);

            phoneSys.CopyToEntity(dtsAccountData.PhoneNumber, UserID,
                EntityType.TYPE_ACCOUNT, AccID, PhoneNumberType.TYPE_SHIPPING_PHONE,
                ToEntityType, EntityID, PhoneNumberType.TYPE_SHIPPING_PHONE);

            phoneSys.CopyToEntity(dtsAccountData.PhoneNumber, UserID,
                EntityType.TYPE_ACCOUNT, AccID, PhoneNumberType.TYPE_SHIPPING_FAX,
                ToEntityType, EntityID, PhoneNumberType.TYPE_SHIPPING_FAX);



        }
        public void UpdateEmailInformationToEntity(AccountData dtsAccountData, int UserID, int ToEntityType)
        {
            DataRow accRow = dtsAccountData.Account.Rows[0];
            int AccID = Convert.ToInt32(accRow[AccountTable.FLD_PKID]);

            int EntityID = 0;
            switch (ToEntityType)
            {
                case Common.EntityType.TYPE_ORGANIZATION:
                    {
                        EntityID = Convert.ToInt32(dtsAccountData.Organization.Rows[0][OrganizationTable.FLD_PKID]);
                        break;
                    }
                case Common.EntityType.TYPE_CAMPAIGN:
                    {
                        EntityID = Convert.ToInt32(dtsAccountData.Campaign.Rows[0][CampaignTable.FLD_PKID]);
                        break;
                    }

            }

            EmailAddressSystem emailSys = new EmailAddressSystem();
            dtsAccountData.Merge(emailSys.SelectAllByEntityID(ToEntityType, EntityID));

            emailSys.CopyToEntity(dtsAccountData.EmailAddress, UserID,
                                    EntityType.TYPE_ACCOUNT, AccID, EmailType.TYPE_BILLING,
                                    ToEntityType, EntityID, EmailType.TYPE_BILLING);

            emailSys.CopyToEntity(dtsAccountData.EmailAddress, UserID,
                                EntityType.TYPE_ACCOUNT, AccID, EmailType.TYPE_SHIPPING,
                                ToEntityType, EntityID, EmailType.TYPE_SHIPPING);


        }


		public void CopyInformationToEntity(AccountData dtsAccountData, int UserID, int CopyToEntityType)
		{
			//This method is used to copy all address info to the specified Level Org Campaign) when this is a 
			//new Account			
			
			DataRow accRow = dtsAccountData.Account.Rows[0];			
			int AccID = Convert.ToInt32(accRow[AccountTable.FLD_PKID]);
			int EntityID = 0;

			if (CopyToEntityType == Common.EntityType.TYPE_ORGANIZATION)
			{
				DataRow orgRow = dtsAccountData.Organization.Rows[0];
				EntityID = Convert.ToInt32(orgRow[OrganizationTable.FLD_PKID]);
			}
			else if (CopyToEntityType == Common.EntityType.TYPE_CAMPAIGN)
			{
				DataRow campRow = dtsAccountData.Campaign.Rows[0];
				EntityID = Convert.ToInt32(campRow[CampaignTable.FLD_PKID]);
			}

			PostalAddressSystem	postSys = new PostalAddressSystem();
			postSys.CopyToEntity(dtsAccountData.PostalAddress, UserID, 
				EntityType.TYPE_ACCOUNT,AccID,
				CopyToEntityType, EntityID);
			
			PhoneNumberSystem phoneSys = new PhoneNumberSystem();
			phoneSys.CopyToEntity(dtsAccountData.PhoneNumber, UserID, 
				EntityType.TYPE_ACCOUNT,AccID,
				CopyToEntityType, EntityID);

			EmailAddressSystem emailSys = new EmailAddressSystem();
			emailSys.CopyToEntity(dtsAccountData.EmailAddress, UserID, 
				EntityType.TYPE_ACCOUNT,AccID,
				CopyToEntityType, EntityID);


		}

        
        internal bool Refresh(int AccountID, int UserID)
		{
			bool IsSuccess = false;

			AccountData dts = SelectAllDetail(AccountID);
			Refresh(dts, UserID, QSPForm.Common.DataOperation.UPDATE, null);					
			
			return IsSuccess;
            
		}
		internal bool Refresh(int AccountID, int UserID, Data.ConnectionProvider connProvider)
		{
			bool IsSuccess = false;			
			Data.Account accDataAccess = new QSPForm.Data.Account();
			if (connProvider != null)
				accDataAccess.MainConnectionProvider = connProvider;
			AccountData dts = accDataAccess.SelectAllDetailWithLastCampaign(AccountID);
			Refresh(dts, UserID, Common.DataOperation.UPDATE, connProvider);					
			
			return IsSuccess;
            
		}
		private bool Refresh(AccountData dts, int UserID, int dataOperation, Data.ConnectionProvider connProvider)
		{
			bool HasChanged = false;
			DateTime dStart = DateTime.Now;
			TimeSpan tpDuration;
			try
			{
				FormData dtsForm = new FormData();
				FormSystem frmSys = new FormSystem();
			
				int FormID = 0;
				if (!dts.IsFormIDNull)
				{
					FormID = dts.FormID;
				}

				//****************BEGIN FORM PROCESS
				DateTime dStartProcess = DateTime.Now;
				dtsForm = frmSys.SelectAllDetail(FormID, true);
				Debug.WriteLine("Form: " + DateTime.Now.ToLongTimeString());
				tpDuration = DateTime.Now.Subtract(dStartProcess);
				Debug.WriteLine("Form Duration: " + tpDuration.TotalSeconds.ToString());				
				//****************END FORM PROCESS

				//****************BEGIN REFRESH VALIDATION PROCESS
				dStartProcess = DateTime.Now;
				HasChanged = RefreshValidation(dts, dtsForm, UserID, dataOperation, connProvider);									
				Debug.WriteLine("RefreshValidation: " + DateTime.Now.ToLongTimeString());
				tpDuration = DateTime.Now.Subtract(dStartProcess);
				Debug.WriteLine("RefreshValidation Duration: " + tpDuration.TotalSeconds.ToString());				
				//****************END REFRESH VALIDATION PROCESS

				//****************BEGIN REFRESH VALIDATION PROCESS
				dStartProcess = DateTime.Now;
				RefreshTask(dts, dtsForm, UserID, dataOperation, connProvider);	
				Debug.WriteLine("RefreshTask: " + DateTime.Now.ToLongTimeString());
				tpDuration = DateTime.Now.Subtract(dStartProcess);
				Debug.WriteLine("RefreshTask Duration: " + tpDuration.TotalSeconds.ToString());				
				//****************END REFRESH VALIDATION PROCESS

				//****************BEGIN REFRESH ORDER PROCESS
				dStartProcess = DateTime.Now;
				//If a change happen --> cascade the change to all orders under account
                //if (HasChanged)
                //{
                //    OrderSystem ordSys = new OrderSystem();
                //    //Cascading to the Order Level
                //    int CampaignID = Convert.ToInt32(dts.Campaign.Rows[0][CampaignTable.FLD_PKID]);
                //    OrderHeaderTable dTblOrder = ordSys.SelectAllByCampaignID(CampaignID);
                //    foreach (DataRow ordRow in dTblOrder.Rows)
                //    {
                //        int OrderID = Convert.ToInt32(ordRow[OrderHeaderTable.FLD_PKID]);
                //        HasChanged &= ordSys.Refresh(OrderID, dts, UserID, connProvider);					
                //    }				
                //}
				Debug.WriteLine("Refresh Order: " + DateTime.Now.ToLongTimeString());
				tpDuration = DateTime.Now.Subtract(dStartProcess);
				Debug.WriteLine("Refresh Order Duration: " + tpDuration.TotalSeconds.ToString());				
				//****************END REFRESH VALIDATION PROCESS
			}
			catch (Exception ex)
			{
				throw ex;
			}
			Debug.WriteLine("Refresh Account: " + DateTime.Now.ToLongTimeString());
			tpDuration = DateTime.Now.Subtract(dStart);
			Debug.WriteLine("Refresh Account Duration: " + tpDuration.TotalSeconds.ToString());				
				
			return HasChanged;
            
		}

		public bool PerformValidation(AccountData dtsAccount, int UserID, int dataOperation)
		{		
			bool IsValid = true;

			try
			{
				int FormID = 0;
				if (!dtsAccount.IsFormIDNull)
				{
					FormID = dtsAccount.FormID;
				}
				QSPForm.Business.FormSystem formSys = new QSPForm.Business.FormSystem();
				FormData dtsForm = formSys.SelectAllDetail(FormID, true);

				IsValid = PerformValidation(dtsAccount, dtsForm, UserID, dataOperation);
			}
			catch(Exception ex)
			{
				throw ex;
			}
			return IsValid;
		}
		private bool PerformValidation(AccountData dtsAccount, FormData dtsForm, int UserID, int dataOperation)
		{		
			bool IsValid = true;

			try
			{
				QSPForm.Business.FormSystem formSys = new QSPForm.Business.FormSystem();
				IsValid = formSys.PerformValidation(dtsAccount, dtsForm, UserID, dataOperation);						
				DataRow accRow = dtsAccount.Account.Rows[0];
				accRow[dataDef.FLD_IS_VALIDATION_PERFORMED] = true;
			}
			catch(Exception ex)
			{
				throw ex;
			}
			return IsValid;
		}
		private bool RefreshValidation(AccountData dts, FormData dtsForm, int UserID, int dataOperation, Data.ConnectionProvider connProvider)
		{
			bool HasChanged = false;
			try
			{
				//1 - Perform Account Exception First		
				DataRow accRow = dts.Account.Rows[0];
				//1 - Perform Order Exception
				bool IsValidationPerformed = false;
				if (!accRow.IsNull(dataDef.FLD_IS_VALIDATION_PERFORMED))
					IsValidationPerformed = Convert.ToBoolean(accRow[dataDef.FLD_IS_VALIDATION_PERFORMED]);
				//In a Refresh we only performed the validation if it's not already done
				//Otherwise we shoud use the Perform Method directly
				if (!IsValidationPerformed)
					PerformValidation(dts, dtsForm, UserID, dataOperation);			
			
				if (dts.AccountException.GetChanges() != null)
				{
					Data.Entity_exception excDataAccess = new Data.Entity_exception();
					if (connProvider != null)
						excDataAccess.MainConnectionProvider = connProvider;
					
					excDataAccess.UpdateBatch(dts.AccountException);
					HasChanged = true;
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}

			return HasChanged;
		}

		private bool RefreshTask(AccountData dts, FormData dtsForm, int UserID, int dataOperation, Data.ConnectionProvider connProvider)
		{
			bool IsSuccess = false;
			IsSuccess = PerformTask(dts, dtsForm, UserID, dataOperation, connProvider);

			return IsSuccess;
		}
		private bool PerformTask(AccountData dtsAccount, FormData dtsForm, int UserID, int dataOperation, Data.ConnectionProvider connProvider)
		{		
			bool IsSuccess = true;

			try
			{	
				QSPForm.Business.FormSystem formSys = new QSPForm.Business.FormSystem();
				IsSuccess = formSys.PerformTask(dtsAccount, dtsForm, UserID, dataOperation, connProvider);
			}
			catch(Exception ex)
			{
				throw ex;
			}
			return IsSuccess;
		}

        #endregion

	}
}
