namespace GA.BDC.Data.MGP.eFundweb.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Advertising_Support
    {
        public Advertising_Support()
        {
            C_tbd_promotion = new HashSet<C_tbd_promotion>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Advertising_Support_ID { get; set; }

        public int? Advertising_Support_Type_ID { get; set; }

        [StringLength(50)]
        public string Title { get; set; }

        public DateTime? Publishnig_Date { get; set; }

        [StringLength(100)]
        public string Web_Site { get; set; }

        [StringLength(25)]
        public string Ordering_Phone_Number { get; set; }

        public int? Periodicity { get; set; }

        public int? Nb_Draw { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? Magazine_Price { get; set; }

        [StringLength(255)]
        public string Comments { get; set; }

        public virtual ICollection<C_tbd_promotion> C_tbd_promotion { get; set; }

        public virtual Advertising_Support_Type Advertising_Support_Type { get; set; }
    }
}
