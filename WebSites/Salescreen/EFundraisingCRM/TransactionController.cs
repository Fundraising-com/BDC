using System;
using efundraising.Data.Sql;
using efundraising.EFundraisingCRM.DataAccess;
using System.Collections;

namespace efundraising.EFundraisingCRM
{
	/// <summary>
	/// Summary description for TransactionController.
	/// </summary>
	public class TransactionController : EFundraisingCRMLogic
	{
		public TransactionController()
		{

		}


		#region Public Methods


		public void InsertSalesForExistingClient(Client client, SaleCollection sales, CommentsCollection comments)
		{
			EFundraisingCRMDatabase dbo = new EFundraisingCRMDatabase();
			dbo.InsertSalesForExistingClient(client, sales, comments);
		}

		public void UpdateCreditCardRefundRequest(CreditCardRefundRequest ccr)
		{
			EFundraisingCRMDatabase dbo = new EFundraisingCRMDatabase();
			dbo.UpdateCreditCardRefundRequest(ccr);
		}

		public void UpdateAndInsertSalesItems(Client client, Sale sale, SalesItemCollection insertItems,
			SalesItemCollection updateItems, CommentsCollection comments)
		{
			EFundraisingCRMDatabase dbo = new EFundraisingCRMDatabase();
			dbo.UpdateAndInsertSalesItems(client, sale, insertItems, updateItems, comments);
		}

		public void InsertSalesPackByParticipants(Client client, SaleCollection sales, CommentsCollection comments)
		{
			EFundraisingCRMDatabase dbo = new EFundraisingCRMDatabase();
			dbo.InsertSalesPackByParticipants(client, sales, comments);
		}

		public void UpdateSalesAndInsertComments(SaleCollection sales, CommentsCollection comments)
		{
			EFundraisingCRMDatabase dbo = new EFundraisingCRMDatabase();
			dbo.UpdateSalesAndInsertComments(sales, comments);
		}

		public void UpdatePromotionalKitsAndInsertComments(PromotionalKitCollection promotionalKits, CommentsCollection comments)
		{
			EFundraisingCRMDatabase dbo = new EFundraisingCRMDatabase();
			dbo.UpdatePromotionalKitsAndInsertComments(promotionalKits, comments);
		}

		public void UpdateAndInsertSalesItemsPackByParticipants(Client client, SaleCollection sales, CommentsCollection comments)
		{
			EFundraisingCRMDatabase dbo = new EFundraisingCRMDatabase();
			dbo.UpdateAndInsertSalesItemsPackByParticipants(client, sales, comments);
		}

		public void InsertPaymentsForCompletedTransactions(PaymentCollection payments)
		{
			EFundraisingCRMDatabase dbo = new EFundraisingCRMDatabase();
			dbo.InsertPaymentsForCompletedTransactions(payments);
		}

		public void UpdateFedexObjects(PromotionalKit[] kits, Sale[] sales, Fedex[] fedexs)
		{
			EFundraisingCRMDatabase dbo = new EFundraisingCRMDatabase();
			dbo.UpdateFedexObjects(kits, sales, fedexs);
		}

		public void InsertPaymentsAndUpdateSales(PaymentCollection payments, SaleCollection sales)
		{
			EFundraisingCRMDatabase dbo = new EFundraisingCRMDatabase();
			dbo.InsertPaymentsAndUpdateSales(payments, sales);
		}

		public bool InsertPaymentsAndCommentsAndUpdateSales(PaymentCollection payments, SaleCollection sales, CommentsCollection comments, int createUserID, ref string message)
		{
            
			EFundraisingCRMDatabase dbo = new EFundraisingCRMDatabase();
			bool success = dbo.InsertPaymentsAndCommentsAndUpdateSales(payments, sales, comments, createUserID,ref message);
            return success;
		}

		public void InsertPaymentAndCommentsAndUpdateSale(Payment payment, Sale sale, Comments comments, int createUserID)
		{
			EFundraisingCRMDatabase dbo = new EFundraisingCRMDatabase();
			dbo.InsertPaymentAndCommentsAndUpdateSale(payment, sale, comments, createUserID);
		}

		public void InsertPromotionalKitAndPostalAddress(PromotionalKit promoKit, PostalAddress postalAddress)
		{
			EFundraisingCRMDatabase dbo = new EFundraisingCRMDatabase();
			dbo.InsertPromotionalKitAndPostalAddress(promoKit, postalAddress);
		}
		#endregion

		#region Handle Clients


		public string InsertClientAndSales(Client client, ClientActivity clientActivity, ClientAddress billingAddress, ClientAddress shippingAddress, ref SaleCollection sales, CommentsCollection comments)
		{
			EFundraisingCRMDatabase dbo = new EFundraisingCRMDatabase();
			dbo.InsertClientAndSales(client, clientActivity, billingAddress, shippingAddress, ref sales, comments);

			// return the list of salesId of the newly created sales in a "|" separated list string
			string salesIdList = "";
			foreach (Sale s in sales)
				salesIdList += s.SalesId.ToString() + "|";
			if (salesIdList.Length > 0)
				salesIdList = salesIdList.Remove(salesIdList.Length - 1, 1);
			return salesIdList;

		}

		public string InsertClientAndSalesNoTransaction(Client client, ClientActivity clientActivity, ClientAddress billingAddress, ClientAddress shippingAddress, ref SaleCollection sales, CommentsCollection comments)
		{
			EFundraisingCRMDatabase dbo = new EFundraisingCRMDatabase();
			dbo.InsertClientAndSalesNoTransaction(client, clientActivity, billingAddress, shippingAddress, ref sales, comments);

			// return the list of salesId of the newly created sales in a "|" separated list string
			string salesIdList = "";
			foreach (Sale s in sales)
				salesIdList += s.SalesId.ToString() + "|";
			if (salesIdList.Length > 0)
				salesIdList = salesIdList.Remove(salesIdList.Length - 1, 1);
			return salesIdList;

		}

		public bool UpdateClient(Client client)
		{
			EFundraisingCRMDatabase dbo = new EFundraisingCRMDatabase();
			return true;
		}

		#endregion
	}
}
