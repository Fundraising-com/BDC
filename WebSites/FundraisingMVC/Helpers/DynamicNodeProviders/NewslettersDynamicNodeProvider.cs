using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using GA.BDC.Shared.Entities;
using MvcSiteMapProvider;

namespace GA.BDC.Web.Fundraising.MVC.Helpers.DynamicNodeProviders
{
    // ReSharper disable once InconsistentNaming
    public class NewslettersDynamicNodeProvider : DynamicNodeProviderBase
    {
        public override IEnumerable<DynamicNode> GetDynamicNodeCollection(ISiteMapNode node)
        {
            using (var client = new HttpClient())
            {

                var uri = new Uri(string.Format("{0}/newsletters", ConfigurationManager.AppSettings["fundraising.webapi.host"]));
                var newsletters = client
                            .GetAsync(uri)
                            .Result
                            .Content.ReadAsAsync<IEnumerable<Newsletter>>().Result;
                foreach (var newsletter in newsletters)
                {
                    var dynamicNode = new DynamicNode
                    {
                        Title = newsletter.Title,
                        ParentKey = "Newsletters",
                        Url = string.Format("/newsletters/{0}", newsletter.Url),
                        Protocol = "https"
                    };
                    yield return dynamicNode;
                }

            }
        }
    }
}