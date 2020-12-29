namespace GA.BDC.Data.MGP.EfundraisingProd.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class AR_Consultant
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int AR_Consultant_ID { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        [StringLength(3)]
        public string Phone_Ext { get; set; }

        public bool Is_Active { get; set; }

        [StringLength(50)]
        public string Nt_Login { get; set; }
    }
}
