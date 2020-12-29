using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;


using GA.BDC.Data;
using GA.BDC.PAP.Data.Utilities;
namespace GA.BDC.PAP.Data
{
   internal class SaleTrack:PAPCommnicationBase
   {
      public SaleTrack() { }
      public SaleTrack(pap_get_sales_to_be_processedResult input)
      {
         T = input.total_amount.ToString();
         O = input.sales_id.ToString();
         P = input.product_category_code;
         D1 = input.lead_id.ToString();
         D3 = input.client_sequence_code + input.client_id.ToString();
         D4 = input.sales_id.ToString();
         A = input.a_aid;
         B = input.a_bid;
        
      }
      public string VisitorId { get; set; }
      public string AccountId { get; set; }
      public string URL { get; set; }
      public string Referrer { get; set; }
      public string Tracking { get; set; }
      public string IP { get; set; }
      public string UserAgent { get; set; }
      public string AC { get; set; }
      public string T { get; set; }
      public string F { get; set; }
      public string O { get; set; }
      public string P { get; set; }
      public string D1 { get; set; }
      public string D2 { get; set; }
      public string D3 { get; set; }
      public string D4 { get; set; }
      public string D5 { get; set; }
      public string A { get; set; }
      public string C { get; set; }
      public string B { get; set; }
      public string CH { get; set; }
      public string CC { get; set; }
      public string S { get; set; }
      public string CR { get; set; }
      public string CP { get; set; }

      public string GetWebSerializedObject()
      {
         StringBuilder output = new StringBuilder();
         output.Append(trackerUrl);
         output.Append("?");

         Type typ = this.GetType();
         BindingFlags flags = BindingFlags.Public | BindingFlags.Instance;
         PropertyInfo[] pr = typ.GetProperties(flags);
         foreach (PropertyInfo pi in pr)
         {
            output.Append(pi.Name.ToLower());
            output.Append("=".ToString());
            output.Append(pi.GetValue(this, null)??string.Empty);
            output.Append("&".ToString());
         }

         if (pr.Count() > 0)
         {
            output.Remove(output.Length - 1, 1);
         }
         

         return  output.ToString();
      }
   }

   

}
