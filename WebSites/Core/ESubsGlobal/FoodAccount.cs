using System;

namespace GA.BDC.Core.ESubsGlobal
{
	/// <summary>
	/// Summary description for FoodAccount.
	/// </summary>
	[Serializable]
	public class FoodAccount
	{
		private int organizationID = int.MinValue;
		private int accountID = int.MinValue;
		private int fulfAccountID = int.MinValue;
		private string accountName ;
		private int businessDivisionID = int.MinValue;
		private string managedPWD;
		private string fmID ;
		private int postalAddressTypeID = int.MinValue;
		private int postalAddressID = int.MinValue;
		private string address1;
		private string address2;
		private string city;
		private string subdivisionCode;
		private string zip;
		private string zip4;
		private string country;
		private string sponsorName;
		private string sponsorEmail;
		private string prefix;
		private int userID;



		public FoodAccount()
		{
			
		}

		#region Public Properties

		public int OrganizationID
		{
			get{ return organizationID;}
			set{ organizationID = value;}
		}

		
		public int AccountID
		{
			get{ return accountID;}
			set{ accountID = value;}
		}

		public int FulfAccountID
		{
			get{ return fulfAccountID;}
			set{ fulfAccountID = value;}
		}

		public string AccountName
		{
			get{ return accountName;}
			set{ accountName = value;}
		}

		public int BusinessDivisionID
		{
			get{ return businessDivisionID;}
			set{ businessDivisionID = value;}
		}

		public string ManagedPWD
		{
			get{ return managedPWD;}
			set{ managedPWD = value;}
		}

		public string FmID
		{
			get{ return fmID;}
			set{ fmID = value;}
		}

		public int  PostalAddressTypeID
		{
			get{ return  postalAddressTypeID;}
			set{  postalAddressTypeID = value;}
		}

		public int  PostalAddressID
		{
			get{ return  postalAddressID;}
			set{  postalAddressID = value;}
		}

		public string Address1
		{
			get{ return address1;}
			set{ address1 = value;}
		}

		public string Address2
		{
			get{ return address2;}
			set{ address2 = value;}
		}

		public string City
		{
			get{ return city;}
			set{ city = value;}
		}

		public string SubdivisionCode
		{
			get{ return subdivisionCode;}
			set{ subdivisionCode = value;}
		}

		public string Zip
		{
			get{ return zip;}
			set{ zip = value;}
		}

		public string Zip4
		{
			get{ return zip4;}
			set{ zip4 = value;}
		}

		public string Country
		{
			get{ return country;}
			set{ country = value;}
		}

		public string SponsorName
		{
			get{ return sponsorName;}
			set{ sponsorName = value;}
		}

		public string SponsorEmail
		{
			get{ return sponsorEmail;}
			set{ sponsorEmail = value;}
		}

		public string Prefix
		{
			get{ return prefix;}
			set{ prefix = value;}
		}

		public int UserID
		{
			get{ return userID;}
			set{ userID = value;}
		}
		#endregion

		public static FoodAccount[] GetOnlineAccountByFoodAccountId( int foodID) 
		{
			DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
			return dbo.GetOnlineAccountByFoodAccountID(foodID);
		}

		public static FoodAccount GetOnlineAccountByAccountId( int foodID)
		{
			DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
			return dbo.GetOnlineAccountByOnlineAccountID(foodID);
		}

		public static FoodAccount GetFoodAccountByAccountId( int foodID)
		{
			DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
			return dbo.GetFoodAccountByAccountId(foodID);
		}

		public void GetReservedAccount()
		{
			DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
			dbo.GetReservedAccount(this);
		}
	}
}
