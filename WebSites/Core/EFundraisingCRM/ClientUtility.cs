using System;
using System.Collections;




namespace GA.BDC.Core.EFundraisingCRM {
	/// <summary>
	/// Summary description for ClientUtility.
	/// </summary>
	public class ClientUtility : EFundraisingCRMLogic	{
		public ClientUtility() {
			
		}

		public string GenerateClientReport(Client client, ClientAddress billing, ClientAddress ship, SaleCollection sales) {
			try {
				GA.BDC.Core.Reporting.Artefact.SeeThat artefact =
					new GA.BDC.Core.Reporting.Artefact.SeeThat("<b>Unable to insert sale</b>",
					"This reports shows information about a sale that failed to be inserted " +
					"automatically throug the FR web site",
					"");

				System.Xml.XmlDocument xmlDoc = new System.Xml.XmlDocument();
				xmlDoc.LoadXml(client.ToXmlString());
				artefact.AddTable(xmlDoc, true, System.Drawing.Color.LightGray,
					"&nbsp;&nbsp;&nbsp;&nbsp;", "", false, "", false);

				xmlDoc.LoadXml(billing.ToXmlString());
				artefact.AddTable(xmlDoc, true, System.Drawing.Color.LightGray,
					"&nbsp;&nbsp;&nbsp;&nbsp;", "", false, "", false);

				xmlDoc.LoadXml(ship.ToXmlString());
				artefact.AddTable(xmlDoc, true, System.Drawing.Color.LightGray,
					"&nbsp;&nbsp;&nbsp;&nbsp;", "", false, "", false);

				foreach(Sale s in sales) {
					xmlDoc.LoadXml(s.ToXmlString());
					artefact.AddTable(xmlDoc, true, System.Drawing.Color.LightGray,
						"&nbsp;&nbsp;&nbsp;&nbsp;", "", false, "", false);
					foreach(SalesItem si in s.SalesItems) {
						xmlDoc.LoadXml(si.ToXmlString());
						artefact.AddTable(xmlDoc, true, System.Drawing.Color.LightGray,
							"&nbsp;&nbsp;&nbsp;&nbsp;", "", false, "", false);
					}
				}

				return artefact.ToHtmlString();
			} catch (System.Exception ex) {
				return "ERROR GENERATING REPORT: " + ex.Message;
			}
		}

		private SalesItemCollection GetSalesItemCollectionByProductClass(ArrayList salesItems, ScratchBook sc) {
			for(int i=0;i<salesItems.Count;i++) {
				SalesItemCollection sic = (SalesItemCollection)salesItems[i];
				if(sic.ProductClass.ProductClassId == sc.ProductClassId) {
					return sic;
				}
			}
			return null;
		}

		public SalesItemCollection[] GetGroupedItems(ScratchBookCollection scratchBookCollection) {
			ArrayList salesItems = new ArrayList();

			for(int i=0;i<scratchBookCollection.Count;i++) {
				ScratchBook sc = (ScratchBook)scratchBookCollection[i];
				SalesItemCollection sic = GetSalesItemCollectionByProductClass(salesItems, sc);
				if(sic != null) {
					sic += sc;
				} else {
					SalesItemCollection newSalesItemCollection = new SalesItemCollection(new ProductClass(sc.ProductClassId));
					newSalesItemCollection += sc;
					salesItems.Add(newSalesItemCollection);
				}
			}

			return (SalesItemCollection[])salesItems.ToArray(typeof(SalesItemCollection));
		}
	}
}
