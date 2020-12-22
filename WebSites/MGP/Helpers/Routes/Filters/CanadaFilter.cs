using System;
using System.Configuration;
using System.Web.Mvc;
using System.Linq;
using GA.BDC.Web.MGP.Helpers.Extensions;
using GA.BDC.Data.MGP.esubs_global_v2.Models;
using GA.BDC.Web.MGP.Helpers.Exceptions;

namespace GA.BDC.Web.MGP.Helpers.Routes.Filters
{
   /// <summary>
   /// Handles the redirect of all ESubs Canada campaigns to http://efundraisingonline.ca
   /// </summary>
   public class CanadaFilter : IActionFilter
   {
      #region IActionFilter Members

      public void OnActionExecuted(ActionExecutedContext filterContext)
      {
      }

      public void OnActionExecuting(ActionExecutingContext filterContext)
      {
         // Redirect to ESubs Canada if event or participant id is Canadian
         if (filterContext.Controller.ValueProvider.GetValue(Constants.QUERY_PARAMETER_EVENT_ID) == null &&
             filterContext.Controller.ValueProvider.GetValue(Constants.QUERY_PARAMETER_PARTICIPANT_ID) == null)
            return;

         using (var dataProvider = new DataProvider())
         {
            string accessedPage = string.Empty, destinationFilePath = string.Empty;
            var request = filterContext.RequestContext.HttpContext.Request;
            var response = filterContext.RequestContext.HttpContext.Response;

            // Sanitize data
            var rawEventId = filterContext.Controller.ValueProvider.GetValue(Constants.QUERY_PARAMETER_EVENT_ID) != null
                             ? filterContext.Controller.ValueProvider.GetValue(Constants.QUERY_PARAMETER_EVENT_ID).AttemptedValue
                             : "0";
            var rawParticipantId = filterContext.Controller.ValueProvider.GetValue(Constants.QUERY_PARAMETER_PARTICIPANT_ID) != null
                                   ? filterContext.Controller.ValueProvider.GetValue(Constants.QUERY_PARAMETER_PARTICIPANT_ID).AttemptedValue
                                   : "0";
            if (rawEventId.Contains("#"))
               rawEventId = rawEventId.Substring(0, rawEventId.IndexOf("#"));
            if (rawParticipantId.Contains("#"))
               rawParticipantId = rawParticipantId.Substring(0, rawParticipantId.IndexOf("#"));
            if (rawEventId.Contains("'\""))
               rawEventId = rawEventId.Substring(0, rawEventId.IndexOf("'\""));
            if (rawParticipantId.Contains("'\""))
               rawParticipantId = rawParticipantId.Substring(0, rawParticipantId.IndexOf("'\""));
            if (rawEventId.Contains("?"))
               rawEventId = rawEventId.Substring(0, rawEventId.IndexOf("?"));
            if (rawParticipantId.Contains("?"))
               rawParticipantId = rawParticipantId.Substring(0, rawParticipantId.IndexOf("?"));

            _event @event = null;
            int eventId = 0;
            try
            {
               eventId = Convert.ToInt32(rawEventId);
            }
            catch
            {
               response.RedirectToRoute("Index", "Home");
               return;
            }

            if (eventId > 0)
            {
               @event = dataProvider.events.SingleOrDefault(e => e.event_id == eventId);
               if (@event != null)
               {
                  if (@event.culture_code.EndsWith("CA"))
                  {
                     if (destinationFilePath.IsEmpty())
                        destinationFilePath = "GroupPage.aspx";
                  }
                  else
                  {
                     return;
                  }
               }
            }
            int participantId = 0;
            try
            {
               participantId = Convert.ToInt32(rawParticipantId);
            }
            catch
            {
               response.RedirectToRoute("Index", "Home");
               return;
            }
            
            if (participantId > 0)
            {
               @event = (from ep in dataProvider.event_participation
                         from e in dataProvider.events
                         where ep.event_participation_id == participantId
                            && ep.event_id == e.event_id
                         select e).FirstOrDefault();
               if (@event != null)
               {
                  if (@event.culture_code.EndsWith("CA"))
                  {
                     if (destinationFilePath.IsEmpty())
                        destinationFilePath = "CMHome.aspx";
                  }
                  else
                  {
                     return;
                  }
               }
            }

            for (int i = 0; i < request.Url.Segments.Length; i++)
            {
               if (request.Url.Segments[i] != "/")
                  accessedPage = string.Concat(accessedPage, request.Url.Segments[i]).ToLower();
            }

            var @map = (from prm in dataProvider.page_route_mapper
                        where prm.file_path_extension == ".aspx"
                           && prm.destination_file_path.ToLower() == accessedPage
                        select prm).FirstOrDefault();
            if (@map != null)
            {
               destinationFilePath = @map.source_file_path;
            }

            string qs = request.Url.Query.IsNotEmpty()
                        ? request.Url.Query.Replace(Constants.QUERY_PARAMETER_EVENT_ID, Constants.QUERY_PARAMETER_EVENT_ID_2)
                                           .Replace(Constants.QUERY_PARAMETER_PARTICIPANT_ID, Constants.QUERY_PARAMETER_EVENT_PARTICIPATION_ID)
                        : string.Empty;

            response.Redirect(string.Format("{0}/{1}{2}", ConfigurationManager.AppSettings["MGPCAN.Domain"], destinationFilePath, qs), true);
            return;
         }
      }

      #endregion
   }
}