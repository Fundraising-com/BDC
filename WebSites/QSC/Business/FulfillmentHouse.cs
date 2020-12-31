using System;

namespace Business
{
	/// <summary>
	/// Summary description for FulfillmentHouse.
	/// </summary>
	public class FulfillmentHouse : QBusinessObject
	{
		#region Class Members
		// Column fields
		protected int IDM=-1;
		[DAL.DataColumn("ID")]
		public int ID
		{
			get{ return this.IDM; }
			set{ this.IDM=value;  }
		}
		protected string nameM;
		[DAL.DataColumn("Name")]
		public string Program_Type
		{
			get{ return this.nameM; }
			set{ this.nameM = value; }
		}
		protected string countryCodeM;
		[DAL.DataColumn("CountryCode")]
		public string CountryCode
		{
			get{ return this.countryCodeM; }
			set{ this.countryCodeM = value; }
		}
		#endregion
//		FulfillmentHouseDataAccess aDAM;

		public FulfillmentHouse()
		{
			
		}
		
		
		/// <summary>
		/// Method:  Exists
		/// Description: Process any order/customer associated with this remit batch.
		/// </summary>
		public bool Exists(int nIDP)
		{
			bool bOk = true;

			return bOk;
		}
		/// <summary>
		/// Method:  ValidateAndSave
		/// </summary>
		override public bool ValidateAndSave()
		{
			bool bOk=true;


			return bOk;

		}
	}
}
