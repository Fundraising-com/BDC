using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GA.BDC.Web.MGP.Helpers.Extensions;

namespace GA.BDC.Web.MGP.Models.Views
{
    public class ParticipantPage
    {
        /// <summary>
        /// Participant Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Personalization Id
        /// </summary>
        public int PersonalizationId { get; set; }        
        /// <summary>
        /// First Name
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// Last Name
        /// </summary>
        public string LastName { get; set; }
        /// <summary>
        /// Participant Name
        /// </summary>
        public string Name
        {
            get
            {
                if (LastName.IsNotEmpty())
                    return FirstName.Trim() + " " + LastName.Trim();
                else
                    return FirstName.Trim();
            }
        }
        /// <summary>
        /// Participant message
        /// </summary>
        public string Message { get; set; }

        public string GroupMessage { get; set; }
    }
}