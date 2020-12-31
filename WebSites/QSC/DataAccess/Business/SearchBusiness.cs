using System;
using System.Data;

namespace QSPFulfillment.DataAccess.Business
{
	using System;
	using System.Data;
	using QSPFulfillment.DataAccess.Data;
	using dataAccessRef =QSPFulfillment.DataAccess.Data.SearchData;
	using QSPFulfillment.DataAccess.Common;

	/// <summary>
	/// Summary description for SearchBusiness.
	/// </summary>
	public class SearchBusiness:BusinessSystem
	{
		dataAccessRef dataAccess = new dataAccessRef();
		public SearchBusiness(Message MessageManager):base(MessageManager)
		{
		
		}
		public SearchBusiness(bool AsMessageManager):base(AsMessageManager)
		{
		
		}

		public void SelectSearchOrder(DataTable Table,ParameterValueList List)
		{
			try
			{
				if(List.Count != 0)
				{
					dataAccess.SelectSearchOrder(Table,List);
				}
				else
				{
					messageManager.ValidationExceptionType = ExceptionType.OtherBusinessRules;
					messageManager.SetErrorMessage(Message.ERRMSG_SEARCH_AT_LEAST_ONE_ENTRY_0);
					throw new ExceptionFulf(messageManager);
				}
			}
			catch (Exception ex)
			{	
				ManageError(ex);
				messageManager.ValidationExceptionType =  ExceptionType.Select;
				throw new ExceptionFulf(messageManager);
			}
		}
		public void SelectSearchSubscription(DataTable Table,ParameterValueList List)
		{
			try
			{
				if(List.Count != 0)
				{
					dataAccess.SelectSearchSubscription(Table,List);
				}
				else
				{
					messageManager.ValidationExceptionType = ExceptionType.OtherBusinessRules;
					messageManager.SetErrorMessage(Message.ERRMSG_SEARCH_AT_LEAST_ONE_ENTRY_0);
					throw new ExceptionFulf(messageManager);
				}
			}
			catch (Exception ex)
			{	
				ManageError(ex);
				messageManager.ValidationExceptionType =  ExceptionType.Select;
				throw new ExceptionFulf(messageManager);
			}
		}
		public void SelectSearchSubscription(DataTable Table,DataTable ListOrderID,ParameterValueList List)
		{
			try
			{
				if(ListOrderID.Rows.Count != 0)
				{
			
					dataAccess.SelectSearchSubscription(Table,ListOrderID,List);
				}
				else
				{
					messageManager.ValidationExceptionType = ExceptionType.OtherBusinessRules;
					messageManager.SetErrorMessage(Message.ERRMSG_SEARCH_AT_LEAST_ONE_ENTRY_0);
					throw new ExceptionFulf(messageManager);
				}
			}
			catch (Exception ex)
			{	
				ManageError(ex);
				messageManager.ValidationExceptionType =  ExceptionType.Select;
				throw new ExceptionFulf(messageManager);
			}
		}
		public void SelectSearchShippement(DataTable Table,ParameterValueList List)
		{
			try
			{
				if(List.Count != 0)
				{
					dataAccess.SelectSearchShippement(Table,List);
				}
				else
				{
					messageManager.ValidationExceptionType = ExceptionType.OtherBusinessRules;
					messageManager.SetErrorMessage(Message.ERRMSG_SEARCH_AT_LEAST_ONE_ENTRY_0);
					throw new ExceptionFulf(messageManager);
				}
			}
			catch (Exception ex)
			{	
				ManageError(ex);
				messageManager.ValidationExceptionType =  ExceptionType.Select;
				throw new ExceptionFulf(messageManager);
			}
		}
		public void SelectSearchMagazine(DataTable Table,ParameterValueList List)
		{
			try
			{				
					dataAccess.SelectSearchMagazine(Table,List);
			}
			catch (Exception ex)
			{	
				ManageError(ex);
				messageManager.ValidationExceptionType =  ExceptionType.Select;
				throw new ExceptionFulf(messageManager);
			}
		}
		public void SelectSearchProduct(DataTable Table,DataTable ListOrderID,int ItemType)
		{
			try
			{				
				dataAccess.SelectSearchProduct(Table,ListOrderID,(int)ItemType);
			}
			catch (Exception ex)
			{	
				ManageError(ex);
				messageManager.ValidationExceptionType =  ExceptionType.Select;
				throw new ExceptionFulf(messageManager);
			}
		}
		public void SelectSearchCreditCard(DataTable Table,ParameterValueList List)
		{
			try
			{
				if(List.Count != 0)
				{
					dataAccess.SelectSearchCreditCard(Table,List);
				}
				else
				{
					messageManager.ValidationExceptionType = ExceptionType.OtherBusinessRules;
					messageManager.SetErrorMessage(Message.ERRMSG_SEARCH_AT_LEAST_ONE_ENTRY_0);
					throw new ExceptionFulf(messageManager);
				}
			}
			catch (Exception ex)
			{	
				ManageError(ex);
				messageManager.ValidationExceptionType =  ExceptionType.Select;
				throw new ExceptionFulf(messageManager);
			}
			
		}
		public void SelectSearchCreditCardDetails(DataTable Table,DataTable List)
		{

			try
			{				
				dataAccess.SelectSearchCreditCardDetails(Table,List);
			}
			catch (Exception ex)
			{	
				ManageError(ex);
				messageManager.ValidationExceptionType =  ExceptionType.Select;
				throw new ExceptionFulf(messageManager);
			}
		}

		public void SelectCampaignsForStatementsByAdjustmentBatchID(DataTable Table, int AdjustmentBatchID) 
		{
			try
			{				
				dataAccess.SelectCampaignsForStatementsByAdjustmentBatchID(Table, AdjustmentBatchID);
			}
			catch (Exception ex)
			{	
				ManageError(ex);
				messageManager.ValidationExceptionType =  ExceptionType.Select;
				throw new ExceptionFulf(messageManager);
			}
		}

        public void SelectCampaignsForCAStatement(DataTable Table, int StatementPrintRequestBatchID) 
		{
			try
			{
                dataAccess.SelectCampaignsForCAStatement(Table, StatementPrintRequestBatchID);
			}
			catch (Exception ex)
			{	
				ManageError(ex);
				messageManager.ValidationExceptionType =  ExceptionType.Select;
				throw new ExceptionFulf(messageManager);
			}
		}

		public void SelectCCProcessList(DataTable table) 
		{
			try 
			{
				dataAccess.SelectCCProcessList(table);
			} 
			catch(Exception ex) 
			{
				ManageError(ex);
				messageManager.ValidationExceptionType =  ExceptionType.Select;
				throw new ExceptionFulf(messageManager);
			}
		}

		public void UpdateCCProcessList(string creditCardNumber, string authorizationCode) 
		{
			try 
			{
				dataAccess.UpdateCCProcessList(creditCardNumber, authorizationCode);
			} 
			catch(Exception ex) 
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
