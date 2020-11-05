using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GA.BDC.Shared.Entities;

// ReSharper disable once CheckNamespace
namespace GA.BDC.WebApi.Fundraising.Core.Templates.Emails
{
   public partial class OrderTakerEmailTemplate
    {
      public string Subject { get; set; }
      //public OrderInfo { get; set; }
 public string name { get; set; }

        public string gname { get; set; }

        public string phone { get; set; }
        public string email { get; set; }

        public string members { get; set; }

        public string address { get; set; }
            public string city { get; set; }
            public string state { get; set; }
            public string zipcode { get; set; }
            public string pname { get; set; }
      

    //public OrderTakerEmailTemplate(string subject, OrderInfo info)
    //  {
    //     Subject = subject;
    //     //Product = product;
         
    //  }

        public OrderTakerEmailTemplate(string subject, Notification orderInfo)
        {
            Subject = subject;
            name = orderInfo.Name;
            gname = orderInfo.GroupName;
            phone = orderInfo.Phone;
            email = orderInfo.Email;
            members = orderInfo.Members;
            address = orderInfo.Address;
            city = orderInfo.City;
            state = orderInfo.State;
            zipcode = orderInfo.ZipCode;
            pname = orderInfo.Product;
        }
    }
}