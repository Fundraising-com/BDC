using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PdfSharp.Drawing;
using PdfSharp.Pdf.IO;
using PdfSharp.Pdf;
using PdfSharp.Drawing.Layout;
using System.IO;

namespace GA.BDC.Core.ESubsGlobal.DirectMail.Document
{
    public class CreateMailPdf
    {

        static double pixelC(double d)
        {
            d = d + 39;
            return d / 3.96f;
        }


        static XSize DrawText(XGraphics gfx, XPen pen, XBrush brush, string txt, double x, double y, string fontname, double fontSize, bool italic)
        {

            XSize size = gfx.PageSize;
            XGraphicsPath path1 = new XGraphicsPath();
            var options = new XPdfFontOptions(PdfFontEmbedding.Always);

            XFontStyle style = XFontStyle.Regular;
            if (italic)
            {
                style = XFontStyle.Italic;
            }
            var font = new XFont(fontname, fontSize, style, options);

            gfx.DrawString(txt, font, brush, new XPoint(x, y));

            return gfx.MeasureString(txt, font);
        }

        static void DrawTextFormat(XGraphics gfx, XPen pen, XBrush brush, string txt, XRect rect, string fontname, double fontSize)
        {

            XSize size = gfx.PageSize;
            XGraphicsPath path1 = new XGraphicsPath();
            var options = new XPdfFontOptions(PdfFontEmbedding.Always);

            var font = new XFont(fontname, fontSize, XFontStyle.Italic, options);
            XTextFormatter tf = new XTextFormatter(gfx);

            tf.DrawString(txt, font, XBrushes.Black, rect, XStringFormats.TopLeft);
        }

        public string createPdfWeb(string greeting, string completename, string fromName, string teamName, string pathImage, string blocText, string efundbloctext, string url, string code, string address, string city, string state, string zip, string pathBarreCode)
        {
            string pdfTemplatePath = HttpContext.Current.Server.MapPath(@"\Resources\PDF\en-US\");
            string pathTemp = HttpContext.Current.Server.MapPath(@"\temp\");
            if (pathImage != "")
            {
                pathImage = HttpContext.Current.Server.MapPath(pathImage);
            }
            if (pathBarreCode != "")
            {
                pathBarreCode = HttpContext.Current.Server.MapPath(pathBarreCode);
            }

            return create(greeting, completename, fromName, teamName, pathImage, blocText, efundbloctext, url, code, address, city, state, zip, pathBarreCode, pdfTemplatePath, pathImage, pathTemp, 56421, 45530432);
        }

        public string create(string greeting, string completename, string fromName, string teamName, string pathImage, string blocText, string efundbloctext, string url, string code, string address, string city, string state, string zip, string pathBarreCode, string pdfTemplatePath, string imagePath, string pdfTargetFolder, int eventId, int eventParticipationId)
        {
            return this.create(greeting, completename, fromName, teamName, pathImage, blocText, efundbloctext, url, code, address, city, state, zip, pathBarreCode, pdfTemplatePath, imagePath, pdfTargetFolder, eventId, eventParticipationId, true);
        }

        public string create(string greeting, string completename, string fromName, string teamName, string pathImage, string blocText, string efundbloctext, string url, string code, string address, string city, string state, string zip, string pathBarreCode, string pdfTemplatePath, string imagePath, string pdfTargetFolder, int eventId, int eventParticipationId, bool preview)
        {
            int xoffset = 0;
            int yoffset = 0;
            int idxoffset = 0;
            int idyoffset = 0;
            int texttooffset = 0;
            int eventxoffset = -145;
            int epxoffset = -60;
            int teamxoffset = -120;

            //UPDATE SEPT2012: Both Preview and directmail builder app will need the following offset changes
            if (true)
            {
                xoffset = 96;
                yoffset = -5;
                idxoffset = 0;
                idyoffset = -10;
                texttooffset = 200;
                eventxoffset = -47;
                epxoffset = 0;
                teamxoffset = 0;
            }

            XPrivateFontCollection globalFontCollection = XPrivateFontCollection.Global;

            PdfDocument pdfDocument = PdfReader.Open(pdfTemplatePath + "letterDirectMail.pdf");

            XColor strokeColor = XColors.Black;
            XColor fillColor = XColors.Black;
            XGraphics gfxPage1 = XGraphics.FromPdfPage(pdfDocument.Pages[0]);
            XGraphics gfxPage2 = XGraphics.FromPdfPage(pdfDocument.Pages[1]);

            // gfx.MFEH = PdfFontEmbedding.Automatic;

            XPrivateFontCollection privateFonts = new XPrivateFontCollection();
            Uri uri = new Uri(pdfTemplatePath);

            try
            {
                globalFontCollection.Add(uri, "./#Caliban Std");
                globalFontCollection.Add(uri, "./#OCRA");
            }
            catch (Exception ex)
            {

            }

            XPen pen = new XPen(strokeColor, 2);
            XBrush brush = new XSolidBrush(fillColor);

            XSize size = DrawText(gfxPage1, pen, brush, greeting + " ", pixelC(196), pixelC(1011), "Caliban Std", 33, true);
            DrawText(gfxPage1, pen, brush, completename, pixelC(196) + size.Width, pixelC(1011), "Caliban Std", 30, true);
            //DrawText(gfxPage1, pen, brush, teamName, pixelC(294), pixelC(2754), "Caliban Std", 32, true);

            DrawText(gfxPage1, pen, brush, fromName, pixelC(210 + teamxoffset), pixelC(2543), "Caliban Std", 33, true);
            DrawText(gfxPage1, pen, brush, teamName, pixelC(210 + teamxoffset), pixelC(2643), "Caliban Std", 33, true);

            XRect rect = new XRect(pixelC(194), pixelC(1040), pixelC(1750), pixelC(1375));

            blocText = blocText.Replace("\r\n", " ").Replace("\r", " ").Replace("\n", " ");

            //center text
            DrawTextFormat(gfxPage1, pen, brush, blocText + "\r\n\r\n" + efundbloctext, rect, "Caliban Std", 13);

            //  DrawText(gfxPage1, pen, brush, url, pixelC(514), pixelC(2132), "Arial", 12, false);

            //code
            if (!string.IsNullOrEmpty(code))
            {
                code = code.PadLeft(9, '0');
            }
            DrawText(gfxPage1, pen, brush, code, pixelC(860) + idxoffset, pixelC(2068) + idyoffset, "Arial", 13, false);
            //DrawText(gfxPage1, pen, brush, code, pixelC(1550) + idxoffset, pixelC(2162) + idyoffset, "Arial", 13, false);

            PdfRectangle rec = new PdfRectangle(new XPoint(pixelC(535), pixelC(2132)), new XPoint(pixelC(2800), pixelC(2160)));

            pdfDocument.Pages[0].AddWebLink(rec, url);
            XImage image = null;
            XImage image2 = null;

            //TOP IMAGE
            if (pathImage != string.Empty)
            {
                FileInfo fi = new FileInfo(pathImage);
                if (fi.Exists)
                {
                    image = XImage.FromFile(pathImage);
                    gfxPage1.DrawImage(image, pixelC(540 + xoffset), pixelC(135 + yoffset), pixelC(895 - 39 - 13), pixelC(670 - 39 - 13));
                }
                else
                {
                    GA.BDC.Core.Diagnostics.Logger.LogError("CreateMailPdf Can't find image : " + pathImage + " eventParticipant: " + code);
                }
            }

            //BARRECODE
            if (pathBarreCode != string.Empty)
            {
                image2 = XImage.FromFile(pathBarreCode);
                gfxPage1.DrawImage(image2, pixelC(1200), pixelC(3340), pixelC(800), pixelC(169));
            }

            //Pinkbox 
            int posC = 942;

            DrawText(gfxPage1, pen, brush, "#" + eventId, pixelC(posC + 155 + xoffset + eventxoffset), pixelC(3085 + yoffset), "OCRA", 10, false);
            DrawText(gfxPage1, pen, brush, "ID# " + eventParticipationId, pixelC(1500 + 155 + xoffset + epxoffset), pixelC(3085 + yoffset), "OCRA", 10, false);

            rect = new XRect(pixelC(posC + texttooffset), pixelC(3108), pixelC(820), pixelC(800));
            DrawTextFormat(gfxPage1, pen, brush, teamName, rect, "OCRA", 10);

            //address2
            //int posA2X = 1360;
            //int posA2X = 792;
            int posA2X = -140;
            int posAY = 3700;

            DrawText(gfxPage1, pen, brush, completename, pixelC(posA2X + 155 + xoffset), pixelC(posAY + yoffset), "Arial", 12, false);
            DrawText(gfxPage1, pen, brush, address, pixelC(posA2X + 155 + xoffset), pixelC(posAY + 40 + yoffset), "Arial", 12, false);
            DrawText(gfxPage1, pen, brush, city + ", " + state, pixelC(posA2X + 155 + xoffset), pixelC(posAY + 80 + yoffset), "Arial", 12, false);
            DrawText(gfxPage1, pen, brush, zip, pixelC(posA2X + 155 + xoffset), pixelC(posAY + 120 + yoffset), "Arial", 12, false);

            string filename = DateTime.Now.Ticks.ToString() + ".pdf";
            string finalpath = pdfTargetFolder + filename;

            pdfDocument.Save(finalpath);

            if (image != null) { image.Dispose(); }
            if (image2 != null) { image2.Dispose(); }

            pdfDocument.Close();

            return "/temp/" + filename;
        }

        public void Append(string source, string destination)
        {
            if (File.Exists(destination))
            {
                PdfDocument pdfDocumentSource = PdfReader.Open(source, PdfDocumentOpenMode.Import);
                PdfDocument pdfDocumentDestination = PdfReader.Open(destination, PdfDocumentOpenMode.Modify);

                foreach (PdfPage page in pdfDocumentSource.Pages)
                {
                    //PdfPage page = pdfDocumentSource.Pages[i];
                    pdfDocumentDestination.Pages.Add(page);
                    //pdfDocumentDestination.AddPage(page);
                }

                pdfDocumentDestination.Close();
                pdfDocumentDestination.Dispose();
                pdfDocumentDestination.Save(destination);
            }
            else
            {
                File.Copy(source, destination);
            }            
        }
    }

}

/*
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PdfSharp.Drawing;
using PdfSharp.Pdf.IO;
using PdfSharp.Pdf;
using PdfSharp.Drawing.Layout;
using System.IO;

namespace GA.BDC.Core.ESubsGlobal.DirectMail.Document
{
    public class CreateMailPdf
    {

        static double pixelC(double d)
        {
            return d / 3.96f;
        }


        static XSize DrawText(XGraphics gfx, XPen pen, XBrush brush, string txt, double x, double y, string fontname, double fontSize, bool italic)
        {

            XSize size = gfx.PageSize;
            XGraphicsPath path1 = new XGraphicsPath();
            var options = new XPdfFontOptions(PdfFontEmbedding.Always);

            XFontStyle style = XFontStyle.Regular;
            if (italic)
            {
                style = XFontStyle.Italic;
            }
            var font = new XFont(fontname, fontSize, style, options);

            gfx.DrawString(txt, font, brush, new XPoint(x, y));

            return gfx.MeasureString(txt, font);
        }


        static void DrawTextFormat(XGraphics gfx, XPen pen, XBrush brush, string txt, XRect rect, string fontname, double fontSize)
        {

            XSize size = gfx.PageSize;
            XGraphicsPath path1 = new XGraphicsPath();
            var options = new XPdfFontOptions(PdfFontEmbedding.Always);

            var font = new XFont(fontname, fontSize, XFontStyle.Italic, options);
            XTextFormatter tf = new XTextFormatter(gfx);

            tf.DrawString(txt, font, XBrushes.Black, rect, XStringFormats.TopLeft);


        }


        public string createPdfWeb(string greeting, string completename, string fromName, string teamName, string pathImage, string blocText, string efundbloctext, string url, string code, string address, string city, string state, string zip, string pathBarreCode)
        {
            string pdfTemplatePath = HttpContext.Current.Server.MapPath(@"\Resources\PDF\en-US\");
            string pathTemp = HttpContext.Current.Server.MapPath(@"\temp\");
            if (pathImage != "")
            {
                pathImage = HttpContext.Current.Server.MapPath(pathImage);
            }
            if (pathBarreCode != "")
            {
                pathBarreCode = HttpContext.Current.Server.MapPath(pathBarreCode);
            }

            return create(greeting, completename, fromName, teamName, pathImage, blocText, efundbloctext, url, code, address, city, state, zip, pathBarreCode, pdfTemplatePath, pathImage, pathTemp, 56421, 45530432);


        }

        public string create(string greeting, string completename, string fromName, string teamName, string pathImage, string blocText, string efundbloctext, string url, string code, string address, string city, string state, string zip, string pathBarreCode, string pdfTemplatePath, string imagePath, string pdfTargetFolder, int eventId, int eventParticipationId)
        {
            XPrivateFontCollection globalFontCollection = XPrivateFontCollection.Global;

            PdfDocument pdfDocument = PdfReader.Open(pdfTemplatePath+ "letterDirectMail.pdf");


            XColor strokeColor = XColors.Black;
            XColor fillColor = XColors.Black;
            XGraphics gfxPage1 = XGraphics.FromPdfPage(pdfDocument.Pages[0]);
            XGraphics gfxPage2 = XGraphics.FromPdfPage(pdfDocument.Pages[1]);

            // gfx.MFEH = PdfFontEmbedding.Automatic;


            XPrivateFontCollection privateFonts = new XPrivateFontCollection();
            Uri uri = new Uri(pdfTemplatePath);

            try
            {
                globalFontCollection.Add(uri, "./#Caliban Std");
                globalFontCollection.Add(uri, "./#OCRA");
            }
            catch (Exception ex)
            {

            }


            XPen pen = new XPen(strokeColor, 2);
            XBrush brush = new XSolidBrush(fillColor);


            XSize size = DrawText(gfxPage1, pen, brush, greeting + " ", pixelC(316), pixelC(1100), "Caliban Std", 36, true);
            DrawText(gfxPage1, pen, brush, completename, pixelC(316) + size.Width, pixelC(1100), "Caliban Std", 32, true);
            //DrawText(gfxPage1, pen, brush, teamName, pixelC(294), pixelC(2754), "Caliban Std", 32, true);


            DrawText(gfxPage1, pen, brush, fromName, pixelC(314), pixelC(2643), "Caliban Std", 36, true);
            DrawText(gfxPage1, pen, brush, teamName, pixelC(314), pixelC(2743), "Caliban Std", 36, true);


            XRect rect = new XRect(pixelC(314), pixelC(1200), pixelC(1800), pixelC(1375));



            blocText = blocText.Replace("\r\n"," ").Replace("\r", " ").Replace("\n", " ");


            DrawTextFormat(gfxPage1, pen, brush, blocText + "\r\n\r\n" + efundbloctext, rect, "Caliban Std", 13);

          //  DrawText(gfxPage1, pen, brush, url, pixelC(514), pixelC(2132), "Arial", 12, false);


            //code
            code= code.PadLeft(9, '0');
            DrawText(gfxPage1, pen, brush, code, pixelC(1065), pixelC(2068), "Arial", 13, false);
            DrawText(gfxPage1, pen, brush,  code, pixelC(1760), pixelC(2162), "Arial", 13, false);



            PdfRectangle rec = new PdfRectangle(new XPoint(pixelC(535), pixelC(2132)), new XPoint(pixelC(2800), pixelC(2160)));


            pdfDocument.Pages[0].AddWebLink(rec, url);
            XImage image = null;
            XImage image2 = null;

            //TOP IMAGE
            if( pathImage != string.Empty)
            {                    

                FileInfo fi = new FileInfo(pathImage);
                if (fi.Exists)
                {

                    image = XImage.FromFile(pathImage);
                    gfxPage1.DrawImage(image, pixelC(770), pixelC(167), pixelC(892), pixelC(654));
                }
                else
                {
                    efundraising.Diagnostics.Logger.LogError("CreateMailPdf Can't find image : " + pathImage + " eventParticipant: " + code);
                }
            }


            //BARRECODE
            if (pathBarreCode != string.Empty)
            {
                image2 = XImage.FromFile(pathBarreCode);
                gfxPage1.DrawImage(image2, pixelC(1200), pixelC(3340), pixelC(800), pixelC(169));
            }


            //address1
            DrawText(gfxPage1, pen, brush, "#" + eventId, pixelC(1370), pixelC(3085), "OCRA", 10, false);
            DrawText(gfxPage1, pen, brush, "ID# " + eventParticipationId, pixelC(1900), pixelC(3085), "OCRA", 10, false);


            rect = new XRect(pixelC(1370), pixelC(3108), pixelC(820), pixelC(800));
            DrawTextFormat(gfxPage1, pen, brush, teamName , rect, "OCRA", 10);



            //address2
            int posA2X = 1360;

            DrawText(gfxPage1, pen, brush, completename, pixelC(posA2X), pixelC(3610), "Arial", 12, false);
            DrawText(gfxPage1, pen, brush, address, pixelC(posA2X), pixelC(3655), "Arial", 12, false);
            DrawText(gfxPage1, pen, brush, city + ", " + state, pixelC(posA2X), pixelC(3695), "Arial", 12, false);
            DrawText(gfxPage1, pen, brush, zip, pixelC(posA2X), pixelC(3735), "Arial", 12, false);


            string filename = DateTime.Now.Ticks.ToString() + ".pdf";
            string finalpath = pdfTargetFolder + filename;

            pdfDocument.Save(finalpath);

            if (image != null) { image.Dispose();}
            if (image2 != null) { image2.Dispose();}

            pdfDocument.Close();

            return "/temp/" + filename;
        }

        public void Append(string source, string destination)
        {
            if (File.Exists(destination))
            {
                PdfDocument pdfDocumentSource = PdfReader.Open(source, PdfDocumentOpenMode.Import);
                PdfDocument pdfDocumentDestination = PdfReader.Open(destination, PdfDocumentOpenMode.Modify);

                foreach (PdfPage page in pdfDocumentSource.Pages)
                {
                    //PdfPage page = pdfDocumentSource.Pages[i];
                    pdfDocumentDestination.Pages.Add(page);
                    //pdfDocumentDestination.AddPage(page);
                }

                pdfDocumentDestination.Close();
                pdfDocumentDestination.Dispose();
                pdfDocumentDestination.Save(destination);
            }
            else
            {
                File.Copy(source, destination);
            }
            
        }
    }


}

*/