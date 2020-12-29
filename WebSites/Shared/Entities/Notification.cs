using System;

namespace GA.BDC.Shared.Entities
{
   public class Notification
   {
      public int Id { get; set; }
      public int ExternalId { get; set; }
      public string Email { get; set; }
      public string ExtraData { get; set; }
      public NotificationType Type { get; set; }
      public DateTime Created { get; set; }

        //Addtion for Easy sports apparel website
        public string Phone { get; set; }
        public string Name { get; set; }
        public string ImageFileName { get; set; }
        public string Members { get; set; }
        public string Startdate { get; set; }
        public string Group { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public string GroupName { get; set; }
        public string Address { get; set; }
        public string Product { get; set; }


    }
}
