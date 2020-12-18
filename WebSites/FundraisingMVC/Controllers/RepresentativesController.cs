using System.IO;
using System.Web.Mvc;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Ninject;
using Ninject.Extensions.Logging;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace GA.BDC.Web.Fundraising.MVC.Controllers
{
   [RoutePrefix("representatives"), AllowAnonymous]
   public class RepresentativesController : Controller
   {
      [Inject]
      public ILogger Logger { get; set; }

      public ActionResult Index(int id = 1)
      {
         ViewBag.RepresentativeId = id;
         return View();
      }

      [Route("request-information")]
      public ActionResult RequestInformation()
      {
         return View();
      }

      [Route("my-campaigns")]
      public ActionResult MyCampaigns()
      {
         return View();
      }

      [Route("thank-you")]
      public ActionResult ThankYou()
      {
         return View();
      }

      [Route("fundraisers")]
      public ActionResult Fundraisers()
      {
         return View();
      }

      [Route("categories")]
      public ActionResult Categories(int categoryId = 1, string url = "")
      {
         ViewBag.CategoryId = categoryId;
          ViewBag.Url = url;
         return View();
      }

      [Route("product")]
      public ActionResult Product(int productId = 1)
      {
         ViewBag.ProductId = productId;
         return View();
      }

      [Route("testimonials")]
      public ActionResult Testimonials()
      {
         return View();
      }

      [Route("sell-sheet")]
      public FileResult DownloadSellSheet(string representativeId, int representativePartnerId, string representativeName, int representativeExt, string email, string phone, string fileName, string redirect, string outputName)
      {
         var pdf = Server.MapPath("/content/external/rep/sellsheets/" + fileName);
         var outputFilePath = Server.MapPath("/content/external/rep/temp.pdf");

         PdfReader pdfReader = null;

         try
         {

            pdfReader = new PdfReader(pdf);

            using (
                    var pdfOutputFile = new FileStream(outputFilePath, FileMode.Create))
            {
               PdfStamper pdfStamper = null;
               try
               {
                  pdfStamper = new PdfStamper(pdfReader, pdfOutputFile);
                  var acroFields = pdfStamper.AcroFields;
                  if (acroFields.Fields.ContainsKey("Name"))
                  {
                     acroFields.SetField("Name", Server.HtmlDecode(representativeName));   
                  }
                  if (acroFields.Fields.ContainsKey("Email"))
                  {
                     acroFields.SetField("Email", Server.HtmlDecode(email));
                  }
                  if (acroFields.Fields.ContainsKey("URL"))
                  {
                     acroFields.SetField("URL", Server.HtmlDecode(string.Format("https://www.fundraising.com/{0}", redirect)));
                  }
                  if (acroFields.Fields.ContainsKey("Phone"))
                  {
                    acroFields.SetField("Phone", Server.HtmlDecode(phone));
                  }

                        if (System.IO.File.Exists(Server.MapPath("/Content/external/rep/SmallRepImage/" + representativeExt + ".jpg")))
                        {

                            if (fileName == "all.pdf")
                            {
                                var pdfContentByte = pdfStamper.GetOverContent(2);
                                using (var inputImageStream = new FileStream(Server.MapPath("/Content/external/rep/SmallRepImage/" + representativeExt + ".jpg"), FileMode.Open, FileAccess.Read, FileShare.Read))
                                {
                                    var image = Image.GetInstance(inputImageStream);
                                    image.ScaleAbsolute(47, 70);
                                    image.SetAbsolutePosition(50, 0);

                                    pdfContentByte.AddImage(image);
                                }
                            }
                            else
                            {

                                var pdfContentByte = pdfStamper.GetOverContent(1);
                                using (var inputImageStream = new FileStream(Server.MapPath("/Content/external/rep/SmallRepImage/" + representativeExt + ".jpg"), FileMode.Open, FileAccess.Read, FileShare.Read))
                                {
                                    var image = Image.GetInstance(inputImageStream);
                                    image.ScaleAbsolute(47, 70);
                                    image.SetAbsolutePosition(50, 0);

                                    pdfContentByte.AddImage(image);
                                }
                            }
                        }
                        if (representativePartnerId == 686)
                        {
                            var pdfContentByte2 = pdfStamper.GetOverContent(1);
                            var size = pdfReader.GetPageSize(1);
                            using (var inputImageStream2 = new FileStream(Server.MapPath("/Content/external/rep/logo/" + "fr-logo.png"), FileMode.Open, FileAccess.Read, FileShare.Read))
                            {
                                var imageLogo = Image.GetInstance(inputImageStream2);
                                imageLogo.SetAbsolutePosition(40, 200);
                                imageLogo.ScalePercent(100f);
                                pdfContentByte2.AddImage(imageLogo);
                            }

                        }
                        else
                        {
                            var pdfContentByte2 = pdfStamper.GetOverContent(1);
                            var size = pdfReader.GetPageSize(1);
                            using (var inputImageStream2 = new FileStream(Server.MapPath("/Content/external/rep/newrep/" + "SWFundraising-logo-sellsheet-upt.png"), FileMode.Open, FileAccess.Read, FileShare.Read))
                            {
                                var imageLogo = Image.GetInstance(inputImageStream2);
                                imageLogo.SetAbsolutePosition(10, 215);
                                imageLogo.ScalePercent(30f);
                                pdfContentByte2.AddImage(imageLogo);
                            }

                        }


                  pdfStamper.FormFlattening = true;
               }
               finally
               {
                  if (pdfStamper != null)
                  {
                     pdfStamper.Close();
                  }
               }
            }
         }
         finally
         {
            pdfReader.Close();
         }

            var filename = "temp.pdf";
            //byte[] doc = _docService.GetDocument(zipCode, loanNumber, classification, fileName);
            Response.AppendHeader("Content-Disposition", "inline; filename=\"" + filename + "\"");
            return File(outputFilePath, "application/pdf");
        }



        [Route("sell-sheet-home")]
        public FileResult DownloadSellSheetHome(string representativeId, int representativePartnerId, string representativeName, int representativeExt, string email, string phone, string fileName, string redirect, string outputName)
        {
            var pdf = Server.MapPath("/content/external/rep/newrep/pdf/" + fileName);
            var outputFilePath = Server.MapPath("/content/external/rep/temp.pdf");

            PdfReader pdfReader = null;

            try
            {

                pdfReader = new PdfReader(pdf);

                using (
                        var pdfOutputFile = new FileStream(outputFilePath, FileMode.Create))
                {
                    PdfStamper pdfStamper = null;
                    try
                    {
                        pdfStamper = new PdfStamper(pdfReader, pdfOutputFile);
                        var acroFields = pdfStamper.AcroFields;
                        if (acroFields.Fields.ContainsKey("Name"))
                        {
                            acroFields.SetField("Name", Server.HtmlDecode(representativeName));
                        }
                        if (acroFields.Fields.ContainsKey("Email"))
                        {
                            acroFields.SetField("Email", Server.HtmlDecode(email));
                        }
                        if (acroFields.Fields.ContainsKey("URL"))
                        {
                            acroFields.SetField("URL", Server.HtmlDecode(string.Format("https://www.fundraising.com/{0}", redirect)));
                        }
                        if (acroFields.Fields.ContainsKey("Phone"))
                        {
                            acroFields.SetField("Phone", Server.HtmlDecode(phone));
                        }

                        if (System.IO.File.Exists(Server.MapPath("/Content/external/rep/SmallRepImage/" + representativeExt + ".jpg")))
                        {

                            if (fileName == "all.pdf")
                            {
                                var pdfContentByte = pdfStamper.GetOverContent(2);
                                using (var inputImageStream = new FileStream(Server.MapPath("/Content/external/rep/SmallRepImage/" + representativeExt + ".jpg"), FileMode.Open, FileAccess.Read, FileShare.Read))
                                {
                                    var image = Image.GetInstance(inputImageStream);
                                    image.ScaleAbsolute(47, 70);
                                    image.SetAbsolutePosition(50, 0);

                                    pdfContentByte.AddImage(image);
                                }
                            }
                            else
                            {

                                var pdfContentByte = pdfStamper.GetOverContent(1);
                                using (var inputImageStream = new FileStream(Server.MapPath("/Content/external/rep/SmallRepImage/" + representativeExt + ".jpg"), FileMode.Open, FileAccess.Read, FileShare.Read))
                                {
                                    var image = Image.GetInstance(inputImageStream);
                                    image.ScaleAbsolute(47, 70);
                                    image.SetAbsolutePosition(50, 0);

                                    pdfContentByte.AddImage(image);
                                }
                            }
                        }
                        if (representativePartnerId == 686)
                        {
                            var pdfContentByte2 = pdfStamper.GetOverContent(1);
                            var size = pdfReader.GetPageSize(1);
                            using (var inputImageStream2 = new FileStream(Server.MapPath("/Content/external/rep/logo/" + "fr-logo.png"), FileMode.Open, FileAccess.Read, FileShare.Read))
                            {
                                var imageLogo = Image.GetInstance(inputImageStream2);
                                imageLogo.SetAbsolutePosition(40, 200);
                                imageLogo.ScalePercent(100f);
                                pdfContentByte2.AddImage(imageLogo);
                            }

                        }
                        else
                        {
                            var pdfContentByte2 = pdfStamper.GetOverContent(1);
                            var size = pdfReader.GetPageSize(1);
                            using (var inputImageStream2 = new FileStream(Server.MapPath("/Content/external/rep/newrep/" + "SWFundraising-logo-sellsheet-upt.png"), FileMode.Open, FileAccess.Read, FileShare.Read))
                            {
                                var imageLogo = Image.GetInstance(inputImageStream2);
                                imageLogo.SetAbsolutePosition(10, 215);
                                imageLogo.ScalePercent(30f);
                                pdfContentByte2.AddImage(imageLogo);
                            }

                        }


                        pdfStamper.FormFlattening = true;
                    }
                    finally
                    {
                        if (pdfStamper != null)
                        {
                            pdfStamper.Close();
                        }
                    }
                }
            }
            finally
            {
                pdfReader.Close();
            }

            var filename = "temp.pdf";
            //byte[] doc = _docService.GetDocument(zipCode, loanNumber, classification, fileName);
            Response.AppendHeader("Content-Disposition", "inline; filename=\"" + filename + "\"");
            return File(outputFilePath, "application/pdf");
        }


        [Route("sell-sheet-featured")]
        public FileResult DownloadSellSheetFeatured(string representativeId, int representativePartnerId, string representativeName, int representativeExt, string email, string phone, string fileName, string redirect, string outputName)
        {
            var pdf = Server.MapPath("/content/external/rep/newrep/pdf/" + fileName);
            var outputFilePath = Server.MapPath("/content/external/rep/temp.pdf");

            PdfReader pdfReader = null;

            try
            {

                pdfReader = new PdfReader(pdf);

                using (
                        var pdfOutputFile = new FileStream(outputFilePath, FileMode.Create))
                {
                    PdfStamper pdfStamper = null;
                    try
                    {
                        pdfStamper = new PdfStamper(pdfReader, pdfOutputFile);
                        var acroFields = pdfStamper.AcroFields;
                        if (acroFields.Fields.ContainsKey("Name"))
                        {
                            acroFields.SetField("Name", Server.HtmlDecode(representativeName));
                        }
                        if (acroFields.Fields.ContainsKey("Email"))
                        {
                            acroFields.SetField("Email", Server.HtmlDecode(email));
                        }
                        if (acroFields.Fields.ContainsKey("URL"))
                        {
                            acroFields.SetField("URL", Server.HtmlDecode(string.Format("https://www.fundraising.com/{0}", redirect)));
                        }
                        if (acroFields.Fields.ContainsKey("Phone"))
                        {
                            acroFields.SetField("Phone", Server.HtmlDecode(phone));
                        }

                        if (System.IO.File.Exists(Server.MapPath("/Content/external/rep/SmallRepImage/" + representativeExt + ".jpg")))
                        {

                            if (fileName == "all.pdf")
                            {
                                var pdfContentByte = pdfStamper.GetOverContent(2);
                                using (var inputImageStream = new FileStream(Server.MapPath("/Content/external/rep/SmallRepImage/" + representativeExt + ".jpg"), FileMode.Open, FileAccess.Read, FileShare.Read))
                                {
                                    var image = Image.GetInstance(inputImageStream);
                                    image.ScaleAbsolute(47, 70);
                                    image.SetAbsolutePosition(50, 0);

                                    pdfContentByte.AddImage(image);
                                }
                            }
                            else
                            {

                                var pdfContentByte = pdfStamper.GetOverContent(5);
                                using (var inputImageStream = new FileStream(Server.MapPath("/Content/external/rep/SmallRepImage/" + representativeExt + ".jpg"), FileMode.Open, FileAccess.Read, FileShare.Read))
                                {
                                    var image = Image.GetInstance(inputImageStream);
                                    image.ScaleAbsolute(47, 70);
                                    image.SetAbsolutePosition(50, 0);
                                    pdfContentByte.AddImage(image);
                                }
                            }
                        }
                        if (representativePartnerId == 686)
                        {

                            object size = null;
                            var pdfContentByte = pdfStamper.GetOverContent(1);
                            size = pdfReader.GetPageSize(1);
                            using (var inputImageStream = new FileStream(Server.MapPath("/Content/external/rep/logo/" + "fr-logo.png"), FileMode.Open, FileAccess.Read, FileShare.Read))
                            {
                                var imageLogo = Image.GetInstance(inputImageStream);
                                imageLogo.SetAbsolutePosition(40, 200);
                                imageLogo.ScalePercent(100f);
                                pdfContentByte.AddImage(imageLogo);
                            }

                            var pdfContentByte2 = pdfStamper.GetOverContent(2);
                            size = pdfReader.GetPageSize(1);
                            using (var inputImageStream = new FileStream(Server.MapPath("/Content/external/rep/logo/" + "fr-logo.png"), FileMode.Open, FileAccess.Read, FileShare.Read))
                            {
                                var imageLogo = Image.GetInstance(inputImageStream);
                                imageLogo.SetAbsolutePosition(40, 200);
                                imageLogo.ScalePercent(100f);
                                pdfContentByte2.AddImage(imageLogo);
                            }

                            var pdfContentByte3 = pdfStamper.GetOverContent(3);
                            size = pdfReader.GetPageSize(1);
                            using (var inputImageStream = new FileStream(Server.MapPath("/Content/external/rep/logo/" + "fr-logo.png"), FileMode.Open, FileAccess.Read, FileShare.Read))
                            {
                                var imageLogo = Image.GetInstance(inputImageStream);
                                imageLogo.SetAbsolutePosition(40, 200);
                                imageLogo.ScalePercent(100f);
                                pdfContentByte3.AddImage(imageLogo);
                            }

                            var pdfContentByte4 = pdfStamper.GetOverContent(4);
                            size = pdfReader.GetPageSize(1);
                            using (var inputImageStream = new FileStream(Server.MapPath("/Content/external/rep/logo/" + "fr-logo.png"), FileMode.Open, FileAccess.Read, FileShare.Read))
                            {
                                var imageLogo = Image.GetInstance(inputImageStream);
                                imageLogo.SetAbsolutePosition(40, 200);
                                imageLogo.ScalePercent(100f);
                                pdfContentByte4.AddImage(imageLogo);
                            }

                            var pdfContentByte5 = pdfStamper.GetOverContent(5);
                            size = pdfReader.GetPageSize(1);
                            using (var inputImageStream = new FileStream(Server.MapPath("/Content/external/rep/logo/" + "fr-logo.png"), FileMode.Open, FileAccess.Read, FileShare.Read))
                            {
                                var imageLogo = Image.GetInstance(inputImageStream);
                                imageLogo.SetAbsolutePosition(40, 200);
                                imageLogo.ScalePercent(100f);
                                pdfContentByte5.AddImage(imageLogo);
                            }

                        }
                        else
                        {
                            object size = null;
                            //object pdfContentByte = null;

                            var pdfContentByte1 = pdfStamper.GetOverContent(1);
                            size = pdfReader.GetPageSize(1);
                            using (var inputImageStream = new FileStream(Server.MapPath("/Content/external/rep/newrep/" + "SWFundraising-logo-sellsheet-upt.png"), FileMode.Open, FileAccess.Read, FileShare.Read))
                            {
                                var imageLogo = Image.GetInstance(inputImageStream);
                                imageLogo.SetAbsolutePosition(10, 210);
                                imageLogo.ScalePercent(30f);
                                pdfContentByte1.AddImage(imageLogo);
                            }

                            var pdfContentByte2 = pdfStamper.GetOverContent(2);
                            size = pdfReader.GetPageSize(1);
                            using (var inputImageStream = new FileStream(Server.MapPath("/Content/external/rep/newrep/" + "SWFundraising-logo-sellsheet-upt.png"), FileMode.Open, FileAccess.Read, FileShare.Read))
                            {
                                var imageLogo = Image.GetInstance(inputImageStream);
                                imageLogo.SetAbsolutePosition(10, 210);
                                imageLogo.ScalePercent(30f);
                                pdfContentByte2.AddImage(imageLogo);
                            }


                            var pdfContentByte3 = pdfStamper.GetOverContent(3);
                            size = pdfReader.GetPageSize(1);
                            using (var inputImageStream = new FileStream(Server.MapPath("/Content/external/rep/newrep/" + "SWFundraising-logo-sellsheet-upt.png"), FileMode.Open, FileAccess.Read, FileShare.Read))
                            {
                                var imageLogo = Image.GetInstance(inputImageStream);
                                imageLogo.SetAbsolutePosition(10, 210);
                                imageLogo.ScalePercent(30f);
                                pdfContentByte3.AddImage(imageLogo);
                            }

                            var pdfContentByte4 = pdfStamper.GetOverContent(4);
                            size = pdfReader.GetPageSize(1);
                            using (var inputImageStream = new FileStream(Server.MapPath("/Content/external/rep/newrep/" + "SWFundraising-logo-sellsheet-upt.png"), FileMode.Open, FileAccess.Read, FileShare.Read))
                            {
                                var imageLogo = Image.GetInstance(inputImageStream);
                                imageLogo.SetAbsolutePosition(10, 210);
                                imageLogo.ScalePercent(30f);
                                pdfContentByte4.AddImage(imageLogo);
                            }

                            var pdfContentByte5 = pdfStamper.GetOverContent(5);
                            size = pdfReader.GetPageSize(1);
                            using (var inputImageStream = new FileStream(Server.MapPath("/Content/external/rep/newrep/" + "SWFundraising-logo-sellsheet-upt.png"), FileMode.Open, FileAccess.Read, FileShare.Read))
                            {
                                var imageLogo = Image.GetInstance(inputImageStream);
                                imageLogo.SetAbsolutePosition(10, 210);
                                imageLogo.ScalePercent(30f);
                                pdfContentByte5.AddImage(imageLogo);
                            }




                        }


                        pdfStamper.FormFlattening = true;
                    }
                    finally
                    {
                        if (pdfStamper != null)
                        {
                            pdfStamper.Close();
                        }
                    }
                }
            }
            finally
            {
                pdfReader.Close();
            }

            var filename = "temp.pdf";
            //byte[] doc = _docService.GetDocument(zipCode, loanNumber, classification, fileName);
            Response.AppendHeader("Content-Disposition", "inline; filename=\"" + filename + "\"");
            return File(outputFilePath, "application/pdf");
        }





    }
}