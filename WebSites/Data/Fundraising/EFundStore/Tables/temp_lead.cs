namespace GA.BDC.Data.Fundraising.EFundStore.Tables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class temp_lead
    {
        [Key]
        public int temp_lead_id { get; set; }

        public int? division_id { get; set; }

        public int? promotion_id { get; set; }

        [StringLength(4)]
        public string channel_code { get; set; }

        public int? lead_status_id { get; set; }

        public byte organization_type_id { get; set; }

        public byte campaign_reason_id { get; set; }

        public short? web_site_id { get; set; }

        public byte? group_type_id { get; set; }

        [StringLength(10)]
        public string salutation { get; set; }

        public byte? title_id { get; set; }

        public byte hear_id { get; set; }

        public DateTime? lead_entry_date { get; set; }

        [StringLength(50)]
        public string first_name { get; set; }

        [StringLength(50)]
        public string last_name { get; set; }

        [StringLength(100)]
        public string organization { get; set; }

        [StringLength(100)]
        public string street_address { get; set; }

        [StringLength(50)]
        public string city { get; set; }

        [StringLength(10)]
        public string state_code { get; set; }

        [StringLength(2)]
        public string country_code { get; set; }

        [StringLength(10)]
        public string zip_code { get; set; }

        [StringLength(20)]
        public string day_phone { get; set; }

        [StringLength(20)]
        public string day_time_call { get; set; }

        [StringLength(20)]
        public string evening_phone { get; set; }

        [StringLength(20)]
        public string fax { get; set; }

        [StringLength(50)]
        public string email { get; set; }

        public int? participant_count { get; set; }

        public int? fund_raising_goal { get; set; }

        public DateTime? decision_date { get; set; }

        public bool decision_maker { get; set; }

        public DateTime? fund_raiser_start_date { get; set; }

        public bool onemaillist { get; set; }

        [StringLength(2000)]
        public string comments { get; set; }

        [StringLength(10)]
        public string day_phone_ext { get; set; }

        [StringLength(10)]
        public string evening_phone_ext { get; set; }

        [StringLength(20)]
        public string other_phone { get; set; }

        [StringLength(255)]
        public string cookie_content { get; set; }

        [StringLength(50)]
        public string group_web_site { get; set; }

        [StringLength(10)]
        public string other_phone_ext { get; set; }

        public bool isnew { get; set; }

        public bool? opt_in_sweepstakes { get; set; }
    }
}
