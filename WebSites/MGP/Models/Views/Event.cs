using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GA.BDC.Web.MGP.Models.Views
{
    public class Event
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Create { get; set; }
        public bool IsActive { get; set; }
        public string UserType { get; set; }
        public int ParticipantId { get; set; }
    }
}