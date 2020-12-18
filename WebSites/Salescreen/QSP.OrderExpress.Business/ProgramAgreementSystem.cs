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
using dataDef = QSPForm.Common.DataDef.ProgramAgreementTable;
using dataAccessRef = QSPForm.Data.ProgramAgreement;

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
    ///     This class contains the business rules that are used for a Program Agreement
    /// </summary>
    public class ProgramAgreementSystem : BusinessSystem
    {

        #region Version 2 code

        public List<ProgramAgreementSearchItem> Search(ProgramAgreementSearchParameters parameters)
        {
            List<ProgramAgreementSearchItem> result = new List<ProgramAgreementSearchItem>();

            LinqContext.OrderExpressDataContext db = new LinqContext.OrderExpressDataContext();
            //using (TransactionScope t = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted }))
            //{

                #region Base query

                var query = from pac in db.ProgramAgreementCampaigns
                            join paa in db.PostalAddressAccounts on pac.Campaign.Account.AccountId equals paa.AccountId
                            join fsm in db.FieldSalesManagers on pac.Campaign.Account.FmId equals fsm.FmId
                            where pac.IsDeleted == false
                               && pac.Campaign.IsDeleted == false
                                //&& fsm.Deleted == false //it is commented because at line #227 it is already handled
                               && pac.Campaign.Account.IsDeleted == false
                               && pac.Program.IsDeleted == false
                               && pac.Program.ProgramType.Enabled == true
                               && paa.IsDeleted == false
                               && paa.PostalAddressTypeId == 1
                               && (pac.Campaign.Account.Organization.BusinessDivisionId == (int)BusinessDivisionEnum.US
                                    || pac.Campaign.Account.Organization.BusinessDivisionId == (int)BusinessDivisionEnum.EFR)
                            select new
                            {
                                StatusId = pac.ProgramAgreement.ProgramAgreementStatusId,
                                StatusCategoryId = pac.ProgramAgreement.ProgramAgreementStatus.StatusCategoryId,
                                StatusColorCode = pac.ProgramAgreement.ProgramAgreementStatus.ColorCode,
                                StatusShortDescription = pac.ProgramAgreement.ProgramAgreementStatus.ShortDescription,
                                ProgramAgreementId = pac.ProgramAgreement.ProgramAgreementId,
                                EDSProgramAgreementId = pac.ProgramAgreement.FulfProgramAgreementId,
                                AccountId = pac.Campaign.Account.AccountId,
                                EDSAccountId = pac.Campaign.Account.FulfAccountId,
                                AccountName = pac.Campaign.Account.AccountName,
                                FsmIsDeleted = fsm.Deleted,
                                FsmId = fsm.FmId,
                                FsmFirstName = fsm.FirstName,
                                FsmLastName = fsm.LastName,
                                ProgramId = pac.ProgramId,
                                ProgramName = pac.Program.ProgramName,
                                ProgramTypeId = pac.Program.ProgramTypeId,
                                ProgramTypeName = pac.Program.ProgramType.ProgramTypeName,
                                FormId = pac.ProgramAgreement.FormId,
                                FormName = pac.ProgramAgreement.Form.FormName,
                                Address1 = paa.PostalAddress.Address1,
                                AddressCity = paa.PostalAddress.City,
                                AddressSubdivisionCode = paa.PostalAddress.SubdivisionCode,
                                AddressZip = paa.PostalAddress.Zip,
                                CreateDate = pac.ProgramAgreement.CreateDate,
                                CreateUserId = pac.ProgramAgreement.CreateUserId,
                                FiscalYear = pac.Campaign.FiscalYear
                            };

                #endregion

                #region Filters

                if (parameters.SearchValue.Length > 0)
                {
                    if (parameters.SearchField == ProgramAgreementSearchFieldEnum.Any)
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
                                    || q.ProgramAgreementId == number
                                    || q.EDSProgramAgreementId == parameters.SearchValue
                                    select q;
                        }
                        else
                        {
                            query = from q in query
                                    where q.AddressCity.Contains(parameters.SearchValue)
                                    || q.AccountName.Contains(parameters.SearchValue)
                                    || q.AddressZip.Contains(parameters.SearchValue)
                                    || q.EDSProgramAgreementId == parameters.SearchValue
                                    select q;
                        }
                    }
                    else if (parameters.SearchField == ProgramAgreementSearchFieldEnum.City)
                    {
                        query = from q in query
                                where q.AddressCity.Contains(parameters.SearchValue)
                                select q;
                    }
                    else if (parameters.SearchField == ProgramAgreementSearchFieldEnum.Name)
                    {
                        query = from q in query
                                where q.AccountName.Contains(parameters.SearchValue)
                                select q;
                    }
                    else if (parameters.SearchField == ProgramAgreementSearchFieldEnum.NameBeginingWith)
                    {
                        query = from q in query
                                where q.AccountName.StartsWith(parameters.SearchValue)
                                select q;
                    }
                    else if (parameters.SearchField == ProgramAgreementSearchFieldEnum.QSPProgramAgreementId)
                    {
                        int number = 0;
                        bool isNumber = int.TryParse(parameters.SearchValue, out number);

                        if (isNumber)
                        {
                            query = from q in query
                                    where q.ProgramAgreementId == number
                                    select q;
                        }
                    }
                    else if (parameters.SearchField == ProgramAgreementSearchFieldEnum.EDSProgramAgreementId)
                    {
                        query = from q in query
                                where q.EDSProgramAgreementId == parameters.SearchValue
                                select q;
                    }
                    else if (parameters.SearchField == ProgramAgreementSearchFieldEnum.QSPAccountId)
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
                    else if (parameters.SearchField == ProgramAgreementSearchFieldEnum.EDSAccountId)
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
                    else if (parameters.SearchField == ProgramAgreementSearchFieldEnum.ZipCode)
                    {
                        query = from q in query
                                where q.AddressZip.Contains(parameters.SearchValue)
                                select q;
                    }
                }

                if (parameters.FormId.HasValue)
                {
                    query = from q in query
                            where q.FormId == parameters.FormId.Value
                            select q;
                }

                if (parameters.ProgramId.HasValue)
                {
                    query = from q in query
                            where q.ProgramId == parameters.ProgramId.Value
                            select q;
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
                            where q.StatusCategoryId == parameters.StatusCategoryId.Value
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
                                     ProgramAgreementId = q.ProgramAgreementId,
                                     EDSProgramAgreementId = q.EDSProgramAgreementId,
                                     AccountId = q.AccountId,
                                     EDSAccountId = q.EDSAccountId,
                                     AccountName = q.AccountName,
                                     FsmId = q.FsmId,
                                     FsmFirstName = q.FsmFirstName,
                                     FsmLastName = q.FsmLastName,
                                     ProgramId = q.ProgramId,
                                     ProgramName = q.ProgramName,
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
                            select new ProgramAgreementSearchItem
                            {
                                StatusId = q.StatusId,
                                StatusCategoryId = q.StatusCategoryId,
                                StatusColorCode = q.StatusColorCode,
                                StatusShortDescription = q.StatusShortDescription,
                                ProgramAgreementId = q.ProgramAgreementId,
                                EDSProgramAgreementId = q.EDSProgramAgreementId,
                                AccountId = q.AccountId,
                                EDSAccountId = q.EDSAccountId,
                                AccountName = q.AccountName,
                                FmId = q.FsmId,
                                FmFirstName = q.FsmFirstName,
                                FmLastName = q.FsmLastName,
                                ProgramId = q.ProgramId,
                                ProgramName = q.ProgramName,
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
                                new ProgramAgreementSearchItem
                                {
                                    StatusId = q.StatusId,
                                    StatusCategoryId = q.StatusCategoryId,
                                    StatusColorCode = q.StatusColorCode,
                                    StatusShortDescription = q.StatusShortDescription,
                                    ProgramAgreementId = q.ProgramAgreementId,
                                    EDSProgramAgreementId = q.EDSProgramAgreementId,
                                    AccountId = q.AccountId,
                                    EDSAccountId = q.EDSAccountId,
                                    AccountName = q.AccountName,
                                    FmId = q.FsmId,
                                    FmFirstName = q.FsmFirstName,
                                    FmLastName = q.FsmLastName,
                                    ProgramId = q.ProgramId,
                                    ProgramName = q.ProgramName,
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
                        select new ProgramAgreementSearchItem
                        {
                            StatusId = q.StatusId,
                            StatusCategoryId = q.StatusCategoryId,
                            StatusColorCode = q.StatusColorCode,
                            StatusShortDescription = q.StatusShortDescription,
                            ProgramAgreementId = q.ProgramAgreementId,
                            EDSProgramAgreementId = q.EDSProgramAgreementId,
                            AccountId = q.AccountId,
                            EDSAccountId = q.EDSAccountId,
                            AccountName = q.AccountName,
                            FmId = q.FsmId,
                            FmFirstName = q.FsmFirstName,
                            FmLastName = q.FsmLastName,
                            ProgramId = q.ProgramId,
                            ProgramName = q.ProgramName,
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
        public int SearchTotalRowCount(ProgramAgreementSearchParameters parameters)
        {
            int result = 0;

            LinqContext.OrderExpressDataContext db = new LinqContext.OrderExpressDataContext();

            #region Base query

            var query = from pac in db.ProgramAgreementCampaigns
                        join paa in db.PostalAddressAccounts on pac.Campaign.Account.AccountId equals paa.AccountId
                        join fsm in db.FieldSalesManagers on pac.Campaign.Account.FmId equals fsm.FmId
                        where pac.IsDeleted == false
                           && pac.Campaign.IsDeleted == false
                           && pac.Campaign.Account.IsDeleted == false
                           && pac.Program.IsDeleted == false
                           && pac.Program.ProgramType.Enabled == true
                           && paa.IsDeleted == false
                           && paa.PostalAddressTypeId == 1
                           && (pac.Campaign.Account.Organization.BusinessDivisionId == (int)BusinessDivisionEnum.US
                                || pac.Campaign.Account.Organization.BusinessDivisionId == (int)BusinessDivisionEnum.EFR)
                        select new
                        {
                            StatusId = pac.ProgramAgreement.ProgramAgreementStatusId,
                            StatusCategoryId = pac.ProgramAgreement.ProgramAgreementStatus.StatusCategoryId,
                            StatusColorCode = pac.ProgramAgreement.ProgramAgreementStatus.ColorCode,
                            StatusShortDescription = pac.ProgramAgreement.ProgramAgreementStatus.ShortDescription,
                            ProgramAgreementId = pac.ProgramAgreement.ProgramAgreementId,
                            EDSProgramAgreementId = pac.ProgramAgreement.FulfProgramAgreementId,
                            AccountId = pac.Campaign.Account.AccountId,
                            EDSAccountId = pac.Campaign.Account.FulfAccountId,
                            AccountName = pac.Campaign.Account.AccountName,
                            FsmIsDeleted = fsm.Deleted,
                            FsmId = fsm.FmId,
                            FsmFirstName = fsm.FirstName,
                            FsmLastName = fsm.LastName,
                            ProgramId = pac.ProgramId,
                            ProgramName = pac.Program.ProgramName,
                            ProgramTypeId = pac.Program.ProgramTypeId,
                            ProgramTypeName = pac.Program.ProgramType.ProgramTypeName,
                            FormId = pac.ProgramAgreement.FormId,
                            FormName = pac.ProgramAgreement.Form.FormName,
                            Address1 = paa.PostalAddress.Address1,
                            AddressCity = paa.PostalAddress.City,
                            AddressSubdivisionCode = paa.PostalAddress.SubdivisionCode,
                            AddressZip = paa.PostalAddress.Zip,
                            CreateDate = pac.ProgramAgreement.CreateDate,
                            CreateUserId = pac.ProgramAgreement.CreateUserId,
                            FiscalYear = pac.Campaign.FiscalYear
                        };

            #endregion

            #region Filters

            if (parameters.SearchValue.Length > 0)
            {
                if (parameters.SearchField == ProgramAgreementSearchFieldEnum.Any)
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
                                || q.ProgramAgreementId == number
                                || q.EDSProgramAgreementId == parameters.SearchValue
                                select q;
                    }
                    else
                    {
                        query = from q in query
                                where q.AddressCity.Contains(parameters.SearchValue)
                                || q.AccountName.Contains(parameters.SearchValue)
                                || q.AddressZip.Contains(parameters.SearchValue)
                                || q.EDSProgramAgreementId == parameters.SearchValue
                                select q;
                    }
                }
                else if (parameters.SearchField == ProgramAgreementSearchFieldEnum.City)
                {
                    query = from q in query
                            where q.AddressCity.Contains(parameters.SearchValue)
                            select q;
                }
                else if (parameters.SearchField == ProgramAgreementSearchFieldEnum.Name)
                {
                    query = from q in query
                            where q.AccountName.Contains(parameters.SearchValue)
                            select q;
                }
                else if (parameters.SearchField == ProgramAgreementSearchFieldEnum.NameBeginingWith)
                {
                    query = from q in query
                            where q.AccountName.StartsWith(parameters.SearchValue)
                            select q;
                }
                else if (parameters.SearchField == ProgramAgreementSearchFieldEnum.QSPProgramAgreementId)
                {
                    int number = 0;
                    bool isNumber = int.TryParse(parameters.SearchValue, out number);

                    if (isNumber)
                    {
                        query = from q in query
                                where q.ProgramAgreementId == number
                                select q;
                    }
                }
                else if (parameters.SearchField == ProgramAgreementSearchFieldEnum.EDSProgramAgreementId)
                {
                    query = from q in query
                            where q.EDSProgramAgreementId == parameters.SearchValue
                            select q;
                }
                else if (parameters.SearchField == ProgramAgreementSearchFieldEnum.QSPAccountId)
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
                else if (parameters.SearchField == ProgramAgreementSearchFieldEnum.EDSAccountId)
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
                else if (parameters.SearchField == ProgramAgreementSearchFieldEnum.ZipCode)
                {
                    query = from q in query
                            where q.AddressZip.Contains(parameters.SearchValue)
                            select q;
                }
            }

            if (parameters.FormId.HasValue)
            {
                query = from q in query
                        where q.FormId == parameters.FormId.Value
                        select q;
            }

            if (parameters.ProgramId.HasValue)
            {
                query = from q in query
                        where q.ProgramId == parameters.ProgramId.Value
                        select q;
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
                        where q.StatusCategoryId == parameters.StatusCategoryId.Value
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

        public EntityData.ProgramAgreementData GetProgramAgreement(int programAgreementId)
        {
            return this.GetProgramAgreement(programAgreementId, false);
        }
        public EntityData.ProgramAgreementData GetProgramAgreement(int programAgreementId, bool loadChildrenObjects)
        {
            EntityData.ProgramAgreementData result = new EntityData.ProgramAgreementData();

            LinqContext.OrderExpressDataContext db = new LinqContext.OrderExpressDataContext();

            #region Load program agreement data

            LinqEntity.ProgramAgreement programAgreement =
                (from pa in db.ProgramAgreements
                 where pa.ProgramAgreementId == programAgreementId
                 select pa
                 ).SingleOrDefault();

            result.Id = programAgreement.ProgramAgreementId;
            result.EdsId = programAgreement.FulfProgramAgreementId;
            result.TaxExemptionExpirationDate = programAgreement.TaxExemptionExpirationDate;
            result.TaxExemptionNumber = programAgreement.TaxExemptionNumber;
            result.Comments = programAgreement.Comments;

            result.FormId = programAgreement.FormId;
            result.StatusId = programAgreement.ProgramAgreementStatusId;

            result.StartDate = programAgreement.StartDate;
            result.EndDate = programAgreement.EndDate;
            result.HolidayEndDate = programAgreement.HolidayStartDate;
            result.HolidayStartDate = programAgreement.HolidayEndDate;
            result.Enrollment = programAgreement.Enrollment;
            result.GoalEstimatedGross = programAgreement.GoalEstimatedGross;
            result.AccountProfitRate = programAgreement.ProfitRate;
            result.RenewalSignupTerm = programAgreement.RenewalSignUpTerm;
            result.IsPriced = programAgreement.IsPriced;

            result.IsDeleted = programAgreement.IsDeleted;
            result.CreateDate = programAgreement.CreateDate;
            result.CreateUserId = programAgreement.CreateUserId;
            result.UpdateDate = programAgreement.UpdateDate;
            result.UpdateUserId = programAgreement.UpdateUserId;

            #endregion

            if (loadChildrenObjects)
            {
                #region Load extended organization data

                if (programAgreement.FormId.HasValue)
                {
                    result.FormName = programAgreement.Form.FormName;
                }


                result.StatusName = programAgreement.ProgramAgreementStatus.ProgramAgreementStatusName;
                result.StatusColor = programAgreement.ProgramAgreementStatus.ColorCode;


                LinqEntity.User createUser =
                    (from u in db.Users
                     where u.UserId == programAgreement.CreateUserId
                     select u
                     ).SingleOrDefault();

                if (createUser != null)
                {
                    result.CreateUserFirstName = createUser.FirstName;
                    result.CreateUserLastName = createUser.LastName;
                }


                if (programAgreement.UpdateUserId.HasValue)
                {
                    LinqEntity.User updateUser =
                        (from u in db.Users
                         where u.UserId == programAgreement.UpdateUserId.Value
                         select u
                         ).SingleOrDefault();

                    if (updateUser != null)
                    {
                        result.UpdateUserFirstName = updateUser.FirstName;
                        result.UpdateUserLastName = updateUser.LastName;
                    }
                }

                #endregion

                #region Load shipping addresses

                LinqEntity.PostalAddress shippingAddress = (
                    from paa in programAgreement.PostalAddressProgramAgreements
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
                    from pna in programAgreement.PhoneNumberProgramAgreements
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
                    from pna in programAgreement.PhoneNumberProgramAgreements
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
                    from ea in programAgreement.EmailProgramAgreements
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

                #region Campaign data

                result.Campaign = new EntityData.CampaignData();

                LinqEntity.Campaign lastCampaign =
                    (from pac in db.ProgramAgreementCampaigns
                     where pac.ProgramAgreementId == programAgreementId
                        && pac.IsDeleted == false
                     orderby pac.CreateDate descending
                     select pac.Campaign
                     ).FirstOrDefault();

                if (lastCampaign != null)
                {
                    result.Campaign.Id = lastCampaign.CampaignId;
                    result.Campaign.AccountId = lastCampaign.AccountId;
                    result.Campaign.FiscalYear = lastCampaign.FiscalYear;
                    result.Campaign.StartDate = lastCampaign.StartDate;
                    result.Campaign.EndDate = lastCampaign.EndDate;
                    result.Campaign.Enrollment = lastCampaign.Enrollment;
                    result.Campaign.GoalEstimatedGross = lastCampaign.GoalEstimatedGross;

                    result.Campaign.ProgramTypeId = lastCampaign.ProgramTypeId;
                    result.Campaign.ProgramTypeName = lastCampaign.ProgramType.ProgramTypeName;

                    result.Campaign.TradeClassId = lastCampaign.TradeClassId;
                    if (lastCampaign.TradeClassId.HasValue)
                    {
                        result.Campaign.TradeClassName = lastCampaign.TradeClass.TradeClassName;
                    }

                    result.Campaign.WarehouseId = lastCampaign.WarehouseId;
                    if (lastCampaign.WarehouseId.HasValue)
                    {
                        LinqEntity.Warehouse warehouse =
                            (from w in db.Warehouses
                             where w.WarehouseId == lastCampaign.WarehouseId.Value
                             select w
                             ).SingleOrDefault();

                        if (warehouse != null)
                        {
                            result.Campaign.WarehouseName = warehouse.WarehouseName;
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
                }

                #endregion

                #region Catalogs selected

                result.Catalogs = new List<EntityData.CatalogData>();

                result.Catalogs =
                    (from pac in db.ProgramAgreementCatalogs
                     where pac.ProgramAgreementId == programAgreementId
                     select new EntityData.CatalogData
                     {
                         Id = pac.Catalog.CatalogId,
                         CatalogGroupId = pac.Catalog.CatalogGroupId,
                         Code = pac.Catalog.CatalogCode,
                         Name = pac.Catalog.CatalogName,
                         Description = pac.Catalog.Description,
                         Culture = pac.Catalog.Culture,
                         IsPriced = pac.Catalog.IsPriced,
                         IsDeleted = pac.Catalog.IsDeleted,
                         CreateDate = pac.Catalog.CreateDate,
                         CreateUserId = pac.Catalog.CreateUserId,
                         UpdateDate = pac.Catalog.UpdateDate,
                         UpdateUserId = pac.Catalog.UpdateUserId
                     }
                     ).ToList();

                #endregion

                #region Status history

                StatusSystem statusSystem = new StatusSystem();
                result.StatusHistory = statusSystem.GetStatusHistoryFromProgramAgreement(result.Id);

                #endregion
            }

            return result;
        }

        public bool IsProgramAgreementEditable(int programAgreementId)
        {
            bool result = false;

            EntityData.ProgramAgreementData programAgreementData = this.GetProgramAgreement(programAgreementId);
            result = this.IsProgramAgreementEditable(programAgreementData);

            return result;
        }
        public bool IsProgramAgreementEditable(EntityData.ProgramAgreementData programAgreementData)
        {
            bool result = false;

            if (programAgreementData.StatusId >= 0 && programAgreementData.StatusId < 101)
            {
                result = true;
            }
            else if (programAgreementData.StatusId >= 9000)
            {
                result = true;
            }

            return result;
        }

        #endregion

        #region Version 1 code

        dataAccessRef prgDataAccess;

        public ProgramAgreementSystem()
        {
            prgDataAccess = new dataAccessRef();
        }
        public bool Insert(dataDef Table)
        {
            //We call a method from the inherit class, but the
            //validation with the overriden Validate Method 
            //is in the current class
            return this.Insert(Table, prgDataAccess);
        }

        public bool Update(dataDef Table)
        {
            //We call a method from the inherit class, but the
            //validation with the overriden Validate Method 
            //is in the current class
            return this.Update(Table, prgDataAccess);
        }

        public bool UpdateBatch(dataDef Table)
        {
            //We call a method from the inherit class, but the
            //validation with the overriden Validate Method 
            //is in the current class
            return this.UpdateBatch(Table, prgDataAccess);
        }

        public bool Delete(dataDef Table)
        {
            //We call a method from the inherit class, but the
            //validation with the overriden Validate Method 
            //is in the current class
            return this.Delete(Table, prgDataAccess);
        }

        //----------------------------------------------------------------
        // Function Validate:
        //   Validates ProgramAgreement row
        // Returns:
        //   true if validation is successful 
        //   false if invalid fields exist 
        // Parameters:
        //   [in]  row: DataRow to be validated
        //   [out] row: Returns row data.  If there are fields
        //              that contain errors they are individually marked.  
        //----------------------------------------------------------------
        protected override bool Validate(DataRow row)
        {
            bool isValid = true;

            //Clear all errors
            row.ClearErrors();

            if ((row.RowState == DataRowState.Modified) || (row.RowState == DataRowState.Added))
            {
                //Apply Mandatory rules
                isValid = IsValid_RequiredFields(row);
                //Apply Maxlength rules
                isValid &= IsValid_FieldsLength(row);
                //apply any other rules like unicity, integrity ...
                //Not for now
            }
            //Validation only for Delete Operation
            else if (row.RowState == DataRowState.Deleted)
            {
                isValid = IsValid_Integrity(row);
            }

            return isValid;
        }

        //----------------------------------------------------------------
        // Function IsValid_FieldLength:
        //   Validates a specific ProgramAgreement Ownership Table field against his maxlength 
        // Returns:
        //   False if field fails the validation rules.
        // Parameters:
        //   [in]  row: DataRow of DataTable to be validated
        //   [in]  fieldName: field in DataTable to be validated
        //   [in]  maxLen: max length for the field
        //----------------------------------------------------------------
        private bool IsValid_FieldsLength(DataRow row)
        {
            bool isValid = false;

            //No string variable to test
            isValid = true;


            return isValid;
        }


        //----------------------------------------------------------------
        // Function IsValid_RequiredField:
        //   Validates a specific DataTable field as Mandatory 
        // Returns:
        //   False if field fails the validation rules.
        // Parameters:
        //   [in]  row: DataRow from DataTable to be validated
        //----------------------------------------------------------------
        private bool IsValid_RequiredFields(DataRow row)
        {
            bool IsValid = true;
            //Start Date
            IsValid &= IsValid_RequiredField(row, dataDef.FLD_START_DATE, "Strat Date");
            //End Date
            IsValid &= IsValid_RequiredField(row, dataDef.FLD_END_DATE, "End Date");
            //STATUS
            IsValid &= IsValid_RequiredField(row, dataDef.FLD_PROGRAM_AGREEMENT_STATUS_ID, "PA Status");

            if (!IsValid)
            {
                messageManager.ValidationExceptionType = QSPFormExceptionType.RequiredFields;
            }

            return IsValid;
        }


        private bool IsValid_Unicity(DataRow row)
        {

            return true;

        }

        private bool IsValid_Integrity(DataRow row)
        {

            return true;

        }



        public dataDef SelectAllByFMID(string FMID)
        {
            dataDef dTbl;
            //
            // Get the user DataTable from the DataLayer
            //
            dTbl = prgDataAccess.SelectAllWfm_idLogic(FMID);

            return dTbl;
        }

        public dataDef SelectAllByProgramTypeID(int ProgramTypeID)
        {
            dataDef dTbl;
            //
            // Get the user DataTable from the DataLayer
            //
            dTbl = prgDataAccess.SelectAllWprogram_type_idLogic(ProgramTypeID);

            return dTbl;
        }

        public dataDef SelectAllByProgramID(int ProgramID)
        {
            dataDef dTbl;
            //
            // Get the user DataTable from the DataLayer
            //
            dTbl = prgDataAccess.SelectAllWprogram_idLogic(ProgramID);

            return dTbl;
        }

        public dataDef SelectAllByCampaignID(int CampaignID)
        {
            dataDef dTbl;
            //
            // Get the user DataTable from the DataLayer
            //
            dTbl = prgDataAccess.SelectAllWcampaign_idLogic(CampaignID);

            return dTbl;
        }


        public dataDef SelectOne(int ID)
        {
            dataDef dTbl;
            //
            // Get the user DataTable from the DataLayer
            //
            dTbl = prgDataAccess.SelectOne(ID);

            return dTbl;

        }

        public ProgramAgreementData SelectAllDetail(int ID)
        {
            return prgDataAccess.SelectAllDetail(ID);
        }

        public bool UpdateAllDetail(ProgramAgreementData dts, int UserID)
        {
            //Variable to handle the operation in One transaction transaction
            String TransactionName = "ProgramAgreement_UpdateAllDetail";
            Data.ConnectionProvider connProvider = new Data.ConnectionProvider();

            bool IsSuccess = true;
            try
            {
                //This method fill the All Data needed for an organization
                //into a predefined DataSet

                //Data Object Instanciation
                Data.ProgramAgreement prgDataAccess = new Data.ProgramAgreement();
                Data.Campaign campaignDataAccess = new QSPForm.Data.Campaign();
                Data.ProgramAgreementCampaign prgCampDataAccess = new Data.ProgramAgreementCampaign();
                Data.Postal_address_entity postDataAccess = new Data.Postal_address_entity();
                Data.Phone_number_entity phoneDataAccess = new Data.Phone_number_entity();
                Data.Email_entity emailDataAccess = new Data.Email_entity();
                Data.ProgramAgreement_status_change prgStatusDataAccess = new Data.ProgramAgreement_status_change();
                Data.ProgramAgreementCatalog prgCatalogDataAccess = new Data.ProgramAgreementCatalog();

                // Pass the created ConnectionProvider object to the data-access objects.
                prgDataAccess.MainConnectionProvider = connProvider;
                campaignDataAccess.MainConnectionProvider = connProvider;
                prgCampDataAccess.MainConnectionProvider = connProvider;
                postDataAccess.MainConnectionProvider = connProvider;
                phoneDataAccess.MainConnectionProvider = connProvider;
                emailDataAccess.MainConnectionProvider = connProvider;
                prgStatusDataAccess.MainConnectionProvider = connProvider;
                prgCatalogDataAccess.MainConnectionProvider = connProvider;

                connProvider.OpenConnection();
                connProvider.BeginTransaction(TransactionName);

                bool HasChanges = false;
                int ProgramAgreementID = 0;
                DataRow prgRow = dts.ProgramAgreement.Rows[0];
                ProgramAgreementID = Convert.ToInt32(prgRow[dataDef.FLD_PKID]);

                //***CHECK FOR CONCURENTIAL MODIFICATION
                ProgramAgreementTable prgVersion = prgDataAccess.SelectOne(ProgramAgreementID);
                if (prgVersion.Rows.Count > 0)
                {
                    DataRow prgVersionRow = prgVersion.Rows[0];
                    if (Convert.ToDateTime(prgVersionRow[dataDef.FLD_UPDATE_DATE]) != Convert.ToDateTime(prgRow[dataDef.FLD_UPDATE_DATE]))
                    {
                        messageManager.HeaderText = "System Error";
                        messageManager.ValidationExceptionType = QSPFormExceptionType.RecordIsModified;
                        messageManager.SetErrorMessage(messageManager.FormatErrorMessage(QSPFormMessage.CONCURENT_RECORD_MODIFIED, "Program Agreement"));

                        throw new QSPFormValidationException(messageManager);
                    }
                }
                else
                {
                    messageManager.HeaderText = "System Error";
                    messageManager.ValidationExceptionType = QSPFormExceptionType.RecordIsDeleted;
                    messageManager.SetErrorMessage(messageManager.FormatErrorMessage(QSPFormMessage.CONCURENT_RECORD_DELETED, "Program Agreement"));

                    throw new QSPFormValidationException(messageManager);
                }

                if (dts.Campaign.GetChanges() != null)
                {
                    campaignDataAccess.UpdateBatch(dts.Campaign);
                }

                //Campaign
                if (dts.ProgramAgreementCampaign.GetChanges() != null)
                {
                    //if the account tax information is changed
                    //we have to refresh the campaign information
                    prgCampDataAccess.UpdateBatch(dts.ProgramAgreementCampaign);
                    HasChanges = true;
                }

                PrepareTransactionWithNewID(dts);

                //Postal Address
                if (dts.PostalAddress.GetChanges() != null)
                {
                    //if the account postal address is changed
                    //we have to refresh the campaign information
                    postDataAccess.UpdateBatch(dts.PostalAddress);
                    HasChanges = true;

                }
                //Phone Number
                if (dts.PhoneNumber.GetChanges() != null)
                {
                    phoneDataAccess.UpdateBatch(dts.PhoneNumber);
                    HasChanges = true;
                }
                //Email Addess
                if (dts.EmailAddress.GetChanges() != null)
                {
                    emailDataAccess.UpdateBatch(dts.EmailAddress);
                    HasChanges = true;
                }

                if (dts.ProgramAgreementCatalog.GetChanges() != null)
                {
                    prgCatalogDataAccess.UpdateBatch(dts.ProgramAgreementCatalog);
                    HasChanges = true;
                }

                //******SAVING THE GLOBAL UPDATE DATE IN ORDER HEADER TABLE
                if (HasChanges && dts.ProgramAgreement.GetChanges() == null)
                {
                    //If no change have been made in the main table
                    //but in at least one sattelite table
                    //a change have been made, we have to update the main table
                    prgRow[dataDef.FLD_UPDATE_USER_ID] = UserID;
                }

                //Submit information for ProgramAgreement at the end
                //to consider in a global way any changes from all tables
                if (prgRow.IsNull(dataDef.FLD_PROGRAM_AGREEMENT_STATUS_ID) || Convert.ToInt32(prgRow[dataDef.FLD_PROGRAM_AGREEMENT_STATUS_ID]) >= Convert.ToInt32(ProgramAgreementStatus.Exported))
                {
                    prgRow[dataDef.FLD_PROGRAM_AGREEMENT_STATUS_ID] = (int)ProgramAgreementStatus.InProcess;
                }

                //This method fill the All Data needed for an organization
                //into a predefined DataSet			
                if (dts.ProgramAgreement.GetChanges() != null)
                {
                    prgDataAccess.UpdateBatch(dts.ProgramAgreement);
                    HasChanges = true;
                }

                //We need to put the Refresh after
                //Exception and Task
                Refresh(dts, UserID, DataOperation.UPDATE, connProvider);

                //Register the update in the order_status_change table.
                ProgramAgreementStatusChangeTable dTblChange = new ProgramAgreementStatusChangeTable();
                DataRow newChangerRow = dTblChange.NewRow();

                newChangerRow[ProgramAgreementStatusChangeTable.FLD_PROGRAM_AGREEMENT_ID] = prgRow[dataDef.FLD_PKID];
                newChangerRow[ProgramAgreementStatusChangeTable.FLD_PROGRAM_AGREEMENT_STATUS_ID] = prgRow[dataDef.FLD_PROGRAM_AGREEMENT_STATUS_ID];
                newChangerRow[ProgramAgreementStatusChangeTable.FLD_STATUS_CHANGE_REASON] = "Update from Order Express";
                newChangerRow[ProgramAgreementStatusChangeTable.FLD_CREATE_USER_ID] = UserID;
                dTblChange.Rows.Add(newChangerRow);
                //Execute the chnage to DB
                prgStatusDataAccess.Insert(dTblChange);

                //Commit transaction 
                connProvider.CommitTransaction();
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

        public dataDef SelectAll_Search(int SearchType, String Criteria, int ProgramType, string SubdivisionCode, string FMID, int FSM_DisplayMode, int StatusCategoryID, string FMName)
        {
            dataDef dTbl;

            //
            // Get the user DataTable from the DataLayer
            //				
            dTbl = prgDataAccess.SelectAll_Search(SearchType, Criteria, ProgramType, SubdivisionCode, FMID, FSM_DisplayMode, StatusCategoryID, FMName);

            return dTbl;
        }

        public ProgramAgreementData InitializeProgramAgreement(int UserID, String FMID, int CampaignID, int ProgramID, int FormID)
        {
            //We prepare the DataSet for all step
            //Add a new Row
            ProgramAgreementData dts = new ProgramAgreementData();

            //Create a new Organization  row at start
            ProgramAgreementTable prgTable = dts.ProgramAgreement;
            DataRow row;
            row = prgTable.NewRow();
            row[ProgramAgreementTable.FLD_PROGRAM_AGREEMENT_STATUS_ID] = (int)ProgramAgreementStatus.InProcess;
            row[ProgramAgreementTable.FLD_FORM_ID] = FormID;
            row[ProgramAgreementTable.FLD_CREATE_USER_ID] = UserID;
            prgTable.Rows.Add(row);

            //Retreive Campaign Information
            Data.Campaign campDataAccess = new QSPForm.Data.Campaign();
            dts.Merge(campDataAccess.SelectOne(CampaignID));
            CampaignTable dtCamp = dts.Campaign;
            DataRow campRow = dtCamp.Rows[0];

            //Create a row for program agreement campaign at init
            ProgramAgreementCampaignTable dtProgCamp = dts.ProgramAgreementCampaign;
            DataRow prgCampRow = dtProgCamp.NewRow();
            prgCampRow[ProgramAgreementCampaignTable.FLD_PROGRAM_AGREEMENT_ID] = row[ProgramAgreementTable.FLD_PKID];
            prgCampRow[ProgramAgreementCampaignTable.FLD_CAMPAIGN_ID] = CampaignID;
            if (ProgramID > 0)
                prgCampRow[ProgramAgreementCampaignTable.FLD_PROGRAM_ID] = ProgramID;
            prgCampRow[ProgramAgreementCampaignTable.FLD_CREATE_USER_ID] = UserID;
            dtProgCamp.Rows.Add(prgCampRow);

            //Create a new Order Group row a start
            OrderGroupTable orderGroup = dts.OrderGroup;
            DataRow rowGrp;
            rowGrp = orderGroup.NewRow();
            rowGrp[OrderGroupTable.FLD_CREATE_USER_ID] = UserID;
            orderGroup.Rows.Add(rowGrp);

            //Create a new Order row a start
            OrderHeaderTable orderHeader = dts.OrderHeader;
            DataRow ordRow;
            ordRow = orderHeader.NewRow();
            ordRow[OrderHeaderTable.FLD_ORDER_GROUP_ID] = rowGrp[OrderGroupTable.FLD_PKID];
            ordRow[OrderHeaderTable.FLD_FM_ID] = FMID;
            ordRow[OrderHeaderTable.FLD_ORDER_TYPE_ID] = OrderType.SUPPLY;
            ordRow[OrderHeaderTable.FLD_FORM_ID] = FormID;
            ordRow[OrderHeaderTable.FLD_ORDER_STATUS_ID] = (int)ProgramAgreementStatus.InProcess;
            ordRow[OrderHeaderTable.FLD_CAMPAIGN_ID] = CampaignID;
            ordRow[OrderHeaderTable.FLD_SOURCE_ID] = 1; //Internal
            ordRow[OrderHeaderTable.FLD_CREATE_USER_ID] = UserID;
            ordRow[OrderHeaderTable.FLD_ORDER_DATE] = DateTime.Now;
            orderHeader.Rows.Add(ordRow);

            //Create One Row for the Shipment Group
            ShipmentGroupTable dTblShipmentGroup = dts.ShipmentGroup;
            DataRow rowShipGrp = dTblShipmentGroup.NewRow();
            rowShipGrp[ShipmentGroupTable.FLD_CREATE_USER_ID] = UserID;
            rowShipGrp[ShipmentGroupTable.FLD_SHIP_SUPPLY_ID] = -1;
            dTblShipmentGroup.Rows.Add(rowShipGrp);

            //Create One row for Validation
            ValidationTable dTblVal = dts.ProgramAgreementValidation;
            if (dTblVal.Rows.Count == 0)
            {
                dTblVal.Rows.Add(dTblVal.NewRow());
            }


            //Data.ProgramAgreementCatalog  catalogDataAccess = new Data.ProgramAgreementCatalog();
            //dts.Merge(catalogDataAccess.SelectAllProgramAgreementCatalogs(ProgramID,EntityType.TYPE_PROGRAM_AGREEMENT));           
            SetDefaultFormSupply(dts, UserID, true);

            SetDefaultInformation(dts, UserID);

            return dts;

        }

        public bool InsertAllDetail(ProgramAgreementData dts, int UserID)
        {
            //Variable to handle the operation in One transaction transaction
            String TransactionName = "ProgramAgreement_InsertAllDetail";
            Data.ConnectionProvider connProvider = new Data.ConnectionProvider();
            bool IsSuccess = true;

            try
            {
                //This method fill the All Data needed for an organization
                //into a predefined DataSet	
                //Data Object Instanciation
                Data.ProgramAgreement prgDataAccess = new Data.ProgramAgreement();
                Data.Campaign campaignDataAccess = new QSPForm.Data.Campaign();
                Data.ProgramAgreementCampaign prgCampDataAccess = new Data.ProgramAgreementCampaign();
                Data.Postal_address_entity postDataAccess = new Data.Postal_address_entity();
                Data.Phone_number_entity phoneDataAccess = new Data.Phone_number_entity();
                Data.Email_entity emailDataAccess = new Data.Email_entity();
                Data.ProgramAgreement_status_change prgStatusDataAccess = new Data.ProgramAgreement_status_change();
                Data.ProgramAgreementCatalog prgCatalogDataAccess = new Data.ProgramAgreementCatalog();

                // Pass the created ConnectionProvider object to the data-access objects.
                prgDataAccess.MainConnectionProvider = connProvider;
                campaignDataAccess.MainConnectionProvider = connProvider;
                prgCampDataAccess.MainConnectionProvider = connProvider;
                postDataAccess.MainConnectionProvider = connProvider;
                phoneDataAccess.MainConnectionProvider = connProvider;
                emailDataAccess.MainConnectionProvider = connProvider;
                prgStatusDataAccess.MainConnectionProvider = connProvider;
                prgCatalogDataAccess.MainConnectionProvider = connProvider;

                //****************BEGIN OPEN TRANSACTION PROCESS
                connProvider.OpenConnection();
                connProvider.BeginTransaction(TransactionName);
                //****************END OPEN TRANSACTION PROCESS

                DataRow prgRow = dts.ProgramAgreement.Rows[0];
                DataRow prgCampRow = dts.ProgramAgreementCampaign.Rows[0];
                int prgID = 0;
                //****************BEGIN PROG AGR PROCESS
                if (prgRow[dataDef.FLD_PROGRAM_AGREEMENT_STATUS_ID] == null || Convert.ToInt32(prgRow[dataDef.FLD_PROGRAM_AGREEMENT_STATUS_ID]) != Convert.ToInt32(ProgramAgreementStatus.SavedForLater))
                {
                    prgRow[dataDef.FLD_PROGRAM_AGREEMENT_STATUS_ID] = (int)ProgramAgreementStatus.InProcess;
                }
                prgDataAccess.UpdateBatch(dts.ProgramAgreement);
                prgID = Convert.ToInt32(dts.ProgramAgreement.Rows[0][ProgramAgreementTable.FLD_PKID]);

                prgCampRow[ProgramAgreementCampaignTable.FLD_PROGRAM_AGREEMENT_ID] = prgID;

                if (dts.Campaign.GetChanges() != null)
                {
                    campaignDataAccess.UpdateBatch(dts.Campaign);
                    prgCampRow[ProgramAgreementCampaignTable.FLD_CAMPAIGN_ID] = Convert.ToInt32(dts.Campaign.Rows[dts.Campaign.Rows.Count - 1][CampaignTable.FLD_PKID]);
                }
                //****************END PROG AGR PROCESS

                //****************BEGIN PROG AGR CAMPAIGN PROCESS
                prgCampDataAccess.UpdateBatch(dts.ProgramAgreementCampaign);
                //****************END PROG AGR PROCESS

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

                //PA Catalog
                if ((dts.ProgramAgreementCatalog.GetChanges(DataRowState.Deleted) != null) || (dts.ProgramAgreementCatalog.GetChanges(DataRowState.Added) != null))
                {
                    prgCatalogDataAccess.UpdateBatch(dts.ProgramAgreementCatalog);
                }

                //****************BEGIN REFRESH PROCESS
                //Exception and Task
                Refresh(dts, UserID, DataOperation.INSERT, connProvider);
                Debug.WriteLine("Refresh: " + DateTime.Now.ToLongTimeString());
                //****************END REFRESH PROCESS

                //Register the update in the order_status_change table.
                ProgramAgreementStatusChangeTable dTblChange = new ProgramAgreementStatusChangeTable();
                DataRow newChangerRow = dTblChange.NewRow();

                newChangerRow[ProgramAgreementStatusChangeTable.FLD_PROGRAM_AGREEMENT_ID] = prgRow[dataDef.FLD_PKID];
                newChangerRow[ProgramAgreementStatusChangeTable.FLD_PROGRAM_AGREEMENT_STATUS_ID] = prgRow[dataDef.FLD_PROGRAM_AGREEMENT_STATUS_ID];
                newChangerRow[ProgramAgreementStatusChangeTable.FLD_STATUS_CHANGE_REASON] = "Update from Order Express";
                newChangerRow[ProgramAgreementStatusChangeTable.FLD_CREATE_USER_ID] = UserID;
                dTblChange.Rows.Add(newChangerRow);
                //Execute the chnage to DB
                prgStatusDataAccess.Insert(dTblChange);

                //****************BEGIN COMMIT PROCESS
                //Commit transaction 
                connProvider.CommitTransaction();
                //****************BEGIN REFRESH PROCESS

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

            // Add Order Supply separately in order to avoid database locks between transactions
            if (dts.OrderSupply.TotalQuantity > 0)
            {
                // Read values.
                int FormId = (int)dts.ProgramAgreement.Rows[0][ProgramAgreementTable.FLD_FORM_ID];
                int CampaignId = (int)dts.ProgramAgreementCampaign.Rows[0][ProgramAgreementCampaignTable.FLD_CAMPAIGN_ID];
                int AccountId = (int)dts.Campaign.Rows[0][CampaignTable.FLD_ACCOUNT_ID];
                string FmId = (string)dts.Campaign.Rows[0][CampaignTable.FLD_FM_ID];

                #region Get fmid from account

                QSP.Business.Fulfillment.Account account = QSP.Business.Fulfillment.Account.GetAccount(AccountId);
                if (account != null)
                {
                    FmId = account.FmId;
                }

                #endregion

                // Create supply order.
                OrderSystem OrderSys = new OrderSystem();
                OrderData OrderDts = OrderSys.InitializeOrder(UserID, FmId, FormId, CampaignId);
                OrderDts.OrderHeader.Rows[0][OrderHeaderTable.FLD_ORDER_TYPE_ID] = 3;
                OrderDts.OrderDetail.Merge(dts.OrderSupply);

                // Merge shipment group info.
                OrderDts.ShipmentGroup.Rows[0][ShipmentGroupTable.FLD_SHIP_SUPPLY_TO] = dts.ShipmentGroup.Rows[0][ShipmentGroupTable.FLD_SHIP_SUPPLY_TO];
                OrderDts.ShipmentGroup.Rows[0][ShipmentGroupTable.FLD_REQUESTED_DELIVERY_DATE] = dts.ShipmentGroup.Rows[0][ShipmentGroupTable.FLD_REQUESTED_DELIVERY_DATE];
                OrderDts.ShipmentGroup.Rows[0][ShipmentGroupTable.FLD_SUPPLY_REQUESTED_DELIVERY_DATE] = dts.ShipmentGroup.Rows[0][ShipmentGroupTable.FLD_SUPPLY_REQUESTED_DELIVERY_DATE];
                OrderDts.ShipmentGroup.Rows[0][ShipmentGroupTable.FLD_DELIVERY_NLT] = dts.ShipmentGroup.Rows[0][ShipmentGroupTable.FLD_DELIVERY_NLT];

                // Set order billing address.
                // If shipping to Account(2), EntityType is 2. If shipping to FSM(1) or other(3), EntityType is 5.
                int ShippingEntityType = (int)OrderDts.ShipmentGroup.Rows[0][ShipmentGroupTable.FLD_SHIP_SUPPLY_TO] == 2 ? 2 : 5;
                OrderDts.OrderHeader.Rows[0][OrderHeaderTable.FLD_BILLING_POSTAL_ADDRESS_ID] = FindPostalAddress(dts, 2, 1); // 1: Billing
                OrderDts.OrderHeader.Rows[0][OrderHeaderTable.FLD_BILLING_PHONE_NUMBER_ID] = FindPhoneNumber(dts, 2, 5); // 5: Billing Phone Number
                OrderDts.OrderHeader.Rows[0][OrderHeaderTable.FLD_BILLING_FAX_NUMBER_ID] = FindPhoneNumber(dts, 2, 6); // 6: Billing Fax
                OrderDts.OrderHeader.Rows[0][OrderHeaderTable.FLD_BILLING_EMAIL_ADDRESS_ID] = FindEmail(dts, 2, 4); // 4: Billing Corporative

                // Set shipment group address.
                OrderDts.ShipmentGroup.Rows[0][ShipmentGroupTable.FLD_SHIPPING_POSTAL_ADDRESS_ID] = FindPostalAddress(dts, ShippingEntityType, 2); // 2: Shipping
                OrderDts.ShipmentGroup.Rows[0][ShipmentGroupTable.FLD_SHIPPING_PHONE_NUMBER_ID] = FindPhoneNumber(dts, ShippingEntityType, 7); // 7: Shipping Phone Number
                OrderDts.ShipmentGroup.Rows[0][ShipmentGroupTable.FLD_SHIPPING_FAX_NUMBER_ID] = FindPhoneNumber(dts, ShippingEntityType, 8); // 8: Billing Fax
                OrderDts.ShipmentGroup.Rows[0][ShipmentGroupTable.FLD_SHIPPING_EMAIL_ADDRESS_ID] = FindEmail(dts, ShippingEntityType, 5); // 5: Billing Corporative

                // Save to database.
                OrderSys.InsertAllDetail(OrderDts, UserID);

                int orderId = 0;
                DataRow orderRow = OrderDts.OrderHeader.Rows[0];
                bool isOrderIdParseSuccessful = Int32.TryParse(orderRow[OrderHeaderTable.FLD_PKID].ToString(), out orderId);

                dts.OrderHeader.Rows[0][OrderHeaderTable.FLD_PKID] = orderId;
            }

            return IsSuccess;
        }

        private object FindPostalAddress(ProgramAgreementData data, int entityTypeId, int postalAddressTypeId)
        {
            foreach (DataRow row in data.PostalAddress.Rows)
            {
                if (row.RowState != DataRowState.Deleted)
                {
                    if ((int)row[PostalAddressEntityTable.FLD_ENTITY_TYPE_ID] == entityTypeId && (int)row[PostalAddressEntityTable.FLD_TYPE] == postalAddressTypeId)
                        return (int)row[PostalAddressEntityTable.FLD_ADDRESS_ID];
                }
            }
            return DBNull.Value;
        }

        private object FindPhoneNumber(ProgramAgreementData data, int entityTypeId, int phoneNumberTypeId)
        {
            foreach (DataRow row in data.PhoneNumber.Rows)
            {
                if (row.RowState != DataRowState.Deleted)
                {
                    if ((int)row[PhoneNumberEntityTable.FLD_ENTITY_TYPE_ID] == entityTypeId && (int)row[PhoneNumberEntityTable.FLD_TYPE] == phoneNumberTypeId)
                        return (int)row[PhoneNumberEntityTable.FLD_PHONE_NUMBER_ID];
                }
            }
            return DBNull.Value;
        }

        private object FindEmail(ProgramAgreementData data, int entityTypeId, int emailTypeId)
        {
            foreach (DataRow row in data.EmailAddress.Rows)
            {
                if (row.RowState != DataRowState.Deleted)
                {
                    if ((int)row[EmailEntityTable.FLD_ENTITY_TYPE_ID] == entityTypeId && (int)row[EmailEntityTable.FLD_TYPE] == emailTypeId)
                        return (int)row[EmailEntityTable.FLD_EMAIL_ID];
                }
            }
            return DBNull.Value;
        }

        public bool ValidateDuplicatePA(ProgramAgreementData programAgreementData, DateTime startDate)
        {
            bool isValid = true;

            #region Get variables

            // Get the campaign id
            int campaignId = 0;
            if (programAgreementData.Campaign.Rows.Count > 0)
            {
                if (programAgreementData.Campaign.Rows[0].ItemArray[0] != null)
                {
                    campaignId = Convert.ToInt32(programAgreementData.Campaign.Rows[0].ItemArray[0]);
                }
            }

            // Get the form and program id
            int formId = programAgreementData.FormID;

            // Get the current fiscal year
            // BusinessCalendarSystem businessCalendarSystem = new BusinessCalendarSystem();
            // string currentFiscalYear = businessCalendarSystem.GetFiscalYear(startDate).ToString();

            #endregion

            #region Validate

            // Get the form data of the program agreement we want to create
            QSP.Business.Fulfillment.Form newPAForm = QSP.Business.Fulfillment.Form.GetForm(formId);

            List<QSP.Business.Fulfillment.ProgramAgreementCampaign> pacList = new List<QSP.Business.Fulfillment.ProgramAgreementCampaign>();
            if (newPAForm.ProgramId != null)
            {
                pacList = QSP.Business.Fulfillment.ProgramAgreementCampaign.GetProgramAgreementCampaignList(campaignId, (int)newPAForm.ProgramId);
            }

            if (pacList.Count > 0)
            {
                #region Validate the status of the program agreement

                foreach (QSP.Business.Fulfillment.ProgramAgreementCampaign pacItem in pacList)
                {
                    // Get the PA data
                    int paId = pacItem.ProgramAgreementId;
                    QSP.Business.Fulfillment.ProgramAgreement pacItemPA = QSP.Business.Fulfillment.ProgramAgreement.GetProgramAgreement(paId);

                    if (pacItemPA.FormId != null)
                    {
                        // Get the form data of the program agreement in the list
                        QSP.Business.Fulfillment.Form pacItemForm = QSP.Business.Fulfillment.Form.GetForm((int)pacItemPA.FormId);

                        // Validate PA on list against the one we want to create
                        if ((pacItemPA.ProgramAgreementStatusId == 101 ||
                                pacItemPA.ProgramAgreementStatusId == 201 ||
                                pacItemPA.ProgramAgreementStatusId == 301 ||
                                pacItemPA.ProgramAgreementStatusId == 302) &&
                                pacItemForm.IsBulk == newPAForm.IsBulk &&
                                pacItemForm.FormCode == newPAForm.FormCode)
                        {
                            // We have a valid PA for the campaign and program and bulk / pfs type

                            isValid = false;

                            throw new QSPFormValidationException("<br>A " + newPAForm.FormName + " PA for the same Fiscal Year is already on file for this Account.<br><br><br>");
                        }
                    }
                }

                #endregion
            }
            else
            {
                // We have no PAs for campaign and program
            }

            #endregion

            #region Old code

            // The problem with this code is that it only looked for a PA for all 
            // forms that belong to a program type
            // This limits an account / campaign to one PA per FY for all forms
            // We need one campaign to have one PA per FY for each form that needs it

            //BusinessCalendarSystem businessCalendarSystem = new BusinessCalendarSystem();
            //DataRow programAgreementRow = programAgreementData.ProgramAgreement.Rows[0];
            //DataRow campaignRow = programAgreementData.Campaign.Rows[0];

            //ProgramAgreementTable programAgreementTable = SelectAll_Search(7, campaignRow[CampaignTable.FLD_ACCOUNT_ID].ToString(), 7, String.Empty, String.Empty, 0, 0, String.Empty);

            //if (programAgreementTable.Select(ProgramAgreementTable.FLD_PKID + " <> " + programAgreementRow[ProgramAgreementTable.FLD_PKID].ToString() + " AND " +
            //    ProgramAgreementTable.FLD_FISCAL_YEAR + " = " + businessCalendarSystem.GetFiscalYear(startDate).ToString() + " AND " +
            //    ProgramAgreementTable.FLD_PROGRAM_AGREEMENT_STATUS_ID + " >= 100 AND " +
            //    ProgramAgreementTable.FLD_PROGRAM_AGREEMENT_STATUS_ID + " < 9000").Length > 0)
            //{
            //    throw new QSPFormValidationException("<br>A Frozen Food PA for the same Fiscal Year is already on file for this Account.<br><br><br>");
            //}

            #endregion

            return isValid;
        }

        private void PrepareTransactionWithNewID(ProgramAgreementData dts)
        {
            int NewID = Convert.ToInt32(dts.ProgramAgreement.Rows[0][ProgramAgreementTable.FLD_PKID]);

            foreach (DataRow row in dts.PostalAddress.Rows)
            {
                if (row.RowState == DataRowState.Added)
                {
                    if (row[PostalAddressEntityTable.FLD_ENTITY_TYPE_ID].ToString() == EntityType.TYPE_PROGRAM_AGREEMENT.ToString())
                        row[PostalAddressEntityTable.FLD_ENTITY_ID] = NewID;

                }
            }

            foreach (DataRow row in dts.PhoneNumber.Rows)
            {
                if (row.RowState == DataRowState.Added)
                {
                    if (row[PhoneNumberEntityTable.FLD_ENTITY_TYPE_ID].ToString() == EntityType.TYPE_PROGRAM_AGREEMENT.ToString())
                        row[PhoneNumberEntityTable.FLD_ENTITY_ID] = NewID;

                }
            }

            foreach (DataRow row in dts.EmailAddress.Rows)
            {
                if (row.RowState == DataRowState.Added)
                {
                    if (row[EmailEntityTable.FLD_ENTITY_TYPE_ID].ToString() == EntityType.TYPE_PROGRAM_AGREEMENT.ToString())
                        row[EmailEntityTable.FLD_ENTITY_ID] = NewID;
                }
            }

            foreach (DataRow row in dts.ProgramAgreementException.Rows)
            {
                if (row.RowState == DataRowState.Added)
                {
                    row[EntityExceptionTable.FLD_ENTITY_TYPE_ID] = EntityType.TYPE_PROGRAM_AGREEMENT;
                    row[EntityExceptionTable.FLD_ENTITY_ID] = NewID;
                }
            }
            foreach (DataRow row in dts.ProgramAgreementCatalog.Rows)
            {
                if (row.RowState == DataRowState.Added)
                {
                    row[ProgramAgreementCatalogTable.FLD_ENTITY_TYPE_ID] = EntityType.TYPE_PROGRAM_AGREEMENT;
                    row[ProgramAgreementCatalogTable.FLD_ENTITY_ID] = NewID;
                    row[ProgramAgreementCatalogTable.FLD_PROGRAM_AGREEMENT_ID] = NewID;
                }
                //if (row.RowState == DataRowState.Deleted)
                //{
                //    row[ProgramAgreementCatalogTable.FLD_ENTITY_TYPE_ID] = EntityType.TYPE_PROGRAM_AGREEMENT;
                //    row[ProgramAgreementCatalogTable.FLD_ENTITY_ID] = NewID;
                //}
            }

        }

        public void SetDefaultInformation(ProgramAgreementData dtsProgramAgreementData, int UserID)
        {
            ProgramAgreementCampaignTable dtblProgAgreeCamp = dtsProgramAgreementData.ProgramAgreementCampaign;
            DataRow prgCampRow = dtblProgAgreeCamp.Rows[0];
            ProgramAgreementTable dtblProgAgree = dtsProgramAgreementData.ProgramAgreement;
            DataRow prgRow = dtblProgAgree.Rows[0];
            DataRow campRow = dtsProgramAgreementData.Campaign.Rows[0];
            BusinessCalendarSystem businessCalendarSystem = new BusinessCalendarSystem();

            if (prgCampRow[ProgramAgreementCampaignTable.FLD_CAMPAIGN_ID] != DBNull.Value)
            {
                int prgID = Convert.ToInt32(prgCampRow[ProgramAgreementCampaignTable.FLD_PROGRAM_AGREEMENT_ID]);
                int CampID = Convert.ToInt32(prgCampRow[ProgramAgreementCampaignTable.FLD_CAMPAIGN_ID]);
                if (CampID > 0)
                {
                    //COPY DEFAULT FROM CAMPAIGN TO PA HEADER
                    prgRow[ProgramAgreementTable.FLD_GOAL_ESTIMATED_GROSS] = campRow[CampaignTable.FLD_GOAL_ESTIMATED_GROSS];
                    prgRow[ProgramAgreementTable.FLD_ENROLLMENT] = campRow[CampaignTable.FLD_ENROLLMENT];
                    prgRow[ProgramAgreementTable.FLD_FM_ID] = campRow[CampaignTable.FLD_FM_ID];
                    prgRow[ProgramAgreementTable.FLD_FM_NAME] = campRow[CampaignTable.FLD_FM_NAME];
                    prgRow[ProgramAgreementTable.FLD_TAX_EXEMPTION_NO] = campRow[CampaignTable.FLD_TAX_EXEMPTION_NO];
                    prgRow[ProgramAgreementTable.FLD_TAX_EXEMPTION_EXP_DATE] = campRow[CampaignTable.FLD_TAX_EXEMPTION_EXP_DATE];

                    if (Convert.ToInt32(campRow[CampaignTable.FLD_FISCAL_YEAR]) >= businessCalendarSystem.GetFiscalYear() &&
                        ((DateTime)campRow[CampaignTable.FLD_END_DATE]) >= DateTime.Now)
                    {
                        prgRow[ProgramAgreementTable.FLD_START_DATE] = campRow[CampaignTable.FLD_START_DATE];
                        prgRow[ProgramAgreementTable.FLD_END_DATE] = campRow[CampaignTable.FLD_END_DATE];
                    }
                    else
                    {
                        prgRow[ProgramAgreementTable.FLD_START_DATE] = DBNull.Value;
                        prgRow[ProgramAgreementTable.FLD_END_DATE] = DBNull.Value;
                    }

                    // Only copy shipping address
                    PostalAddressSystem postSys = new PostalAddressSystem();
                    PostalAddressEntityTable postTbl = postSys.SelectAllByCampaignID(CampID);
                    postSys.CopyToEntity(postTbl, dtsProgramAgreementData.PostalAddress, UserID,
                        EntityType.TYPE_CAMPAIGN, CampID, PostalAddressType.TYPE_SHIPPING,
                        EntityType.TYPE_PROGRAM_AGREEMENT, prgID, PostalAddressType.TYPE_SHIPPING);

                    PhoneNumberSystem phoneSys = new PhoneNumberSystem();
                    PhoneNumberEntityTable phoneTbl = phoneSys.SelectAllByCampaignID(CampID);
                    phoneSys.CopyToEntity(phoneTbl, dtsProgramAgreementData.PhoneNumber, UserID,
                        EntityType.TYPE_CAMPAIGN, CampID, PhoneNumberType.TYPE_SHIPPING_PHONE,
                        EntityType.TYPE_PROGRAM_AGREEMENT, prgID, PhoneNumberType.TYPE_SHIPPING_PHONE);

                    EmailAddressSystem emailSys = new EmailAddressSystem();
                    EmailEntityTable emailTbl = emailSys.SelectAllByCampaignID(CampID);
                    emailSys.CopyToEntity(emailTbl, dtsProgramAgreementData.EmailAddress, UserID,
                        EntityType.TYPE_CAMPAIGN, CampID, EmailType.TYPE_SHIPPING,
                        EntityType.TYPE_PROGRAM_AGREEMENT, prgID, EmailType.TYPE_SHIPPING);

                    // Load account billing address
                    if (dtsProgramAgreementData.Campaign.Rows.Count > 0)
                    {
                        dtsProgramAgreementData.Merge(postSys.SelectAllByEntityID(EntityType.TYPE_ACCOUNT, (int)dtsProgramAgreementData.Campaign.Rows[0][CampaignTable.FLD_ACCOUNT_ID]));
                        dtsProgramAgreementData.Merge(phoneSys.SelectAllByEntityID(EntityType.TYPE_ACCOUNT, (int)dtsProgramAgreementData.Campaign.Rows[0][CampaignTable.FLD_ACCOUNT_ID]));
                        dtsProgramAgreementData.Merge(emailSys.SelectAllByEntityID(EntityType.TYPE_ACCOUNT, (int)dtsProgramAgreementData.Campaign.Rows[0][CampaignTable.FLD_ACCOUNT_ID]));
                    }
                }
            }

        }

        //************************************************************************//
        //				  REFRESH --BUSINESS EXCEPTION AND TASK 				  //
        //************************************************************************//
        internal bool Refresh(int ProgramAgreementID, int UserID)
        {
            bool IsSuccess = false;

            ProgramAgreementData dts = SelectAllDetail(ProgramAgreementID);
            Refresh(dts, UserID, QSPForm.Common.DataOperation.UPDATE, null);

            return IsSuccess;

        }

        internal bool Refresh(int ProgramAgreementID, int UserID, Data.ConnectionProvider connProvider)
        {
            bool IsSuccess = false;
            Data.ProgramAgreement prgDataAccess = new QSPForm.Data.ProgramAgreement();
            if (connProvider != null)
                prgDataAccess.MainConnectionProvider = connProvider;
            ProgramAgreementData dts = prgDataAccess.SelectAllDetail(ProgramAgreementID);
            Refresh(dts, UserID, Common.DataOperation.UPDATE, connProvider);

            return IsSuccess;

        }

        private bool Refresh(ProgramAgreementData dts, int UserID, int dataOperation, Data.ConnectionProvider connProvider)
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
                if (HasChanged)
                {
                    OrderSystem ordSys = new OrderSystem();
                    //Cascading to the Order Level
                    int CampaignID = Convert.ToInt32(dts.Campaign.Rows[0][CampaignTable.FLD_PKID]);
                    OrderHeaderTable dTblOrder = ordSys.SelectAllByCampaignID(CampaignID);
                    foreach (DataRow ordRow in dTblOrder.Rows)
                    {
                        //int OrderID = Convert.ToInt32(ordRow[OrderHeaderTable.FLD_PKID]);
                        //HasChanged &= ordSys.Refresh(OrderID, dts, UserID, connProvider);					
                    }
                }
                Debug.WriteLine("Refresh Order: " + DateTime.Now.ToLongTimeString());
                tpDuration = DateTime.Now.Subtract(dStartProcess);
                Debug.WriteLine("Refresh Order Duration: " + tpDuration.TotalSeconds.ToString());
                //****************END REFRESH VALIDATION PROCESS
            }
            catch (Exception ex)
            {
                throw ex;
            }
            Debug.WriteLine("Refresh ProgramAgreement: " + DateTime.Now.ToLongTimeString());
            tpDuration = DateTime.Now.Subtract(dStart);
            Debug.WriteLine("Refresh ProgramAgreement Duration: " + tpDuration.TotalSeconds.ToString());

            return HasChanged;

        }

        //************************************************************************//
        //			  VALIDATION -- BUSINESS EXCEPTION -- ACCOUNT				  //
        //************************************************************************//
        public bool PerformValidation(ProgramAgreementData dtsProgramAgreement, int UserID, int dataOperation)
        {
            bool IsValid = true;

            try
            {
                int FormID = 0;
                if (!dtsProgramAgreement.IsFormIDNull)
                {
                    FormID = dtsProgramAgreement.FormID;
                }
                QSPForm.Business.FormSystem formSys = new QSPForm.Business.FormSystem();
                FormData dtsForm = formSys.SelectAllDetail(FormID, true);

                IsValid = PerformValidation(dtsProgramAgreement, dtsForm, UserID, dataOperation);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return IsValid;
        }

        private bool PerformValidation(ProgramAgreementData dtsProgramAgreement, FormData dtsForm, int UserID, int dataOperation)
        {
            bool IsValid = true;

            try
            {
                QSPForm.Business.FormSystem formSys = new QSPForm.Business.FormSystem();
                IsValid = formSys.PerformValidation(dtsProgramAgreement, dtsForm, UserID, dataOperation);
                DataRow accRow = dtsProgramAgreement.ProgramAgreement.Rows[0];
                accRow[dataDef.FLD_IS_VALIDATION_PERFORMED] = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return IsValid;
        }

        private bool RefreshValidation(ProgramAgreementData dts, FormData dtsForm, int UserID, int dataOperation, Data.ConnectionProvider connProvider)
        {
            bool HasChanged = false;
            try
            {
                //1 - Perform ProgramAgreement Exception First		
                DataRow accRow = dts.ProgramAgreement.Rows[0];
                //1 - Perform Order Exception
                bool IsValidationPerformed = false;
                if (!accRow.IsNull(dataDef.FLD_IS_VALIDATION_PERFORMED))
                    IsValidationPerformed = Convert.ToBoolean(accRow[dataDef.FLD_IS_VALIDATION_PERFORMED]);
                //In a Refresh we only performed the validation if it's not already done
                //Otherwise we shoud use the Perform Method directly
                if (!IsValidationPerformed)
                    PerformValidation(dts, dtsForm, UserID, dataOperation);

                if (dts.ProgramAgreementException.GetChanges() != null)
                {
                    Data.Entity_exception excDataAccess = new Data.Entity_exception();
                    if (connProvider != null)
                        excDataAccess.MainConnectionProvider = connProvider;

                    excDataAccess.UpdateBatch(dts.ProgramAgreementException);
                    HasChanged = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return HasChanged;
        }

        //************************************************************************//
        //					BUSINESS TASK -- ACCOUNT							  //
        //************************************************************************//
        private bool RefreshTask(ProgramAgreementData dts, FormData dtsForm, int UserID, int dataOperation)
        {
            return false;// RefreshTask(dts, dtsForm, UserID, dataOperation, null);
        }

        private bool RefreshTask(ProgramAgreementData dts, FormData dtsForm, int UserID, int dataOperation, Data.ConnectionProvider connProvider)
        {
            bool IsSuccess = false;
            //IsSuccess = PerformTask(dts, dtsForm, UserID, dataOperation, connProvider);

            return IsSuccess;
        }

        private bool PerformTask(ProgramAgreementData dtsProgramAgreement, FormData dtsForm, int UserID, int dataOperation, Data.ConnectionProvider connProvider)
        {
            bool IsSuccess = true;

            try
            {
                QSPForm.Business.FormSystem formSys = new QSPForm.Business.FormSystem();
                IsSuccess = formSys.PerformTask(dtsProgramAgreement, dtsForm, UserID, dataOperation, connProvider);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return IsSuccess;
        }



        public ProgramAgreementStatusChangeTable SelectAllProgramAgreementStatusChangeByProgramAgreementID(int ProgramAgreementID)
        {
            ProgramAgreementStatusChangeTable dTbl;
            //
            // Get the user DataTable from the DataLayer
            //
            Data.ProgramAgreement_status_change prgStatusDataAccess = new Data.ProgramAgreement_status_change();
            dTbl = prgStatusDataAccess.SelectAllWprogram_agreement_idLogic(ProgramAgreementID);

            return dTbl;

        }

        public void SetDefaultFormSupply(ProgramAgreementData dtsProgramAgreementData, int UserID, bool IncludeAllFormProduct)
        {
            int FormID = Convert.ToInt32(dtsProgramAgreementData.ProgramAgreement.Rows[0][OrderHeaderTable.FLD_FORM_ID]);
            int OrderID = Convert.ToInt32(dtsProgramAgreementData.OrderHeader.Rows[0][OrderHeaderTable.FLD_PKID]);
            int ShipGrpID = Convert.ToInt32(dtsProgramAgreementData.ShipmentGroup.Rows[0][ShipmentGroupTable.FLD_SHIP_SUPPLY_ID]);
            OrderSystem ordSys = new OrderSystem();
            ordSys.SetDefaultFormSupply(dtsProgramAgreementData.OrderSupply, OrderID, ShipGrpID, FormID, UserID, IncludeAllFormProduct);
        }

        public void SetDefaultShippingSupplyPostalAddress(ProgramAgreementData dtsProgramAgreementData, int UserID)
        {
            //Copy the Address info from the Standard Order Shipping address			
            int ShipGrpID = Convert.ToInt32(dtsProgramAgreementData.ShipmentGroup.Rows[0][ShipmentGroupTable.FLD_PKID]);
            int PrgID = Convert.ToInt32(dtsProgramAgreementData.ProgramAgreement.Rows[0][ProgramAgreementTable.FLD_PKID]);

            PostalAddressEntityTable dtblAddress = dtsProgramAgreementData.PostalAddress;

            PostalAddressSystem postSys = new PostalAddressSystem();
            postSys.CopyToEntity(dtblAddress, UserID,
                EntityType.TYPE_PROGRAM_AGREEMENT, PrgID, PostalAddressType.TYPE_SHIPPING,
                EntityType.TYPE_ORDER_SHIPPING, ShipGrpID, PostalAddressType.TYPE_SHIPPING);

        }

        public void SetDefaultShippingSupplyPhoneNumber(ProgramAgreementData dtsProgramAgreementData, int UserID)
        {
            PhoneNumberEntityTable dtblPhoneNumber = dtsProgramAgreementData.PhoneNumber;
            ShipmentGroupTable dTblShipmentGroup = dtsProgramAgreementData.ShipmentGroup;
            DataRow rowShipGrp = dTblShipmentGroup.Rows[0];

            int ShipGrpID = Convert.ToInt32(rowShipGrp[ShipmentGroupTable.FLD_PKID]);
            int PrgID = Convert.ToInt32(dtsProgramAgreementData.ProgramAgreement.Rows[0][ProgramAgreementTable.FLD_PKID]);

            //Phone Number
            PhoneNumberSystem phoneSys = new PhoneNumberSystem();
            phoneSys.CopyToEntity(dtblPhoneNumber, UserID,
                EntityType.TYPE_PROGRAM_AGREEMENT, PrgID, PhoneNumberType.TYPE_SHIPPING_PHONE,
                EntityType.TYPE_ORDER_SHIPPING, ShipGrpID, PhoneNumberType.TYPE_SHIPPING_PHONE);

            phoneSys.CopyToEntity(dtblPhoneNumber, UserID,
                EntityType.TYPE_PROGRAM_AGREEMENT, PrgID, PhoneNumberType.TYPE_SHIPPING_FAX,
                EntityType.TYPE_ORDER_SHIPPING, ShipGrpID, PhoneNumberType.TYPE_SHIPPING_FAX);

        }

        public void SetDefaultShippingSupplyEmailAddress(ProgramAgreementData dtsProgramAgreementData, int UserID)
        {
            ShipmentGroupTable dTblShipmentGroup = dtsProgramAgreementData.ShipmentGroup;
            DataRow rowShipGrp = dTblShipmentGroup.Rows[0];

            int ShipGrpID = Convert.ToInt32(rowShipGrp[ShipmentGroupTable.FLD_PKID]);
            int PrgID = Convert.ToInt32(dtsProgramAgreementData.ProgramAgreement.Rows[0][ProgramAgreementTable.FLD_PKID]);

            EmailEntityTable dTblEmailAddress = dtsProgramAgreementData.EmailAddress;

            EmailAddressSystem emailSys = new EmailAddressSystem();
            emailSys.CopyToEntity(dTblEmailAddress, UserID,
                EntityType.TYPE_PROGRAM_AGREEMENT, PrgID, EmailType.TYPE_SHIPPING,
                EntityType.TYPE_ORDER_SHIPPING, ShipGrpID, EmailType.TYPE_SHIPPING);


        }

        public void SetFMShippingSupplyPostalAddress(ProgramAgreementData dtsProgramAgreementData, int UserID)
        {
            ShipmentGroupTable dTblShipmentGroup = dtsProgramAgreementData.ShipmentGroup;
            DataRow rowShipGrp = dTblShipmentGroup.Rows[0];
            //Copy the Address info from the Standard Order Shipping address			
            int ShipGrpID = Convert.ToInt32(rowShipGrp[ShipmentGroupTable.FLD_PKID]);

            QSPForm.Business.CUserSystem fmSys = new CUserSystem();
            CUserTable cuser = fmSys.SelectOne(dtsProgramAgreementData.Campaign.Rows[0][CampaignTable.FLD_FM_ID].ToString());

            PostalAddressEntityTable dtblAddress = dtsProgramAgreementData.PostalAddress;
            DataView DVShippingSupply = new DataView(dtblAddress);

            DVShippingSupply.RowFilter = PostalAddressEntityTable.FLD_ENTITY_TYPE_ID + " = " + EntityType.TYPE_ORDER_SHIPPING.ToString()
                + " AND " + PostalAddressEntityTable.FLD_TYPE + " = " + PostalAddressType.TYPE_SHIPPING.ToString()
                + " AND " + PostalAddressEntityTable.FLD_ENTITY_ID + " = " + ShipGrpID.ToString();


            if (DVShippingSupply.Count == 0)
            {
                if (cuser.Rows.Count > 0)
                {
                    //Add a new Shipping Address as default
                    int AddrNewID = dtblAddress.Rows.Count;
                    DataRow row = dtblAddress.NewRow();

                    row[PostalAddressEntityTable.FLD_ADDRESS_ID] = AddrNewID;
                    row[PostalAddressEntityTable.FLD_ENTITY_ID] = ShipGrpID;
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
            SetFMShippingSupplyPhoneNumber(dtsProgramAgreementData, UserID, cuser);
            SetFMShippingSupplyEmailAddress(dtsProgramAgreementData, UserID, cuser);


        }


        public void SetFMShippingSupplyPhoneNumber(ProgramAgreementData dtsProgramAgreementData, int UserID, CUserTable cuser)
        {
            ShipmentGroupTable dTblShipmentGroup = dtsProgramAgreementData.ShipmentGroup;
            DataRow rowShipGrp = dTblShipmentGroup.Rows[0];
            int ShipGrpID = Convert.ToInt32(rowShipGrp[ShipmentGroupTable.FLD_PKID]);

            PhoneNumberEntityTable dtblPhoneNumber = dtsProgramAgreementData.PhoneNumber;
            DataView DVShippingSupply = new DataView(dtblPhoneNumber);

            //Phone Number			
            DVShippingSupply.RowFilter = PhoneNumberEntityTable.FLD_ENTITY_TYPE_ID + " = " + EntityType.TYPE_ORDER_SHIPPING
                + " AND " + PhoneNumberEntityTable.FLD_TYPE + " = " + PhoneNumberType.TYPE_SHIPPING_PHONE.ToString()
                + " AND " + PhoneNumberEntityTable.FLD_ENTITY_ID + " = " + ShipGrpID.ToString();

            if (DVShippingSupply.Count == 0) //Do the operation if never done
            {
                if (cuser.Rows.Count > 0)
                {
                    //Add a new Shipping Phone Number as default
                    int PhoneNewID = dtblPhoneNumber.Rows.Count;
                    DataRow row = dtblPhoneNumber.NewRow();

                    row[PhoneNumberEntityTable.FLD_PHONE_NUMBER_ID] = PhoneNewID;
                    row[PhoneNumberEntityTable.FLD_ENTITY_ID] = ShipGrpID;
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
                + " AND " + PhoneNumberEntityTable.FLD_ENTITY_ID + " = " + ShipGrpID.ToString();

            if (DVShippingSupply.Count == 0) //Do the operation if never done
            {
                if (cuser.Rows.Count > 0)
                {
                    //Add a new Shipping Phone Number as default
                    int PhoneNewID = dtblPhoneNumber.Rows.Count;
                    DataRow row = dtblPhoneNumber.NewRow();

                    row[PhoneNumberEntityTable.FLD_PHONE_NUMBER_ID] = PhoneNewID;
                    row[PhoneNumberEntityTable.FLD_ENTITY_ID] = ShipGrpID;
                    row[PhoneNumberEntityTable.FLD_ENTITY_TYPE_ID] = EntityType.TYPE_ORDER_SHIPPING; //Shipping;
                    row[PhoneNumberEntityTable.FLD_TYPE] = PhoneNumberType.TYPE_SHIPPING_FAX; //Fax
                    row[PhoneNumberEntityTable.FLD_PHONE_NUMBER] = cuser.Rows[0][CUserTable.FLD_FAX_PHONE];

                    row[PhoneNumberEntityTable.FLD_CREATE_USER_ID] = UserID;

                    dtblPhoneNumber.Rows.Add(row);



                }
            }

        }

        public void SetFMShippingSupplyEmailAddress(ProgramAgreementData dtsProgramAgreementData, int UserID, CUserTable cuser)
        {
            ShipmentGroupTable dTblShipmentGroup = dtsProgramAgreementData.ShipmentGroup;
            DataRow rowShipGrp = dTblShipmentGroup.Rows[0];
            int ShipGrpID = Convert.ToInt32(rowShipGrp[ShipmentGroupTable.FLD_PKID]);

            EmailEntityTable dTblEmailAddress = dtsProgramAgreementData.EmailAddress;
            DataView DVShippingSupply = new DataView(dTblEmailAddress);
            DVShippingSupply.RowFilter = EmailEntityTable.FLD_ENTITY_TYPE_ID + " = " + EntityType.TYPE_ORDER_BILLING
                + " AND " + EmailEntityTable.FLD_TYPE + " = " + EmailType.TYPE_SHIPPING.ToString()
                + " AND " + PhoneNumberEntityTable.FLD_ENTITY_ID + " = " + ShipGrpID.ToString();

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
                    newRow[EmailEntityTable.FLD_ENTITY_ID] = ShipGrpID;
                    newRow[EmailEntityTable.FLD_CREATE_USER_ID] = UserID;
                    dTblEmailAddress.Rows.Add(newRow);


                }
            }
        }

        public int GetAdditionalLeadTime(OrderData orderData)
        {
            int additionalLeadTime = 0;
            QSPForm.Data.ProgramAgreement_status_change programAgreementStatusChangeData = new QSPForm.Data.ProgramAgreement_status_change();
            ProgramAgreementTable programAgreementTable = SelectAllByCampaignID((int)orderData.OrderHeader.Rows[0][OrderHeaderTable.FLD_CAMPAIGN_ID]);
            DataRow[] programAgreementRows = programAgreementTable.Select(ProgramAgreementTable.FLD_PROGRAM_AGREEMENT_STATUS_ID + " IN (301, 302)");

            ProgramAgreementStatusChangeTable programAgreementStatusChangeTable = programAgreementStatusChangeData.SelectAllWprogram_agreement_idLogic((int)programAgreementRows[0][ProgramAgreementTable.FLD_PKID]);
            DataView programAgreementStatusChangeView = new DataView(programAgreementStatusChangeTable);
            programAgreementStatusChangeView.RowFilter = ProgramAgreementStatusChangeTable.FLD_PROGRAM_AGREEMENT_STATUS_ID + " IN (301, 302)";
            programAgreementStatusChangeView.Sort = ProgramAgreementStatusChangeTable.FLD_CREATE_DATE + " DESC";

            if (programAgreementStatusChangeView.Count > 0)
            {
                BusinessCalendarSystem businessCalendarSystem = new BusinessCalendarSystem();
                BusinessCalendarTable businessCalendarTable = businessCalendarSystem.SelectAll_Search((DateTime)programAgreementStatusChangeView[0][ProgramAgreementStatusChangeTable.FLD_CREATE_DATE], DateTime.Now, DateTime.Now);
                DataRow[] businessCalendarRows = businessCalendarTable.Select(
                    BusinessCalendarTable.FLD_IS_HOLIDAY + " = False AND " +
                    BusinessCalendarTable.FLD_IS_WEEKEND + " = False AND " +
                    BusinessCalendarTable.FLD_IS_CLOSED + " = False");

                if (businessCalendarRows.Length < 10)
                {
                    additionalLeadTime = 10 - businessCalendarRows.Length;
                }
            }

            return additionalLeadTime;
        }

        public void SetCancelStatus(ProgramAgreementData programAgreementData)
        {
            int programAgreementStatusID = 0;
            DataRow programAgreementRow = programAgreementData.ProgramAgreement.Rows[0];

            if (!programAgreementRow.IsNull(dataDef.FLD_PROGRAM_AGREEMENT_STATUS_ID))
            {
                programAgreementStatusID = Convert.ToInt32(programAgreementRow[dataDef.FLD_PROGRAM_AGREEMENT_STATUS_ID]);
            }

            if (programAgreementRow.IsNull(dataDef.FLD_FULF_PROGRAM_AGREEMENT_ID))
            {
                if (programAgreementStatusID >= 200 && programAgreementStatusID < 9000)
                {
                    programAgreementRow[dataDef.FLD_PROGRAM_AGREEMENT_STATUS_ID] = (int)Common.ProgramAgreementStatus.InProcessToBeCancelled;
                }
                else
                {
                    programAgreementRow[dataDef.FLD_PROGRAM_AGREEMENT_STATUS_ID] = (int)Common.ProgramAgreementStatus.Cancelled;
                }
            }
            else
            {
                programAgreementRow[dataDef.FLD_PROGRAM_AGREEMENT_STATUS_ID] = (int)Common.ProgramAgreementStatus.InProcessToBeCancelled;
            }
        }

        #endregion

    }
}
