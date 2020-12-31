namespace QSPFulfillment.DataAccess.Business
{
	using System;
	using System.Data;
	using QSPFulfillment.DataAccess.Common.TableDef;
	using QSPFulfillment.DataAccess.Data;
	using dataAccessRef =QSPFulfillment.DataAccess.Data.ReportRequestBatch_OrderEntryFollowupReportData;
	using DataAccess.Common;
	using QSPFulfillment.DataAccess.Common.ActionObject;
	/// <summary>
	///     This class contains the business rules that are used for a 
	///     collection Day.
	/// </summary>
	public class ReportRequestBatch_OrderEntryFollowupReportBusiness : QSPFulfillment.DataAccess.Business.BusinessSystem
	{
		dataAccessRef dataAccess = new dataAccessRef();
		public ReportRequestBatch_OrderEntryFollowupReportBusiness(Message MessageManager):base(MessageManager)
		{
		
		}
		public ReportRequestBatch_OrderEntryFollowupReportBusiness(bool AsMessageManager):base(AsMessageManager)
		{
		
		}

		public void SelectOne(DataTable table, int customerOrderHeaderInstance)
		{
			try
			{
				dataAccess.SelectOne(table, customerOrderHeaderInstance);
			}
			catch (Exception ex)
			{	
				ManageError(ex);
				messageManager.ValidationExceptionType =  ExceptionType.Select;
				throw new ExceptionFulf(messageManager);
			}
		}

        public void Requeue(int orderID)
        {
            try
            {
                dataAccess.Requeue(orderID);
            }
            catch (Exception ex)
            {
                ManageError(ex);
                messageManager.ValidationExceptionType = ExceptionType.Select;
                throw new ExceptionFulf(messageManager);
            }
        }

		protected override DBInteractionBase DataAccessReference
		{
			get{return this.dataAccess;}
		}
	}
}