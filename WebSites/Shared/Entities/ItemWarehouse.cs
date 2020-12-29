using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GA.BDC.Shared.Entities
{
    public class ItemWarehouse
    {
        /// <summary>
        /// Item Code
        /// </summary>
        public string ItemCode { get; set; }
        /// <summary>
        /// Warehouse Code
        /// </summary>
        public string WarehouseCode { get; set; }
        /// <summary>
        /// Warehouse Item Code
        /// </summary>
        public string WarehouseItemCode { get; set; }
        /// <summary>
        /// Warehouse Item Name
        /// </summary>
        public string WarehouseItemName { get; set; }
        /// <summary>
        /// Warehouse Item Sequential Number
        /// </summary>
        public int WarehouseItemSeqNumber { get; set; }
    }
}
