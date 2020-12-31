using System;

namespace QSPFulfillment.DataAccess.Common.ActionObject
{
	/// <summary>
	/// Wraps a DataGridItem to allow it to represent a magazine item.
	/// </summary>
	[Serializable]
	public class ProductItem
	{
		private ProductItemCollection parentCollection = null;
		private int magPrice_instance = 0;
		private string product_code = String.Empty;
		private string product_sort_name = String.Empty;
		private int term = 0;
		private int quantity = 0;
		private float catalog_price = 0.0F;
		private float price = 0.0F;
		private string lang = String.Empty;
		private string catalog_name = String.Empty;
		private int productType = 0;
		private float enterredPrice = 0.0F;
		private int priceOverrideReason = 0;
		private int transID=-1;
		private int statusInstance=-1;
		private string zRecipient="";
		private int isDeleted = 0;
		private int customerShipToInstance = 0;
		private int productYear = 0;
		private string productSeason = String.Empty;
        private int productReplacementReason = 0;

		public ProductItemCollection ParentCollection 
		{
			get 
			{
				return parentCollection;
			}
			set 
			{
				parentCollection = value;
			}
		}

		public int MagPrice_instance 
		{
			get 
			{
				return this.magPrice_instance;
			}
			set 
			{
				this.magPrice_instance = value;
			}
		}

		public string Product_code 
		{
			get 
			{
				return this.product_code;
			}
			set 
			{
				this.product_code = value;
			}
		}

		public string Product_sort_name 
		{
			get 
			{
				return this.product_sort_name;
			}
			set 
			{
				this.product_sort_name = value;
			}
		}

		public int Term 
		{
			get 
			{
				return this.term;
			}
			set 
			{
				this.term = value;
			}
		}

		public int Quantity 
		{
			get 
			{
				return this.quantity;
			}
			set 
			{
				this.quantity = value;
			}
		}

		public float Catalog_Price 
		{
			get 
			{
				return this.catalog_price;
			}
			set 
			{
				this.catalog_price = value;
			}
		}

		public float Price 
		{
			get 
			{
				return this.price;
			}
			set 
			{
				this.price = value;
			}
		}

		public string Lang 
		{
			get 
			{
				return this.lang;
			}
			set 
			{
				this.lang = value;
			}
		}

		public string Catalog_Name 
		{
			get 
			{
				return this.catalog_name;
			}
			set 
			{
				this.catalog_name = value;
			}
		}

		public int ProductType 
		{
			get 
			{
				return this.productType;
			}
			set 
			{
				this.productType = value;
			}
		}

		public float EnterredPrice 
		{
			get 
			{
				return this.enterredPrice;
			}
			set 
			{
				this.enterredPrice = value;
			}
		}

		public int PriceOverrideReason 
		{
			get 
			{
				return this.priceOverrideReason;
			}
			set 
			{
				this.priceOverrideReason = value;
			}
		}
		public int TransID 
		{
			get 
			{
				return this.transID;
			}
			set 
			{
				this.transID = value;
			}
		}
		public int StatusInstance 
		{
			get 
			{
				return this.statusInstance;
			}
			set 
			{
				this.statusInstance = value;
			}
		}
		public string Recipient
		{
			get
			{
				return this.zRecipient;
			}
			set
			{
				this.zRecipient=value;
			}
		}

		public int CustomerShipToInstance 
		{
			get 
			{
				return this.customerShipToInstance;
			}
			set 
			{
				this.customerShipToInstance = value;
			}
		}

		public int Product_Year 
		{
			get 
			{
				return this.productYear;
			}
			set 
			{
				this.productYear = value;
			}
		}

		public string Product_Season
		{
			get 
			{
				return this.productSeason;
			}
			set 
			{
				this.productSeason = value;
			}
		}

        public int ProductReplacementReason
        {
            get
            {
                return this.productReplacementReason;
            }
            set
            {
                this.productReplacementReason = value;
            }
        }

		public override bool Equals(object obj)
		{
			ProductItem item = obj as ProductItem;
			bool isEqual = false;
			
			if(item != null) 
			{
				isEqual = (this.MagPrice_instance == item.MagPrice_instance);
			}

			return isEqual;
		}

		public override int GetHashCode()
		{
			return this.MagPrice_instance;
		}

		public int IsDeleted 
		{
			get 
			{
				return this.isDeleted;
			}
			set 
			{
				this.isDeleted = value;
			}
		}
	}
}
