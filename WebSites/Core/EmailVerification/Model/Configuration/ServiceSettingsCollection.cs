using System;
using System.Configuration;

namespace GA.BDC.Core.EmailVerification.Model.Configuration
{
    [ConfigurationCollection(typeof(ServiceSettings), AddItemName=ServiceSettings.ConfigurationElementName, CollectionType=ConfigurationElementCollectionType.BasicMap)]
    public sealed class ServiceSettingsCollection : ConfigurationElementCollection
    {
        public ServiceSettings this[int index]
        {
            get 
            { 
                return (ServiceSettings)BaseGet(index); 
            }
            set 
            {
                if (BaseGet(index) != null)
                {
                    BaseRemoveAt(index);
                }

                BaseAdd(index, value);
            }
        }

        new public ServiceSettings this[string name]
        {
            get 
            { 
                return (ServiceSettings)BaseGet(name); 
            }
            set 
            {
                if (BaseGet(name) != null)
                {
                    BaseRemove(name);
                }

                BaseAdd(value);
            }
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new ServiceSettings();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            ServiceSettings service = (ServiceSettings)element;

            return service.Name;
        }

        public void Add(ServiceSettings element)
        {
            BaseAdd(element);
        }

        public void Add(string name, ServiceProvider provider, int timeout, string optionalSourceID, ServiceEnvironment environment)
        {
            ServiceSettings element = new ServiceSettings(name, provider, timeout, optionalSourceID, environment);

            BaseAdd(element);
        }

        public void Clear()
        {
            BaseClear();
        }

        public int IndexOf(ServiceSettings element)
        {
            return BaseIndexOf(element);
        }

        public void Remove(ServiceSettings element)
        {
            Remove(element.Name);
        }

        public void Remove(string name)
        {
            BaseRemove(name);
        }

        public void RemoveAt(int index)
        {
            BaseRemoveAt(index);
        }
    }
}
