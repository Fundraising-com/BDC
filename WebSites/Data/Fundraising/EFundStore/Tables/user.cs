using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GA.BDC.Data.Fundraising.EFundStore.Tables
{
    [Table("AspNetUsers")]
    public partial class user
    {
        [Key]
        [MaxLength(128)]
        public string Id { get; set; }
        [EmailAddress]
        [MaxLength(256)]
        public string Email { get; set; }

        public int profile_id { get; set; }
    }
}
