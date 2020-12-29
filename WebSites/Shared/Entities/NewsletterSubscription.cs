using System;

namespace GA.BDC.Shared.Entities
{
    public class NewsletterSubscription
    {

        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Partner_Id
        /// </summary>
        public int PartnerId { get; set; }
        /// <summary>
        /// Culture_Code
        /// </summary>
        public string CultureCode { get; set; }
        /// <summary>
        /// Referrer
        /// </summary>
        public string Referrer { get; set; }
        /// <summary>
        /// Email
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// Fullname
        /// </summary>
        public String Fullname { get; set; }
        /// <summary>
        /// Unsubscribed
        /// </summary>
        public bool Unsubscribed { get; set; }
        /// <summary>
        /// Updated On
        /// </summary>
        public DateTime SubscribeDate { get; set; }
        /// <summary>
        /// Partner
        /// </summary>
        public DateTime? UnsubscribeDate { get; set; }
      

    }
}
