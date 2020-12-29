using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GA.BDC.Shared.Entities
{
    public class EzFundContact
    {
        /// <summary>
        /// Contact Id
        /// </summary>
        public int ContactId { get; set; }
        /// <summary>
        /// Organization Id
        /// </summary>
        public int OrganizationId { get; set; }
        /// <summary>
        /// Contact Sequencial Number
        /// </summary>
        public int ContactSequencialNumber { get; set; }
        /// <summary>
        /// Contact Name
        /// </summary>
        public string ContactName { get; set; }
        /// <summary>
        /// Contact Title Id
        /// </summary>
        public int ContactTitleId { get; set; }
        /// <summary>
        /// Phone Number 1 Type Code
        /// </summary>
        public string Phone1TypeCode { get; set; }
        /// <summary>
        /// Phone Number 1
        /// </summary>
        public string Phone1{ get; set; }
        /// <summary>
        /// Phone Number 2 Type Code
        /// </summary>
        public string Phone2TypeCode { get; set; }
        /// <summary>
        /// Phone Number 2
        /// </summary>
        public string Phone2 { get; set; }
        /// <summary>
        /// Phone Number 3 Type Code
        /// </summary>
        public string Phone3TypeCode { get; set; }
        /// <summary>
        /// Phone Number 3
        /// </summary>
        public string Phone3 { get; set; }
        /// <summary>
        /// Fax Number Type Code
        /// </summary>
        public string FaxTypeCode { get; set; }
        /// <summary>
        /// Fax Number
        /// </summary>
        public string Fax { get; set; }
        /// <summary>
        /// Email
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// Contact Note
        /// </summary>
        public string ContactNote { get; set; }
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
