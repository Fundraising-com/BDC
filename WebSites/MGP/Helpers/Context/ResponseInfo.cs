using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GA.BDC.Web.MGP.Helpers.Context
{
    public class ResponseInfo
    {
        public int Status { get; set; }
        public ResponseType Type { get; set; }
        public string ContentType { get; set; }
        public string Body { get; set; }
        public KeyValuePair<string, ModelState>  ModelStateError { get; set; }
    }

    public enum ResponseType
    {
        REGULAR,
        ERROR
    }
}