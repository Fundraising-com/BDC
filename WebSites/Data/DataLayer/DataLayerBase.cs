using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Caching;

namespace GA.BDC.Data.DataLayer
{
   public class DataLayerBase
   {
      static readonly ObjectCache cache = MemoryCache.Default;

      protected static T Get<T>(string key) where T : class
      {
         try
         {
            return cache[key] as T;
         }
         catch (Exception)
         {
            return null;
         }
      }

     protected static void Add<T>(T objectToCache, string key) where T : class
      {
         cache.Add(key, objectToCache, DateTime.Now.AddDays(1));
      }
   }
}
