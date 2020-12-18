using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using GA.BDC.Shared.Entities;
using MvcSiteMapProvider;

namespace GA.BDC.Web.Fundraising.MVC.Helpers.DynamicNodeProviders
{
    // ReSharper disable once InconsistentNaming
    public class BlogPostsDynamicNodeProvider : DynamicNodeProviderBase
    {
        public override IEnumerable<DynamicNode> GetDynamicNodeCollection(ISiteMapNode node)
        {
            using (var client = new HttpClient())
            {

                var uri = new Uri(string.Format("{0}/blog", ConfigurationManager.AppSettings["fundraising.webapi.host"]));
                var posts = client
                            .GetAsync(uri)
                            .Result
                            .Content.ReadAsAsync<IEnumerable<BlogPost>>().Result;
                foreach (var post in posts)
                {
                    var dynamicNode = new DynamicNode
                    {
                        Title = post.Title,
                        ParentKey = "Blog",
                        Url = string.Format("/blog/post/{0}", post.Url),
                        Protocol = "https"
                    };
                    yield return dynamicNode;
                }

            }
        }
    }
}