using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Web.Mvc;
using GA.BDC.Shared.Entities;

namespace GA.BDC.Web.Fundraising.MVC.Controllers
{
    [RoutePrefix("products"), AllowAnonymous]
    public class ProductsController : Controller
    {

        /// <summary>
        /// Redirects the action to the correct method
        /// </summary>
        /// <param name="country">1 = Canada, 2 = USA</param>
        /// <param name="query">Category and Product</param>
        /// <returns>Method</returns>
        public ActionResult Redirect(int country, string query = "")
        {
            var variables = query.Split('/');
            var rootCategory = variables.Length > 0 ? variables[0] : string.Empty;
            var category = variables.Length > 1 ? variables[1] : string.Empty;
            var product = variables.Length > 2 ? variables[2] : string.Empty;
            if (!string.IsNullOrEmpty(product) && !string.IsNullOrEmpty(category) && !string.IsNullOrEmpty(rootCategory))
            {
                return Product(country, rootCategory, category, product);
            }
            if (!string.IsNullOrEmpty(category) && !string.IsNullOrEmpty(rootCategory))
            {
                return SubCategory(country, rootCategory, category);
            }
            return !string.IsNullOrEmpty(rootCategory) ? Category(country, rootCategory) : All(country);
        }
        /// <summary>
        /// Shows all the products in the most general page
        /// </summary>
        /// <returns></returns>
        private ActionResult All(int country)
        {
            ViewBag.CountryId = country;
            ViewBag.HideBreadcrumbs = true;
            return View("Index");
        }

        /// <summary>
        /// Shows the category selected with the products related to it
        /// </summary>
        /// <param name="country"></param>
        /// <param name="rootCategory"></param>
        /// <returns></returns>
        private ActionResult Category(int country, string rootCategory)
        {
            try
            {
                ViewBag.CountryId = country;
                ViewBag.HideBreadcrumbs = true;
                using (var client = new HttpClient())
                {
                    var uri = new Uri(string.Format("{0}/categories?country={2}&url={1}&isRoot=true", ConfigurationManager.AppSettings["fundraising.webapi.host"], rootCategory, country));
                    var parentCategory = client
                                .GetAsync(uri)
                                .Result
                                .Content.ReadAsAsync<Category>().Result;
                    if (parentCategory == null)
                    {
                        return All(country);
                    }
                    
                    uri = new Uri($"{ConfigurationManager.AppSettings["fundraising.webapi.host"]}/categories?parentId={parentCategory.Id}");
                    var subCategories = client
                                .GetAsync(uri)
                                .Result
                                .Content.ReadAsAsync<IEnumerable<Category>>().Result;
                    if (subCategories == null)
                    {
                        return All(country);
                    }
                    foreach (var subCategory in subCategories)
                    {
                        subCategory.Parent = parentCategory;
                        uri = new Uri($"{ConfigurationManager.AppSettings["fundraising.webapi.host"]}/products?categoryId={subCategory.Id}");
                        var products = client
                                    .GetAsync(uri)
                                    .Result
                                    .Content.ReadAsAsync<IEnumerable<Product>>().Result;
                        subCategory.Products = products;
                    }
                    ViewBag.SubCategories = subCategories;
                    return View("Category", parentCategory);
                }
            }
            catch (Exception)
            {
                return All(country);
            }                        
        }

        /// <summary>
        /// Shows the category selected with the products related to it
        /// </summary>
        /// <param name="country"></param>
        /// <param name="rootCategory"></param>
        /// <param name="category"></param>
        /// <returns></returns>
        private ActionResult SubCategory(int country, string rootCategory, string category)
        {
            try
            {
                ViewBag.CountryId = country;
                ViewBag.HideBreadcrumbs = true;
                using (var client = new HttpClient())
                {
                    var uri = new Uri(string.Format("{0}/categories?country={2}&url={1}&isRoot=true", ConfigurationManager.AppSettings["fundraising.webapi.host"], rootCategory, country));
                    var parentCategory = client
                                .GetAsync(uri)
                                .Result
                                .Content.ReadAsAsync<Category>().Result;
                    if (parentCategory == null)
                    {
                        return All(country);
                    }
                    
                    uri = new Uri($"{ConfigurationManager.AppSettings["fundraising.webapi.host"]}/categories?parentId={parentCategory.Id}");
                    var subCategories = client
                                .GetAsync(uri)
                                .Result
                                .Content.ReadAsAsync<IEnumerable<Category>>().Result;
                    if (subCategories == null)
                    {
                        return All(country);
                    }
                    var subCategory = subCategories.First(p => p.Url == category);
                    if (subCategory == null)
                    {
                        return All(country);
                    }
                    subCategory.Parent = parentCategory;
                    return View("SubCategory", subCategory);
                }
            }
            catch (Exception)
            {
                return All(country);
            }
        }

        /// <summary>
        /// Shows a specific product page
        /// </summary>
        /// <param name="country"></param>
        /// <param name="rootCategory">Root Category</param>
        /// <param name="category"></param>
        /// <param name="product"></param>
        /// <returns></returns>
        private ActionResult Product(int country, string rootCategory, string category, string product)
        {
            try
            {
                ViewBag.CountryId = country;
                ViewBag.HideBreadcrumbs = true;
                using (var client = new HttpClient())
                {
                    var uri =
                        new Uri(string.Format("{0}/products?country={4}&rootCategory={1}&category={2}&url={3}",
                            ConfigurationManager.AppSettings["fundraising.webapi.host"], rootCategory, category, product, country));
                    var productFound = client
                        .GetAsync(uri)
                        .Result
                        .Content.ReadAsAsync<Product>().Result;
                    if (productFound == null)
                    {
                        return All(country);
                    }
                    
                    return View("Product", productFound);
                }
            }
            catch (Exception)
            {
                return All(country);
            }
        }
    }
}