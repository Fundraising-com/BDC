namespace GA.BDC.Data.Fundraising.EFundraisingProd.Tables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class language
    {
        public language()
        {
            best_time_call_desc = new HashSet<best_time_call_desc>();
            campaign_reason_desc = new HashSet<campaign_reason_desc>();
            country_names = new HashSet<country_names>();
            cultures = new HashSet<culture1>();
            group_type_desc = new HashSet<group_type_desc>();
            hear_about_us_desc = new HashSet<hear_about_us_desc>();
            language_desc = new HashSet<language_desc>();
            language_desc1 = new HashSet<language_desc>();
            organization_type_desc = new HashSet<organization_type_desc>();
            package_desc = new HashSet<package_desc>();
            partner_contacts = new HashSet<partner_contacts>();
            product_desc = new HashSet<product_desc>();
            salutation_desc = new HashSet<salutation_desc>();
            title_desc = new HashSet<title_desc>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public byte language_id { get; set; }

        [Required]
        [StringLength(50)]
        public string language_name { get; set; }

        [Required]
        [StringLength(3)]
        public string long_language_code { get; set; }

        [StringLength(2)]
        public string short_language_code { get; set; }

        public virtual ICollection<best_time_call_desc> best_time_call_desc { get; set; }

        public virtual ICollection<campaign_reason_desc> campaign_reason_desc { get; set; }

        public virtual ICollection<country_names> country_names { get; set; }

        public virtual ICollection<culture1> cultures { get; set; }

        public virtual ICollection<group_type_desc> group_type_desc { get; set; }

        public virtual ICollection<hear_about_us_desc> hear_about_us_desc { get; set; }

        public virtual ICollection<language_desc> language_desc { get; set; }

        public virtual ICollection<language_desc> language_desc1 { get; set; }

        public virtual ICollection<organization_type_desc> organization_type_desc { get; set; }

        public virtual ICollection<package_desc> package_desc { get; set; }

        public virtual ICollection<partner_contacts> partner_contacts { get; set; }

        public virtual ICollection<product_desc> product_desc { get; set; }

        public virtual ICollection<salutation_desc> salutation_desc { get; set; }

        public virtual ICollection<title_desc> title_desc { get; set; }
    }
}
