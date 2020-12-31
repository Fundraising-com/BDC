using System;
using System.IO;
using System.Xml;

namespace FileStore
{
	/// <summary>
	/// Summary description for StoreConfiguration.
	/// </summary>
	[Serializable]
	public class StoreConfiguration
	{
		private string sXmlConfigurationPath = System.Configuration.ConfigurationSettings.AppSettings["FileStoreConfigurationPath"];

		#region Xml Nodes Constants

		private const string ROOT_NODE = "StoreConfiguration";
		private const string DIRECTORY_NODE = "Directory";

		#endregion

		private string sDirectory = String.Empty;

		public StoreConfiguration()
		{
			LoadStoreConfiguration();
		}

		public string Directory 
		{
			get 
			{
				return sDirectory;
			}
		}

		private void LoadStoreConfiguration() 
		{
			try 
			{
				XmlDocument oXmlConfiguration = new XmlDocument();
				oXmlConfiguration.Load(sXmlConfigurationPath);

				ReadSettings(oXmlConfiguration);
			} 
			catch
			{
				throw new ConfigurationFailedException();
			}
		}

		private void ReadSettings(XmlDocument xmlConfiguration) 
		{
			ReadDirectorySetting(xmlConfiguration);
		}

		private void ReadDirectorySetting(XmlDocument xmlConfiguration) 
		{
			XmlNode oXmlNode = xmlConfiguration.DocumentElement;

			if(oXmlNode.Name == ROOT_NODE && oXmlNode.ChildNodes.Count > 0) 
			{
				oXmlNode = oXmlNode.ChildNodes[0];
				
				if(oXmlNode.Name == DIRECTORY_NODE && oXmlNode.InnerText != String.Empty) 
				{
					sDirectory = oXmlNode.InnerText;
				} 
				else 
				{
					throw new ConfigurationFailedException();
				}
			} 
			else 
			{
				throw new ConfigurationFailedException();
			}
		}
	}
}
