using System.Collections.Generic;

namespace GA.BDC.Shared.Entities
{
    public class Category
    {
        public Category()
        {
            ShippingFees = new List<ShippingFee>();
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
        /// Url (Redirect)
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// Url (Redirect)
        /// </summary>
        public string ParentUrl { get; set; }
        /// <summary>
        /// Url (Redirect)
        /// </summary>
        public bool IsProduct { get; set; }
        /// <summary>
        /// Parent Category, WARNING, may no exist sometimes.
        /// </summary>
        public Category Parent { get; set; }
        /// <summary>
        /// Products under this category.  WARNING, may no exist sometimes.
        /// </summary>
        public IEnumerable<Product> Products { get; set; }
        /// <summary>
        /// Parent Package Id
        /// </summary>
        public int ParentId { get; set; }
        /// <summary>
        /// Shipping Fees
        /// </summary>
        public IList<ShippingFee> ShippingFees { get; set; }
        /// <summary>
        /// Meta Descriptions
        /// </summary>
        public string METADescription { get; set; }
        /// <summary>
        /// Meta Keywords
        /// </summary>
        public string METAKeywords { get; set; }
        /// <summary>
        /// Meta Title
        /// </summary>
        public string METATitle { get; set; }
        /// <summary>
        /// Description
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Used to Group the Top Menu in the Homepage
        /// </summary>
        public string LinkGroupKey { get; set; } 

        public string Description2 { get; set; }
        /// <summary>
        /// Image
        /// </summary>
        public Image Image { get; set; }
        /// <summary>
        /// Code
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// Title
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// Title
        /// </summary>
        public string TitleExtra { get; set; }
    }

    
}
