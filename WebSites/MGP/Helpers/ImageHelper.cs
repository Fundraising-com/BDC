using System;
using System.IO;
using System.Web;
using System.Linq;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Collections.Generic;

namespace GA.BDC.Web.MGP.Helpers
{
   public class ImageHelper
   {
      public Image Original { get; set; }

      #region Public Methods
      public bool WriteByteArrayToFile(byte[] buff, string fileName)
      {
         bool response = false;
         try
         {
            FileStream fs = new FileStream(fileName, FileMode.Create, FileAccess.ReadWrite);
            BinaryWriter bw = new BinaryWriter(fs);
            bw.Write(buff);
            bw.Close();
            response = true;
         }
         catch (Exception ex)
         {
            Console.WriteLine(ex.Message);
         }
         return response;
      }
      public Byte[] Resize(string mimeType, string path, Size size)
      {
         return Resize(mimeType, extractBuffer(path), size);
      }
      public Byte[] Resize(string mimeType, HttpPostedFileBase file, Size size)
      {
         return Resize(mimeType, extractBuffer(file.InputStream), size);
      }
      public Byte[] Resize(string mimeType, Byte[] p_byteImage, Size size)
      {
         MemoryStream ms = new MemoryStream(p_byteImage);
         Image img = Image.FromStream(ms);
         Original = img;
         ImageCodecInfo jpegCodec = getEncoderInfo(mimeType);
         Image newimage = null;
         if (img.Width != size.Width || img.Height != size.Height)
         {
            newimage = resizeImage(img, size);
         }
         else
         {
            newimage = img;
         }

         MemoryStream ms2 = new MemoryStream();
         if (mimeType.IndexOf("jpeg") > -1)
         {
            newimage.Save(ms2, ImageFormat.Jpeg);
         }
         else if (mimeType.IndexOf("gif") > -1)
         {
            newimage.Save(ms2, ImageFormat.Gif);
         }
         else if (mimeType.IndexOf("png") > -1)
         {
            newimage.Save(ms2, ImageFormat.Png);
         }
         return ms2.GetBuffer();
      }
      #endregion

      #region Static Methods
      public static string GetMonthPersonalizationFolder(string pathGroup, int PersonalizationId, out string monthFolder)
      {
         DateTime tday = DateTime.Today;
         monthFolder = string.Concat("\\", tday.ToString("yyyyMM"));
         var folderPath = string.Concat(pathGroup, monthFolder);
         var di = new DirectoryInfo(folderPath);
         if (!di.Exists)
         {
            try
            {
               Directory.CreateDirectory(folderPath);
               //di.Create();
            }
            catch (Exception exception)
            {
               throw new Exception("Unable to create Folder " + folderPath, exception);

            }
         }
         folderPath = string.Concat(pathGroup, monthFolder, "\\", PersonalizationId.ToString());
         di = new DirectoryInfo(folderPath);
         if (!di.Exists)
         {
            try
            {
               Directory.CreateDirectory(folderPath);
               //di.Create();
            }
            catch (Exception exception)
            {
               throw new Exception("Unable to create Folder " + folderPath, exception);
            }

         }
         return di.FullName;
      }
      #endregion

      #region Private Methods
      private ImageCodecInfo getEncoderInfo(string mimeType)
      {
         // Get image codecs for all image formats
         ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();

         // Find the correct image codec
         for (int i = 0; i < codecs.Length; i++)
            if (codecs[i].MimeType == mimeType)
               return codecs[i];
         return null;
      }
      private byte[] extractBuffer(Stream strm)
      {
         byte[] buffer = new byte[strm.Length];
         strm.Read(buffer, 0, (int)strm.Length);
         strm.Dispose();
         strm.Close();
         return buffer;
      }
      private byte[] extractBuffer(string path)
      {
         // Load file meta data with FileInfo
         FileInfo fileInfo = new FileInfo(path);

         // The byte[] to save the data in
         byte[] buffer = new byte[fileInfo.Length];

         // Load a filestream and put its content into the byte[]
         using (FileStream fs = fileInfo.OpenRead())
         {
            fs.Read(buffer, 0, buffer.Length);
         }
         return buffer;
      }
      private Image resizeImage(Image FullsizeImage, Size size)
      {
         System.Drawing.Image thumbnail = new Bitmap(size.Width, size.Height);
         System.Drawing.Graphics graphic = System.Drawing.Graphics.FromImage(thumbnail);

         graphic.InterpolationMode = InterpolationMode.HighQualityBicubic;
         graphic.SmoothingMode = SmoothingMode.HighQuality;
         graphic.PixelOffsetMode = PixelOffsetMode.HighQuality;
         graphic.CompositingQuality = CompositingQuality.HighQuality;

         // Figure out the ratio
         double ratioX = (double)size.Width / (double)FullsizeImage.Width;
         double ratioY = (double)size.Height / (double)FullsizeImage.Height;
         double ratio = ratioX < ratioY ? ratioX : ratioY;

         // now we can get the new height and width
         int newHeight = Convert.ToInt32(FullsizeImage.Height * ratio);
         int newWidth = Convert.ToInt32(FullsizeImage.Width * ratio);

         // Now calculate the X,Y position of the upper-left corner 
         // (one of these will always be zero)
         int posX = Convert.ToInt32((size.Width - (FullsizeImage.Width * ratio)) / 2);
         int posY = Convert.ToInt32((size.Height - (FullsizeImage.Height * ratio)) / 2);

         graphic.Clear(Color.White); // white padding
         graphic.DrawImage(FullsizeImage, posX, posY, newWidth, newHeight);

         System.Drawing.Imaging.ImageCodecInfo[] info = ImageCodecInfo.GetImageEncoders();
         EncoderParameters encoderParameters;
         encoderParameters = new EncoderParameters(1);
         encoderParameters.Param[0] = new EncoderParameter(Encoder.Quality, 100L);
         return thumbnail;
      }
      #endregion
   }
}