using System.Collections.Generic;

namespace GA.BDC.Shared.Entities
{
    public class Representative
    {
        public Representative()
        {
            Testimonials = new List<Testimonial>();
        }
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// External Id
        /// </summary>
        public int ExternalId { get; set; }
        /// <summary>
        /// Email
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// Is Active
        /// </summary>
        public bool IsActive { get; set; }
        /// <summary>
        /// Redirect
        /// </summary>
        public string Redirect { get; set; }
        /// <summary>
        /// City
        /// </summary>
        public string City { get; set; }
        /// <summary>
        /// State
        /// </summary>
        public string State { get; set; }
        /// <summary>
        /// Image
        /// </summary>
        public string Image { get; set; }
        /// <summary>
        /// Phone
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// SAP Account
        /// </summary>
        public int SAPAccount { get; set; }
        /// <summary>
        /// Amount Raised
        /// </summary>
        public double AmountRaised { get; set; }
        /// <summary>
        /// Testimonials
        /// </summary>
        public IList<Testimonial> Testimonials { get; set; }

        public int PartnerId { get; set; }
    }

    
}
