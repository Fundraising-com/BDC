using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace GA.BDC.Web.Scratchcard.MVC.Helpers
{
    public static class HTMLHelpers
    {
        public static IHtmlString CategoryUrlByCountry(int country)
        {
            switch (country)
            {
                case 1:
                    return new HtmlString("canada");
                case 2:
                    return new HtmlString("products");
                default:
                    return new HtmlString("products");
            }
        }

        public static IHtmlString ChocolateIdByCountry(int country)
        {
            switch (country)
            {
                case 1:
                    return new HtmlString(ConfigurationManager.AppSettings["Canada.ChocolateCategoryId"]);
                case 2:
                    return new HtmlString(ConfigurationManager.AppSettings["US.ChocolateCategoryId"]);
                default:
                    return new HtmlString(ConfigurationManager.AppSettings["US.ChocolateCategoryId"]);
            }
        }
    }
}