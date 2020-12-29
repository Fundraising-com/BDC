using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace GA.BDC.PAP.Data.Utilities
{
   public class PAPCommnicationBase
   {
      public PAPCommnicationBase()
      {               
        foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(this))
        {
            DefaultValueAttribute myAttribute = (DefaultValueAttribute)property.Attributes[typeof(DefaultValueAttribute)];

            if (myAttribute != null)
            {
                property.SetValue(this, myAttribute.Value);
            }
        }
      }

     [DefaultValue("application/x-www-form-urlencoded")]
     public string serverMediaType { get; set; }
     [DefaultValue(60000)]
     public int requestTimeout { get; set; }
     [DefaultValue("http://fundraising.postaffiliatepro.com/scripts/server.php")]
     public string serverURL { get; set; }
     [DefaultValue("http://fundraising.postaffiliatepro.com/scripts/track.php")]
     public string trackerUrl { get; set; }
     [DefaultValue("POST")]
     public string serverMethodPost { get; set; }
     [DefaultValue("GET")]
     public string serverMethodGet { get; set; }
   }
}
