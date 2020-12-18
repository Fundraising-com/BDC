using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
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

        [MvcSiteMapNode(Title = "Search", ParentKey = "Root", Protocol = "http"), Route("search")]
        public ActionResult Search()
        {
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

                var client = new HttpClient();
                var values = new Dictionary<string, string>
                              {
                                 { "Type", "18" },
                                 { "name", name },
                                 { "phone", phone },
                                 { "email", email },
                                 { "members", members },
                                 { "startdate", startdate },
                                 { "group", group },
                                 { "state", state },
                                 { "country", country },
                                 { "imageFileName", renamedFile }
                              };
                
                var content = new FormUrlEncodedContent(values);
                var response = await client.PostAsync(serverNotificationUrl, content);
                var responseString = await response.Content.ReadAsStringAsync();



                HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(serverNotificationUrl);
                webRequest.Method = "POST";
                webRequest.ContentType = "application/json";
                
                using (var streamWriter = new StreamWriter(webRequest.GetRequestStream()))
                {
                    string json = "{ \"Type\":\"" + 18 +
                             "\",\"name\":\"" + name +
                             "\",\"phone\":\"" + phone +
                              "\",\"email\":\"" + email +
                             "\",\"members\":\"" + members +
                              "\",\"startdate\":\"" + startdate +
                             "\",\"group\":\"" + group +
                             "\",\"state\":\"" + state +
                              "\",\"country\":\"" + country +
                             "\",\"imageFileName\":\"" + renamedFile +
                              "\"}";

                    streamWriter.Write(json);
                    streamWriter.Flush();
                    streamWriter.Close();
                }

                var httpResponse = (HttpWebResponse)webRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                }



                ServerXMLHTTP emailProcessing = new ServerXMLHTTP();
                var strJSONToSend2 = "{ \"Type\":\"" + 18 +
                             "\",\"name\":\"" + name +
                             "\",\"phone\":\"" + phone +
                              "\",\"email\":\"" + email +
                             "\",\"members\":\"" + members +
                              "\",\"startdate\":\"" + startdate +
                             "\",\"group\":\"" + group +
                             "\",\"state\":\"" + state +
                              "\",\"country\":\"" + country +
                             "\",\"imageFileName\":\"" + renamedFile +
                              "\"}";

                var postDataBytes2 = Encoding.Default.GetBytes(strJSONToSend2);
                emailProcessing.open("POST", serverNotificationUrl);
                emailProcessing.setRequestHeader("Content-Type", "application/json");
                emailProcessing.setRequestHeader("Content-Length", postDataBytes2.Length.ToString());
                emailProcessing.send(postDataBytes2);

                if (emailProcessing.status == 200)

                {
                    return Redirect("/get-started#success");
                }
                else
                {
                    Logger.Error("easy sports apparel error - " + emailProcessing.responseText);
                    return Redirect("/get-started#error");
                }


                


                //if (response.ReasonPhrase == "OK")
                //{
                //    return Redirect("/get-started#success");
                //}
                //else
                //{
                //    return Redirect("/get-started#error");
                //}

            }
            catch (Exception ex)
            {
                Logger.Error("easy sports apparel error - " + ex);
                return Redirect("/get-started#error");
            }
        }

    }
}