using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Xml;
using GA.BDC.Console.TaskExecutor.Properties;
using GA.BDC.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SWCorporate.SystemEx.Console;

namespace GA.BDC.Console.TaskExecutor.Tasks
{
   /// <summary>
   /// Sends the Leads to Publi Trac
   /// </summary>
   internal sealed class SendPubliTracLead : ITask<TaskFlags>
   {
      private static IList<Exception> _exceptions;

      void ITask<TaskFlags>.Execute(TaskFlags taskFlags, params string[] taskArgs)
      {
         _exceptions = new List<Exception>();
         try
         {
            IList<es_get_leads_for_publitracResult> leads;
            using (var dataContext = new eFundraisingProdDataContext(Settings.Default.eFundraisingProdConnectionString))
            {
               leads = dataContext.es_get_leads_for_publitrac().ToList();
            }
            foreach (var lead in leads)
            {
               try
               {
                  Send(lead);
               }
               catch (Exception exception)
               {
                  _exceptions.Add(exception);
               }
            }
         }
         catch (Exception exception)
         {
            _exceptions.Add(exception);
         }
         if (_exceptions.Any())
         {
            throw new AggregateException(_exceptions);
         }
      }

      private static void Send(es_get_leads_for_publitracResult lead)
      {

            if (!string.IsNullOrEmpty(lead.email))
            {
           

            try
            {
               
                string leadKittype;
                switch (lead.kit_type)
                {
                    case 42:
                        leadKittype = "Mail";
                        break;
                    case 43:
                        leadKittype = "Email";
                        break;
                    default:
                        leadKittype = "Mail and email";
                        break;
                }
                    if (string.IsNullOrEmpty(lead.group_web_site))
                    {
                    lead.group_web_site = "No Website";
                }

                //Construct the request
                var strJSONToSend2 = "{ \"leadInfo\":" +
                    "{ \"crmLeadId\":\"" + lead.lead_id +
                        "\",\"firstName\":\"" + lead.first_name +
                        "\",\"lastName\":\"" + lead.last_name +
                        "\",\"phone\":\"" + lead.day_phone + " ext:" + lead.day_phone_ext +
                        "\",\"Email\":\"" + lead.email +
                        "\",\"mailingAddress\":\"" + lead.street_address +
                        "\",\"leadSource\":\"" + lead.promotion_id +
                         "\",\"originalSource\":\"API Fundraising.com" +
                        "\",\"originalSourceType\":\"Website" +
                         "\",\"postalCode\":\"" + lead.zip_code +
                        "\",\"country\":\"" + lead.country_code +
                         "\",\"city\":\"" + lead.city +
                        "\",\"state\":\"" + lead.state_code +
                         "\",\"website\":\"" + lead.group_web_site + 
                         "\"}," +
                         "\"leadCustomField\":{ \"number_of_participants\":\"" + lead.participant_count +
                                    "\",\"send_newsletter\":\"" + (lead.onemaillist ? 1 : 0) +
                                    "\",\"kittype_to_send\":\"" + leadKittype +
                                    "\"} }";


                //Get publitract token
                var token = string.Empty;
                var apiUrl = "https://app.publitrac.com/v2/auth/getToken";
                var request = (HttpWebRequest)WebRequest.Create(apiUrl);
                request.Headers.Add("Authorization", "74533284-27ac-11e3-9105-22000a98b9e3");
                request.Method = "GET";
                request.ContentType = "application/json; charset=utf-8";
                var response = (HttpWebResponse)request.GetResponse();
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var responseStream = response.GetResponseStream();
                    StreamReader reader = new StreamReader(responseStream);
                    string responseFromServer = reader.ReadToEnd();
                    JObject jObject = JObject.Parse(responseFromServer);
                    token  = (string)jObject["token"];
                }
                //end publitract token
                //Post new lead
                var postApiUrl = "https://app.publitrac.com/v2/lead/delayedSync";
                var req = (HttpWebRequest)WebRequest.Create(postApiUrl);
                req.ContentType = "application/json";
                req.Method = "POST";
                req.Headers.Add("Token", token);
                using (var streamWriter = new StreamWriter(req.GetRequestStream()))
                {
                    streamWriter.Write(strJSONToSend2);
                    streamWriter.Flush();
                    streamWriter.Close();
                }
                var httpResponse = (HttpWebResponse)req.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                }
                //END Post new lead

                using (var dataContext = new eFundraisingProdDataContext(Settings.Default.eFundraisingProdConnectionString))
                {
                    var dblead = dataContext.leads.First(p => p.lead_id == lead.lead_id);
                    dblead.sent_to_publitrac = true;
                    dataContext.SubmitChanges();
                }
            }
            catch (Exception exception)
            {
                _exceptions.Add(new Exception("Exception while trying to send Lead [" + lead.lead_id + "] to Publictrac", exception));
            }
        }
            else
            {
                //Do nothing
                _exceptions.Add(new Exception("No email in lead [" + lead.lead_id + "] - Do not send to  publitract"));
            }


        }

        private static string ConvertStringToXmlPostFormat(string content)
      {
         var doc = new System.Xml.XmlDocument();
         var element = doc.CreateElement("tag");
         element.InnerText = content;
         content = element.OuterXml.Substring(5); // remove the <tag> at the begining
         content = content.Substring(0, content.Length - 6); // remove the </tag> at the end
         content = System.Web.HttpUtility.HtmlEncode(content);
         content = content.Replace("&", "%26");
         return content;
      }
   }
}
