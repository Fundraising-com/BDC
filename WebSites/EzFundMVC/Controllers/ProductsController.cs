using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Mvc;
using GA.BDC.Shared.Entities;
using System.Configuration;

namespace GA.BDC.Web.EzFundMVC.Controllers
{
    [RoutePrefix("products"), AllowAnonymous]
    public class ProductsController : Controller
    {
        /// <summary>
        /// Redirects the action to the correct method
        /// </summary>
        /// <param name="query">Category and Product</param>
        /// <returns>Method</returns>
        public ActionResult CleanUrlRedirect(string query = "")
        {
            var variables = query.Split('/');
            var rootCategory = variables.Length > 0 ? variables[0] : string.Empty;
            var category = variables.Length > 1 ? variables[1] : string.Empty;
            var product = variables.Length > 2 ? variables[2] : string.Empty;

            if (!string.IsNullOrEmpty(product) && !string.IsNullOrEmpty(category) && !string.IsNullOrEmpty(rootCategory))
            {
                return View("Product");
            }
            if (!string.IsNullOrEmpty(category) && !string.IsNullOrEmpty(rootCategory))
            {
                return View("SubCategory");
            }
            return !string.IsNullOrEmpty(rootCategory) ? View("Category") : View("Index");
        }

        /// <summary>
        /// Shows all the products in the most general page
        /// </summary>
        /// <returns></returns>
        private ActionResult All()
        {
            ViewBag.HideBreadcrumbs = true;
            return View("Index");
        }
    }
}
