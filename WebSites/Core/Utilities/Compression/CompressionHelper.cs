using System;
using System.Text;
using System.IO;
using ICSharpCode.SharpZipLib;

namespace GA.BDC.Core.Utilities.Compression
{
	/// <summary>
	/// Summary description for CompressionHelper.
	/// </summary>
	public enum CompressionType
	{
		GZip,
		BZip2,
		Zip
	}

 

	public class Compression
	{

		public static CompressionType CompressionProvider = CompressionType.GZip;
		private static Stream OutputStream(Stream inputStream)
		{

			switch(CompressionProvider)
			{
				case CompressionType.BZip2:
					return new ICSharpCode.SharpZipLib.BZip2.BZip2OutputStream(inputStream);
				case CompressionType.GZip:
					return new ICSharpCode.SharpZipLib.GZip.GZipOutputStream(inputStream);
				case CompressionType.Zip:
					return new ICSharpCode.SharpZipLib.Zip.ZipOutputStream(inputStream);
				default:
					return new ICSharpCode.SharpZipLib.GZip.GZipOutputStream(inputStream);                                               

			}

		}

		private static Stream InputStream(Stream inputStream)
		{
			switch(CompressionProvider)
			{
				case CompressionType.BZip2:
					return new ICSharpCode.SharpZipLib.BZip2.BZip2InputStream(inputStream);
				case CompressionType.GZip:
					return new ICSharpCode.SharpZipLib.GZip.GZipInputStream(inputStream);
				case CompressionType.Zip:
					return new ICSharpCode.SharpZipLib.Zip.ZipInputStream(inputStream);
				default:
					return new ICSharpCode.SharpZipLib.GZip.GZipInputStream(inputStream);                                                                        
			}

		}

            

		public static byte[] Compress(byte[] bytesToCompress)
		{
			MemoryStream ms = new MemoryStream();
			Stream s = OutputStream(ms);
			s.Write(bytesToCompress,0, bytesToCompress.Length);
			s.Close();
			return  ms.ToArray();
		}

 

		public static string Compress(string stringToCompress)
		{
			byte[] compressedData = CompressToByte(stringToCompress);
			string strOut = Convert.ToBase64String(compressedData);
			return strOut;
		}

		public static  byte[] CompressToByte(string stringToCompress)
		{
			byte[] bytData = Encoding.Unicode.GetBytes(stringToCompress);
			return Compress(bytData);;
		}

 

		public static string DeCompress(string stringToDecompress)
		{
			string outString = string.Empty;
			if (stringToDecompress == null)
			{
				throw new ArgumentNullException("stringToDecompress","You tried to use an empty string");
			}

			try
			{
				byte[] inArr = Convert.FromBase64String(stringToDecompress.Trim());
				outString = System.Text.Encoding.Unicode.GetString(DeCompress(inArr));
			}
			catch (NullReferenceException  nEx)
			{
				return nEx.Message;
			}
			return outString;
		}


 

		public static  byte[]  DeCompress(byte[] bytesToDecompress)
		{
			byte[] writeData = new byte[4096];
			Stream s2 = InputStream(new MemoryStream(bytesToDecompress));
			MemoryStream outStream = new MemoryStream();
			while(true)
			{
				int size = s2.Read(writeData,0,writeData.Length);
				if(size>0)
				{
					outStream.Write(writeData,0,size);
				}
				else
				{
					break;
				}

			}
			s2.Close();
			byte[] outArr = outStream.ToArray();
			outStream.Close();
			return outArr;

		}
	}
}
