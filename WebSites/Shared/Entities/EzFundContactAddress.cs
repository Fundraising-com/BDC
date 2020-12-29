using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GA.BDC.Shared.Entities
{
    public class EzFundContactAddress
    {
        /// <summary>
        /// Address Id
        /// </summary>
        public int AddressId { get; set; }
        /// <summary>
        /// Contact Id
        /// </summary>
        public int ContactId { get; set; }
        /// <summary>
        /// Organization Id
        /// </summary>
        public int OrganizationId { get; set; }
        /// <summary>
        /// Address Type Code
        /// </summary>
        public string AddressTypeCode { get; set; }
        /// <summary>
        /// Address 1
        /// </summary>
        public string Address1 { get; set; }
        /// <summary>
        /// Address 2
        /// </summary>
        public string Address2 { get; set; }
        /// <summary>
        /// Address 3
        /// </summary>
        public string Address3 { get; set; }
        /// <summary>
        /// City Name
        /// </summary>
        public string CityName { get; set; }
        /// <summary>
        /// State Code
        /// </summary>
        public string StateCode { get; set; }
        /// <summary>
        /// Zip Code
        /// </summary>
        public string ZipCode { get; set; }
        /// <summary>
        /// Country Name
        /// </summary>
        public string CountryName { get; set; }
        /// <summary>
        /// Address Note
        /// </summary>
        public string AddressNote { get; set; }
        /// <summary>
        /// Creation Date
        /// </summary>
        public DateTime CreatedDate { get; set; }
        /// <summary>
        /// Created By Person Code
        /// </summary>
        public string CreatedPersonCode { get; set; }
        /// <summary>
        /// Last Modification Date
        /// </summary>
        public DateTime LastModificationDate { get; set; }
        /// <summary>
        /// Last Modification By Person Code
        /// </summary>
        public string LastModificationPersonCode { get; set; }
    }
}
