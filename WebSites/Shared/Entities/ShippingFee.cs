namespace GA.BDC.Shared.Entities
{
    public class ShippingFee
    {
        /// <summary>
        /// Minimum quantity
        /// </summary>
        public int MinimumQuantity { get; set; }
        /// <summary>
        /// Maximum Quantity
        /// </summary>
        public int MaximumQuantity { get; set; }
        /// <summary>
        /// Fee
        /// </summary>
        public double Fee { get; set; }
    }
}
