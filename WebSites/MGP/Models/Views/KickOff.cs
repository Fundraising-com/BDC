using System.ComponentModel.DataAnnotations;
using System.Text;
using GA.BDC.Data.MGP.esubs_global_v2.Models;

namespace GA.BDC.Web.MGP.Models.Views
{
    public class EmailTemplateBase
    {
        [Required]
        public int TemplateId { get; set; }
        [Required]
        public string From { get; set; }
        [Required]
        public string Subject { get; set; }
        
        public string Message { get; set; }
        public string TextMessage { get; set; }
        [Required]
        public int CreationChannelManual { get; set; }
        [Required]
        public int CreationChannelImport { get; set; }
        [Required]
        public int BusinessRuleId { get; set; }
        public int MessageType { get; set; }

        public string CustomMessage { get; set; }
    }

    public class KickOff : EmailTemplateBase
    {        
        [Required]
        public int ReminderRecurrency { get; set; }
        [Required(ErrorMessage = "You must specified at least one valid Recipient.")]
        public Recipient[] Recipients { get; set; }
        public Reminder[] Reminders { get; set; }        
        [Required]
        public bool IsDraft { get; set; }

        public int ParticipationChannelId { get; set; }
    }

    public class Reminder : EmailTemplateBase
    {
        public bool DeleteReminder { get; set; }
    }

    public class Recipient
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Required, EmailAddress]
        public string Email { get; set; }
        public bool IsManual { get; set; }

        public override string ToString()
        {
            var stringBuilder = new StringBuilder();
            if (!string.IsNullOrWhiteSpace(FirstName))
            {
                stringBuilder.Append(FirstName + " ");
            }
            if (!string.IsNullOrWhiteSpace(LastName))
            {
                stringBuilder.Append(LastName + " ");
            }
            stringBuilder.Append(Email);
            return stringBuilder.ToString();
        }
    }
}