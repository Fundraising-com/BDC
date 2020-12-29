namespace GA.BDC.Data.MGP.eFundweb.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class unsubscribe_puffy
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int unsubscribe_id { get; set; }
    }
}
