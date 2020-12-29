using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GA.BDC.Shared.Entities
{
    public class SubProduct
    {
        public string ItemCode { get; set; }
        public string ProductCode { get; set; }
        public string Name { get; set; }
        public string SizeName { get; set; }
        public int SapNumber { get; set; }
        public double Price { get; set; }
        public string SkuCode{ get; set; }
        public int ItemSeqNbr { get; set; }
        public int ParentId { get; set; }
        public int ProductQuantity { get; set; }
        public double ProductSuggestedPrice { get; set; }
        public int SelectedQuantity { get; set; }
        public int StackedQuantity { get; set; } //used when inserting in EzOps
        public IList<ItemWarehouse> Warehouse { get; set; }
        public IList<ItemVendor> Vendor { get; set; }
		  public IList<Profit> Profit { get; set; }
	}
}
