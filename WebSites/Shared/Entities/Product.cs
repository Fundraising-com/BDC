using System;
using System.Collections.Generic;
using System.Linq;

namespace GA.BDC.Shared.Entities
{
   public class Product
   {
      public Product()
      {
         Profits = new List<Profit>();
         Reviews = new List<Review>();
			SubProducts = new List<SubProduct>();
         ShippingFees = new List<ShippingFee>();
        }
      /// <summary>
      /// Id
      /// </summary>
      public int Id { get; set; }
      /// <summary>
      /// Parent Id
      /// </summary>
      public int ParentId { get; set; }
      /// <summary>
      /// ExternalId
      /// </summary>
      public int ExternalId { get; set; }
      /// <summary>
      /// ExternalId
      /// </summary>
      public string ExternalCode { get; set; }
      /// <summary>
      /// Name
      /// </summary>
      public string Name { get; set; }
      /// <summary>
      /// Friendly Url
      /// </summary>
      public string Url { get; set; }
      /// <summary>
      /// Base Price, used in case there is no Profit Chart
      /// </summary>
      public double Price { get; set; }
      /// <summary>
      /// Selling suggested rice 
      /// </summary>
      public double SuggestedPrice { get; set; }
      /// <summary>
      /// MYOC minimum purchased quantity
      /// </summary>
      public int MinimumDivisor { get; set; }
      /// <summary>
      /// Description
      /// </summary>
      public string Description { get; set; }
      /// <summary>
      /// Flavors
      /// </summary>
      public string Flavors { get; set; }
      /// <summary>
      /// Packaging
      /// </summary>
      public string Packaging { get; set; }
      /// <summary>
      /// Image
      /// </summary>
      public Image Image { get; set; }
      /// <summary>
      /// BannerImage
      /// </summary>
      public Image BannerImage { get; set; }
      /// <summary>
      /// Category
      /// </summary>
      public Category Category { get; set; }
      /// <summary>
      /// Category Id
      /// </summary>
      public int CategoryId { get; set; }
      /// <summary>
      /// Retail Price
      /// </summary>
      public double RetailPrice { get; set; }
      /// <summary>
      /// When True, Product can be Purchased in the Website
      /// </summary>
      public bool CanBePurchased { get; set; }
      /// <summary>
      /// When True, the client can create its own case with the subproducts
      /// </summary>
      public bool IsStackedProduct { get; set; }
      /// <summary>
      /// Profit Chart Items
      /// </summary>
      public IList<Profit> Profits { get; set; }
		/// <summary>
		/// Raising Potential
		/// </summary>
		public double RaisingPotential { get; set; }
      /// <summary>
      /// Minium Purchase Quantity
      /// </summary>
      public int MinimumQuantity { get; set; }
      /// <summary>
      /// Extra Information
      /// </summary>
      public string ExtraInformation { get; set; }
      /// <summary>
      /// Extra Information
      /// </summary>
      public string FeaturedInformation { get; set; }
      /// <summary>
      /// Scratchbook Id, this must be set if the item is to be purchased
      /// </summary>
      public int ScratchBookId { get; set; }
      /// <summary>
      /// Code
      /// </summary>
      public string Code { get; set; }
      /// <summary>
      /// Display Order in the general products page
      /// </summary>
      public int DisplayOrder { get; set; }
      /// <summary>
      /// Display Order in the featured products rotator in home page
      /// </summary>
      public int DisplayOrderFeatured { get; set; }
      /// <summary>
      /// Product classes, used to group sales
      /// </summary>
      public KeyValuePair<int, string> ProductClass { get; set; }
      /// <summary>
      /// SubProducts List. Ej. Same product but different packaging
      /// </summary>
      public IList<SubProduct> SubProducts { get; set; }
      /// <summary>
      /// Meta Description
      /// </summary>
      public string METADescription { get; set; }
      /// <summary>
      /// Meta keywords
      /// </summary>
      public string METAKeywords { get; set; }
      /// <summary>
      /// Meta title
      /// </summary>
      public string METATitle { get; set; }
      /// <summary>
      /// Canonical Url
      /// </summary>
      public string CanonicalUrl { get; set; }
      /// <summary>
      /// Profit Percentage
      /// </summary>
      public double ProfitPercentage { get; set; }
      /// <summary>
      /// Calculated Price based on the Profit Chart and Number of Items Purchased, this doesn't come from the DB.
      /// </summary>
      public double CalculatedPrice { get; set; }
      /// <summary>
      /// Indicates that the Product requires taxes at the moment of being sold
      /// </summary>
      public bool RequireTaxes { get; set; }
      /// <summary>
      /// Two letters that indicates the country where this product can be sold. I.e. US, CA, etc.
      /// </summary>
      public string CountryCode { get; set; }
      /// <summary>
      /// List of Taxes for this particular Product, in case if it requires taxes
      /// </summary>
      public IList<StateTax> Taxes { get; set; }
      /// <summary>
      /// Currency code, i.e. USD, CAD
      /// </summary>
      public string CurrencyCode { get; set; }
        /// <summary>
        /// Reviews
        /// </summary>

      public AdvertiserProducts AdvertisingOverride { get; set; }

        /// <summary>
        /// Shipping Fees
        /// </summary>
        public IList<ShippingFee> ShippingFees { get; set; }

        public IList<Review> Reviews { get; set; }
      /// <summary>
      /// Calculated Review's Rating
      /// </summary>
      public int Rating {
         get { return Reviews.Any() ? (int) Math.Round(Reviews.Average(p => p.Rating), 0) : 0; }
        }
    }
}
