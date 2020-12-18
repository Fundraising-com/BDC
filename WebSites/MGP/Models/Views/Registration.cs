using System;
using System.ComponentModel.DataAnnotations;
using GA.BDC.Web.MGP.Helpers.Validations.Attributes;

namespace GA.BDC.Web.MGP.Models.Views
{
    [Serializable]
    public class Registration
    {
        [Required, MaxLength(100), Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required, MaxLength(100), Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required, MaxLength(100), EmailAddress]
        public string Email { get; set; }

        [MaxLength(100), Phone]
        public string Phone { get; set; }

        [MaxLength(6), ExcludeValue("us-ms", ErrorMessage = "Due to the Non-Profit/Charities Act of 2009 passed by the state of Mississippi, we regret that we cannot extend our service to you at this time.")]
        public string State { get; set; }

        [Required, MaxLength(100), Display(Name = "Password"), MinLength(5)]
        public string Password1 { get; set; }

        [MaxLength(200), Display(Name = "Campaign Name")]
        public string CampaignName { get; set; }

        [Display(Name = "Group Type")]
        public int GroupType { get; set; }

        [Required, Display(Name = "Terms of Service"), Range(1, 1, ErrorMessage = "You must accept the Terms of Service.")]
        public bool Terms { get; set; }

        public bool Newsletter { get; set; }

        public int GroupId { get; set; }
    }

    public class RegistrationParticipant
    {
        [Required, MaxLength(100), Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required, MaxLength(100), Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required, MaxLength(100), EmailAddress]
        public string Email { get; set; }

        [Required, Display(Name = "Terms of Service"), Range(1, 1, ErrorMessage = "You must accept the Terms of Service.")]
        public bool Terms { get; set; }

        [Required, MaxLength(100), Display(Name = "Password"), MinLength(5)]
        public string Password1 { get; set; }

        [Required]
        public int EventId { get; set; }
    }
    public class JoinParticipant
    {
        [Required, MaxLength(100), EmailAddress]
        public string Username { get; set; }

        [Required, Display(Name = "Terms of Service"), Range(1, 1, ErrorMessage = "You must accept the Terms of Service.")]
        public bool Terms { get; set; }

        [Required, MaxLength(100), Display(Name = "Password"), MinLength(5)]
        public string Password { get; set; }

        [Required]
        public int EventId { get; set; }

        public bool RememberMe { get; set; }
    }
}