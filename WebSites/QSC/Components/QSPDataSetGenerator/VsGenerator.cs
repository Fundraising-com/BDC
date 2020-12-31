using System;
using System.Runtime.InteropServices;
using System.IO;
using VSInterfaces;

namespace QSP.CommonObjects
{
	/// <summary>
	/// Summary description for BaseGenerator.
	/// </summary>
	abstract public class VsGenerator : IVsSingleFileGenerator
	{
        protected const int       E_FAIL = unchecked((int)0x80004005);
        protected const int       E_NOINTERFACE = unchecked((int)0x80004002);

        protected IVsGeneratorProgress	codeGeneratorProgress;
        protected string					codeFileNameSpace	= String.Empty;
        protected string					codeFilePath		= String.Empty;

        #region Implementation of IVsSingleFileGenerator
        public string GetDefaultExtension()
        {
            return GetExtension();
        }

        public void Generate(string wszInputFilePath, string bstrInputFileContents, string wszDefaultNamespace, out System.IntPtr rgbOutputFileContents, out int pcbOutput, VSInterfaces.IVsGeneratorProgress pGenerateProgress)
        {
            if (bstrInputFileContents == null) 
            {
                throw new ArgumentNullException(bstrInputFileContents);
            }

            codeFilePath = wszInputFilePath;
            codeFileNameSpace = wszDefaultNamespace;
            codeGeneratorProgress = pGenerateProgress;

            byte[] bytes = GenerateCode(wszInputFilePath, bstrInputFileContents);

            if (bytes == null) 
            {
                rgbOutputFileContents = IntPtr.Zero;
                pcbOutput = 0;
            }
            else 
            {
                pcbOutput = bytes.Length;
                rgbOutputFileContents = Marshal.AllocCoTaskMem(pcbOutput);
                Marshal.Copy(bytes, 0, rgbOutputFileContents, pcbOutput);                
            }
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        abstract protected string GetExtension();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <param name="inputfilecontents"></param>
        /// <returns></returns>
        abstract protected byte[] GenerateCode(string input, string inputfilecontents);

        /// <summary>
        /// method to return a byte-array given a Stream
        /// </summary>
        /// <param name="stream">stream to convert to a byte-array</param>
        /// <returns>the stream's contents as a byte-array</returns>
        protected byte[] StreamToBytes(Stream stream) 
        {

            if (stream.Length == 0) 
            {
                return new byte[] { };
            }

            long position = stream.Position;
            stream.Position = 0;
            byte[] bytes = new byte[(int)stream.Length];
            stream.Read(bytes, 0, bytes.Length);
            stream.Position = position;

            return bytes;
        }
    }
}
