using System;
using System.Collections.Generic;
using System.Text;

namespace GA.BDC.Shared.Entities
{
    public class Lead
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// First Name
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// Last Name
        /// </summary>
        public string LastName { get; set; }
        /// <summary>
        /// Group
        /// </summary>
        public string Group { get; set; }
        /// <summary>
        /// Website
        /// </summary>
        public string Website { get; set; }
        /// <summary>
        /// Email
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// GroupType
        /// </summary>
        public int GroupType { get; set; }
        /// <summary>
        /// Organization Type
        /// </summary>
        public int OrgType { get; set; }
        /// <summary>
        /// Address
        /// </summary>
        public Address Address { get; set; }
        /// <summary>
        /// Phone
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// Number of Members
        /// </summary>
        public int NumberOfMembers { get; set; }
        /// <summary>
        /// Promotion Id
        /// </summary>
        public int PromotionId { get; set; }
        /// <summary>
        /// Partner Id
        /// </summary>
        public int PartnerId { get; set; }
         /// <summary>
        /// Interest
        /// </summary>
        public string Interest { get; set; }
        /// <summary>
        /// Comments
        /// </summary>
        public string Comments { get; set; }
        /// <summary>
        /// Kit Type
        /// </summary>
        public int KitType { get; set; }
        /// <summary>
        /// Partner Guid
        /// </summary>
        public string PartnerGuid { get; set; }
        /// <summary>
        /// Consultant id
        /// </summary>
        public int ConsultantId { get; set; }
        /// <summary>
        /// Representative Id
        /// </summary>
        public int RepresentativeId { get; set; }
        /// <summary>
        /// Channel Code
        /// </summary>
        public string ChannelCode { get; set; }
        /// <summary>
        /// Request Type
        /// </summary>
        public int RequestType { get; set; }
        /// <summary>
        /// Tell More Information
        /// </summary>
        public int TellMore { get; set; }
        /// <summary>
        /// Indicates if the Lead is duplicated
        /// </summary>
        public bool IsDuplicated { get; set; }
        /// <summary>
        /// indicates potential duplicate lead
        /// </summary>
        public bool IsPotentiallyDuplicated { get; set; }

        /// <summary>
        /// indicates phone number intially added on lead submittion
        /// </summary>
        public bool? InitialPhoneNumberEntered { get; set; }

        /// <summary>
        /// indicates sent_to_pap 
        /// </summary>
        public bool? SentToPap { get; set; }

        /// <summary>
        /// Is Suscribed to Newsletter
        /// </summary>
        public bool IsSuscribed { get; set; }
        /// <summary>
        /// Consultant
        /// </summary>
        public Consultant Consultant { get; set; }
        /// <summary>
        /// Promotion
        /// </summary>
        public Promotion Promotion { get; set; }
        /// <summary>
        /// Partner
        /// </summary>
        public Partner Partner { get; set; }

        /// <summary>
        /// Product List
        /// </summary>
        public List<string> SelectedProducts { get; set; }
        /// <summary>
        /// Expected Amount to Raise
        /// </summary>
        public string AmountToRaise { get; set; }
        /// <summary>
        /// Expected Start Range
        /// </summary>
        public string StartRange { get; set; }
        /// <summary>
        /// How Were We Found Code
        /// </summary>
        public string ReferralCode { get; set; }
        /// <summary>
        /// EzFund Lead Referral Url
        /// </summary>
        public string ReferralUrl { get; set; }
        /// <summary>
        /// EzFund Lead Source Code
        /// </summary>
        public string SourceCode { get; set; }
       
        
        public string ContactTitle { get; set; }
        
        public string Phone2 { get; set; }
        public string Fax { get; set; }
       
        public string OrgMembQtyTxt { get; set; }
        public string UnitSlsSizeTxt { get; set; }
        public int SlsInqQty { get; set; }
        public int SrcSeqNbr { get; set; }
        public string RfrlCde { get; set; }
        public DateTime? OrigProsDte { get; set; }
        public object SlspRfrlCde { get; set; }
        public object ProcMailDte { get; set; }
        public object LastModfPrsnCde { get; set; }
        public DateTime? LastModfDte { get; set; }
        public string ProsStatCdc { get; set; }
        public string RmtIpAddr { get; set; }
        public string SessIdNbr { get; set; }
        public string CtctNme { get; set; }
        public string OrgNme { get; set; }
        public string Addr1Txt { get; set; }
        public string CityNme { get; set; }
        public string StCde { get; set; }
        public string ZipCde { get; set; }
        public string EmlTxt { get; set; }
        public string Ph1Nbr { get; set; }
        public int OrgMembQty { get; set; }
        public string TargPrftAmtTxt { get; set; }
        public string SlsStrtTxt { get; set; }
        public string CmntTxt { get; set; }
        public string GrpType { get; set; }


        /// <summary>
        /// Compares two Leads intances and return their differences in a String
        /// </summary>
        /// <param name="targetLead"></param>
        /// <returns></returns>
        public string FindDifferences(Lead targetLead)
        {
            var result = new StringBuilder();
            if (!String.Equals(FirstName, targetLead.FirstName, StringComparison.InvariantCultureIgnoreCase))
            {
                result.AppendFormat("First Name: {0},", FirstName);
            }
            if (!String.Equals(LastName, targetLead.LastName, StringComparison.InvariantCultureIgnoreCase))
            {
                result.AppendFormat("Last Name: {0},", LastName);
            }
            if (!String.Equals(Email, targetLead.Email, StringComparison.InvariantCultureIgnoreCase))
            {
                result.AppendFormat("Email: {0},", Email);
            }
            if (!String.Equals(Phone, targetLead.Phone, StringComparison.InvariantCultureIgnoreCase))
            {
                result.AppendFormat("Phone: {0},", Phone);
            }
            if (!String.Equals(Group, targetLead.Group, StringComparison.InvariantCultureIgnoreCase))
            {
                result.AppendFormat("Group: {0},", Group);
            }
            if (NumberOfMembers != targetLead.NumberOfMembers)
            {
                result.AppendFormat("Number of Members: {0},", NumberOfMembers);
            }
            if (Address != null && targetLead.Address != null && !String.Equals(Address.Address1, targetLead.Address.Address1, StringComparison.InvariantCultureIgnoreCase))
            {
                result.AppendFormat("Address: {0},", Address.Address1);
            }
            if (Address != null && targetLead.Address != null && !String.Equals(Address.City, targetLead.Address.City, StringComparison.InvariantCultureIgnoreCase))
            {
                result.AppendFormat("City: {0},", Address.City);
            }
            if (Address != null && targetLead.Address != null && !String.Equals(Address.PostCode, targetLead.Address.PostCode, StringComparison.InvariantCultureIgnoreCase))
            {
                result.AppendFormat("Postal Code: {0},", Address.PostCode);
            }
            return result.ToString();
        }
    }
}
