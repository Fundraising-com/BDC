using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GA.BDC.PAP.Data.Utilities
{
   internal class JsonWrapper
   {
      public static string JsonSingleObjectRequestWrapper(string obj1)
      {
         return "D={\"requests\":["+ obj1+"],\"C\":\"Gpf_Rpc_Server\", \"M\":\"run\"}";
      }

      public static string JsonMultiRequestWrapper(string obj1, string guid)
      {
         return "D={\"C\":\"Gpf_Rpc_Server\", \"M\":\"run\", \"requests\":[" + obj1 + "], \"S\":\"" + guid + "\"}";
      }
      public static string JsonFormRequestWrapper(string obj1, string guid)
      {
         return "D={\"C\":\"Gpf_Rpc_FormRequest\", \"M\":\"run\", \"requests\":[" + obj1 + "], \"S\":\"" + guid + "\"}";
      }
   }
}
