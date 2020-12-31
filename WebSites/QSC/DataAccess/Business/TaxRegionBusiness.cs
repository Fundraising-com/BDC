namespace QSPFulfillment.DataAccess.Business
{
	using System;
	using System.Data;
	using QSPFulfillment.DataAccess.Common.TableDef;
	using QSPFulfillment.DataAccess.Data;
	using dataAccessRef =QSPFulfillment.DataAccess.Data.TaxRegionData;
	using QSPFulfillment.DataAccess.Common;
	/// <summary>
	///     This class contains the business rules that are used for a 
	///     collection Day.
	/// </summary>
	public class TaxRegionBusiness : QSPFulfillment.DataAccess.Business.BusinessSystem
	{
		dataAccessRef dataAccess = new dataAccessRef();

		public TaxRegionBusiness(Message messageManager) : base(messageManager) { }
		public TaxRegionBusiness(bool asMessageManager) : base(asMessageManager) { }
		
		public void SelectAll(DataTable table)
		{
			try
			{
				dataAccess.SelectAll(table);
			}
			catch (Exception ex)
			{	
				ManageError(ex);
				messageManager.ValidationExceptionType =  ExceptionType.Select;
				throw new ExceptionFulf(messageManager);
			}
		}

		protected override DBInteractionBase DataAccessReference
		{
			get{return this.dataAccess;}
		}
	}
}