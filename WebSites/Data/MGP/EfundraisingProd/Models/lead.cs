namespace GA.BDC.Data.MGP.EfundraisingProd.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("lead")]
    public partial class lead
    {
        public lead()
        {
            clients = new HashSet<client>();
            Comments1 = new HashSet<Comment>();
            Efr_Lead_Activity = new HashSet<Efr_Lead_Activity>();
            General_Comment = new HashSet<General_Comment>();
            lead_activity = new HashSet<lead_activity>();
            Lead_Promotion = new HashSet<Lead_Promotion>();
            Lead_Visit = new HashSet<Lead_Visit>();
            promotional_kit = new HashSet<promotional_kit>();
            Referees = new HashSet<Referee>();
            sale_to_add = new HashSet<sale_to_add>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int lead_id { get; set; }

        public int lead_status_id { get; set; }

        public int? lead_qualification_type_id { get; set; }

        public int? lead_priority_id { get; set; }

        public int? temp_lead_id { get; set; }

        public byte division_id { get; set; }

        public int promotion_id { get; set; }

        [Required]
        [StringLength(4)]
        public string channel_code { get; set; }

        public int consultant_id { get; set; }

        public byte group_type_id { get; set; }

        public byte organization_type_id { get; set; }

        public byte hear_id { get; set; }

        public int fk_kit_type_id { get; set; }

        public int? old_lead_id { get; set; }

        public int? assigner_id { get; set; }

        public int? referee_id { get; set; }

        public byte title_id { get; set; }

        public byte campaign_reason_id { get; set; }

        public int web_site_id { get; set; }

        public int? promotion_code_id { get; set; }

        public byte activity_closing_reason_id { get; set; }

        public int? ext_consultant_id { get; set; }

        [StringLength(10)]
        public string salutation { get; set; }

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

        [Required]
        [StringLength(10)]
        public string state_code { get; set; }

        [Required]
        [StringLength(10)]
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
        public string evening_time_call { get; set; }

        [StringLength(20)]
        public string fax { get; set; }

        [StringLength(50)]
        public string email { get; set; }

        public DateTime lead_entry_date { get; set; }

        public int? member_count { get; set; }

        public int? participant_count { get; set; }

        public int? fund_raising_goal { get; set; }

        public DateTime? decision_date { get; set; }

        public bool decision_maker { get; set; }

        public bool committee_meeting_required { get; set; }

        public DateTime? committee_meeting_date { get; set; }

        public DateTime? fund_raiser_start_date { get; set; }

        public bool onemaillist { get; set; }

        public bool faxkit { get; set; }

        public bool emailkit { get; set; }

        [StringLength(1750)]
        public string comments { get; set; }

        public bool kit_to_send { get; set; }

        public bool kit_sent { get; set; }

        public DateTime? kit_sent_date { get; set; }

        public DateTime? lead_assignment_date { get; set; }

        [StringLength(2800)]
        public string interests { get; set; }

        public bool? has_been_contacted { get; set; }

        [StringLength(10)]
        public string day_phone_ext { get; set; }

        [StringLength(10)]
        public string evening_phone_ext { get; set; }

        [StringLength(20)]
        public string other_phone { get; set; }

        [StringLength(50)]
        public string group_web_site { get; set; }

        public int? nb_queries { get; set; }

        public DateTime? submit_date { get; set; }

        [StringLength(255)]
        public string cookie_content { get; set; }

        public DateTime? first_contact_date { get; set; }

        public bool day_phone_is_good { get; set; }

        public bool evening_phone_is_good { get; set; }

        public int? account_number { get; set; }

        public bool valid_email { get; set; }

        [StringLength(50)]
        public string other_closing_activity_reason { get; set; }

        public DateTime? transfered_date { get; set; }

        [StringLength(10)]
        public string matching_code { get; set; }

        public int? phone_number_tracking_id { get; set; }

        public int? customer_status_id { get; set; }

        [StringLength(15)]
        public string vif { get; set; }

        public int address_zone_id { get; set; }

        public int is_postal_address_validated { get; set; }

        public int? client_status_id { get; set; }

        public DateTime? activation_date { get; set; }

        public byte? fundraisers_per_year { get; set; }

        public int? wfc_customer_number { get; set; }

        public int? other_phone_is_good { get; set; }

        public virtual ICollection<client> clients { get; set; }

        public virtual ICollection<Comment> Comments1 { get; set; }

        public virtual consultant consultant { get; set; }

        public virtual consultant consultant1 { get; set; }

        public virtual division division { get; set; }

        public virtual ICollection<Efr_Lead_Activity> Efr_Lead_Activity { get; set; }

        public virtual ICollection<General_Comment> General_Comment { get; set; }

        public virtual hear_about_us hear_about_us { get; set; }

        public virtual ICollection<lead_activity> lead_activity { get; set; }

        public virtual Lead_Channel Lead_Channel { get; set; }

        public virtual Lead_Activity_Closing_Reason Lead_Activity_Closing_Reason { get; set; }

        public virtual Lead_Priority Lead_Priority { get; set; }

        public virtual Lead_Qualification_Type Lead_Qualification_Type { get; set; }

        public virtual Lead_Status Lead_Status { get; set; }

        public virtual ICollection<Lead_Promotion> Lead_Promotion { get; set; }

        public virtual Referee Referee { get; set; }

        public virtual title title { get; set; }

        public virtual ICollection<Lead_Visit> Lead_Visit { get; set; }

        public virtual ICollection<promotional_kit> promotional_kit { get; set; }

        public virtual ICollection<Referee> Referees { get; set; }

        public virtual ICollection<sale_to_add> sale_to_add { get; set; }
    }
}
