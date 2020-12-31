namespace QSPFulfillment.DataAccess.Business
{
	using System;
	using System.Data;
	using QSPFulfillment.DataAccess.Common.TableDef;
	using QSPFulfillment.DataAccess.Data;
	using QSPFulfillment.DataAccess.Common;
	using QSPFulfillment.DataAccess.Common.ActionObject;

	/// <summary>
	///     This class contains the business rules that are used for a 
	///     collection Day.
	/// </summary>
	public class IncentivesCumulativeBusiness : ProductBusiness
	{
		public IncentivesCumulativeBusiness(Message messageManager) : base(messageManager) { }

		public IncentivesCumulativeBusiness(bool asMessageManager) : base(asMessageManager) { }

		protected override bool ValidateProductCode(string productCode, string season, int year)
		{
			return true;
		}
	}
}