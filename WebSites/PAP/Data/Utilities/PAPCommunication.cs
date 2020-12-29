using System;
using System.Text;
using System.Net;
using System.IO;
using Newtonsoft.Json;


namespace GA.BDC.PAP.Data.Utilities
{
    // ReSharper disable once InconsistentNaming
    public class PAPCommunication : PAPCommnicationBase
    {

        private WebRequest getPAPRequest(string url, string method)
        {
            var output = WebRequest.Create(url);
            output.Method = method;
            output.ContentType = serverMediaType;
            output.Timeout = requestTimeout;
            return output;
        }


        public string CallPapAPI(string input, string url, string method)
        {
            string responseFromServer = string.Empty;
            Stream dataStream = null;
            StreamReader reader = null;
            WebResponse response = null;
            try
            {
                var wr = getPAPRequest(url, method);
                if (!String.IsNullOrEmpty(input))
                {
                    var byteArray = Encoding.UTF8.GetBytes(input);
                    wr.ContentLength = byteArray.Length;
                    dataStream = wr.GetRequestStream();
                    dataStream.Write(byteArray, 0, byteArray.Length);
                    dataStream.Close();
                }
                response = wr.GetResponse();
                dataStream = response.GetResponseStream();
                if (dataStream != null)
                {
                    reader = new StreamReader(dataStream);
                }
                if (reader != null)
                {
                    responseFromServer = reader.ReadToEnd();
                }
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
                if (dataStream != null)
                {
                    dataStream.Close();
                }
                if (response != null)
                {
                    response.Close();
                }
            }
            return responseFromServer;
        }


    }
}
