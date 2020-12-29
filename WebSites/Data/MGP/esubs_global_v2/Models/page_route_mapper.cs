namespace GA.BDC.Data.MGP.esubs_global_v2.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public class page_route_mapper
    {
        [Key]
        public int route_id { get; set; }

        [StringLength(10)]
        public string file_path_extension { get; set; }

        [StringLength(200)]
        public string source_file_path { get; set; }

        [StringLength(200)]
        public string destination_file_path { get; set; }

        public bool is_public { get; set; }

        [StringLength(300)]
        public string description { get; set; }

        public DateTime create_date { get; set; }

        public bool participant_id_required { get; set; }

        public bool enforce_parent_participant_id { get; set; }

        [StringLength(200)]
        public string append_qs { get; set; }
    }
}
