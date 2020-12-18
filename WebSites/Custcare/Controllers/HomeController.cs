using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GA.BDC.Web.Custcare.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetSAPProfitCheckReport(int groupId)
        {
            try
            {
                byte[] buffer = SAP.ZBapiGa.Client.GetSAPOnlineProfitStatement(groupId.ToString());
                string fileName = "Statement.pdf";
                return File(buffer, System.Net.Mime.MediaTypeNames.Application.Pdf, fileName);
            }
            catch (Exception ex)
            {
                return Content(String.Format("Error generating PDF Statement: {0}", ex.Message));
            }            
        }
    }
}
