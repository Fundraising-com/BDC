namespace GA.BDC.Data.MGP.EfundraisingProd.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class EFO_Sale
    {
        public EFO_Sale()
        {
            EFO_Sale_Item = new HashSet<EFO_Sale_Item>();
        }

        [Key]
        public int Sale_ID { get; set; }

        public int Supporter_ID { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime Sale_Date { get; set; }

        [Column(TypeName = "smallmoney")]
        public decimal? Amount_To_Group { get; set; }

        [Column(TypeName = "smallmoney")]
        public decimal? Amount_To_Supplier { get; set; }

        [Column(TypeName = "smallmoney")]
        public decimal? Amount { get; set; }

        [StringLength(75)]
        public string Delivery_Address { get; set; }

        [StringLength(10)]
        public string State_Code { get; set; }

        [StringLength(10)]
        public string Country_Code { get; set; }

        [StringLength(50)]
        public string Delivery_City { get; set; }

        [StringLength(10)]
        public string Delivery_Zip_Code { get; set; }

        [StringLength(30)]
        public string Card_Name { get; set; }

        [StringLength(75)]
        public string Card_Address { get; set; }

        [StringLength(15)]
        public string Transaction_ID { get; set; }

        public virtual Country1 Country { get; set; }

        public virtual State State { get; set; }

        public virtual EFO_Supporter EFO_Supporter { get; set; }

        public virtual ICollection<EFO_Sale_Item> EFO_Sale_Item { get; set; }
    }
}
