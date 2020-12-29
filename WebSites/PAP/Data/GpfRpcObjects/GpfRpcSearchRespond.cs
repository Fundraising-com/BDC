using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace GA.BDC.PAP.Data.GpfRpcObjects
{
   [Serializable]
   internal class GpfRpcSearchRespond
   {
      public GpfRpcSearchRespond(Newtonsoft.Json.Linq.JArray input)
      {
         if (input != null && input.First != null)
         {
            if (input.First.First != null && input.First.First.First != null && input.First.First.First.HasValues)
            {
               if (input.First.First.First is JArray && input.First.First.First.Count() > 0)
               {
                  this.Columns = new List<string[]>();
                  foreach (JToken jt in input.First.First.First)
                  {
                     if (jt is JArray && jt.Count() > 0)
                     {
                      List <string> temp = new List<string>();
                      foreach (JValue jv in jt)
                      {
                         if (jv.Type == JTokenType.String)
                         {
                            temp.Add(jv.Value.ToString());
                         }
                      }
                      this.Columns.Add(temp.ToArray());
                     }
                  }
               }
            }
            if (input.First.Last != null && input.First.Last.Last != null)
            {
               if (input.First.Last.Last is JValue)
               {
                  if ((input.First.Last.Last as JValue).Type == JTokenType.Integer)
                  {
                     this.RowCount = Convert.ToInt32( (input.First.Last.Last as JValue).Value);
                  }
               }
            }
         }
      }
      public List<String[]> Columns { get; set; }
      int? RowCount { get; set; }
      public string GetFieldValue(string fieldName)
      {
         if (this.RowCount != null && this.RowCount > 0)
         {
            if (this.Columns != null && Columns.Count > 1)
            {
                               
               int pos = Array.IndexOf(Columns[0], fieldName);
               if (pos >= 0)
               {
                  try
                  {
                     return Columns[1][pos].ToString();
                  }
                  catch
                  {
                     return string.Empty;
                  }
               }
                  
              
            }
         }
         return string.Empty;
      }
   }
}
