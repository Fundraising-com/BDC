using System;
using System.Data;
using GA.BDC.Core.BusinessBase;

using GA.BDC.Core.Database.efundraising;

namespace GA.BDC.Core.efundraisingCore.Geography {
	
	public class CountryCollections : System.Collections.CollectionBase 
	{

		#region public constructors

		public CountryCollections() 
		{
		
		}

		#endregion

		#region IList Members

		public Country this[int index] {
			get{ return (Country)List[index]; }
		}

		public Country this[string pCountryCode] {
			get{
				foreach(Country feCt in this) {
					if(feCt.CountryCode == pCountryCode) 
						return feCt;
				}
				return null;
			}
		}

		public void Insert(int index, Country value) {
			List.Insert(index, value);
		}

		public void Remove(Country value) {
			List.Remove(value);		
		}

		public bool Contains(Country value) {
			return List.Contains(value);
		}

		public int IndexOf(Country value) {
			return List.IndexOf(value);
		}

		public int Add(Country value) {
			return List.Add(value);
		}

		#endregion
		
		#region public static methods

		private static CountryCollections Create() {
			CountryCollections oCtn = new CountryCollections();
			DatabaseObject oEFund = new DatabaseObject();
			foreach(DataRow feRow in oEFund.GetCountry().Rows) {
				oCtn.Add(new Country(feRow["country_code"].ToString(), feRow["country_name"].ToString()));
				//oCtn[0].States = StateCollections.Create(oCtn[0].CountryCode);
			}
			System.Web.HttpContext.Current.Application["CountryCollections"] = oCtn;
			return oCtn;
		}

		public static CountryCollections Load() {
			CountryCollections oCtn = new CountryCollections();
			if(System.Web.HttpContext.Current.Application != null) {
				if(System.Web.HttpContext.Current.Application["CountryCollections"] != null)
					return (CountryCollections)System.Web.HttpContext.Current.Application["CountryCollections"];
				else
					return Create();
			}
			return oCtn;
        }

        #region New Country Codes
        private static Country LoadCountry(DataRow row)
        {
            Country country = new Country();

            // Store database values into our business object
            country.CountryCode = DBValue.ToString(row["country_code"]);
            country.CountryName = DBValue.ToString(row["Country_Name"]);
            //country.DescriptiveInformation = DBValue.ToString(row["descriptive_information"]);
            //country.Display = DBValue.ToInt16(row["display"]);
            country.CountryID = DBValue.ToString(row["Country_ID"]);
            country.CountryCurrencyCode = DBValue.ToString(row["Currency_Code"]);

            // return the filled object
            return country;
        }

        private static Country[] GetCountrys()
        {
            DatabaseObject dbo = new DatabaseObject();
            DataTable dt = dbo.GetCountrys();

            //GetCountrys
            Country[] countrys = null;
            if (dt != null)
            {
                countrys = new Country[dt.Rows.Count];

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    // fill our objects
                    try
                    {
                        countrys[i] = LoadCountry(dt.Rows[i]);
                    }
                    catch (Exception ex)
                    {
                        throw new GA.BDC.Core.Data.Sql.SqlDataException("Unable to fill object using ", ex);
                    }
                }
            }
            return countrys;

        }
        
        private static Country[] CreateCountries()
        {
            Country[] oCtn = GetCountrys();
            System.Web.HttpContext.Current.Application["Country"] = oCtn;
            return oCtn;
        }

        public static Country[] LoadCountries()
        {
            if (System.Web.HttpContext.Current.Application != null)
            {
                if (System.Web.HttpContext.Current.Application["Country"] != null)
                    return (Country[])System.Web.HttpContext.Current.Application["Country"];
                else
                    return CreateCountries();
            }
            else
                return CreateCountries();
        }
        #endregion

        #endregion
    }
}
