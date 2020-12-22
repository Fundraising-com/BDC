using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace GA.BDC.Web.Fundraising.MVC.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public virtual ApplicationUserProfile Profile { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);

            // Add custom user claims here
            return userIdentity;
        }
    }
    [Table("AspNetUserProfiles")]
    public class ApplicationUserProfile
    {
        [Key]
        public int Id { get; set; }
        [DisplayName("First Name")]
        [MaxLength(200)]
        public string FirstName { get; set; }
        [DisplayName("Last Name")]
        [MaxLength(200)]
        public string LastName { get; set; }
        [DisplayName("Group Name")]
        [MaxLength(200)]
        public string GroupName { get; set; }
        [DisplayName("Phone")]
        [Phone, NotMapped]
        public string Phone { get; set; }
        [DisplayName("Email")]
        [EmailAddress, NotMapped]
        public string Email { get; set; }
        // Billing Information
        [DisplayName("First Name")]
        [MaxLength(200)]
        public string BillingFirstName { get; set; }
        [DisplayName("Last Name")]
        [MaxLength(200)]
        public string BillingLastName { get; set; }
        [DisplayName("Phone")]
        [Phone, MaxLength(30)]
        public string BillingPhone { get; set; }
        [DisplayName("Address")]
        [MaxLength(500)]
        public string BillingAddress { get; set; }
        [DisplayName("City")]
        [MaxLength(200)]
        public string BillingCity { get; set; }
        [DisplayName("State")]
        [MaxLength(5)]
        public string BillingState { get; set; }
        [DisplayName("ZIP")]
        [MaxLength(10)]
        public string BillingZIP { get; set; }
        [Required]
        [DisplayName("Address Type")]
        public int BillingAddressType { get; set; }
        
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("EFundStore", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public System.Data.Entity.DbSet<GA.BDC.Shared.Entities.Notification> Notifications { get; set; }
    }
}