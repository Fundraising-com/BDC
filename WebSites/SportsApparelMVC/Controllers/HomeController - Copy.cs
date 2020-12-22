using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using MSXML2;
using System.Configuration;
using Ninject;
using Ninject.Extensions.Logging;
using System.Text;
using MvcSiteMapProvider;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using log4net;


namespace GA.BDC.Web.SportsApparel.MVC.Controllers
{
    public class HomeController : Controller
    {
        [Inject]
        public ILogger Logger { get; set; }



        public ActionResult Index()
        {
            
            return View();
        }

        [MvcSiteMapNode(Title = "Get Started", ParentKey = "Root", Protocol = "https"), Route("get-started")]

        public ActionResult GetStarted()
        {

            return View();
        }

        public ActionResult ConfirmationPage()
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

        public ActionResult HowItWorks()
        {
            ViewBag.Message = "How It Works";

            return View();
        }

        public class TestModel
        {
            public bool ShowDialog { get; set; }
        }


        [HttpPost]
        public async System.Threading.Tasks.Task<ActionResult> UploadFileAsync(HttpPostedFileBase uploadFile)
        {
            var serverNotificationUrl = (ConfigurationManager.AppSettings["core.webapi.host.notification"]);
            var serverLeadUrl = (ConfigurationManager.AppSettings["core.webapi.host.lead"]);

            string name = Request.Form["gs.Fullname"];
            string phone = Request.Form["gs.Phone"];
            string email = Request.Form["gs.Email"];
            string members = Request.Form["gs.NumMembers"];
            string startdate = Request.Form["gs.StartDate"];
            string group = Request.Form["gs.Group"];
            string state = Request.Form["gs.Address.Region"];
            string country = Request.Form["gs.Address.Country"];
            


            try
            {
                var fileName = Path.GetFileName(uploadFile.FileName);
                var renamedFile = DateTime.Now.ToString("yyyyMMddHHmmss") + "_" + fileName;
                var path = Path.Combine(Server.MapPath("~/UploadedFiles"), renamedFile);
                uploadFile.SaveAs(path);


                var client2 = new HttpClient();
                //ServerXMLHTTP leadProcessing = new ServerXMLHTTP();
                //     var strJSONToSend2 = "{ \"FirstName\":\"" + name +
                //         "\",\"Email\":\"" + email +
                //         "\",\"Phone\":\"" + phone +
                //         "\",\"Comments\":\"" + "Lead from easy sports apparel" +
                //         "\",\"TellMore\":\"" + 2 +
                //          "\",\"OrigProsDte\":\"" + startdate +
                //         "\",\"RequestType\":\"" + 1 +
                //         "\",\"ChannelCode\":\"" + "INT" +
                //         "\",\"Group\":\"" + group +
                //         "\",\"NumberOfMembers\":\"" + members +
                //         "\",\"Address\":{ \"Address1\":\"" + "NULL" +
                //                         "\",\"City\":\"" + "NULL" +
                //                         "\",\"Region\":{ \"Code\":\"" + state +
                //                         "\",\"Name\":null,\"CountryCode\":null},\"Country\":{ \"Code\":\"" + country.ToString() +
                //                         "\",\"Name\":\"" + "null" +
                //                         "\"},\"PostCode\":\"" + "NULL" +
                //                     "\"} }";

                //     var postDataBytes2 = Encoding.Default.GetBytes(strJSONToSend2);
                //     leadProcessing.open("POST", serverLeadUrl);
                //     leadProcessing.setRequestHeader("Content-Type", "application/json");
                //     leadProcessing.setRequestHeader("Content-Length", postDataBytes2.Length.ToString());
                //     leadProcessing.send(postDataBytes2);
                //     //LOGS
                //     Logger.Error(leadProcessing.status + " - " + leadProcessing.statusText + " - " + leadProcessing.responseText);

                //     var response = JObject.Parse(leadProcessing.responseText);
                //     var leadId = response["Id"].Value<string>();

                //send email with sports lead info and image
                

                    //ServerXMLHTTP emailProcessing = new ServerXMLHTTP();
                    //var strJSONToSend = "{ \"Type\":\"" + 18 +
                    //     "\",\"name\":\"" + name +
                    //    "\",\"ExternalId\":\"" + 1210906 +
                    //    "\",\"ExtraData\":\"" + renamedFile +
                    //    "\"}";

                var client = new HttpClient();

               var values = new Dictionary<string, string>
                              {
                                 { "Type", "18" },
                                 { "name", name },
                                 { "ExternalId", "1210906" },
                                 { "ExtraData", renamedFile }
                              };
                
                  var content = new FormUrlEncodedContent(values);
                var response = await client.PostAsync(serverNotificationUrl, content);
                var responseString = await response.Content.ReadAsStringAsync();


                TempData["message"] = "test";
                return Redirect("/get-started#success");


            }
            catch (Exception ex)
            {
                TempData["message"] = ex;
                Logger.Error("easy sports apparel error - " + ex);
                return Redirect("/get-started#error");
            }
        }

    }
}