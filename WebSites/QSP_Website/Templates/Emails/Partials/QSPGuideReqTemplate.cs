using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace QSP_Website.Templates.Emails
{
    public partial class QSPGuideReqTemplate
    {
        public string Name { get; set; }
        public string Subject { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string GroupName { get; set; }



        public QSPGuideReqTemplate(string subject, string name, string group, string email, string phone)
        {
            Subject = subject;
            Name = name;
            Phone = phone;
            GroupName = group;
            Email = email;


        }




    }


}