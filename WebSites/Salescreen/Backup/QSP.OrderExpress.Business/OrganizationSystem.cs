using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Dynamic;
using System.Transactions;

using QSPForm.Common;
using QSPForm.Common.DataDef;
using QSPForm.Data;
using dataDef = QSPForm.Common.DataDef.OrganizationTable;
using dataAccessRef = QSPForm.Data.Organization;

using LinqContext = QSP.OrderExpress.Business.Context;
using LinqEntity = QSP.OrderExpress.Business.Entity;
using EntityData = QSP.OrderExpress.Common.Data;

using QSP.OrderExpress.Business.Validation;
using QSP.OrderExpress.Common.Comunication;
using QSP.OrderExpress.Common.Enum;
using QSP.OrderExpress.Common.Search;

namespace QSPForm.Business
{
	public class OrganizationSystem : BusinessSystem
    {
        #region Refactored code

        // This method will be deprecated, you should use the Search method instead
        public IEnumerable<LinqEntity.OrganizationList> SelectAll_Search(SearchSettings settings, ref int count)
        {
            LinqContext.OrderExpressDataContext db = new LinqContext.OrderExpressDataContext();

            var Query = (from o in db.Organizations
                         join ot in db.OrganizationTypes on o.OrganizationTypeId equals ot.OrganizationTypeId
                         join pjoin in db.PostalAddressOrganizations on o.OrganizationId equals pjoin.OrganizationId
                            into pinto
                         from p in pinto.DefaultIfEmpty()
                         join pajoin in db.PostalAddresses on p.PostalAddressId equals pajoin.PostalAddressId
                            into painto
                         from pa in painto.DefaultIfEmpty()
                         join fjoin in db.FieldSalesManagers on o.FmId equals fjoin.FmId
                            into finto
                         from f in finto.DefaultIfEmpty()

                         where o.IsDeleted == false &&
                            o.BusinessDivisionId == 1 &&
                            p.IsDeleted == false &&
                            p.PostalAddressTypeId == 1 &&
                            pa.IsDeleted == 0

                         select new
                         {
                             o.OrganizationId,
                             o.OrganizationTypeId,
                             o.OrganizationName,
                             ot.OrganizationTypeName,
                             o.FmId,
                             f.FirstName,
                             f.LastName,
                             pa.Address1,
                             pa.City,
                             pa.SubdivisionCode,
                             pa.Zip
                         });

            if (settings.OrganizationName.Length > 0)
                Query = Query.Where(a => a.OrganizationName.Contains(settings.OrganizationName));
            if (settings.City.Length > 0)
                Query = Query.Where(a => a.City.Contains(settings.City));
            if (settings.OrganizationId.Length > 0)
                Query = Query.Where(a => a.OrganizationId.ToString().Contains(settings.OrganizationId));
            if (settings.ZipCode.Length > 0)
                Query = Query.Where(a => a.Zip.Contains(settings.ZipCode));
            if (settings.FirstChar.Length > 0)
                Query = Query.Where(a => a.OrganizationName.StartsWith(settings.FirstChar));
            if (settings.OrganizationTypeId.HasValue)
                Query = Query.Where(a => a.OrganizationTypeId == settings.OrganizationTypeId);
            if (settings.SubdivisionCode.Length > 0)
                Query = Query.Where(a => a.SubdivisionCode == settings.SubdivisionCode);
            if (settings.FsmName.Length > 0)
                Query = Query.Where(a => (a.FirstName + " " + a.LastName + " " + a.FirstName).Contains(settings.FsmName));



            if (settings.DisplayMode == DisplayMode.All && settings.FsmId.Length > 0)
                Query = Query.Where(a => a.FmId.Contains(settings.FsmId));
            if (settings.DisplayMode == DisplayMode.Current)
                Query = Query.Where(a => a.FmId == settings.FsmId);
            if (settings.DisplayMode == DisplayMode.ChildrenOnly || settings.DisplayMode == DisplayMode.CurrentAndChildren)
            {
                LinqContext.QSPCommonDataContext dbCommon = new LinqContext.QSPCommonDataContext();
                List<string> FmTree = (from u in dbCommon.fnc_FMHierarchyList_FMID(settings.FsmId) select u.FMNumber).ToList();
                Query = Query.Where(a => FmTree.Contains(a.FmId));
            }
            if (settings.DisplayMode == DisplayMode.ChildrenOnly)
                Query = Query.Where(a => a.FmId != settings.FsmId);




            if (settings.Sort.Length > 0)
                Query = Query.OrderBy(settings.Sort);

            // Fill output parameter Count only if count is 0.
            if (count == 0)
            {
                count = Query.Count();
            }

            Properties.Settings localsettings = new Properties.Settings();
            if (!localsettings.UseDatabasePaging)
            {
                // Code paging.
                Query = Query.Take((settings.PageIndex + 1) * settings.PageSize);
                return Query.AsEnumerable().Skip(settings.PageIndex * settings.PageSize).Select(a => new LinqEntity.OrganizationList(a.OrganizationId, a.OrganizationTypeId, a.OrganizationName, a.OrganizationTypeName, a.FmId, a.FirstName, a.LastName, a.Address1, a.City, a.SubdivisionCode, a.Zip));
            }
            else
            {
                // Database paging. Not used until we switch to SQL Server 2005.
                // Note: if you want to use database paging with SQL Server 2000, you must add .Distinct()
                Query = Query.Skip(settings.PageIndex * settings.PageSize).Take(settings.PageSize);
                return Query.AsEnumerable().Select(a => new LinqEntity.OrganizationList(a.OrganizationId, a.OrganizationTypeId, a.OrganizationName, a.OrganizationTypeName, a.FmId, a.FirstName, a.LastName, a.Address1, a.City, a.SubdivisionCode, a.Zip));
            }

        }

        #endregion

        #region Version 2 code

        public List<OrganizationSearchItem> Search(OrganizationSearchParameters parameters)
        {
            List<OrganizationSearchItem> result = new List<OrganizationSearchItem>();

            LinqContext.OrderExpressDataContext db = new LinqContext.OrderExpressDataContext();

            #region Base query

            var query = from o in db.Organizations
                        join fsmjoin in db.FieldSalesManagers on o.FmId equals fsmjoin.FmId 
                            into fsmtemptable
                            from fsm in fsmtemptable.DefaultIfEmpty()
                        join pao in db.PostalAddressOrganizations on o.OrganizationId equals pao.OrganizationId
                        where  o.IsDeleted == false
                            && (o.BusinessDivisionId == (int)BusinessDivisionEnum.US
                                || o.BusinessDivisionId == (int)BusinessDivisionEnum.EFR)
                            && pao.IsDeleted == false
                            && pao.PostalAddressTypeId == 1
                            && pao.PostalAddress.IsDeleted == 0
                        select new
                        {
                            o.OrganizationId, 
                            o.OrganizationName, 
                            o.OrganizationType.OrganizationTypeId, 
                            o.OrganizationType.OrganizationTypeName, 
                            fsm.FmId, 
                            fsm.FirstName, 
                            fsm.LastName,
                            pao.PostalAddress.Address1,
                            pao.PostalAddress.City,
                            pao.PostalAddress.SubdivisionCode,
                            pao.PostalAddress.Zip
                        };

            #endregion

            #region Filters

            if (parameters.SearchValue.Length > 0)
            {
                if (parameters.SearchField == OrganizationSearchFieldEnum.Any)
                {
                    int number = 0;
                    bool isNumber = int.TryParse(parameters.SearchValue, out number);

                    if (isNumber)
                    {
                        query = from q in query
                                where q.City.Contains(parameters.SearchValue)
                                || q.OrganizationName.Contains(parameters.SearchValue)
                                || q.Zip.Contains(parameters.SearchValue)
                                || q.OrganizationId == number
                                select q;
                    }
                    else
                    {
                        query = from q in query
                                where q.City.Contains(parameters.SearchValue)
                                || q.OrganizationName.Contains(parameters.SearchValue)
                                || q.Zip.Contains(parameters.SearchValue)
                                select q;
                    }
                }
                else if (parameters.SearchField == OrganizationSearchFieldEnum.City)
                {
                    query = from q in query
                            where q.City.Contains(parameters.SearchValue)
                            select q;
                }
                else if (parameters.SearchField == OrganizationSearchFieldEnum.Name)
                {
                    query = from q in query
                            where q.OrganizationName.Contains(parameters.SearchValue)
                            select q;
                }
                else if (parameters.SearchField == OrganizationSearchFieldEnum.NameBeginingWith)
                {
                    query = from q in query
                            where q.OrganizationName.StartsWith(parameters.SearchValue)
                            select q;
                }
                else if (parameters.SearchField == OrganizationSearchFieldEnum.QSPOrganizationId)
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
                else if (parameters.SearchField == OrganizationSearchFieldEnum.ZipCode)
                {
                    query = from q in query
                            where q.Zip.Contains(parameters.SearchValue)
                            select q;
                }
            }

            if (parameters.OrganizationTypeId.HasValue)
            {
                query = from q in query
                        where q.OrganizationTypeId == parameters.OrganizationTypeId.Value
                        select q;
            }
            if (parameters.SubdivisionCode.Length > 0)
            {
                query = from q in query
                        where q.SubdivisionCode == parameters.SubdivisionCode
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
                            where q.FmId.Contains(parameters.FSMId)
                            select q;
                }
                if (parameters.FSMName.Length > 0)
                {
                    query = from q in query
                            where (q.FirstName + " " + q.LastName + " " + q.FirstName).Contains(parameters.FSMName)
                            select q;
                }
            }
            else if (parameters.SearchFSMOption == SearchFSMHierarchyOptionEnum.Own)
            {
                query = query.Where(q => q.FmId == parameters.FSMId);
            }
            else if (parameters.SearchFSMOption == SearchFSMHierarchyOptionEnum.Children)
            {
                LinqContext.QSPCommonDataContext dbCommon = new LinqContext.QSPCommonDataContext();
                List<string> FmTree = (from u in dbCommon.fnc_FMHierarchyList_FMID(parameters.FSMId) select u.FMNumber).ToList();
                query = query.Where(q => FmTree.Contains(q.FmId));
                query = query.Where(q => q.FmId != parameters.FSMId);
            }
            else if (parameters.SearchFSMOption == SearchFSMHierarchyOptionEnum.OwnAndChildren)
            {
                LinqContext.QSPCommonDataContext dbCommon = new LinqContext.QSPCommonDataContext();
                List<string> FmTree = (from u in dbCommon.fnc_FMHierarchyList_FMID(parameters.FSMId) select u.FMNumber).ToList();
                query = query.Where(q => FmTree.Contains(q.FmId));
            }

            #endregion

            #region Sort

            query = query.OrderBy(parameters.SortField);

            #endregion`

            //using (TransactionScope t = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted }))
            //{
                if (parameters.IsPagingEnabled)
                {
                    Properties.Settings settings = new Properties.Settings();

                    if (settings.UseDatabasePaging)
                    {
                        #region Paging

                        query = (
                            from q in query
                            select q
                            ).Skip((parameters.RequestedPage - 1) * parameters.ItemsPerPage).Take(parameters.ItemsPerPage);

                        #endregion

                        #region Get final result

                        result = (
                            from q in query
                            select new OrganizationSearchItem
                            {
                                OrganizationId = q.OrganizationId,
                                OrganizationName = q.OrganizationName,
                                OrganizationTypeId = q.OrganizationTypeId,
                                OrganizationTypeName = q.OrganizationTypeName,
                                FmFirstName = q.FirstName,
                                FmLastName = q.LastName,
                                FmId = q.FmId,
                                Address1 = q.Address1,
                                City = q.City,
                                SubdivisionCode = q.SubdivisionCode,
                                Zip = q.Zip
                            }
                            ).ToList();

                        #endregion
                    }
                    else
                    {
                        #region Paging and final result

                        query = query.Take((parameters.RequestedPage + 1) * parameters.ItemsPerPage);

                        var temp = query.AsEnumerable().Skip(
                            (parameters.RequestedPage - 1) * parameters.ItemsPerPage).Select(q =>
                                new OrganizationSearchItem
                                {
                                    OrganizationId = q.OrganizationId,
                                    OrganizationName = q.OrganizationName,
                                    OrganizationTypeId = q.OrganizationTypeId,
                                    OrganizationTypeName = q.OrganizationTypeName,
                                    FmFirstName = q.FirstName,
                                    FmLastName = q.LastName,
                                    FmId = q.FmId,
                                    Address1 = q.Address1,
                                    City = q.City,
                                    SubdivisionCode = q.SubdivisionCode,
                                    Zip = q.Zip
                                });

                        result = temp.ToList();

                        #endregion
                    }
                }
                else
                {
                    #region Get final result

                    result = (
                        from q in query
                        select new OrganizationSearchItem
                        {
                            OrganizationId = q.OrganizationId,
                            OrganizationName = q.OrganizationName,
                            OrganizationTypeId = q.OrganizationTypeId,
                            OrganizationTypeName = q.OrganizationTypeName,
                            FmFirstName = q.FirstName,
                            FmLastName = q.LastName,
                            FmId = q.FmId,
                            Address1 = q.Address1,
                            City = q.City,
                            SubdivisionCode = q.SubdivisionCode,
                            Zip = q.Zip
                        }
                        ).ToList();

                    #endregion
                }
            //}

            return result;
        }
        public int SearchTotalRowCount(OrganizationSearchParameters parameters)
        {
            int result = 0;

            LinqContext.OrderExpressDataContext db = new LinqContext.OrderExpressDataContext();

            #region Base query

            var query = from o in db.Organizations
                        join fsmjoin in db.FieldSalesManagers on o.FmId equals fsmjoin.FmId
                            into fsmtemptable
                        from fsm in fsmtemptable.DefaultIfEmpty()
                        join pao in db.PostalAddressOrganizations on o.OrganizationId equals pao.OrganizationId
                        where o.IsDeleted == false
                            && (o.BusinessDivisionId == (int)BusinessDivisionEnum.US
                                || o.BusinessDivisionId == (int)BusinessDivisionEnum.EFR)
                            && pao.IsDeleted == false
                            && pao.PostalAddressTypeId == 1
                            && pao.PostalAddress.IsDeleted == 0
                        select new
                        {
                            o.OrganizationId,
                            o.OrganizationName,
                            o.OrganizationType.OrganizationTypeId,
                            o.OrganizationType.OrganizationTypeName,
                            fsm.FmId,
                            fsm.FirstName,
                            fsm.LastName,
                            pao.PostalAddress.Address1,
                            pao.PostalAddress.City,
                            pao.PostalAddress.SubdivisionCode,
                            pao.PostalAddress.Zip
                        };

            #endregion

            #region Filters

            if (parameters.SearchValue.Length > 0)
            {
                if (parameters.SearchField == OrganizationSearchFieldEnum.Any)
                {
                    int number = 0;
                    bool isNumber = int.TryParse(parameters.SearchValue, out number);

                    if (isNumber)
                    {
                        query = from q in query
                                where q.City.Contains(parameters.SearchValue)
                                || q.OrganizationName.Contains(parameters.SearchValue)
                                || q.Zip.Contains(parameters.SearchValue)
                                || q.OrganizationId == number
                                select q;
                    }
                    else
                    {
                        query = from q in query
                                where q.City.Contains(parameters.SearchValue)
                                || q.OrganizationName.Contains(parameters.SearchValue)
                                || q.Zip.Contains(parameters.SearchValue)
                                select q;
                    }
                }
                else if (parameters.SearchField == OrganizationSearchFieldEnum.City)
                {
                    query = from q in query
                            where q.City.Contains(parameters.SearchValue)
                            select q;
                }
                else if (parameters.SearchField == OrganizationSearchFieldEnum.Name)
                {
                    query = from q in query
                            where q.OrganizationName.Contains(parameters.SearchValue)
                            select q;
                }
                else if (parameters.SearchField == OrganizationSearchFieldEnum.NameBeginingWith)
                {
                    query = from q in query
                            where q.OrganizationName.StartsWith(parameters.SearchValue)
                            select q;
                }
                else if (parameters.SearchField == OrganizationSearchFieldEnum.QSPOrganizationId)
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
                else if (parameters.SearchField == OrganizationSearchFieldEnum.ZipCode)
                {
                    query = from q in query
                            where q.Zip.Contains(parameters.SearchValue)
                            select q;
                }
            }

            if (parameters.OrganizationTypeId.HasValue)
            {
                query = from q in query
                        where q.OrganizationTypeId == parameters.OrganizationTypeId.Value
                        select q;
            }
            if (parameters.SubdivisionCode.Length > 0)
            {
                query = from q in query
                        where q.SubdivisionCode == parameters.SubdivisionCode
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
                            where q.FmId.Contains(parameters.FSMId)
                            select q;
                }
                if (parameters.FSMName.Length > 0)
                {
                    query = from q in query
                            where (q.FirstName + " " + q.LastName + " " + q.FirstName).Contains(parameters.FSMName)
                            select q;
                }
            }
            else if (parameters.SearchFSMOption == SearchFSMHierarchyOptionEnum.Own)
            {
                query = query.Where(q => q.FmId == parameters.FSMId);
            }
            else if (parameters.SearchFSMOption == SearchFSMHierarchyOptionEnum.Children)
            {
                LinqContext.QSPCommonDataContext dbCommon = new LinqContext.QSPCommonDataContext();
                List<string> FmTree = (from u in dbCommon.fnc_FMHierarchyList_FMID(parameters.FSMId) select u.FMNumber).ToList();
                query = query.Where(q => FmTree.Contains(q.FmId));
                query = query.Where(q => q.FmId != parameters.FSMId);
            }
            else if (parameters.SearchFSMOption == SearchFSMHierarchyOptionEnum.OwnAndChildren)
            {
                LinqContext.QSPCommonDataContext dbCommon = new LinqContext.QSPCommonDataContext();
                List<string> FmTree = (from u in dbCommon.fnc_FMHierarchyList_FMID(parameters.FSMId) select u.FMNumber).ToList();
                query = query.Where(q => FmTree.Contains(q.FmId));
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

        public EntityData.OrganizationData GetOrganization(int organizationId)
        {
            return this.GetOrganization(organizationId, false);
        }
        public EntityData.OrganizationData GetOrganization(int organizationId, bool loadChildrenObjects)
        {
            EntityData.OrganizationData result = new EntityData.OrganizationData();

            LinqContext.OrderExpressDataContext db = new LinqContext.OrderExpressDataContext();

            #region Load organization data

            LinqEntity.Organization organization = 
                (from o in db.Organizations
                 where o.OrganizationId == organizationId
                 select o
                 ).SingleOrDefault();

            result.ARNMBL = organization.Arnmbl;
            result.CAccountId = organization.DtsCAccountId;
            result.Comments = organization.Comments;
            result.CreateDate = organization.CreateDate;
            result.CreateUserId = organization.CreateUserId;
            result.FlagpoleInstance = organization.DtsFlagpoleInstance;
            result.FmId = organization.FmId;
            result.Id = organization.OrganizationId;
            result.IsDeleted = organization.IsDeleted;
            result.MDRPID = organization.Mdrpid;
            result.Name = organization.OrganizationName;
            result.StatusId = organization.OrganizationStatusId;
            result.TaxExemptionExpirationDate = organization.TaxExemptionExpirationDate;
            result.TaxExemptionNumber = organization.TaxExemptionNumber;
            result.UpdateDate = organization.UpdateDate;
            result.UpdateUserId = organization.UpdateUserId;

            result.Type = new EntityData.OrganizationTypeData();
            result.Type.Id = organization.OrganizationTypeId;

            if (organization.OrganizationLevelId.HasValue)
            {
                result.Level = new EntityData.OrganizationLevelData();
                result.Level.Id = organization.OrganizationLevelId.Value;
            }

            result.BusinessDivision = new EntityData.BusinessDivisionData();
            result.BusinessDivision.Id = organization.BusinessDivisionId;

            #endregion

            if (loadChildrenObjects)
            {
                #region Load extended organization data

                result.Type.Name = organization.OrganizationType.OrganizationTypeName;
                result.Type.ARSTYP = organization.OrganizationType.Arstyp;

                if (organization.OrganizationLevelId.HasValue)
                {
                    result.Level.Name = organization.OrganizationLevel.OrganizationLevelName;
                    result.Level.ARSLEV = organization.OrganizationLevel.ARSLEV;
                }

                LinqEntity.BusinessDivision businessDivision =
                    (from bd in db.BusinessDivisions
                     where bd.BusinessDivisionId == organization.BusinessDivisionId
                     select bd
                     ).SingleOrDefault();

                if (businessDivision != null)
                {
                    result.BusinessDivision.Name = businessDivision.BusinessDivisionName;
                }

                #endregion

                #region Load shipping addresses

                LinqEntity.PostalAddress shippingAddress = (
                    from pao in organization.PostalAddressOrganizations
                    where pao.PostalAddressTypeId == (int)PostalAddressTypeEnum.Shipping
                        && pao.IsDeleted == false
                        && pao.PostalAddress.IsDeleted == 0
                    select pao.PostalAddress
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
                    from pno in organization.PhoneNumberOrganizations
                    where pno.PhoneNumberTypeId == (int)PhoneNumberTypeEnum.ShippingPhone
                        && pno.IsDeleted == false
                        && pno.PhoneNumber.IsDeleted == false
                    select pno.PhoneNumber
                    ).SingleOrDefault();

                if (shippingPhone != null)
                {
                    result.ShippingAddress.Phone = shippingPhone.Number;
                }


                LinqEntity.PhoneNumber shippingFax = (
                    from pno in organization.PhoneNumberOrganizations
                    where pno.PhoneNumberTypeId == (int)PhoneNumberTypeEnum.ShippingFax
                        && pno.IsDeleted == false
                        && pno.PhoneNumber.IsDeleted == false
                    select pno.PhoneNumber
                    ).SingleOrDefault();

                if (shippingFax != null)
                {
                    result.ShippingAddress.Fax = shippingFax.Number;
                }


                LinqEntity.Email shippingEmail = (
                    from eo in organization.EmailOrganizations
                    where eo.EmailTypeId == (int)EmailTypeEnum.Shipping
                        && eo.IsDeleted == false
                        && eo.Email.IsDeleted == 0
                    select eo.Email
                    ).SingleOrDefault();

                if (shippingEmail != null)
                {
                    result.ShippingAddress.Email = shippingEmail.EmailAddress;
                }

                #endregion

                #region Load billing addresses

                LinqEntity.PostalAddress billingAddress = (
                    from pao in organization.PostalAddressOrganizations
                    where pao.PostalAddressTypeId == (int)PostalAddressTypeEnum.Billing
                        && pao.IsDeleted == false
                        && pao.PostalAddress.IsDeleted == 0
                    select pao.PostalAddress
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
                    from pno in organization.PhoneNumberOrganizations
                    where pno.PhoneNumberTypeId == (int)PhoneNumberTypeEnum.BillingPhone
                        && pno.IsDeleted == false
                        && pno.PhoneNumber.IsDeleted == false
                    select pno.PhoneNumber
                    ).SingleOrDefault();

                if (billingPhone != null)
                {
                    result.BillingAddress.Phone = billingPhone.Number;
                }


                LinqEntity.PhoneNumber billingFax = (
                    from pno in organization.PhoneNumberOrganizations
                    where pno.PhoneNumberTypeId == (int)PhoneNumberTypeEnum.BillingFax
                        && pno.IsDeleted == false
                        && pno.PhoneNumber.IsDeleted == false
                    select pno.PhoneNumber
                    ).SingleOrDefault();

                if (billingFax != null)
                {
                    result.BillingAddress.Fax = billingFax.Number;
                }


                LinqEntity.Email billingEmail = (
                    from eo in organization.EmailOrganizations
                    where eo.EmailTypeId == (int)EmailTypeEnum.Billing
                        && eo.IsDeleted == false
                        && eo.Email.IsDeleted == 0
                    select eo.Email
                    ).SingleOrDefault();

                if (billingEmail != null)
                {
                    result.BillingAddress.Email = billingEmail.EmailAddress;
                }

                #endregion
            }

            return result;
        }
        public MethodResult SaveOrganization(EntityData.OrganizationData organization)
        {
            MethodResult result = new MethodResult();

            OrganizationValidation organizationValidation = new OrganizationValidation();
            MethodResult validationResult = organizationValidation.ValidateOrganization(organization);

            if (validationResult.IsSuccessful)
            {
                PostalAddressSystem postalAddressSystem = new PostalAddressSystem();
                PhoneNumberSystem phoneNumberSystem = new PhoneNumberSystem();
                EmailAddressSystem emailAddressSystem = new EmailAddressSystem();

                LinqContext.OrderExpressDataContext db = new LinqContext.OrderExpressDataContext();
                db.Connection.Open();
                db.Transaction = db.Connection.BeginTransaction();

                try
                {
                    #region Save organization data

                    LinqEntity.Organization existingOrganization =
                        (from o in db.Organizations
                         where o.OrganizationId == organization.Id
                         select o
                         ).SingleOrDefault();

                    existingOrganization.OrganizationName = organization.Name;
                    existingOrganization.OrganizationTypeId = organization.Type.Id;
                    existingOrganization.OrganizationLevelId = organization.Level.Id;
                    existingOrganization.TaxExemptionNumber = organization.TaxExemptionNumber;
                    existingOrganization.TaxExemptionExpirationDate = organization.TaxExemptionExpirationDate;
                    existingOrganization.Mdrpid = organization.MDRPID;
                    existingOrganization.Comments = organization.Comments;

                    db.SubmitChanges();

                    #endregion

                    #region Billing address

                    MethodResult billingAddressResult = postalAddressSystem.SaveToOrganization(organization.Id, organization.BillingAddress, PostalAddressTypeEnum.Billing, organization.UpdateUserId.Value, db);
                    MethodResult billingPhoneResult = phoneNumberSystem.SaveToOrganization(organization.Id, organization.BillingAddress.Phone, PhoneNumberTypeEnum.BillingPhone, organization.UpdateUserId.Value, db);
                    MethodResult billingFaxResult = phoneNumberSystem.SaveToOrganization(organization.Id, organization.BillingAddress.Fax, PhoneNumberTypeEnum.BillingFax, organization.UpdateUserId.Value, db);
                    MethodResult billingEmailResult = emailAddressSystem.SaveToOrganization(organization.Id, organization.BillingAddress.Email, EmailTypeEnum.Billing, organization.UpdateUserId.Value, db);

                    #endregion

                    #region Shipping address

                    MethodResult shippingAddressResult = postalAddressSystem.SaveToOrganization(organization.Id, organization.BillingAddress, PostalAddressTypeEnum.Shipping, organization.UpdateUserId.Value, db);
                    MethodResult shippingPhoneResult = phoneNumberSystem.SaveToOrganization(organization.Id, organization.ShippingAddress.Phone, PhoneNumberTypeEnum.ShippingPhone, organization.UpdateUserId.Value, db);
                    MethodResult shippingFaxResult = phoneNumberSystem.SaveToOrganization(organization.Id, organization.ShippingAddress.Fax, PhoneNumberTypeEnum.ShippingFax, organization.UpdateUserId.Value, db);
                    MethodResult shippingEmailResult = emailAddressSystem.SaveToOrganization(organization.Id, organization.ShippingAddress.Email, EmailTypeEnum.Shipping, organization.UpdateUserId.Value, db);

                    #endregion

                    #region Check results and commit transaction

                    result.Merge(validationResult, "Validation");
                    result.Merge(billingAddressResult, "BillingAddress");
                    result.Merge(billingPhoneResult, "BillingPhone");
                    result.Merge(billingFaxResult, "BillingFax");
                    result.Merge(billingEmailResult, "BillingEmail");
                    result.Merge(shippingAddressResult, "ShippingAddress");
                    result.Merge(shippingPhoneResult, "ShippingPhone");
                    result.Merge(shippingFaxResult, "ShippingFax");
                    result.Merge(shippingEmailResult, "ShippingEmail");

                    if (result.IsSuccessful)
                    {
                        db.Transaction.Commit();
                    }
                    else
                    {
                        db.Transaction.Rollback();
                    }

                    #endregion
                }
                catch (Exception ex)
                {
                    MethodResultNotification notification = new MethodResultNotification();

                    notification.Message = ex.Message;
                    notification.NotificationType = MethodResultNotificationTypeEnum.Error;
                    notification.DynamicValues = new Dictionary<string, object>();
                    notification.DynamicValues.Add("Exception", ex);

                    result.ResultNotifications.Add(notification);
                }
                finally
                {
                    db.Connection.Close();
                }                
            }
            else
            {
                result = validationResult;
            }

            return result;
        }
        
        public LinqEntity.OrganizationType GetOrganizationType(int organizationTypeId)
        {
            LinqEntity.OrganizationType result = new LinqEntity.OrganizationType();

            LinqContext.OrderExpressDataContext db = new LinqContext.OrderExpressDataContext();

            result = (from ot in db.OrganizationTypes
                      where ot.OrganizationTypeId == organizationTypeId
                      select ot
                      ).SingleOrDefault();

            return result;
        }
        public List<LinqEntity.OrganizationType> GetOrganizationTypes()
        {
            List<LinqEntity.OrganizationType> result = new List<LinqEntity.OrganizationType>();

            LinqContext.OrderExpressDataContext db = new LinqContext.OrderExpressDataContext();

            result = (from ot in db.OrganizationTypes
                      select ot
                      ).ToList();

            return result;
        }
        public LinqEntity.OrganizationLevel GetOrganizationLevel(int organizationLevelId)
        {
            LinqEntity.OrganizationLevel result = new LinqEntity.OrganizationLevel();

            LinqContext.OrderExpressDataContext db = new LinqContext.OrderExpressDataContext();

            result = (from ol in db.OrganizationLevels
                      where ol.OrganizationLevelId == organizationLevelId
                      select ol
                      ).SingleOrDefault();

            return result;
        }
        public List<LinqEntity.OrganizationLevel> GetOrganizationLevels()
        {
            List<LinqEntity.OrganizationLevel> result = new List<LinqEntity.OrganizationLevel>();

            LinqContext.OrderExpressDataContext db = new LinqContext.OrderExpressDataContext();

            result = (from ol in db.OrganizationLevels
                      select ol
                      ).ToList();

            return result;
        }

        #endregion

        #region Version 1 code

        dataAccessRef orgDataAccess;
		
		public OrganizationSystem()
		{
			orgDataAccess = new dataAccessRef();
		}
		public bool Insert(dataDef Table)
		{
			//We call a method from the inherit class, but the
			//validation with the overriden Validate Method 
			//is in the current class
			return this.Insert(Table, orgDataAccess);			
		}

		public bool Update(dataDef Table)
		{
			//We call a method from the inherit class, but the
			//validation with the overriden Validate Method 
			//is in the current class
			return this.Update(Table, orgDataAccess);			
		}

		public bool UpdateBatch(dataDef Table)
		{
			//We call a method from the inherit class, but the
			//validation with the overriden Validate Method 
			//is in the current class
			return this.UpdateBatch(Table, orgDataAccess);			
		}

		public bool Delete(dataDef Table)
		{
			//We call a method from the inherit class, but the
			//validation with the overriden Validate Method 
			//is in the current class
			return this.Delete(Table, orgDataAccess);			
		}

		//----------------------------------------------------------------
		// Function Validate:
		//   Validates Organization row
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
		//   Validates a specific Organization Ownership Table field against his maxlength 
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
			//Organization
			IsValid &= IsValid_RequiredField(row, dataDef.FLD_NAME, "Organization Name");
			//Organization Number
			IsValid &= IsValid_RequiredField(row, dataDef.FLD_ORG_TYPE_ID, "Organization Type");
			//FM Number -- Unassigned that rule for Org
			//IsValid &= IsValid_RequiredField(row, dataDef.FLD_FM_ID, "FM Number");
			
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
		

		public bool IsValid_Unicity(AccountData dts)
		{
			return IsValid_Unicity((DataSet) dts);
		}

		public bool IsValid_Unicity(OrganizationData dts)
		{
			return IsValid_Unicity((DataSet) dts);
		}

        //retreive the OrgID of the first Occurence of Organization
        public int GetOrganizationID(string OrganizationName, int OrgType, string AddressLine1, string City, string Zip, string State)
        {
            int OrgID = 0;
            //Search by Zip Code
            dataDef dtbl = orgDataAccess.SelectAll_Search(4, Zip, OrgType, State, "", 0, "");
            
            foreach (DataRow row in dtbl.Rows)
            {
                string _address1 = row[PostalAddressEntityTable.FLD_ADDRESS1].ToString().Trim();
                //string _address2 = row[PostalAddressEntityTable.FLD_ADDRESS2].ToString().ToUpper().Trim();
                string _city = row[PostalAddressEntityTable.FLD_CITY].ToString().Trim();
                string _zip = row[PostalAddressEntityTable.FLD_ZIP].ToString().Trim();
                string _state = row[PostalAddressEntityTable.FLD_SUBDIVISION_CODE].ToString().Trim();
                if ((AddressLine1.ToUpper() == _address1.ToUpper()) &&
                    //	(address2 == _address2) &&
                    (City.ToUpper() == _city.ToUpper()) &&
                    (Zip.ToUpper() == _zip.ToUpper()) &&
                    (State.ToUpper() == _state.ToUpper()))
                {
                    OrgID = Convert.ToInt32(row[OrganizationTable.FLD_PKID]);
                    break;
                }
            }

            return OrgID;
        }

        public bool FoundReplace(AccountData dts)
        {
            PostalAddressSystem postalSys = new PostalAddressSystem();
            PostalAddressEntityTable dTblAddress = dts.PostalAddress;
            OrganizationTable dTblOrg = dts.Organization;
            int OrgID = Convert.ToInt32(dTblOrg.Rows[0][OrganizationTable.FLD_PKID]);			
            DataRow bRow = postalSys.FindRow(dTblAddress, EntityType.TYPE_ORGANIZATION, OrgID, PostalAddressType.TYPE_BILLING);
            //Org Info
            string OrganizationName = dTblOrg.Rows[0][dataDef.FLD_NAME].ToString().Trim();
            int OrgType = Convert.ToInt32(dTblOrg.Rows[0][dataDef.FLD_ORG_TYPE_ID]);
            //address Info
            string address1 = bRow[PostalAddressEntityTable.FLD_ADDRESS1].ToString().Trim();
            //string address2 = bRow[PostalAddressEntityTable.FLD_ADDRESS2].ToString().Trim();
            string city = bRow[PostalAddressEntityTable.FLD_CITY].ToString().Trim();
            string zip = bRow[PostalAddressEntityTable.FLD_ZIP].ToString().Trim();
            string state = bRow[PostalAddressEntityTable.FLD_SUBDIVISION_CODE].ToString().Trim();
            

            bool isValid = true;
            int foundOrgID = 0;

            foundOrgID = GetOrganizationID(OrganizationName, OrgType, address1, city, zip, state);
            if (foundOrgID == 0)
            {
                //False for the element has not been replaced
                return false;
            }
            else
            { 
                DataRow orgRow = dTblOrg.Rows[0];
                orgRow[OrganizationTable.FLD_PKID] = foundOrgID;
                DataRow accRow = dts.Account.Rows[0];
                accRow[AccountTable.FLD_ORG_ID] = foundOrgID;
                //to avoid any change
                orgRow.AcceptChanges();
                DataRow[] arrRow = postalSys.FindRows(dTblAddress, EntityType.TYPE_ORGANIZATION);
                foreach (DataRow rRow in arrRow)
                {
                    rRow.AcceptChanges();                
                }
                PhoneNumberSystem phoneSys = new PhoneNumberSystem();
                arrRow = phoneSys.FindRows(dts.PhoneNumber, EntityType.TYPE_ORGANIZATION);
                foreach (DataRow rRow in arrRow)
                {
                    rRow.AcceptChanges();
                }
                EmailAddressSystem emailSys = new EmailAddressSystem();
                arrRow = emailSys.FindRows(dts.EmailAddress, EntityType.TYPE_ORGANIZATION);
                foreach (DataRow rRow in arrRow)
                {
                    rRow.AcceptChanges();
                }

                //True for the element has been replaced
                return true;
            }
        }  

		private bool IsValid_Unicity(DataSet dts)
		{
			PostalAddressSystem postalSys = new PostalAddressSystem();
			PostalAddressEntityTable dTblAddress = (PostalAddressEntityTable) dts.Tables[PostalAddressEntityTable.TBL_POSTAL_ADDRESS_ENTITY];
			OrganizationTable dTblOrg = (OrganizationTable) dts.Tables[OrganizationTable.TBL_ORGANIZATION];
			int OrgID = Convert.ToInt32(dTblOrg.Rows[0][OrganizationTable.FLD_PKID]);
			DataRow bRow = postalSys.FindRow(dTblAddress, EntityType.TYPE_ORGANIZATION, OrgID, PostalAddressType.TYPE_BILLING);
			if (bRow != null)
			{
				string OrganizationName = dTblOrg.Rows[0][dataDef.FLD_NAME].ToString().Trim();
                int OrgType = Convert.ToInt32(dTblOrg.Rows[0][dataDef.FLD_ORG_TYPE_ID]);
				string address1 = bRow[PostalAddressEntityTable.FLD_ADDRESS1].ToString().Trim();
				//string address2 = bRow[PostalAddressEntityTable.FLD_ADDRESS2].ToString().Trim();
				string city = bRow[PostalAddressEntityTable.FLD_CITY].ToString().Trim();
				string zip = bRow[PostalAddressEntityTable.FLD_ZIP].ToString().Trim();
				string state = bRow[PostalAddressEntityTable.FLD_SUBDIVISION_CODE].ToString().Trim();
                				
				bool isValid = true;
                int foundOrgID = 0;

                foundOrgID = GetOrganizationID(OrganizationName, OrgType, address1, city, zip, state);
                isValid = (foundOrgID == 0);

                return isValid;
			}
			else
			{
				return false;
			}
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
			dTbl = orgDataAccess.SelectAllWfm_idLogic(FMID);				
			
			return dTbl;			
		}

		public dataDef SelectOne(int ID)
		{			
			dataDef dTbl;			
			//
			// Get the user DataTable from the DataLayer
			//
			dTbl = orgDataAccess.SelectOne(ID);				
			
			return dTbl;
			
		}

		public dataDef SelectAllByAccountID(int AccountID)
		{			
			dataDef dTbl;			
			//
			// Get the user DataTable from the DataLayer
			//
			dTbl = orgDataAccess.SelectAllWaccount_idLogic(AccountID);				
			
			return dTbl;
			
		}

		public dataDef SelectAllByCampaignID(int CampaignID)
		{			
			dataDef dTbl;			
			//
			// Get the user DataTable from the DataLayer
			//
			dTbl = orgDataAccess.SelectAllWcampaign_idLogic(CampaignID);				
			
			return dTbl;
			
		}

		public OrganizationData SelectAllDetail(int ID)
		{			
			//This method fill the All Data needed for an organization
			//into a predefined DataSet
			OrganizationData dts = new OrganizationData();
			dts.Merge(orgDataAccess.SelectOne(ID));			
			//dts.Organization = orgDataAccess.SelectOne(ID);
			//Postal Address
			PostalAddressSystem addSys = new PostalAddressSystem();
			dts.Merge(addSys.SelectAllByOrganizationID(ID));
			//dts.PostalAddress  = addSys.SelectAllByOrganizationID(ID);
			//Phone Number
			PhoneNumberSystem phoneSys = new PhoneNumberSystem();
			dts.Merge(phoneSys.SelectAllByOrganizationID(ID));
			//dts.PhoneNumber  = phoneSys.SelectAllByOrganizationID(ID);
			//Email Addess
			EmailAddressSystem emailSys = new EmailAddressSystem();
			dts.Merge(emailSys.SelectAllByOrganizationID(ID));			
			//dts.EmailAddress  = emailSys.SelectAllByOrganizationID(ID);
			return dts;
			
		}

		public bool  UpdateAllDetail(OrganizationData dts)
		{			
			bool IsSuccess = true;
			//This method fill the All Data needed for an organization
			//into a predefined DataSet			
			if (dts.Organization.GetChanges() != null)
			{
				IsSuccess = UpdateBatch(dts.Organization);
				if (!IsSuccess)
					return IsSuccess;
			}
			//Postal Address
			if (dts.PostalAddress.GetChanges() != null)
			{
				PostalAddressSystem addSys = new PostalAddressSystem();
				IsSuccess = addSys.UpdateBatch(dts.PostalAddress);
				if (!IsSuccess)
					return IsSuccess;
			}
			//Phone Number
			if (dts.PhoneNumber.GetChanges() != null)
			{
				PhoneNumberSystem phoneSys = new PhoneNumberSystem();
				IsSuccess = phoneSys.UpdateBatch(dts.PhoneNumber);
				if (!IsSuccess)
					return IsSuccess;
			}
			//Email Addess
			if (dts.EmailAddress.GetChanges() != null)
			{
				EmailAddressSystem emailSys = new EmailAddressSystem();
				IsSuccess = emailSys.UpdateBatch(dts.EmailAddress);
			}
			return IsSuccess;
			
		}

		public bool  InsertAllDetail(OrganizationData dts)
		{
			
			if(IsValid_Unicity(dts))
			{
				bool IsSuccess = true;
				//This method fill the All Data needed for an organization
				//into a predefined DataSet			
				IsSuccess = UpdateBatch(dts.Organization);
				if (!IsSuccess)
					return IsSuccess;
				PrepareTransactionWithNewID(dts);
				//Postal Address
				if (dts.PostalAddress.GetChanges() != null)
				{
					PostalAddressSystem addSys = new PostalAddressSystem();
					IsSuccess = addSys.UpdateBatch(dts.PostalAddress);
					if (!IsSuccess)
						return IsSuccess;
				}
			
				//Phone Number
				if (dts.PhoneNumber.GetChanges() != null)
				{
					PhoneNumberSystem phoneSys = new PhoneNumberSystem();
					IsSuccess = phoneSys.UpdateBatch(dts.PhoneNumber);
					if (!IsSuccess)
						return IsSuccess;
				}
				//Email Address
				if (dts.EmailAddress.GetChanges() != null)
				{
					EmailAddressSystem emailSys = new EmailAddressSystem();
					IsSuccess = emailSys.UpdateBatch(dts.EmailAddress);
				}
				return IsSuccess;
			}
			else
			{
				return false;
			}
		}

		private void PrepareTransactionWithNewID(OrganizationData dts)
		{
			int NewID = Convert.ToInt32(dts.Organization.Rows[0][OrganizationTable.FLD_PKID]);
			foreach(DataRow row in dts.PostalAddress.Rows)
			{
				if (row.RowState == DataRowState.Added)
				{
					row[PostalAddressEntityTable.FLD_ENTITY_ID] = NewID;
				}
			}
			foreach(DataRow row in dts.PhoneNumber.Rows)
			{
				if (row.RowState == DataRowState.Added)
				{
					row[PhoneNumberEntityTable.FLD_ENTITY_ID] = NewID;
				}
			}
			foreach(DataRow row in dts.EmailAddress.Rows)
			{
				if (row.RowState == DataRowState.Added)
				{
					row[EmailEntityTable.FLD_ENTITY_ID] = NewID;
				}
			}		
		}


        public dataDef SelectAll_Search(int SearchType, String Criteria, int OrgType, string SubdivisionCode, string FMID, int FSM_DisplayMode, string FMName)
		{			
			dataDef dTbl;
			
			//
			// Get the user DataTable from the DataLayer
			//				
            dTbl = orgDataAccess.SelectAll_Search(SearchType, Criteria, OrgType, SubdivisionCode, FMID, FSM_DisplayMode, FMName);				
			
			return dTbl;			
		}	

		public OrganizationData InitializeOrganization(int UserID, String FMID)
		{
			//We prepare the DataSet for all step
			//Add a new Row
			OrganizationData dtsOrganization = new OrganizationData();
			
			//Create a new Organization  row at start
			OrganizationTable orgTable = dtsOrganization.Organization;
			DataRow row;
			row = orgTable.NewRow();
			//FM Info -- if this is an FM
			if (FMID.Length > 0)
			{
				row[OrganizationTable.FLD_FM_ID] = FMID;
				//Get the FM Name
				CUserSystem fmSys = new CUserSystem();
				CUserTable dTblfm = new CUserTable();
				dTblfm = fmSys.SelectOne(FMID);
				DataRow fmRow = dTblfm.Rows[0];
				row[OrganizationTable.FLD_FM_NAME] = fmRow[CUserTable.FLD_LAST_NAME] + ", " + fmRow[CUserTable.FLD_FIRST_NAME];			
			}
			//Org Name
			row[OrganizationTable.FLD_NAME] = "New Organization"; //Name
			row[OrganizationTable.FLD_CREATE_USER_ID] = UserID;
			row[OrganizationTable.FLD_BIZ_DIVISION_ID] = 1; // default value USA
			orgTable.Rows.Add(row);

			//Postal Address
//			PostalAddressEntityTable dTblPostalAddress = dtsOrganization.PostalAddress;
//			if (dTblPostalAddress.Rows.Count == 0)
//			{				
//				DataRow addessRow = dTblPostalAddress.NewRow();
//				addessRow[PostalAddressEntityTable.FLD_ENTITY_ID] = 0;
//				addessRow[PostalAddressEntityTable.FLD_ENTITY_TYPE_ID] = EntityType.TYPE_ORGANIZATION;
//				addessRow[PostalAddressEntityTable.FLD_CREATE_USER_ID] = UserID;
//				dTblPostalAddress.Rows.Add(addessRow);							
//			}
//
//			//Phone Number
//			PhoneNumberEntityTable dTblPhoneNumber = dtsOrganization.PhoneNumber;
//			if (dTblPhoneNumber.Rows.Count == 0)
//			{				
//				DataRow phoneRow = dTblPhoneNumber.NewRow();
//				phoneRow[PhoneNumberEntityTable.FLD_ENTITY_ID] = 0;
//				phoneRow[PhoneNumberEntityTable.FLD_ENTITY_TYPE_ID] = EntityType.TYPE_ORGANIZATION;
//				phoneRow[PhoneNumberEntityTable.FLD_CREATE_USER_ID] = UserID;
//				dTblPhoneNumber.Rows.Add(phoneRow);							
//			}
//
//			//Email Address
//			EmailEntityTable dTblEmailAddress = dtsOrganization.EmailAddress;
//			if (dTblEmailAddress.Rows.Count == 0)
//			{				
//				DataRow emailRow = dTblEmailAddress.NewRow();
//				emailRow[EmailEntityTable.FLD_ENTITY_ID] = 0;
//				emailRow[EmailEntityTable.FLD_ENTITY_TYPE_ID] = EntityType.TYPE_ORGANIZATION;
//				emailRow[EmailEntityTable.FLD_CREATE_USER_ID] = UserID;
//				dTblEmailAddress.Rows.Add(emailRow);							
//			}

			return dtsOrganization;
			
		}

		public void SetDefaultInformation(OrganizationData dtsOrgData, int UserID)
		{
			OrganizationTable dtblOrganization =  dtsOrgData.Organization;
			DataRow orgRow = dtblOrganization.Rows[0];
			if (orgRow[OrganizationTable.FLD_MDRPID] != DBNull.Value)
			{
				string MDRSchoolPID = orgRow[OrganizationTable.FLD_MDRPID].ToString();
				if (MDRSchoolPID.Length > 0)
				{
					MDRSystem mdrSys  = new MDRSystem();
					CMDRTable dTblMDRSchool = mdrSys.SelectOne(MDRSchoolPID);
					if (dTblMDRSchool.Rows.Count >0)
					{
						DataRow mdrRow = dTblMDRSchool.Rows[0];
						orgRow[OrganizationTable.FLD_NAME] = mdrRow[CMDRTable.FLD_NAME];
						//Organization Type and Level
						orgRow[OrganizationTable.FLD_ORG_TYPE_ID] = mdrRow[CMDRTable.FLD_ORG_TYPE_ID];						
						orgRow[OrganizationTable.FLD_ORG_LEVEL_ID] = mdrRow[CMDRTable.FLD_ORG_LEVEL_ID];

						//Do the same thing for the Postal Address
						PostalAddressEntityTable dTblAddress = dtsOrgData.PostalAddress;
						DataRow addrRow = dTblAddress.NewRow();
						int OrgID = Convert.ToInt32(orgRow[OrganizationTable.FLD_PKID]);
						//Split the name in two
						string sfullName = mdrRow[CMDRTable.FLD_PRINCIPAL_NAME].ToString();
						sfullName = sfullName.Replace("MRS ", "").Replace("MS ", "").Replace("MR ", "").Trim();
						string delimStr = " ";
						char [] delimiter = delimStr.ToCharArray();
						string[] arrName = 	sfullName.Split(delimiter, 2);
						string sfirstName = "";
						string slastName = "";
						if (arrName.Length == 2)
						{
							sfirstName = arrName[0].ToString();
							slastName = arrName[1].ToString();
						}
						else
						{
							slastName = arrName[0].ToString();
						}
						//BILLING ADDRESS//*******************************************
						addrRow[PostalAddressEntityTable.FLD_ENTITY_ID] = OrgID;
						addrRow[PostalAddressEntityTable.FLD_ENTITY_TYPE_ID] = EntityType.TYPE_ORGANIZATION;						
						addrRow[PostalAddressEntityTable.FLD_NAME] = mdrRow[CMDRTable.FLD_NAME];
						addrRow[PostalAddressEntityTable.FLD_FIRST_NAME] = sfirstName;
						addrRow[PostalAddressEntityTable.FLD_LAST_NAME] = slastName;
						addrRow[PostalAddressEntityTable.FLD_ADDRESS1] = mdrRow[CMDRTable.FLD_ADDR];
						addrRow[PostalAddressEntityTable.FLD_CITY] = mdrRow[CMDRTable.FLD_CITY];
						addrRow[PostalAddressEntityTable.FLD_TYPE] = PostalAddressType.TYPE_BILLING; //Billing
						addrRow[PostalAddressEntityTable.FLD_COUNTY] = mdrRow[CMDRTable.FLD_COUNTY];
						addrRow[PostalAddressEntityTable.FLD_COUNTRY_CODE] = "US";
						addrRow[PostalAddressEntityTable.FLD_SUBDIVISION_CODE] = "US-" + mdrRow[CMDRTable.FLD_STATE].ToString();
						addrRow[PostalAddressEntityTable.FLD_ZIP] = mdrRow[CMDRTable.FLD_POSTAL_CODE];
						addrRow[PostalAddressEntityTable.FLD_CREATE_USER_ID] = UserID;
						dTblAddress.Rows.Add(addrRow);
						//SHIPPING ADDRESS//*******************************************
						//Add Also as Shipping Address
						addrRow = dTblAddress.NewRow();												
						addrRow[PostalAddressEntityTable.FLD_ENTITY_ID] = OrgID;
						addrRow[PostalAddressEntityTable.FLD_ENTITY_TYPE_ID] = EntityType.TYPE_ORGANIZATION;
						addrRow[PostalAddressEntityTable.FLD_NAME] = mdrRow[CMDRTable.FLD_NAME];
						addrRow[PostalAddressEntityTable.FLD_FIRST_NAME] = sfirstName;
						addrRow[PostalAddressEntityTable.FLD_LAST_NAME] = slastName;
						addrRow[PostalAddressEntityTable.FLD_ADDRESS1] = mdrRow[CMDRTable.FLD_ADDR];
						addrRow[PostalAddressEntityTable.FLD_CITY] = mdrRow[CMDRTable.FLD_CITY];
						addrRow[PostalAddressEntityTable.FLD_TYPE] = PostalAddressType.TYPE_SHIPPING; //Billing
						addrRow[PostalAddressEntityTable.FLD_COUNTY] = mdrRow[CMDRTable.FLD_COUNTY];
						addrRow[PostalAddressEntityTable.FLD_COUNTRY_CODE] = "US";
						addrRow[PostalAddressEntityTable.FLD_SUBDIVISION_CODE] = "US-" + mdrRow[CMDRTable.FLD_STATE].ToString();
						addrRow[PostalAddressEntityTable.FLD_ZIP] = mdrRow[CMDRTable.FLD_POSTAL_CODE];
						addrRow[PostalAddressEntityTable.FLD_CREATE_USER_ID] = UserID;
						dTblAddress.Rows.Add(addrRow);

						//Do the same thing for the Postal Address
						PhoneNumberEntityTable dTblPhone = dtsOrgData.PhoneNumber;
						if (mdrRow[CMDRTable.FLD_PHONE_NUMBER] != System.DBNull.Value)
						{
							DataRow phoneRow = dTblPhone.NewRow();
							phoneRow[PhoneNumberEntityTable.FLD_ENTITY_ID] = OrgID;
							phoneRow[PhoneNumberEntityTable.FLD_ENTITY_TYPE_ID] = EntityType.TYPE_ORGANIZATION;	
							phoneRow[PhoneNumberEntityTable.FLD_PHONE_NUMBER] = mdrRow[CMDRTable.FLD_PHONE_NUMBER];
							phoneRow[PhoneNumberEntityTable.FLD_TYPE] = PhoneNumberType.TYPE_BILLING_PHONE; //Billing
							phoneRow[PhoneNumberEntityTable.FLD_CREATE_USER_ID] = UserID;
							dTblPhone.Rows.Add(phoneRow);
							//Add a Shipping Phone Number
							phoneRow = dTblPhone.NewRow();
							phoneRow[PhoneNumberEntityTable.FLD_ENTITY_ID] = OrgID;
							phoneRow[PhoneNumberEntityTable.FLD_ENTITY_TYPE_ID] = EntityType.TYPE_ORGANIZATION;	
							phoneRow[PhoneNumberEntityTable.FLD_PHONE_NUMBER] = mdrRow[CMDRTable.FLD_PHONE_NUMBER];
							phoneRow[PhoneNumberEntityTable.FLD_TYPE] = PhoneNumberType.TYPE_SHIPPING_PHONE; //Billing
							phoneRow[PhoneNumberEntityTable.FLD_CREATE_USER_ID] = UserID;
							dTblPhone.Rows.Add(phoneRow);
						}
					}


				}
			}

        }

        #endregion
    }
}
