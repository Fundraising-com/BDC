namespace GA.BDC.Data.ESubs.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class email_import_test
    {
        [StringLength(200)]
        public string first_name { get; set; }

        [StringLength(200)]
        public string last_name { get; set; }

        [StringLength(200)]
        public string email { get; set; }

        public int id { get; set; }
    }
}
