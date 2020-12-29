namespace GA.BDC.Data.MGP.EfundraisingProd.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class credit_card_types
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public byte credit_card_type_id { get; set; }

        public byte payment_method_id { get; set; }

        [Required]
        [StringLength(25)]
        public string credit_card_type_name { get; set; }

        [StringLength(25)]
        public string credit_card_image { get; set; }

        public byte display_order { get; set; }

        public bool displayable { get; set; }

        public virtual payment_method payment_method { get; set; }
    }
}
