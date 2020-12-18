using System;
using PdfSharp.Pdf.IO;
using PdfSharp.Pdf;
using PdfSharp.Drawing.Layout;
using PdfSharp.Drawing;

namespace GA.BDC.Web.MGP.Helpers
{
   // ReSharper disable once InconsistentNaming
   public class PDFSharpReport
   {
      #region Static Methods

      private static double PixelC(double d)
      {
         return d/3.96f;
      }

      private static void DrawText(XGraphics gfx, XBrush brush, string txt, double x, double y, string fontname,
         double fontSize, XFontStyle style)
      {
         var options = new XPdfFontOptions(PdfFontEmbedding.Always);
         var font = new XFont(fontname, fontSize, style, options);
         gfx.DrawString(txt, font, brush, new XPoint(x, y));
      }

      private static void DrawTextFormat(XGraphics gfx, XBrush brush, string txt, XRect rect, string fontname,
         double fontSize, XFontStyle style, XParagraphAlignment align)
      {
         var options = new XPdfFontOptions(PdfFontEmbedding.Always);
         var font = new XFont(fontname, fontSize, style, options);
         var tf = new XTextFormatter(gfx) {Alignment = align};
         tf.DrawString(txt, font, brush, rect, XStringFormats.TopLeft);
      }

      #endregion Static Methods

      #region Public Methods

      public static string CreateFreeKit(string groupName, string profitRate, string redirect, string pdfTemplatePath,
         string pdfTargetFolder)
      {
         var globalFontCollection = XPrivateFontCollection.Global;

         var pdfDocument = PdfReader.Open(pdfTemplatePath + "FreeKit.pdf");

         var fillBlackColor = XColors.Black;
         var fillWhiteColor = XColors.White;

         var gfx = XGraphics.FromPdfPage(pdfDocument.Pages[0]);

         var uri = new Uri(pdfTemplatePath);

         globalFontCollection.Add(uri, "./#Omnes_GirlScouts Regular");
         globalFontCollection.Add(uri, "./#Omnes_GirlScouts Bold");
         globalFontCollection.Add(uri, "./#Omnes_GirlScouts Semibold");


         var brushBlack = new XSolidBrush(fillBlackColor);
         var brushWhite = new XSolidBrush(fillWhiteColor);

         // Header text
         int fontSize;
         var rect = new XRect(PixelC(10), PixelC(270), PixelC(2470), PixelC(230));
         var txtSize = groupName.Length;
         if (txtSize >= 45)
            fontSize = 20;
         else if (txtSize > 40)
            fontSize = 23;
         else if (txtSize > 35)
            fontSize = 25;
         else if (txtSize >= 30)
            fontSize = 27;
         else if (txtSize >= 25)
            fontSize = 30;
         else if (txtSize >= 20)
            fontSize = 35;
         else
            fontSize = 37;
         DrawTextFormat(gfx, brushBlack, groupName, rect, "Arial Black", fontSize, XFontStyle.Regular,
            XParagraphAlignment.Center);

         // Profit rate text
         DrawText(gfx, brushWhite, profitRate, PixelC(222), PixelC(969), "Omnes_GirlScouts Regular", 12,
            XFontStyle.Regular); //Omnes_GirlScouts Semibold

         // Set URL texts
         double firstUrlFontSize = 13D;
         if (redirect.Trim().Length >= 40)
            firstUrlFontSize = 10D;
         else if (redirect.Trim().Length >= 30)
            firstUrlFontSize = 12D;

         DrawText(gfx, brushBlack, redirect, PixelC(1303), PixelC(1264), "Omnes_GirlScouts Regular", firstUrlFontSize,
            XFontStyle.Regular);
         DrawText(gfx, brushWhite, redirect, PixelC(978), PixelC(3112), "Omnes_GirlScouts Regular", 21,
            XFontStyle.Regular);

         var filename = DateTime.Now.Ticks + ".pdf";
         var finalpath = pdfTargetFolder + filename;

         pdfDocument.Save(finalpath);
         pdfDocument.Close();

         return finalpath;
      }

      public static string CreateInviteToHelp(string bodyMsg, string groupName, string redirect, string pdfTemplatePath,
         string pdfTargetFolder)
      {
         var globalFontCollection = XPrivateFontCollection.Global;

         var pdfDocument = PdfReader.Open(pdfTemplatePath + "InviteToHelp.pdf");

         var fillBlackColor = XColors.Black;
         var fillWhiteColor = XColors.White;

         var gfx = XGraphics.FromPdfPage(pdfDocument.Pages[0]);

         var uri = new Uri(pdfTemplatePath);


         try
         {
            globalFontCollection.Add(uri, "./#Omnes_GirlScouts Regular");
            globalFontCollection.Add(uri, "./#Omnes_GirlScouts Bold");
            globalFontCollection.Add(uri, "./#Omnes_GirlScouts Semibold");
         }
	      catch
	      {
		      // ignored
	      }


	      XBrush brushBlack = new XSolidBrush(fillBlackColor);
         XBrush brushWhite = new XSolidBrush(fillWhiteColor);

         // Header text
         int fontSize;
         var rect = new XRect(PixelC(10), PixelC(5), PixelC(2470), PixelC(230));
         int txtSize = groupName.Length;
         if (txtSize >= 45)
            fontSize = 20;
         else if (txtSize > 40)
            fontSize = 23;
         else if (txtSize > 35)
            fontSize = 25;
         else if (txtSize >= 30)
            fontSize = 27;
         else if (txtSize >= 25)
            fontSize = 30;
         else if (txtSize >= 20)
            fontSize = 35;
         else
            fontSize = 40;
         DrawTextFormat(gfx, brushBlack, groupName, rect, "Arial Black", fontSize, XFontStyle.Regular, XParagraphAlignment.Center);

         // set Body message
         int fontSize2 = groupName.Length > 60 ? 13 : 14;
         var rect2 = new XRect(PixelC(210), PixelC(554), PixelC(2079), PixelC(673));
         //gfx.DrawRectangle(XPens.Red, rect2);
         DrawTextFormat(gfx, brushWhite, bodyMsg, rect2, "Omnes_GirlScouts Regular", fontSize2, XFontStyle.Regular, XParagraphAlignment.Left);

         // Set URL texts
         double visitUsUrlFontSize = 30D;
         if (redirect.Trim().Length >= 80)
            visitUsUrlFontSize = 13D;
         else if (redirect.Trim().Length > 70)
            visitUsUrlFontSize = 15D;
         else if (redirect.Trim().Length > 60)
            visitUsUrlFontSize = 19D;
         else if (redirect.Trim().Length > 50)
            visitUsUrlFontSize = 21D;
         else if (redirect.Trim().Length > 40)
            visitUsUrlFontSize = 25D;
         else if (redirect.Trim().Length > 30)
            visitUsUrlFontSize = 27D;
         DrawText(gfx, brushBlack, redirect, 70, 686, "Omnes_GirlScouts Regular", visitUsUrlFontSize, XFontStyle.Regular);

         var filename = DateTime.Now.Ticks + ".pdf";
         var finalpath = pdfTargetFolder + filename;

         pdfDocument.Save(finalpath);
         pdfDocument.Close();

         return finalpath;
      }
      #endregion Public Methods
   }
}