using System;
using System.Xml;

namespace GA.BDC.Core.EFundraisingCRM {

	public class State: EFundraisingCRMDataObject {

		private string stateCode;
		private string stateName;
		private int avgDeliveryDays;
		private int timeZoneDifference;
		private string countryCode;


		public State() : this(null) { }
		public State(string stateCode) : this(stateCode, null) { }
		public State(string stateCode, string stateName) : this(stateCode, stateName, int.MinValue) { }
		public State(string stateCode, string stateName, int avgDeliveryDays) : this(stateCode, stateName, avgDeliveryDays, int.MinValue) { }
		public State(string stateCode, string stateName, int avgDeliveryDays, int timeZoneDifference) : this(stateCode, stateName, avgDeliveryDays, timeZoneDifference, null) { }
		public State(string stateCode, string stateName, int avgDeliveryDays, int timeZoneDifference, string countryCode) {
			this.stateCode = stateCode;
			this.stateName = stateName;
			this.avgDeliveryDays = avgDeliveryDays;
			this.timeZoneDifference = timeZoneDifference;
			this.countryCode = countryCode;
		}

		#region Static Data
		
		public static State ArmedForcesAmericas {
			get { return new State("AA", "Armed Forces Americas", 0, 0, "US"); }
		}
		
		public static State ArmedForcesEurope_MiddleEast_Canada {
			get { return new State("AE", "Armed Forces Europe/Middle East/Canada", 0, 0, "US"); }
		}
		
		public static State Alaska {
			get { return new State("AK", "Alaska", 6, -4, "US"); }
		}
		
		public static State Alabama {
			get { return new State("AL", "Alabama", 3, -1, "US"); }
		}
		
		public static State Alberta {
			get { return new State("ALB", "Alberta", 4, -2, "CA"); }
		}
		
		public static State ArmedForcesPacific {
			get { return new State("AP", "Armed Forces Pacific", 0, 0, "US"); }
		}
		
		public static State Arkansas {
			get { return new State("AR", "Arkansas", 4, -1, "US"); }
		}
		
		public static State Arizona {
			get { return new State("AZ", "Arizona", 5, -2, "US"); }
		}
		
		public static State BritishColumbia {
			get { return new State("BC", "British Columbia", 6, -3, "US"); }
		}
		
		public static State California {
			get { return new State("CA", "California", 5, -3, "US"); }
		}
		
		public static State Colorado {
			get { return new State("CO", "Colorado", 4, -2, "US"); }
		}
		
		public static State Connecticut {
			get { return new State("CT", "Connecticut", 2, 0, "US"); }
		}
		
		public static State DistrictOfColumbia {
			get { return new State("DC", "District of Columbia", 2, 0, "US"); }
		}
		
		public static State Delaware {
			get { return new State("DE", "Delaware", 2, 0, "US"); }
		}
		
		public static State Florida {
			get { return new State("FL", "Florida", 4, 0, "US"); }
		}
		
		public static State Georgia {
			get { return new State("GA", "Georgia", 3, 0, "US"); }
		}
		
		public static State Hawaii {
			get { return new State("HI", "Hawaii", 6, -5, "US"); }
		}
		
		public static State Iowa {
			get { return new State("IA", "Iowa", 3, -1, "US"); }
		}
		
		public static State Idaho {
			get { return new State("ID", "Idaho", 5, -2, "US"); }
		}
		
		public static State Illinois {
			get { return new State("IL", "Illinois", 3, -1, "US"); }
		}
		
		public static State Indiana {
			get { return new State("IN", "Indiana", 2, -1, "US"); }
		}
		
		public static State Kansas {
			get { return new State("KS", "Kansas", 4, -1, "US"); }
		}
		
		public static State Kentucky {
			get { return new State("KY", "Kentucky", 2, 0, "US"); }
		}
		
		public static State Louisiana {
			get { return new State("LA", "Louisiana", 4, -1, "US"); }
		}
		
		public static State Labrador {
			get { return new State("LB", "Labrador", 5, 2, "CA"); }
		}
		
		public static State Massachusetts {
			get { return new State("MA", "Massachusetts", 2, 0, "US"); }
		}
		
		public static State Manitoba {
			get { return new State("MAN", "Manitoba", 4, -1, "CA"); }
		}
		
		public static State Maryland {
			get { return new State("MD", "Maryland", 2, 0, "US"); }
		}
		
		public static State Maine {
			get { return new State("ME", "Maine", 2, 0, "US"); }
		}
		
		public static State Michigan {
			get { return new State("MI", "Michigan", 2, 0, "US"); }
		}
		
		public static State Minnesota {
			get { return new State("MN", "Minnesota", 5, -1, "US"); }
		}
		
		public static State Missouri {
			get { return new State("MO", "Missouri", 3, -1, "US"); }
		}
		
		public static State Mississippi {
			get { return new State("MS", "Mississippi", 4, -1, "US"); }
		}
		
		public static State Montana {
			get { return new State("MT", "Montana", 5, -2, "US"); }
		}
		
		public static State NotApplicable {
			get { return new State("N/A", "Not Applicable (unknown)", 0, 0, "Unknown"); }
		}
		
		public static State NewBrunswick {
			get { return new State("NB", "New Brunswick", 2, 1, "CA"); }
		}
		
		public static State NorthCarolina {
			get { return new State("NC", "North Carolina", 3, 0, "US"); }
		}
		
		public static State NorthDakota {
			get { return new State("ND", "North Dakota", 4, -1, "US"); }
		}
		
		public static State Nebraska {
			get { return new State("NE", "Nebraska", 4, -1, "US"); }
		}
		
		public static State Newfoundland {
			get { return new State("NF", "Newfoundland", 5, 2, "CA"); }
		}
		
		public static State NewHampshire {
			get { return new State("NH", "New Hampshire", 2, 0, "CA"); }
		}
		
		public static State NewJersey {
			get { return new State("NJ", "New Jersey", 2, 0, "US"); }
		}
		
		public static State NewMexico {
			get { return new State("NM", "New Mexico", 5, -2, "US"); }
		}
		
		public static State NovaScotia {
			get { return new State("NS", "Nova Scotia", 2, 1, "CA"); }
		}
		
		public static State NorthwestTerritories {
			get { return new State("NT", "Northwest Territories", 6, -2, "CA"); }
		}
		
		public static State Nunavut {
			get { return new State("NU", "Nunavut", 6, 0, "CA"); }
		}
		
		public static State Nevada {
			get { return new State("NV", "Nevada", 5, -3, "US"); }
		}
		
		public static State NewYork {
			get { return new State("NY", "New York", 1, 0, "US"); }
		}
		
		public static State Ohio {
			get { return new State("OH", "Ohio", 2, 0, "US"); }
		}
		
		public static State Oklahoma {
			get { return new State("OK", "Oklahoma", 4, -1, "US"); }
		}
		
		public static State Oregon {
			get { return new State("OR", "Oregon", 5, -1, "US"); }
		}
		
		public static State Ontario {
			get { return new State("OT", "Ontario", 2, 0, "CA"); }
		}
		
		public static State Pennsylvania {
			get { return new State("PA", "Pennsylvania", 2, 0, "US"); }
		}
		
		public static State PrinceEdwardIsland {
			get { return new State("PE", "Prince Edward Island", 3, 1, "CA"); }
		}
		
		public static State PuertoRico {
			get { return new State("PR", "Puerto Rico", 2, 0, "PR"); }
		}
		
		public static State Quebec {
			get { return new State("QU", "Québec", 2, 0, "CA"); }
		}
		
		public static State RhodeIsland {
			get { return new State("RI", "Rhode Island", 2, 0, "US"); }
		}
		
		public static State Saskatchewan {
			get { return new State("SA", "Saskatchewan", 4, -1, "US"); }
		}
		
		public static State SouthCarolina {
			get { return new State("SC", "South Carolina", 3, 0, "US"); }
		}
		
		public static State SouthDakota {
			get { return new State("SD", "South Dakota", 4, -1, "US"); }
		}
		public static State Tennessee {
			get { return new State("TN", "Tennessee", 3, 0, "US"); }
		}
		
		public static State Texas {
			get { return new State("TX", "Texas", 5, -1, "US"); }
		}
		
		public static State Utah {
			get { return new State("UT", "Utah", 5, -2, "US"); }
		}
		
		public static State Virginia {
			get { return new State("VA", "Virginia", 2, 0, "US"); }
		}
		
		public static State Vermont {
			get { return new State("VT", "Vermont", 2, 0, "US"); }
		}
		
		public static State Washington {
			get { return new State("WA", "Washington", 5, -3, "US"); }
		}
		
		public static State Wisconsin {
			get { return new State("WI", "Wisconsin", 3, -1, "US"); }
		}
		
		public static State WestVirginia {
			get { return new State("WV", "West Virginia", 2, 0, "US"); }
		}
		
		public static State Wyoming {
			get { return new State("WY", "Wyoming", 5, -2, "US"); }
		}
		
		public static State YukonTerritory {
			get { return new State("YT", "Yukon Territory", 6, -3, "CA"); }
		}
				
		#endregion

		#region XML Methods

		#region Save XML
		public override string GenerateXML() 
		{
			return "<State>\r\n" +
			"	<StateCode>" + System.Web.HttpUtility.HtmlEncode(stateCode) + "</StateCode>\r\n" +
			"	<StateName>" + System.Web.HttpUtility.HtmlEncode(stateName) + "</StateName>\r\n" +
			"	<AvgDeliveryDays>" + avgDeliveryDays + "</AvgDeliveryDays>\r\n" +
			"	<TimeZoneDifference>" + timeZoneDifference + "</TimeZoneDifference>\r\n" +
			"	<CountryCode>" + System.Web.HttpUtility.HtmlEncode(countryCode) + "</CountryCode>\r\n" +
			"</State>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("stateCode")) {
					SetXmlValue(ref stateCode, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("stateName")) {
					SetXmlValue(ref stateName, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("avgDeliveryDays")) {
					SetXmlValue(ref avgDeliveryDays, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("timeZoneDifference")) {
					SetXmlValue(ref timeZoneDifference, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("countryCode")) {
					SetXmlValue(ref countryCode, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static State[] GetStates() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetStates();
		}

		public static State[] GetStatesByCountryCode(string countryCode) 
		{
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetStatesByCountryCode(countryCode);
		}

		/*
		public static State GetStateByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetStateByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertState(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateState(this);
		}*/
		#endregion

		#region Properties
		public string StateCode 
		{
			set { stateCode = value; }
			get { return stateCode; }
		}

		public string StateName {
			set { stateName = value; }
			get { return stateName; }
		}

		public int AvgDeliveryDays {
			set { avgDeliveryDays = value; }
			get { return avgDeliveryDays; }
		}

		public int TimeZoneDifference {
			set { timeZoneDifference = value; }
			get { return timeZoneDifference; }
		}

		public string CountryCode {
			set { countryCode = value; }
			get { return countryCode; }
		}

		#endregion

		#region IComparable Members

		public override int CompareTo(object obj)
		{
			// TODO:  Add State.CompareTo implementation
			State s = obj as State;
			if (s != null)
				return string.Compare(this.StateName, s.StateName);
			return 0;
		}

		#endregion
	}


	public class StateCollection : EFundraisingCRMCollectionBase 
	{
		
		public StateCollection()
		{}

		#region Comparable Methods
		
		// sort the collection list using the default sort argument of
		// the default one.
		public void Sort() 
		{
			// sort the collection
			SortProcess();
		}

		/*// sort the collection list using the specified sort argument
		public void Sort(SaleComparable sortBy) {
			// set the sort by option
			SetSortBy(sortBy);

			// sort the collection
			SortProcess();
		}*/

		// sort the collection list using a custom comparer
		public void Sort(System.Collections.IComparer comparer) 
		{
			SaleCollection copy =
				(SaleCollection)EFundraisingCRMCollectionBase.SortWithComparable(this, comparer);
			ReplaceByCollection(copy);
		}

		#endregion
       
		#region Operators
		public static StateCollection operator +(StateCollection collection1, StateCollection collection2) 
		{
			return (StateCollection)EFundraisingCRMCollectionBase.AddCollection(collection1, collection2);
		}

		public static StateCollection operator +(StateCollection collection, State s) 
		{
			return (StateCollection)EFundraisingCRMCollectionBase.AddItem(collection, s);
		}

		public static StateCollection operator -(StateCollection collection1, StateCollection collection2) 
		{
			return (StateCollection)EFundraisingCRMCollectionBase.RemoveCollection(collection1, collection2);
		}

		public static StateCollection operator -(StateCollection collection, State s) 
		{
			return (StateCollection)EFundraisingCRMCollectionBase.RemoveItem(collection, s);
		}
		#endregion

		#region Properties
		
		#endregion

	}
}
