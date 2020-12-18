using System;
using System.Xml;
using System.Xml.XPath;
using GA.BDC.Core.Diagnostics;


namespace GA.BDC.WEB.ScratchcardWeb.Components.Server.SEO
{
	/// <summary>
	/// Summary description for WebpageHeader.
	/// </summary>
	public class Webpage301Redirect
	{

		private string redirectURL;


		public Webpage301Redirect(string pageURL, string xmlPath)
		{
			
			SetValues(pageURL, xmlPath);
		}

		private void SetValues(string pageURL, string xmlPath)
		{
			redirectURL = GetValueFromXml("NewURL", pageURL, xmlPath);
		}


		private string GetValueFromXml(string node, string pageURL, string xmlPath)
		{
			

			XPathNavigator		nav;
			XPathDocument		docNav;
			XPathNodeIterator	NodeIter;
			String				strExpression;
			string				nodeValue;

			nodeValue ="/";
            try
			{
				// Open the XML.
				docNav = new XPathDocument(xmlPath);

				// Create a navigator to query with XPath.
				nav = docNav.CreateNavigator();
				strExpression = "/Webpage301Redirect/Page/"+node+"[translate(../OldURL, 'ABCDEFGHIJKLMNOPQRSTUVWXYZ','abcdefghijklmnopqrstuvwxyz')='" + ReformatPath(pageURL) +"']";
			
				NodeIter = nav.Select(strExpression);
				//Iterate through the results showing the element value.
				while (NodeIter.MoveNext())
				{
					nodeValue= NodeIter.Current.Value;
				}
			}
			catch(Exception ex)
			{
				Logger.LogError("Error in GetValueFromXMl in WebPage301Redirect", ex);
			}

			return nodeValue;

		}


		private string ReformatPath(string path)
		{
			if(path.IndexOf("?")>0)
				return path.Substring(0,path.IndexOf("?")).Replace("/", "//").ToLower();
			else
				return path.Replace("/", "//").ToLower();
		}

	
		#region Properties

		public string RedirectURL
		{
			get { return redirectURL.Replace("//", "/"); }

		}

		#endregion Properties

	}
}
