using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GA.BDC.Data.MGP.esubs_global_v2.Models
{
    public partial class default_personalization
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public int PartnerId { get; set; }
        [Required]
        public int EventTypeId { get; set; }
        [Required]
        public int ParticipantTypeId { get; set; }
        [StringLength(100)]
        public string HeaderTitle1 { get; set; }
        [StringLength(100)]
        public string HeaderTitle2 { get; set; }
        public string Body { get; set; }
        public double Goal { get; set; }
    }
}
