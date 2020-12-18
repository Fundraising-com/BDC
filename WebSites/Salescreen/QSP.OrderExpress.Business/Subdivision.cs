using System;
using System.Collections;

namespace QSPForm.Business
{
	/// <summary>
	/// Object that represent a Subdivision
	/// </summary>
	[Serializable]
	public class Subdivision
	{
		private string _subdivision_code;
		private string _country_code;
		private string _subdivision_name_1;
		private string _subdivision_name_2;
		private string _subdivision_name_3;
		private string _regional_division;
		private string _subdivision_category;

		public Subdivision(){}
		public Subdivision(string subdivision_code,string country_code, string subdivision_name_1,string subdivision_name_2,string subdivision_name_3,string regional_division,string subdivision_category)
		{
			_subdivision_code = subdivision_code;
			_country_code = country_code;
			_subdivision_name_1 = subdivision_name_1;
			_subdivision_name_2 = subdivision_name_2;
			_subdivision_name_3 = subdivision_name_3;
			_regional_division = regional_division;
			_subdivision_category = subdivision_category;
		}
		public Subdivision(string subdivision_code,string subdivision_name_1)
		{
			_subdivision_code = subdivision_code;
			_country_code = "";
			_subdivision_name_1 = subdivision_name_1;
			_subdivision_name_2 = "";
			_subdivision_name_3 = "";
			_regional_division = "";
			_subdivision_category = "";
		}

		public string SubdivisionCode
		{
			get{return _subdivision_code;}
			set{_subdivision_code = value;}
		}
		public string CountryCode
		{
			get{return _country_code;}
			set{_country_code = value;}
		}
		public string SubdivisionName1
		{
			get{return _subdivision_name_1;}
			set{_subdivision_name_1 =value;}
		}
		public string SubdivisionName2
		{
			get{return _subdivision_name_2;}
			set{_subdivision_name_2 =value;}
		}
		public string SubdivisionName3
		{
			get{return _subdivision_name_3;}
			set{_subdivision_name_3 =value;}
		}
		public string RegionalDivision
		{
			get{return _regional_division;}
			set{_regional_division=value;}
		}
		public string SubdivisionCategory
		{
			get{return _subdivision_category;}
			set{_subdivision_category=value;}
		}
	}
}