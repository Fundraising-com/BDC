using System.Collections.Generic;

namespace GA.BDC.Shared.Entities
{
   public class Client
   {
      public Client()
      {
         Addresses = new List<ClientAddress>();
         Activities = new List<ClientActivity>();
      }

      /// <summary>
      /// Id
      /// </summary>
      public int Id { get; set; }
      /// <summary>
      /// Sequience Code
      /// </summary>
      public string SequenceCode { get; set; }
      /// <summary>
      /// Organization Class Code
      /// </summary>
      public string OrganizationClassCode { get; set; }
      /// <summary>
      /// Group Type Id
      /// </summary>
      public int GroupTypeId { get; set; }
      /// <summary>
      /// Channel Code
      /// </summary>
      public string ChannelCode { get; set; }
      /// <summary>
      /// Promotion Id
      /// </summary>
      public int PromotionId { get; set; }
      /// <summary>
      /// Lead Id
      /// </summary>
      public int LeadId { get; set; }
      /// <summary>
      /// Division Id
      /// </summary>
      public int DivisionId { get; set; }
      /// <summary>
      /// Consultant Id
      /// </summary>
      public int ConsultantId { get; set; }
      /// <summary>
      /// Title Id
      /// </summary>
      public int TitleId { get; set; }
      /// <summary>
      /// Salutation
      /// </summary>
      public string Salutation { get; set; }
      /// <summary>
      /// First Name
      /// </summary>
      public string FirstName { get; set; }
      /// <summary>
      /// Last Name
      /// </summary>
      public string LastName { get; set; }
      /// <summary>
      /// Title
      /// </summary>
      public string Title { get; set; }
      /// <summary>
      /// Organization
      /// </summary>
      public string Organization { get; set; }
      /// <summary>
      /// Phone
      /// </summary>
      public string Phone { get; set; }
      /// <summary>
      /// Email
      /// </summary>
      public string Email { get; set; }
      public string Email2 { get; set; }
      /// <summary>
      /// Comments
      /// </summary>
      public string Comments { get; set; }
      /// <summary>
      /// Addresses
      /// </summary>
      public IList<ClientAddress> Addresses { get; set; }
      /// <summary>
      /// Activities
      /// </summary>
      public IList<ClientActivity> Activities { get; set; }
      /// <summary>
      /// Lead
      /// </summary>
      public Lead Lead { get; set; }
   }

}
