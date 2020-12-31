namespace QSPFulfillment.DataAccess.Business
{
	using System;
	using System.Data;
	using System.Text.RegularExpressions;
	using QSPFulfillment.DataAccess.Common.TableDef;
	using QSPFulfillment.DataAccess.Data;
	using dataAccessRef =QSPFulfillment.DataAccess.Data.AdPageSizeData;
	using QSPFulfillment.DataAccess.Common;

	/// <summary>
	///     This class contains the business rules that are used for a 
	///     collection Day.
	/// </summary>
	public class AdPageSizeBusiness : QSPFulfillment.DataAccess.Business.BusinessSystem
	{
		dataAccessRef dataAccess = new dataAccessRef();

		public AdPageSizeBusiness(Message messageManager) : base(messageManager) { }
		public AdPageSizeBusiness(bool asMessageManager) : base(asMessageManager) { }
		
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