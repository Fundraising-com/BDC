using System.ComponentModel.DataAnnotations;
using GA.BDC.Web.MGP.Helpers.Validations.Attributes;

namespace GA.BDC.Web.MGP.Models.Views
{
    public class NewCampaign
    {
        [MaxLength(100), Phone]
        public string Phone { get; set; }

        [Required, MaxLength(6), ExcludeValue("us-ms", ErrorMessage = "Due to the Non-Profit/Charities Act of 2009 passed by the state of Mississippi, we regret that we cannot extend our service to you at this time.")]
        public string State { get; set; }

        [Required, MaxLength(200), Display(Name = "Campaign Name")]
        public string CampaignName { get; set; }

        [Required, Display(Name = "Group Type")]
        public int GroupType { get; set; }
        
    }
}