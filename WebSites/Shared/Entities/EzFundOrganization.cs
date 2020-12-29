using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GA.BDC.Shared.Entities
{
    public class EzFundOrganization
    {
        public EzFundOrganization()
        {
            Contacts = new List<EzFundContact>();
            ContactAddresses = new List<EzFundContactAddress>();
        }
        /// <summary>
        /// Organization Id
        /// </summary>
        public int OrganizationId { get; set; }
        /// <summary>
        /// Organization Name
        /// </summary>
        public string OrganizationName { get; set; }
        /// <summary>
        /// Department Id
        /// </summary>
        public int DepartmentId { get; set; }
        /// <summary>
        /// Organization Type Id
        /// </summary>
        public int OrganizationTypeId { get; set; }
        /// <summary>
        /// ISD Id
        /// </summary>
        public int ISDId { get; set; }
        /// <summary>
        /// Local Code
        /// </summary>
        public string LocalCode { get; set; }
        /// <summary>
        /// Zip LookUp Code
        /// </summary>
        public string ZipLookUpCode { get; set; }
        /// <summary>
        /// Organization Members Quantity
        /// </summary>
        public int OrganizationMembersQuantity { get; set; }
        /// <summary>
        /// Primary Address Id
        /// </summary>
        public int PrimaryAddressId { get; set; }
        /// <summary>
        /// Primary Contact Id
        /// </summary>
        public int PrimaryContactId { get; set; }
        /// <summary>
        /// Phone Number 1 Type Code
        /// </summary>
        public string Phone1TypeCode { get; set; }
        /// <summary>
        /// Phone Number 1
        /// </summary>
        public string Phone1 { get; set; }
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
        /// Web Page
        /// </summary>
        public string WebPage { get; set; }
        /// <summary>
        /// Sales Person Code
        /// </summary>
        public string SalesPersonCode { get; set; }
        /// <summary>
        /// Sales Service Person Code
        /// </summary>
        public string SalesServicePersonCode { get; set; }
        /// <summary>
        /// Lead RTG Code
        /// </summary>
        public string LeadRTGCode { get; set; }
        /// <summary>
        /// Lead Status Code
        /// </summary>
        public string LeadStatusCode { get; set; }
        /// <summary>
        /// Lead Status Modification Date
        /// </summary>
        public DateTime? LeadStatusModificationDate { get; set; }
        /// <summary>
        /// Lead Referral Code
        /// </summary>
        public string LeadReferralCode { get; set; }
        /// <summary>
        /// Lead Referral Modification Date
        /// </summary>
        public DateTime? LeadReferralModificationDate { get; set; }
        /// <summary>
        /// Payment Termination Code
        /// </summary>
        public string PaymentTerminationCode { get; set; }
        /// <summary>
        /// SOLM Account Number
        /// </summary>
        public string SOLMAccountNumber { get; set; }
        /// <summary>
        /// GM Account Number
        /// </summary>
        public string GMAccountNumber { get; set; }
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
        public DateTime? LastModificationDate { get; set; }
        /// <summary>
        /// Last Modification By Person Code
        /// </summary>
        public string LastModificationPersonCode { get; set; }
        /// <summary>
        /// OrganizationCanonical Name
        /// </summary>
        public string OrganizationCanonicalName { get; set; }
        /// <summary>
        /// Contacts
        /// </summary>
        public IList<EzFundContact> Contacts { get; set; }
        /// <summary>
        /// Contact Addresses
        /// </summary>
        public IList<EzFundContactAddress> ContactAddresses { get; set; }
    }
}
