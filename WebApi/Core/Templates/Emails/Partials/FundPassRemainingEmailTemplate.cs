using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GA.BDC.Shared.Entities;

namespace GA.BDC.WebApi.Fundraising.Core.Templates.Emails
{
    public partial class FundPassRemainingEmailTemplate
    {
        public string Subject2 { get; set; }
        public int CodesLefts { get; set; }
        //public Lead Lead { get; private set; }

       
       

        public FundPassRemainingEmailTemplate(string subject, int codesleft) 
        {
            Subject2 = subject;
            CodesLefts = codesleft;



        }
    }

}