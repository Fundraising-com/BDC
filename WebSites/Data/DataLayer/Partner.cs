using System.Collections.Generic;
using System.Linq;

namespace GA.BDC.Data.DataLayer
{
   public class Partner
   {
       /// <summary>
       /// Returns all the Partners that have a PAP attribute
       /// </summary>
       /// <param name="efrCommonDataContext"></param>
       /// <returns></returns>
      public static List<partner> GetPapParners(EFRCommonDataContext efrCommonDataContext)
      {
         return (from p in efrCommonDataContext.partners 
                 join pav in efrCommonDataContext.partner_attribute_values on p.partner_id equals pav.partner_id 
                 where pav.partner_attribute_id == 12 
                 && p.is_active 
                 select p).ToList<partner>(); 
      }
       /// <summary>
       /// Returns the PAP Attribute values for all the Partners
       /// </summary>
       /// <param name="efrCommonDataContext"></param>
       /// <returns></returns>
      public static List<partner_attribute_value> GetPapParnerAttributeValues(EFRCommonDataContext efrCommonDataContext)
      {
         return (from p in efrCommonDataContext.partners 
                 join pav in efrCommonDataContext.partner_attribute_values on p.partner_id equals pav.partner_id 
                 where pav.partner_attribute_id == 12 
                 && p.is_active 
                 select pav).ToList<partner_attribute_value>();
      }
   }
}
