using System;

namespace GA.BDC.Shared.Entities
{
    public class ShoppingCartItem
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Id
        /// </summary>
        public int ParentId { get; set; }
        /// <summary>
        /// Quantity
        /// </summary>
        public int Quantity { get; set; }
        /// <summary>
        /// Product
        /// </summary>
        public Product Product { get; set; }
        /// <summary>
        /// Comments
        /// </summary>
        public string Comments { get; set; }
        /// <summary>
        /// Created
        /// </summary>
        public DateTime Created { get; set; }
        /// <summary>
        /// Product Id
        /// </summary>
        public int ProductId { get; set; }
        /// <summary>
        /// Item Code
        /// </summary>
        public string ItemCode { get; set; }
        /// <summary>
        /// Shopping Cart Id
        /// </summary>
        public int ShoppingCartId { get; set; }
    }
}
