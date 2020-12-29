using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GA.BDC.Data.MGP.esubs_global_v2.Models
{
    public partial class offensive_content
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public int Type { get; set; }        
        [StringLength(100)]
        public string Word { get; set; }
    }
}
