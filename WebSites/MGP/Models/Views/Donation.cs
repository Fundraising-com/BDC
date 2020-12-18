using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using GA.BDC.Web.MGP.Helpers.Validations.Attributes;

namespace GA.BDC.Web.MGP.Models.Views
{
    public class Donation
    {
        public int EventId { get; set; }

        public int ParticipantId { get; set; }

        public decimal DonationAmount { get; set; }

        public int RecurringType { get; set; }

        public int RecurringState { get; set; }

        [Required, MaxLength(50), Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required, MaxLength(50), Display(Name = "Last Name")]
        public string LastName { get; set; }

        public bool HideName { get; set; }

        [MaxLength(100)]
        public string Comment { get; set; }

        [Required, MaxLength(320), EmailAddress]
        public string Email { get; set; }

        [Required, MaxLength(50)]
        public string Address1 { get; set; }

        [MaxLength(50)]
        public string Address2 { get; set; }

        [Required, MaxLength(50)]
        public string City { get; set; }

        [Required, MaxLength(50)]
        public string ZipCode { get; set; }

        [Required, MaxLength(7), ExcludeValue("us-ms", ErrorMessage = "Due to the Non-Profit/Charities Act of 2009 passed by the state of Mississippi, we regret that we cannot extend our service to you at this time.")]
        public string State { get; set; }

        public string Country { get; set; }

        public int CardType { get; set; }

        public string CardTypeName { get; set; }

        [Required, MaxLength(100)]
        public string CardName { get; set; }

        [Required, MaxLength(100)]
        public string CardNumber { get; set; }

        public int CardExpiryMonth { get; set; }

        public int CardExpiryYear { get; set; }

        public string MemberNameToSupport { get; set; }

        public string Detail1 { get; set; }

        public string Detail2 { get; set; }

        public string ConfirmationNumber { get; set; }

        public string LastCreditCardDigits { get; set; }        

        public string CreditCardDisplay
        {
            get
            {
                if (CardType == 5) //MASTERCARD
                    return string.Concat("************", LastCreditCardDigits);
                else if (CardType == 6) //VISA
                    return string.Concat("************", LastCreditCardDigits);
                else if (CardType == 7) //DISCOVER
                    return string.Concat("************", LastCreditCardDigits);
                else if (CardType == 8) //AMEX
                    return string.Concat("***********", LastCreditCardDigits);
                else
                    return string.Concat("************", LastCreditCardDigits);
            }
        }
    }
}