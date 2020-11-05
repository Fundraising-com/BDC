using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GA.BDC.Shared.Entities;

namespace GA.BDC.WebApi.Fundraising.Core.Templates.Emails
{
    public partial class EasyApparelEmailTemplate
    {
        public string Subject { get; set; }
        //public Lead Lead { get; private set; }
        public string ImageFileName { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Members { get; set; }
        public string Startdate { get; set; }
        public string Group { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
       

        public EasyApparelEmailTemplate(string subject, string name, string phone, string email, string members, string startdate, string group, string state, string country, string imageFileName) 
        {
            Subject = subject;
            //Lead = lead;
            Name = name;
            Phone = phone;
            Email = email;
            Members = members;
            Startdate = startdate;
            Group = group;
            State = state;
            Country = country;
            ImageFileName = imageFileName;



            #region Review Empty Strings
            //Lead.LastName = Lead.LastName ?? string.Empty;
            #endregion

        }
    }

}