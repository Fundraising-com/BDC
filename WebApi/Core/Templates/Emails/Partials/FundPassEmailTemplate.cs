using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GA.BDC.Shared.Entities;

namespace GA.BDC.WebApi.Fundraising.Core.Templates.Emails
{
    public partial class FundPassEmailTemplate
    {
        public string Subject { get; set; }

        public string Firstname { get; set; }
        public string FundCode { get; set; }
        //public Lead Lead { get; private set; }

       
       

        public FundPassEmailTemplate(string subject, string firstname, string fundcode) 
        {
            Subject = subject;
            FundCode = fundcode;
            Firstname = firstname;



            #region Review Empty Strings
            //Lead.LastName = Lead.LastName ?? string.Empty;
            #endregion

        }
    }

}