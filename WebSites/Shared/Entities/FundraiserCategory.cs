using System.Collections.Generic;

namespace GA.BDC.Shared.Entities
{
    public class FundraiserCategory
    {
        public FundraiserCategory()
        {
            Products = new List<FundraiserProduct>();
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
        /// Order
        /// </summary>
        public int Order { get; set; }
        /// <summary>
        /// Image
        /// </summary>
        public string Image { get; set; }
        /// <summary>
        /// Url
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// Products
        /// </summary>
        public IList<FundraiserProduct> Products { get; set; }
    }
}
