//
//	April	07, 2005	-	Louis Turmel	-	First implementation of class
//	April	20, 2005	-	Louis Turmel	-	New Class Sales
//	April	22, 2005	-	Louis Turmel	-	New Namespace name
//

using System;

namespace GA.BDC.Core.efundraisingCore {

	/// <summary>
	/// 
	/// </summary>
	public class Sales {
		
		#region private variables

		private string _DateSales;			
		private string _ConfirmDateSales;	
		private string _ShipDate;			
		private string _ProductClassDesc;	
		private int _TotalProduct;			
		private int _SalesID;				
		private double _TotalAmount;
		
		#endregion

		#region class constructors

		/// <summary>
		/// 
		/// </summary>
		public Sales() {

		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="pDateSales"></param>
		/// <param name="pConfirmDateSales"></param>
		/// <param name="pShipDate"></param>
		/// <param name="pProductClassDesc"></param>
		/// <param name="pTotalProduct"></param>
		/// <param name="pSalesID"></param>
		/// <param name="pTotalAmount"></param>
		public Sales(string pDateSales, string pConfirmDateSales, string pShipDate, string pProductClassDesc,
			int pTotalProduct, int pSalesID, double pTotalAmount) {
			this._DateSales = pDateSales;
			this._ConfirmDateSales = pConfirmDateSales;
			this._ShipDate = pShipDate;
			this._ProductClassDesc = pProductClassDesc;
			this._TotalProduct = pTotalProduct;
			this._SalesID = pSalesID;
			this._TotalAmount = pTotalAmount;
		}

		#endregion

		#region public attributes

		/// <summary>
		/// Get the Total Amount of sale
		/// </summary>
		public double TotalAmount {
			get{ return this._TotalAmount; }
			set{ this._TotalAmount = value; }
		}

		/// <summary>
		/// Get the Sales ID
		/// </summary>
		public int SalesID {
			get{ return this._SalesID; }
			set{ this._SalesID = value; }
		}
		
		/// <summary>
		/// Get the Total of Products
		/// </summary>
		public int TotalProduct {
			get{ return this._TotalProduct; }
			set{ this._TotalProduct = value; }
		}

		/// <summary>
		/// Get the Product class description
		/// </summary>
		public string ProductClassDesc {
			get{ return this._ProductClassDesc; }
			set{ this._ProductClassDesc = value; }
		}

		/// <summary>
		/// Get the Date of shipping
		/// </summary>
		public string ShipDate {
			get{ return this._ShipDate; }
			set{ this._ShipDate = value; }
		}

		/// <summary>
		/// Get the Date of Confirm sale
		/// </summary>
		public string ConfirmDateSales {
			get{ return this._ConfirmDateSales; }
			set{ this._ConfirmDateSales = value; }
		}

		/// <summary>
		/// Get the Date of this Sale
		/// </summary>
		public string DateSales {
			get{ return this._DateSales; }
			set{ this._DateSales = value; }
		}

		#endregion

	}

	/// <summary>
	/// 
	/// </summary>
	/// <remarks>(sealed class) - This class cannot be Inherit from another class</remarks>
	[Serializable()]
	public sealed class Fundraiser : Person {
		
		#region private variables
		/*
			, s.sales_id
			, s.sales_date
			, s.confirmed_date
			, s.actual_ship_date as ship_date 
			, min(pc.description) as product_class_desc
			, s.total_amount 
			, count(si.sales_item_no) as total_product
			
			produit vendu, le montant vendu et, si possible, le nombre de produits vendus?
		*/
		private Sales _Sales;

		private string _Date;
		private string _OrganizationType;
		private string _GroupType;
		private int _GroupSize;
		private string _Organization;
		private string _Interests;
		private string promotionDescription;

		#endregion
		
		#region constructor

		/// <summary>
		/// Default constructor of the class
		/// </summary>
		public Fundraiser() {
		
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="pFirstName">First Name of the Fundraiser</param>
		/// <param name="pLastName">Last Name of the Fundraiser</param>
		/// <param name="pEmail">Email address of the Fundraiser</param>
		/// <param name="pAddress">Corresponding Address of Fundraiser</param>
		/// <param name="pCity">City of Fundraiser</param>
		/// <param name="pState">Residence State</param>
		/// <param name="pZip">Postal Code</param>
		/// <param name="pCountry">Country</param>
		/// <param name="pDayPhone">Phone Number to communicate during the Day</param>
		/// <param name="pEveningPhone">Phone Number to Communicate during the evening</param>
		/// <param name="pDate">Last Date</param>
		/// <param name="pOrganizationType">Type of his Fundraising Organization</param>
		/// <param name="pGroupType">Type of his Fundraising Group</param>
		/// <param name="pGroupSize">Number of members of his Fundraising Campaign</param>
		public Fundraiser(string pFirstName, string pLastName, string pEmail, string pAddress,
			string pCity, string pState, string pZip, string pCountry, string pDayPhone, 
			string pEveningPhone, string pDate, string pOrganizationType, string pGroupType, 
			int pGroupSize, string _Organization, string _Interests, string promotionDescription) {
			
			base.FirstName = pFirstName;
			base.LastName = pLastName;
			base.Email = pEmail;
			base.Address = pAddress;
			base.City = pCity;
			base.State = pState;
			base.Zip = pZip;
			base.Country = pCountry;
			base.DayPhone = pDayPhone;
			base.EveningPhone = pEveningPhone;
			this.Date = pDate;
			this.OrganizationType = pOrganizationType;
			this.GroupType = pGroupType;
			this.GroupSize = pGroupSize;
			this._Organization = _Organization;
			this._Interests = _Interests;
			this.promotionDescription = promotionDescription;
		}


		#endregion

		#region public properties

		/// <summary>
		/// Get - Set the Date
		/// </summary>
		public string Date {
			get{ return this._Date; }
			set{ this._Date = value; }
		}
	
		/// <summary>
		/// Get - Set the Organization Type
		/// </summary>
		public string OrganizationType {
			get{ return this._OrganizationType; }
			set{ this._OrganizationType = value; }
		}

		/// <summary>
		/// Get - Set the Group Type
		/// </summary>
		public string GroupType {
			get{ return this._GroupType; }
			set{ this._GroupType = value; } 
		}
		
		/// <summary>
		/// Get - Set the Group Size
		/// </summary>
		public int GroupSize {
			get{ return this._GroupSize; }
			set{ this._GroupSize = value; }
		}

		
		/// <summary>
		/// Get - Set the sale of Fundraiser
		/// </summary>
		public Sales Sale {
			get{ return this._Sales; }
			set{ this._Sales = value; }
		}

		public string Organization {
			get { return _Organization; }
			set { this._Organization = value; }
		}

		public string Interests {
			get { return _Interests; }
			set { _Interests = value; }
		}

		public string PromotionDescription 
		{
			get { return promotionDescription; }
			set { promotionDescription = value; }
		}

		#endregion
		
	}

	/// <summary>
	/// This class define an Person object
	/// </summary>
	/// <remarks>(abstract class) - This class cannot be Instanciate</remarks>
	public abstract class Person {
		
		#region private variables

		private string _FirstName;
		private string _LastName;
		private string _Email;
		private string _Address;
		private string _City;
		private string _State;
		private string _Zip;
		private string _Country;
		private string _DayPhone;
		private string _EveningPhone;

		#endregion

		#region constructor

		/// <summary>
		/// Default Constructor of Person Class
		/// </summary>
		public Person(){}

		#endregion

		#region public properties

		/// <summary>
		/// Get - Set First Name of Person
		/// </summary>
		public string FirstName {
			get{ return this._FirstName; }
			set{ this._FirstName = value; }
		}
		
		/// <summary>
		/// Get - Set LastName of Person
		/// </summary>
		public string LastName {
			get{ return this._LastName; }
			set{ this._LastName = value; }
		}

		/// <summary>
		/// Get the Full Name of Person based on FirstName and LastName
		/// </summary>
		public string FullName {
			get{ return this._FirstName + " " + this.LastName; }
		}

		/// <summary>
		/// Get the Email of Person
		/// </summary>
		public string Email {
			get{ return this._Email; }
			set{ this._Email = value; }
		}

		/// <summary>
		/// Get the Address of Person
		/// </summary>
		public string Address {
			get{ return this._Address; }
			set{ this._Address = value; }
		}

		/// <summary>
		/// Get the City of Person
		/// </summary>
		public string City {
			get{ return this._City; }
			set{ this._City = value; }
		}

		/// <summary>
		/// Get the State of Person
		/// </summary>
		public string State {
			get{ return this._State; }
			set{ this._State = value; }
		}

		/// <summary>
		/// Get the Zip of Person
		/// </summary>
		public string Zip {
			get{ return this._Zip; }
			set{ this._Zip = value; }
		}

		/// <summary>
		/// Get the Country of Person
		/// </summary>
		public string Country {
			get{ return this._Country; }
			set{ this._Country = value; }
		}

		/// <summary>
		/// Get the DayPhone of Person
		/// </summary>
		public string DayPhone {
			get{ return this._DayPhone; }
			set{ this._DayPhone = value; }
		}

		/// <summary>
		/// Get the Evening Phone of Person
		/// </summary>
		public string EveningPhone {
			get{ return this._EveningPhone; }
			set{ this._EveningPhone = value; }
		}

		#endregion
	}
}
