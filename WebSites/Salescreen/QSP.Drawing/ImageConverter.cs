using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Collections.Generic;
using System.Text;

namespace QSP.Drawing
{
    public class ImageConverter
    {
        private ImageConverter()
        {

        }


        public static Bitmap CreateWebPreview(Bitmap sourceImage)
        {
            // Create a copy so we don't overwrite original
            Bitmap tempBmp = new Bitmap(sourceImage);

            // Force to 96 pixels/inch horizontal and vertical for the Web
            tempBmp.SetResolution(96, 96);

            // Make sure we're in RGB mode because browsers do not support CMYK.
            if (IsCMYK(tempBmp))
                tempBmp = ConvertToRgb(tempBmp);

            return tempBmp;
        }

        public static bool IsCMYK(Image sourceImage)
        {
            ImageFlags flags = (ImageFlags)Enum.Parse(typeof(ImageFlags), sourceImage.Flags.ToString());

            // Check bitwise flag for CMYK or YCCK
            return ((flags & ImageFlags.ColorSpaceCmyk) == ImageFlags.ColorSpaceCmyk || 
                (flags & ImageFlags.ColorSpaceYcck) == ImageFlags.ColorSpaceYcck ||
                (flags & ImageFlags.HasAlpha) == ImageFlags.HasAlpha);
        }

        public static Bitmap ConvertToRgb(Bitmap sourceImage)
        {
            // Create a new bitmap specifying RGB of the same dimension
            Bitmap tempBmp = new Bitmap(sourceImage.Width, sourceImage.Height, PixelFormat.Format24bppRgb);
            Graphics graphic = Graphics.FromImage(tempBmp);

            try
            {
                // Draw the source image into our new bitmap
                Rectangle rect = new Rectangle(0, 0, sourceImage.Width, sourceImage.Height);
                graphic.CompositingQuality = CompositingQuality.HighQuality;
                graphic.SmoothingMode = SmoothingMode.HighQuality;
                graphic.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphic.DrawImage(sourceImage, rect);

                return tempBmp;
            }
            finally
            {
                // Dispose Graphics object
                if (graphic != null)
                    graphic.Dispose();
            }
        }
    }
}
