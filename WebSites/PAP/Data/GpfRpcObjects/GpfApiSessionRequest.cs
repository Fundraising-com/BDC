using System;
using System.Collections.Generic;
using System.Configuration;

namespace GA.BDC.PAP.Data.GpfRpcObjects
{
   [Serializable]
   internal class GpfApiSessionRequest 
   {
      
      internal GpfApiSessionRequest()
      {
         C = "Gpf_Auth_Service";
         M = "authenticate";
        
         fields = new List<String[]> { 
                                       new[]{ "name","value"},
                                       new[]{ "Id",""},
                                       new[]{ "username","marketing@fundraising.com"},
                                       new[]{ "password",ConfigurationManager.AppSettings["PAPPassword"]},
                                       new[]{ "rememberMe","Y"},
                                       new[]{ "language","en-US"},
                                       new[]{ "roleType","M"}
                                    };


      }
      public string C { get; set; }
      public string M { get; set; }
      public List<String[]> fields { get; set; }
      public void SetUsername(string input)
      {
         if (fields != null && fields.Count > 0)
         {
            //fields[0].Value = input;
         }
      }

      public void SetPassword(string input)
      {
         if (fields != null && fields.Count > 1)
         {
            //fields[1].Value = input;
         }
      }
   }

 

}
