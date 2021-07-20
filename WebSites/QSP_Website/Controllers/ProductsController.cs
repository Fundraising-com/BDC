using System.Web.Mvc;


namespace QSP_Website.Controllers
{
    public class ProductsController : Controller
    {




      
        public ActionResult BeefJerky()
        {
            ViewBag.PageTitle = "Beef Jerky Fundraising Ideas - Qsp.com";
            ViewBag.Description = "Beef Jerky are some great Fundraising Products and Ideas";
            ViewBag.Keywords = "Ideas for non profit, school, help fundraising, beef jerky Fundraising, buy online";
            return View();
        }

        public ActionResult Lollipops ()
        {
            ViewBag.PageTitle = "Lollipop Fundraising Ideas - Qsp.com";
            ViewBag.Description = "Lollipop Fundraising Ideas for your next fundraisers";
            ViewBag.Keywords = "Ideas for non profit, school, help fundraising, Lollipop Fundraising, buy online";
            return View();
        }

        public ActionResult Smencils()
        {
            ViewBag.PageTitle = "Smencils Fundraising Ideas - Qsp.com";
            ViewBag.Description = "Good causes deserves great fundraisers! Fundraising ideas for schools, sports teams, churches and non-profits using Smencils with $1 Original Smencils";
            ViewBag.Keywords = "Fundraising Products, Sports Teams, Schools, Fundraising Ideas,Smencils";
            return View();
        }

        public ActionResult Cookies()
        {
            ViewBag.PageTitle = "Cookie Dough Fundraising Ideas - Qsp.com";
            ViewBag.Description = "Good causes deserves great fundraisers! Fundraising ideas for schools, sports teams, churches and non-profits using Cookie Dough";
            ViewBag.Keywords = "Fundraising Products, Sports Teams, Schools, Fundraising Ideas,Cookie Dough";

            return View();
        }

        public ActionResult Shiptohome()
        {
            ViewBag.PageTitle = "Ship to home programs - Qsp.com";
            ViewBag.Description = "Ship to home programs fundraising programs";
            ViewBag.Keywords = "ship to home, safe fundraisers";

            return View();
        }
    }
}