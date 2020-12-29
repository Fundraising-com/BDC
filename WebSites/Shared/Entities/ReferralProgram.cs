using System.Collections.Generic;

namespace GA.BDC.Shared.Entities
{
    public class ReferralProgram
    {
        public ReferralProgram()
        {
            Friends = new List<Friend>();
        }
        /// <summary>
        /// First Name
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// Last Name
        /// </summary>
        public string LastName { get; set; }
        /// <summary>
        /// Consultant
        /// </summary>
        public string Consultant { get; set; }
        /// <summary>
        /// Already Purchased
        /// </summary>
        public int AlreadyPurchased { get; set; }
        /// <summary>
        /// Address
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// City
        /// </summary>
        public string City { get; set; }
        /// <summary>
        /// State
        /// </summary>
        public string State { get; set; }
        /// <summary>
        /// Country
        /// </summary>
        public string Country { get; set; }
        /// <summary>
        /// Phone
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// Phone 2
        /// </summary>
        public string Phone2 { get; set; }
        /// <summary>
        /// Email
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// Reference Number
        /// </summary>
        public string ReferenceNumber { get; set; }
        /// <summary>
        /// Friends
        /// </summary>
        public IList<Friend> Friends { get; set; }
    }

    
}
