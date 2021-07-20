using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MSXML2;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Configuration;
using RestSharp.Portable;
using RestSharp.Portable.HttpClient;
using QSP_Website.Templates.Emails;

namespace QSP_Website.Controllers
{
    public class HomeController : Controller
    {

        public class CustomerInfo
        {
            public string FullName { get; set; }

            public string GroupName { get; set; }

            public string Email { get; set; }

            public string Phone { get; set; }

        }


        [HttpPost]
        public ActionResult Index(CustomerInfo customer)
        {
            string name = customer.FullName;
            string group = customer.GroupName;
            string email = customer.Email;
            string phone = customer.Phone;

            const string subject = "QSP Free Guide Request";
            var guideReqTemplate = new QSPGuideReqTemplate(subject, name, group, email, phone);


            var clientBlue = new RestClient("https://api.sendinblue.com/v3/smtp/email");
            var request = new RestRequest(Method.POST);
            request.AddHeader("accept", "application/json");
            request.AddHeader("content-type", "application/json");
            request.AddHeader("api-key", "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");
            var body = new { sender = new { name = "QSP.com", email = "online@fundraising.com" }, to = new[] { new { email = "Marc.Alcindor@fundraising.com", name = "Marc" }, new { email = "sadday.zivec@fundraising.com", name = "Saddaycita" }, new { email = "jason.farrell@fundraising.com", name = "Jay" }, new { email = "support@fundraising.com", name = "Sales Support" } }, replyTo = new { email = "online@fundraising.com", name = "Fundraising.com" }, htmlContent = guideReqTemplate.TransformText(), subject = "QSP Free Guide Request" };
            request.AddBody(body);
            var response = clientBlue.Execute(request);


            return View();
        }



        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}