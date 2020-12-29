using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GA.BDC.Data.Fundraising.EFundStore.Tables
{
    [Table("AspNetUserProfiles")]
    public partial class user_profile
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(200)]
        public string FirstName { get; set; }
        [MaxLength(200)]
        public string LastName { get; set; }
        [MaxLength(200)]
        public string GroupName { get; set; }
        [MaxLength(200)]
        public string BillingFirstName { get; set; }
        [MaxLength(200)]
        public string BillingLastName { get; set; }
        [MaxLength(30)]
        [Phone]
        public string BillingPhone { get; set; }
        [MaxLength(500)]
        public string BillingAddress { get; set; }
        [MaxLength(200)]
        public string BillingCity { get; set; }
        [MaxLength(5)]
        public string BillingState { get; set; }
        [MaxLength(10)]
        public string BillingZIP { get; set; }

        public int? BillingAddressType { get; set; }
    }
}
