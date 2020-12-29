using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GA.BDC.Data.EzFund.EZMain.Tables;
using GA.BDC.Shared.Entities;

namespace GA.BDC.Data.EzFund.EZMain.Mappers
{
    public static class OrganizationTypeMapper
    {
        public static OrganizationType HydrateOrganization(org_type_lkup_tbl organizationtype)
        {

            return new  OrganizationType
            {
                Id = organizationtype.ORG_TYPE_ID,
                Name = organizationtype.ORG_TYPE_TXT,
                SqeNbr = organizationtype.SEQ_NBR
                
            };
           
        }
        



        public static org_type_lkup_tbl DehydrateOrganization(OrganizationType organizationtype)
        {
            return new org_type_lkup_tbl
            {
                ORG_TYPE_ID = organizationtype.Id,
                ORG_TYPE_TXT = organizationtype.Name,
                SEQ_NBR = organizationtype.SqeNbr
                
            };
            
        }
        
      
    }
}
