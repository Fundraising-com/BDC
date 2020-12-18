using System;
using System.Configuration;
using System.Xml;
using System.Xml.Serialization;
using System.Xml.XPath;

namespace QSP.Configuration
{
    /// <summary>
    /// XML configuration section handler.
    /// </summary>
    public class XmlSectionHandler : IConfigurationSectionHandler
    {
        #region Methods
        /// <summary>
        /// Create configuration object based on attribute "configuratorType".
        /// </summary>
        /// <param name="parent">Parent object</param>
        /// <param name="configContext">Configuration context</param>
        /// <param name="section">Xml section to handle.</param>
        /// <returns>New configuration object.</returns>
        public object Create(object parent, object configContext, System.Xml.XmlNode section)
        {
            try
            {
                Object settings = null;
                if (section == null)
                {
                    return settings;
                }

                XPathNavigator navigator = section.CreateNavigator();
                String typeName = (string)navigator.Evaluate("string(@type)");
                Type sectionType = Type.GetType(typeName);

                if (sectionType == null)
                {
                    throw new ConfigurationException("Missing type attribute in element.");
                }

                XmlSerializer xs = new XmlSerializer(sectionType);
                XmlNodeReader reader = new XmlNodeReader(section);
                settings = xs.Deserialize(reader);
                return settings;
            }
            catch (Exception ex)
            {
                throw new ConfigurationException(ex.Message);
            }
        }
        #endregion

    }
}