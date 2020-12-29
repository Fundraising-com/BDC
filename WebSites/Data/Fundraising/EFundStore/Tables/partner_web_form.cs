namespace GA.BDC.Data.Fundraising.EFundStore.Tables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class partner_web_form
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int partner_id { get; set; }

        public int web_form_id { get; set; }

        [StringLength(600)]
        public string recipient { get; set; }

        public int? web_from_type_id { get; set; }
    }
}
