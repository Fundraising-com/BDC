namespace GA.BDC.Data.MGP.eFundweb.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Hear_About_Us
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int hear_ID { get; set; }

        [Required]
        [StringLength(50)]
        public string name { get; set; }

        public int Order_On_Web { get; set; }

        public bool? Is_Active { get; set; }

        public int Party_Type_ID { get; set; }

        [StringLength(50)]
        public string Name_Fr { get; set; }
    }
}
