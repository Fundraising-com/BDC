using System;
using System.Runtime.Serialization;
using QSPFulfillment.DataAccess.Business;

namespace QSPFulfillment.DataAccess.Common.ActionObject
{
	/// <summary>
	/// Summary description for Magazine.
	/// </summary>
	/// 
	[Serializable]
	public class Premium
	{
		private int iPremiumID = 0;
		private string sPremiumCode = "";
		private int iYear = 0;
		private string sSeason = "";
		private string sEnglishDescription = "";
		private string sFrenchDescription = "";
		private string sIsValid = "";

		public Premium()
		{
		}
		public Premium(int PremiumID, string PremiumCode, int Year, string Season, string EnglishDescription, string FrenchDescription, string IsValid)
		{
			iPremiumID = PremiumID;
			sPremiumCode = PremiumCode;
			iYear = Year;
			sSeason = Season;
			sEnglishDescription = EnglishDescription;
			sFrenchDescription = FrenchDescription;
			sIsValid = IsValid;
		}

		public int PremiumID
		{
			get
			{
				return iPremiumID;
			}
			set 
			{
				iPremiumID = value;
			}
		}
		public string PremiumCode 
		{
			get 
			{
				return sPremiumCode;
			}
			set 
			{
				sPremiumCode = value;
			}
		}
		public int Year
		{
			get
			{
				return iYear;
			}
			set 
			{
				iYear = value;
			}
		}
		public string Season
		{
			get
			{
				return sSeason;
			}
			set 
			{
				sSeason = value;
			}
		}
		public string EnglishDescription
		{
			get
			{
				return sEnglishDescription;
			}
			set 
			{
				sEnglishDescription = value;
			}
		}
		public string FrenchDescription
		{
			get
			{
				return sFrenchDescription;
			}
			set 
			{
				sFrenchDescription = value;
			}
		}
		public string IsValid
		{
			get
			{
				return sIsValid;
			}
			set 
			{
				sIsValid = value;
			}
		}
	}
}
