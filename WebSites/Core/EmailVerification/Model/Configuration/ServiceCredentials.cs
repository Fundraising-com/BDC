using System;
using System.Collections.Specialized;
using System.Configuration;

namespace GA.BDC.Core.EmailVerification.Model.Configuration
{
    public sealed class ServiceCredentials : ConfigurationElement
    {
        public const string ConfigurationElementName = "credentials";

      private readonly NameValueCollection _propertyNameValueCollection;

      public ServiceCredentials()
      {
         _propertyNameValueCollection = new NameValueCollection();
      }

      internal object GetDynamicProperty(string propertyName)
      {
         return _propertyNameValueCollection[propertyName];
      }

      protected override bool OnDeserializeUnrecognizedAttribute(string name, string value)
      {
          _propertyNameValueCollection.Add(name, value);

          return true;
      }
    }
}
