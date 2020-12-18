using GA.BDC.Shared.Entities;
using MvcSiteMapProvider;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace GA.BDC.Web.EzFundMVC.Helpers.DynamicNodeProviders
{
    public class ProductsDynamicNodeProvider : DynamicNodeProviderBase
    {
        public override IEnumerable<DynamicNode> GetDynamicNodeCollection(ISiteMapNode node)
        {
           
            using (var client = new HttpClient())
            {

                var uri = new Uri(string.Format("{0}/products?IsSitemap=true", ConfigurationManager.AppSettings["ezfund.webapi.host"]));
                var products = client
                            .GetAsync(uri)
                            .Result
                            .Content.ReadAsAsync<IEnumerable<Product>>().Result;
                foreach (var product in products)
                {
                    var dynamicNode = new DynamicNode
                    {
                        Title = product.Name,
                        ParentKey = "Products",
                        Url =
                     string.Format("/products/{0}/{1}/{2}", product.Category.ParentUrl, product.Category.Url,
                        product.Url),
                        Protocol = "http",
                        UpdatePriority = UpdatePriority.Absolute_080
                    };
                    yield return dynamicNode;
                }

            }
        }
    }
}