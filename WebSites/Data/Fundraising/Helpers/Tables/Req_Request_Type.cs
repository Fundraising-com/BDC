namespace GA.BDC.Data.Fundraising.EFundraisingProd.Tables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Req_Request_Type
    {
        public Req_Request_Type()
        {
            Req_Request = new HashSet<Req_Request>();
        }

        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Request_Type_ID { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Language_Id { get; set; }

        [StringLength(50)]
        public string Description { get; set; }

        public virtual Req_Language Req_Language { get; set; }

        public virtual ICollection<Req_Request> Req_Request { get; set; }
    }
}
