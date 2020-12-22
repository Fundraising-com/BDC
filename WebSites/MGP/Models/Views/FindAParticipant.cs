using System.ComponentModel.DataAnnotations;

namespace GA.BDC.Web.MGP.Models.Views
{
    public class FindAParticipant
    {
        [Required, MaxLength(100)]
        public string Name { get; set; }
        [Required]
        public int EventId { get; set; }
    }
}