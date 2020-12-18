using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using GA.BDC.Web.MGP.Helpers.Validations.Attributes;

namespace GA.BDC.Web.MGP.Models.Views
{
    public class Information
    {
        [Required, MaxLength(100), Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required, MaxLength(100), Display(Name = "Last Name")]
        public string LastName { get; set; }

        public bool IgnoreAddressHygiene { get; set; }

        [Required, MaxLength(100)]
        public string City { get; set; }

        [Required, MaxLength(100)]
        public string Address { get; set; }

        [Required, RegularExpression(@"^\d{5}(-\d{4})?$", ErrorMessage = "Zipcode must be in the proper format: '12345' or '12345-6789'")]
        public string ZipCode { get; set; }

        [MaxLength(100)]
        public string PayTo { get; set; }

        [Required, MaxLength(100), EmailAddress]
        public string Email { get; set; }

        [MaxLength(100), Phone]
        public string Phone { get; set; }

        [Required, MaxLength(6), ExcludeValue("us-ms", ErrorMessage = "Due to the Non-Profit/Charities Act of 2009 passed by the state of Mississippi, we regret that we cannot extend our service to you at this time.")]
        public string State { get; set; }

        public bool ChangePassword { get; set; }

        [MaxLength(100), Display(Name = "Current Password"), MinLength(5)]
        public string CurrentPassword { get; set; }

        [MaxLength(100), Display(Name = "New Password"), MinLength(5)]
        public string NewPassword1 { get; set; }

        [MaxLength(100), Compare("NewPassword1", ErrorMessage = "The passwords do not match."), Display(Name = "Repeat New Password"), MinLength(5)]
        public string NewPassword2 { get; set; }

        [Required, MaxLength(200), Display(Name = "Campaign Name")]
        public string CampaignName { get; set; }

        [Required, Display(Name = "Payment Type")]
        public int PaymentType { get; set; }

        public bool Newsletter { get; set; }

        public int ParticipantId { get; set; }
    }
}