using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using GA.BDC.Shared.Entities;
using MvcSiteMapProvider;

namespace GA.BDC.Web.Fundraising.MVC.Helpers.DynamicNodeProviders
{
    // ReSharper disable once InconsistentNaming
    public class USAProductsDynamicNodeProvider : DynamicNodeProviderBase
    {

        public override IEnumerable<DynamicNode> GetDynamicNodeCollection(ISiteMapNode node)
        {
            using (var client = new HttpClient())
            {

                var uri = new Uri(string.Format("{0}/products?rootCategoryId=" + ConfigurationManager.AppSettings["US.Root.PackageId"], ConfigurationManager.AppSettings["fundraising.webapi.host"]));
                var products = client
                   .GetAsync(uri)
                   .Result
                   .Content.ReadAsAsync<IEnumerable<Product>>().Result;
                foreach (var product in products)
                {
                    var dynamicNode = new DynamicNode
                    {
                        Title = product.Name,
                        ParentKey = "USAProducts",
                        Url =
                          string.Format("/products/{0}/{1}/{2}", product.Category.Parent.Url, product.Category.Url,
                             product.Url),
                        Protocol = "https",
                        UpdatePriority = UpdatePriority.Absolute_080
                    };
                    yield return dynamicNode;
                }
            }
        }
    }
}