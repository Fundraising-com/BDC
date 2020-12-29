namespace GA.BDC.Data.MGP.EfundraisingProd.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Req_Language
    {
        public Req_Language()
        {
            Req_Decision = new HashSet<Req_Decision>();
            Req_Priority = new HashSet<Req_Priority>();
            Req_Project_Type = new HashSet<Req_Project_Type>();
            Req_Request_Type = new HashSet<Req_Request_Type>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Language_Id { get; set; }

        [StringLength(30)]
        public string Language { get; set; }

        public virtual ICollection<Req_Decision> Req_Decision { get; set; }

        public virtual ICollection<Req_Priority> Req_Priority { get; set; }

        public virtual ICollection<Req_Project_Type> Req_Project_Type { get; set; }

        public virtual ICollection<Req_Request_Type> Req_Request_Type { get; set; }
    }
}
