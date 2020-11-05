using GA.BDC.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GA.BDC.WebApi.EzFund.Controllers
{
    public class BannersController : ApiController
    {
      //[HttpGet]
		//public IHttpActionResult GetAllActive(bool isActive)
		//{
			
		//			return Ok(new List<Banner>() {
		//					 new Banner { Image = "/Content/images/public/bannerhomepage-cars3.jpg", Id = 1, Url ="/products/in-hand-sellers/smecils-scented-pencils-and-pens/pixar-cars-3", AlternativeText ="smencils cars"},
		//					 new Banner { Image = "/Content/images/public/banner-homepage-cookies.jpg", Id = 2, Url ="/products/order-takers/food-products", AlternativeText ="cookie dough"},
		//					 new Banner { Image = "/Content/images/public/banner-homepage-tees.jpg", Id = 3, Url ="/products", AlternativeText ="t-shirts"}});

				
		//}
		[HttpGet]
		public IHttpActionResult GetAllActive(bool isActive, BannerType bannerType)
		{
			//TODO: Change all this logic to DB
			switch (bannerType)
			{
				case BannerType.Desktop:
					return Ok(new List<Banner>() {
                        new Banner { Image = "/Content/images/public/banner-sellingkit.jpg", Id = 1, Url ="/request-selling-kit", AlternativeText="request selling kit"},
                        new Banner { Image = "/Content/images/public/banner-guide.jpg", Id = 2, Url ="/request-a-kit", AlternativeText ="request a kit"},
                        new Banner { Image = "/Content/images/public/banner-homepage-cookies.jpg", Id = 4, Url ="/products/cookie-dough-fundraisers/cookie-dough", AlternativeText ="cookie dough"},
					    new Banner { Image = "/Content/images/public/banner-homepage-tees.jpg", Id = 5, Url ="/products/custom/customized-Fundraising-Products/custom-t-shirts", AlternativeText ="t-shirts"},
                    new Banner { Image = "/Content/images/public/banner-ezf_td.jpg", Id = 6, Url = "/products/cookie-dough-fundraisers/cookie-dough/COOKIE-DOUGH-BAR", AlternativeText = "cookie dough bar" }});


				case BannerType.Mobile:
					return Ok(new List<Banner>() {
                        new Banner { Image = "/Content/images/public/mobile-banner-sellingkit.jpg", Id = 1, Url ="/request-selling-kit", AlternativeText ="request selling kit"},
                        new Banner { Image = "/Content/images/public/mobile-banner-guide.jpg", Id = 2, Url ="/request-a-kit", AlternativeText ="request a kit"},
                       new Banner { Image = "/Content/images/public/mobile-bannerhomepage-cookies.jpg", Id = 4, Url ="/products/cookie-dough-fundraisers/cookie-dough", AlternativeText ="cookie dough"},
					    new Banner { Image = "/Content/images/public/mobile-bannerhomepage-tees.jpg", Id = 5, Url ="/products/custom/customized-Fundraising-Products/custom-t-shirts", AlternativeText ="t-shirts"},
                    new Banner { Image = "/Content/images/public/banner_mobile_ezf_td.jpg", Id = 6, Url = "/products/cookie-dough-fundraisers/cookie-dough/COOKIE-DOUGH-BAR", AlternativeText = "t-shirts" }});

            default:
					return Ok();
			}
		}
	}
}
