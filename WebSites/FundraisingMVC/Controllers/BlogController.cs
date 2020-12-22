using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Ninject;
using Ninject.Extensions.Logging;
using System.Net.Http;
using System.Configuration;
using GA.BDC.Shared.Entities;
using System.Web;

namespace GA.BDC.Web.Fundraising.MVC.Controllers
{
   [RoutePrefix("blog"), AllowAnonymous]


    
    public class BlogController : Controller
   {
      [Inject]
      public ILogger Logger { get; set; }

        [OutputCache(CacheProfile = "cache1Hour")]
        public new ActionResult Redirect(string query = "")
      {

         using (var client = new HttpClient())
         {
            var uri = new Uri($"{ConfigurationManager.AppSettings["fundraising.webapi.host"]}/blogcategories");
            var categories = client
                            .GetAsync(uri)
                            .Result
                            .Content.ReadAsAsync<IList<BlogCategory>>().Result;
            ViewBag.Categories = categories;
            uri = new Uri($"{ConfigurationManager.AppSettings["fundraising.webapi.host"]}/blog?sortByRecent=true&limit=5");
            var recentPosts = client
                            .GetAsync(uri)
                            .Result
                            .Content.ReadAsAsync<IList<BlogPost>>().Result;
            ViewBag.LastPosts = recentPosts;
         }

         if (string.IsNullOrEmpty(query))
         {
            return Index();
         }
         var queryArray = query.Split('/');
         var firstFolder = queryArray[0].ToLower();

         switch (firstFolder)
         {
            case "post":
               return Post(query);
            case "category":
               return Category(query);
            case "tag":
               return Tag(query);
            case "old-posts":
               return All();
            default:
               return Index();
         }
      }

        [OutputCache(CacheProfile = "cache1Hour")]
        private ActionResult Post(string query)
      {
         var isPreview = Request.QueryString["f"] != null && Request.QueryString["f"] == "1";
         var queryArray = query.Split('/');
         if (queryArray.Length < 2)
         {
            return All();
         }
         var url = queryArray[1];
         //now you have the url to search for, call the web api and return it to the view

         using (var client = new HttpClient())
         {
            var uri = new Uri($"{ConfigurationManager.AppSettings["fundraising.webapi.host"]}/blog/?url={url}&isPreview={isPreview}");
            var result = client.GetAsync(uri).Result;
            if (!result.IsSuccessStatusCode)
            {
               return All();
            }
            var post = result.Content.ReadAsAsync<BlogPost>().Result;
            uri = new Uri($"{ConfigurationManager.AppSettings["fundraising.webapi.host"]}/blog/?random=true&limit=4");
            var randomPosts = client
                            .GetAsync(uri)
                            .Result
                            .Content.ReadAsAsync<IList<BlogPost>>().Result;
            ViewBag.RandomPosts = randomPosts;
            return View("Post", post);
         }


      }

        [OutputCache(CacheProfile = "cache1Hour")]
        private ActionResult Category(string query)
      {
         var queryArray = query.Split('/');
         if (queryArray.Length < 2)
         {
            return All();
         }
         var url = queryArray[1];
         using (var client = new HttpClient())
         {
            var uri = new Uri($"{ConfigurationManager.AppSettings["fundraising.webapi.host"]}/blogCategories/?url={url}");
            var result = client.GetAsync(uri).Result;
            if (!result.IsSuccessStatusCode)
            {
               return All();
            }
            var category = result.Content.ReadAsAsync<BlogCategory>().Result;
            uri = new Uri($"{ConfigurationManager.AppSettings["fundraising.webapi.host"]}/blog/?categoryId={category.Id}");
            var posts = client
                            .GetAsync(uri)
                            .Result
                            .Content.ReadAsAsync<IList<BlogPost>>().Result;
            ViewBag.Posts = posts;
            return View("Category", category);
         }
      }

        [OutputCache(CacheProfile = "cache1Hour")]
        private ActionResult All()

      {
         using (var client = new HttpClient())
         {
            var uri = new Uri($"{ConfigurationManager.AppSettings["fundraising.webapi.host"]}/blog");
            var posts = client
                            .GetAsync(uri)
                            .Result
                            .Content.ReadAsAsync<IList<BlogPost>>().Result;
            ViewBag.Posts = posts;
            return View("All", posts);
         }
      }

        [OutputCache(CacheProfile = "cache1Hour")]
        private ActionResult Tag(string query)
      {
         var queryArray = query.Split('/');
         if (queryArray.Length < 2)
         {
            return All();
         }
         var url = queryArray[1];
         using (var client = new HttpClient())
         {
            var uri = new Uri($"{ConfigurationManager.AppSettings["fundraising.webapi.host"]}/blogTags/?url={url}");
            var result = client.GetAsync(uri).Result;
            if (!result.IsSuccessStatusCode)
            {
               return All();
            }
            var tag = result.Content.ReadAsAsync<BlogTag>().Result;
            uri = new Uri($"{ConfigurationManager.AppSettings["fundraising.webapi.host"]}/blog/?tagId={tag.Id}");
            var posts = client
                            .GetAsync(uri)
                            .Result
                            .Content.ReadAsAsync<IList<BlogPost>>().Result;
            ViewBag.Posts = posts;
            return View("Tag", tag);
         }
      }

        [OutputCache(CacheProfile = "cache1Hour")]
       
        private ActionResult Index()
      {
         using (var client = new HttpClient())
         {

                //if (System.Web.HttpContext.Current.Cache["BPost"] == null)
                //{
                //    var uri = new Uri($"{ConfigurationManager.AppSettings["fundraising.webapi.host"]}/blog?sortByRecent=true&limit=15");
                //    var posts = client
                //                    .GetAsync(uri)
                //                    .Result
                //                    .Content.ReadAsAsync<IList<BlogPost>>().Result;
                //    ViewBag.Posts = posts;
                //    System.Web.HttpContext.Current.Cache["BPost"] = posts;
                //    //System.Web.HttpContext.Current.Cache["BPost"] = posts;
                //    return View("Index", posts);
                //}
                //else
                //{
                //    var posts = System.Web.HttpContext.Current.Cache["BPost"];
                //    return View("Index", posts);
                //}


                var uri = new Uri($"{ConfigurationManager.AppSettings["fundraising.webapi.host"]}/blog?sortByRecent=true&limit=15");
                var posts = client
                                .GetAsync(uri)
                                .Result
                                .Content.ReadAsAsync<IList<BlogPost>>().Result;
                ViewBag.Posts = posts;
                

                return View("Index", posts);
            }
      }


        



    }
}