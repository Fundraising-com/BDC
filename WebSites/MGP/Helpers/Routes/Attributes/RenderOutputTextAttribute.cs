using System;
using System.Web.Mvc;
using System.Linq;
using System.Web;
using System.Text;
using System.Text.RegularExpressions;
using GA.BDC.Web.MGP.Helpers.FilterStream;
using GA.BDC.Data.MGP.esubs_global_v2.Models;
using GA.BDC.Web.MGP.Models.Branding;
using SWCorporate.ServiceModelEx.Extensions;

namespace GA.BDC.Web.MGP.Helpers.Routes.Attributes
{
    public class RenderOutputTextAttribute : ActionFilterAttribute
    {
        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            var disable = filterContext.Controller.ViewBag.DisableRenderTextOutput;
            if (disable != null)
            {
                if (disable == "true")
                {
                    return;
                }
            }

            if (filterContext.HttpContext.Session == null) throw new Exception("Session is null");
            var partnerId = Convert.ToInt32(filterContext.HttpContext.Session[Constants.SESSION_KEY_PARTNER_ID]);
            var request = filterContext.HttpContext.Request;
            var response = filterContext.HttpContext.Response;

            response.Filter = new TextAsset(response.Filter, s =>
            {
                using (var dataProvider = new DataProvider())
                {
                    dataProvider.Configuration.LazyLoadingEnabled = false;
                    dataProvider.Configuration.AutoDetectChangesEnabled = false;
                    var @all = (from ptg in dataProvider.partner_text_group
                                from txt in dataProvider.text_asset_replace
                                where ptg.partner_id == partnerId
                                   && ptg.text_group_id == txt.text_group_id
                                   && txt.culture_code == "en-US"
                                select txt).OrderByDescending(p => p.is_priority).ToList();
                    var output = new StringBuilder(s);
                    foreach (var row in @all)
                    {
                        output.Replace(row.old_text, row.new_text);
                    }
                    return output.ToString();
                }
            });

            base.OnResultExecuted(filterContext);
        }
    }
}