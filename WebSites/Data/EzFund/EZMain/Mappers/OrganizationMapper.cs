using GA.BDC.Data.EzFund.EZMain.Tables;
using GA.BDC.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GA.BDC.Data.EzFund.EZMain.Mappers
{
    public static class OrganizationMapper
    {
        public static EzFundOrganization HydrateOrganization(org_mstr_tbl organization)
        {

            var result = new EzFundOrganization
            {
                OrganizationId = organization.ORG_ID,
                OrganizationName = organization.ORG_NME,
                DepartmentId = organization.DEPT_ID,
                OrganizationTypeId = organization.ORG_TYPE_ID,
                ISDId = organization.ISD_ID,
                LocalCode = organization.LOCL_CDE,
                ZipLookUpCode = organization.ZIP_LKUP_CDE,
                OrganizationMembersQuantity = organization.ORG_MEMB_QTY ?? 0,
                PrimaryAddressId = organization.PRIM_ADDR_ID ?? 0,
                PrimaryContactId = organization.PRIM_CTCT_ID ?? 0,
                Phone1TypeCode = organization.PH_1_TYPE_CDE,
                Phone1 = organization.PH_1_NBR,
                Phone2TypeCode = organization.PH_2_TYPE_CDE,
                Phone2 = organization.PH_2_NBR,
                Phone3TypeCode = organization.PH_3_TYPE_CDE,
                Phone3 = organization.PH_3_NBR,
                FaxTypeCode = organization.FAX_TYPE_CDE,
                Fax = organization.FAX_NBR,
                WebPage = organization.WWW_TXT,
                SalesPersonCode = organization.SLSP_CDE,
                SalesServicePersonCode = organization.SSPP_CDE,
                LeadRTGCode = organization.LEAD_RTG_CDE,
                LeadStatusCode = organization.LEAD_STAT_CDE,
                LeadStatusModificationDate = organization.LEAD_STAT_MODF_DTE,
                LeadReferralCode = organization.LEAD_RFRL_CDE,
                LeadReferralModificationDate = organization.LEAD_RFRL_MODF_DTE,
                PaymentTerminationCode = organization.PMT_TERM_CDE,
                SOLMAccountNumber = organization.SOLM_ACCT_NBR,
                GMAccountNumber = organization.GM_ACCT_NBR,
                CreatedDate = organization.CREA_DTE,
                CreatedPersonCode = organization.CREA_PRSN_CDE,
                LastModificationDate = organization.LAST_MODF_DTE,
                LastModificationPersonCode = organization.LAST_MODF_PRSN_CDE,
                OrganizationCanonicalName = organization.CANON_ORG_NME
            };
            return result;
        }
        public static EzFundContactAddress HydrateOrganizationContactAddress(org_ctct_addr_tbl contact_address)
        {
            var result = new EzFundContactAddress
            {
                AddressId = contact_address.ADDR_ID,
                ContactId = contact_address.CTCT_ID,
                OrganizationId = contact_address.ORG_ID,
                AddressTypeCode = contact_address.ADDR_TYPE_CDE,
                Address1 = contact_address.ADDR_1_TXT,
                Address2 = contact_address.ADDR_2_TXT,
                Address3 = contact_address.ADDR_3_TXT,
                CityName = contact_address.CITY_NME,
                StateCode = contact_address.ST_CDE,
                ZipCode = contact_address.ZIP_CDE,
                CountryName = contact_address.CTRY_NME,
                AddressNote = contact_address.ADDR_NOTE_TXT,
                CreatedDate = contact_address.CREA_DTE,
                CreatedPersonCode = contact_address.CREA_PRSN_CDE,
                LastModificationDate = contact_address.LAST_MODF_DTE,
                LastModificationPersonCode = contact_address.LAST_MODF_PRSN_CDE
            };
            return result;
        }
        public static EzFundContact HydrateOrganizationContact(org_ctct_tbl contact)
        {
            var result =  new EzFundContact
            {
                ContactId =  contact.CTCT_ID,
                OrganizationId = contact.ORG_ID,
                ContactSequencialNumber = contact.CTCT_SEQ_NBR,
                ContactName = contact.CTCT_NME,
                ContactTitleId = contact.CTCT_TITL_ID,
                Phone1TypeCode = contact.PH_1_TYPE_CDE,
                Phone1 = contact.PH_1_NBR,
                Phone2TypeCode = contact.PH_2_TYPE_CDE,
                Phone2 = contact.PH_2_NBR,
                Phone3TypeCode = contact.PH_3_TYPE_CDE,
                Phone3 = contact.PH_3_NBR,
                FaxTypeCode = contact.FAX_TYPE_CDE,
                Fax = contact.FAX_NBR,
                Email = contact.EML_TXT,
                ContactNote = contact.CTCT_NOTE_TXT,
                CreatedDate = contact.CREA_DTE,
                CreatedPersonCode = contact.CREA_PRSN_CDE,
                LastModificationDate = contact.LAST_MODF_DTE,
                LastModificationPersonCode = contact.LAST_MODF_PRSN_CDE
            };
            return result;
        }




        public static org_mstr_tbl DehydrateOrganization(EzFundOrganization organization)
        {
            return new org_mstr_tbl
            {
                ORG_ID = organization.OrganizationId,
                ORG_NME = organization.OrganizationName ,
                DEPT_ID = organization.DepartmentId,
                ORG_TYPE_ID = organization.OrganizationTypeId,
                ISD_ID = organization.ISDId,
                LOCL_CDE = organization.LocalCode,
                ZIP_LKUP_CDE = organization.ZipLookUpCode,
                ORG_MEMB_QTY = organization.OrganizationMembersQuantity,
                PRIM_ADDR_ID = organization.PrimaryAddressId,
                PRIM_CTCT_ID  = organization.PrimaryContactId,
                PH_1_TYPE_CDE = organization.Phone1TypeCode,
                PH_1_NBR = organization.Phone1,
                PH_2_TYPE_CDE = organization.Phone2TypeCode,
                PH_2_NBR = organization.Phone2,
                PH_3_TYPE_CDE = organization.Phone3TypeCode,
                PH_3_NBR = organization.Phone3,
                FAX_TYPE_CDE = organization.FaxTypeCode,
                FAX_NBR = organization.Fax,
                WWW_TXT = organization.WebPage,
                SLSP_CDE = organization.SalesPersonCode,
                SSPP_CDE = organization.SalesServicePersonCode,
                LEAD_RTG_CDE = organization.LeadRTGCode,
                LEAD_STAT_CDE = organization.LeadStatusCode,
                LEAD_STAT_MODF_DTE = organization.LeadStatusModificationDate,
                LEAD_RFRL_CDE = organization.LeadReferralCode,
                LEAD_RFRL_MODF_DTE = organization.LeadReferralModificationDate,
                PMT_TERM_CDE = organization.PaymentTerminationCode,
                SOLM_ACCT_NBR = organization.SOLMAccountNumber,
                GM_ACCT_NBR = organization.GMAccountNumber,
                CREA_DTE = organization.CreatedDate,
                CREA_PRSN_CDE = organization.CreatedPersonCode,
                LAST_MODF_DTE = DateTime.Now,
                LAST_MODF_PRSN_CDE = organization.LastModificationPersonCode,
                CANON_ORG_NME = organization.OrganizationCanonicalName
            };
            
        }
        public static org_ctct_addr_tbl DehydrateOrganizationContactAddress(EzFundContactAddress contactAddress)
        {
            return new org_ctct_addr_tbl
            {
                ADDR_ID = contactAddress.AddressId,
                CTCT_ID = contactAddress.ContactId,
                ORG_ID = contactAddress.OrganizationId,
                ADDR_TYPE_CDE = contactAddress.AddressTypeCode,
                ADDR_1_TXT = contactAddress.Address1,
                ADDR_2_TXT = contactAddress.Address2,
                ADDR_3_TXT = contactAddress.Address3,
                CITY_NME = contactAddress.CityName,
                ST_CDE = contactAddress.StateCode,
                ZIP_CDE = contactAddress.ZipCode,
                CTRY_NME = contactAddress.CountryName,
                ADDR_NOTE_TXT = contactAddress.AddressNote,
                CREA_DTE = contactAddress.CreatedDate,
                CREA_PRSN_CDE = contactAddress.CreatedPersonCode,
                LAST_MODF_DTE = contactAddress.LastModificationDate,
                LAST_MODF_PRSN_CDE = contactAddress.LastModificationPersonCode
            };
        }
        public static org_ctct_tbl DehydrateOrganizationContact(EzFundContact contact)
        {
            return new org_ctct_tbl
            {
                CTCT_ID = contact.ContactId,
                ORG_ID = contact.OrganizationId,
                CTCT_SEQ_NBR = contact.ContactSequencialNumber,
                CTCT_NME = contact.ContactName,
                CTCT_TITL_ID = contact.ContactTitleId,
                PH_1_TYPE_CDE = contact.Phone1TypeCode,
                PH_1_NBR = contact.Phone1,
                PH_2_TYPE_CDE = contact.Phone2TypeCode,
                PH_2_NBR = contact.Phone2,
                PH_3_TYPE_CDE = contact.Phone3TypeCode,
                PH_3_NBR = contact.Phone3,
                FAX_TYPE_CDE = contact.FaxTypeCode,
                FAX_NBR = contact.Fax,
                EML_TXT = contact.Email,
                CTCT_NOTE_TXT = contact.ContactNote,
                CREA_DTE = contact.CreatedDate,
                CREA_PRSN_CDE = contact.CreatedPersonCode,
                LAST_MODF_DTE = contact.LastModificationDate,
                LAST_MODF_PRSN_CDE = contact.LastModificationPersonCode
            };
        }
        
    }
}
