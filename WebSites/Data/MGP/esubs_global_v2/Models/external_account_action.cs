namespace GA.BDC.Data.MGP.esubs_global_v2.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class external_account_action
    {
        [Key]
        public int external_account_action_id { get; set; }

        public int external_account_id { get; set; }

        public int action_id { get; set; }

        public DateTime create_date { get; set; }
    }
}
