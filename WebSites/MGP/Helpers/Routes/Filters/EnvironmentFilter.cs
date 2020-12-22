using System;
using System.Configuration;
using System.Web.Mvc;
using System.Linq;
using GA.BDC.Web.MGP.Helpers.Extensions;
using GA.BDC.Data.MGP.esubs_global_v2.Models;
using GA.BDC.Shared.Helpers;

namespace GA.BDC.Web.MGP.Helpers.Routes.Filters
{
    /// <summary>
    /// Handles all environment variables necessary to cover MGP business rules
    /// </summary>
    public class EnvironmentFilter : IActionFilter
    {
        #region Constants
        private const string ENCRYPTION_KEY = "3fuNd84151NG";
        #endregion

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            int promotionId, fcExternalId, eventParticipationId, participantHomeId, touchId, creationChannelId, gaExternalSupporterID;
            string header, footer;
            if (filterContext.Controller.ValueProvider.GetValue(Constants.QUERY_PARAMETER_TOUCH_ID) != null)
            {
                touchId = ToInt(filterContext.Controller.ValueProvider.GetValue(Constants.QUERY_PARAMETER_TOUCH_ID).AttemptedValue);
                if (touchId > 0)
                    filterContext.HttpContext.Session[Constants.SESSION_KEY_TOUCH_ID] = touchId;
            }
            if (filterContext.Controller.ValueProvider.GetValue(Constants.QUERY_PARAMETER_TOUCH_ID_2) != null)
            {
                touchId = ToInt(filterContext.Controller.ValueProvider.GetValue(Constants.QUERY_PARAMETER_TOUCH_ID_2).AttemptedValue);
                if (touchId > 0)
                    filterContext.HttpContext.Session[Constants.SESSION_KEY_TOUCH_ID] = touchId;
            }
            if (filterContext.Controller.ValueProvider.GetValue(Constants.QUERY_PARAMETER_EVENT_PARTICIPATION_ID) != null)
            {
                eventParticipationId = ToInt(filterContext.Controller.ValueProvider.GetValue(Constants.QUERY_PARAMETER_EVENT_PARTICIPATION_ID).AttemptedValue);
                if (eventParticipationId > 0)
                    filterContext.HttpContext.Session[Constants.SESSION_KEY_EVENT_PARTICIPATION_ID] = eventParticipationId;
            }
            if (filterContext.Controller.ValueProvider.GetValue(Constants.QUERY_PARAMETER_PARTICIPANT_HOME_ID) != null)
            {
                participantHomeId = ToInt(filterContext.Controller.ValueProvider.GetValue(Constants.QUERY_PARAMETER_PARTICIPANT_HOME_ID).AttemptedValue);
                if (participantHomeId > 0)
                    filterContext.HttpContext.Session[Constants.SESSION_KEY_PARTICIPANT_HOME_ID] = participantHomeId;
            }
            if (filterContext.Controller.ValueProvider.GetValue(Constants.QUERY_PARAMETER_PROMOTIONID) != null)
            {
                promotionId = ToInt(filterContext.Controller.ValueProvider.GetValue(Constants.QUERY_PARAMETER_PROMOTIONID).AttemptedValue);
                if (promotionId > 0)
                    filterContext.HttpContext.Session[Constants.SESSION_KEY_PROMOTION_ID] = promotionId;
            }
            if (filterContext.Controller.ValueProvider.GetValue(Constants.QUERY_PARAMETER_FCEXTERNALID) != null)
            {
                fcExternalId = ToInt(filterContext.Controller.ValueProvider.GetValue(Constants.QUERY_PARAMETER_FCEXTERNALID).AttemptedValue);
                if (fcExternalId > 0)
                    filterContext.HttpContext.Session[Constants.SESSION_KEY_FCEXTERNAL_ID] = fcExternalId;
            }
            if (filterContext.Controller.ValueProvider.GetValue(Constants.QUERY_PARAMETER_CREATIONCHANNEL) != null)
            {
                creationChannelId = ToInt(filterContext.Controller.ValueProvider.GetValue(Constants.QUERY_PARAMETER_CREATIONCHANNEL).AttemptedValue);
                if (creationChannelId > 0)
                    filterContext.HttpContext.Session[Constants.SESSION_KEY_CREATION_CHANNEL_ID] = creationChannelId;
            }
            if (filterContext.Controller.ValueProvider.GetValue(Constants.QUERY_PARAMETER_GA_EXTERNAL_SUPPORTER_ID) != null)
            {
                gaExternalSupporterID = ToInt(filterContext.Controller.ValueProvider.GetValue(Constants.QUERY_PARAMETER_GA_EXTERNAL_SUPPORTER_ID).AttemptedValue);
                if (gaExternalSupporterID > 0)
                    filterContext.HttpContext.Session[Constants.SESSION_KEY_GA_EXTERNAL_SUPPORTER_ID] = gaExternalSupporterID;
            }
            if (filterContext.Controller.ValueProvider.GetValue(Constants.QUERY_PARAMETER_HEADER) != null)
            {
                header = filterContext.Controller.ValueProvider.GetValue(Constants.QUERY_PARAMETER_HEADER).AttemptedValue;
                if (header.IsNotEmpty())
                    filterContext.HttpContext.Session[Constants.SESSION_KEY_DISABLE_HEADER] = (header == "0" ? true : false);
            }
            if (filterContext.Controller.ValueProvider.GetValue(Constants.QUERY_PARAMETER_FOOTER) != null)
            {
                footer = filterContext.Controller.ValueProvider.GetValue(Constants.QUERY_PARAMETER_FOOTER).AttemptedValue;
                if (footer.IsNotEmpty())
                    filterContext.HttpContext.Session[Constants.SESSION_KEY_DISABLE_FOOTER] = (footer == "0" ? true : false);
            }
        }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
        }

        #region Static Functions
        public static string Encrypt(string val)
        {
            return System.Web.HttpUtility.UrlEncode(EncryptHelper.EncryptTripleDES(val, ENCRYPTION_KEY));
        }

        public static int DecryptInteger(string val)
        {
            return new EnvironmentFilter().ToInt(val);
        }

        public static string DecryptString(string val)
        {
            try
            {
                val = val.Replace("%3d", "=");
                if (!val.EndsWith("%3d") && !val.EndsWith("="))
                {
                    val += "=";
                }
                if (val.EndsWith("%"))
                {
                    val = val.Replace("%", "=");
                }

                if (val.EndsWith("%="))
                {
                    val = val.Replace("%=", "=");
                }

                val = val.Replace("%3", "");
                val = val.Replace("%25", "");

                val = System.Web.HttpUtility.UrlDecode(val);
                val = val.Replace(" ", "+");
               val = EncryptHelper.DecryptTripleDES(val, ENCRYPTION_KEY);
            }
            catch
            {
                try
                {

                    val = val.Replace("=", "==");
                    val = val.Replace("%3d", "==");
                    if (!val.EndsWith("%3d") && !val.EndsWith("="))
                    {
                        val += "==";
                    }
                    if (val.EndsWith("%"))
                    {
                        val = val.Replace("%", "=");
                    }

                    if (val.EndsWith("%="))
                    {
                        val = val.Replace("%=", "=");
                    }

                    val = val.Replace("%3", "");
                    val = val.Replace("%25", "");

                    val = System.Web.HttpUtility.UrlDecode(val);
                    val = val.Replace(" ", "+");
                    val = EncryptHelper.DecryptTripleDES(val, ENCRYPTION_KEY);
            }
                catch { val = string.Empty; }
            }
            return val;
        }
        #endregion

        #region Private Functions
        private int ToInt(object o)
        {
            if (o != null)
            {
                int i = int.MinValue;
                try
                {
                    i = int.Parse(o.ToString().Replace("robots.txt", ""));
                }
                catch
                {
                    try
                    {

                        string id = o.ToString();

                        id = id.Replace("%3d", "=");
                        if (!id.EndsWith("%3d") && !id.EndsWith("="))
                        {
                            id += "=";
                        }
                        if (id.EndsWith("%"))
                        {
                            id = id.Replace("%", "=");
                        }

                        if (id.EndsWith("%="))
                        {
                            id = id.Replace("%=", "=");
                        }

                        id = id.Replace("%3", "");
                        id = id.Replace("%25", "");

                        id = System.Web.HttpUtility.UrlDecode(id);
                        id = id.Replace(" ", "+");
                        id = EncryptHelper.DecryptTripleDES(id, ENCRYPTION_KEY);
                  i = int.Parse(id);
                    }
                    catch
                    {
                        try
                        {
                            string id = o.ToString();
                            id = id.Replace("=", "==");
                            id = id.Replace("%3d", "==");
                            if (!id.EndsWith("%3d") && !id.EndsWith("="))
                            {
                                id += "==";
                            }
                            if (id.EndsWith("%"))
                            {
                                id = id.Replace("%", "=");
                            }

                            if (id.EndsWith("%="))
                            {
                                id = id.Replace("%=", "=");
                            }

                            id = id.Replace("%3", "");
                            id = id.Replace("%25", "");

                            id = System.Web.HttpUtility.UrlDecode(id);
                            id = id.Replace(" ", "+");
                            id = EncryptHelper.DecryptTripleDES(id, ENCRYPTION_KEY);
                            i = int.Parse(id);
                        }
                        catch { return int.MinValue; }
                    }
                }
                return i;
            }
            else
            {
                return int.MinValue;
            }
        }
        private string ToString(object o)
        {
            if (o != null)
            {
                string id = o.ToString();
                if (!id.EndsWith("%3d") && id.EndsWith("="))
                {
                    id = id.Replace(" ", "+");
                }
                return id;
            }
            else
            {
                return null;
            }
        }
        private bool ToBool(object o)
        {
            if (o != null)
            {
                string sValue = o.ToString().Trim();
                if (
                    string.Compare(sValue, "1", true) == 0
                    || string.Compare(sValue, "T", true) == 0
                    )
                    return true;
                return (string.Compare(sValue, bool.TrueString, true) == 0);
            }
            else
            {
                return false;
            }
        }
        #endregion
    }
}