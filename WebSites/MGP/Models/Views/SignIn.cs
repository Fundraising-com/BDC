using System.ComponentModel.DataAnnotations;

namespace GA.BDC.Web.MGP.Models.Views
{
    public class SignIn
    {
        [Required, EmailAddress, MaxLength(100), Display(Name = "Email Address")]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}