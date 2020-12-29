namespace GA.BDC.Data.MGP.esubs_global_v2.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("event")]
    public partial class _event
    {
        public _event()
        {
            event_gasavingcard = new HashSet<event_gasavingcard>();
            event_group = new HashSet<event_group>();
            event_participation = new HashSet<event_participation>();
            event_profit_range = new HashSet<event_profit_range>();
        }

        [Key]
        public int event_id { get; set; }

        public int event_type_id { get; set; }

        [Required]
        [StringLength(5)]
        public string culture_code { get; set; }

        [StringLength(200)]
        public string event_name { get; set; }

        public DateTime start_date { get; set; }

        public DateTime? end_date { get; set; }

        public bool active { get; set; }

        [StringLength(1024)]
        public string comments { get; set; }

        public DateTime create_date { get; set; }

        [StringLength(255)]
        public string redirect { get; set; }

        public bool displayable { get; set; }

        public bool? want_sales_rep_call { get; set; }

        public int group_type_id { get; set; }

        public bool? processing_fee { get; set; }

        public double profit_calculated { get; set; }

        public int event_status_id { get; set; }

        public int profit_group_id { get; set; }

        public bool donation { get; set; }

        public DateTime? date_of_event { get; set; }

        public bool? discount_site { get; set; }

        [StringLength(200)]
        public string humeur_representative { get; set; }

        public virtual culture culture { get; set; }

        public virtual event_status event_status { get; set; }

        public virtual event_type event_type { get; set; }

        public virtual ICollection<event_gasavingcard> event_gasavingcard { get; set; }

        public virtual ICollection<event_group> event_group { get; set; }

        public virtual ICollection<event_participation> event_participation { get; set; }

        public virtual ICollection<event_profit_range> event_profit_range { get; set; }

        public virtual group_type group_type { get; set; }
        /// <summary>
        /// 1 Efundraising
        /// </summary>
        public virtual int? referral_application { get; set; }
    }
}
