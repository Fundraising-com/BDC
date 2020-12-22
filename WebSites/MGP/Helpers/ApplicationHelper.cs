using System;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Routing;
using GA.BDC.Web.MGP.Properties;

namespace GA.BDC.Web.MGP.Helpers
{
    public static class ApplicationHelper
    {
        #region Public Methods

        public static string GetImageSrc(bool isSecure = false)
        {
            var path = string.Format("{0}/{1}/",
                                 ConfigurationManager.AppSettings["Fundraising.Domain"],
                                 Settings.Default.ImagesVirtualDirectory);
            if (isSecure)
                path = path.Replace("http", "https");
            return path;
        }

        public static string GetImageSrc(string imgFullImagePath, bool isSecure = false)
        {
            var path = string.Format("{0}/{1}/{2}",
                                 ConfigurationManager.AppSettings["Fundraising.Domain"],
                                 Settings.Default.ImagesVirtualDirectory,
                                 imgFullImagePath);
            if (isSecure)
                path = path.Replace("http", "https");
            return path;
        }

        #endregion
    }
}