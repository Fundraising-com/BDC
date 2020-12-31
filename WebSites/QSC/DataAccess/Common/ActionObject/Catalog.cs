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
	public class Catalog
	{
		private int iCatalogID = 0;
		private string sCatalogCode = "";
		private string sName = "";
		private int iYear = 0;
		private string sSeason = "";
		private CatalogType ctType = CatalogType.None;
		private string sLanguage = "";
		private CatalogStatus csStatus = CatalogStatus.None;
		private string sIsReplacement = "";


		public Catalog()
		{
		}
		public Catalog(int CatalogID, string CatalogCode, string Name, int Year, string Season, CatalogType Type, string Language, CatalogStatus Status, string IsReplacement)
		{
			iCatalogID = CatalogID;
			sCatalogCode = CatalogCode;
			sName = Name;
			iYear = Year;
			sSeason = Season;
			ctType = Type;
			sLanguage = Language;
			csStatus = Status;
			sIsReplacement = IsReplacement;
		}


		public int CatalogID
		{
			get
			{
				return iCatalogID;
			}
			set 
			{
				iCatalogID = value;
			}
		}
		public string CatalogCode
		{
			get
			{
				return sCatalogCode;
			}
			set 
			{
				sCatalogCode = value;
			}
		}								  
		public string Name
		{
			get
			{
				return sName;
			}
			set 
			{
				sName = value;
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
		public CatalogType Type
		{
			get
			{
				return ctType;
			}
			set 
			{
				ctType = value;
			}
		}
		public string Language
		{
			get
			{
				return sLanguage;
			}
			set 
			{
				sLanguage = value;
			}
		}
		public CatalogStatus Status
		{
			get
			{
				return csStatus;
			}
			set 
			{
				csStatus = value;
			}
		}
		public string IsReplacement
		{
			get
			{
				return sIsReplacement;
			}
			set 
			{
				sIsReplacement = value;
			}
		}
	}
}
