namespace GA.BDC.Data.MGP.eFundweb.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Template")]
    public partial class Template
    {
        public Template()
        {
            Images = new HashSet<Image>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Template_ID { get; set; }

        public int Language_ID { get; set; }

        public int Partner_ID { get; set; }

        [Required]
        [StringLength(120)]
        public string Template_Path { get; set; }

        public bool? Is_Default { get; set; }

        public virtual ICollection<Image> Images { get; set; }

        public virtual Language Language { get; set; }
    }
}
