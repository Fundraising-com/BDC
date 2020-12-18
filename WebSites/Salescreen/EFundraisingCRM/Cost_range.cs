using System;
using System.Xml;

namespace efundraising.EFundraisingCRM {

	public class CostRange: EFundraisingCRMDataObject {

		private int costRangeId;
		private int scratchBookId;
		private short serviceTypeId;
		private int minimum;
		private int maximum;
		private float cost;
		private float marginPlan;


		public CostRange() : this(int.MinValue) { }
		public CostRange(int costRangeId) : this(costRangeId, int.MinValue) { }
		public CostRange(int costRangeId, int scratchBookId) : this(costRangeId, scratchBookId, short.MinValue) { }
		public CostRange(int costRangeId, int scratchBookId, short serviceTypeId) : this(costRangeId, scratchBookId, serviceTypeId, int.MinValue) { }
		public CostRange(int costRangeId, int scratchBookId, short serviceTypeId, int minimum) : this(costRangeId, scratchBookId, serviceTypeId, minimum, int.MinValue) { }
		public CostRange(int costRangeId, int scratchBookId, short serviceTypeId, int minimum, int maximum) : this(costRangeId, scratchBookId, serviceTypeId, minimum, maximum, float.MinValue) { }
		public CostRange(int costRangeId, int scratchBookId, short serviceTypeId, int minimum, int maximum, float cost) : this(costRangeId, scratchBookId, serviceTypeId, minimum, maximum, cost, float.MinValue) { }
		public CostRange(int costRangeId, int scratchBookId, short serviceTypeId, int minimum, int maximum, float cost, float marginPlan) {
			this.costRangeId = costRangeId;
			this.scratchBookId = scratchBookId;
			this.serviceTypeId = serviceTypeId;
			this.minimum = minimum;
			this.maximum = maximum;
			this.cost = cost;
			this.marginPlan = marginPlan;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<CostRange>\r\n" +
			"	<CostRangeId>" + costRangeId + "</CostRangeId>\r\n" +
			"	<ScratchBookId>" + scratchBookId + "</ScratchBookId>\r\n" +
			"	<ServiceTypeId>" + serviceTypeId + "</ServiceTypeId>\r\n" +
			"	<Minimum>" + minimum + "</Minimum>\r\n" +
			"	<Maximum>" + maximum + "</Maximum>\r\n" +
			"	<Cost>" + cost + "</Cost>\r\n" +
			"	<MarginPlan>" + marginPlan + "</MarginPlan>\r\n" +
			"</CostRange>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("costRangeId")) {
					SetXmlValue(ref costRangeId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("scratchBookId")) {
					SetXmlValue(ref scratchBookId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("serviceTypeId")) {
					SetXmlValue(ref serviceTypeId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("minimum")) {
					SetXmlValue(ref minimum, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("maximum")) {
					SetXmlValue(ref maximum, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("cost")) {
					SetXmlValue(ref cost, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("marginPlan")) {
					SetXmlValue(ref marginPlan, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static CostRange[] GetCostRanges() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetCostRanges();
		}

		public static CostRange GetCostRangeByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetCostRangeByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertCostRange(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateCostRange(this);
		}
		#endregion

		#region Properties
		public int CostRangeId {
			set { costRangeId = value; }
			get { return costRangeId; }
		}

		public int ScratchBookId {
			set { scratchBookId = value; }
			get { return scratchBookId; }
		}

		public short ServiceTypeId {
			set { serviceTypeId = value; }
			get { return serviceTypeId; }
		}

		public int Minimum {
			set { minimum = value; }
			get { return minimum; }
		}

		public int Maximum {
			set { maximum = value; }
			get { return maximum; }
		}

		public float Cost {
			set { cost = value; }
			get { return cost; }
		}

		public float MarginPlan {
			set { marginPlan = value; }
			get { return marginPlan; }
		}

		#endregion
	}
}
