using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QSP.OrderExpress.Common.Search
{
    [Serializable]
    public class OrganizationSearchItem
    {
        #region Properties

        public int OrganizationId { get; set; }
        public int OrganizationTypeId { get; set; }
        public string OrganizationName { get; set; }
        public string OrganizationTypeName { get; set; }
        public string FmId { get; set; }
        public string FmFirstName { get; set; }
        public string FmLastName { get; set; }
        public string Address1 { get; set; }
        public string City { get; set; }
        public string SubdivisionCode { get; set; }
        public string Zip { get; set; }

        #endregion

        #region Display Properties

        public string DisplayFsmName
        {
            get
            {
                if (string.IsNullOrEmpty(FmFirstName) && string.IsNullOrEmpty(FmLastName))
                {
                    return "";
                }
                else
                {
                    return string.Format("{0}, {1}", FmLastName, FmFirstName);
                }
            }
        }
        public string DisplaySubdivisionCode
        {
            get
            {
                if (SubdivisionCode != null)
                {
                    return SubdivisionCode.Replace("US-", "");
                }
                else
                {
                    return "";
                }
            }
        }

        #endregion
    }
}
