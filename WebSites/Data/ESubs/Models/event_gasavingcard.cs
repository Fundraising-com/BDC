namespace GA.BDC.Data.ESubs.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class event_gasavingcard
    {
        public int id { get; set; }

        public int event_id { get; set; }

        public virtual _event _event { get; set; }
    }
}
