namespace GA.BDC.Shared.Entities
{
    public class Address
    {
        /// <summary>
        /// City
        /// </summary>
        public string City { get; set; }
        /// <summary>
        /// Address 1
        /// </summary>
        public string Address1 { get; set; }
        /// <summary>
        /// Address 2
        /// </summary>
        public string Address2 { get; set; }
        /// <summary>
        /// Country
        /// </summary>
        public Country Country { get; set; }
        /// <summary>
        /// County
        /// </summary>
        public string County { get; set; }
        /// <summary>
        /// Post Code
        /// </summary>
        public string PostCode { get; set; }
        /// <summary>
        /// Post Code 2
        /// </summary>
        public string PostCode2 { get; set; }
        /// <summary>
        /// Region (State)
        /// </summary>
        public Region Region { get; set; }
    }
}
