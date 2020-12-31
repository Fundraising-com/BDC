using System;

namespace QSPFulfillment.DataAccess.Common.ActionObject
{
	/// <summary>
	/// Summary description for NewSub.
	/// </summary>
	public class NewSubcription:ActionObjectCommon
	{

		private int iCampaignID;
		private	int iMagPriceInstance;
		private	string sNewRenewal;
		private	float fPrice;
		private	int iOverrideCode;
		private	int iCustomerInstance;
		private float fCatalogPrice;
		private string sFirstName;
		private string sLastName;
		private string sAddress1;
		private string sAddress2;
		private string sCity;
		private string sProvince;
		private string sPostalCode;

	
	

		public NewSubcription(int CampaignID,
			int MagPriceInstance,
			string NewRenewal,
			float Price,
			int OverrideCode,
			int CustomerInstance,
			float CatalogPrice,
			string UserID,
			string FirstName,
			string LastName,
			string Address1,
			string Address2,
			string City,
			string Province,
			string PostalCode)
		{
			this.iCampaignID = CampaignID;
			this.iMagPriceInstance = MagPriceInstance;
			this.sNewRenewal = NewRenewal;
			this.fPrice = Price;
			this.iOverrideCode = OverrideCode;
			this.iCustomerInstance = CustomerInstance;
			this.sUserID = UserID;
			this.fCatalogPrice = CatalogPrice;
			this.sFirstName = FirstName;
			this.sLastName = LastName;
			this.sAddress1 = Address1;
			this.sAddress2 = Address2;
			this.sCity = City;
			this.sProvince = Province;
			this.sPostalCode = PostalCode;
		}
		public int CampaignID
		{
			get
			{
				return this.iCampaignID;
			}
		}
		public int MagPriceInstance
		{
			get
			{
				return this.iMagPriceInstance;
			}
		}
		public string NewRenewal
		{
			get
			{
				return this.sNewRenewal;
			}
		}
		public float Price
		{
			get
			{
				return this.fPrice;
			}
		}
		public int OverrideCode
		{
			get
			{
				return this.iOverrideCode;
			}
		}
		public int CustomerInstance
		{
			get
			{
				return this.iCustomerInstance;
			}
		}
		public float CatalogPrice
		{
			get
			{
				return this.fCatalogPrice;
			}
		}
		public string FirstName 
		{
			get 
			{
				return this.sFirstName;
			}
		}
		public string LastName 
		{
			get 
			{
				return this.sLastName;
			}
		}
		public string Address1 
		{
			get
			{
				return this.sAddress1;
			}
		}
		public string Address2
		{
			get
			{
				return this.sAddress2;
			}
		}
		public string City
		{
			get
			{
				return this.sCity;
			}
		}
		public string Province 
		{
			get
			{
				return this.sProvince;
			}
		}
		public string PostalCode 
		{
			get
			{
				return this.sPostalCode;
			}
		}
	}
}