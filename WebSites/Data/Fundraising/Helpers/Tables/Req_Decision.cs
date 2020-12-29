namespace GA.BDC.Data.Fundraising.EFundraisingProd.Tables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Req_Decision
    {
        public Req_Decision()
        {
            Req_Request = new HashSet<Req_Request>();
        }

        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Decision_Id { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Language_Id { get; set; }

        [StringLength(100)]
        public string Description { get; set; }

        public virtual ICollection<Req_Request> Req_Request { get; set; }

        public virtual Req_Language Req_Language { get; set; }
    }
}
