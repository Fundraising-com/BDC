using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using GA.BDC.Core.ESubsGlobal.DataAccess;

namespace GA.BDC.Core.ESubsGlobal.Common {

	/// <summary>
	/// Summary description for DivisionCode.
	/// </summary>
    [Serializable]
	public class SubDivisionCode {

		#region Structs
		private struct SubDivision
		{
			#region Fields
			private string _code;
			private string _countryCode;
			private string _subdivisionCategory;
			private string _regionalDivision;
			private string _subDivisionNameEN;
			private string _subDivisionNameFR;
			#endregion

			#region Constructors
			public SubDivision(string code, string countryCode, 
								string subDivisionNameEN, string subDivisionNameFR, 
								string regionalDivision, string subDivisionCategory)
			{
				_code = code;
				_countryCode = countryCode;
				_subdivisionCategory = subDivisionCategory;
				_regionalDivision = regionalDivision;
				_subDivisionNameEN = subDivisionNameEN;
				_subDivisionNameFR = subDivisionNameFR;
			}
			#endregion

			#region Properties
			public string Code
			{
				get { return _code; }
				set { _code = value; }
			}

			public string CountryCode
			{
				get { return _countryCode; }
				set { _countryCode = value; }
			}

			public string SubdivisionCategory
			{
				get { return _subdivisionCategory; }
				set { _subdivisionCategory = value; }
			}

			public string RegionalDivision
			{
				get { return _regionalDivision; }
				set { _regionalDivision = value; }
			}

			public string SubDivisionNameEN
			{
				get { return _subDivisionNameEN; }
				set { _subDivisionNameEN = value; }
			}

			public string SubDivisionNameFR
			{
				get { return _subDivisionNameFR; }
				set { _subDivisionNameFR = value; }
			}
			#endregion
		}
		#endregion

		#region Fields
		private static Hashtable _subDivisions = new Hashtable();
		private SubDivision _currentSubDivision;
		#endregion

		#region Constructors

		/// <summary>
		/// Static class to initialize our class.
		/// </summary>
		static SubDivisionCode()
		{
			// Load all subdivisions into static variable 
			// on creation once to reduce memory consumption
            _subDivisions.Add("CA-AB", new SubDivision("CA-AB", "CA", "Alberta", "Alberta", null, "province"));
			_subDivisions.Add("CA-BC", new SubDivision("CA-BC", "CA", "British Columbia", "Colombie-Britannique", null, "province"));
            _subDivisions.Add("CA-MB", new SubDivision("CA-MB", "CA", "Manitoba", "Manitoba", null, "province"));
			_subDivisions.Add("CA-NB", new SubDivision("CA-NB", "CA", "New Brunswick", "Nouveau-Brunswick", null, "province"));
			_subDivisions.Add("CA-NL", new SubDivision("CA-NL", "CA", "Newfoundland and Labrador", "Terre-Neuve et Labrador", null, "province"));
            _subDivisions.Add("CA-NS", new SubDivision("CA-NS", "CA", "Nova Scotia", "Nouvelle-Écosse", null, "province"));
            _subDivisions.Add("CA-NT", new SubDivision("CA-NT", "CA", "Northwest Territories", "Territoires du Nord-Ouest", null, "territory"));
			_subDivisions.Add("CA-NU", new SubDivision("CA-NU", "CA", "Nunavut", "Nunavut", null, "territory"));
			_subDivisions.Add("CA-ON", new SubDivision("CA-ON", "CA", "Ontario", "Ontario", null, "province"));
            _subDivisions.Add("CA-PE", new SubDivision("CA-PE", "CA", "Prince Edward Island", "Île-du-Prince-Édouard", null, "province"));
            _subDivisions.Add("CA-QC", new SubDivision("CA-QC", "CA", "Quebec", "Québec", null, "province"));
			_subDivisions.Add("CA-SK", new SubDivision("CA-SK", "CA", "Saskatchewan", "Saskatchewan", null, "province"));
            _subDivisions.Add("CA-YT", new SubDivision("CA-YT", "CA", "Yukon Territory", "Territoire du Yukon", null, "territory"));
			_subDivisions.Add("US-AA", new SubDivision("US-AA", "US", "Armed Forces Americas", null, null, "us army"));
			_subDivisions.Add("US-AE", new SubDivision("US-AE", "US", "Armed Forces", null, null, "us army"));
			_subDivisions.Add("US-AK", new SubDivision("US-AK", "US", "Alaska", null, null, "state"));
			_subDivisions.Add("US-AL", new SubDivision("US-AL", "US", "Alabama", null, null, "state"));
			_subDivisions.Add("US-AP", new SubDivision("US-AP", "US", "Armed Forces Pacific", null, null, "us army"));
			_subDivisions.Add("US-AR", new SubDivision("US-AR", "US", "Arkansas", null, null, "state"));
			_subDivisions.Add("US-AS", new SubDivision("US-AS", "US", "American Samoa", null, null, "outlying area"));
			_subDivisions.Add("US-AZ", new SubDivision("US-AZ", "US", "Arizona", null, null, "state"));
			_subDivisions.Add("US-CA", new SubDivision("US-CA", "US", "California", null, null, "state"));
			_subDivisions.Add("US-CO", new SubDivision("US-CO", "US", "Colorado", null, null, "state"));
			_subDivisions.Add("US-CT", new SubDivision("US-CT", "US", "Connecticut", null, null, "state"));
			_subDivisions.Add("US-DC", new SubDivision("US-DC", "US", "District of Columbia", null, null, "district"));
			_subDivisions.Add("US-DE", new SubDivision("US-DE", "US", "Delaware", null, null, "state"));
			_subDivisions.Add("US-FL", new SubDivision("US-FL", "US", "Florida", null, null, "state"));
			_subDivisions.Add("US-GA", new SubDivision("US-GA", "US", "Georgia", null, null, "state"));
			_subDivisions.Add("US-GU", new SubDivision("US-GU", "US", "Guam", null, null, "outlying area"));
			_subDivisions.Add("US-HI", new SubDivision("US-HI", "US", "Hawaii", null, null, "state"));
			_subDivisions.Add("US-IA", new SubDivision("US-IA", "US", "Iowa", null, null, "state"));
			_subDivisions.Add("US-ID", new SubDivision("US-ID", "US", "Idaho", null, null, "state"));
			_subDivisions.Add("US-IL", new SubDivision("US-IL", "US", "Illinois", null, null, "state"));
			_subDivisions.Add("US-IN", new SubDivision("US-IN", "US", "Indiana", null, null, "state"));
			_subDivisions.Add("US-KS", new SubDivision("US-KS", "US", "Kansas", null, null, "state"));
			_subDivisions.Add("US-KY", new SubDivision("US-KY", "US", "Kentucky", null, null, "state"));
			_subDivisions.Add("US-LA", new SubDivision("US-LA", "US", "Louisiana", null, null, "state"));
			_subDivisions.Add("US-MA", new SubDivision("US-MA", "US", "Massachusetts", null, null, "state"));
			_subDivisions.Add("US-MD", new SubDivision("US-MD", "US", "Maryland", null, null, "state"));
			_subDivisions.Add("US-ME", new SubDivision("US-ME", "US", "Maine", null, null, "state"));
			_subDivisions.Add("US-MI", new SubDivision("US-MI", "US", "Michigan", null, null, "state"));
			_subDivisions.Add("US-MN", new SubDivision("US-MN", "US", "Minnesota", null, null, "state"));
			_subDivisions.Add("US-MO", new SubDivision("US-MO", "US", "Missouri", null, null, "state"));
			_subDivisions.Add("US-MP", new SubDivision("US-MP", "US", "Northern Mariana Islands", null, null, "outlying area"));
			_subDivisions.Add("US-MS", new SubDivision("US-MS", "US", "Mississippi", null, null, "state"));
			_subDivisions.Add("US-MT", new SubDivision("US-MT", "US", "Montana", null, null, "state"));
			_subDivisions.Add("US-NC", new SubDivision("US-NC", "US", "North Carolina", null, null, "state"));
			_subDivisions.Add("US-ND", new SubDivision("US-ND", "US", "North Dakota", null, null, "state"));
			_subDivisions.Add("US-NE", new SubDivision("US-NE", "US", "Nebraska", null, null, "state"));
			_subDivisions.Add("US-NH", new SubDivision("US-NH", "US", "New Hampshire", null, null, "state"));
			_subDivisions.Add("US-NJ", new SubDivision("US-NJ", "US", "New Jersey", null, null, "state"));
			_subDivisions.Add("US-NM", new SubDivision("US-NM", "US", "New Mexico", null, null, "state"));
			_subDivisions.Add("US-NV", new SubDivision("US-NV", "US", "Nevada", null, null, "state"));
			_subDivisions.Add("US-NY", new SubDivision("US-NY", "US", "New York", null, null, "state"));
			_subDivisions.Add("US-OH", new SubDivision("US-OH", "US", "Ohio", null, null, "state"));
			_subDivisions.Add("US-OK", new SubDivision("US-OK", "US", "Oklahoma", null, null, "state"));
			_subDivisions.Add("US-OR", new SubDivision("US-OR", "US", "Oregon", null, null, "state"));
			_subDivisions.Add("US-PA", new SubDivision("US-PA", "US", "Pennsylvania", null, null, "state"));
			_subDivisions.Add("US-PR", new SubDivision("US-PR", "US", "Puerto Rico", null, null, "outlying area"));
			_subDivisions.Add("US-RI", new SubDivision("US-RI", "US", "Rhode Island", null, null, "state"));
			_subDivisions.Add("US-SC", new SubDivision("US-SC", "US", "South Carolina", null, null, "state"));
			_subDivisions.Add("US-SD", new SubDivision("US-SD", "US", "South Dakota", null, null, "state"));
			_subDivisions.Add("US-TN", new SubDivision("US-TN", "US", "Tennessee", null, null, "state"));
			_subDivisions.Add("US-TX", new SubDivision("US-TX", "US", "Texas", null, null, "state"));
			_subDivisions.Add("US-UM", new SubDivision("US-UM", "US", "United States Minor Outlying Islands", null, null, "outlying area"));
			_subDivisions.Add("US-UT", new SubDivision("US-UT", "US", "Utah", null, null, "state"));
			_subDivisions.Add("US-VA", new SubDivision("US-VA", "US", "Virginia", null, null, "state"));
			_subDivisions.Add("US-VI", new SubDivision("US-VI", "US", "Virgin Islands, U.S.", null, null, "outlying area"));
			_subDivisions.Add("US-VT", new SubDivision("US-VT", "US", "Vermont", null, null, "state"));
			_subDivisions.Add("US-WA", new SubDivision("US-WA", "US", "Washington", null, null, "state"));
			_subDivisions.Add("US-WI", new SubDivision("US-WI", "US", "Wisconsin", null, null, "state"));
			_subDivisions.Add("US-WV", new SubDivision("US-WV", "US", "West Virginia", null, null, "state"));
			_subDivisions.Add("US-WY", new SubDivision("US-WY", "US", "Wyoming", null, null, "state"));
		}

		internal SubDivisionCode() {}

		private SubDivisionCode(string code) {
			_currentSubDivision = (SubDivision) _subDivisions[code.ToUpper()];
		}

		private SubDivisionCode(SubDivision sd)
		{
			_currentSubDivision = sd;
		}
		#endregion

		#region Methods


		
		public static SubDivisionCollections GetSubDivisionCollectionsByCountryCode(string CountryCode) 
		{
			SubDivisionCollections subDivCol = new SubDivisionCollections();
			foreach (string code in _subDivisions.Keys)
			{
				SubDivision sd = (SubDivision) _subDivisions[code];
				if (sd.CountryCode == CountryCode)
				{
					//subDiv.Add(new Common.SubDivision(); 
					Common.SubDivision subDiv = new GA.BDC.Core.ESubsGlobal.Common.SubDivision();
					subDiv.SetSubDivisionCode = new SubDivisionCode();
					subDiv.SetSubDivisionCode.SubDivisionNameEN = sd.SubDivisionNameEN;
					subDiv.SetSubDivisionCode.SubDivisionNameFR = sd.SubDivisionNameFR;
					subDiv.SetSubDivisionCode.Code = sd.Code;
					subDivCol.Add(subDiv);
				}

			}

			return subDivCol;
		}

		public static SubDivisionCode[] GetCASubDivisions() 
		{

            List<SubDivisionCode> divisions = new List<SubDivisionCode>();

			// Look for CA SubDivisions
			foreach (string code in _subDivisions.Keys)
			{
				SubDivision sd = (SubDivision) _subDivisions[code];
				if (sd.CountryCode == "CA")
					divisions.Add(new SubDivisionCode(sd));
			}

			return (SubDivisionCode[]) divisions.ToArray();
		}

		public static SubDivisionCode[] GetUSSubDivisions() {

            List<SubDivisionCode> divisions = new List<SubDivisionCode>();

			// Look for US SubDivisions
			foreach (string code in _subDivisions.Keys)
			{
				SubDivision sd = (SubDivision) _subDivisions[code];
				if (sd.CountryCode == "US")
					divisions.Add(new SubDivisionCode(sd));
			}

			return (SubDivisionCode[]) divisions.ToArray();
		}

		/// <summary>
		/// Get all SubDivisionCode
		/// </summary>
		/// <returns></returns>
		public static SubDivisionCode[] GetAllSubDivisions() {

            List<SubDivisionCode> divisions = new List<SubDivisionCode>();

			foreach (string code in _subDivisions.Keys)
			{
				SubDivision sd = (SubDivision) _subDivisions[code];
				divisions.Add(new SubDivisionCode(sd.Code));
			}
			return (SubDivisionCode[]) divisions.ToArray();
		}

		public static string GetSubDivisionDescriptionFromCode(string code)
		{
			if (_subDivisions[code] == null)
				return "NA";
			return ((SubDivision)_subDivisions[code]).SubDivisionNameEN;
		}

		
		public static string GetSubDivision2CharCode(string code)
		{
			if (code == null || _subDivisions[code] == null)
				return "NA";
			return code.Substring(3,2);
		}

		/// <summary>
		/// Create instance by code.
		/// </summary>
		/// <param name="code"></param>
		/// <returns></returns>
		internal static SubDivisionCode Create(string code) {

			try 
			{
				return new SubDivisionCode(code.ToUpper());
			}
			catch
			{
				return null;
			}
		}

		/// <summary>
		/// Create instance by name.
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		public static SubDivisionCode CreateBySubDivisionName(string name)
		{
			// Remap common subdivision names
			name = name.Replace(".", " ");
			name = name.Replace("-", " ");
			name = name.Replace("/", " ");
			name = name.Replace("\t", " ");
			name = name.Replace("  ", " ");
			name = name.ToLower().Trim();
			
			// Find common matches
			if (name == "b c" || name == "bc" || name == "british columbia")
				name = "british columbia";

			if (name == "québec" || name == "quèbec" ||
				name == "que" || name == "qué" || name == "què" ||
				name == "qc")
				name = "quebec";
						
			if (name == "newfoundland" || name == "labrador")
				name = "newfoundland and labrador";

			if (name == "pei" || name == "p e i")
				name = "prince edward island";

			if (name == "d c" || name == "dc" || name == "district columbia" || 
				name == "washington dc" || name == "washington d c")
				name = "district of columbia";							  

			foreach (string code in _subDivisions.Keys)
			{
				SubDivision sd = (SubDivision) _subDivisions[code];
				if ((sd.SubDivisionNameEN != null && sd.SubDivisionNameEN.ToLower() == name) ||
					(sd.SubDivisionNameFR != null && sd.SubDivisionNameFR.ToLower() == name) ||
					sd.Code.ToLower() == "ca-" + name ||
					sd.Code.ToLower() == "us-" + name)
					return new SubDivisionCode(sd);
			}

			return null;
		}

		public static SortedList GetSubDivisionsByCulture(Culture c)
		{
			ESubsGlobalDatabase dbo = new ESubsGlobalDatabase();
			return dbo.GetSubDivisions(c);
		}

		#endregion

		#region Operators

		public static explicit operator string(SubDivisionCode dc) 
		{

			return dc.Code;
		}

		#endregion

		#region Properties

		public string SubDivisionNameEN
		{
			get {return _currentSubDivision.SubDivisionNameEN;}
			set {_currentSubDivision.SubDivisionNameEN = value;}
		}

		public string SubDivisionNameFR
		{
			get {return _currentSubDivision.SubDivisionNameFR;}
			set {_currentSubDivision.SubDivisionNameFR = value;}
		}

		public string Code
		{
			get {return _currentSubDivision.Code;}
			set { _currentSubDivision.Code =value;}
		}

		/// <summary>
		/// Alberta
		/// </summary>
		public static SubDivisionCode CA_AB {
			get { return new SubDivisionCode("CA-AB"); }
		}


		/// <summary>
		/// British Columbia
		/// </summary>
		public static SubDivisionCode CA_BC {
			get { return new SubDivisionCode("CA-BC"); }
		}


		/// <summary>
		/// Manitoba
		/// </summary>
		public static SubDivisionCode CA_MB {
			get { return new SubDivisionCode("CA-MB"); }
		}


		/// <summary>
		/// New Brunswick
		/// </summary>
		public static SubDivisionCode CA_NB {
			get { return new SubDivisionCode("CA-NB"); }
		}


		/// <summary>
		/// Newfoundland and Labrador
		/// </summary>
		public static SubDivisionCode CA_NL {
			get { return new SubDivisionCode("CA-NL"); }
		}


		/// <summary>
		/// Nova Scotia
		/// </summary>
		public static SubDivisionCode CA_NS {
			get { return new SubDivisionCode("CA-NS"); }
		}


		/// <summary>
		/// Northwest Territories
		/// </summary>
		public static SubDivisionCode CA_NT {
			get { return new SubDivisionCode("CA-NT"); }
		}


		/// <summary>
		/// Nunavut
		/// </summary>
		public static SubDivisionCode CA_NU {
			get { return new SubDivisionCode("CA-NU"); }
		}

		/// <summary>
		/// Ontario
		/// </summary>
		public static SubDivisionCode CA_ON {
			get { return new SubDivisionCode("CA-ON"); }
		}


		/// <summary>
		/// Prince Edward Island
		/// </summary>
		public static SubDivisionCode CA_PE {
			get { return new SubDivisionCode("CA-PE"); }
		}


		/// <summary>
		/// Quebec
		/// </summary>
		public static SubDivisionCode CA_QC {
			get { return new SubDivisionCode("CA-QC"); }
		}


		/// <summary>
		/// Saskatchewan
		/// </summary>
		public static SubDivisionCode CA_SK {
			get { return new SubDivisionCode("CA-SK"); }
		}


		/// <summary>
		/// Yukon Territory
		/// </summary>
		public static SubDivisionCode CA_YT {
			get { return new SubDivisionCode("CA-YT"); }
		}


		/// <summary>
		/// Armed Forces Americas (except Canada)
		/// </summary>
		public static SubDivisionCode US_AA {
			get { return new SubDivisionCode("US-AA"); }
		}


		/// <summary>
		/// Armed Forces (Africa, Canada, Europe, Middle East)
		/// </summary>
		public static SubDivisionCode US_AE {
			get { return new SubDivisionCode("US-AE"); }
		}


		/// <summary>
		/// Alaska
		/// </summary>
		public static SubDivisionCode US_AK {
			get { return new SubDivisionCode("US-AK"); }
		}


		/// <summary>
		/// Alabama
		/// </summary>
		public static SubDivisionCode US_AL {
			get { return new SubDivisionCode("US-AL"); }
		}


		/// <summary>
		/// Armed Forces Pacific
		/// </summary>
		public static SubDivisionCode US_AP {
			get { return new SubDivisionCode("US-AP"); }
		}


		/// <summary>
		/// Arkansas
		/// </summary>
		public static SubDivisionCode US_AR {
			get { return new SubDivisionCode("US-AR"); }
		}


		/// <summary>
		/// American Samoa (see also separate entry under AS)
		/// </summary>
		public static SubDivisionCode US_AS {
			get { return new SubDivisionCode("US-AS"); }
		}


		/// <summary>
		/// Arizona
		/// </summary>
		public static SubDivisionCode US_AZ {
			get { return new SubDivisionCode("US-AZ"); }
		}


		/// <summary>
		/// California
		/// </summary>
		public static SubDivisionCode US_CA {
			get { return new SubDivisionCode("US-CA"); }
		}
		
		/// <summary>
		/// Colorado
		/// </summary>
		public static SubDivisionCode US_CO {
			get { return new SubDivisionCode("US-CO"); }
		}


		/// <summary>
		/// Connecticut
		/// </summary>
		public static SubDivisionCode US_CT {
			get { return new SubDivisionCode("US-CT"); }
		}


		/// <summary>
		/// District of Columbia
		/// </summary>
		public static SubDivisionCode US_DC {
			get { return new SubDivisionCode("US-DC"); }
		}


		/// <summary>
		/// Delaware
		/// </summary>
		public static SubDivisionCode US_DE {
			get { return new SubDivisionCode("US-DE"); }
		}


		/// <summary>
		/// Florida
		/// </summary>
		public static SubDivisionCode US_FL {
			get { return new SubDivisionCode("US-FL"); }
		}


		/// <summary>
		/// Georgia
		/// </summary>
		public static SubDivisionCode US_GA {
			get { return new SubDivisionCode("US-GA"); }
		}


		/// <summary>
		/// Guam (see also separate entry under GU)
		/// </summary>
		public static SubDivisionCode US_GU {
			get { return new SubDivisionCode("US-GU"); }
		}


		/// <summary>
		/// Hawaii
		/// </summary>
		public static SubDivisionCode US_HI {
			get { return new SubDivisionCode("US-HI"); }
		}


		/// <summary>
		/// Iowa
		/// </summary>
		public static SubDivisionCode US_IA {
			get { return new SubDivisionCode("US-IA"); }
		}


		/// <summary>
		/// Idaho
		/// </summary>
		public static SubDivisionCode US_ID {
			get { return new SubDivisionCode("US-ID"); }
		}


		/// <summary>
		/// Illinois
		/// </summary>
		public static SubDivisionCode US_IL {
			get { return new SubDivisionCode("US-IL"); }
		}


		/// <summary>
		/// Indiana
		/// </summary>
		public static SubDivisionCode US_IN {
			get { return new SubDivisionCode("US-IN"); }
		}


		/// <summary>
		/// Kansas
		/// </summary>
		public static SubDivisionCode US_KS {
			get { return new SubDivisionCode("US-KS"); }
		}


		/// <summary>
		/// Kentucky
		/// </summary>
		public static SubDivisionCode US_KY {
			get { return new SubDivisionCode("US-KY"); }
		}


		/// <summary>
		/// Louisiana
		/// </summary>
		public static SubDivisionCode US_LA {
			get { return new SubDivisionCode("US-LA"); }
		}


		/// <summary>
		/// Massachusetts
		/// </summary>
		public static SubDivisionCode US_MA {
			get { return new SubDivisionCode("US-MA"); }
		}


		/// <summary>
		/// Maryland
		/// </summary>
		public static SubDivisionCode US_MD {
			get { return new SubDivisionCode("US-MD"); }
		}


		/// <summary>
		/// Maine
		/// </summary>
		public static SubDivisionCode US_ME {
			get { return new SubDivisionCode("US-ME"); }
		}


		/// <summary>
		/// Michigan
		/// </summary>
		public static SubDivisionCode US_MI {
			get { return new SubDivisionCode("US-MI"); }
		}


		/// <summary>
		/// Minnesota
		/// </summary>
		public static SubDivisionCode US_MN {
			get { return new SubDivisionCode("US-MN"); }
		}


		/// <summary>
		/// Missouri
		/// </summary>
		public static SubDivisionCode US_MO {
			get { return new SubDivisionCode("US-MO"); }
		}


		/// <summary>
		/// Northern Mariana Islands (see also separate entry under MP)
		/// </summary>
		public static SubDivisionCode US_MP {
			get { return new SubDivisionCode("US-MP"); }
		}


		/// <summary>
		/// Mississippi
		/// </summary>
		public static SubDivisionCode US_MS {
			get { return new SubDivisionCode("US-MS"); }
		}


		/// <summary>
		/// Montana
		/// </summary>
		public static SubDivisionCode US_MT {
			get { return new SubDivisionCode("US-MT"); }
		}


		/// <summary>
		/// North Carolina
		/// </summary>
		public static SubDivisionCode US_NC {
			get { return new SubDivisionCode("US-NC"); }
		}


		/// <summary>
		/// North Dakota
		/// </summary>
		public static SubDivisionCode US_ND {
			get { return new SubDivisionCode("US-ND"); }
		}


		/// <summary>
		/// Nebraska
		/// </summary>
		public static SubDivisionCode US_NE {
			get { return new SubDivisionCode("US-NE"); }
		}


		/// <summary>
		/// New Hampshire
		/// </summary>
		public static SubDivisionCode US_NH {
			get { return new SubDivisionCode("US-NH"); }
		}


		/// <summary>
		/// New Jersey
		/// </summary>
		public static SubDivisionCode US_NJ {
			get { return new SubDivisionCode("US-NJ"); }
		}


		/// <summary>
		/// New Mexico
		/// </summary>
		public static SubDivisionCode US_NM {
			get { return new SubDivisionCode("US-NM"); }
		}


		/// <summary>
		/// Nevada
		/// </summary>
		public static SubDivisionCode US_NV {
			get { return new SubDivisionCode("US-NV"); }
		}


		/// <summary>
		/// New York
		/// </summary>
		public static SubDivisionCode US_NY {
			get { return new SubDivisionCode("US-NY"); }
		}


		/// <summary>
		/// Ohio
		/// </summary>
		public static SubDivisionCode US_OH {
			get { return new SubDivisionCode("US-OH"); }
		}


		/// <summary>
		/// Oklahoma
		/// </summary>
		public static SubDivisionCode US_OK {
			get { return new SubDivisionCode("US-OK"); }
		}


		/// <summary>
		/// Oregon
		/// </summary>
		public static SubDivisionCode US_OR {
			get { return new SubDivisionCode("US-OR"); }
		}


		/// <summary>
		/// Pennsylvania
		/// </summary>
		public static SubDivisionCode US_PA {
			get { return new SubDivisionCode("US-PA"); }
		}

		/// <summary>
		/// Puerto Rico (see also separate entry under PR)
		/// </summary>
		public static SubDivisionCode US_PR {
			get { return new SubDivisionCode("US-PR"); }
		}

		/// <summary>
		/// Rhode Island
		/// </summary>
		public static SubDivisionCode US_RI {
			get { return new SubDivisionCode("US-RI"); }
		}

		/// <summary>
		/// South Carolina
		/// </summary>
		public static SubDivisionCode US_SC {
			get { return new SubDivisionCode("US-SC"); }
		}

		/// <summary>
		/// South Dakota
		/// </summary>
		public static SubDivisionCode US_SD {
			get { return new SubDivisionCode("US-SD"); }
		}

		/// <summary>
		/// Tennessee
		/// </summary>
		public static SubDivisionCode US_TN {
			get { return new SubDivisionCode("US-TN"); }
		}

		/// <summary>
		/// Texas
		/// </summary>
		public static SubDivisionCode US_TX {
			get { return new SubDivisionCode("US-TX"); }
		}

		/// <summary>
		/// United States Minor Outlying Islands (see also separate entry under UM)
		/// </summary>
		public static SubDivisionCode US_UM {
			get { return new SubDivisionCode("US-UM"); }
		}

		/// <summary>
		/// Utah
		/// </summary>
		public static SubDivisionCode US_UT {
			get { return new SubDivisionCode("US-UT"); }
		}

		/// <summary>
		/// Virginia
		/// </summary>
		public static SubDivisionCode US_VA {
			get { return new SubDivisionCode("US-VA"); }
		}

		/// <summary>
		/// Virgin Islands, U.S. (see also separate entry under VI)
		/// </summary>
		public static SubDivisionCode US_VI {
			get { return new SubDivisionCode("US-VI"); }
		}

		/// <summary>
		/// Vermont
		/// </summary>
		public static SubDivisionCode US_VT {
			get { return new SubDivisionCode("US-VT"); }
		}

		/// <summary>
		/// Washington
		/// </summary>
		public static SubDivisionCode US_WA {
			get { return new SubDivisionCode("US-WA"); }
		}

		/// <summary>
		/// Wisconsin
		/// </summary>
		public static SubDivisionCode US_WI {
			get { return new SubDivisionCode("US-WI"); }
		}

		/// <summary>
		/// West Virginia
		/// </summary>
		public static SubDivisionCode US_WV {
			get { return new SubDivisionCode("US-WV"); }
		}

		/// <summary>
		/// Wyoming
		/// </summary>
		public static SubDivisionCode US_WY {
			get { return new SubDivisionCode("US-WY"); }
		}
		#endregion
	}
}
