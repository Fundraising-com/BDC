namespace GA.BDC.Data.MGP.eFundweb.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Web_Site
    {
        public Web_Site()
        {
            Destinations = new HashSet<Destination>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Web_Site_ID { get; set; }

        [StringLength(50)]
        public string Web_Site_Name { get; set; }

        public virtual ICollection<Destination> Destinations { get; set; }
    }
}
