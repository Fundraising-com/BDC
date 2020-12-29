namespace GA.BDC.Data.Fundraising.EFundraisingProd.Tables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("cultures")]
    public partial class culture1
    {
        public culture1()
        {
            scratch_book = new HashSet<scratch_book>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public byte culture_id { get; set; }

        public byte? language_id { get; set; }

        [StringLength(2)]
        public string country_code { get; set; }

        [Required]
        [StringLength(10)]
        public string culture_name { get; set; }

        [Required]
        [StringLength(50)]
        public string display_name { get; set; }

        [Required]
        [StringLength(6)]
        public string culture_code { get; set; }

        [StringLength(3)]
        public string iso_code { get; set; }

        public virtual country country { get; set; }

        public virtual language language { get; set; }

        public virtual ICollection<scratch_book> scratch_book { get; set; }
    }
}
