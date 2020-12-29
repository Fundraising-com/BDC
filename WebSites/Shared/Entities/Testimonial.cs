using System;

namespace GA.BDC.Shared.Entities
{
    public class Testimonial
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Rep's Id
        /// </summary>
        public int RepresentativeId { get; set; }
        /// <summary>
        /// Created
        /// </summary>
        public DateTime? Created { get; set; }
        /// <summary>
        /// Author
        /// </summary>
        public string Author { get; set; }
        /// <summary>
        /// Message
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// Account
        /// </summary>
        public string Account { get; set; }
    }
}
