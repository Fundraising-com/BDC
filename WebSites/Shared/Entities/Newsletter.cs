using System;

namespace GA.BDC.Shared.Entities
{
    public class Newsletter
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Title
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// Url
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// Body
        /// </summary>
        public string Body { get; set; }
        /// <summary>
        /// Created On
        /// </summary>
        public DateTime Created_on { get; set; }
        /// <summary>
        /// Enabled
        /// </summary>
        public bool Enabled { get; set; }
        /// <summary>
        /// Author
        /// </summary>
        public string Author { get; set; }
        /// <summary>
        /// Updated On
        /// </summary>
        public DateTime UpdatedOn { get; set; }
        /// <summary>
        /// Partner
        /// </summary>
        public int Partner { get; set; }
        /// <summary>
        /// Dispay Order
        /// </summary>
        public int DisplayOrder { get; set; }

    }
}
