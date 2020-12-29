namespace GA.BDC.Data.MGP.EfundraisingProd.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Lead_Activity_Closing_Reason
    {
        public Lead_Activity_Closing_Reason()
        {
            leads = new HashSet<lead>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public byte Activity_Closing_Reason_ID { get; set; }

        [Required]
        [StringLength(50)]
        public string Reason { get; set; }

        public virtual ICollection<lead> leads { get; set; }
    }
}
