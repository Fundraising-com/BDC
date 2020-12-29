namespace GA.BDC.Data.MGP.esubs_global_v2.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class member_phone_number
    {
        [Key]
        public int member_phone_number_id { get; set; }

        public int member_id { get; set; }

        public int phone_number_type_id { get; set; }

        public int phone_number_id { get; set; }

        public bool active { get; set; }

        public DateTime create_date { get; set; }

        public virtual member member { get; set; }

        public virtual phone_number phone_number { get; set; }

        public virtual phone_number_type phone_number_type { get; set; }
    }
}
