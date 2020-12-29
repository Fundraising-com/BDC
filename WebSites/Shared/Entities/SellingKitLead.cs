using System;
using System.Collections.Generic;
using System.Text;

namespace GA.BDC.Shared.Entities
{
    public class SellingKitLead
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }
        
        /// <summary>
        /// First Name
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// Title
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Group
        /// </summary>
        public string Group { get; set; }
        
        /// <summary>
        /// GroupType
        /// </summary>
        public string GroupType { get; set; }

        /// <summary>
        /// Address1
        /// </summary>
        public Address Address1 { get; set; }

        /// <summary>
        /// Address2
        /// </summary>
        public Address Address2 { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        public string Email { get; set; }
        
        /// <summary>
        /// Phone1
        /// </summary>
        public string Phone1 { get; set; }

        /// <summary>
        /// Phone2
        /// </summary>
        public string Phone2 { get; set; }

        /// <summary>
        /// Fax
        /// </summary>
        public string Fax { get; set; }

        /// <summary>
        /// Number of Members
        /// </summary>
        public int NumberOfMembers { get; set; }
        
        /// <summary>
        /// Profit Amount
        /// </summary>
        public int ProfitAmount { get; set; }
        
        /// <summary>
        /// Start Date
        /// </summary>
        public DateTime StartDate { get; set; }
        
        /// <summary>
        /// Prize Required
        /// </summary>
        public bool PrizeRequired { get; set; }
       
        /// <summary>
        /// Age Level
        /// </summary>
        public string AgeLevel { get; set; }
        
        /// <summary>
        /// Special Note Text
        /// </summary>
        public string SpecialNoteText { get; set; }
        
        /// <summary>
        /// Primary Program Code
        /// </summary>
        public string PrimaryProgramCode { get; set; }
        
        /// <summary>
        /// Tag Program
        /// </summary>
        public string TagProgram { get; set; }
        
        /// <summary>
        /// Comment Text
        /// </summary>
        public string CommentText { get; set; }
        
        /// <summary>
        /// Source Code
        /// </summary>
        public string SourceCode { get; set; }
        
        /// <summary>
        /// Referral Code
        /// </summary>
        public string ReferralCode { get; set; }
        
        /// <summary>
        /// Original Prospect Date
        /// </summary>
        public DateTime OriginalProspectDate { get; set; }
        
        /// <summary>
        /// Session ID
        /// </summary>
        public string SessionId { get; set; }
        
        /// <summary>
        /// Remote Ip Address
        /// </summary>
        public string RemoteIpAddress { get; set; }
        
        /// <summary>
        /// Prospect Stat Code
        /// </summary>
        public string ProspectStatCode { get; set; }
        
        /// <summary>
        /// Last Date Modified
        /// </summary>
        public DateTime LastDateModified { get; set; }
        
        /// <summary>
        /// Last Modified Person
        /// </summary>
        public string LastModifiedPerson { get; set; }

        /// <summary>
        /// Prospect Mail Date
        /// </summary>
        public DateTime ProspectMailDate { get; set; }
        
        /// <summary>
        /// Sales Person Code
        /// </summary>
        public string SalesPersonCode { get; set; }
        
        //public string FindDifferences(Lead targetLead)
        //{
        //    var result = new StringBuilder();
        //    if (!String.Equals(FirstName, targetLead.FirstName, StringComparison.InvariantCultureIgnoreCase))
        //    {
        //        result.AppendFormat("First Name: {0},", FirstName);
        //    }
        //    if (!String.Equals(LastName, targetLead.LastName, StringComparison.InvariantCultureIgnoreCase))
        //    {
        //        result.AppendFormat("Last Name: {0},", LastName);
        //    }
        //    if (!String.Equals(Email, targetLead.Email, StringComparison.InvariantCultureIgnoreCase))
        //    {
        //        result.AppendFormat("Email: {0},", Email);
        //    }
        //    if (!String.Equals(Phone, targetLead.Phone, StringComparison.InvariantCultureIgnoreCase))
        //    {
        //        result.AppendFormat("Phone: {0},", Phone);
        //    }
        //    if (!String.Equals(Group, targetLead.Group, StringComparison.InvariantCultureIgnoreCase))
        //    {
        //        result.AppendFormat("Group: {0},", Group);
        //    }
        //    if (NumberOfMembers != targetLead.NumberOfMembers)
        //    {
        //        result.AppendFormat("Number of Members: {0},", NumberOfMembers);
        //    }
        //    if (Address != null && targetLead.Address != null && !String.Equals(Address.Address1, targetLead.Address.Address1, StringComparison.InvariantCultureIgnoreCase))
        //    {
        //        result.AppendFormat("Address: {0},", Address.Address1);
        //    }
        //    if (Address != null && targetLead.Address != null && !String.Equals(Address.City, targetLead.Address.City, StringComparison.InvariantCultureIgnoreCase))
        //    {
        //        result.AppendFormat("City: {0},", Address.City);
        //    }
        //    if (Address != null && targetLead.Address != null && !String.Equals(Address.PostCode, targetLead.Address.PostCode, StringComparison.InvariantCultureIgnoreCase))
        //    {
        //        result.AppendFormat("Postal Code: {0},", Address.PostCode);
        //    }
        //    return result.ToString();
       
    }
}
